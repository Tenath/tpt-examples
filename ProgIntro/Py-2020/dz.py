# Исходная функция
def fn1(x,y,z):
    return 6.3 * (x**2) + 3.1*y - z

# Чтение файла
try:
    lines=[]
    with open("data.csv", "r") as file:
        lines = file.read().splitlines()
except:
    print("File error")
    exit()

#for i in range(0,len(lines)):
#    line = lines[i]

# Выделение компонентов и вызов функции
results = []
ctr = 1
for line in lines:
    parts = line.split(',')
    # ["3.2", "5.1", "2", "6"]
    try:
        x = float(parts[0])
        y = float(parts[1])
        z = float(parts[2])
        
        results.append(fn1(x,y,z))
    except:
        print(f'Error on line #{ctr}')
    ctr += 1

# Запись результатов в файл
with open("output.txt", "w") as file:
    for result in results:
        file.write(f'{result}\n')

