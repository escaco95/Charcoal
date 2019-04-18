using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//TODO 현재 ProcessExitedHandler가 작동하지 않고 있습니다.
namespace Charcoal.Python
{
    /// <summary>
    /// python.exe 실행 파일과, 파이썬 스크립트 파일(*.py) 문자열 경로를 받아 로컬 시스템 프로세스를 시작하고 중지할 수 있습니다.
    /// </summary>
    public partial class PythonExecutor
    {
        private Process _process = null;
        private string _exepath = String.Empty;
        /// <summary>
        /// 시작할 응용 프로그램 또는 문서를 가져오거나 설정합니다. <see cref="PythonExecutor"/>의 경우, 실행할 python.exe 경로에 해당합니다. 
        /// </summary>
        public string FileName { get { return _exepath; } set { _exepath = value; } }
        private string _pypath = String.Empty;
        /// <summary>
        /// 응용 프로그램을 시작할 때 사용할 명령줄 인수 집합을 가져오거나 설정합니다. <see cref="PythonExecutor"/>의 경우, 실행할 py 파일 경로에 해당합니다. 
        /// </summary>
        public string Arguments { get { return _pypath; } set { _pypath = value; } }
        public PythonExecutor()
        {
            _process = new Process();
            _process.OutputDataReceived += ProcessOutputDataHandler;
            _process.ErrorDataReceived += ProcessErrorDataHandler;
            _process.Exited += ProcessExitedHandler;
        }
        /// <summary>
        /// 인스턴스 작업을 시작합니다.
        /// </summary>
        public void Start()
        {
            _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.WorkingDirectory = Path.GetDirectoryName(_pypath);
            _process.StartInfo.FileName = _exepath;
            _process.StartInfo.Arguments = " -u " + _pypath;

            _process.StartInfo.RedirectStandardInput = true;

            _process.Start();
            _process.BeginErrorReadLine();
            _process.BeginOutputReadLine();
        }
        /// <summary>
        /// 인스턴스 작업을 즉시 중단합니다.
        /// </summary>
        public void Kill()
        {
            _process.Kill();
            _process.CancelOutputRead();
            _process.CancelErrorRead();
            OnExitedAsync(new EventArgs());
        }
        public void ProcessExitedHandler(object sendingProcess, EventArgs e)
        {
            _process.CancelOutputRead();
            _process.CancelErrorRead();
            OnExitedAsync(e);
        }
        public void ProcessOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            OnOutputDataReceivedAsync(outLine);
        }
        public void ProcessErrorDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            OnErrorDataReceivedAsync(outLine);
        }
    }
    /// <summary>
    /// 이벤트 정의 영역입니다
    /// </summary>
    public partial class PythonExecutor
    {
        protected virtual void OnExitedAsync(EventArgs e) { ExitedAsync?.Invoke(this, e); }
        protected virtual void OnOutputDataReceivedAsync(DataReceivedEventArgs e) { OutputDataReceivedAsync?.Invoke(this,e); }
        protected virtual void OnErrorDataReceivedAsync(DataReceivedEventArgs e) { ErrorDataReceivedAsync?.Invoke(this,e); }
        /// <summary>
        /// 비동기 이벤트 : 인스턴스의 비동기 작업 도중 <see cref="Process.Exited"/> 이벤트와 함께 격발됩니다.
        /// </summary>
        public event EventHandler ExitedAsync;
        /// <summary>
        /// 비동기 이벤트 : 인스턴스의 비동기 작업 도중 <see cref="Process.OutputDataReceived"/> 이벤트와 함께 격발됩니다.
        /// </summary>
        public event ExecutorDataReceivedAsyncEventHandler OutputDataReceivedAsync;
        /// <summary>
        /// 비동기 이벤트 : 인스턴스의 비동기 작업 도중 <see cref="Process.ErrorDataReceived"/> 이벤트와 함께 격발됩니다.
        /// </summary>
        public event ExecutorDataReceivedAsyncEventHandler ErrorDataReceivedAsync;
    }
    /// <summary>
    /// 처리 하는 메서드를 나타내는 <see cref="PythonExecutor.OutputDataReceivedAsync"/> 이벤트 또는 <see cref="PythonExecutor.ErrorDataReceived"/>의 이벤트를 처리합니다.
    /// </summary>
    public delegate void ExecutorDataReceivedAsyncEventHandler(object sender, DataReceivedEventArgs e);
}