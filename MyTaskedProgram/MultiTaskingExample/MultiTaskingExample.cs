using System;
using Charcoal.Task;
using System.Windows.Forms;
using System.Threading;

namespace MyTaskedProgram.MultiTaskingExample
{
    public partial class MultiTaskingExample : Form
    {
        public MultiTaskingExample()
        {
            InitializeComponent();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //인터페이스 초기화
            this.button3.Enabled = false;
            this.button3.Text = "다운로드 중...";
            StartAsync(this.progressBar1);
            StartAsync(this.progressBar2);
            StartAsync(this.progressBar3);
            StartAsync(this.progressBar4);
            StartAsync(this.progressBar5);
        }
        void StartAsync(ProgressBar bar)
        {
            //작업 인터페이스 초기화
            bar.Value = 0;

            //작업에 대한 정보 job 초기화
            JobDownload job = new JobDownload();

            //작업에 대한 정보 job 을 기반으로, 작업의 '감독관' 생성
            BackgroundJob<JobDownload> watcher = new BackgroundJob<JobDownload>(job, false, true);
            //작업에 대한 정보 job 을 기반으로, 작업의 '일꾼' 생성
            BackgroundJob<JobDownload> worker = new BackgroundJob<JobDownload>(job);

            //무명 메소드를 사용하는 방법을 통해 작업 감독관이 해야 할 일을 정의
            watcher.DoWork += (senderWorker, eWorker) =>
            {
                JobDownload Job = watcher.JobInfo;
                while (Job.Watch(100))
                    watcher.ReportProgress();
            };
            watcher.ProgressChanged += (senderWorker, eWorker) =>
            {
                bar.Value = watcher.JobInfo.Progress;
            };
            watcher.RunWorkerCompleted += (senderWorker, eWorker) =>
            {
                bar.Value = bar.Maximum;
                this.button3.Text = "다운로드";
                this.button3.Enabled = true;
                worker.Dispose();
                watcher.Dispose();
            };
            //무명 메소드를 사용하는 방법을 통해 작업 일꾼이 해야 할 일을 정의
            worker.DoWork += (senderWorker, eWorker) =>
            {
                JobDownload Job = worker.JobInfo;
                for (int i = 0; i < 1000000000; i++)
                    Job.Progress = i;
            };
            worker.RunWorkerCompleted += (senderWorker, eWorker) =>
            {
                worker.JobInfo.Finish();
            };

            //감독관이 감독 작업 시작
            watcher.RunWorkerAsync();
            //일꾼이 노가다 작업 시작
            worker.RunWorkerAsync();
        }
    }
}