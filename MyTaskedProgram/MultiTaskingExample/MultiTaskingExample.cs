using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTaskedProgram.MultiTaskingExample
{
    public partial class MultiTaskingExample : Form
    {
        public MultiTaskingExample()
        {
            InitializeComponent();
        }

        DownloadBackgroundWork _worker = new DownloadBackgroundWork(true,false);

        private void Button3_Click(object sender, EventArgs e)
        {
            this.button3.Enabled = false;
            this.progressBar1.Value = 0;
            this._worker.RunAsync();
            while (this._worker.IsBusy)
            {
                progressBar1.Value = _worker.Progress;
                Application.DoEvents();
            }
            progressBar1.Value = _worker.Progress;
            this.button3.Enabled = true;
        }
    }
}