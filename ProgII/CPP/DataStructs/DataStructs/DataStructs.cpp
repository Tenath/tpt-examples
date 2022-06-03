// DataStructs.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <string>
#include "IntArray.hpp"
#include "Array.hpp"
#include "LinkedList.hpp"

template<typename T> void PrintArray(Array<T>& array)
{
    for (size_t i = 0; i < array.GetSize(); i++)
    {
        std::cout << array[i] << "\n";
    }
}

void ArrayTest()
{
    int basic_array[] = { 1, 4, 5 };

    std::string str = "hello";
    //Array<std::string> str_array(3,str);
    Array<std::string> str_array = { "Hello", "world", "and" };

    str_array.Add("TA-20V");
    std::cout << "Array of strings:\n";
    PrintArray(str_array);
    std::cout << "\n";

    Array<int> arr(5, 3);
    Array<int> copy = arr;

    std::cout << "Copy made with constructor:\n";
    PrintArray(copy);

    std::cout << "Initial state of array:\n";
    PrintArray(arr);

    arr[2] = 1;
    std::cout << "\nModified array:\n";
    PrintArray(arr);

    copy = arr;

    std::cout << "Copy made with assignment operator:\n";
    PrintArray(copy);

    arr.Add(10);
    std::cout << "\nElement 10 added to the end of the array:\n";
    PrintArray(arr);

    for (int i : arr)
    {
        std::cout << i << " ";
    }

    /*int array[10];
    int* array_ptr = new int[10];
    *array_ptr = 0;*/
}

void PrintList(LinkedList& lst)
{
    LinkedListNode* node = lst.GetFirst();
    size_t node_ctr = 1;
    while (node != nullptr)
    {
        std::cout << "Node #" << node_ctr++ << ", Address: 0x" << node << ", Value: "
            << node->GetValue() << "\n";
        node = node->GetNext();
    }
}

int main()
{
    LinkedList list;

    //std::cout << "First node address: 0x" << list.GetFirst() << "\n";

    list.Add("TA-20V");

    //std::cout << "First node address: 0x" << list.GetFirst() << ", value: "
    //    << list.GetFirst()->GetValue() << "\n";

    list.Add("TA-19V");

    //std::cout << "Second node address: 0x" << list.GetFirst()->GetNext() << ", value: "
    //    << list.GetFirst()->GetNext()->GetValue() << "\n";

    list.Add("TA-18V");

    LinkedListNode* node = list.GetFirst();

    //list.RemoveLast();
    list.AddFirst("TA-21V");

    PrintList(list);

    LinkedListNode* n = list.GetFirst()->GetNext()->GetNext();
    LinkedListNode* n_prev = list.FindPrevious(n);

    std::cout << list[2];
}