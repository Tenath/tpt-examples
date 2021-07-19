numbers = [1, 4, 2, 6, 8, 11]

# Открыть файл в режиме записи
file = open("numbers.txt", "w")

# Записать элементы списка как отдельные строки в файле
for n in numbers:
    file.write(f'{n}\n')

# Закрыть файл
file.close()