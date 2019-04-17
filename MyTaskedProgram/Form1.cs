using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTaskedProgram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Charcoal.Task.TaskManager TaskMgr = new Charcoal.Task.TaskManager();

        private void Button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            if (!Directory.Exists(TBPath.Text))
            {
                listBox1.Items.Add($"오류: 폴더가 존재하지 않음");
                return;
            }
            //컨트롤 사용 중단
            TBPath.Enabled = false;
            button1.Enabled = false;

            listBox1.Items.Add($"작업 초기화");

            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;

            MyTaskCountFiles counter = new MyTaskCountFiles(this);
            counter.DirectoryPath = TBPath.Text;
            counter.TaskStopped += TaskCounterDone;
            TaskMgr.Tasks.Add(counter);

            listBox1.Items.Add($"탐색 중: {counter.DirectoryPath}");
            counter.Start();
            /*
            MySampleTask task = new MySampleTask(this);
            task.TaskUpdate += OnTaskUpdate;
            task.TaskStopped += OnTaskStopped;
            task.MAX = progressBar1.Maximum;
            TaskMgr.Tasks.Add(task);

            MySampleTaskUpdater updater = new MySampleTaskUpdater(this);
            updater.UpdateTarget = task;
            TaskMgr.Tasks.Add(updater);

            task.Start();
            updater.Start();
            */
        }

        int WorkingThread = 0;
        int MaxProcessor = 0;
        MyTaskConvertImageFile[] Converters = null;

        void TaskCounterDone(object sender)
        {
            MyTaskCountFiles counter = sender as MyTaskCountFiles;
            TaskMgr.Tasks.Remove(counter);

            string[] files = counter.Files;
            progressBar1.Maximum = files.Count();

            int processor = Environment.ProcessorCount;
            if (processor < 1)
            {
                listBox1.Items.Add($"오류: 이 장치에 가용 가능한 논리 프로세서가 없음");
                EntireTaskFinished();
                return;
            }
            listBox1.Items.Add($"탐색된 파일 {progressBar1.Maximum} 개에 대한 일괄 처리 개시");
            listBox1.Items.Add($"일괄 처리에 프로세서 {processor}개 가용");
            MaxProcessor = processor;
            WorkingThread = processor;
            Converters = new MyTaskConvertImageFile[processor];
            listBox1.Items.Add($"처리 중: {progressBar1.Value}/{progressBar1.Maximum}");

            Queue<string>[] subfiles = new Queue<string>[processor];
            for (int i = 0; i < MaxProcessor; i++)
                subfiles[i] = new Queue<string>();
            {
                int imax = progressBar1.Maximum;
                int splt = 0;
                for (int i = 0; i < imax; i++)
                {
                    subfiles[splt].Enqueue(files[i]);
                    splt++;
                    if (splt >= MaxProcessor)
                        splt = 0;
                }
            }

            for (int i = 0; i < MaxProcessor; i++)
            {
                MyTaskConvertImageFile converter = new MyTaskConvertImageFile(this);
                converter.Files = subfiles[i].ToArray();
                converter.TaskUpdate += TaskConverterUpdate;
                converter.TaskStopped += TaskConverterDone;
                TaskMgr.Tasks.Add(converter);

                Converters[i] = converter;

                MyTaskUpdater updater = new MyTaskUpdater(this);
                updater.UpdateTarget = converter;
                updater.UpdateDelay = 500 + 25 * i;

                converter.Start();
                updater.Start();
            }
        }
        void TaskConverterDone(object sender)
        {
            MyTaskConvertImageFile converter = sender as MyTaskConvertImageFile;
            TaskMgr.Tasks.Remove(converter);
            WorkingThread--;
            if (WorkingThread > 0)
                return;
            progressBar1.Value = progressBar1.Maximum;
            listBox1.Items[listBox1.Items.Count - 1] = $"처리 중: {progressBar1.Value}/{progressBar1.Maximum}";
            int valueSum = 0;
            int convSum = 0;
            int failSum = 0;
            for (int i = 0; i < MaxProcessor; i++)
            {
                valueSum += Converters[i].Value;
                convSum += Converters[i].Converted;
                failSum += Converters[i].Failed;
            }
            listBox1.Items.Add($"처리 완료. {valueSum}개 중 {convSum}개 성공, {failSum}개 실패");

            EntireTaskFinished();
        }
        void TaskConverterUpdate(object sender)
        {
            MyTaskConvertImageFile converter = sender as MyTaskConvertImageFile;
            int valueSum = 0;
            for (int i = 0; i < MaxProcessor; i++)
                valueSum += Converters[i].Value;
            progressBar1.Value = valueSum;
            listBox1.Items[listBox1.Items.Count - 1] = $"처리 중: {progressBar1.Value}/{progressBar1.Maximum}";
        }

        void EntireTaskFinished()
        {
            TBPath.Enabled = true;
            button1.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            TaskMgr.StopAsync();
        }
    }
}
