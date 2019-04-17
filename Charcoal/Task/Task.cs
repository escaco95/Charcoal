using System.Threading;

namespace Charcoal.Task
{
    /// <summary>
    /// Task의 동작 상태
    /// </summary>
    public enum TaskState
    {
        /// <summary>
        /// Task가 아무 일도 하고 있지 않음
        /// </summary>
        Idle,
        /// <summary>
        /// Task가 작동 중임
        /// </summary>
        Running,
        /// <summary>
        /// Task가 비동기 정지(StopAsync) 요청을 받아 정지 중임
        /// </summary>
        Stopping,
        /// <summary>
        /// Task가 완전히 정지됨
        /// </summary>
        Stopped = Idle
    };
    /// <summary>
    /// 비동기 작업 단위를 만들고 제어합니다
    /// <para>작업 단위 : 비동기적</para>
    /// <para>작업 결과 : 비동기 이벤트 격발</para>
    /// </summary>
    public abstract partial class Task
    {
        private volatile TaskState _state = TaskState.Idle;
        /// <summary>
        /// 현재 인스턴스의 TaskState를 반환합니다
        /// </summary>
        public TaskState TaskState { get { return _state; } }
        public bool IsTaskStopping() { return _state == TaskState.Stopping; }
        public bool IsTaskRunning() { return _state == TaskState.Running; }
        public bool IsTaskStopped() { return _state == TaskState.Stopped; }
        public bool IsTaskIdle() { return _state == TaskState.Idle; }
        /// <summary>
        /// 운영 체제에서 현재 인스턴스의 상태를 TaskState.Running으로 변경하도록 지시합니다
        /// </summary>
        /// <exception cref="ThreadStateException"/>
        public void Start()
        {
            if (_state != TaskState.Idle)
                throw new ThreadStateException("작업의 TaskState값이 Idle인 상태에서만 Start될 수 있습니다");
            _state = TaskState.Running;
            Thread t = new Thread(new ThreadStart(TaskProc));
            t.Start();
        }
        /// <summary>
        /// 운영 체제에서 현재 인스턴스의 상태를 TaskState.Stopping으로 변경하도록 지시합니다
        /// </summary>
        /// <exception cref="ThreadStateException"/>
        public void StopAsync()
        {
            if (_state != TaskState.Running)
                throw new ThreadStateException("작업의 TaskState값이 Running인 상태에서만 StopAsync될 수 있습니다");
            _state = TaskState.Stopping;
        }
        /// <summary>
        /// 현재 작업 상태를 Update하여 TaskUpdate(+Async)이벤트를 호출하도록 지시합니다
        /// </summary>
        public void Update()
        {
            if (_state != TaskState.Running)
                throw new ThreadStateException("작업의 TaskState값이 Running인 상태에서만 Update될 수 있습니다");
            OnTaskUpdateAsync();
        }
        private void TaskProc()
        {
            OnTaskStart();
            OnTaskStoppingAsync();
            _state = TaskState.Stopped;
            OnTaskStoppedAsync();
        }
        /// <summary>
        /// 현재 인스턴스 작업이 Running 되었을 때 별도의 스레드에서 동작할 메소드입니다
        /// </summary>
        protected abstract void OnTaskStart();
    }
    /// <summary>
    /// Task 객체의 이벤트 정의 영역입니다
    /// </summary>
    public abstract partial class Task
    {
        protected virtual void OnTaskStoppedAsync() { TaskStoppedAsync?.Invoke(this); }
        protected virtual void OnTaskStoppingAsync() { TaskStoppingAsync?.Invoke(this); }
        protected virtual void OnTaskUpdateAsync() { TaskUpdateAsync?.Invoke(this); }
        /// <summary>
        /// 비동기 이벤트 : 인스턴스의 TaskState값이 TaskState.Stopping으로 변경되면 격발됩니다
        /// </summary>
        public event TaskEventHandler TaskStoppingAsync;
        /// <summary>
        /// 비동기 이벤트 : 인스턴스의 TaskState값이 TaskState.Stopped으로 변경되면 격발됩니다
        /// </summary>
        public event TaskEventHandler TaskStoppedAsync;
        /// <summary>
        /// 비동기 이벤트 : 인스턴스가 비동기 작업 도중 Update()를 호출하면 격발됩니다
        /// </summary>
        public event TaskEventHandler TaskUpdateAsync;
    }
    /// <summary>
    /// 작업 이벤트를 나타냅니다
    /// </summary>
    /// <param name="sender">이벤트를 작동시킨 작업 인스턴스</param>
    public delegate void TaskEventHandler(object sender);
}