// StringClientExplicitTA20V.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <windows.h>

// алиас для интересующего нас типа указателя на функцию
typedef int(__cdecl* FINDPROC)(const char* str, char sym);
FINDPROC str_find_first = nullptr;
HINSTANCE lib = nullptr;

int LoadProcedures()
{
    lib = LoadLibrary(TEXT("DllStringsTA20V.dll"));

    if (lib == nullptr)
    {
        std::cout << "DLL load error\n";
        return -1;
    }

    str_find_first = (FINDPROC)GetProcAddress(lib, "str_find_first");

    if (str_find_first == nullptr)
    {
        std::cout << "Unable to find procedure in DLL\n";
        return -1;
    }

    return 0;
}

int main()
{
    int lresult = LoadProcedures();
    if (lresult != 0) return lresult;

    std::cout << "Hello World!\n";

    const char* input_str = "abcd";
    char sym = 'c';

    int index = str_find_first(input_str, sym);

    std::cout << "The first occurence of \'" << sym << "\' at string \""
        << input_str << "\" is at position " << index << "\n";

    FreeLibrary(lib);
}
