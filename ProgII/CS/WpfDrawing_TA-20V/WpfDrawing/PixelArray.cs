using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfDrawing
{
    // Обёртка вокруг массива пикселей
    public class PixelArray
    {
        public byte[] Pixels { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public PixelArray(int w, int h)
        {
            Width = w;
            Height = h;
            Pixels = new byte[Width*Height*3];
        }

        public void SetPixel(int x, int y, Color c)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return;

            int array_index = (y * Width + x)*3;

            Pixels[array_index] = c.R;
            Pixels[array_index + 1] = c.G;
            Pixels[array_index + 2] = c.B;
        }

        public Color GetPixel(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return new Color();
            int array_index = (y * Width + x) * 3;

            Color c = new Color();
            c.R = Pixels[array_index];
            c.G = Pixels[array_index+1];
            c.B = Pixels[array_index+2];

            return c;
        }

        public void Fill(Color c)
        {
            for(int y = 0; y<Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    SetPixel(x, y, c);
                }
            }
        }
    }
}
