using System;
using System.Drawing;

namespace Charcoal.Extensions
{
    // RectangleF 확장 메소드
    public static partial class RectangleFExtensions
    {
        /// <summary>
        /// 이하 직사각형 개체를 모두 내포하도록 설정합니다.
        /// </summary>
        /// <param name="rects">내포될 직사각형 인스턴스들입니다.</param>
        /// <exception cref="ArgumentException"/>
        public static RectangleF Nest(this ref RectangleF rect, params RectangleF[] rects)
        {
            float minX = 0.0F, minY = 0F, maxX = 0.0F, maxY = 0.0F;
            bool first = true;
            foreach (var rct in rects)
            {
                if (first)
                {
                    first = false;
                    minX = rct.X; minY = rct.Y; maxX = rct.X + rct.Width; maxY = rct.Y + rct.Height;
                }
                else
                {
                    if (rct.X < minX) minX = rct.X;
                    if (rct.Y < minY) minY = rct.Y;
                    if (rct.X + rct.Width > maxX) maxX = rct.X + rct.Width;
                    if (rct.Y + rct.Height > maxY) maxY = rct.Y + rct.Height;
                }
            }
            rect.X = minX;
            rect.Y = minY;
            rect.Width = maxX - minX;
            rect.Height = maxY - minY;
            return rect;
        }
    }
}
