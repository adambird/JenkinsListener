using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation.Runspaces;
using Newtonsoft.Json;

namespace JenkinsListener
{
    public class Notification
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public Build Build { get; set; }

        public static Notification New(string source)
        {
            return JsonConvert.DeserializeObject<Notification>(source);
        }

        public void Execute()
        {
            if (Build.Status == Jenkins.BuildStatus.Success && Build.Phase == Jenkins.BuildPhase.Finished)
            {
                Trace.TraceInformation("Executing script for job [{0}] at Url in directory [{1}]", Name, Url, Configuration.ScriptFileDirectory);

                try
                {
                    using (var runspace = InitializeRunspace())
                    {
                        runspace.Open();

                        using (var pipeline = runspace.CreatePipeline())
                        {

                            pipeline.Commands.AddScript(GetScriptFileContentForJobUrl(Name));

                            var results = pipeline.Invoke();

                            foreach (var result in results)
                            {
                                Trace.TraceInformation(result.ToString());
                            }
                        }
                    }

                    Trace.TraceInformation("Execution of script for job [{0}] for Url [{1}] complete", Name, Url);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                    throw;
                }
            }
            else
            {
                Trace.TraceInformation("Not Executing as Notification Status:{0} Phase:{1}", Build.Status, Build.Phase);
            }
        }

        private static Runspace InitializeRunspace()
        {
            var config = RunspaceConfiguration.Create();
            if (!String.IsNullOrEmpty(Configuration.SnapIns))
            {
                var snapins = Configuration.SnapIns.Split(",".ToCharArray());

                foreach (var snapin in snapins)
                {
                    PSSnapInException warning;
                    config.AddPSSnapIn(snapin, out warning);
                    if (warning != null)
                    {
                        Trace.TraceError("Error loading snapin {0} - {1}", snapin, warning.Message);
                    }
                }
            }
            return RunspaceFactory.CreateRunspace(config);
        }

        internal static string GetScriptFileContentForJobUrl(string jobName)
        {
            var filename = String.Concat(Configuration.ScriptFileDirectory, jobName,".ps1");
            return File.ReadAllText(filename);
        }
    }
}
