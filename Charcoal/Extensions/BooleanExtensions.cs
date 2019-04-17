using System;

namespace Charcoal.Extensions
{
    public static class BooleanExtensions
    {
        /// <summary>
        /// 이 인스턴스의 값을 해당하는 정수 표현으로 변환합니다.
        /// </summary>
        /// <param name="trueValue">인스턴스의 값이 참일때 반환될 정수입니다.</param>
        /// <param name="falseValue">인스턴스의 값이 거짓일때 반환될 정수입니다.</param>
        public static int ToInt(this Boolean value, int trueValue = 1, int falseValue = 0) { return value ? trueValue : falseValue; }
    }
}
