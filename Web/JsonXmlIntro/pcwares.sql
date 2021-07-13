-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 08, 2018 at 01:50 PM
-- Server version: 10.1.28-MariaDB
-- PHP Version: 7.1.11
CREATE DATABASE pcwares;
USE pcwares;

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pcwares`
--

-- --------------------------------------------------------

--
-- Table structure for table `manufacturers`
--

CREATE TABLE `manufacturers` (
  `ID` int(11) NOT NULL,
  `Name` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `manufacturers`
--

INSERT INTO `manufacturers` (`ID`, `Name`) VALUES
(1, 'Intel'),
(2, 'AMD'),
(3, 'ASUS'),
(4, 'Gigabyte'),
(5, 'MSI'),
(6, 'Dell'),
(7, 'Lenovo'),
(8, 'HP'),
(9, 'Seasonic'),
(10, 'Corsair'),
(11, 'EVGA');

-- --------------------------------------------------------

--
-- Table structure for table `orderinfo`
--

CREATE TABLE `orderinfo` (
  `ID` int(11) NOT NULL,
  `ProductID` int(11) NOT NULL,
  `SupplierID` int(11) NOT NULL,
  `OrderPrice` double DEFAULT '0',
  `Available` int(11) NOT NULL DEFAULT '0',
  `PriceAdjustment` float DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `orderinfo`
--

INSERT INTO `orderinfo` (`ID`, `ProductID`, `SupplierID`, `OrderPrice`, `Available`, `PriceAdjustment`) VALUES
(1, 1, 1, 370, 50, 1),
(2, 3, 2, 147.45, 14, 1),
(3, 2, 1, 350, 1, 1),
(4, 2, 3, 348, 23, 1),
(5, 4, 3, 201.15, 1, 1),
(6, 4, 1, 180.71, 15, 1);

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `ID` int(11) NOT NULL,
  `Name` varchar(150) NOT NULL,
  `SKU` varchar(60) NOT NULL,
  `Manufacturer` int(11) NOT NULL,
  `ProductType` int(11) NOT NULL,
  `MSRP` float DEFAULT NULL,
  `WARRANTY` int(11) DEFAULT '12',
  `ImageURL` varchar(200) DEFAULT NULL,
  `InfoURL` text,
  `Description` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`ID`, `Name`, `SKU`, `Manufacturer`, `ProductType`, `MSRP`, `WARRANTY`, `ImageURL`, `InfoURL`, `Description`) VALUES
(1, 'Core i7-8700K', 'BX80684I78700K', 1, 1, 370, 36, 'cpus/i7-8700k.png', 'http://ark.intel.com/', 'Socket 1151(CfL), 6 cores, 3.70/4.70 GHz, 12MB Cache'),
(2, 'Ryzen 7 2700X', 'YD270XBGAFBOX', 2, 1, 329, 36, 'cpus/r7-2700k.jpg', 'http://amd.com/', 'Socket AM4, 8 cores, 3.70/4.30GHz'),
(3, 'Core i3-8350K', 'BX80684I38350K', 1, 1, 179, 36, 'cpus/i3-8350k.jpg', 'http://ark.intel.com/', 'Socket 1151(CfL), 4 cores, 4.00 GHZ, 8MB Cache'),
(4, 'Aorus GA-AX370-Gaming 5', 'GA-AX380-Gaming 5', 4, 2, 179.99, 24, 'mb/ga-ax370-gaming5.jpg', 'http://gigabyte.eu/', 'Socket AM4, Chipset X370, 4x RAM'),
(5, 'ROG STRIX Z370-I Gaming', 'ROG STRIX Z370-I GAMING', 3, 2, 199.99, 24, 'mb/rog-strix-z370i-gaming.jpg', 'http://asus.com/', 'Socket 1151(CfL), Chipset Z370, mini-ITX, 2x RAM');

-- --------------------------------------------------------

--
-- Table structure for table `producttypes`
--

CREATE TABLE `producttypes` (
  `ID` int(11) NOT NULL,
  `Name` varchar(150) NOT NULL,
  `Category` enum('COMPONENTS','COMPUTERS','SERVERS','OFFICE','NETWORK') DEFAULT NULL,
  `PackageSize` enum('TINY','SMALL','MEDIUM','LARGE') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `producttypes`
--

INSERT INTO `producttypes` (`ID`, `Name`, `Category`, `PackageSize`) VALUES
(1, 'CPU', 'COMPONENTS', 'TINY'),
(2, 'Motherboard', 'COMPONENTS', 'SMALL'),
(3, 'RAM', 'COMPONENTS', 'TINY'),
(4, 'Storage', 'COMPONENTS', 'TINY'),
(5, 'Case', 'COMPONENTS', 'MEDIUM'),
(6, 'PSU', 'COMPONENTS', 'SMALL'),
(7, 'PrebuiltPC', 'COMPUTERS', 'MEDIUM'),
(8, 'Server', 'SERVERS', 'LARGE'),
(9, 'Printer', 'OFFICE', 'MEDIUM'),
(10, 'PrintingDevice', 'OFFICE', 'LARGE');

-- --------------------------------------------------------

--
-- Table structure for table `suppliers`
--

CREATE TABLE `suppliers` (
  `ID` int(11) NOT NULL,
  `Name` varchar(150) NOT NULL,
  `Country` varchar(40) NOT NULL,
  `EU` tinyint(1) NOT NULL DEFAULT '0',
  `MinDelivery` int(11) NOT NULL DEFAULT '0',
  `MaxDelivery` int(11) NOT NULL DEFAULT '0',
  `PriceFactor` double NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `suppliers`
--

INSERT INTO `suppliers` (`ID`, `Name`, `Country`, `EU`, `MinDelivery`, `MaxDelivery`, `PriceFactor`) VALUES
(1, 'Store', 'Estonia', 1, 0, 0, 1),
(2, 'Supplier A', 'Lithuania', 1, 2, 3, 1),
(3, 'Supplier B', 'Latvia', 1, 2, 3, 1),
(4, 'Supplier C', 'Poland', 1, 3, 5, 1),
(5, 'Supplier D', 'Germany', 1, 5, 7, 1),
(6, 'Supplier E', 'China', 0, 14, 21, 1.3);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `manufacturers`
--
ALTER TABLE `manufacturers`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `orderinfo`
--
ALTER TABLE `orderinfo`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `ProductID` (`ProductID`),
  ADD KEY `SupplierID` (`SupplierID`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Manufacturer` (`Manufacturer`),
  ADD KEY `ProductType` (`ProductType`);

--
-- Indexes for table `producttypes`
--
ALTER TABLE `producttypes`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `suppliers`
--
ALTER TABLE `suppliers`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `manufacturers`
--
ALTER TABLE `manufacturers`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `orderinfo`
--
ALTER TABLE `orderinfo`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `producttypes`
--
ALTER TABLE `producttypes`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `suppliers`
--
ALTER TABLE `suppliers`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `orderinfo`
--
ALTER TABLE `orderinfo`
  ADD CONSTRAINT `orderinfo_ibfk_1` FOREIGN KEY (`ProductID`) REFERENCES `products` (`ID`),
  ADD CONSTRAINT `orderinfo_ibfk_2` FOREIGN KEY (`SupplierID`) REFERENCES `suppliers` (`ID`);

--
-- Constraints for table `products`
--
ALTER TABLE `products`
  ADD CONSTRAINT `products_ibfk_1` FOREIGN KEY (`Manufacturer`) REFERENCES `manufacturers` (`ID`),
  ADD CONSTRAINT `products_ibfk_2` FOREIGN KEY (`ProductType`) REFERENCES `producttypes` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
