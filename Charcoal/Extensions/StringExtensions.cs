using System;
using System.IO;
using System.Text;

namespace Charcoal.Extensions
{
    // 경로 문자열과 관련된 확장 메소드
    #region[    StringExtensions.Path    ]
    public static partial class StringExtensions
    {
        /// <summary>
        /// 경로 문자열에 대한 디렉터리 정보를 반환합니다.
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="PathTooLongException"/>
        public static String GetDirectoryName(this String str) { return Path.GetDirectoryName(str); }
        /// <summary>
        /// 경로 문자열에서 파일 이름과 확장명을 반환합니다.
        /// </summary>
        /// <exception cref="ArgumentException"/>
        public static String GetFileName(this String str) { return Path.GetFileName(str); }
        /// <summary>
        /// 확장명 없이 경로 문자열의 파일 이름을 반환합니다.
        /// </summary>
        /// <exception cref="ArgumentException"/>
        public static String GetFileNameWithoutExtension(this String str) { return Path.GetFileNameWithoutExtension(str); }
        /// <summary>
        /// 경로 문자열의 확장명을 변경합니다.
        /// </summary>
        /// <param name="extension">앞에 마침표가 있거나 없는 새 확장명입니다. 문자열에서 기존 확장명을 제거하려면 null을(를) 지정하세요.</param>
        /// <exception cref="ArgumentException"/>
        public static String ChangeExtension(this String str, String extension) { return Path.ChangeExtension(str, extension); }
        /// <summary>
        /// 문자열 경로에 지정된 파일이 있는지를 확인합니다.
        /// </summary>
        public static Boolean FileExists(this String str) { return File.Exists(str); }
        /// <summary>
        /// 문자열 경로가 디스크에 있는 기존 디렉터리를 참조하는지를 확인합니다.
        /// </summary>
        public static Boolean DirectoryExists(this String str) { return Directory.Exists(str); }
    }
    #endregion

    // 바이트 배열과 관련된 확장 메소드
    #region[    StringExtensions.Bytes    ]
    public static partial class StringExtensions
    {
        /// <summary>
        /// 이 인스턴스의 문자를 <see cref="Encoding"/>된 바이트 배열에 복사합니다.
        /// </summary>
        /// <param name="encoding">바이트 인코딩 규칙입니다.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="EncoderFallbackException"/>
        public static byte[] ToByteArray(this String str, Encoding encoding){return encoding.GetBytes(str);}
    }
    #endregion
}