using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfDrawing
{
    public class Drawing
    {
        public static void DrawVerticalLine(PixelArray img, int x, Color c)
        {
            for(int y=0; y<img.Height; y++)
            {
                img.SetPixel(x, y, c);
            }
        }

        public static void DrawHorizontalLine(PixelArray img, int y, Color c)
        {
            for (int x = 0; x < img.Width; x++)
            {
                img.SetPixel(x, y, c);
            }
        }

        // Задание 1: Реализовать функцию для отрисовки горизонтальной линии
        // Задание 2: Реализовать функцию для отрисовки диагональной линии
        //            (определяем начальный y, рисуем от x=0)

        // Сделать линейную функцию
        // y = k*x + b
        // y = tg(a)*x + b
        // k = tg(a)
        public static int LinearFunction(int x, double k, double b)
        {
            return (int)Math.Round(k * x + b);
        }

        // Функция, обратная к линейной
        // x = (y - b) / k
        public static int InverseLinearFunction(int y, double k, double b)
        {
            return (int)Math.Round((y - b) / k);
        }

        public static void DrawLinearFunctionBetweenPoints(
            PixelArray img, Color c, double k, double b,
            int x0, int y0, int x1, int y1
            )
        {
            // 1 - int
            // 1.0 - double
            // 1.0f - float
            if (Math.Abs(k) <= 1.0)
            {
                for (int x = x0; x <= x1; x++)
                {
                    int y = LinearFunction(x, k, b);
                    img.SetPixel(x, y, c);
                }
            }
            else
            {
                for (int y = y0; y <= y1; y++)
                {
                    int x = InverseLinearFunction(y, k, b);
                    img.SetPixel(x, y, c);
                }
            }
        }

        public static void DrawLinearFunction(PixelArray img, double k, double b, Color c)
        {
            DrawLinearFunctionBetweenPoints(img, c, k, b, 0, 0, img.Width - 1, img.Height - 1);
        }

        public static void DrawLine(PixelArray img, Color c, Point2i p1, Point2i p2)
        {
            DrawLine(img, c, p1.x, p1.y, p2.x, p2.y);
        }

        public static void DrawLine(
            PixelArray img, Color c,
            int x0, int y0, int x1, int y1)
        {
            double delta_x = x1 - x0;
            double delta_y = y1 - y0;

            int start_x = (x0 < x1) ? x0 : x1;
            int start_y = (y0 < y1) ? y0 : y1;
            int end_x = (x0 > x1) ? x0 : x1;
            int end_y = (y0 > y1) ? y0 : y1;


            if (delta_x == 0.0)
            {
                for (int y = start_y; y <= end_y; y++) 
                {
                    img.SetPixel(start_x, y, c);
                }
            }
            else if(delta_y == 0.0)
            {
                for (int x = start_x; x <= end_x; x++)
                {
                    img.SetPixel(x, start_y, c);
                }
            }
            else
            {
                double k = delta_y / delta_x;
                double b = y0 - k * x0;

                DrawLinearFunctionBetweenPoints(img, c, k, b, 
                    start_x, start_y, end_x, end_y);
            }
        }

        public static void DrawRectangle(PixelArray img, Color c, 
            int min_x, int min_y, int max_x, int max_y)
        {
            DrawLine(img, c, min_x, min_y, max_x, min_y);
            DrawLine(img, c, min_x, max_y, max_x, max_y);
            DrawLine(img, c, min_x, min_y, min_x, max_y);
            DrawLine(img, c, max_x, min_y, max_x, max_y);
        }

        // Задание 7: Сделать функцию, которая рисует треугольник по трём точкам
        public static void DrawTriangle(PixelArray img, Color c, int Ax, int Ay, int Bx, int By, int Cx, int Cy)
        {
            DrawLine(img, c, Ax, Ay, Bx, By);
            DrawLine(img, c, Ax, Ay, Cx, Cy);
            DrawLine(img, c, Cx, Cy, Bx, By);
        }

        public static void DrawFilledRectangle(PixelArray img, Color c,
            int min_x, int min_y, int max_x, int max_y)
        {
            /*for (int y = 0; y < img.Height; y++) 
            {
                for(int x = 0; x < img.Width; x++)
                {
                    if(x >= min_x && x <= max_x && y >= min_y && y <= max_y)
                    {
                        img.SetPixel(x, y, c);
                    }
                }
            }*/

            for(int y = min_y; y <= max_y; y++)
            {
                for (int x = min_x; x <= max_x; x++)
                {
                    img.SetPixel(x, y, c);
                }
            }
        }

        public static void DrawCircle(PixelArray img, Color c, 
            Point2i center, double radius)
        {
            double min_x = -radius;
            double max_x = radius;

            double min_y = -radius;
            double max_y = radius;

            for (double y = min_y; y <= max_y; y++)
            {
                for (double x = min_x; x <= max_x; x++)
                {
                    double xy = Math.Sqrt(x * x + y * y);

                    if((int)xy == (int)radius)
                    {
                        img.SetPixel((int)x+center.x, (int)y+center.y, c);
                    }
                }
            }
        }

        public static void DrawFilledCircle(PixelArray img, Color c,
            Point2i center, double radius)
        {
            double min_x = -radius;
            double max_x = radius;

            double min_y = -radius;
            double max_y = radius;

            for (double y = min_y; y <= max_y; y++)
            {
                for (double x = min_x; x <= max_x; x++)
                {
                    double xy = Math.Sqrt(x * x + y * y);

                    if ((int)xy <= (int)radius)
                    {
                        img.SetPixel((int)x + center.x, (int)y + center.y, c);
                    }
                }
            }
        }

        public static void DrawHorizontalGradient(PixelArray img, 
            Rectangle rect, Color c1, Color c2)
        {
            int start_x = rect.MinX;
            int end_x = rect.MaxX; 

            for(int x = start_x; x <=end_x; x++)
            {
                // c1 = { 0, 0, 0, 255 } => c2 = { 255, 255, 255, 255 }
                // Здесь будут преобразования цветов
                Color color = c1;

                // Находим нормализованное расстояние текущей X от конечной X
                // (т.е. долю пройденного пути от начальной до конечной X)
                double normdist = (x - start_x) / (double)(end_x - start_x);

                int r_diff = c2.R - c1.R;
                color.R = (byte)(c1.R + normdist * r_diff);

                // { 0, 128, 0 } => { 0, 0, 0 }
                int g_diff = c2.G - c1.G;
                color.G = (byte)(c1.G + normdist * g_diff);

                int b_diff = c2.B - c1.B;
                color.B = (byte)(c1.B + normdist * b_diff);

                //DrawVerticalLine(img, x, color);
                for(int y = rect.MinY; y <= rect.MaxY; y++)
                {
                    img.SetPixel(x, y, color);
                }
            }
        }

        // Задание 10: Реализовать функцию для отрисовки вертикального градиента

        public static void DrawCircularGradient(PixelArray img, Circle circle,
            Color c1, Color c2)
        {
            double min_x = -circle.radius;
            double max_x = circle.radius;

            double min_y = -circle.radius;
            double max_y = circle.radius;

            for (double y = min_y; y <= max_y; y++)
            {
                for (double x = min_x; x <= max_x; x++)
                {
                    // double normdist = (x - start_x) / (double)(end_x - start_x);
                    // double normdist

                    double distance = Math.Sqrt(x * x + y * y);

                    double normdist = distance / circle.Radius;

                    if ((int)distance <= (int)circle.Radius)
                    {
                        Color color = new Color();

                        int r_diff = c2.R - c1.R;
                        color.R = (byte)(c1.R + normdist * r_diff);

                        int g_diff = c2.G - c1.G;
                        color.G = (byte)(c1.G + normdist * g_diff);

                        int b_diff = c2.B - c1.B;
                        color.B = (byte)(c1.B + normdist * b_diff);

                        img.SetPixel(
                            (int)x + circle.Center.x, 
                            (int)y + circle.Center.y, 
                            color);
                    }
                }
            }
        }

        // Задание 11: Сделать функцию, которая рисует кольцо одним цветом
        // (принимает два круга - объекта типа Circle и цвет заливки,
        //  заливает только площадь между меньшим и большим кругом)

        // Задание 12: Сделать функцию, которая рисует градиент для кольца
        // (даёт плавный переход от первого цвета ко второму при движении
        //  от внутреннего круга к внешнему)

        // Задание 13: Нарисовать по такой схеме круг с градиентом в центре и 10 колец
        // расходящихся от него по радиусу. Радиус круга и толщина колец 25.

        public static void DrawFilledTriangle(PixelArray img, Color c, Triangle tri)
        {
            // Находим размеры прямоугольника, в который помещается треугольник
            // из него берём точки - кандидаты на попадание в область треугольника
            int min_x = tri.Points.Min((pt) => pt.x);
            int max_x = tri.Points.Max((pt) => pt.x);
            int min_y = tri.Points.Min((pt) => pt.y);
            int max_y = tri.Points.Max((pt) => pt.y);

            // Сортируем точки по y
            List<Point2i> pts = new List<Point2i>(tri.Points);
            pts.Sort((pt1, pt2) => pt1.y - pt2.y);

            int mid_x = pts[1].x;
            int mid_y = pts[1].y;

            /*foreach(Point2i pt in tri.Points)
            {
                if (pt.x > min_x && pt.x < max_x) { mid_x = pt.x; }
                if (pt.y > min_y && pt.y < max_y) { mid_y = pt.y; }
            }*/

            // Рисуем верхний треугольник
            double k1 = (pts[0].y - pts[1].y) / (double)(pts[0].x - pts[1].x);
            double k2 = (pts[0].y - pts[2].y) / (double)(pts[0].x - pts[2].x);
            double k3 = (pts[1].y - pts[2].y) / (double)(pts[1].x - pts[2].x);

            double b1 = pts[0].y - k1 * pts[0].x;
            double b2 = pts[0].y - k2 * pts[0].x;
            double b3 = pts[1].y - k3 * pts[1].x;

            // 0 - верхняя, 1 - средняя, 2 - нижняя
            Function f1 = new LinearFunction(k1, b1); // 0-1
            Function f2 = new LinearFunction(k2, b2); // 0-2
            Function f3 = new LinearFunction(k3, b3); // 1-2

            if (double.IsInfinity(k1)) f1 = new VerticalLineFunction(pts[0].x);
            else if (double.IsInfinity(k2)) f2 = new VerticalLineFunction(pts[0].x);
            else if (double.IsInfinity(k3)) f3 = new VerticalLineFunction(pts[1].x);

            if (pts[0].y == pts[1].y)
            {
                DrawSubtriangle(img, f2, f3, min_y, max_y, (x, y) => c);
                //DrawSubtriangle(img, new VerticalLineFunction(pts[0].x), f3, min_y, max_y, (x, y) => c);
            }
            else if(pts[1].y == pts[2].y)
            {
                DrawSubtriangle(img, f1, f2, min_y, max_y, (x, y) => c);
            }
            else
            {
                DrawSubtriangle(img, f1, f2, min_y, mid_y, (x, y) => c);
                DrawSubtriangle(img, f2, f3, mid_y + 1, max_y, (x, y) => c);
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

        public static void DrawSubtriangle(PixelArray img, Function fn1, Function fn2, int start_y, int end_y, Func<int, int, Color> color_fn)
        {
            for (int y = start_y; y <= end_y; y++)
            {
                int start_x = (int)fn1.x(y);
                int end_x = (int)fn2.x(y);

                if (start_x > end_x)
                {
                    int temp = start_x;
                    start_x = end_x;
                    end_x = temp;
                }

                for (int x = start_x; x <= end_x; x++)
                {
                    img.SetPixel(x, y, color_fn(x,y));
                }
            }
        }
    }

    public interface Function
    {
        double y(int x);
        double x(int y);
    }

    // y = k * x + b
    public class LinearFunction : Function
    {
        double k, b;

        public LinearFunction(double p_k, double p_b)
        {
            k = p_k;
            b = p_b;
        }

        // Значение прямой линейной функции
        // y = k * x + b
        public double y(int x) => k * x + b;

        // Функция, обратная к линейной
        // x = (y - b) / k
        public double x(int y) => (y - b) / k;
    }

    public class VerticalLineFunction : Function
    {
        double x_value;

        public VerticalLineFunction(double p_x)
        {
            x_value = p_x;
        }

        public double y(int x) => double.PositiveInfinity;
        public double x(int y) => x_value;
    }

    public class Point2i
    {
        public int x, y;
    }

    public class Triangle
    {
        public Point2i[] Points { get; private set; } = new Point2i[3];

        public Point2i A { get => Points[0]; set => Points[0] = value; }
        public Point2i B { get => Points[1]; set => Points[1] = value; }
        public Point2i C { get => Points[2]; set => Points[2] = value; }

        public Triangle(Point2i a, Point2i b, Point2i c)
        {
            A = a;
            B = b;
            C = c;
        }
    }

    public class Circle
    {
        public Point2i Center { get; set; } = new Point2i();
        public double radius = 0.0;

        public double Radius
        {
            get => radius;
            // Если присваивается отрицательный радиус - берём его со знаком "-"
            set => radius = (value >= 0.0) ? value : -value;
        }

        public Circle() {}

        public Circle(Point2i p_center, double p_radius)
        {
            Center = p_center;
            Radius = p_radius;
        }
    }

    public class Rectangle
    {
        public Point2i p1 = new Point2i();
        public Point2i p2 = new Point2i();

        public int MinX { get => p1.x; set => p1.x = value; }
        public int MaxX { get => p2.x; set => p2.x = value; }
        public int MinY { get => p1.y; set => p1.y = value; }
        public int MaxY { get => p2.y; set => p2.y = value; }

        public Rectangle() { }

        public Rectangle(int min_x, int max_x, int min_y, int max_y)
        {
            MinX = min_x;
            MaxX = max_x;
            MinY = min_y;
            MaxY = max_y;
        }
    }

    public class Polygon
    {
        public List<Point2i> Points { get; private set; } = new List<Point2i>();
        public Color Color { get; set; } = Colors.White;
        public Point2i Position { get; private set; } = new Point2i();

        // sides - количество сторон многоугольника
        // radius - радиус описанной окружности
        // pos - смещение относительно центра
        public static Polygon GenerateRegularPolygon(
            int sides, double radius, Point2i pos)
        {
            Polygon p = new Polygon();
            p.Position = pos;

            double angle = 2.0 * Math.PI / sides;

            for (int i = 0; i < sides; i++)
            {
                double x = radius * Math.Cos(angle * i);
                double y = radius * Math.Sin(angle * i);

                Point2i pt = new Point2i() { x = (int)x, y = (int)y };
                p.Points.Add(pt);
            }

            return p;
        }

        public void Draw(PixelArray img)
        {
            Point2i p1 = new Point2i(), p2 = new Point2i();

            for(int i=0; i < Points.Count; i++)
            {
                p1.x = Points[i].x + Position.x;
                p1.y = Points[i].y + Position.y;

                p2.x = Points[(i + 1) % Points.Count].x + Position.x;
                p2.y = Points[(i + 1) % Points.Count].y + Position.y;

                Drawing.DrawLine(img, Color, p1, p2);
            }
        }
    }
}
