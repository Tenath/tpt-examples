// (c) Andrei Veeremaa @ TPT, 2019
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfDrawingManual
{
    // Пример для #3
    public class Shape
    {
        public virtual void Draw(PixelArrayRGB bitmap) { }
    }

    // За вершиной треугольника закреплены два свойства:
    // Её положение в пространстве, и цвет
    public class Vertex
    {
        public PointI pos;
        public Color color;
    }

    public class ColoredTriangle : Shape
    {
        public Vertex[] points { get; private set; }
        public PointI Position { get; set; }

        public ColoredTriangle(Vertex p1, Vertex p2, Vertex p3)
        {
            points = new Vertex[3];
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;

            Position = new PointI(0, 0);
        }

        public override void Draw(PixelArrayRGB bitmap)
        {
            Drawing.DrawGradientTriangle(bitmap, this);
        }

        public Color InterpolateColor(TriWeight wt)
        {
            return Color.FromArgb(
                (byte)wt.Interpolate(points[0].color.A, points[1].color.A, points[2].color.A),
                (byte)wt.Interpolate(points[0].color.R, points[1].color.R, points[2].color.R),
                (byte)wt.Interpolate(points[0].color.G, points[1].color.G, points[2].color.G),
                (byte)wt.Interpolate(points[0].color.B, points[1].color.B, points[2].color.B)
                );
        }
    }

    public class TriWeight
    {
        // веса (значимости) точек
        private double W1, W2, W3;

        public TriWeight(ColoredTriangle tri, PointI pt)
        {
            // https://codeplea.com/triangular-interpolation
            // Задача: взять из теоретического материала (по ссылке)
            // из раздела The Naive Solution формулы веса трёх точек
            // оформить их в коде

            // Дать соответствующие значения для W1, W2, W3
            // Подсказка: X,Y - координаты вершин треугольника (tri.points)
            // P - точка внутри треугольника, для которой интерполируем значения
            double[] d = new double[3];

            for(int i = 0; i<3; i++)
            {
                d[i] = Math.Sqrt(Math.Pow(tri.points[i].pos.x - pt.x, 2) +
                    Math.Pow(tri.points[i].pos.y - pt.y, 2));
            }

            W1 = 1 / d[0];
            W2 = 1 / d[1];
            W3 = 1 / d[2];
        }

        public double Interpolate(double v1, double v2, double v3)
        {
            // Сюда адаптируем формулу Color = ... там же, по ссылке
            return (W1 * v1 + W2 * v2 + W3 * v3) / (W1 + W2 + W3);
        }
    }

    // Пример для #4, #5
    public class Triangle : Shape
    {
        public PointI[] points { get; private set; }
        public PointI Position { get; set; }
        public Color Color { get; set; }

        public Triangle(PointI p1, PointI p2, PointI p3)
        {
            points = new PointI[3];
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
        }

        public override void Draw(PixelArrayRGB bitmap)
        {
            Drawing.DrawFilledTriangle(bitmap, Color, this);
            Drawing.DrawTriangle(bitmap, Color, points[0], points[1], points[2]);
        }
    }

    public class Polygon : Shape
    {
        public List<PointI> points { get; private set; } = new List<PointI>();
        public PointI Position { get; set; } // #1
        public Color Color { get; set; } // #2

        public void SetPosition(int x, int y) { Position = new PointI(x, y); }

        public override void Draw(PixelArrayRGB bitmap)
        {
            PointI p1 = new PointI(), p2 = new PointI();
            for (int i = 0; i < points.Count; i++)
            {
                // #1
                p1.x = points[i].x + Position.x;
                p1.y = points[i].y + Position.y;
                p2.x = points[(i + 1) % points.Count].x + Position.x;
                p2.y = points[(i + 1) % points.Count].y + Position.y;
                Drawing.DrawLine(bitmap, p1, p2, Color); // #1,#2
            }
        }

        // Статический метод для генерации правильного многоугольника
        // Параметры:
        // sides - количество сторон
        // radius - радиус описанной окружности
        // offset - смещение на картинке
        public static Polygon GenerateRegularPolygon(uint sides, double radius, PointI offset)
        {
            Polygon p = new Polygon();

            // Угол между двумя сторонами многоугольника = 360 / кол-во сторон
            double alpha = (2 * Math.PI) / sides;

            for (uint i = 0; i < sides; i++)
            {
                double x = offset.x + radius * Math.Cos(-Math.PI / 2 + i * alpha);
                double y = offset.y + radius * Math.Sin(-Math.PI / 2 + i * alpha);
                p.points.Add(new PointI((int)x, (int)y));
            }

            return p;
        }
    }
}
