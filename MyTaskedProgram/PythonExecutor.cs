using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTaskedProgram
{
    public partial class PythonExecutor : Form
    {
        public PythonExecutor()
        {
            InitializeComponent();
        }

        Process Python = null;

        private void Button1_Click(object sender, EventArgs e)
        {
            TimeEvaluate.Start();
            Process process = new Process();
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.WorkingDirectory = Path.GetDirectoryName(TBpy.Text);
                process.StartInfo.FileName = Path.Combine(Environment.SystemDirectory, "cmd.exe");

                process.StartInfo.RedirectStandardInput = true;

                process.OutputDataReceived += ProcessOutputDataHandler;
                process.ErrorDataReceived += ProcessErrorDataHandler;

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.StandardInput.WriteLine(TBexe.Text + " -u " + TBpy.Text);
            }
            Python = process;
        }

        char[] OutputTokenSeperator = new char[]{' '};
        bool OutputEpochCalled = false;
        int OutputEpochCurrent = 0;
        int OutputEpochMaximum = 0;
        bool OutputTrainCalled = false;
        int OutputTrainCurrent = 0;
        int OutputTrainMaximum = 0;

        Stopwatch TimeEvaluate = new Stopwatch();

        public void ParseCmdOutput(string s)
        {
            if (s == null)
            {
                TimeEvaluate.Stop();
                LBOutput.Items.Add("기계학습 종료");
            }
            if (s.Length == 0)
                return;
            string[] token = s.Split(OutputTokenSeperator, 2);
            if (token.Count() == 0)
                return;
            switch(token[0])
            {
                case "Epoch":
                    {
                        string[] msg = token[1].Split('/');
                        OutputEpochCurrent = Int32.Parse(msg[0]);
                        OutputEpochMaximum = Int32.Parse(msg[1]);
                        LBOutput.Items.Add(s);
                        OutputEpochCalled = true;
                        OutputTrainCalled = false;
                    }
                    break;
                default:
                    {
                        if (!OutputEpochCalled)
                            return;
                        string ts = s.Trim();
                        token = ts.Split(OutputTokenSeperator, 2);
                        if (token.Count() < 2)
                            return;
                        string[] msg = token[0].Split('/');
                        if (msg.Count() < 2)
                            return;
                        OutputTrainCurrent = Int32.Parse(msg[0]);
                        OutputTrainMaximum = Int32.Parse(msg[1]);
                        PBoutput.Maximum = (OutputEpochMaximum) * OutputTrainMaximum;
                        PBoutput.Value = (OutputEpochCurrent - 1) * OutputTrainMaximum + OutputTrainCurrent;
                        double passed = (double)TimeEvaluate.ElapsedMilliseconds / 1000D;
                        double perc = (double)(PBoutput.Maximum - PBoutput.Value) / (double)PBoutput.Maximum;
                        double max = (double)passed / (double)Math.Max(1, PBoutput.Value) * PBoutput.Maximum;
                        int icur = (int)passed;
                        int ileft = (int)(max * perc);
                        LOutput.Text = $"남은 시간: {ileft / 3600}시간 {(ileft / 60) % 60}분 {ileft % 60}초\n경과 시간: {icur/3600}시간 {(icur/60)%60}분 {icur%60}초";
                        if (OutputTrainCalled)
                            LBOutput.Items[LBOutput.Items.Count - 1] = s;
                        else
                            LBOutput.Items.Add(s);
                        OutputTrainCalled = true;
                    }
                    break;
            }
        }

        public void ProcessOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (InvokeRequired)
                this.Invoke(new Action(() => ParseCmdOutput(outLine.Data)));
        }

        public void ProcessErrorDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (InvokeRequired)
                this.Invoke(new Action(() => ParseCmdOutput(outLine.Data)));
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Python.Kill();
        }
    }
}