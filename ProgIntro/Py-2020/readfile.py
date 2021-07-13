lines = []

# Способ 1: Обработка вручную
# file = open("fruits.txt", "r")
# 
# strlen = 1
# while strlen != 0:
#     line = file.readline()
#     line = line.replace('\n','')
#     lines.append(line)
#     strlen = len(line)
#     #print(line, end='')
# 
# file.close()

# Вариант 2
# Открыли файл в режиме чтения и прочитали все строчки
# получили список строчек
# with open("fruits.txt", "r") as file:
#     lines = file.readlines()
# # Прошли по каждой строчке, убрали из неё символ перехода
# # на новую строку (newline)
# for i in range(len(lines)):
#     lines[i] = lines[i].replace('\n','')
# 
# # Вывели строчки
# for i in range(len(lines)-1,-1,-1):
#     print(lines[i])

# Вариант 3
# Считываем весь файл как одну длинную строчку
# и далее нарезаем его на отдельные строки
# (splitlines() при этом удаляет \n)

# Длинный вариант (с промежуточной строковой
# переменной)
with open("fruits.txt", "r") as file:
    all_lines = file.read()
    lines = all_lines.splitlines()

# Короткий вариант с цепочкой операций в
# одном выражении
with open("fruits.txt", "r") as file:
    lines = file.read().splitlines()
    
for i in range(len(lines)-1,-1,-1):
     print(lines[i])
     
with open("fruits_reverse.txt", "w") as file:
    for i in range(len(lines)-1,-1,-1):
        file.write(f'{lines[i]}\n')