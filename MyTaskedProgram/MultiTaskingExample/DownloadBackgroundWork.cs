using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Charcoal.Task;

namespace MyTaskedProgram.MultiTaskingExample
{
    class DownloadBackgroundWork : BackgroundWork
    {
        public volatile int _progress = 0;
        public int Progress { get { return _progress; } }
        public DownloadBackgroundWork(bool supportsCancellation, bool reportsProgress) : base(supportsCancellation, reportsProgress) { }
        protected override void DoWork(object sender, DoWorkEventArgs e)
        {
            _progress = 0;
            for(int i = 0; i < 100000000; i++)
            {
                _progress = i;
            }
        }
    }
}