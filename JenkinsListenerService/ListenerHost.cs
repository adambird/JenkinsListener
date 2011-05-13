using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace JenkinsListenerService
{
    public partial class ListenerHost : ServiceBase
    {
        private Listener _listener;


        public ListenerHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _listener = new Listener();
            _listener.Start();
        }

        protected override void OnStop()
        {
            _listener.Stop();
            _listener = null;
        }

    }
}
