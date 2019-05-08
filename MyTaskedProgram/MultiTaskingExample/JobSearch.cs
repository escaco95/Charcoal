using Charcoal.Task;
using System.Windows.Forms;

namespace MyTaskedProgram.MultiTaskingExample
{
    class JobSearch : Job
    {
        public volatile JobSearch ParentJob;
        public volatile int ChildJobRemaining;
        public volatile string Path;
        public volatile TreeNode Root;
    }
}