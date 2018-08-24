# Host: 119.29.11.92  (Version 5.7.22-log)
# Date: 2018-08-24 16:10:30
# Generator: MySQL-Front 6.0  (Build 2.20)


#
# Structure for table "lytable"
#

DROP TABLE IF EXISTS `lytable`;
CREATE TABLE `lytable` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `detail` text NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='留言';
