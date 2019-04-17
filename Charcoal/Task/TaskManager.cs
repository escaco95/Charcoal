using System.Collections.Generic;

namespace Charcoal.Task
{
    /// <summary>
    /// Task, ControlTask로부터 파생된 모든 작업 인스턴스를 관리합니다
    /// </summary>
    public partial class TaskManager
    {
        private List<Task> _tasks = new List<Task>();
        /// <summary>
        /// 인스턴스가 관리 중인 모든 작업 개체 목록
        /// </summary>
        public List<Task> Tasks { get { return _tasks; } }
    }
    public partial class TaskManager
    {
        public void StopAsync()
        {
            foreach(var task in _tasks)
            {
                if (task.TaskState != TaskState.Running)
                    continue;
                task.StopAsync();
            }
        }
    }
}
