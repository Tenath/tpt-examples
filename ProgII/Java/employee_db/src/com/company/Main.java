package com.company;

import java.time.LocalDate;

public class Main {
    // Ранний тест для объектов класса Employee, больше не нужен
    /*public static void CreateEmployees()
    {
        Employee e = new Employee("Vasya", "Pupkin", LocalDate.of(1990, 1, 1));

        e.setFirstName("");
    }*/

    public static void main(String[] args) {
        try
        {
            // Инстанцируем класс "приложение"
            DatabaseApplication app = new DatabaseApplication();
            // Производим его настройку (добавление записей в меню)
            app.Setup();
            // Запускаем основной цикл
            app.Run();
        }
        // Если возникает исключение, не отловленное ниже по уровню - выводим трассировку стэка вызовов, чтобы
        // понять, где в коде возникла ошибка
        catch (Exception e)
        {
            //System.out.println("Error: "+e.toString());
            e.printStackTrace();
        }
    }
}
