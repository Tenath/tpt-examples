class Manufacturer:
	# Конструктор - функция, которая инициализирует создаваемый объект
	def __init__(self, id, name):
		self.ID = id
		self.Name = name
	
	# Статический метод для создания объекта из строки в формате .csv
	@staticmethod
	def FromCsv(line):
		fields = line.split(",")
		return Manufacturer(fields[0], fields[1])
	
	# Метод для красивого форматирования объекта
	def description(self):
		return f"ID: {self.ID}\nName: {self.Name}"

	# Метод для формирования строки с описанием объекта для .csv файла
	def csv(self):
		return f"{self.ID},{self.Name}"

def ReadLines(filename):
	lines = []
	file = open(filename,"r")
	
	strlen = 1
	while strlen != 0:
		line = file.readline()
		strlen = len(line)
		line = line.strip('\n')
		if strlen!=0:
			lines.append(line)
	
	file.close()
	
	return lines

def WriteManufacturers(filename, manuf):
	file = open(filename,"w")
	
	for m in manuf:
		file.write(f"{m.csv()}\n")
	
	file.close()

def ReadManufacturers(filename):
	result = []
	
	lines = ReadLines(filename)
	
	for line in lines:
		mf = Manufacturer.FromCsv(line)
		result.append(mf)
	return result
# Создаём объект
#m = Manufacturer(1, "Intel")

# Обращаемся к полям вручную
#print(f'ID: {m.ID}')
#print(f'Name: {m.Name}')

# Используем метод для подготовки строчки вывода
#print(m.description())

# Формируем строку в формате .csv
#print(m.csv())
#csvline = m.csv()

# Создаём объект из строки в формате .csv
#m2 = Manufacturer.FromCsv("2,AMD")
# Выводим информацию о свежесозданном объекте
#print(m2.description())