# Функция (общего пользования), которая проходит по предоставленному списку строчек
# (полученному из файла или другого источника), нарезает его на числовые значения
# по разделителям, и возвращает список наборов таких значений
def GetFnArgsFromLines(lines):
    # Список "наборы аргументов" - его возвращаем в конце функции
    argument_sets = []
    ctr = 1
    # Проходим по каждой строке в исходном списке строк
    for line in lines:
        # Делим строку на части по сепаратору (получаем список строк-частей)
        parts = line.split(',')
        # ["3.2", "5.1", "2", "6"]
        try:
            # Список компонентов, выделенных из строчки
            arguments = []
            # Проходим по каждой подстроке
            for part in parts:
                # Пытаемся интерпретировать строчку, как число (типа float)
                # если это удаётся, то добавляем полученное число в список
                # компонентов
                arguments.append(float(part))
            # После того, как составили список компонентов, выделенных из
            # одной строки, добавляем его в список наборов аргументов
            argument_sets.append(arguments)
        except:
            # Если возникает ошибка при обработке строчки, сообщаем о
            # проблемной строчке (по индексу), и переходим к следующей
            # строке в файле
            print(f'Error on line #{ctr}')
        ctr += 1
    # Возвращаем полученный список наборов аргументов (список списков)
    return argument_sets

args = GetFnArgsFromLines(["1,2,3","4,5,6"])
print(args)

def fn1(x,y,z):
    return 6.3 * (x**2) + 3.1*y - z

# Задача: для каждого набора аргументов отобразить значение функции
# отформатировать в виде: fn1(арг1,арг2,арг3) = результат







