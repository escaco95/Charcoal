using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charcoal.Extensions
{
    public static class Int32Extensions
    {
        /// <summary>
        /// 이 인스턴스의 숫자 값을 해당하는 바이트 배열 표현으로 변환합니다.
        /// </summary>
        public static byte[] ToByteArray(this Int32 value)
        {
            byte[] bytes = new byte[4]
            {
                (byte)(value >> 24),
                (byte)(value >> 16),
                (byte)(value >> 8),
                (byte)value
            };
            return bytes;
        }
    }
}