﻿using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace Charcoal.Extensions
{
    // 단순 문자열 처리와 관련된 확장 메소드
    public static partial class StringExtensions
    {
        /// <summary>
        /// 문자열의 가장 마지막 문자를 반환합니다. 빈 문자열의 경우 <see cref="IndexOutOfRangeException"/>가 발생합니다.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException"/>
        public static char GetLastChar(this String str) { return str[str.Length - 1]; }
        /// <summary>
        /// 문자열의 가장 마지막 문자를 비교합니다. 빈 문자열의 경우 <see cref="IndexOutOfRangeException"/>가 발생합니다.
        /// </summary>
        /// <param name="cmp">비교할 단일 문자입니다.</param>
        /// <exception cref="IndexOutOfRangeException"/>
        public static bool IsLastChar(this String str, char cmp) { return str[str.Length - 1] == cmp; }
        /// <summary>
        /// 문자열의 물리적 크기를 계산합니다.
        /// </summary>
        /// <param name="str">크기를 계산할 문자열입니다.</param>
        /// <param name="font">계산의 기준이 될 폰트 정보입니다.</param>
        /// <exception cref="ArgumentException"/>
        public static SizeF MeasureSize(this String str, Font font = null)
        {
            if (font == null)
                font = SystemFonts.DefaultFont;
            SizeF result;
            using (var g = Graphics.FromHwnd(IntPtr.Zero))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                result = g.MeasureString(str, font, int.MaxValue, StringFormat.GenericTypographic);
            }
            return result;
        }
        /// <summary>
        /// 문자열의 물리적 크기를 계산합니다.
        /// </summary>
        /// <param name="str">크기를 계산할 문자열입니다.</param>
        /// <param name="font">계산의 기준이 될 폰트 정보입니다.</param>
        /// <exception cref="ArgumentException"/>
        public static RectangleF MeasureRectangle(this String str, Font font = null)
        {
            if (font == null)
                font = SystemFonts.DefaultFont;
            RectangleF result;
            using (var g = Graphics.FromHwnd(IntPtr.Zero))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                result = new RectangleF(new PointF(0, 0), g.MeasureString(str, font, int.MaxValue, StringFormat.GenericTypographic));
            }
            return result;
        }
    }

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
        /// <summary>
        /// 문자열 경로가 지정하고 있는 디렉터리에 있는 하위 디렉터리의 이름(경로 포함)을 반환합니다.
        /// </summary>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="PathTooLongException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="DirectoryNotFoundException"/>
        public static String[] GetDirectories(this String str) { return Directory.GetDirectories(str); }
        /// <summary>
        /// 문자열 경로가 지정하고 있는 디렉터리에 있는 하위 파일의 이름(경로 포함)을 반환합니다.
        /// </summary>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="PathTooLongException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="DirectoryNotFoundException"/>
        public static String[] GetFiles(this String str) { return Directory.GetFiles(str); }
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