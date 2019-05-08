using System;
using Charcoal.Extensions;
using Charcoal.Task;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Collections.Generic;

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
            DoLeftJob(TV1, "c:\\");
            DoRightJob(TV2, "c:\\");
        }
        void DoLeftJob(TreeView tv, string topdir)
        {
            tv.Nodes.Clear();
            TreeNode node = new TreeNode(topdir);
            JobSearch job = new JobSearch() { Path = topdir, Root = node };
            BackgroundJob<JobSearch> worker = new BackgroundJob<JobSearch>(job);
            worker.DoWork += delegate
            {
                JobSearch Job = worker.JobInfo;
                var stack = new Stack<TreeNode>();
                var rootDirectory = new DirectoryInfo(Job.Path);
                node.Tag = rootDirectory;
                stack.Push(node);

                while (stack.Count > 0)
                {
                    var currentNode = stack.Pop();
                    var directoryInfo = (DirectoryInfo)currentNode.Tag;
                    DirectoryInfo[] dirs;
                    try
                    {
                        dirs = directoryInfo.GetDirectories();
                        foreach (var directory in dirs)
                        {
                            var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
                            currentNode.Nodes.Add(childDirectoryNode);
                            stack.Push(childDirectoryNode);
                        }
                    }
                    catch { }
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                worker.JobInfo.Finish();
                tv.SuspendLayout();
                tv.Nodes.Add(node);
                //tv.ExpandAll();
                tv.ResumeLayout();
            };
            worker.RunWorkerAsync();
        }
        void DoRightJob(TreeView tv, string topdir)
        {
            tv.Nodes.Clear();
            TreeNode node = new TreeNode(topdir);
            JobSearch job = new JobSearch() { Path = topdir, Root = node, ChildJobRemaining = 0, };
            BackgroundJob<JobSearch> worker = new BackgroundJob<JobSearch>(job);
            worker.DoWork += delegate
            {
                JobSearch Job = worker.JobInfo;
                var rootDirectory = new DirectoryInfo(topdir);
                node.Tag = rootDirectory;
                foreach (var dir in rootDirectory.GetDirectories())
                {
                    TreeNode dirnode = new TreeNode(dir.Name);
                    dirnode.Tag = dir;
                    Job.Root.Nodes.Add(dirnode);
                    Job.ChildJobRemaining++;
                    if (this.InvokeRequired)
                        this.BeginInvoke(new Action(() => DoSubJob(Job, dirnode, dir.FullName)));
                    Thread.Sleep(100);
                }
                while (Job.ChildJobRemaining > 0)
                    Thread.Sleep(100);
            };
            worker.RunWorkerCompleted += delegate
            {
                worker.JobInfo.Finish();
                tv.SuspendLayout();
                tv.Nodes.Add(node);
                //tv.ExpandAll();
                tv.ResumeLayout();
            };
            worker.RunWorkerAsync();
        }
        void DoSubJob(JobSearch parentJob, TreeNode tp, string topdir)
        {
            JobSearch job = new JobSearch() { Path = topdir, Root = tp, ParentJob = parentJob, ChildJobRemaining = 0 };
            BackgroundJob<JobSearch> worker = new BackgroundJob<JobSearch>(job);
            worker.DoWork += delegate
            {
                JobSearch Job = worker.JobInfo;
                var rootDirectory = tp.Tag as DirectoryInfo;
                try
                {
                    var dirs = rootDirectory.GetDirectories();
                    foreach (var dir in dirs)
                    {
                        TreeNode dirnode = new TreeNode(dir.Name);
                        dirnode.Tag = dir;
                        Job.Root.Nodes.Add(dirnode);
                        if (this.InvokeRequired)
                            this.BeginInvoke(new Action(() => DoSubJob(Job, dirnode, dir.FullName)));
                        Thread.Sleep(100);
                    }
                }
                catch { }
                while (Job.ChildJobRemaining > 0)
                    Thread.Sleep(100);
            };
            worker.RunWorkerCompleted += delegate
            {
                worker.JobInfo.Finish();
                worker.JobInfo.ParentJob.ChildJobRemaining--;
            };
            worker.RunWorkerAsync();
        }
    }
}