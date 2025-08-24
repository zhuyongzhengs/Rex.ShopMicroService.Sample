/*
 Navicat Premium Data Transfer

 Source Server         : Local_MySQL
 Source Server Type    : MySQL
 Source Server Version : 80036
 Source Host           : localhost:3306
 Source Schema         : rex.shop.promotionservice

 Target Server Type    : MySQL
 Target Server Version : 80036
 File Encoding         : 65001

 Date: 23/08/2025 23:48:06
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for __efmigrationshistory
-- ----------------------------
DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE `__efmigrationshistory`  (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of __efmigrationshistory
-- ----------------------------

-- ----------------------------
-- Table structure for cap.published
-- ----------------------------
DROP TABLE IF EXISTS `cap.published`;
CREATE TABLE `cap.published`  (
  `Id` bigint NOT NULL,
  `Version` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Content` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Retries` int NULL DEFAULT NULL,
  `Added` datetime NOT NULL,
  `ExpiresAt` datetime NULL DEFAULT NULL,
  `StatusName` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_ExpiresAt`(`ExpiresAt`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of cap.published
-- ----------------------------

-- ----------------------------
-- Table structure for cap.received
-- ----------------------------
DROP TABLE IF EXISTS `cap.received`;
CREATE TABLE `cap.received`  (
  `Id` bigint NOT NULL,
  `Version` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Name` varchar(400) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Group` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Content` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Retries` int NULL DEFAULT NULL,
  `Added` datetime NOT NULL,
  `ExpiresAt` datetime NULL DEFAULT NULL,
  `StatusName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_ExpiresAt`(`ExpiresAt`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of cap.received
-- ----------------------------
INSERT INTO `cap.received` VALUES (9027127011265376257, 'v1', 'Used.Coupon', 'Rex.PromotionService.HttpApi.Host.v1', '{\"Headers\":{\"cap-callback-name\":null,\"cap-corr-id\":\"9027127011262185474\",\"cap-corr-seq\":\"0\",\"cap-msg-id\":\"9027127011262185474\",\"cap-msg-name\":\"Used.Coupon\",\"cap-msg-type\":\"UsedCouponEto\",\"cap-senttime\":\"2025/8/23 23:36:35 \\u002B08:00\",\"cap-msg-group\":\"Rex.PromotionService.HttpApi.Host.v1\",\"cap-exec-instance-id\":\"LAPTOP-A4AM01CM\"},\"Value\":{\"CouponCode\":[\"QfCtj8qW8C\"],\"UsedId\":\"3a1be9bf-e6a1-4c82-399e-d821cc554718\",\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:36:36', '2025-08-24 23:36:36', 'Succeeded');

-- ----------------------------
-- Table structure for ps_coupons
-- ----------------------------
DROP TABLE IF EXISTS `ps_coupons`;
CREATE TABLE `ps_coupons`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `CouponCode` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PromotionId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `IsUsed` tinyint(1) NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `UsedId` char(50) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `StartTime` datetime(6) NOT NULL,
  `EndTime` datetime(6) NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_Ps_Coupons_CouponCode`(`CouponCode`) USING BTREE,
  INDEX `IX_Ps_Coupons_PromotionId`(`PromotionId`) USING BTREE,
  CONSTRAINT `FK_Ps_Coupons_Ps_Promotions_PromotionId` FOREIGN KEY (`PromotionId`) REFERENCES `ps_promotions` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ps_coupons
-- ----------------------------
INSERT INTO `ps_coupons` VALUES ('3a1be99f-ddfa-4a40-5e6f-dbb170ff2682', NULL, 'QfCtj8qW8C', '3a1be99e-81a7-bc02-9f7e-a19ad01663a4', 1, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', '3a1be9bf-e6a1-4c82-399e-d821cc554718', '[接口]用户领取优惠券。', '2025-08-23 23:01:36.373847', '2025-09-13 04:01:36.373848', '{}', 'f636d159adec4c03b829b31e02ddcf77', '2025-08-23 23:01:36.373847', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', '2025-08-23 23:36:36.321461', NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for ps_pintuangoods
-- ----------------------------
DROP TABLE IF EXISTS `ps_pintuangoods`;
CREATE TABLE `ps_pintuangoods`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `RuleId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GoodId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Ps_PinTuanGoods_RuleId`(`RuleId`) USING BTREE,
  CONSTRAINT `FK_Ps_PinTuanGoods_Ps_PinTuanRules_RuleId` FOREIGN KEY (`RuleId`) REFERENCES `ps_pintuanrules` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ps_pintuangoods
-- ----------------------------

-- ----------------------------
-- Table structure for ps_pintuanrecords
-- ----------------------------
DROP TABLE IF EXISTS `ps_pintuanrecords`;
CREATE TABLE `ps_pintuanrecords`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `TeamId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `RuleId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GoodId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Status` int NOT NULL,
  `OrderId` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Parameters` varchar(1500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CloseTime` datetime(6) NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ps_pintuanrecords
-- ----------------------------

-- ----------------------------
-- Table structure for ps_pintuanrules
-- ----------------------------
DROP TABLE IF EXISTS `ps_pintuanrules`;
CREATE TABLE `ps_pintuanrules`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `StartTime` datetime(6) NOT NULL,
  `EndTime` datetime(6) NOT NULL,
  `PeopleNumber` int NOT NULL,
  `SignificantInterval` int NOT NULL,
  `DiscountAmount` decimal(65, 30) NOT NULL,
  `MaxNums` int NOT NULL,
  `MaxGoodsNums` int NOT NULL,
  `Sort` int NOT NULL,
  `IsStatusOpen` tinyint(1) NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ps_pintuanrules
-- ----------------------------

-- ----------------------------
-- Table structure for ps_promotionconditions
-- ----------------------------
DROP TABLE IF EXISTS `ps_promotionconditions`;
CREATE TABLE `ps_promotionconditions`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `PromotionId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Parameters` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Ps_PromotionConditions_PromotionId`(`PromotionId`) USING BTREE,
  CONSTRAINT `FK_Ps_PromotionConditions_Ps_Promotions_PromotionId` FOREIGN KEY (`PromotionId`) REFERENCES `ps_promotions` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ps_promotionconditions
-- ----------------------------
INSERT INTO `ps_promotionconditions` VALUES ('3a1be99b-7d60-8ebe-ceb7-0113da0ce47b', NULL, '3a1be99a-b722-0990-e21a-34ab69db3e22', 'USER_GRADE', '{\"grades\":\"3a0d4972-e6b6-93ef-45eb-bbca4ae40270,3a0d4972-872c-9198-d9f6-d4e3bba35f5e\"}', '6af7a429bac04d29a37e0aec50f16bed');
INSERT INTO `ps_promotionconditions` VALUES ('3a1be99f-8aea-c1fc-2751-1d8771d74152', NULL, '3a1be99e-81a7-bc02-9f7e-a19ad01663a4', 'GOODS_IDS', '{\"goods\":\"3a1be8f5-77c6-abfd-9f34-bbbc0419b30f,3a1be90a-804f-3ea5-ad0b-113f581c0d93\",\"nums\":1}', '5f70085a44834f07b5342f59132e4cea');
INSERT INTO `ps_promotionconditions` VALUES ('3a1be9a2-e48b-1ee2-8453-817b2d1d3e09', NULL, '3a1be9a2-21a7-0ec0-3f7c-3b9d0b2719bd', 'GOODS_IDS', '{\"goodsId\":\"3a1be913-be5c-9884-7a18-17fb9b8a1860\",\"nums\":1}', '238aa30d61d1470abc40a32e3d74338d');

-- ----------------------------
-- Table structure for ps_promotionrecords
-- ----------------------------
DROP TABLE IF EXISTS `ps_promotionrecords`;
CREATE TABLE `ps_promotionrecords`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `PromotionId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GoodId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `ProductId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `OrderId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Type` int NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ps_promotionrecords
-- ----------------------------

-- ----------------------------
-- Table structure for ps_promotionresults
-- ----------------------------
DROP TABLE IF EXISTS `ps_promotionresults`;
CREATE TABLE `ps_promotionresults`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `PromotionId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Parameters` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Ps_PromotionResults_PromotionId`(`PromotionId`) USING BTREE,
  CONSTRAINT `FK_Ps_PromotionResults_Ps_Promotions_PromotionId` FOREIGN KEY (`PromotionId`) REFERENCES `ps_promotions` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ps_promotionresults
-- ----------------------------
INSERT INTO `ps_promotionresults` VALUES ('3a1be99b-7d80-0914-00eb-7b1827b172a0', NULL, '3a1be99a-b722-0990-e21a-34ab69db3e22', 'GOODS_DISCOUNT', '{\"discount\":\"7\"}', 'a831834da4cf41cb9fa4d37e3f173a15');
INSERT INTO `ps_promotionresults` VALUES ('3a1be99f-8aeb-4c94-392b-bb6fc0f7ccf6', NULL, '3a1be99e-81a7-bc02-9f7e-a19ad01663a4', 'GOODS_DISCOUNT', '{\"discount\":\"8\"}', '8c04f10de5ab45d1bb6639768857b59c');
INSERT INTO `ps_promotionresults` VALUES ('3a1be9a2-e48b-0218-feac-7234a5f64ab0', NULL, '3a1be9a2-21a7-0ec0-3f7c-3b9d0b2719bd', 'GOODS_DISCOUNT', '{\"discount\":\"7\"}', 'd6a3ce83261146aeaad7a059b1bf48f5');

-- ----------------------------
-- Table structure for ps_promotions
-- ----------------------------
DROP TABLE IF EXISTS `ps_promotions`;
CREATE TABLE `ps_promotions`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Name` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Type` int NOT NULL,
  `Weight` int NOT NULL,
  `Parameters` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `MaxNums` int NOT NULL,
  `MaxGoodNums` int NOT NULL,
  `MaxRecevieNums` int NOT NULL,
  `StartTime` datetime(6) NOT NULL,
  `EndTime` datetime(6) NOT NULL,
  `IsExclusive` tinyint(1) NOT NULL,
  `IsAutoReceive` tinyint(1) NOT NULL,
  `IsEnable` tinyint(1) NOT NULL,
  `EffectiveDays` int NOT NULL,
  `EffectiveHours` int NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ps_promotions
-- ----------------------------
INSERT INTO `ps_promotions` VALUES ('3a1be99a-b722-0990-e21a-34ab69db3e22', NULL, '双11促销', 1, 100, NULL, 0, 0, 0, '2025-08-23 00:00:00.000000', '2025-11-12 00:00:00.000000', 1, 0, 1, 0, 0, '{}', '10de10bc37f4467cab91f0affe8dc4d4', '2025-08-23 22:55:58.846904', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `ps_promotions` VALUES ('3a1be99e-81a7-bc02-9f7e-a19ad01663a4', NULL, '苹果优惠劵', 2, 100, NULL, 1, 0, 100, '2025-08-25 00:00:00.000000', '2025-10-20 00:00:00.000000', 1, 1, 1, 20, 5, '{}', '5da0c783115244a5ab15c9c2cbd88900', '2025-08-23 23:00:07.212376', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `ps_promotions` VALUES ('3a1be9a2-21a7-0ec0-3f7c-3b9d0b2719bd', NULL, '速来！小米蕉大甩卖！', 4, 100, '{\"goodsId\":\"3a1be913-be5c-9884-7a18-17fb9b8a1860\",\"nums\":1}', 1, 10, 100, '2025-08-20 00:00:00.000000', '2025-11-25 00:00:00.000000', 0, 1, 1, 0, 0, '{}', 'ec93f75b1c384a69b5039a1df69cb82a', '2025-08-23 23:04:04.779605', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 23:04:54.663750', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);

SET FOREIGN_KEY_CHECKS = 1;
