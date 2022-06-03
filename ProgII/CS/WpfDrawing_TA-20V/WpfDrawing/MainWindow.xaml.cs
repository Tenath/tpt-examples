using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDrawing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PixelArray pixels = new PixelArray(800, 600);
            /*pixels.Fill(Colors.Black);
            pixels.SetPixel(pixels.Width/2,pixels.Height/2, Colors.White);
            Drawing.DrawVerticalLine(pixels, pixels.Width / 2, Colors.White);
            Drawing.DrawHorizontalLine(pixels, pixels.Height / 2, Colors.White);
            Drawing.DrawLinearFunction(pixels, 0.25, 10, Colors.Red);
            Drawing.DrawLinearFunction(pixels, 0.5, 10, Colors.Red);
            Drawing.DrawLinearFunction(pixels, 1, 10, Colors.Red);
            Drawing.DrawLinearFunction(pixels, 2, 10, Colors.Red);
            Drawing.DrawLinearFunction(pixels, 4, 10, Colors.Red);
            Drawing.DrawLinearFunction(pixels, -0.25, 100, Colors.Green);
            Drawing.DrawLine(pixels, Colors.Red, 0, 0,
                pixels.Width / 2, pixels.Height / 2);

            Drawing.DrawLine(pixels, Colors.Red, 
                pixels.Width / 2, pixels.Height / 2,
                pixels.Width - 1, 0);

            Drawing.DrawLine(pixels, Colors.Magenta,
                pixels.Width / 4, 0,
                pixels.Width / 4, pixels.Height/2);

            Drawing.DrawLine(pixels, Colors.Cyan,
                0, pixels.Height / 4,
                pixels.Width / 2, pixels.Height / 4);

            // Задание 3: Нарисовать прямоугольник 
            //            (в середине картинки, 
            //             размер произвольный, но меньше размера картинки и больше 0)
            Drawing.DrawRectangle(pixels, Colors.Yellow,
                pixels.Width / 2 - 160 / 2, // min_x
                pixels.Height / 2 - 120 / 2, // min_y
                pixels.Width / 2 + 160 / 2, // max_x
                pixels.Height / 2 + 120 / 2 // max_y
                );

            Drawing.DrawLine(pixels, Colors.Green, 400, 450, 350, 500);

            Drawing.DrawTriangle(pixels, Colors.Azure, 
                pixels.Width / 2, pixels.Height / 2 - 100,
                pixels.Width / 2 - 50, pixels.Height / 2 + 25, 
                pixels.Width / 2 + 50, pixels.Height / 2 + 25);

            Drawing.DrawTriangle(pixels, Colors.Azure,
                pixels.Width / 2 - 50, pixels.Height / 2 - 75,
                pixels.Width / 2 + 50, pixels.Height / 2 - 75,
                pixels.Width/2, pixels.Height / 2 + 50
                );

            // Задание 4: Нарисовать ромб (условия те же)

            // Задание 5: Нарисовать шестиугольник (условия те же)

            Drawing.DrawFilledRectangle(pixels, Colors.DarkOrange, 
                600, 350, 700, 450);

            Drawing.DrawCircle(pixels, Colors.Olive,
                new Point2i() { x = pixels.Width / 2, y = pixels.Height / 2 }, 100); */

            // Задание 8: В точке, отличной от центра картинки с помощью треугольников
            //            нарисовать правильный пяти- или шестиугольник, 
            //            и окружность, описывающую его

            Point2i center = new Point2i() { x = pixels.Width / 2, y = pixels.Height / 2 };
            //Polygon pentagon = Polygon.GenerateRegularPolygon(100, 100, center);
            //pentagon.Draw(pixels);

            // Задание 9: Нарисовать окружность в центре изображения или рядом с ним
            //            в окружность вписать многоугольники со сторонами от 3-х до 20

            Rectangle r = new Rectangle(
                pixels.Width / 2 - 200,
                pixels.Width / 2 + 200,
                pixels.Height / 2 - 150,
                pixels.Height / 2 + 150);

            Color c1 = new Color();
            c1.R = 0;
            c1.G = 128;
            c1.B = 0;

            Circle circ = new Circle(center, 300);
            
            /*

            //Drawing.DrawCircularGradient(pixels, circ, Colors.Red, Colors.Blue);

            //Drawing.DrawHorizontalGradient(pixels, r, Colors.Pink, Colors.Blue);

            Point2i a_1 = new Point2i() { x = pixels.Width / 2 - 100, y = pixels.Height / 2 };
            Point2i b_1 = new Point2i() { x = pixels.Width / 2 - 200, y = pixels.Height / 2 + 250 };
            Point2i c_1 = new Point2i() { x = pixels.Width / 2, y = pixels.Height / 2 + 150 };

            Triangle t_1 = new Triangle(a_1, b_1, c_1);

            Point2i a_2 = new Point2i() { x = pixels.Width / 2 + 100, y = pixels.Height / 2 };
            Point2i b_2 = new Point2i() { x = pixels.Width / 2, y = pixels.Height / 2 + 150 };
            Point2i c_2 = new Point2i() { x = pixels.Width / 2 + 200, y = pixels.Height / 2 + 250 };

            Triangle t_2 = new Triangle(a_2, b_2, c_2);

            Point2i a_3 = new Point2i() { x = pixels.Width / 2 - 100, y = pixels.Height / 2 };
            Point2i b_3 = new Point2i() { x = pixels.Width / 2 - 200, y = pixels.Height / 2 - 250 };
            Point2i c_3 = new Point2i() { x = pixels.Width / 2, y = pixels.Height / 2 - 150 };

            Triangle t_3 = new Triangle(a_3, b_3, c_3);

            Point2i a_4 = new Point2i() { x = pixels.Width / 2 + 100, y = pixels.Height / 2 };
            Point2i b_4 = new Point2i() { x = pixels.Width / 2, y = pixels.Height / 2 - 150 };
            Point2i c_4 = new Point2i() { x = pixels.Width / 2 + 200, y = pixels.Height / 2 - 250 };

            Triangle t_4 = new Triangle(a_4, b_4, c_4);

            Drawing.DrawFilledTriangle(pixels, Colors.Green, t_1);
            Drawing.DrawFilledTriangle(pixels, Colors.Green, t_2);
            Drawing.DrawFilledTriangle(pixels, Colors.Green, t_3);
            Drawing.DrawFilledTriangle(pixels, Colors.Green, t_4);

            Point2i a_5 = new Point2i() { x = 100, y = 100 };
            Point2i b_5 = new Point2i() { x = 100, y = 200 };
            Point2i c_5 = new Point2i() { x = 200, y = 100 };

            Triangle t_5 = new Triangle(a_5, b_5, c_5);*/

            Point2i a_trb = new Point2i() { x = 110, y = 200 };
            Point2i b_trb = new Point2i() { x = 160, y = 200 };
            Point2i c_trb = new Point2i() { x = 135, y = 100 };

            Triangle t_trb = new Triangle(a_trb, b_trb, c_trb);

            //Drawing.DrawFilledTriangle(pixels, Colors.Red, t_trb);

            Point2i a_brb = new Point2i() { x = 110, y = 270 };
            Point2i b_brb = new Point2i() { x = 160, y = 270 };
            Point2i c_brb = new Point2i() { x = 135, y = 370 };

            Triangle t_brb = new Triangle(a_brb, b_brb, c_brb);

            //Drawing.DrawFilledTriangle(pixels, Colors.Red, t_brb);

            Point2i a_lrb = new Point2i() { x = 100, y = 210 };
            Point2i b_lrb = new Point2i() { x = 100, y = 260 };
            Point2i c_lrb = new Point2i() { x = 0, y = 235 };

            Triangle t_lrb = new Triangle(a_lrb, b_lrb, c_lrb);

            //Drawing.DrawFilledTriangle(pixels, Colors.Red, t_lrb);

            Point2i a_rrb = new Point2i() { x = 170, y = 210 };
            Point2i b_rrb = new Point2i() { x = 170, y = 260 };
            Point2i c_rrb = new Point2i() { x = 270, y = 235 };

            Triangle t_rrb = new Triangle(a_rrb, b_rrb, c_rrb);

            //Drawing.DrawFilledTriangle(pixels, Colors.Red, t_rrb);

            ColoredTriangle tctr = new ColoredTriangle(
                new Vertex(a_trb, Colors.Red),
                new Vertex(b_trb, Colors.Green),
                new Vertex(c_trb, Colors.Blue)
                );

            ColoredTriangle bctr = new ColoredTriangle(
                new Vertex(a_brb, Colors.Red),
                new Vertex(b_brb, Colors.Green),
                new Vertex(c_brb, Colors.Blue)
                );

            ColoredTriangle lctr = new ColoredTriangle(
                new Vertex(a_lrb, Colors.Red),
                new Vertex(b_lrb, Colors.Green),
                new Vertex(c_lrb, Colors.Blue)
                );

            ColoredTriangle rctr = new ColoredTriangle(
                new Vertex(a_rrb, Colors.Red),
                new Vertex(b_rrb, Colors.Green),
                new Vertex(c_rrb, Colors.Blue)
                );

            tctr.Draw(pixels);
            bctr.Draw(pixels);
            lctr.Draw(pixels);
            rctr.Draw(pixels);

            Point2i ca = new Point2i() { x = pixels.Width / 2, y = pixels.Height / 2 - 100 };
            Point2i cb = new Point2i() { x = pixels.Width / 2 - 150, y = pixels.Height / 2 + 100 };
            Point2i cc = new Point2i() { x = pixels.Width / 2 + 150, y = pixels.Height / 2 + 100 };

            ColoredTriangle center_tri = new ColoredTriangle(
                new Vertex(ca,Colors.Blue),
                new Vertex(cb, Colors.Blue),
                new Vertex(cc, Colors.Blue)
                );

            Func<int, int, Color> GetColor = (x, y) =>
            {
                Point2i pt = new Point2i();
                pt.x = x;
                pt.y = y;
                var coord = BarycentricCoordinate.Compute(center_tri.A, center_tri.B, center_tri.C, pt);

                //Color c = center_tri.InterpolateColor(coord);
                Color c = Color.FromRgb(0, 0, 0);

                double threshold = 0.15;

                double mindist = (coord.L1 < coord.L2) ? (coord.L3 < coord.L1 ? coord.L3 : coord.L1) : 
                                                     (coord.L3 < coord.L2 ? coord.L3 : coord.L2);
                if (mindist > threshold) c.B = 0;
                else
                {
                    byte cvalue = (byte)(255.0 * (threshold - mindist) /threshold);
                    c.B = cvalue;
                    c.G = cvalue;
                }

                return c;
            };

            center_tri.DrawColor(pixels, GetColor);

            //Drawing.DrawFilledTriangle(pixels, Colors.Red, t_5);

            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(System.Drawing.Image.FromFile("kitten.jpg"));

            center_tri.B.texpos.x = 80;
            center_tri.B.texpos.y = 220;

            center_tri.C.texpos.x = 400;
            center_tri.C.texpos.y = 60;

            center_tri.A.texpos.x = 377;
            center_tri.A.texpos.y = 486;

            Func<int, int, Color> FromTexture = (x, y) =>
            {
                Point2i pt = new Point2i();
                pt.x = x;
                pt.y = y;
                var coord = BarycentricCoordinate.Compute(center_tri.A, center_tri.B, center_tri.C, pt);

                Point2i tex_coord = center_tri.InterpolateTexel(coord);

                var c = bitmap.GetPixel(tex_coord.x, tex_coord.y);

                Color c2 = Color.FromRgb(c.R, c.G, c.B);

                return c2;
            };

            center_tri.DrawColor(pixels, FromTexture);

            BitmapSource src = BitmapSource.Create(
                800, 600, // Размеры: ширина, высота
                96, 96, // Плотность точек (DPI)
                PixelFormats.Rgb24,
                null, pixels.Pixels, pixels.Width * 3);

            img.Source = src;
        }
    }
}
