// (c) Andrei Veeremaa @ TPT, 2019
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

namespace WpfDrawingManual
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PixelFormat pf = PixelFormats.Rgb24;
        PixelArrayRGB bitmap = new PixelArrayRGB(800, 600);

        List<Shape> shapes = new List<Shape>();
        PointI center;

        public MainWindow()
        {
            InitializeComponent();

            Init();
            Draw();
            BitmapSource src = BitmapSource.Create(800, 600, 96, 96, pf, null, 
                bitmap.Pixels, bitmap.Width * 3);
            img.Source = src;
        }

        void Init()
        {
            center = new PointI(bitmap.Width / 2, bitmap.Height / 2);

            /*Polygon p = Polygon.GenerateRegularPolygon(3, 100, new PointI(0, 0));
            p.Color = Colors.Black;
            p.SetPosition(200, 250);
            shapes.Add(p);

            p = Polygon.GenerateRegularPolygon(6, 200, new PointI(0, 0));
            p.Color = Colors.Red;
            p.SetPosition(400, 300);
            shapes.Add(p);

            Triangle tri = new Triangle(new PointI(30, 30), new PointI(60, 30), new PointI(60, 60));
            tri.Color = Colors.Green;
            tri.Position = new PointI(center.x, center.y);

            shapes.Add(tri);*/
            shapes = Drawing.ReadFromFile("shapes.txt");
        }

        void Draw()
        {
            bitmap.Fill(Colors.White);


            Color first_color = Color.FromArgb(255, 128, 196, 64);
            Color second_color = Color.FromArgb(255, 32, 255, 64);

            /*Drawing.DrawFilledRectangleGradient(bitmap, new PointI(center.x - 300, center.y - 100), 100, 200,
                first_color, second_color);

            foreach(Shape s in shapes)
            {
                s.Draw(bitmap);
            }*/

            ColoredTriangle tri = new ColoredTriangle(
                new Vertex() { pos = new PointI(center.x, center.y - 200), color = Colors.Red },
                new Vertex() { pos = new PointI(center.x - 300, center.y + 200), color = Colors.Green },
                new Vertex() { pos = new PointI(center.x + 300, center.y + 200), color = Colors.Blue });

            Drawing.DrawGradientTriangle(bitmap, tri);

            //Drawing.DrawHorizontalLine(bitmap);
            //Drawing.DrawVerticalLine(bitmap);
            /*Drawing.DrawLinearFunction(bitmap);
            Drawing.DrawFunction(bitmap, Colors.Aqua, (x) => x);
            Drawing.DrawFunction(bitmap, Colors.Magenta, (x) => 2*x);
            Drawing.DrawFunction(bitmap, Colors.Orange, (x) => x/2);
            Drawing.DrawFunction(bitmap, Colors.Tan, (x) => x / 2 + 80);
            Drawing.DrawFunction(bitmap, Colors.Black, (x) => Drawing.LinearFunction(x, 0.5, -80));

            Drawing.DrawFunction(bitmap, Colors.Orange, (x) => 10*x);*/

            /*Drawing.DrawLine(bitmap, 100, 217, 120, 25, Colors.Black);
            Drawing.DrawLine(bitmap, 100, 217, 220, 25, Colors.Black);
            Drawing.DrawLine(bitmap, 100, 217, 220, 217, Colors.Black);

            Drawing.DrawTriangle(bitmap, Colors.Green,
                new PointI(500, 500), new PointI(350, 550), new PointI(650, 400));*/

            /*Polygon p = new Polygon();
            p.points.Add(new PointI(300, 200));
            p.points.Add(new PointI(500, 200));
            p.points.Add(new PointI(400, 200-(int)(200*Math.Sin(Math.PI*60.0/180.0))));
            p.Draw(bitmap, Colors.Gold);

            Polygon p_tri = Polygon.GenerateRegularPolygon(3, 100, new PointI(400, 300));
            p_tri.Draw(bitmap, Colors.Indigo);

            Polygon p2 = Polygon.GenerateRegularPolygon(4, 100, new PointI(400, 300));*/

            /*for(uint i = 3; i<=30; i++)
            {
                Polygon p = Polygon.GenerateRegularPolygon(i, 100, new PointI(400, 300));
                p.Draw(bitmap, Colors.Indigo);
            }*/

            //Drawing.DrawFilledRectangle(bitmap, Colors.LightGreen, new PointI(200, 400), 200, 100);

            /*Drawing.DrawFilledCircle(bitmap, Colors.Orange, new PointI(400, 300), 100);*/
            //Drawing.DrawCircle(bitmap, Colors.Aqua, new PointI(400, 300), 100);
            //Drawing.DrawEllipse(bitmap, Colors.Aqua, new PointI(400, 300), 200, 100);
            // Задание 1: Нарисовать прямоугольный, равнобедренный и равносторонний треугольники

            // Задание 2: Сделать функцию, которая будет отрисовывать прямоугольник
            //            (или по двум точкам, или по точке и ширине/высоте)

            // * Задание 2.5: Сделать функцию, которая рисует произвольный многоугольник
            //                (по массиву точек, или по структуре с таковым)
        }
    }
}
