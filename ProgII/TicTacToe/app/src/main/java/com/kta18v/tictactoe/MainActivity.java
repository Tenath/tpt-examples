// (c) Andrei Veeremaa @ TPT, 2019
package com.kta18v.tictactoe;

import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Point;
import android.graphics.Rect;
import android.os.Bundle;
import android.view.Display;
import android.view.MotionEvent;
import android.widget.ImageView;

public class MainActivity extends Activity {

    // Перечисление для обозначения игроков
    public enum Player
    {
        None,
        X,
        O
    }

    // Возможные состояния игры
    public enum GameState
    {
        InProgress, // игра в процессе
        Draw, // ничья
        XWins, // крестики победили
        OWins // нолики победили
    }

    // [X] [ ] [ ]
    // [ ] [O] [ ]
    // [ ] [ ] [ ]
    // Player.None используется для указания пустоты поля
    Player[][] board = new Player[3][3];

    // Переменная "текущее состояние игры"
    GameState State = GameState.InProgress;
    // Текущий игрок
    Player ActivePlayer = Player.X;
    // # хода, отображается сверху, используется для определения ничьи
    int Turn = 1;

    // Виджет для отображения картинки
    // в нашем случае занимает весь экран
    ImageView imgView;
    // "Битовая карта" - редактируемая картинка
    Bitmap bmp;
    // "Холст", содержит функции для заливки, отрисовки линий, текста и т.д.
    Canvas canvas;
    // "Краска", то же, что Brush("Кисть") в C#
    Paint xpaint; // для отрисовки крестика
    Paint opaint; // для отрисовки нолика
    Paint rpaint; // для отрисовки ячеек игрового поля
    Paint tpaint; // для отрисовки текста

    // Центральная точка экрана, периодически используется в вычислениях
    Point center;

    // Размеры экрана
    int ScreenW, ScreenH;
    // Размер квадрата, в котором размещается игровое поле
    int DrawAreaSize;
    // Размер ячейки игрового поля
    int SqSize;

    // Цвета игроков
    int colorX = Color.argb(255,0,0,200);
    int colorO = Color.argb(255,200,0,0);
    int colorD = Color.argb(255,0,200,0);

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        // Вся инициализация вынесена в setup()
        setup();
        // После инициализации начинаем новую игру
        newgame();
    }

    // Обработчик событий типа "прикосновение"
    @Override
    public boolean onTouchEvent(MotionEvent motionEvent)
    {
        // Если вид события - любой кроме поднятия пальца (отпускания экрана)
        // значит нам его обрабатывать не нужно
        if(motionEvent.getActionMasked() != MotionEvent.ACTION_UP) return false;

        // Если игра находится в завершённом состоянии - любое нажатие приводит к рестарту
        if(State != GameState.InProgress)
        {
            newgame();
            return true;
        }

        // Защита от мульти-тача, чтобы не возиться с определением нескольких точек нажатия
        if(motionEvent.getPointerCount()>1) return false;

        // Координаты нажатия
        float x = motionEvent.getX();
        float y = motionEvent.getY();

        // Плохой метод определения ячейки, в которую нажали, попробуйте сделать лучше
        // Для каждой ячейки
        for(int i=1; i<=3; i++)
        {
            for(int j=1; j<=3; j++)
            {
                // Получаем координаты данной ячейки
                Rect r = GetCellRect(i,j);
                // Если координата нажатия x >= левого края ячейки И <= правого края ячейки
                // аналогично по y
                if(x>=r.left && x<=r.right && y>=r.top && y<=r.bottom)
                {
                    // Обрабатываем ход, координаты ячейки нашли
                    handle_move(i,j);
                    // Квик хак чтобы быстро выйти из двух циклов
                    i=4; j=4;
                }
            }
        }

        // После проведения хода проверяем на победу/ничью
        check_victory();
        // Перерисовывем экран
        draw();

        // Сообщаем О/С, что обработали событие
        return true;
    }

    // Всю первичную инициализацию производим здесь
    void setup()
    {
        /* Определение размеров экрана */
        Display display = getWindowManager().getDefaultDisplay();
        Point size = new Point();
        display.getSize(size);

        ScreenW = size.x;
        ScreenH = size.y;

        center = new Point();
        center.x = ScreenW/2;
        center.y = ScreenH/2;

        // Обозначаем размер квадрата, в котором рисуется игровое поле
        // (90% размера меньшей координаты)
        DrawAreaSize = (int)(ScreenW > ScreenH ? ScreenH*0.9 : ScreenW*0.9);
        // Каждая ячейка составляет 1/3 этого квадрата
        SqSize = DrawAreaSize/3;

        // Инициализируем битмап с размерами экрана и 32-битной цветностью типа ARGB
        bmp = Bitmap.createBitmap(ScreenW, ScreenH, Bitmap.Config.ARGB_8888);
        canvas = new Canvas(bmp);
        imgView = new ImageView(this);

        xpaint = new Paint();
        xpaint.setStrokeWidth(10);
        xpaint.setColor(colorX);

        opaint = new Paint();
        opaint.setColor(colorO);
        opaint.setStrokeWidth(10);
        opaint.setStyle(Paint.Style.STROKE);

        rpaint = new Paint();
        rpaint.setColor(Color.argb(255,200,200,200));

        tpaint = new Paint();
        tpaint.setColor(Color.argb(255,255,255,255));
        tpaint.setTextSize(80);
        tpaint.setTextAlign(Paint.Align.CENTER);

        imgView.setImageBitmap(bmp);
        // Содержимое этого Activity - только виджет ImageView, не используем Layout
        setContentView(imgView);
    }

    // Обработка хода, параметры - координаты ячейки
    void handle_move(int x, int y)
    {
        // Если ячейка не занята
        if(board[x-1][y-1] == Player.None)
        {
            // Отмечаем её за текущим игроком
            board[x-1][y-1] = ActivePlayer;

            // Делаем переход хода
            if(ActivePlayer == Player.X) ActivePlayer = Player.O;
            else ActivePlayer = Player.X;
            // Инкрементируем счётчик ходов
            Turn++;
        }
    }

    // Проверка на победу или ничью
    void check_victory()
    {
        // Строки
        for(int x = 0; x<3; x++)
        {
            if(board[x][0] != Player.None && board[x][0]==board[x][1] && board[x][0]==board[x][2])
            {
                declare_victory(board[x][0]);
                return;
            }
        }

        // Столбцы
        for(int y = 0; y<3; y++)
        {
            if(board[0][y] != Player.None && board[0][y]==board[1][y] && board[0][y]==board[2][y])
            {
                declare_victory(board[0][y]);
                return;
            }
        }

        // Диагонали
        if(board[0][0]!=Player.None && board[0][0]==board[1][1] && board[0][0]==board[2][2])
        {
            declare_victory(board[0][0]);
			return;
        }
        else if(board[0][2]!=Player.None && board[0][2]==board[1][1] && board[0][2]==board[2][0])
        {
            declare_victory(board[0][2]);
			return;
        }

        // Если достигли 10 хода (т.е. заполнены 9 ячеек) - ничья
        if(Turn>=10) declare_victory(Player.None);
    }

    // Смена состояния игры на победу/ничью
    void declare_victory(Player p)
    {
        switch(p)
        {
            case X: State = GameState.XWins; break;
            case O: State = GameState.OWins; break;
            case None: State = GameState.Draw; break;
        }
    }

    // Обнуление игры
    void newgame()
    {
        // Выставляем во всех ячейках игрока None (не заняты ни одним из игроков)
        for(int i=0; i<3; i++)
            for(int j=0; j<3; j++)
                board[i][j] = Player.None;

        ActivePlayer = Player.X;
        State = GameState.InProgress;
        Turn = 1;

        // Это было для тестов по отрисовке крестиков/ноликов
        //board[1][0] = Player.X;
        //board[1][1] = Player.O;

        // После начала игры
        draw();
    }

    void draw()
    {
        // Производим диспетчеризацию в функции отрисовки в зависимости от состояния
        switch (State)
        {
            case InProgress: DrawGame(); break;
            case XWins: DrawVictory(Player.X); break;
            case OWins: DrawVictory(Player.O); break;
            case Draw: DrawVictory(Player.None); break;
        }

        // Сообщаем О/С, что содержимое виджета измениолось, нужно его перерисовать на экране
        imgView.postInvalidate();
    }

    // Отрисовка игрового экрана
    void DrawGame()
    {
        // Забыл этот момент в видео
        // Заливаем задник в цвет игрока
        canvas.drawColor(ActivePlayer == Player.X ? colorX : colorO);

        canvas.drawText(("Player: "+ActivePlayer.toString()),200,100,tpaint);
        canvas.drawText(("Turn: "+Turn), ScreenW-150, 100, tpaint);

        // Рисуем ячейки (вместе с их содержимым)
        for(int i=1; i<=3; i++)
            for(int j=1; j<=3; j++)
                drawRectangle(i,j);
    }

    // Отрисовка экрана победы/ничьи
    void DrawVictory(Player p)
    {
        String victory_desc = "";
        int fillcolor=0;

        // В зависимости от победителя выставляем цвет и текст
        switch(p)
        {
            case None:
                fillcolor = colorD;
                victory_desc = "Draw!";
                break;
            case X:
                fillcolor = colorX;
                victory_desc = "X wins!";
                break;
            case O:
                fillcolor = colorO;
                victory_desc = "O wins!";
                break;
        }

        canvas.drawColor(fillcolor);
        canvas.drawText(victory_desc, center.x, center.y, tpaint);
        imgView.postInvalidate();
    }

    // Определение координат
    Rect GetCellRect(int x, int y)
    {
        // центр экрана + размер_ячейки*(x-2) - половина размера ячейки + 10*(x-2)
        // x-2 для x=1 даёт -1, т.е. сдвиг влево и вверх относительно центра
        // x-2 для x=3 даёт +1, т.е. сдвиг вправо и вниз относительно центра
        int topLeftX = center.x + SqSize*(x-2) - SqSize/2 + 10*(x-2);
        // то же для y
        int topLeftY = center.y + SqSize*(y-2) - SqSize/2 + 10*(y-2);

        // Возвращаем квадрат с координатами
        // Нижняя или права координата = Верхняя или левая + Размер ячейки
        return new Rect(topLeftX,topLeftY, topLeftX+SqSize, topLeftY+SqSize);
    }

    // Отрисовка квадрата с содержимым, координаты игрового поля, а не экрана
    void drawRectangle(int x, int y)
    {
        /*canvas.drawRect(center.x-SqSize/2,center.y-SqSize/2,
            center.x+SqSize/2, center.y+SqSize/2, rpaint);*/

        // Получаем экранные координаты ячейки
        Rect r = GetCellRect(x,y);

        // Рисуем сам квадрат
        canvas.drawRect(r, rpaint);

        // Проверяем, что содержится в ячейке (индексы на 1 меньше)
        switch (board[x-1][y-1])
        {
            // Если пусто, то кроме квадрата ничего рисовать не нужно
            case None: break;
            // Отрисовка крестика
            case X:
                canvas.drawLine(r.left+10, r.top+10, r.right-10, r.bottom-10, xpaint);
                canvas.drawLine(r.left+10, r.bottom-10, r.right-10, r.top+10, xpaint);
                break;
            // Отрисовка нолика
            case O:
                canvas.drawCircle(r.left+SqSize/2.0f, r.top+SqSize/2.0f, SqSize*0.45f, opaint);
                break;
        }
    }
}
