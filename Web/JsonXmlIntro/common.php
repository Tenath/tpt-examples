<?php
function ReadFruits($filename)
{
	// Результат: Массив для прочитанных из файла данных по фруктам
	$fruits = array();

	// Открываем файл
	$file = fopen($filename,"r");

	while(!feof($file))
	{
		// Считываем строчку из файла
		$line = fgets($file);
		// Делим её на составляющие по разделителю ("," - запятая)
		$fields = explode(",", $line);
		
		// Создаём ассоциативный массив с данными по одному фрукту
		// (прочитанными из отдельных полей)
		$fruit = array(
			"index" => $fields[0],
			"name" => $fields[1],
			"color" => $fields[2],
			"size" => $fields[3]
		);
		
		// Добавляем ассоциативный массив с данными по фрукту
		// в общий массив фруктов
		$fruits[]=$fruit;
	}
	
	// Закрываем файл
	fclose($file);
	// Возвращаем результат
	return $fruits;
}


// Функция, которая подключается к базе, и формирует список продуктов
// который и возвращает
function ReadProductsDb($host, $user, $pass, $db)
{
	$products = array();
	
	// Создаём соединение с базой данных
	$conn = new mysqli($host,$user,$pass,$db);
	
	// Если возникла ошибка при соединении
	// останавливаем работу скрипта с текстом об ошибке
	if($conn->connect_error)
		die("Unable to connect to database");
	
	// Строка с SQL-запросом
	$query = "SELECT * FROM products";
	
	// Выполняем запрос, сохраняем данные в переменную
	$result = $conn->query($query);
	
	// Крутимся в цикле до тех пор, пока в очереди
	// остаются строчки, полученные из базы
	while($data = $result->fetch_assoc())
	{
		// Добавляем полученные данные в возвратный массив
		$products[]=$data;
	}
	
	return $products;
}
?>