#pragma once
#include <stdexcept>
#include <string>

class LinkedListNode
{
private:
	std::string payload;
	LinkedListNode* next = nullptr;
public:
	LinkedListNode(const std::string& value, LinkedListNode* p_next = nullptr)
	{
		payload = value;
		next = p_next;
	}

	std::string GetValue() { return payload; }
	LinkedListNode* GetNext() { return next; }

	void SetNext(LinkedListNode* p_next)
	{
		if (p_next == this)
			throw std::invalid_argument("Trying to add circular reference in linked list");
		next = p_next;
	}
};

class LinkedList
{
private:
	LinkedListNode* first = nullptr;

public:
	void Add(const std::string& value)
	{
		if (first == nullptr)
		{
			first = new LinkedListNode(value);
		}
		else
		{
			// Ищем последний узел в списке
			LinkedListNode* last = GetLast();
			// Создаём новый узел (следующим для него будет 0)
			LinkedListNode* node = new LinkedListNode(value);
			// Выставляем добавляемый узел как следующий
			// у последнего (т.е. он становится предпоследним)
			last->SetNext(node);
		}
	}

	// Задание 1: Реализовать удаление последнего элемента в списке
	//            метод RemoveLast()
	// Задание 2: Реализовать добавление элемента в начало списка AddFirst()
	// Задание 3: и удаление элемента из начала списка RemoveFirst()
	// Задание 4: Реализовать обращение к элементу по индексу (можно через оператор[])
	// Задание 5: Реализовать добавление элемента по указанному индексу 
	//            (перед N-ным элементом)
	// Задание 6: Реализовать удаление элемента по указанному индексу
	// * Задание 7: Реализовать метод Swap(LinkedListNode* n1, LinkedListNode* n2),
	//              который меняет два узла местами в списке (путем обмена поинтерами)

	LinkedListNode* GetFirst()
	{
		return first;
	}

	LinkedListNode* GetLast()
	{
		if (first == nullptr) return nullptr;

		LinkedListNode* node = first;

		while (node->GetNext() != nullptr)
		{
			node = node->GetNext();
		}

		return node;
	}
};