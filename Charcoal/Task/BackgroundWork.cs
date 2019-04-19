using System;
using System.ComponentModel;

namespace Charcoal.Task
{
    /// <summary>
    /// <see cref="BackgroundWorker"/>기반의 백그라운드 작업을 관리하는 개체입니다.
    /// </summary>
    public abstract partial class BackgroundWork : IDisposable
    {
        private volatile BackgroundWorker _worker = new BackgroundWorker();
        /// <summary>
        /// <see cref="BackgroundWork"/> 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="supportsCancellation">비동기 작업 취소를 지원합니다.</param>
        /// <param name="reportsProgress">비동기 작업의 진행률 업데이트를 보고할 수 있습니다.</param>
        public BackgroundWork(bool supportsCancellation = false, bool reportsProgress = false)
        {
            SupportsCancellation = supportsCancellation;
            ReportsProgress = reportsProgress;
            _worker.DoWork += Main;
            _worker.RunWorkerCompleted += OnWorkerCompleted;
        }
        /// <summary>
        /// 백그라운드 작업을 시작합니다.
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        public void RunAsync() { _worker.RunWorkerAsync(); }
        /// <summary>
        /// <see cref="BackgroundWork"/> 가 비동기 작업을 실행 중인지 여부를 반환합니다.
        /// </summary>
        public bool IsBusy { get { return _worker.IsBusy; } }
        protected abstract void Main(object sender, DoWorkEventArgs e);

        /// <summary>
        /// 백그라운드 작업을 이상 없이 완료했을 때 발생합니다. 예외 발생, 또는 작업 취소에 관한 이벤트는 <see cref="Cancelled"/> 를 참고하세요.
        /// </summary>
        public event RunWorkerCompletedEventHandler Completed;
        /// <summary>
        /// 백그라운드 작업이 취소 되었거나 예외가 발생했을 때 발생합니다.
        /// </summary>
        public event RunWorkerCompletedEventHandler Cancelled;
        /// <summary>
        /// 백그라운드 작업이 완료, 또는 취소 되었거나 예외가 발생했을 때 발생합니다.
        /// </summary>
        public event RunWorkerCompletedEventHandler Finished;
        private void OnWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }
        protected virtual void OnCompleted(RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                Cancelled?.Invoke(this, e);
            else
                Completed?.Invoke(this, e);
            Finished?.Invoke(this, e);
        }
        /// <summary>
        /// 관리되지 않는 리소스의 확보, 해제 또는 다시 설정과 관련된 응용 프로그램 정의 작업을 수행합니다.
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)_worker).Dispose();
        }
    }
    public abstract partial class BackgroundWork
    {
        /// <summary>
        /// 비동기 작업의 진행률 업데이트를 보고할 수 있습니다. true로 설정했을 경우 다음의 기능 집합을 제공합니다.
        /// <para><see cref="CancelAsync"/> : 백그라운드 작업의 취소를 요청합니다.</para>
        /// <para><see cref="CancellationPending"/> : 백그라운드 작업의 취소를 요청했는지의 여부를 반환합니다.</para>
        /// </summary>
        public bool ReportsProgress
        {
            get { return _worker.WorkerReportsProgress; }
            set
            {
                _worker.WorkerReportsProgress = value;
                if (_worker.WorkerReportsProgress)
                    _worker.ProgressChanged += OnWorkerProgressChanged;
                else
                    _worker.ProgressChanged -= OnWorkerProgressChanged;
            }
        }
        /// <summary>
        /// <see cref=""/> 이벤트를 발생시킵니다.
        /// </summary>
        /// <param name="percentProgress">0에서 100 사이의 백그라운드 작업 진행도입니다.</param>
        /// <exception cref="InvalidOperationException"/>
        protected void ReportProgress(int percentProgress) { _worker.ReportProgress(percentProgress); }
        /// <summary>
        /// 내부 기능 <see cref="ReportProgress"/> 메소드 사용으로 인해 격발됩니다.
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged;
        private void OnWorkerProgressChanged(object sender, ProgressChangedEventArgs e) {  }
        protected virtual void OnProgressChanged(ProgressChangedEventArgs e) { ProgressChanged?.Invoke(this, e); }
    }
    public abstract partial class BackgroundWork
    {
        /// <summary>
        /// 비동기 작업 취소를 지원합니다. true로 설정했을 경우 다음의 기능 집합을 제공합니다.
        /// <para><see cref="CancelAsync"/> : 백그라운드 작업의 취소를 요청합니다.</para>
        /// <para><see cref="CancellationPending"/> : 백그라운드 작업의 취소를 요청했는지의 여부를 반환합니다.</para>
        /// </summary>
        public bool SupportsCancellation { get { return _worker.WorkerSupportsCancellation; } set { _worker.WorkerSupportsCancellation = value; } }
        /// <summary>
        /// 백그라운드 작업의 취소를 요청했는지의 여부를 반환합니다.
        /// </summary>
        public bool CancellationPending { get { return _worker.CancellationPending; } }
        /// <summary>
        /// 백그라운드 작업의 취소를 요청합니다.
        /// <para>클래스 내의 <see cref="Main"/> 메소드에서 <see cref="CancellationPending"/> 값을 지속적으로 확인하여 중간에 작업을 중단할 수 있도록 코드를 작성해야 합니다.</para>
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        public void CancelAsync() { _worker.CancelAsync(); }
    }
}
