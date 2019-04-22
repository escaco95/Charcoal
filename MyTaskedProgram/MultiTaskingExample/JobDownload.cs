using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Charcoal.Task;

namespace MyTaskedProgram.MultiTaskingExample
{
    class JobDownload : Job
    {
        public volatile int Progress = 0;
    }
}