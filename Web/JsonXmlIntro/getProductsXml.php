<?php
header("Content-Type: application/xml; charset=utf-8");
include("common.php");

$products = ReadProductsDb("localhost","root","","pcwares");

echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "<Products>";
foreach($products as $product)
{
	echo "<Product id=\"".$product["ID"]."\">";
	echo "<Name>".$book["Name"]."</Title>";
	echo "<SKU>".$book["SKU"]."</SKU>";
	echo "<Manufacturer>".$book["Manufacturer"]."</Manufacturer>";
	echo "<ProductType>".$book["ProductType"]."</ProductType>";
	echo "<MSRP>".$book["MSRP"]."</MSRP>";
	echo "<WARRANTY>".$book["WARRANTY"]."</WARRANTY>";
	echo "<ImageURL>".$book["ImageURL"]."</ImageURL>";
	echo "<InfoURL>".$book["InfoURL"]."</InfoURL>";
	echo "<Description>".$book["Description"]."</Description>";
	echo "</Product>";
}
echo "</Product>";
?>