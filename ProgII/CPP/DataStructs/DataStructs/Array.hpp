#pragma once
#pragma once
#include <stdexcept>
#include <initializer_list>

// TODO (к 14.10.21):
// * Реализовать метод Clear() для удаления всех элементов массива
// * Реализовать метод Resize() для изменения размера массива (заполняется значениями по умолчанию)
// * Оптимизировать оператор присвоения из списка инициализации с их помощью

// Класс-шаблон "массив"
template<typename T> class Array
{
private:
    T* array_ptr = nullptr;
    size_t size = 0;

public:
    // Конструктор по умолчанию
    Array() {}

    // Конструктор с параметрами
    Array(size_t sz, const T& default_value)
    {
        size = sz;
        array_ptr = new T[sz];

        for (size_t s = 0; s < sz; s++)
        {
            array_ptr[s] = default_value;
        }
    }

    // Конструктор копии
    Array(const Array& other)
    {
        operator=(other);
    }

    Array(const std::initializer_list<T> other)
    {
        operator=(other);
    }

    // Оператор присвоения списка инициализации = { 1, 2, 3 }
    Array& operator=(const std::initializer_list<T> lst)
    {
        // Если массив не пустой, удаляем старое содержимое
        if (array_ptr != nullptr) Clear();

        // Выделяем массив требуемого размера (по размеру списка инициализации)
        Resize(lst.size());

        // Проходим по списку инициализации (не поддерживает прямую индексацию,
        // поэтому придётся действовать через range-based for, он же "foreach")
        // Индекс в нашем массиве будем отслеживать отдельной переменной (i)
        size_t i = 0;
        for (T el : lst)
        {
            array_ptr[i++] = el;
        }

        return *this;
    }

    // Оператор присвоения копии
    Array& operator=(const Array& other)
    {
        // Проверяем, что мы не присваиваем объект самому себе
        if (&other == this) return *this;

        // Если во внутреннем массиве уже что-то было, удаляем его
        if (array_ptr != nullptr) delete[] array_ptr;

        // В остальном действуем так же, как в конструкторе копии
        size = other.size;

        // Создаём новый внутренний массив, который
        // будет находится под управлением создаваемого объекта
        array_ptr = new T[size];

        // Переносим элементы из внутреннего массива другого объекта 
        // (с которого делаем копию) во внутренний массив создаваемого
        for (size_t i = 0; i < size; i++)
        {
            array_ptr[i] = other.array_ptr[i];
        }

        // Возвращаем ссылку на текущий объект
        return *this;
    }

    // Переопределение поведения оператора
    // operator overloading
    T& operator[](size_t index)
    {
        return array_ptr[index];
    }

    // Деструктор
    ~Array()
    {
        // Если память под массив выделялась - высвобождаем её
        // оператор delete[] используем для высвобождения массивов
        // оператор delete для высвобождения единичных переменных
        if (array_ptr != nullptr) { delete[] array_ptr; }
    }

    // Задача 1: Реализовать метод Fill(int value)
    // Который будет заполнять массив указанными значениями
    void Fill(const T& value)
    {
        for (size_t s = 0; s < size; s++)
        {
            array_ptr[s] = value;
        }
    }

    void Clear()
    {
        if(array_ptr != nullptr) delete[] array_ptr;
        size = 0;
        array_ptr = nullptr;
    }

    void Resize(size_t new_size)
    {
        if (new_size == 0) Clear();
        // Шаг 1: Выделяем пространство под новый массив
        // и заполняем значениями по умолчанию для данного типа
        T* new_ptr = new T[new_size]();

        if (array_ptr == nullptr)
        {
            array_ptr = new_ptr;
            size = new_size;
        }
        else
        {
            // Шаг 2: Копируем данные из старого массива в новый
            // Определяем наибольший из двух размеров (старый или новый)
            size_t smaller_size = (new_size > size) ? size : new_size;

            for (size_t i = 0; i < smaller_size; i++)
            {
                new_ptr[i] = array_ptr[i];
            }

            // Шаг 3: Удалить старый массив и обновить состояние объекта
            delete[] array_ptr;
            array_ptr = new_ptr;
            size = new_size;
        }
    }

    size_t GetSize() { return size; }

    // Задача 2: Реализовать добавление элемента в конец массива
    //           метод Add(int value)
    //           Проверить работоспособность для массива с заданным 
    //           в конструкторе размером и для массива, созданного с
    //           помощью конструктора по умолчанию
    void Add(const T& value)
    {
        // 1. Выделяем память под новый массив увеличенного размера
        // (с запасом в 1 элемент)
        T* newptr = new T[size + 1];

        // 2. Переносим значения из старого массива
        for (size_t i = 0; i < size; i++)
        {
            newptr[i] = array_ptr[i];
        }
        // 3. Помещаем добавляемое значение в последнюю ячейку
        // нового массива
        newptr[size] = value;

        // 4. Удалить предыдущий массив
        if (array_ptr != nullptr)
            delete[] array_ptr;

        // 5. Обновляем значения в объекте массива
        array_ptr = newptr;
        size = size + 1;
    }

    // Задача 3: Реализовать удаление последнего элемента
    void RemoveLast()
    {
        if (size == 0) return;
        else if (size == 1)
        {
            delete[] array_ptr;
            array_ptr = nullptr;
            size = 0;
        }
        else
        {
            T* new_ptr = new T[size - 1];
            for (size_t i = 0; i < size - 1; i++)
            {
                new_ptr[i] = array_ptr[i];
            }
            delete[] array_ptr;
            array_ptr = new_ptr;
            size = size - 1;
        }
    }

    // Задача 4: Реализовать добавление элемента по произвольному
    // индексу (новый элемент должен помещаться по указанному индексу,
    // сдвигая ранее находившейся там элемент вперёд)
    void Insert(size_t index, const T& value)
    {
        // Если индекс вставки больше размера - кидаем исключение
        if (index > size)
        {
            throw std::out_of_range("Array insert index out of range");
        }

        T* new_ptr = new T[size + 1];

        // В новом массиве по индексу выставляем вставляемое значение
        new_ptr[index] = value;

        // Если в старом массиве что-то было, копируем
        if (size != 0)
        {
            // Часть, идущая до индекса, куда происходит вставка
            // индексы в старом и новом массиве совпадают
            for (size_t i = 0; i < index; i++)
            {
                new_ptr[i] = array_ptr[i];
            }
            // Часть, идущая начиная с индекса, куда происходит
            // вставка - индекс в новом массиве на 1 больше
            // (элементы смещаются на 1 позицию вперёд)
            for (size_t i = index; i < size - 1; i++)
            {
                new_ptr[i + 1] = array_ptr[i];
            }
        }

        delete[] array_ptr;
        array_ptr = new_ptr;
        size = size + 1;
    }

    // Задача 5: Реализовать удаление элемента по произвольной позиции
    void Remove(size_t index)
    {
        if (index >= size)
            throw std::out_of_range("Array index out of range");

        if (size == 1)
        {
            delete[] array_ptr;
            array_ptr = nullptr;
            size = 0;
        }
        else
        {
            T* new_ptr = new T[size - 1];

            // Часть, идущая до индекса, куда происходит вставка
            // индексы в старом и новом массиве совпадают
            for (size_t i = 0; i < index; i++)
            {
                new_ptr[i] = array_ptr[i];
            }

            // Часть, идущая после индекса, откуда происходит
            // удаление - индекс в новом массиве на 1 меньше
            // (элементы смещаются на 1 позицию назад)
            for (size_t i = index + 1; i < size; i++)
            {
                new_ptr[i - 1] = array_ptr[i];
            }

            delete[] array_ptr;
            array_ptr = new_ptr;
            size = size - 1;
        }
    }

    // Задача 6: Реализовать поиск элемента по значению (возвращать индекс)
    static const size_t ELEMENT_NOT_FOUND = std::numeric_limits<size_t>::max();

    size_t FindIndex(const T& value)
    {
        size_t index = ELEMENT_NOT_FOUND;

        for (size_t i = 0; i < size; i++)
        {
            if (array_ptr[i] == value)
            {
                index = i;
                break;
            }
        }

        return index;
    }

    T* begin() { return array_ptr; }
    T* end() { return array_ptr + size; }

    // Ходовые конвенции записи названий методов
    // camelCase() [принята в Java]
    // PascalCase() [принята в C#]
    // snake_case() [принята в C++]
};