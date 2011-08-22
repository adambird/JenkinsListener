using System.ServiceProcess;

namespace JenkinsListenerService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var servicesToRun = new ServiceBase[] 
                                              { 
                                                  new ListenerHost() 
                                              };
            ServiceBase.Run(servicesToRun);
        }
    }
}
