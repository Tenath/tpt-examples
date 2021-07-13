<?php
header("Content-Type: application/json");
include("common.php");

$products = ReadProductsDb("localhost","root","","pcwares");

echo json_encode($products, JSON_PRETTY_PRINT);
?>