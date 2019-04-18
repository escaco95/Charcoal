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
        /// 스트림에서 <see cref="Int32"/>를 읽어서 해당 데이터를 반환합니다
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="NotSupportedException"/>
        public static Int32 ReadInt32(this FileStream fs)
        {
            byte[] bytes = new byte[4];
            if (fs.Read(bytes, 0, 4) < 4)
                throw new IOException("파일의 끝을 초과했습니다");
            return (int)bytes[0] + ((int)bytes[1] << 8) + ((int)bytes[2] << 16) + ((int)bytes[3] << 24);
        }
        /// <summary>
        /// <see cref="String"/>을 파일 스트림에 씁니다
        /// </summary>
        /// <param name="value">스트림에 쓸 <see cref="String"/> 값입니다.</param>
        /// <param name="encoding">스트림에 쓸 <see cref="String"/> 의 인코딩 방식을 결정합니다.</param>
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
        /// <summary>
        /// 파일 포인터를 <see cref="FileStream"/>의 시작 지점으로 되감습니다.
        /// </summary>
        /// <exception cref="IOException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ObjectDisposedException"/>
        public static void Rewind(this FileStream fs)
        {
            fs.Seek(0, SeekOrigin.Begin);
        }
        /// <summary>
        /// 파일 내용을 완전히 비웁니다. (0KB)
        /// </summary>
        /// <exception cref="IOException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ObjectDisposedException"/>
        public static void Clear(this FileStream fs)
        {
            fs.Seek(0, SeekOrigin.Begin);
            fs.SetLength(0);
        }
    }
}
