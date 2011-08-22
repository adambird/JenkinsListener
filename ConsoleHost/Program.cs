using System;
using JenkinsListener;

namespace ConsoleHost
{
    static class Program
    {
        static void Main()
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
