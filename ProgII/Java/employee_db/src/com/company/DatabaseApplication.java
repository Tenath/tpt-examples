package com.company;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Scanner;

// Класс "приложение", наследуется от ConsoleMenuApp, т.е. перенимает все его свойства
public class DatabaseApplication extends ConsoleMenuApp
{
    // Помимо членов из ConsoleMenuApp также имеет список работников
    ArrayList<Employee> employees;

    public DatabaseApplication()
    {
        employees = new ArrayList<>();
    }

    // Операция "добавление записи"
    public void Insert()
    {
        try
        {
            Scanner scanner = new Scanner(System.in);

            System.out.println("Enter employee data");
            System.out.println("===================");
            System.out.print("First name: ");
            String first_name = scanner.next();

            System.out.print("Last name: ");
            String last_name = scanner.next();

            System.out.print("Birthday: ");
            DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd.MM.yyyy");
            LocalDate birthday = LocalDate.parse(scanner.next(),formatter);

            System.out.print("Salary: ");
            double salary = scanner.nextDouble();

            Employee e = new Employee(first_name,last_name,birthday);
            e.setSalary(salary);

            employees.add(e);

            System.out.println();
        }
        catch (Exception e)
        {
            System.out.println("Error :"+e.getMessage());
        }
    }

    // Вывод записей
    public void PrintData()
    {
        System.out.println("Employee list");
        System.out.println("=============");
        if(employees.isEmpty())
            System.out.println("Employee list is empty");

        for (Employee e : employees)
        {
            System.out.println("ID: "+e.getID());
            System.out.println("First name: "+e.getFirstName());
            System.out.println("Last name: "+e.getLastName());
            System.out.println("Birthday: "+e.getBirthday());
            if(e.getSalary() > 0.0)
                System.out.println("Salary: "+e.getSalary());
            System.out.println("");
        }
    }

    // заменяем поведение AppSetup() из ConsoleMenuApp на специализацию в DatabaseApplication
    @Override
    public void AppSetup() throws Exception
    {
        // Регистрируем записи в меню
        RegisterMenuItem("Insert entry", this::Insert);
        RegisterMenuItem("View employee list", this::PrintData);
        RegisterMenuItem("Add test data", this::TestData);
    }

    // Раньше Run() был здесь, пока не вынесли общую часть в ConsoleMenuApp
    /*public void Run()
    {
        boolean running = true;

        Scanner scanner = new Scanner(System.in);

        // Цикл выполняется до тех пор, пока у running остаётся знаяение true
        while(running)
        {
            System.out.println("Menu");
            System.out.println("====");
            System.out.println("1. Insert entry");
            System.out.println("2. View employee list");
            System.out.println("3. Exit");

            System.out.print("\nChoose an operation: ");
            String op = scanner.next();

            switch(op)
            {
                case "1": Insert(); break;
                case "2": PrintData(); break;
                case "3":
                    System.out.println("Exiting...");
                    running = false;
                    break;
                default:
                    System.out.println("Unknown operation.");
                    break;
            }
        }
    }*/

    // Задание 1: Добавить операцию для удаления записи (по ID)
    // - сделать метод, где будем производить операцию
    // - получить у пользователя ID удаляемой записи
    // - найти в списке работников запись с таким ID
    //   - пройти по всему списку (цикл со счётчиком)
    //   - сравнивать ID текущей записей с искомым
    //   - если они равны - текущий индекс указывает на искомый объектв
    //   - (следовательно, удалять нужно по этому индексу, для этого его необходимо куда-то сохранить)
    // - ЕСЛИ запись найдена - удаляем её
    //
    // - добавить метод в меню
    void Remove()
    {
        // Получаете ID от пользователя

        for(int i=0; i<employees.size(); i++)
        {
            //employees.get(i).
        }
    }

    // Задание 2: Добавить операцию для изменения записи (ищем по ID)
    //            если поле пользователем не заполняется - не меняем его

    // Задание 3: Добавить операцию добавления тестовых данных (добавляет 3 статичных записи в список)
    void TestData()
    {
        Employee e = new Employee("Vasya", "Pupkin", LocalDate.of(1990, 1, 1));
        e.setSalary(1000);

        employees.add(e);

        e = new Employee("Masha", "Ivanova", LocalDate.of(1993, 4, 6));
        e.setSalary(900);

        employees.add(e);

        e = new Employee("Petya", "Ivanov", LocalDate.of(1992, 9, 30));
        e.setSalary(1000);

        employees.add(e);
    }
}
