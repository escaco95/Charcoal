using System;
using System.IO;

namespace Charcoal.Extensions
{
    public static class FileStreamExtensions
    {
        /// <summary>
        /// <see cref="Int32"/>를 파일 스트림에 씁니다
        /// </summary>
        /// <param name="value">스트림에 쓸 <see cref="Int32"/> 값입니다.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="NotSupportedException"/>
        public static void WriteInt32(this FileStream fs,Int32 value )
        {
            byte[] bytes = new byte[4]
            {
                (byte)value,
                (byte)(value >> 8),
                (byte)(value >> 16),
                (byte)(value >> 24)
            };
            fs.Write(bytes, 0, 4);
        }
        /// <summary>
        /// <see cref="String"/>을 파일 스트림에 씁니다
        /// </summary>
        /// <param name="value">스트림에 쓸 <see cref="String"/> 값입니다.</param>
        /// <param name="encoding">스트림의 쓸 <see cref="String"/> 의 인코딩 방식을 결정합니다.</param>
        /// <exception cref="System.Text.EncoderFallbackException"/>
        /// <exception cref="OverflowException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="NotSupportedException"/>
        public static void WriteString(this FileStream fs, String value, System.Text.Encoding encoding)
        {
            byte[] bytes = value.ToByteArray(encoding);
            fs.Write(bytes, 0, bytes.Length);
        }
    }
}
