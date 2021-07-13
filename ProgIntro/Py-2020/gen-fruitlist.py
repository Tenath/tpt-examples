fruitslist = [
    {"name":"Apple","count":"3", "color":"red", "size":"small"},
    {"name":"Orange","count":"2", "color":"orange", "size":"medium"},
    {"name":"Kiwi","count":"10", "color":"brown", "size":"small"},
]

template = """<!doctype HTML>
<html>
<head>
	<title>Fruits list</title>
	<style>
		table,th,td {
			border: solid 1px black;
			border-collapse: collapse;
		}
	</style>
</head>
<body>
	<table>
		<tr>
			<th>Name</th>
			<th>Count</th>
			<th>Color</th>
			<th>Size</th>
		</tr>
		{rows}
	</table>
</body>
</html>"""

row_template = """<tr>
			<td>{name}</td>
			<td>{count}</td>
			<td>{color}</td>
			<td>{size}</td>
		</tr>"""
        
rows=""

for fruit in fruitslist:
    row = row_template
    row = row.replace("{name}",fruit["name"])
    row = row.replace("{count}",fruit["count"])
    row = row.replace("{color}",fruit["color"])
    row = row.replace("{size}",fruit["size"])
    rows+=row

template = template.replace("{rows}",rows)

with open("fruits.html", "w") as file:
    file.write(template)

# Д/З
# Задание 7:
# Фрукты брать из .csv файла
# Шаблоны считывать из отдельных файлов

# *** Задание 8:
# Сделать механизм общего применения, где названия полей
# в словарях берутся из файла
# данные также берутся из .csv
# (должно поддерживать произвольное количество полей)

