#pragma once
#include <string>
#include <vector>

#ifdef __cplusplus
extern "C"
{
	__declspec(dllexport) void __cdecl str_print_symbols(const char* str);
	__declspec(dllexport) char* __cdecl str_concat(const char* s1, const char* s2);
	__declspec(dllexport) int __cdecl str_find_first(const char* str, char sym);
	__declspec(dllexport) int __cdecl str_find_first_offset(const char* str, char sym, int offset);
	__declspec(dllexport) int __cdecl str_find_last(const char* str, char sym);
}

__declspec(dllexport) std::vector<std::string> __cdecl ReadAllLines(const std::string& filename);
__declspec(dllexport) std::vector<std::string> __cdecl StringSplit(const std::string& str, char delimiter);

#endif