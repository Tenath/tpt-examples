import pymysql

# Функция, формирующая заполнить из знаков =
# длиною с указанную в аргументе строку
def PrintPadding(string):
	print(" ",end="")
	for i in range(0, len(string)-1):
		print("=",end="")
	print()
	

def PrintTable(conn, table):
	# Работаем в контексте соединения
	with conn:
		# Получаем "курсор" соединения
		cur = conn.cursor()
		# Выполняем запрос
		cur.execute(f"SELECT * FROM {table}")
		
		desc = cur.description
		# Забираем данные в виде списка
		manuf = cur.fetchall()
		
		# Формируем строку заголовка
		header = ""
		# Для каждого столбца таблицы
		for column in desc:
			# добавляем название столбца, выделяем под него 10 символов
			header += f" | {column[0]:>15}"
		header+="|"
			
		PrintPadding(header)
		print(header) # Выводим сформированную строку заголовка
		PrintPadding(header)
		
		# Отображаем полученные записи
		for m in manuf:
			for field in m:
				print(f" | {field:>15}",end="")
			print("|")
		PrintPadding(header)