using System;
using System.IO;

namespace Charcoal.Extensions
{
    public static class FileStreamExtensions
    {
        /// <summary>
        /// <see cref="Int32"/>를 파일 스트림에 씁니다
        /// </summary>
        /// <param name="value">스트림에 쓸 <see cref="Int32"/> 값입니다</param>
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
                (byte)(value >> 24),
                (byte)(value >> 16),
                (byte)(value >> 8),
                (byte)value
            };
            fs.Write(bytes, 0, 4);
        }
    }
}
