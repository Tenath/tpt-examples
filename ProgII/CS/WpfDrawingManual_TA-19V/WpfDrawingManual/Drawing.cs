// (c) Andrei Veeremaa @ TPT, 2019
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfDrawingManual
{
    public static class Drawing
    {
        public static void DrawHorizontalLine(PixelArrayRGB bitmap)
        {
            int center_x = bitmap.Width / 2;
            for (int x = center_x - 1; x < center_x + 2; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    bitmap.SetPixel(x, y, Colors.Red);
                }
            }
        }

        public static void DrawVerticalLine(PixelArrayRGB bitmap)
        {
            int center_y = bitmap.Height / 2;
            for (int x = 0; x < bitmap.Width; x++)
            {
                // y0 = center_y-1
                // y1 = center_y
                // y2 = center_y+1
                for (int y = center_y - 1; y < center_y + 2; y++)
                {
                    bitmap.SetPixel(x, y, Colors.Red);
                }
            }
        }

        public static void DrawLinearFunction(PixelArrayRGB bitmap)
        {
            PointI center = bitmap.Center;
            int startX = center.x, startY = center.y;

            int smallestAxis = (bitmap.Height < bitmap.Width) ? bitmap.Height : bitmap.Width;

            for(int x=-30; x<=30;x++)
            {
                // значение функции y = 1*x + 0 => y=x
                int y = LinearFunction(x, 0.5, -50);

                PointI pointOnImage = bitmap.ShiftToCenter(x, y);

                bitmap.SetPixel(pointOnImage, Colors.Blue);
            }
        }

        public static int LinearFunction(int x, double k, double b)
        {
            double y = k * x + b;
            return (int)Math.Round(y);
        }

        public static void DrawFunction(PixelArrayRGB bitmap, Color c, Func<int,int> fn)
        {
            for (int x = -bitmap.Width/2; x <= bitmap.Width/2; x++)
            {
                // значение функции y = 1*x + 0 => y=x
                int y = fn(x);

                PointI pointOnImage = bitmap.ShiftToCenter(x, y);

                bitmap.SetPixel(pointOnImage, c);
            }
        }

        // Функция-обёртка, которая принимает PointI вместо отдельных координат в int
        public static void DrawLine(PixelArrayRGB bitmap, PointI p1, PointI p2, Color c)
        {
            DrawLine(bitmap, p1.x, p1.y, p2.x, p2.y, c);
        }

        public static void DrawLine(PixelArrayRGB bitmap, int x0, int y0, int x1, int y1, Color c)
        {
            double delta_x = x1 - x0;
            double delta_y = y1 - y0;

            //int x = 0;
            //int y = delta_y / delta_x * (x - x0) + y0;
            int start_x = (x0 < x1) ? x0 : x1;
            int start_y = (y0 < y1) ? y0 : y1;

            int end_x = (x0 > x1) ? x0 : x1;
            int end_y = (y0 > y1) ? y0 : y1;

            if (delta_x != 0) // если линия не вертикальная
            {
                double k = delta_y / (double)delta_x;

                if(Math.Abs(k)<1.0)
                {
                    for (int x = start_x; x <= end_x; x++)
                    {
                        int y = (int)(delta_y / delta_x * (x - x0) + y0);
                        bitmap.SetPixel(x, y, c);
                    }
                }
                else
                {
                    for (int y = start_y; y<=end_y; y++)
                    {
                        int x = (int)(delta_x / delta_y * (y - y0) + x0);
                        bitmap.SetPixel(x, y, c);
                    }
                }
                
            }
            else
            {
                for(int y=start_y; y<end_y;y++)
                {
                    bitmap.SetPixel(x0, y, c);
                }
            }
            // Задание на дом: реализовать отрисовку по y для случаев, когда k >> 1

        }

        public static void DrawTriangle(PixelArrayRGB bitmap, Color c, 
            PointI p1, PointI p2, PointI p3)
        {
            DrawLine(bitmap, p1, p2, c);
            DrawLine(bitmap, p2, p3, c);
            DrawLine(bitmap, p3, p1, c);
        }

        // Отрисовка залитого треугольника - основы полигональной графики
        public static void DrawFilledTriangle(PixelArrayRGB bitmap, Color c,
            Triangle tri)
        {
            // Шаг 1: Определить минимальные/максимальные значения x,y
            // среди трёх точек, составляющих треугольник            
            int min_x = tri.points.Min((p) => p.x);
            int max_x = tri.points.Max((p) => p.x);
            int min_y = tri.points.Min((p) => p.y);
            int max_y = tri.points.Max((p) => p.y);

            // Шаг 2: Делаем сортированный по x список точек
            List<PointI> pts = new List<PointI>(tri.points);
            // Компаратор, во многих языках устроен по такой же схеме
            // < 0, если левое значение меньше правого
            // = 0, если значения равны
            // > 0, если левое значение больше правого
            pts.Sort((pt1, pt2) => pt1.x - pt2.x);

            // Шаг 3: Создаём список ребёр (представленных, как
            // линейные функции)
            // k = tan(a) = dy/dx
            double vertical_line = min_x - 1.0;
            List<LinearFunction> edges = new List<LinearFunction>();
            for (int i = 0; i < 3; i++) 
            {
                double dy = pts[(i + 1) % 3].y - pts[i].y;
                double dx = pts[(i + 1) % 3].x - pts[i].x;

                if(dx==0)
                {
                    vertical_line = pts[i].x;
                    continue;
                }

                double K = dy / dx;
                // y = kx + b => b = y-kx
                double B = pts[i].y - K * pts[i].x;

                edges.Add(new LinearFunction()
                {
                    k = K,
                    b = B
                });
            }

            // Алгоритм "сканирующая прямая (scanline)"
            List<double> x_coords = new List<double>();
            for(int y = min_y; y < max_y; y++)
            {
                // Ищем точки пересечения рёбер со сканирующей прямой
                foreach(LinearFunction edge in edges)
                {
                    // Находим x по известному y (из обратной функции)
                    double x = edge.X(y);
                    // Если найденная координата входит в
                    // прямоугольник, в который вписан треугольник
                    // добавляем в список координат x
                    if (x >= min_x && x <= max_x) x_coords.Add(x);
                }

                if (vertical_line >= min_x) x_coords.Add(vertical_line);
                // Если только одна точка пересечения 
                // (верхняя или нижняя вершина треугольника)
                if (x_coords.Count == 1) bitmap.SetPixel((int)x_coords[0], y, c);
                else if(x_coords.Count >= 2)
                {
                    x_coords.Sort();

                    for(int x = (int)x_coords[0]; x < (int)x_coords[1]; x++)
                    {
                        bitmap.SetPixel(x, y, c);
                    }
                }
                // После того, как отработали со сканирующей прямой,
                // очищаем список пересечений
                x_coords.Clear();
            }
        }

        // https://codeplea.com/triangular-interpolation
        public static void DrawGradientTriangle(PixelArrayRGB bitmap, 
            ColoredTriangle tri)
        {
            // Шаг 1: Определить минимальные/максимальные значения x,y
            // среди трёх точек, составляющих треугольник            
            int min_x = tri.points.Min((p) => p.pos.x);
            int max_x = tri.points.Max((p) => p.pos.x);
            int min_y = tri.points.Min((p) => p.pos.y);
            int max_y = tri.points.Max((p) => p.pos.y);

            // Шаг 2: Делаем сортированный по x список точек
            List<PointI> pts = new List<PointI>(tri.points.Length);
            foreach(Vertex v in tri.points)
            {
                pts.Add(v.pos);
            }

            // Домашнее задание (17.11):
            // * Реализовать чтение треугольников с градиентами из файла
            // * Самостоятельно сделать из треугольников фигуру (на выбор)
            // *** (доп. задание) Реализовать механизм интерполяции координат на текстуре
            //     (т.е. картинке), соответственно спроецировать часть картинки на треугольник

            // Компаратор, во многих языках устроен по такой же схеме
            // < 0, если левое значение меньше правого
            // = 0, если значения равны
            // > 0, если левое значение больше правого
            pts.Sort((pt1, pt2) => pt1.x - pt2.x);

            // Шаг 3: Создаём список ребёр (представленных, как
            // линейные функции)
            // k = tan(a) = dy/dx
            double vertical_line = min_x - 1.0;
            List<LinearFunction> edges = new List<LinearFunction>();
            for (int i = 0; i < 3; i++)
            {
                double dy = pts[(i + 1) % 3].y - pts[i].y;
                double dx = pts[(i + 1) % 3].x - pts[i].x;

                if (dx == 0)
                {
                    vertical_line = pts[i].x;
                    continue;
                }

                double K = dy / dx;
                // y = kx + b => b = y-kx
                double B = pts[i].y - K * pts[i].x;

                edges.Add(new LinearFunction()
                {
                    k = K,
                    b = B
                });
            }

            // Алгоритм "сканирующая прямая (scanline)"
            List<double> x_coords = new List<double>();
            for (int y = min_y; y < max_y; y++)
            {
                // Ищем точки пересечения рёбер со сканирующей прямой
                foreach (LinearFunction edge in edges)
                {
                    // Находим x по известному y (из обратной функции)
                    double x = edge.X(y);
                    // Если найденная координата входит в
                    // прямоугольник, в который вписан треугольник
                    // добавляем в список координат x
                    if (x >= min_x && x <= max_x) x_coords.Add(x);
                }

                if (vertical_line >= min_x) x_coords.Add(vertical_line);
                // Если только одна точка пересечения 
                // (верхняя или нижняя вершина треугольника)
                if (x_coords.Count == 1)
                {
                    TriWeight wt = new TriWeight(tri, new PointI((int)x_coords[0], y));
                    Color c = tri.InterpolateColor(wt);

                    bitmap.SetPixel((int)x_coords[0], y, c);
                }
                else if (x_coords.Count >= 2)
                {
                    x_coords.Sort();

                    for (int x = (int)x_coords[0]; x < (int)x_coords[1]; x++)
                    {
                        TriWeight wt = new TriWeight(tri, new PointI(x, y));
                        Color c = tri.InterpolateColor(wt);
                        bitmap.SetPixel(x, y, c);
                    }
                }
                // После того, как отработали со сканирующей прямой,
                // очищаем список пересечений
                x_coords.Clear();
            }
        }



        public static void DrawRectangle(PixelArrayRGB bitmap, Color c,
            PointI p1, PointI p2)
        {
            DrawLine(bitmap, p1.x, p1.y, p1.x, p2.y, c);
            DrawLine(bitmap, p1.x, p1.y, p2.x, p1.y, c);
            DrawLine(bitmap, p1.x, p2.y, p2.x, p2.y, c);
            DrawLine(bitmap, p2.x, p1.y, p2.x, p2.y, c);
        }

        public static void DrawFilledRectangle(PixelArrayRGB bitmap, Color c,
            PointI p1, int w, int h)
        {
            for(int y = p1.y; y <= p1.y+h; y++)
            {
                for(int x = p1.x; x <= p1.x+w; x++)
                {
                    bitmap.SetPixel(x, y, c);
                }
            }
        }

        public static void DrawFilledRectangleGradient(PixelArrayRGB bitmap, PointI p1, int w, int h,
            Color c1, Color c2)
        {
            // c1, c2 - могут быть произвольными
            // в числе прочих
            // возможен вариант c1.R > c2.R
            //                  c1.G < c2.G
            //                  c1.B == c2.B

            // start_x - начальное значение x (откуда начинаем рисовать) [x0]
            int start_x = p1.x;
            // end_x - конечное значение x (где заканчиваем рисовать) [x1]
            int end_x = p1.x + w;

            // w = x1 - x0 = dx

            // необходимо ввести переменную, которая будет равна 0 при start_x
            // и 1 при end_x, и >0 && <1 при промежуточных значениях
            // double normalized_distance_from_start = ???
            // int current_x = start_x+50;
            // (current_x - start_x) <=> x1 - x0 <=> dx
            //double normdist = (current_x - start_x) / w;
            //                 end_x    - start_x
            //                 p1.x + w - p1.x
            // w/w = 1

            for (int y = p1.y; y <= p1.y + h; y++)
            {
                for (int x = start_x; x <= end_x; x++)
                {
                    double normdist = (x - start_x) / (double)w;
                    Color c = BlendColors(c1, c2, normdist);
                    bitmap.SetPixel(x, y, c);
                }
            }
        }

        public static double BlendValues(double v0, double v1, double normdist)
        {
            double dv = v1 - v0;
            double resulting_v = v0 + dv * normdist;
            return resulting_v;
        }

        public static Color BlendColors(Color c1, Color c2, double normdist)
        {
            // r - значение красного канала

            // цв(нач) = r0 = c1.R
            /*double r0 = c1.R;
            // цв(кон) = r1 = c2.R
            double r1 = c2.R;

            // Найти изменение по r
            double dr = r1 - r0;
            // если r1 = 50, r0 = 26, то dr = 24 => dr > 0
            // если r1 = 50, r0 = 50, то dr = 0
            // если r1 = 26, r0 = 50, то dr = -24 => dr < 0

            double resulting_r = r0 + dr * normdist;*/
            double r = BlendValues(c1.R, c2.R, normdist);
            double g = BlendValues(c1.G, c2.G, normdist);
            double b = BlendValues(c1.B, c2.B, normdist);
            double a = BlendValues(c1.A, c2.A, normdist);
            //double a = 255;

            Color c = Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
            return c;
        }


        // Поддерживает другие значения цвета
        public static void DrawFilledRectangleGradient_v2(PixelArrayRGB bitmap, PointI p1, int w, int h,
            Color c1, Color c2)
        {
            // Задача: Сделать плавный переход цвета от c1 к c2 при движении по горизонтали
            // (т.е. по мере увеличения x). Рассмотрение можно начать с простого варианта, 
            // когда c1 - чёрный, а c2 - красный (или зелёный, или синий, т.е. изменяется
            // только один цветовой канал)
            byte maxR = 200;

            Color c = Colors.Red;
            c.R = maxR; // красный цветовой канал
            c.G = 0; // зелёный цветовой канал
            c.B = 0; // синий цветовой канал
            c.A = 255; // (не)прозрачность, в данном случае не рассматриваем

            // Показывает, сколько пикселей приходится на сегмент с одним цветом
            //            dy / dx
            double ratio = w / (double)maxR;
            int px_ctr=0;

            // если ширина меньше (в числовом отношении), чем изменение цвета
            // тогда на каждый пиксель значение цвета меняется больше, чем на 1
            //  dx_is_less_than_dc = true

            // если ширина больше (или равна), чем изменение цвета
            // тогда на одно изменение цвета (на 1) приходится больше одного пикселя
            //  dx_is_less_than_dc = false
            bool dx_is_less_than_dc = (w < maxR);

            // приращение цвета (для dx_is_less_than_dc == true)
            // соотношение: какое изменение цвета приходится НА 1 пиксель
            int dc = maxR / w;

            for (int y = p1.y; y <= p1.y + h; y++)
            {
                //c = Colors.Red;
                c.R = maxR;
                for (int x = p1.x; x <= p1.x + w; x++)
                {
                    bitmap.SetPixel(x, y, c);
                    if(dx_is_less_than_dc)
                    {
                        c.R -= (byte)dc;
                    }
                    else
                    {
                        px_ctr++;
                        if (px_ctr >= ratio)
                        {
                            c.R--;
                            px_ctr = 0;
                        }
                    }
                }
            }
        }


        // Работает только для начального значения канала 255 и конечного 0
        public static void DrawFilledRectangleGradient_v1(PixelArrayRGB bitmap, PointI p1, int w, int h,
            Color c1, Color c2)
        {
            // Задача: Сделать плавный переход цвета от c1 к c2 при движении по горизонтали
            // (т.е. по мере увеличения x). Рассмотрение можно начать с простого варианта, 
            // когда c1 - чёрный, а c2 - красный (или зелёный, или синий, т.е. изменяется
            // только один цветовой канал)
            Color c = Colors.Red;
            c.R = 255; // красный цветовой канал
            c.G = 0; // зелёный цветовой канал
            c.B = 0; // синий цветовой канал
            c.A = 255; // (не)прозрачность, в данном случае не рассматриваем

            // Показывает, сколько пикселей приходится на сегмент с одним цветом
            //            dy / dx
            double ratio = w / 255.0;
            int px_ctr = 0;

            // если ширина меньше (в числовом отношении), чем изменение цвета
            // тогда на каждый пиксель значение цвета меняется больше, чем на 1
            //  dx_is_less_than_dc = true

            // если ширина больше (или равна), чем изменение цвета
            // тогда на одно изменение цвета (на 1) приходится больше одного пикселя
            //  dx_is_less_than_dc = false
            bool dx_is_less_than_dc = (w < 255);

            // приращение цвета (для dx_is_less_than_dc == true)
            // соотношение: какое изменение цвета приходится НА 1 пиксель
            int dc = 255 / w;

            for (int y = p1.y; y <= p1.y + h; y++)
            {
                c = Colors.Red;
                for (int x = p1.x; x <= p1.x + w; x++)
                {
                    bitmap.SetPixel(x, y, c);
                    if (dx_is_less_than_dc)
                    {
                        c.R -= (byte)dc;
                    }
                    else
                    {
                        px_ctr++;
                        if (px_ctr >= ratio)
                        {
                            c.R--;
                            px_ctr = 0;
                        }
                    }
                }
            }
        }

        public static void DrawFilledCircle(PixelArrayRGB bitmap, Color c,
            PointI center, double radius)
        {
            int start_y = -(int)radius;
            int start_x = -(int)radius;

            int end_y = (int)radius;
            int end_x = (int)radius;

            for (int y = start_y; y<= end_y; y++)
            {
                for(int x = start_x; x <=end_x; x++)
                {
                    double xy = y * y + x * x;
                    if(xy <= radius*radius)
                    {
                        bitmap.SetPixel(x+center.x, y+center.y, c);
                    }
                }
            }
        }

        public static void DrawCircle(PixelArrayRGB bitmap, Color c,
            PointI center, double radius)
        {
            int start_y = -(int)radius;
            int start_x = -(int)radius;

            int end_y = (int)radius;
            int end_x = (int)radius;

            for (int y = start_y; y <= end_y; y++)
            {
                for (int x = start_x; x <= end_x; x++)
                {
                    double xy = Math.Sqrt(y * y + x * x);
                    if ((int)xy == (int)(radius))
                    {
                        bitmap.SetPixel(x + center.x, y + center.y, c);
                    }
                }
            }
        }

        public static void DrawEllipse(PixelArrayRGB bitmap, Color c,
            PointI center, double w, double h)
        {
            int start_y = -(int)w;
            int start_x = -(int)h;

            int end_y = (int)h;
            int end_x = (int)w;

            double hw = w / 2;
            double hh = h / 2;

            for (int y = start_y; y <= end_y; y++)
            {
                for (int x = start_x; x <= end_x; x++)
                {
                    double xy = Math.Sqrt((y * y) / (hh * hh) + (x * x) / (hw * hw));
                    if (Math.Round(xy,2) == 1)
                    {
                        bitmap.SetPixel(x + center.x, y + center.y, c);
                    }
                }
            }
        }

        public static List<Shape> ReadFromFile(string filename)
        {
            List<Shape> result = new List<Shape>();

            string[] lines = File.ReadAllLines(filename);

            foreach(string line in lines)
            {
                try
                {
                    string[] fields = line.Split(',');

                    // Тип     ,p1.x,p1.y,p2.x,..,pos.x,pos.y,color.R,...
                    // Triangle,30,30,30,60,60,60,300,350,255,0,0
                    if(fields[0] == "Triangle")
                    {
                        result.Add(ReadTriangleFromFields(fields));
                    }
                    // Дальше else if(fields[0] == "Rectangle") ... и т.д.
                }
                catch (Exception e) { }
            }

            return result;
        }

        private static Triangle ReadTriangleFromFields(string[] fields)
        {
            PointI p1 = new PointI(int.Parse(fields[1]), int.Parse(fields[2]));
            PointI p2 = new PointI(int.Parse(fields[3]), int.Parse(fields[4]));
            PointI p3 = new PointI(int.Parse(fields[5]), int.Parse(fields[6]));
            PointI position = new PointI(int.Parse(fields[7]), int.Parse(fields[8]));
            Color c = new Color();
            c.R = byte.Parse(fields[9]);
            c.G = byte.Parse(fields[10]);
            c.B = byte.Parse(fields[11]);

            Triangle tri = new Triangle(p1, p2, p3);
            tri.Position = position;
            tri.Color = c;

            return tri;
        }

    }

    // 27.10.20, задания (ориентировочно до 11:00)
    // Сопроводительные материалы лежат на Dropbox: ProgII\urok10_271020
    // 1. Сделать возможность обозначения позиции многоугольника при отрисовке 
    //    (через свойство Position)
    //    Нужно преобразовывать не сразу координаты в списке points, а координаты линий при
    //    отрисовке многоугольника
    //    В GenerateRegularPolygon() offset ставим {0,0}
    // 2. Создать свойство "Цвет"(Color) в классе Polygon, убрать цвет из параметров Draw()
    //    брать цвет отрисовки из свойства
    // 3. Сделать базовый класс (интерфейс) Drawable с методом Draw(PixelArrayRGB)
    //    сделать так, чтобы Polygon наследовал от него, и заменял поведение этой
    //    функции (полиморфизм, см. материал на Dropbox).
    // 4. Сделать класс Circle со свойствами Position, Radius, Color, наследующий
    //    от класса Drawable, и отрисовывающий окружность (или круг) через Draw
    // 5. Сделать класс Rectangle по схожей схеме, основные свойства Width, Height
    // 6. В классе MainWindow сделать в качестве члена список объектов типа Drawable,
    //    при инициализации наполнить его несколькими объектами типа Polygon, Rectangle, 
    //    Circle (в конструкторе, либо при событии Load, можно выделить в 
    //    отдельный метод, вызываемый из конструктора/обработчика события Load)
    //    Отрисовывать все объекты из этого списка в MainWindow.Draw()
    // 7*. Сделать в Drawing статический метод для загрузки объектов из файла, вида:
    //     public static List<Drawable> ReadFromFile(string filename)
    //     (подсказка: вспоминаем, как мы читали книжки из файла в WpfBooks)
    //     Создать файл со списком фигур (по 1 на строчку)
    //     Для прямоугольников сделать формат строчки:
    //     Rectangle(слово,Width,Height,Position.x,Position.y
    //     Пример: "Rectangle,100,150,250,300"
    //     Аналогично сделать для окружностей
    //     Многоугольники пока делать не обязательно (3,{10,50},{100,0},...)
    //     Можно сделать вариант под RegularPolygon

    

    // y = k * x + b    - прямая функция
    // x = (y - b) / k  - обратная функция
    public class LinearFunction
    {
        public double k = 0.0;
        public double b = 0.0;

        public double Y(double x) => k * x + b;
        public double X(double y) => (y - b) / k;
    }
}
