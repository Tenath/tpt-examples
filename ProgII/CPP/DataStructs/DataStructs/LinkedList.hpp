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

	std::string& GetValue() { return payload; }
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
	void RemoveLast()
	{
		// Обработка случая, когда список пуст
		if (first == nullptr) return;

		/* Находим предпоследний элемент */
		LinkedListNode* node = first;

		// Обработка случая, когда в списке один элемент
		// (т.е. нельзя найти предпоследний)
		if (node->GetNext() == nullptr)
		{
			delete node;
			first = nullptr;
			return;
		}

		while (node->GetNext()->GetNext() != nullptr)
		{
			node = node->GetNext();
		}
		// Из предпоследнего берём ссылку на последний
		LinkedListNode* last = node->GetNext();

		// У предпоследнего элемента следующим выставить nullptr
		node->SetNext(nullptr);

		// Удаляем уже исключённый из цепи последний элемент
		delete last;
	}

	// Задание 2: Реализовать добавление элемента в начало списка AddFirst()
	void AddFirst(const std::string& payload)
	{
		first = new LinkedListNode(payload, first);
	}
	// Задание 3: и удаление элемента из начала списка RemoveFirst()
	void RemoveFirst()
	{
		if (first == nullptr) return;

		LinkedListNode* deleted_node = first;
		first = first->GetNext();
		delete deleted_node;
	}
	// Задание 4: Реализовать обращение к элементу по индексу (можно через оператор[])
	std::string& operator[](size_t index)
	{
		return GetNodeAt(index)->GetValue();
	}

	// Поиск узла по индексу вынес в отдельную функцию
	LinkedListNode* GetNodeAt(size_t index)
	{
		LinkedListNode* node = first;

		if (first == nullptr)
			throw std::out_of_range("Linked list index out range");

		for (size_t i = 0; i < index; i++, node = node->GetNext())
		{
			if (node->GetNext() == nullptr)
				throw std::out_of_range("Linked list index out range");
		}

		return node;
	}

	// Задание 5: Реализовать добавление элемента по указанному индексу 
	//            (перед N-ным элементом)
	void Insert(const std::string& payload, size_t index)
	{
		// Если список пуст, либо вставляем по индексу 0,
		// тогда пользуемся услугами AddFirst()
		if (first == nullptr || index == 0) { AddFirst(payload); return; }

		// Находим элемент с индексом на 1 меньшим, чем тот, куда вставляем
		// (у предшествующего узла надо обновить указатель)
		LinkedListNode* node = GetNodeAt(index-1);
		// Следующим для нового узла будет следующий у текущего предшествующего
		LinkedListNode* new_node = new LinkedListNode(payload, node->GetNext());
		// Обновляем указатель в предшествующим на вновь созданный узел
		node->SetNext(new_node);
	}

	// Задание 6: Реализовать удаление элемента по указанному индексу

	// Задание 7: Реализовать метод Swap(LinkedListNode* n1, LinkedListNode* n2),
	//              который меняет два узла местами в списке (путем обмена поинтерами)

	// Можно сделать вспомогательный метод
	LinkedListNode* FindPrevious(LinkedListNode* node)
	{
		if (node == first || first == nullptr) return nullptr;

		LinkedListNode* cur = first;

		while (cur->GetNext() != node)
		{
			if (cur->GetNext() == nullptr) return nullptr;
			cur = cur->GetNext();
		}

		return cur;
	}

	// Задание 8: Реализовать методы 
	// InsertBefore(LinkedListNode* node, const std::string& value)
	// и InsertAfter(LinkedListNode* node, const std::string& value)
	// для вставки до и после указанного узла

	// Задание 9: Переделать связанный список в шаблон (template)

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