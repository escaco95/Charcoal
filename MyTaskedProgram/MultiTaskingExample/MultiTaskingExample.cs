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

        private void Button1_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (!worker.CancellationPending) ;
            e.Cancel = true;
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show("Cancelled");
            else
                MessageBox.Show("WHAT?");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.button3.Enabled = false;
            this.progressBar1.Value = 0;
            this.backgroundWorker1.RunWorkerAsync();
            while (this.backgroundWorker1.IsBusy)
            {
                progressBar1.Increment(1);
                Application.DoEvents();
            }
            this.button3.Enabled = true;
        }
    }
}