import pymysql
import mysqlutils

# Создать соединение с базой
conn = pymysql.connect('localhost', 'root', '', 'pcwares')

# Вызываем функцию для двух разных таблиц
mysqlutils.PrintTable(conn, "Manufacturers")
mysqlutils.PrintTable(conn, "ProductTypes")
