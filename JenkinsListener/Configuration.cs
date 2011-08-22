using System.Configuration;

namespace JenkinsListener
{
    public static class Configuration
    {
        public static int ListenerPort
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ListenerPort"]); }
        }

        public static string SnapIns
        {
            get { return ConfigurationManager.AppSettings["SnapIns"]; }   
        }

        public static string ScriptFileDirectory
        {
            get { return ConfigurationManager.AppSettings["ScriptFileDirectory"]; }
        }
    }
}
