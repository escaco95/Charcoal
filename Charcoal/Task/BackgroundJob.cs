using System;
using System.ComponentModel;

namespace Charcoal.Task
{
    /// <summary>
    /// <see cref="BackgroundWorker"/>기반의 백그라운드 작업을 관리하는 제네릭 개체입니다.
    /// </summary>
    public partial class BackgroundJob <JobInfoClass> : BackgroundWorker
    {
        /// <summary>
        /// <see cref="BackgroundJob{JobInfoClass}"/> 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="value">이 인스턴스가 수행할 작업의 정보를 가진 클래스입니다.</param>
        /// <param name="supportsCancellation">비동기 작업 취소를 지원합니다.</param>
        /// <param name="reportsProgress">비동기 작업의 진행률 업데이트를 보고할 수 있습니다.</param>
        public BackgroundJob(JobInfoClass value, bool supportsCancellation = false, bool reportsProgress = false)
        {
            _info = value;
            WorkerSupportsCancellation = supportsCancellation;
            WorkerReportsProgress = reportsProgress;
        }
        private JobInfoClass _info;
        /// <summary>
        /// 이 인스턴스가 수행할 작업의 정보를 가진 클래스입니다.
        /// </summary>
        public JobInfoClass JobInfo { get { return _info; } set { _info = value; } }
        /// <summary>
        /// System.ComponentModel.BackgroundWorker.ProgressChanged 이벤트를 발생시킵니다.
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        public void ReportProgress() { base.ReportProgress(0); }
    }
}
