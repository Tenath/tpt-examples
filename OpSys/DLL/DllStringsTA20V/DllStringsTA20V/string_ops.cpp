#include "pch.h"
#include "string_ops.hpp"
#include <cstring>
#include <cstdio>
#include <memory>
#include <vector>
#include <string>
#include <fstream>

// string_ops.cpp
#ifdef __cplusplus
extern "C"
{

	__declspec(dllexport) void __cdecl str_print_symbols(const char* str)
	{
		// '\0' - маркер конца строки, байтовое значение 0x00
		size_t length = strlen(str);

		for (size_t i = 0; i < length; i++)
		{
			// указателем можно пользоваться, как массивом 
			// (поддерживает обращение по индексу)
			printf("%d: %c\n", i, str[i]);
		}
	}

	// то же самое, что strcat()
	__declspec(dllexport) char* __cdecl str_concat(const char* s1, const char* s2)
	{
		size_t len_s1 = strlen(s1);
		size_t len_s2 = strlen(s2);

		size_t new_length = len_s1 + len_s2;

		// Выделяем память под новую строку, размер которой равен сумме
		// складываемых строк + 1 (под нулевой символ)
		char* new_str = (char*)malloc(sizeof(char) * (new_length + 1));
		if (new_str == NULL) return NULL;

		new_str[new_length] = '\0';

		// Копирование первой строки в формируемую строку
		// memcpy, strcpy
		for (size_t i = 0; i < len_s1; i++)
		{
			new_str[i] = s1[i];
		}

		// Копирование второй строки в формируемую строку (после первой)
		for (size_t i = 0; i < len_s2; i++)
		{
			new_str[len_s1 + i] = s2[i];
		}

		return new_str;
	}

	__declspec(dllexport) int __cdecl str_find_first(const char* str, char sym)
	{
		size_t length = strlen(str);

		// Как найти первое вхождение искомого символа в строку?
		// Проходим по всей строке от начала к концу
		for (size_t i = 0; i < length; i++)
		{
			// Если текущий символ - искомый, возвращаем его индекс
			if (str[i] == sym) return i;
		}

		// Если символ за время прохода по строке не найден, возвращаем -1
		return -1;
	}

	// Функция ищет первое вхождение символа начиная от указанного оффсета
	// т.е. начинаем искать не с начала строки, а с указанного индекса
	__declspec(dllexport) int __cdecl str_find_first_offset(const char* str, char sym, int offset)
	{
		int length = strlen(str);

		// Как найти первое вхождение искомого символа в строку?
		// Проходим по всей строке от начала к концу
		for (int i = offset; i < length; i++)
		{
			// Если текущий символ - искомый, возвращаем его индекс
			if (str[i] == sym) return i;
		}

		// Если символ за время прохода по строке не найден, возвращаем -1
		return -1;
	}

	// Задание: поиск последнего вхождения в строку реализовать самостоятельно
	// возвращаемый индекс считаем от начала строки
	__declspec(dllexport) int __cdecl str_find_last(const char* str, char sym)
	{
		int length = strlen(str);

		for (int i = length - 1; i > 0; i--)
		{
			if (str[i] == sym) return i;
		}

		return -1;
	}

}
#endif

// File.ReadAllLines()
__declspec(dllexport) std::vector<std::string> __cdecl ReadAllLines(const std::string& filename)
{
	std::vector<std::string> result;

	// Открыли файловый поток в режиме чтения
	std::ifstream fs(filename);

	std::string line;

	// Работая в цикле, считываем строчку из файлового потока
	// пока не дойдём до конца файла (либо не возникнет ошибка чтения)
	while (std::getline(fs, line))
	{
		// В получаемый вектор(массив) добавляем считанную строчку
		result.push_back(line);
	}

	return result;
}

// Функция рвзделения строки на части по разделителю
// "Hello world!", ' ' => { "Hello", "world!" }
// "Hello world!", ',' => { "Hello world!" }
// "1,Intel,Core i9-11900K,650 EUR", ',' => { "1", "Intel", "Core i9-11900K", "650 EUR" }
__declspec(dllexport) std::vector<std::string> __cdecl StringSplit(const std::string& str, char delimiter)
{
	std::vector<std::string> result;

	// Ищем первое вхождение разделителя в строку
	int delim_at = str_find_first(str.c_str(), delimiter);

	// Если разделитель в строке не найден ни разу - возвращаем саму строку
	// в качестве единственного результата
	if (delim_at == -1)
	{
		// Добавляем всю строку в вектор (динамический массив)
		result.push_back(str);
		return result;
	}

	// 0 1 2 3 4 5 6 7 8 9 10 11
	// H e l l o   w o r l d  !  

	// Отмечаем начало подстроки
	int start = 0;

	// Пока разделитель удаётся найти
	while (delim_at != -1)
	{
		// Выделяем подстроку от начала, количество символов
		// считаем как разницу между концом и началом
		std::string part = str.substr(start, delim_at - start);
		result.push_back(part);

		// Начало следующей подстроки идёт сразу за текущим разделителем
		start = delim_at + 1;
		// Находим новый разделитель, уже по оффсету от нового начала
		delim_at = str_find_first_offset(str.c_str(), delimiter, start);
	}

	if (start < str.length())
	{
		// Если в substr() берём все символы от оффсета до конца строки
		// то количество указывать не требуется
		std::string last_part = str.substr(start);
		result.push_back(last_part);
	}

	return result;
}