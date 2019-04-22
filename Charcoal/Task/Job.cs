using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Charcoal.Task
{
    /// <summary>
    /// 프로그램 작업 단위의 정보를 보관하는 추상 개체입니다.
    /// </summary>
    public abstract class Job
    {
        protected volatile bool _isDone = false;
        /// <summary>
        /// 해당 작업이 종료되었는지의 여부를 반환합니다.
        /// </summary>
        public bool IsDone { get { return _isDone; } }
        /// <summary>
        /// 해당 작업을 일정 시간(밀리초) 동안 감시하고, 더 감시해야 할 필요가 있는지를 반환합니다.
        /// <para>true : 해당 작업이 아직 종료되지 않았으며, 더 감시해야 합니다.</para>
        /// <para>false : 해당 작업이 종료되었으며, 더 감시할 필요가 없습니다.</para>
        /// </summary>
        /// <param name="delay">종료 여부를 확인할 간격입니다.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public bool Watch(int delay)
        {
            Thread.Sleep(delay);
            return !_isDone;
        }
        /// <summary>
        /// 해당 작업을 종료됨 상태로 만듭니다.
        /// </summary>
        public void Finish() { _isDone = true; }
    }
}
