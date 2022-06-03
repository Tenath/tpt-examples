using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfDrawing
{
    public class Shape
    {
    }

    public class Vertex
    {
        public Point2i pos;
        public Color color;
        public Point2i texpos;

        public int X { get => pos.x; set => pos.x = value; }
        public int Y { get => pos.y; set => pos.y = value; }

        public Vertex(Point2i p_pos, Color p_color)
        {
            pos = p_pos;
            color = p_color;
            texpos = new Point2i();
        }
    }

    public class ColoredTriangle : Shape
    {
        public Vertex[] Points { get; private set; }
        public Point2i Position { get; set; }

        public Vertex A { get => Points[0]; set => Points[0] = value; }
        public Vertex B { get => Points[1]; set => Points[1] = value; }
        public Vertex C { get => Points[2]; set => Points[2] = value; }

        public ColoredTriangle(Vertex p1, Vertex p2, Vertex p3)
        {
            Points = new Vertex[3];
            Points[0] = p1;
            Points[1] = p2;
            Points[2] = p3;

            Position = new Point2i() { x = 0, y = 0 };
        }
        
        public Color InterpolateColor(BarycentricCoordinate cd)
        {
            double r = (cd.L1 * A.color.R + cd.L2 * B.color.R + cd.L3 * C.color.R);
            double g = (cd.L1 * A.color.G + cd.L2 * B.color.G + cd.L3 * C.color.G);
            double b = (cd.L1 * A.color.B + cd.L2 * B.color.B + cd.L3 * C.color.B);

            return Color.FromRgb(
                (byte)r,
                (byte)g,
                (byte)b
            );
        }

        public Point2i InterpolateTexel(BarycentricCoordinate cd)
        {
            double px = (cd.L1 * A.texpos.x + cd.L2 * B.texpos.x + cd.L3 * C.texpos.x);
            double py = (cd.L1 * A.texpos.y + cd.L2 * B.texpos.y + cd.L3 * C.texpos.y);

            return new Point2i() { x = (int)px, y = (int)py };
        }

        public void Draw(PixelArray img)
        {
            // Находим размеры прямоугольника, в который помещается треугольник
            // из него берём точки - кандидаты на попадание в область треугольника
            int min_x = Points.Min((pt) => pt.pos.x);
            int max_x = Points.Max((pt) => pt.pos.x);
            int min_y = Points.Min((pt) => pt.pos.y);
            int max_y = Points.Max((pt) => pt.pos.y);

            // Сортируем точки по y
            List<Vertex> pts = new List<Vertex>(Points);
            pts.Sort((pt1, pt2) => pt1.pos.y - pt2.pos.y);

            int mid_x = pts[1].pos.x;
            int mid_y = pts[1].pos.y;

            /*foreach(Point2i pt in tri.Points)
            {
                if (pt.x > min_x && pt.x < max_x) { mid_x = pt.x; }
                if (pt.y > min_y && pt.y < max_y) { mid_y = pt.y; }
            }*/

            // Рисуем верхний треугольник
            double k1 = (pts[0].pos.y - pts[1].pos.y) / (double)(pts[0].pos.x - pts[1].pos.x);
            double k2 = (pts[0].pos.y - pts[2].pos.y) / (double)(pts[0].pos.x - pts[2].pos.x);
            double k3 = (pts[1].pos.y - pts[2].pos.y) / (double)(pts[1].pos.x - pts[2].pos.x);

            double b1 = pts[0].pos.y - k1 * pts[0].pos.x;
            double b2 = pts[0].pos.y - k2 * pts[0].pos.x;
            double b3 = pts[1].pos.y - k3 * pts[1].pos.x;

            // 0 - верхняя, 1 - средняя, 2 - нижняя
            Function f1 = new LinearFunction(k1, b1); // 0-1
            Function f2 = new LinearFunction(k2, b2); // 0-2
            Function f3 = new LinearFunction(k3, b3); // 1-2

            if (double.IsInfinity(k1)) f1 = new VerticalLineFunction(pts[0].pos.x);
            else if (double.IsInfinity(k2)) f2 = new VerticalLineFunction(pts[0].pos.x);
            else if (double.IsInfinity(k3)) f3 = new VerticalLineFunction(pts[1].pos.x);

            Func<int, int, Color> GetColor = (x, y) =>
            {
                Point2i pt = new Point2i();
                pt.x = x;
                pt.y = y;
                var coord = BarycentricCoordinate.Compute(A, B, C, pt);
                return InterpolateColor(coord);
            };

            if (pts[0].pos.y == pts[1].pos.y)
            {
                Drawing.DrawSubtriangle(img, f2, f3, min_y, max_y, GetColor);
                //DrawSubtriangle(img, new VerticalLineFunction(pts[0].pos.x), f3, min_y, max_y, (x, y) => c);
            }
            else if (pts[1].pos.y == pts[2].pos.y)
            {
                Drawing.DrawSubtriangle(img, f1, f2, min_y, max_y, GetColor);
            }
            else
            {
                Drawing.DrawSubtriangle(img, f1, f2, min_y, mid_y, GetColor);
                Drawing.DrawSubtriangle(img, f2, f3, mid_y + 1, max_y, GetColor);
            }
            /*for (int y = min_y; y <= mid_y; y++)
            {
                int start_x = (int)f1.x(y);
                int end_x = (int)f2.x(y);

                if(start_x > end_x)
                {
                    int temp = start_x;
                    start_x = end_x;
                    end_x = temp;
                }

                for(int x = start_x; x <= end_x; x++)
                {
                    img.SetPixel(x, y, c);
                }
            }*/

            // Рисуем нижний треугольник
            /*for (int y = mid_y+1; y <= max_y; y++)
            {
                int start_x = (int)f2.x(y);
                int end_x = (int)f3.x(y);

                if (start_x > end_x)
                {
                    int temp = start_x;
                    start_x = end_x;
                    end_x = temp;
                }

                for (int x = start_x; x <= end_x; x++)
                {
                    img.SetPixel(x, y, c);
                }
            }*/
        }

        public void DrawColor(PixelArray img, Func<int, int, Color> GetColor)
        {
            // Находим размеры прямоугольника, в который помещается треугольник
            // из него берём точки - кандидаты на попадание в область треугольника
            int min_x = Points.Min((pt) => pt.pos.x);
            int max_x = Points.Max((pt) => pt.pos.x);
            int min_y = Points.Min((pt) => pt.pos.y);
            int max_y = Points.Max((pt) => pt.pos.y);

            // Сортируем точки по y
            List<Vertex> pts = new List<Vertex>(Points);
            pts.Sort((pt1, pt2) => pt1.pos.y - pt2.pos.y);

            int mid_x = pts[1].pos.x;
            int mid_y = pts[1].pos.y;

            /*foreach(Point2i pt in tri.Points)
            {
                if (pt.x > min_x && pt.x < max_x) { mid_x = pt.x; }
                if (pt.y > min_y && pt.y < max_y) { mid_y = pt.y; }
            }*/

            // Рисуем верхний треугольник
            double k1 = (pts[0].pos.y - pts[1].pos.y) / (double)(pts[0].pos.x - pts[1].pos.x);
            double k2 = (pts[0].pos.y - pts[2].pos.y) / (double)(pts[0].pos.x - pts[2].pos.x);
            double k3 = (pts[1].pos.y - pts[2].pos.y) / (double)(pts[1].pos.x - pts[2].pos.x);

            double b1 = pts[0].pos.y - k1 * pts[0].pos.x;
            double b2 = pts[0].pos.y - k2 * pts[0].pos.x;
            double b3 = pts[1].pos.y - k3 * pts[1].pos.x;

            // 0 - верхняя, 1 - средняя, 2 - нижняя
            Function f1 = new LinearFunction(k1, b1); // 0-1
            Function f2 = new LinearFunction(k2, b2); // 0-2
            Function f3 = new LinearFunction(k3, b3); // 1-2

            if (double.IsInfinity(k1)) f1 = new VerticalLineFunction(pts[0].pos.x);
            else if (double.IsInfinity(k2)) f2 = new VerticalLineFunction(pts[0].pos.x);
            else if (double.IsInfinity(k3)) f3 = new VerticalLineFunction(pts[1].pos.x);

            if (pts[0].pos.y == pts[1].pos.y)
            {
                Drawing.DrawSubtriangle(img, f2, f3, min_y, max_y, GetColor);
                //DrawSubtriangle(img, new VerticalLineFunction(pts[0].pos.x), f3, min_y, max_y, (x, y) => c);
            }
            else if (pts[1].pos.y == pts[2].pos.y)
            {
                Drawing.DrawSubtriangle(img, f1, f2, min_y, max_y, GetColor);
            }
            else
            {
                Drawing.DrawSubtriangle(img, f1, f2, min_y, mid_y, GetColor);
                Drawing.DrawSubtriangle(img, f2, f3, mid_y + 1, max_y, GetColor);
            }
        }
    }

    public class TriWeight
    {
        // веса (значимости) точек
        private double W1, W2, W3;

        public TriWeight(ColoredTriangle tri, Point2i pt)
        {
            // https://codeplea.com/triangular-interpolation
            // Задача: взять из теоретического материала (по ссылке)
            // из раздела The Naive Solution формулы веса трёх точек
            // оформить их в коде

            // Дать соответствующие значения для W1, W2, W3
            // Подсказка: X,Y - координаты вершин треугольника (tri.points)
            // P - точка внутри треугольника, для которой интерполируем значения
            double[] d = new double[3];

            for (int i = 0; i < 3; i++)
            {
                d[i] = Math.Sqrt(Math.Pow(tri.Points[i].pos.x - pt.x, 2) +
                    Math.Pow(tri.Points[i].pos.y - pt.y, 2));
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

    public class BarycentricCoordinate
    {
        public double L1, L2, L3;

        public static BarycentricCoordinate Compute(Vertex v1, Vertex v2, Vertex v3, Point2i pt)
        {
            BarycentricCoordinate b = new BarycentricCoordinate();

            double div = (v2.Y - v3.Y) * (v1.X - v3.X) + (v3.X - v2.X) * (v1.Y - v3.Y);

            double L1 = ((v2.Y - v3.Y) * (pt.x - v3.X) + (v3.X - v2.X) * (pt.y - v3.Y)) / div;
            double L2 = ((v3.Y - v1.Y) * (pt.x - v3.X) + (v1.X - v3.X) * (pt.y - v3.Y)) / div;
            double L3 = 1 - L1 - L2;

            b.L1 = Utils.ClampValue(L1);
            b.L2 = Utils.ClampValue(L2);
            b.L3 = Utils.ClampValue(L3);

            return b;
        }
    }

    public class Utils
    {
        public Color Interpolate(Color c1, Color c2, double normdist)
        {
            Color color = new Color();

            int r_diff = c2.R - c1.R;
            color.R = (byte)(c1.R + normdist * r_diff);

            int g_diff = c2.G - c1.G;
            color.G = (byte)(c1.G + normdist * g_diff);

            int b_diff = c2.B - c1.B;
            color.B = (byte)(c1.B + normdist * b_diff);

            return color;
        }

        public static double ClampValue(double val)
        {
            return (val < 0.0) ? 0.0 : (val > 1.0) ? 1.0 : val;
        }
    }
}
