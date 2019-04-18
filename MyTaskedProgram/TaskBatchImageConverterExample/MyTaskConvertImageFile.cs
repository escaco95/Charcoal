using Charcoal.Task;
using System.Windows.Forms;

namespace MyTaskedProgram
{
    // 실제 작업 기능
    partial class MyTaskConvertImageFile : ControlTask
    {
        public volatile string[] Files = null;
        public volatile int Value = 0;
        public volatile int Failed = 0;
        public volatile int Converted = 0;
        protected override void OnTaskStart()
        {
            string[] files = Files;

            foreach (var file in files)
            {
                Value++;
                if (!System.IO.File.Exists(file))
                    continue;
                if (System.IO.Path.GetExtension(file)=="bmp")
                {
                    Converted++;
                    continue;
                }
                try
                {
                    using(System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(file))
                    {
                        string folderp = System.IO.Path.GetDirectoryName(file);
                        string filep = System.IO.Path.GetFileNameWithoutExtension(file);
                        bmp.Save($"{folderp}\\{filep}.bmp");
                    }
                    System.IO.File.Delete(file);
                    Converted++;
                }
                catch { Failed++; continue; }
            }
        }
    }
    // 생성자만
    partial class MyTaskConvertImageFile : ControlTask
    {
        public MyTaskConvertImageFile(Control parent) : base(parent) { }
    }
    //동적 업데이터
    // 실제 작업 기능
    partial class MyTaskUpdater : ControlTask
    {
        public volatile int UpdateDelay = 100;
        public volatile Task UpdateTarget = null;
        protected override void OnTaskStart()
        {
            int delay = UpdateDelay;
            while (true)
            {
                Task udt = UpdateTarget;
                if (udt.TaskState != TaskState.Running)
                    return;
                udt.Update();
                System.Threading.Thread.Sleep(delay);
                if (IsTaskStopping())
                    return;
            }
        }
    }
    // 생성자만
    partial class MyTaskUpdater : ControlTask
    {
        public MyTaskUpdater(Control parent) : base(parent) { }
    }
}