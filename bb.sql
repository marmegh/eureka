CREATE DATABASE  IF NOT EXISTS `wedding` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `wedding`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: wedding
-- ------------------------------------------------------
-- Server version	5.7.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `ideas`
--

DROP TABLE IF EXISTS `ideas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ideas` (
  `ideaID` int(11) NOT NULL AUTO_INCREMENT,
  `idea` varchar(100) DEFAULT NULL,
  `userID` int(11) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  PRIMARY KEY (`ideaID`),
  KEY `originator_idx` (`userID`),
  CONSTRAINT `origin_userID` FOREIGN KEY (`userID`) REFERENCES `users` (`userID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ideas`
--

LOCK TABLES `ideas` WRITE;
/*!40000 ALTER TABLE `ideas` DISABLE KEYS */;
INSERT INTO `ideas` VALUES (1,'Mittens',1,'2017-11-03 10:46:27','2017-11-03 10:46:27'),(2,'More Mittens',1,'2017-11-03 10:52:33','2017-11-03 10:52:33'),(3,'Scarves',6,'2017-11-03 11:57:02','2017-11-03 11:57:02');
/*!40000 ALTER TABLE `ideas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `likes`
--

DROP TABLE IF EXISTS `likes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `likes` (
  `likeid` int(11) NOT NULL AUTO_INCREMENT,
  `userID` int(11) DEFAULT NULL,
  `ideaid` int(11) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  PRIMARY KEY (`likeid`),
  KEY `liker_userID_idx` (`userID`),
  KEY `liked_ideaid_idx` (`ideaid`),
  CONSTRAINT `liked_ideaid` FOREIGN KEY (`ideaid`) REFERENCES `ideas` (`ideaID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `liker_userID` FOREIGN KEY (`userID`) REFERENCES `users` (`userID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `likes`
--

LOCK TABLES `likes` WRITE;
/*!40000 ALTER TABLE `likes` DISABLE KEYS */;
INSERT INTO `likes` VALUES (2,1,1,'2017-11-03 11:35:29','2017-11-03 11:35:29'),(3,1,2,'2017-11-03 11:35:58','2017-11-03 11:35:58'),(4,3,1,'2017-11-03 11:36:47','2017-11-03 11:36:47'),(5,3,2,'2017-11-03 11:36:48','2017-11-03 11:36:48');
/*!40000 ALTER TABLE `likes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rsvp`
--

DROP TABLE IF EXISTS `rsvp`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `rsvp` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `weddingID` int(11) DEFAULT NULL,
  `userID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `userID_idx` (`userID`),
  KEY `weddingID_idx` (`weddingID`),
  CONSTRAINT `userID` FOREIGN KEY (`userID`) REFERENCES `users` (`userID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `weddingID` FOREIGN KEY (`weddingID`) REFERENCES `weddings` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rsvp`
--

LOCK TABLES `rsvp` WRITE;
/*!40000 ALTER TABLE `rsvp` DISABLE KEYS */;
/*!40000 ALTER TABLE `rsvp` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `userID` int(11) NOT NULL AUTO_INCREMENT,
  `first` varchar(100) DEFAULT NULL,
  `last` varchar(100) DEFAULT NULL,
  `email` varchar(250) DEFAULT NULL,
  `password` varchar(250) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `alias` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`userID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'Mary','Martin','marmegh@where.com','AQAAAAEAACcQAAAAEDBNz2JgX4vKhVsMUzijC47W7ew1flGEK2ohzVBqADc9SP9zXMfV7Hl+2jucA8LnKw==','2017-11-01 14:21:56','2017-11-01 14:21:56','marmegh'),(3,'Kristen','Martin','kam@now.com','AQAAAAEAACcQAAAAEOxIkvj8fd1gxvgMi2sfHgWLNWdy2sI/zLQRRo241KYkVDwKhmwJtZLSFwQGvcUVCA==','2017-11-02 21:39:25','2017-11-02 21:39:25','kam'),(4,'Peggy','Martin','pema@where.com','AQAAAAEAACcQAAAAEMkjTV9Mi8V+3bFWIjrl5tPzo3ZtbN5bVvolPtE7U40XDg8i2ybmjjlV60oO6RSu9Q==','2017-11-02 21:52:38','2017-11-02 21:52:38','pema'),(5,'Chad','Martin','martinch@amgen.com','AQAAAAEAACcQAAAAEELd5V74lzAQrGKpYoNJBrnINQ5IcWEIiiuTEtZCTYxAWR4/HXjx4nioOxPKN6EcDA==','2017-11-02 21:56:05','2017-11-02 21:56:05','martinch'),(6,'Rita','Malloy','ram@here.com','AQAAAAEAACcQAAAAEB4rD3k9+VNmjuWKIPwqJJfXiccwfJx2ikIGbF2UBzieXvfhSQ9KULqpK9ftwqP3vQ==','2017-11-02 21:58:08','2017-11-02 21:58:08','ram'),(7,'Someone Else','newbie','this@that.com','AQAAAAEAACcQAAAAEDrUHFVGRij6pS7dsRJx/35Ff4hb4oT7nIN7ErzoHc4WJyJVyC9fEX1NT09VBNsWTQ==','2017-11-03 09:44:52','2017-11-03 09:44:52','personthing'),(8,'Katie','KGH','kgh@now.com','AQAAAAEAACcQAAAAEMBQMBA0tu5JvLnis2MRbjzzpAoO9A6KOrnbYmZZykd5TvCxSxC4zVubCUo9i800Rw==','2017-11-03 10:00:01','2017-11-03 10:00:01','KGH');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `weddings`
--

DROP TABLE IF EXISTS `weddings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `weddings` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `userID` int(11) DEFAULT NULL,
  `bride` varchar(100) DEFAULT NULL,
  `groom` varchar(100) DEFAULT NULL,
  `address` varchar(150) DEFAULT NULL,
  `date` date DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `planner_userID_idx` (`userID`),
  CONSTRAINT `planner_userID` FOREIGN KEY (`userID`) REFERENCES `users` (`userID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `weddings`
--

LOCK TABLES `weddings` WRITE;
/*!40000 ALTER TABLE `weddings` DISABLE KEYS */;
INSERT INTO `weddings` VALUES (8,1,'test','test','test','2022-01-01','2017-11-02 22:05:58','2017-11-02 22:05:58');
/*!40000 ALTER TABLE `weddings` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-11-03 12:49:32
