# split() - разделение строки на части по указаному
# символу-разделителю (по умолчанию - пробел)
sentence = "I_have_three_apples_and_two_oranges"

result = sentence.split("_")

# Задание 1:
# Прочитать текст из файла, указанного как аргумент командной строки
# посчитать в нём количество слов.
text = ""
with open(sys.argv[1], "r") as file:
    text = file.read()

words = text.split()
len(words)

# Обращение к аргументам командной строки
import sys
for arg in sys.argv:
    print(arg)

##############
str = "1234"
# проверяет, можно ли строку интерпретировать как число
# т.е. содержит ли она только цифры и символы-разделители .,
is_a_number = str.isnumeric() 
# возвращает True/False

str.isalnum() # проверяет, содержит ли строка только буквы и цифры
str.isalpha() # только буквы
str.isupper() # только большие буквы
str.islower() # только маленькие буквы

# Перевод в верхний регистр (только большие буквы)
str.upper()
# Перевод в нижний регистр (только маленькие буквы)
str.lower()

# Задание 2: используя текст из №1, проверить, является
# ли каждое чётное по счёту слово числом
result = True
for i in range(0,len(words)):
    if (i+1) % 2 == 0:
        if words[i].isnumeric():
            result = False
            break

####################
fnargs = " 1,   2, 3     "
parts = fnargs.split(",")
parts = [" 1", "   2", " 3    " ]

# убирает пробелы из начала и конца строки
clean_part = parts[2].strip() 
# в других языках обычно называется trim()

s = "I_have_three_apples_and_two_oranges"
s = s.replace("_", " ")

# Задание 3: заменять яблоки на киви, апельсины на манго
#            а также три на десять, и два на пять
s = s.replace("apple","kiwi")
# s = ...

# Задание 4: сделать так, чтобы вызов функции replace()
#            происходил в коде только в 1 месте
to_replace = ["apples","oranges","three","two"]
replace_with = ["kiwi","mangos","ten","five"]

for i in range(0,len(to_replace)):
    s = s.replace(to_replace[i],replace_with[i])

#############
lst = ["a", "b", "c"]
lst[1]

# Словарь
person = {
    "name": "Vasya",
    "surname": "Pupkin",
    "class": "5B",
    "occupation": "student"
}

print(person["name"])

studentlist = [
    {"name": "Vasya", "surname": "Pupkin"},
    {"name": "Petya", "surname": "Ivanov"}
]

studentlist.append(person)

# Задание 5: Сделать список фруктов (3 элемента)
# каждый фрукт оформить как словарь
# у каждого фрукта должны быть поля:
# * название
# * количество
# * цвет
# * размер
fruitslist = [
    {"name":"Apple","count":"3", "color":"red", "size":"small"},
    {"name":"Orange","count":"2", "color":"orange", "size":"medium"},
    {"name":"Kiwi","count":"10", "color":"brown", "size":"small"},
]

# [Остаётся, как Д/З]
# * Задание 6: поместить данные по фруктам в .csv файле
# прочитать их, и динамически сформировать список словарей
# (на выходе должны получить то же, что и в прошлом задании)

# Пустой словарь
fruit = {}
# Если элемента с таким ключом в словаре не было - теперь добавится
fruit["name"]="Pineapple"
fruit["size"]="Large"

fruits = []
fruits.append(fruit)
fruits.append(fruit)