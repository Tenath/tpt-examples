"""a = 5
b = 3
c = a+b
print(c)"""
# Можно сравнивать строчки между собой
# s = "abc"
# if a == "+":
    
# Задание 1: получить от пользователя
# два целочисленных значения
# также получить от него же обозначение
# арифметической операции (один из знаков + - * /)
# найти и вывести результат
# обработать случай, когда пользователь
# ввёл некорректное обозначение операции

# Задание 2: Получить от пользователя значения
# координат точки (x, y)
# Определить, в какой четверти координатной
# плоскости находится эта точка

# Решение с помощью вложенных if
if x>0:
    if y>0:
        print("1 quarter")
    elif y<0:
        print("4 quarter")
    else:
        print("Y axis")
elif x<0:
    if y>0:
        print("2 quarter")
    elif y<0:
        print("3 quarter")
    else:
        print("Y axis")
else:
    if y!=0:
        print("X axis")
    else:
        print("beginning of coordinates")


# Отдельно можно обработать случай
# когда точка находится на одной из осей
# или в начале координат

