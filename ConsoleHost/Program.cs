using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JenkinsListener;
using JenkinsListenerService;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Jenkins Listener");
            var listener = new Listener();
            listener.Start();
            Console.WriteLine("Started. Any key to stop");
            Console.ReadKey();
            listener.Stop();
        }
    }
}
