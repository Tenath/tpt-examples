// (c) Andrei Veeremaa @ TPT, 2019
package com.kta18v.unitconverter;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.view.inputmethod.EditorInfo;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.TextView;

import org.w3c.dom.Text;

// Activity - суть то же самое, что форма, окно
public class MainActivity extends AppCompatActivity {
    // Перечисление доступных единиц измерения
    // Переменная этого типа может иметь только одно из указанных значений
    // (в Java также может быть null)
    public enum UnitType
    {
        m,
        cm,
        ft,
        in
    }

    // Флажок "Текущий входной тип данных"
    private UnitType InType = UnitType.m;
    // Флажок "Текущий выходной тип данных"
    private UnitType OutType = UnitType.m;


    // Входное значение, после интерпретации
    private double input = 0.0;
    // Промежуточное значение в метрах
    private double meters = 0.0;
    // Выходное значение (в интересующих единицах измерения)
    private double output = 0.0;

    // Обработчик события "Создание Activity"
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        // Вызов аналогичного метода у базового класса (AppCompatActivity)
        super.onCreate(savedInstanceState);
        // Выбираем, что показывать к окне (интерфейс на базе разметки/layout)
        setContentView(R.layout.activity_main);

        // Чёрная магия для отлова событий типа "завершение редактирования" у EditText
        ((EditText)findViewById(R.id.etInput)).setOnEditorActionListener(
                new EditText.OnEditorActionListener()
                {
                    @Override
                    public boolean onEditorAction(TextView v, int actionId, KeyEvent event)
                    {
                        if(actionId == EditorInfo.IME_ACTION_SEARCH ||
                           actionId == EditorInfo.IME_ACTION_DONE ||
                           event != null && event.getAction() == KeyEvent.ACTION_DOWN &&
                                            event.getKeyCode() == KeyEvent.KEYCODE_ENTER)
                        {
                            if(event == null || !event.isShiftPressed())
                            {
                                Update();
                            }
                        }
                        return false;
                    }
                }
        );
    }

    // Обработчик события для выбора радио-кнопок серии From
    public void onInSelect(View view)
    {
        // Обработка события вызывается радио-кнопкой - интерпретируем входной параметр
        // как радио-кнопку, чтобы получить доступ к её полям
        RadioButton rb = (RadioButton)view;

        // Выставляем флажок "Входной тип" в значение, проинтерпретированное как enum типа UnitType
        // Метод getText() возвращает CharSequence, поэтому его надо ещё превратить в String
        InType = UnitType.valueOf(rb.getText().toString());

        // Ищем TextView 'tvInputUnits' (лейбл входных единиц измерения)
        TextView tv = findViewById(R.id.tvInputUnits);
        // Выставляем текст лейбла, значение енума переводим в строчный вид
        tv.setText(InType.toString());

        // Вызываем общий метод для обновления внутреннего состояния системы
        Update();
    }

    // Обработчик события для выбора радио-кнопок серии To
    // Всё то же самое, что и в предыдущем, только чуть другие действующие лица
    public void onOutSelect(View view)
    {
        RadioButton rb = (RadioButton)view;
        OutType = UnitType.valueOf(rb.getText().toString());

        TextView tv = findViewById(R.id.tvOutputUnits);
        tv.setText(OutType.toString());

        Update();
    }

    // Обработчик события для кнопки
    public void onBtnClick(View view)
    {
        Update();
    }

    // метод для обновления внутреннего состояния системы
    // здесь происходят все конверсии и вывод в выходной EditText
    public void Update()
    {
        EditText etInput = findViewById(R.id.etInput);
        // Конверсия строки в double через Double.parseDouble()
        input = Double.parseDouble(etInput.getText().toString());

        // Смотрим, какие у нас входные единицы измерения
        // отсюда определяем формулу для перевода в метры
        // как промежуточный тип хранения
        switch(InType)
        {
            case m: meters = input; break;
            case cm: meters = input/100.0; break;
            case ft: meters = input*0.3048; break;
            case in: meters = input*0.0254; break;
        }

        // То же самое с выходным, переводим уже из метров
        switch(OutType)
        {
            case m: output = meters; break;
            case cm: output = meters*100.0; break;
            case ft: output = meters/0.3048; break;
            case in: output = meters/0.0254; break;
        }

        // Выводим полученный результат, как строку в выходной EditText
        EditText etOutput = findViewById(R.id.etOutput);
        etOutput.setText(Double.toString(output));
    }

}
