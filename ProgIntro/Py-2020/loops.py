# Проходим по числам от 1 до 4 (т.е. интервал в range
# не включает конечное значение)
sum = 0

for i in range(1,5,2):
    # Короткий оператор
    sum += i
    
    # Длинный вариант
    # sum = sum + i

print("The sum is",sum)

# Задание 3: Посчитать сумму чётных чисел в интервале [2,11]
sum = 0
for i in range(2,12,2):
    sum += i
# * Задание 3.5: Посчитать сумму чётных чисел в интервале [1,11]
for i in range(1,12,2):
    if i % 2 == 0:
        sum += i
    #sum += i+1
        

# Задание 4: Вывести числа от 10 до 1 (включительно), т.е. от 1 до 10 в обратном порядке
for i in range(10,0,-1):
    print(i)

for i in range(0,10):
    print(10-i)

# Задание 5: Получить от пользователя левую и правую границу интервала (включительно)
# Посчитать сумму чисел в указанном интервале
# NB! Проверить, чтобы границы интервала были заданы корректно (левая граница меньше правой)

# *** Доп. задание: Получить от пользователя количество чисел для ввода, и затем сами
# числа. Найти их сумму.

# Решение без цикла
# i=1
# print(i)
# 
# i=2
# print(i)
# 
# i=3
# print(i)
# 
# i=4
# print(i)
# 
# i=5