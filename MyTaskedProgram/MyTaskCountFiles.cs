using Charcoal.Task;
using System.Windows.Forms;

namespace MyTaskedProgram
{
    partial class MyTaskCountFiles : ControlTask
    {
        public volatile string DirectoryPath;
        public volatile string[] Files;
        protected override void OnTaskStart()
        {
            var path = DirectoryPath;
            Files = System.IO.Directory.GetFiles(path);
        }
    }
    // 생성자
    partial class MyTaskCountFiles : ControlTask
    {
        public MyTaskCountFiles(Control parent) : base(parent) { }
    }
}
