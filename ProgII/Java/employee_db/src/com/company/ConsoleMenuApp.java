package com.company;

import java.util.ArrayList;
import java.util.Scanner;
import java.util.function.Function;

// NB! После урока:
// * Заменил Function<ConsoleMenuApp,Void> на Runnable
// * Функции при регистрации передаю через this::название_метода

// Класс "запись в меню приложения"
// Symbol. Caption => Operation()
// 1. Insert Entry => Insert()
class MenuItem
{
    // Символ, обозначающий операцию в меню
    // Операция выбирается, если пользователь ввёл этот символ
    public char Symbol;
    // Название операции
    public String Caption;
    // Объект-функция - сюда в виде переменной помещается ссылка на метод класса
    // Тип Runnable - обёртка вокруг функции, которая не принимает аргументов и не возвращает значения
    public Runnable Operation;

    // Конструктор
    public MenuItem(char symbol, String caption, Runnable operation)
    {
        Symbol = symbol;
        Caption = caption;
        Operation = operation;
    }
}

// Базовый класс "Консольное приложение с меню"
public class ConsoleMenuApp {
    // Флажок "продолжить исполнение", изначально поднят (основной цикл будет крутиться до его опускания)
    private boolean Running = true;
    // Список записей в меню
    private ArrayList<MenuItem> MenuItems = new ArrayList<>();
    // Символ, который будет присвоен следующей добавляемой операции
    private char NextSymbol = '1';

    // Чтобы не инстанцировать тяжёлый библиотечный объект каждый раз
    private Scanner Scan = new Scanner(System.in);

    // Регистрация операции, принимает описание и объект-функцию, который будет вызываться при выборе операции
    protected void RegisterMenuItem(String caption, Runnable func) throws Exception
    {
        // Создаём запись с указанными параметрами (название, функция), символ берём из соотв. переменной в данном классе
        MenuItems.add(new MenuItem(NextSymbol, caption, func));

        // Инкрементируем символ для следующей операции
        // Идём от '1' до '9', как только дошли до '9', дальше перескакиваем на 'a'
        if(NextSymbol == '9') NextSymbol = 'a';
        // Если перешли дальше z - кидаем исключение, о том, что место в меню закончилось
        else if(NextSymbol > 'z')
            throw new Exception("Maximum amount of menu operations reached");
        // Если перескок не требуется, инкрементируем символ
        else ++NextSymbol;
    }

    // Операция "выход из приложения", будет регистрироваться последней для любого класса, наследующего от ConsoleMenuApp
    protected void Exit()
    {
        System.out.println("Exiting...");
        // Опускаем флаг "продолжить исполнение"
        Running = false;
    }

    // "Настройка" приложения
    public void Setup() throws Exception
    {
        // Добавляем операции, AppSetup берётся из производного класса, если он там есть и помечен как @Override
        AppSetup();

        // После добавления операций конкретного приложения, в любом случае обавляем операцию "Выход"
        RegisterMenuItem("Exit", this::Exit);
    }

    // Делаем базовую реализацию для AppSetup (здесь пустая, заменяется в производном классе)
    public void AppSetup() throws Exception
    {

    }

    // Основной цикл приложения, запускается в Main() после Setup()
    public void Run()
    {
        // Пока поднят флаг "продолжить исполнение"
        while (Running)
        {
            // Отображаем меню
            DisplayMenu();
            // Обрабатываем выбор операции пользователем
            HandleOperation();
        }
    }

    // Отображение меню
    public void DisplayMenu()
    {
        System.out.println("Menu");
        System.out.println("====");

        // Проходим по каждой записи в меню
        for (MenuItem item : MenuItems)
        {
            // Отображаем символ. Название операции
            System.out.printf("%c. %s\n", item.Symbol, item.Caption);
        }
    }

    public void HandleOperation()
    {
        // Получаем от пользователя операцию
        System.out.print("\nChoose an operation: ");
        String op = Scan.next();

        // Если пользователь ничего не ввёл - тихо выходим из функции
        // (лучше также выводить ошибку)
        if(op.length() == 0) return;

        // Берём первый введённый пользователем символ - его и интерпретируем, как выбор операции
        char sym = op.charAt(0);

        // Искомая запись в меню, изначально null, чтобы обозначить, что мы её не нашли
        MenuItem found_item = null;

        // Проходим по каждой записи в меню
        for (MenuItem item : MenuItems)
        {
            // Если у текущей записи символ совпадает с искомым, значит мы нашли нужный объект
            if(item.Symbol == sym)
            {
                // Помещаем найденный объект в found_item, выходим из цикла
                found_item = item;
                break;
            }
        }

        // Если запись в меню найдена - запускаем связанную с ней операцию (функцию)
        if(found_item != null) found_item.Operation.run();
        // В ином случае пишем "операция не найдена"
        else System.out.println("Unknown operation");

        // Пустая строчка, ятобы в любом случае был интервал в 1 строку между операциями
        System.out.println();
    }
}
