using System.ServiceProcess;
using JenkinsListener;

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
