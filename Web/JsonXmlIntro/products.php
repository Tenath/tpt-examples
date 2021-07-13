<!doctype HTML>
<html>
<head>
	<title>Products</title>
	<style>
		table, th, td {
			border: solid 1px black;
			border-collapse: collapse;
		}
	</style>
</head>
<body>
<?php 
include("common.php");

$products = ReadProductsDb("localhost","root","","pcwares");

//var_dump($products);
?>
<table>
	<tr>
		<th>Manufacturer</th>
		<th>Name</th>
		<th>SKU</th>
		<th>Product Type</th>
		<th>MSRP</th>
		<th>Warranty</th>
		<th>Info</th>
		<th>Description</th>
	</tr>
	<?php
		// Задание: сделать так, чтобы производители и виды продукта
		// выводились в виде текста
	
		// Проходим по всем продуктам
		foreach($products as $prod)
		{
			// Открываем тэг "строка табличка"
			echo "<tr>";
			// Такой вариант подходит, если выводим данные 1:1
			/*foreach($prod as $field)
			{
				echo "<td>".$field."</td>\n";
			}*/
			
			echo "<td>".$prod["Manufacturer"]."</td>";
			echo "<td>".$prod["Name"]."</td>";
			echo "<td>".$prod["SKU"]."</td>";
			echo "<td>".$prod["ProductType"]."</td>";
			echo "<td>\$ ".$prod["MSRP"]."</td>";
			echo "<td>".$prod["WARRANTY"]."</td>";
			echo "<td><a href=\"".$prod["InfoURL"]."\">Link</a></td>";
			echo "<td>".$prod["Description"]."</td>";
			// Закрываем строку таблицы
			echo "</tr>";
		}
	?>
</table>
<a href="localhost">Link</a>
</body>
</html>