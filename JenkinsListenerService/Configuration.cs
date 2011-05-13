using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace JenkinsListenerService
{
    public class Configuration
    {
        public static int ListenerPort
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ListenerPort"]); }
        }

        public static string ScriptFile
        {
            get { return ConfigurationManager.AppSettings["ScriptFile"]; }   
        }

        public static string SnapIns
        {
            get { return ConfigurationManager.AppSettings["SnapIns"]; }   
        }

    }
}
