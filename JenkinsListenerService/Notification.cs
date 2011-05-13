using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Text;
using Newtonsoft.Json;

namespace JenkinsListenerService
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
            if (Build.Status == Jenkins.BuildStatus.Failed)
            {
                Trace.TraceInformation("Doing nothing as build failed");
            }
            else
            {
                Trace.TraceInformation("Executing {0}", Configuration.ScriptFile);

                try
                {
                    using (var runspace = InitializeRunspace())
                    {
                        runspace.Open();

                        using (var pipeline = runspace.CreatePipeline())
                        {

                            pipeline.Commands.AddScript(GetFileContents(Configuration.ScriptFile));

                            var results = pipeline.Invoke();

                            foreach (var result in results)
                            {
                                Trace.TraceInformation(result.ToString());
                            }
                        }
                    }

                    Trace.TraceInformation("Execution complete", Configuration.ScriptFile);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                    throw;
                }
            }
        }

        private static Runspace InitializeRunspace()
        {
            var config = RunspaceConfiguration.Create();
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
            return RunspaceFactory.CreateRunspace(config);
        }

        private static string GetFileContents(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
