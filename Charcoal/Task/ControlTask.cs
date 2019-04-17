using System.Windows.Forms;

namespace Charcoal.Task
{
    /// <summary>
    /// 비동기 작업 단위를 만들고 제어합니다
    /// <para>작업 단위 : 비동기적</para>
    /// <para>작업 결과 : ParentControl과 동기</para>
    /// </summary>
    public abstract class ControlTask : Task
    {
        /// <summary>
        /// 새 작업 단위 인스턴스를 생성합니다
        /// </summary>
        /// <param name="parent">이벤트의 동기화 대상이 되는 부모 컨트롤</param>
        public ControlTask(Control parent){_parent = parent;}
        private volatile Control _parent;
        public Control Parent { get { return _parent; } }
        protected override void OnTaskStoppedAsync()
        {
            Control par = this._parent;
            if (par == null) return;
            if (par.InvokeRequired)
                par.BeginInvoke(new System.Action(() => this.OnTaskStopped()));
        }
        protected override void OnTaskStoppingAsync()
        {
            Control par = this._parent;
            if (par == null) return;
            if (par.InvokeRequired)
                par.BeginInvoke(new System.Action(() => this.OnTaskStopping()));
        }
        protected override void OnTaskUpdateAsync()
        {
            Control par = this._parent;
            if (par == null) return;
            if (par.InvokeRequired)
                par.BeginInvoke(new System.Action(() => this.OnTaskUpdate()));
        }
        protected virtual void OnTaskStopped() { TaskStopped?.Invoke(this); }
        protected virtual void OnTaskStopping() { TaskStopping?.Invoke(this); }
        protected virtual void OnTaskUpdate() { TaskUpdate?.Invoke(this); }
        /// <summary>
        /// 동기 이벤트 : 인스턴스의 TaskState값이 TaskState.Stopping으로 변경되면 격발됩니다
        /// </summary>
        public event TaskEventHandler TaskStopped;
        /// <summary>
        /// 동기 이벤트 : 인스턴스의 TaskState값이 TaskState.Stopping으로 변경되면 격발됩니다
        /// </summary>
        public event TaskEventHandler TaskStopping;
        /// <summary>
        /// 동기 이벤트 : 인스턴스가 비동기 작업 도중 Update()를 호출하면 격발됩니다
        /// </summary>
        public event TaskEventHandler TaskUpdate;
    }
}