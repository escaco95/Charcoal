using System;

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
                (byte)value,
                (byte)(value >> 8),
                (byte)(value >> 16),
                (byte)(value >> 24)
            };
            return bytes;
        }
    }
}