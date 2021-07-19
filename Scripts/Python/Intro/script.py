## Структуры данных
# Список (изменяемый индексированный контейнер == динамический массив)
list = [1,2,"3"]
list.append(4)
print(list[0])

# Кортеж (неизменяеммый индексированный контейнер)
# == константный массив статического размера
tuple = 1,2,3 # можно без скобок
tuple2 = (4,5,6) # можно в скобках

print(tuple[0])

# Словарь == ассоциативный массив
dictionary = {
  "apple": "red",
  "orange": "orange",
  "banana": "yellow"
}

print(dictionary["apple"])

# Добавление/изменение записи в словаре
dictionary["pineapple"] = "brown"

# Удаление элементов из словаря и списка
del dictionary["orange"]
del list[0]
# Кортеж - неизменяемый контейнер, поэтому из него удалять нельзя

# Список кортежей
fruits = [
	("1", "apple", "red"),
	("2", "grape", "purple"),
	("3", "orange", "orange")
]

# Отображаем кортеж в виде таблицы 
# (отделяем через табы)
print()
print("#\tName\tColor")
for fruit in fruits:
	# Объединяем элементы кортежа в одну строку
	# где элементы разделены табами
	line = "\t".join(fruit)
	print(line)

# Генерируем .csv
file = open("fruits.csv", "w")

for fruit in fruits:
	line = ",".join(fruit)
	file.write(f"{line}\n")

file.close()