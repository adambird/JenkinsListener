using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JenkinsListenerService
{
    public class Build
    {
        public int Number { get; set; }
        public string Phase { get; set; }
        public string Status { get; set; }
    }
}
