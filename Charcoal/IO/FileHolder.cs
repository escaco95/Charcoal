using System;
using System.IO;

namespace Charcoal.IO
{
    /// <summary>
    /// 지정된 경로 문자열에 위치한 파일을 점유(<see cref="FileStream"/>)하거나 해제(<see cref="FileStream.Dispose"/>)하는 객체입니다.
    /// <para><seealso cref="Lock"/>으로 점유, <seealso cref="Unlock"/>으로 스트림을 해제합니다.</para>
    /// </summary>
    public class FileHolder : IDisposable
    {
        private volatile FileHolderState _state = FileHolderState.Idle;
        /// <summary>
        /// 이 인스턴스의 상태를 반환합니다.
        /// </summary>
        public FileHolderState State { get { return _state; } }
        private volatile FileStream _filestream = null;
        private volatile string _filename = String.Empty;
        /// <summary>
        /// 해당 인스턴스가 관리할 파일의 경로 문자열입니다. 이 인스턴스의 <see cref="State"/>가 <see cref="FileHolderState.Idle"/>이 아닐 경우, <see cref="IOException"/>이 발생합니다.
        /// </summary>
        public string FileName
        {
            get { return _filename; }
            set
            {
                if (_state != FileHolderState.Idle)
                    throw new IOException("Idle 상태에서만 FileName 값을 변경할 수 있습니다");
                _filename = value;
            }
        }
        /// <summary>
        /// 새 <see cref="FileHolder"/> 인스턴스를 생성하며, 동시에 <see cref="FileName"/>을 설정합니다.
        /// </summary>
        /// <param name="path"><see cref="FileHolder"/>가 접근 불허 상태로 만들 파일입니다.</param>
        public FileHolder(string path){_filename = path;}
        /// <summary>
        /// 새 <see cref="FileHolder"/> 인스턴스를 생성합니다.
        /// </summary>
        public FileHolder() { }
        /// <summary>
        /// <see cref="FileName"/>으로 지정된 경로 문자열에 해당하는 파일을 잠금 처리합니다. 중복된 Lock 처리를 지시하면 예외가 발생합니다.
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="PathTooLongException"/>
        /// <exception cref="DirectoryNotFoundException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="FileNotFoundException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="IOException"/>
        public void Lock()
        {
            FileStream fs = _filestream;
            string fn = _filename;
            if (fs != null || _state != FileHolderState.Idle)
                throw new IOException("파일이 이미 열려 있습니다.");
            _state = FileHolderState.Locking;
            fs = File.OpenRead(fn);
            _filestream = fs;
            _state = FileHolderState.Locked;
        }
        /// <summary>
        /// 파일에 대한 <see cref="Lock"/>을 해제합니다. 중복된 Unlock 처리를 지시하면 예외가 발생합니다.
        /// </summary>
        /// <exception cref="IOException"/>
        public void Unlock()
        {
            FileStream fs = _filestream;
            if (fs == null || _state != FileHolderState.Locked)
                throw new IOException("파일이 이미 닫혀 있습니다.");
            _state = FileHolderState.Unlocking;
            fs.Dispose();
            _filestream = null;
            _state = FileHolderState.Unlocked;
        }
        /// <summary>
        /// 관리되지 않는 리소스의 확보, 해제 또는 다시 설정과 관련된 응용 프로그램 정의 작업을 수행합니다.
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)_filestream).Dispose();
        }
    }
    /// <summary>
    /// <see cref="FileHolder"/>가 도달할 수 있는 다양한 상태입니다.
    /// </summary>
    public enum FileHolderState
    {
        /// <summary>
        /// <see cref="FileHolder.Lock"/>가능한 유휴 상태입니다.
        /// </summary>
        Idle,
        Locking,
        /// <summary>
        /// <see cref="FileHolder.Unlock"/>가능한 잠금 상태입니다.
        /// </summary>
        Locked,
        Unlocking,
        /// <summary>
        /// <see cref="FileHolder.Lock"/>가능한 유휴 상태입니다.
        /// </summary>
        Unlocked = Idle,
    }
}