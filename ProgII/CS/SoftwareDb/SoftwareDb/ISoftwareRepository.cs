using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDb
{
    // Интерфейс = класс, в котором есть только публичные виртуальные методы без реализации
    // Repository - паттерн (шаблон) проектирования
    // описывающий абстрагированный доступ к хранилищу какого-то рода объектов
    public interface ISoftwareRepository
    {
        IEnumerable<Software> GetList();
        void Add(Software sw);
        void Remove(Software sw);
        void RemoveAt(int index);
        void SaveChanges();
    }
}
