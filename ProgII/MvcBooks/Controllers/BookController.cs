using Microsoft.AspNetCore.Mvc;
using MvcBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBooks.Controllers
{
    public class BookController : Controller
    {
        private IBooksStorage database;

        // BooksDatabase доступен через интерфейс IBooksStorage
        // Потребителю (BookController) ничего не нужно знать про имплементацию (BooksDatabase)
        // Схема называется Dependency Injection (внешние зависимости передаются потребителю
        // со стороны как интерфейс, ему ничего не нужно знать про их имплементацию)
        public BookController(IBooksStorage db)
        {
            database = db;
        }

        public IActionResult Index()
        {
            ViewData["books"] = database.GetBooks();
            //ViewData["books"] = Book.ReadBooks("books.csv");
            // Задание 6: Сделать View с таблицей
            // * Задание 6.5: Оформить табличку средствами Bootstrap
            return View();
        }

        // GET: /Book/Details?id=1
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            return View(database.GetBookById((int)id));
        }

        // GET: /Book/Edit?id=1 или /Book/Edit/1
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            Book book = database.GetBookById((int)id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Через Bind(...) формируем на основании формы объект типа Book
        public IActionResult Edit(int id, [Bind("ID,Title,Publisher,ISBN,Author,Year,Edition")] Book book)
        {
            // Если ID ресурса /Book/ID не совпадает с ID в теле запроса POST - возвращаем 404
            if (id != book.ID) return NotFound();

            // Если модель прошла валидацию (сформированный объект корректен)
            if(ModelState.IsValid)
            {
                try
                {
                    // Пробуем обновлять
                    database.UpdateBook(id, book);
                }
                catch (Exception e)
                {
                    // Если возникла ошибка - возвращаем ошибку 500 (Internal Server Error)
                    return new StatusCodeResult(500);
                }
            }

            // Возвращаем View с теми же данными, что и входные
            return View(book);
        }

        // Задания 22.12.20
        // Задание 1: Реализовать функцию удаления книги (с подтверждением, в отдельном View)
        // Задание 2: Реализовать функцию добавления книги
        // Д/З: Реализовать операции CRUD для авторов и издателей
    }
}
