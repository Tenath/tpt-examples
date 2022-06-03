using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using MvcBooks.Models;

namespace MvcBooks.Controllers
{
    public class StudentController : Controller
    {
        List<Student> students = new List<Student>()
        {
            new Student() { FirstName="Vasya", LastName="Pupkin", Class="5B" },
            new Student() { FirstName="Ivan", LastName="Petrov", Class="7A" },
            new Student() { FirstName="Masha", LastName="Pupkina", Class="5B" }
        };

        /*public IActionResult Index()
        {
            return View();
        }*/

        // JSON
        public JsonResult Index()
        {
            /*JsonResult result = new JsonResult(
                new
                {
                    FirstName = "Vasya",
                    LastName = "Pupkin",
                    Class = "5B"
                });*/
            return List();
        }
        // Задание 3: Обозначить на уровне класса статический список с тремя студентами,
        //            отобразить его через JSON.

        // Задание 4: По аналогичной схеме создать контроллер CoursesController для предметов
        //            (учебных), вывести три предмета

        // (Подсказка) Сделать классы Student, Courses (можно сразу в папке Models)
        // сделать список объектов этих класса в StudentController/CourseController
        // далее либо определить его содержимое сразу, либо добавить где-то в методе.

        // В 10:55 продолжаем

        // http://localhost:<порт>/Student/Get?index=3
        public JsonResult Get(int index)
        {
            if (index < 0 || index >= students.Count) return new JsonResult(null);
            else return new JsonResult(students[index]);
        }

        public JsonResult List()
        {
            JsonResult result = new JsonResult(students);

            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            result.SerializerSettings = options;

            return result;
        }

        // Задание 5: Сделать контроллер CalcController с методами Add,Sub,Mul,Div
        // (принимающими аргументы value1 и value2), и отображающими результат
        // соответствующих арифметических операций

        public ViewResult Table()
        {
            ViewData["students"] = students;
            return View();
        }
    }
}
