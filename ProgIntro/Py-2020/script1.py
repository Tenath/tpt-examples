#from MyFunctions import *
import MyFunctions

#a = MyFunctions.InputInteger("Enter the first number: ")
#b = MyFunctions.InputInteger("Enter the second number: ")
#c = MyFunctions.InputInteger("Enter the third number: ")

lst = [1,3,5]
#print(lst[1])

for i in range(0,len(lst)):
    number = lst[i]
    print(lst[i],end=' ')

print()
n1 = MyFunctions.InputInteger("Enter a number to append to the list: ")
lst.append(n1)
lst.insert(0,20)
lst.insert(3,"abcd")

for number in lst:
    print(number,end=' ')

#print(f'The result of {a} + {b} + {c} is {a+b+c}')

# Задание 2: Сделать аналогичную функцию, только для float

sum = 0
for i in range(1,6):
    sum += i

sum = 0
list3 = [1, 3, 6, 8, 2, 4, 7, -1]
for number in lst:
    sum += number


# Задание 3: Сделать (вручную, прямо в коде) список из 8 элементов,
# посчитать их сумму и вывести

# Задание 4: Вывести элементы списка в обратном порядке
# (не изменяя его самого, и не делая с него копий)
for i in range(len(list3)-1,-1,-1):
    print(list3[i])

# Задание 5: Сделать список из 10 элементов, где содержатся как положительные,
# так и отрицательные элементы. На его основе сформировать два новых списка
# в одном из которых содержатся только положительные (и 0), во втором -
# только отрицательные
lstAll = [1,-5,6,7,-3,-9,0,10,2,-1]
lstPos = []
lstNeg = []    

for num in lstAll:
    if num >= 0:
        lstPos.append(num)
    else:
        lstNeg.append(num)

