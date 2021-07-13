// (c) Andrei Veeremaa @ TPT, 2019
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfDrawingManual
{
    public class PointI
    {
        public int x = 0;
        public int y = 0;

        public PointI() { }

        public PointI(int pX, int pY)
        {
            x = pX;
            y = pY;
        }
    }

    public class PixelArrayRGB
    {
        public byte[] Pixels { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public PixelArrayRGB(int w, int h)
        {
            Width = w;
            Height = h;
            Pixels = new byte[Width * Height * 3];
        }

        // При вызове выставляем цвет пикселя по указанным координатам
        public void SetPixel(int x, int y, Color c)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height) return;

            int array_index = (Width * y + x) * 3;

            // Пиксель = 3 байта, сначала R, потом G, потом B
            Pixels[array_index] = c.R;
            Pixels[array_index + 1] = c.G;
            Pixels[array_index + 2] = c.B;
        }
        
        // Адаптер для PointI
        public void SetPixel(PointI pt, Color c)
        {
            SetPixel(pt.x, pt.y, c);
        }

        public void Fill(Color color)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    SetPixel(x, y, color);
                }
            }
        }

        public PointI Center => new PointI(Width / 2, Height / 2);

        public PointI ShiftToCenter(int x, int y)
        {
            /*PointI pt = new PointI();
            pt.x = Center.x + x;
            pt.y = Center.y + y;
            return pt;*/
            return new PointI(Center.x + x, Center.y - y);
        }
    }
}
