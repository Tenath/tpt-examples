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
		// '\0' - ������ ����� ������, �������� �������� 0x00
		size_t length = strlen(str);

		for (size_t i = 0; i < length; i++)
		{
			// ���������� ����� ������������, ��� �������� 
			// (������������ ��������� �� �������)
			printf("%d: %c\n", i, str[i]);
		}
	}

	// �� �� �����, ��� strcat()
	__declspec(dllexport) char* __cdecl str_concat(const char* s1, const char* s2)
	{
		size_t len_s1 = strlen(s1);
		size_t len_s2 = strlen(s2);

		size_t new_length = len_s1 + len_s2;

		// �������� ������ ��� ����� ������, ������ ������� ����� �����
		// ������������ ����� + 1 (��� ������� ������)
		char* new_str = (char*)malloc(sizeof(char) * (new_length + 1));
		if (new_str == NULL) return NULL;

		new_str[new_length] = '\0';

		// ����������� ������ ������ � ����������� ������
		// memcpy, strcpy
		for (size_t i = 0; i < len_s1; i++)
		{
			new_str[i] = s1[i];
		}

		// ����������� ������ ������ � ����������� ������ (����� ������)
		for (size_t i = 0; i < len_s2; i++)
		{
			new_str[len_s1 + i] = s2[i];
		}

		return new_str;
	}

	__declspec(dllexport) int __cdecl str_find_first(const char* str, char sym)
	{
		size_t length = strlen(str);

		// ��� ����� ������ ��������� �������� ������� � ������?
		// �������� �� ���� ������ �� ������ � �����
		for (size_t i = 0; i < length; i++)
		{
			// ���� ������� ������ - �������, ���������� ��� ������
			if (str[i] == sym) return i;
		}

		// ���� ������ �� ����� ������� �� ������ �� ������, ���������� -1
		return -1;
	}

	// ������� ���� ������ ��������� ������� ������� �� ���������� �������
	// �.�. �������� ������ �� � ������ ������, � � ���������� �������
	__declspec(dllexport) int __cdecl str_find_first_offset(const char* str, char sym, int offset)
	{
		int length = strlen(str);

		// ��� ����� ������ ��������� �������� ������� � ������?
		// �������� �� ���� ������ �� ������ � �����
		for (int i = offset; i < length; i++)
		{
			// ���� ������� ������ - �������, ���������� ��� ������
			if (str[i] == sym) return i;
		}

		// ���� ������ �� ����� ������� �� ������ �� ������, ���������� -1
		return -1;
	}

	// �������: ����� ���������� ��������� � ������ ����������� ��������������
	// ������������ ������ ������� �� ������ ������
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

	// ������� �������� ����� � ������ ������
	std::ifstream fs(filename);

	std::string line;

	// ������� � �����, ��������� ������� �� ��������� ������
	// ���� �� ����� �� ����� ����� (���� �� ��������� ������ ������)
	while (std::getline(fs, line))
	{
		// � ���������� ������(������) ��������� ��������� �������
		result.push_back(line);
	}

	return result;
}

// ������� ���������� ������ �� ����� �� �����������
// "Hello world!", ' ' => { "Hello", "world!" }
// "Hello world!", ',' => { "Hello world!" }
// "1,Intel,Core i9-11900K,650 EUR", ',' => { "1", "Intel", "Core i9-11900K", "650 EUR" }
__declspec(dllexport) std::vector<std::string> __cdecl StringSplit(const std::string& str, char delimiter)
{
	std::vector<std::string> result;

	// ���� ������ ��������� ����������� � ������
	int delim_at = str_find_first(str.c_str(), delimiter);

	// ���� ����������� � ������ �� ������ �� ���� - ���������� ���� ������
	// � �������� ������������� ����������
	if (delim_at == -1)
	{
		// ��������� ��� ������ � ������ (������������ ������)
		result.push_back(str);
		return result;
	}

	// 0 1 2 3 4 5 6 7 8 9 10 11
	// H e l l o   w o r l d  !  

	// �������� ������ ���������
	int start = 0;

	// ���� ����������� ������ �����
	while (delim_at != -1)
	{
		// �������� ��������� �� ������, ���������� ��������
		// ������� ��� ������� ����� ������ � �������
		std::string part = str.substr(start, delim_at - start);
		result.push_back(part);

		// ������ ��������� ��������� ��� ����� �� ������� ������������
		start = delim_at + 1;
		// ������� ����� �����������, ��� �� ������� �� ������ ������
		delim_at = str_find_first_offset(str.c_str(), delimiter, start);
	}

	if (start < str.length())
	{
		// ���� � substr() ���� ��� ������� �� ������� �� ����� ������
		// �� ���������� ��������� �� ���������
		std::string last_part = str.substr(start);
		result.push_back(last_part);
	}

	return result;
}