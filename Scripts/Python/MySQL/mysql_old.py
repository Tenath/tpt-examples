import pymysql
import mysqlutils

# Создать соединение с базой
conn = pymysql.connect('localhost', 'root', '', 'pcwares')

# Работаем в контексте соединения
#with conn:
#	# Получаем "курсор" соединения
#	cur = conn.cursor()
#	# Выполняем запрос
#	cur.execute("SELECT * FROM Manufacturers")
#	
#	desc = cur.description
#	# Забираем данные в виде списка
#	manuf = cur.fetchall()
#	# Отображаем полученные записи
#	print(f"{desc[0][0]}\t{desc[1][0]}\n")
#	for m in manuf:
#		print(f"{m[0]}\t{m[1]}")
mysqlutils.PrintTable(conn, "Manufacturers")
mysqlutils.PrintTable(conn, "ProductTypes")
