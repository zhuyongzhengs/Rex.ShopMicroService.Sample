/*
 Navicat Premium Data Transfer

 Source Server         : Local_MySQL
 Source Server Type    : MySQL
 Source Server Version : 80036
 Source Host           : localhost:3306
 Source Schema         : rex.shop.goodservice

 Target Server Type    : MySQL
 Target Server Version : 80036
 File Encoding         : 65001

 Date: 23/08/2025 23:47:40
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
INSERT INTO `cap.received` VALUES (9027127011266076673, 'v1', 'Order.Deduction.Product.Stock', 'Rex.GoodService.HttpApi.Host.v1', '{\"Headers\":{\"cap-callback-name\":null,\"cap-corr-id\":\"9027127011262185476\",\"cap-corr-seq\":\"0\",\"cap-msg-id\":\"9027127011262185476\",\"cap-msg-name\":\"Order.Deduction.Product.Stock\",\"cap-msg-type\":\"OrderDeductionProductStockEto\",\"cap-senttime\":\"2025/8/23 23:36:36 \\u002B08:00\",\"cap-msg-group\":\"Rex.GoodService.HttpApi.Host.v1\",\"cap-exec-instance-id\":\"LAPTOP-A4AM01CM\"},\"Value\":{\"StockDatas\":[{\"ProductId\":\"3a1be90a-8089-d7dc-9e7b-99688c581768\",\"Nums\":2,\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}],\"Id\":\"3a1be9bf-e6a1-4c82-399e-d821cc554718\"}}', 0, '2025-08-23 23:36:36', '2025-08-24 23:36:37', 'Succeeded');
INSERT INTO `cap.received` VALUES (9027127011266076674, 'v1', 'Order.Deduction.Product.Stock', 'Rex.GoodService.HttpApi.Host.v1', '{\"Headers\":{\"cap-callback-name\":null,\"cap-corr-id\":\"9027127011262185480\",\"cap-corr-seq\":\"0\",\"cap-msg-id\":\"9027127011262185480\",\"cap-msg-name\":\"Order.Deduction.Product.Stock\",\"cap-msg-type\":\"OrderDeductionProductStockEto\",\"cap-senttime\":\"2025/8/23 23:38:09 \\u002B08:00\",\"cap-msg-group\":\"Rex.GoodService.HttpApi.Host.v1\",\"cap-exec-instance-id\":\"LAPTOP-A4AM01CM\"},\"Value\":{\"StockDatas\":[{\"ProductId\":\"3a1be8f5-77db-fbc0-6974-ea8afb90d743\",\"Nums\":1,\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}],\"Id\":\"3a1be9c1-55a8-b456-8136-5a91b44c3590\"}}', 0, '2025-08-23 23:38:10', '2025-08-24 23:38:10', 'Succeeded');
INSERT INTO `cap.received` VALUES (9027127011266076675, 'v1', 'Change.Product.Stock', 'Rex.GoodService.HttpApi.Host.v1', '{\"Headers\":{\"cap-callback-name\":null,\"cap-corr-id\":\"9027127011262185482\",\"cap-corr-seq\":\"0\",\"cap-msg-id\":\"9027127011262185482\",\"cap-msg-name\":\"Change.Product.Stock\",\"cap-msg-type\":\"ChangeStockEto\",\"cap-senttime\":\"2025/8/23 23:39:20 \\u002B08:00\",\"cap-msg-group\":\"Rex.GoodService.HttpApi.Host.v1\",\"cap-exec-instance-id\":\"LAPTOP-A4AM01CM\"},\"Value\":{\"ProductId\":\"3a1be90a-8089-d7dc-9e7b-99688c581768\",\"ChangeStockType\":2,\"Nums\":2,\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:39:21', '2025-08-24 23:39:21', 'Succeeded');
INSERT INTO `cap.received` VALUES (9027127011266076676, 'v1', 'Order.Deduction.Product.Stock', 'Rex.GoodService.HttpApi.Host.v1', '{\"Headers\":{\"cap-callback-name\":null,\"cap-corr-id\":\"9027127011262185484\",\"cap-corr-seq\":\"0\",\"cap-msg-id\":\"9027127011262185484\",\"cap-msg-name\":\"Order.Deduction.Product.Stock\",\"cap-msg-type\":\"OrderDeductionProductStockEto\",\"cap-senttime\":\"2025/8/23 23:40:10 \\u002B08:00\",\"cap-msg-group\":\"Rex.GoodService.HttpApi.Host.v1\",\"cap-exec-instance-id\":\"LAPTOP-A4AM01CM\"},\"Value\":{\"StockDatas\":[{\"ProductId\":\"3a1be916-1f3f-89c7-7555-9f268a96f7ed\",\"Nums\":2,\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}],\"Id\":\"3a1be9c3-2d5b-1b7a-8488-ff615cc92308\"}}', 0, '2025-08-23 23:40:11', '2025-08-24 23:40:11', 'Succeeded');

-- ----------------------------
-- Table structure for gd_areas
-- ----------------------------
DROP TABLE IF EXISTS `gd_areas`;
CREATE TABLE `gd_areas`  (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `ParentId` bigint NULL DEFAULT 0,
  `Depth` int NULL DEFAULT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PostalCode` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Sort` int NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Gd_Areas_ParentId`(`ParentId`) USING BTREE,
  CONSTRAINT `FK_Gd_Areas_Gd_Areas_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `gd_areas` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 920119 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_areas
-- ----------------------------
INSERT INTO `gd_areas` VALUES (110000, NULL, 0, 1, '北京市', '100000', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110100, NULL, 110000, 2, '北京市', '100000', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110101, NULL, 110100, 3, '东城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110102, NULL, 110100, 3, '西城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110105, NULL, 110100, 3, '朝阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110106, NULL, 110100, 3, '丰台区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110107, NULL, 110100, 3, '石景山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110108, NULL, 110100, 3, '海淀区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110109, NULL, 110100, 3, '门头沟区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110111, NULL, 110100, 3, '房山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110112, NULL, 110100, 3, '通州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110113, NULL, 110100, 3, '顺义区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110114, NULL, 110100, 3, '昌平区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110115, NULL, 110100, 3, '大兴区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110116, NULL, 110100, 3, '怀柔区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110117, NULL, 110100, 3, '平谷区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110118, NULL, 110100, 3, '密云区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (110119, NULL, 110100, 3, '延庆区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120000, NULL, 0, 1, '天津市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120100, NULL, 120000, 2, '天津市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120101, NULL, 120100, 3, '和平区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120102, NULL, 120100, 3, '河东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120103, NULL, 120100, 3, '河西区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120104, NULL, 120100, 3, '南开区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120105, NULL, 120100, 3, '河北区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120106, NULL, 120100, 3, '红桥区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120110, NULL, 120100, 3, '东丽区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120111, NULL, 120100, 3, '西青区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120112, NULL, 120100, 3, '津南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120113, NULL, 120100, 3, '北辰区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120114, NULL, 120100, 3, '武清区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120115, NULL, 120100, 3, '宝坻区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120116, NULL, 120100, 3, '滨海新区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120117, NULL, 120100, 3, '宁河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120118, NULL, 120100, 3, '静海区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (120119, NULL, 120100, 3, '蓟州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130000, NULL, 0, 1, '河北省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130100, NULL, 130000, 2, '石家庄市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130101, NULL, 130100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130102, NULL, 130100, 3, '长安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130104, NULL, 130100, 3, '桥西区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130105, NULL, 130100, 3, '新华区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130107, NULL, 130100, 3, '井陉矿区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130108, NULL, 130100, 3, '裕华区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130109, NULL, 130100, 3, '藁城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130110, NULL, 130100, 3, '鹿泉区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130111, NULL, 130100, 3, '栾城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130121, NULL, 130100, 3, '井陉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130123, NULL, 130100, 3, '正定县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130125, NULL, 130100, 3, '行唐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130126, NULL, 130100, 3, '灵寿县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130127, NULL, 130100, 3, '高邑县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130128, NULL, 130100, 3, '深泽县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130129, NULL, 130100, 3, '赞皇县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130130, NULL, 130100, 3, '无极县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130131, NULL, 130100, 3, '平山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130132, NULL, 130100, 3, '元氏县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130133, NULL, 130100, 3, '赵县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130183, NULL, 130100, 3, '晋州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130184, NULL, 130100, 3, '新乐市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130200, NULL, 130000, 2, '唐山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130201, NULL, 130200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130202, NULL, 130200, 3, '路南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130203, NULL, 130200, 3, '路北区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130204, NULL, 130200, 3, '古冶区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130205, NULL, 130200, 3, '开平区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130207, NULL, 130200, 3, '丰南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130208, NULL, 130200, 3, '丰润区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130209, NULL, 130200, 3, '曹妃甸区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130223, NULL, 130200, 3, '滦县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130224, NULL, 130200, 3, '滦南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130225, NULL, 130200, 3, '乐亭县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130227, NULL, 130200, 3, '迁西县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130229, NULL, 130200, 3, '玉田县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130281, NULL, 130200, 3, '遵化市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130283, NULL, 130200, 3, '迁安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130300, NULL, 130000, 2, '秦皇岛市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130301, NULL, 130300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130302, NULL, 130300, 3, '海港区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130303, NULL, 130300, 3, '山海关区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130304, NULL, 130300, 3, '北戴河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130306, NULL, 130300, 3, '抚宁区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130321, NULL, 130300, 3, '青龙满族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130322, NULL, 130300, 3, '昌黎县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130324, NULL, 130300, 3, '卢龙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130400, NULL, 130000, 2, '邯郸市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130401, NULL, 130400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130402, NULL, 130400, 3, '邯山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130403, NULL, 130400, 3, '丛台区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130404, NULL, 130400, 3, '复兴区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130406, NULL, 130400, 3, '峰峰矿区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130421, NULL, 130400, 3, '邯郸县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130423, NULL, 130400, 3, '临漳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130424, NULL, 130400, 3, '成安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130425, NULL, 130400, 3, '大名县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130426, NULL, 130400, 3, '涉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130427, NULL, 130400, 3, '磁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130428, NULL, 130400, 3, '肥乡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130429, NULL, 130400, 3, '永年县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130430, NULL, 130400, 3, '邱县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130431, NULL, 130400, 3, '鸡泽县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130432, NULL, 130400, 3, '广平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130433, NULL, 130400, 3, '馆陶县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130434, NULL, 130400, 3, '魏县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130435, NULL, 130400, 3, '曲周县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130481, NULL, 130400, 3, '武安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130500, NULL, 130000, 2, '邢台市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130501, NULL, 130500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130502, NULL, 130500, 3, '桥东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130503, NULL, 130500, 3, '桥西区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130521, NULL, 130500, 3, '邢台县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130522, NULL, 130500, 3, '临城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130523, NULL, 130500, 3, '内丘县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130524, NULL, 130500, 3, '柏乡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130525, NULL, 130500, 3, '隆尧县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130526, NULL, 130500, 3, '任县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130527, NULL, 130500, 3, '南和县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130528, NULL, 130500, 3, '宁晋县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130529, NULL, 130500, 3, '巨鹿县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130530, NULL, 130500, 3, '新河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130531, NULL, 130500, 3, '广宗县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130532, NULL, 130500, 3, '平乡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130533, NULL, 130500, 3, '威县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130534, NULL, 130500, 3, '清河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130535, NULL, 130500, 3, '临西县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130581, NULL, 130500, 3, '南宫市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130582, NULL, 130500, 3, '沙河市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130600, NULL, 130000, 2, '保定市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130601, NULL, 130600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130602, NULL, 130600, 3, '竞秀区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130606, NULL, 130600, 3, '莲池区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130607, NULL, 130600, 3, '满城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130608, NULL, 130600, 3, '清苑区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130609, NULL, 130600, 3, '徐水区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130623, NULL, 130600, 3, '涞水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130624, NULL, 130600, 3, '阜平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130626, NULL, 130600, 3, '定兴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130627, NULL, 130600, 3, '唐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130628, NULL, 130600, 3, '高阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130629, NULL, 130600, 3, '容城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130630, NULL, 130600, 3, '涞源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130631, NULL, 130600, 3, '望都县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130632, NULL, 130600, 3, '安新县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130633, NULL, 130600, 3, '易县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130634, NULL, 130600, 3, '曲阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130635, NULL, 130600, 3, '蠡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130636, NULL, 130600, 3, '顺平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130637, NULL, 130600, 3, '博野县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130638, NULL, 130600, 3, '雄县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130681, NULL, 130600, 3, '涿州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130683, NULL, 130600, 3, '安国市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130684, NULL, 130600, 3, '高碑店市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130700, NULL, 130000, 2, '张家口市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130701, NULL, 130700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130702, NULL, 130700, 3, '桥东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130703, NULL, 130700, 3, '桥西区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130705, NULL, 130700, 3, '宣化区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130706, NULL, 130700, 3, '下花园区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130708, NULL, 130700, 3, '万全区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130709, NULL, 130700, 3, '崇礼区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130722, NULL, 130700, 3, '张北县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130723, NULL, 130700, 3, '康保县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130724, NULL, 130700, 3, '沽源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130725, NULL, 130700, 3, '尚义县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130726, NULL, 130700, 3, '蔚县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130727, NULL, 130700, 3, '阳原县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130728, NULL, 130700, 3, '怀安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130730, NULL, 130700, 3, '怀来县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130731, NULL, 130700, 3, '涿鹿县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130732, NULL, 130700, 3, '赤城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130800, NULL, 130000, 2, '承德市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130801, NULL, 130800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130802, NULL, 130800, 3, '双桥区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130803, NULL, 130800, 3, '双滦区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130804, NULL, 130800, 3, '鹰手营子矿区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130821, NULL, 130800, 3, '承德县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130822, NULL, 130800, 3, '兴隆县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130823, NULL, 130800, 3, '平泉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130824, NULL, 130800, 3, '滦平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130825, NULL, 130800, 3, '隆化县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130826, NULL, 130800, 3, '丰宁满族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130827, NULL, 130800, 3, '宽城满族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130828, NULL, 130800, 3, '围场满族蒙古族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130900, NULL, 130000, 2, '沧州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130901, NULL, 130900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130902, NULL, 130900, 3, '新华区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130903, NULL, 130900, 3, '运河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130921, NULL, 130900, 3, '沧县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130922, NULL, 130900, 3, '青县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130923, NULL, 130900, 3, '东光县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130924, NULL, 130900, 3, '海兴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130925, NULL, 130900, 3, '盐山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130926, NULL, 130900, 3, '肃宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130927, NULL, 130900, 3, '南皮县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130928, NULL, 130900, 3, '吴桥县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130929, NULL, 130900, 3, '献县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130930, NULL, 130900, 3, '孟村回族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130981, NULL, 130900, 3, '泊头市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130982, NULL, 130900, 3, '任丘市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130983, NULL, 130900, 3, '黄骅市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (130984, NULL, 130900, 3, '河间市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131000, NULL, 130000, 2, '廊坊市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131001, NULL, 131000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131002, NULL, 131000, 3, '安次区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131003, NULL, 131000, 3, '广阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131022, NULL, 131000, 3, '固安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131023, NULL, 131000, 3, '永清县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131024, NULL, 131000, 3, '香河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131025, NULL, 131000, 3, '大城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131026, NULL, 131000, 3, '文安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131028, NULL, 131000, 3, '大厂回族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131081, NULL, 131000, 3, '霸州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131082, NULL, 131000, 3, '三河市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131100, NULL, 130000, 2, '衡水市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131101, NULL, 131100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131102, NULL, 131100, 3, '桃城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131103, NULL, 131100, 3, '冀州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131121, NULL, 131100, 3, '枣强县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131122, NULL, 131100, 3, '武邑县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131123, NULL, 131100, 3, '武强县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131124, NULL, 131100, 3, '饶阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131125, NULL, 131100, 3, '安平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131126, NULL, 131100, 3, '故城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131127, NULL, 131100, 3, '景县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131128, NULL, 131100, 3, '阜城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (131182, NULL, 131100, 3, '深州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (139000, NULL, 130000, 2, '省直辖县级行政区划', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (139001, NULL, 139000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (139002, NULL, 139000, 3, '辛集市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140000, NULL, 0, 1, '山西省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140100, NULL, 140000, 2, '太原市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140101, NULL, 140100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140105, NULL, 140100, 3, '小店区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140106, NULL, 140100, 3, '迎泽区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140107, NULL, 140100, 3, '杏花岭区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140108, NULL, 140100, 3, '尖草坪区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140109, NULL, 140100, 3, '万柏林区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140110, NULL, 140100, 3, '晋源区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140121, NULL, 140100, 3, '清徐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140122, NULL, 140100, 3, '阳曲县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140123, NULL, 140100, 3, '娄烦县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140181, NULL, 140100, 3, '古交市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140200, NULL, 140000, 2, '大同市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140201, NULL, 140200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140202, NULL, 140200, 3, '城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140203, NULL, 140200, 3, '矿区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140211, NULL, 140200, 3, '南郊区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140212, NULL, 140200, 3, '新荣区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140221, NULL, 140200, 3, '阳高县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140222, NULL, 140200, 3, '天镇县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140223, NULL, 140200, 3, '广灵县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140224, NULL, 140200, 3, '灵丘县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140225, NULL, 140200, 3, '浑源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140226, NULL, 140200, 3, '左云县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140227, NULL, 140200, 3, '大同县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140300, NULL, 140000, 2, '阳泉市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140301, NULL, 140300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140302, NULL, 140300, 3, '城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140303, NULL, 140300, 3, '矿区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140311, NULL, 140300, 3, '郊区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140321, NULL, 140300, 3, '平定县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140322, NULL, 140300, 3, '盂县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140400, NULL, 140000, 2, '长治市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140401, NULL, 140400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140402, NULL, 140400, 3, '城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140411, NULL, 140400, 3, '郊区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140421, NULL, 140400, 3, '长治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140423, NULL, 140400, 3, '襄垣县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140424, NULL, 140400, 3, '屯留县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140425, NULL, 140400, 3, '平顺县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140426, NULL, 140400, 3, '黎城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140427, NULL, 140400, 3, '壶关县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140428, NULL, 140400, 3, '长子县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140429, NULL, 140400, 3, '武乡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140430, NULL, 140400, 3, '沁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140431, NULL, 140400, 3, '沁源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140481, NULL, 140400, 3, '潞城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140500, NULL, 140000, 2, '晋城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140501, NULL, 140500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140502, NULL, 140500, 3, '城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140521, NULL, 140500, 3, '沁水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140522, NULL, 140500, 3, '阳城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140524, NULL, 140500, 3, '陵川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140525, NULL, 140500, 3, '泽州县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140581, NULL, 140500, 3, '高平市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140600, NULL, 140000, 2, '朔州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140601, NULL, 140600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140602, NULL, 140600, 3, '朔城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140603, NULL, 140600, 3, '平鲁区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140621, NULL, 140600, 3, '山阴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140622, NULL, 140600, 3, '应县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140623, NULL, 140600, 3, '右玉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140624, NULL, 140600, 3, '怀仁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140700, NULL, 140000, 2, '晋中市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140701, NULL, 140700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140702, NULL, 140700, 3, '榆次区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140721, NULL, 140700, 3, '榆社县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140722, NULL, 140700, 3, '左权县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140723, NULL, 140700, 3, '和顺县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140724, NULL, 140700, 3, '昔阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140725, NULL, 140700, 3, '寿阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140726, NULL, 140700, 3, '太谷县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140727, NULL, 140700, 3, '祁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140728, NULL, 140700, 3, '平遥县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140729, NULL, 140700, 3, '灵石县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140781, NULL, 140700, 3, '介休市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140800, NULL, 140000, 2, '运城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140801, NULL, 140800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140802, NULL, 140800, 3, '盐湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140821, NULL, 140800, 3, '临猗县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140822, NULL, 140800, 3, '万荣县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140823, NULL, 140800, 3, '闻喜县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140824, NULL, 140800, 3, '稷山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140825, NULL, 140800, 3, '新绛县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140826, NULL, 140800, 3, '绛县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140827, NULL, 140800, 3, '垣曲县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140828, NULL, 140800, 3, '夏县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140829, NULL, 140800, 3, '平陆县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140830, NULL, 140800, 3, '芮城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140881, NULL, 140800, 3, '永济市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140882, NULL, 140800, 3, '河津市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140900, NULL, 140000, 2, '忻州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140901, NULL, 140900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140902, NULL, 140900, 3, '忻府区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140921, NULL, 140900, 3, '定襄县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140922, NULL, 140900, 3, '五台县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140923, NULL, 140900, 3, '代县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140924, NULL, 140900, 3, '繁峙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140925, NULL, 140900, 3, '宁武县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140926, NULL, 140900, 3, '静乐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140927, NULL, 140900, 3, '神池县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140928, NULL, 140900, 3, '五寨县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140929, NULL, 140900, 3, '岢岚县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140930, NULL, 140900, 3, '河曲县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140931, NULL, 140900, 3, '保德县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140932, NULL, 140900, 3, '偏关县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (140981, NULL, 140900, 3, '原平市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141000, NULL, 140000, 2, '临汾市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141001, NULL, 141000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141002, NULL, 141000, 3, '尧都区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141021, NULL, 141000, 3, '曲沃县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141022, NULL, 141000, 3, '翼城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141023, NULL, 141000, 3, '襄汾县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141024, NULL, 141000, 3, '洪洞县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141025, NULL, 141000, 3, '古县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141026, NULL, 141000, 3, '安泽县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141027, NULL, 141000, 3, '浮山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141028, NULL, 141000, 3, '吉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141029, NULL, 141000, 3, '乡宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141030, NULL, 141000, 3, '大宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141031, NULL, 141000, 3, '隰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141032, NULL, 141000, 3, '永和县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141033, NULL, 141000, 3, '蒲县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141034, NULL, 141000, 3, '汾西县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141081, NULL, 141000, 3, '侯马市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141082, NULL, 141000, 3, '霍州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141100, NULL, 140000, 2, '吕梁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141101, NULL, 141100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141102, NULL, 141100, 3, '离石区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141121, NULL, 141100, 3, '文水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141122, NULL, 141100, 3, '交城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141123, NULL, 141100, 3, '兴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141124, NULL, 141100, 3, '临县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141125, NULL, 141100, 3, '柳林县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141126, NULL, 141100, 3, '石楼县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141127, NULL, 141100, 3, '岚县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141128, NULL, 141100, 3, '方山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141129, NULL, 141100, 3, '中阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141130, NULL, 141100, 3, '交口县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141181, NULL, 141100, 3, '孝义市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (141182, NULL, 141100, 3, '汾阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150000, NULL, 0, 1, '内蒙古自治区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150100, NULL, 150000, 2, '呼和浩特市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150101, NULL, 150100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150102, NULL, 150100, 3, '新城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150103, NULL, 150100, 3, '回民区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150104, NULL, 150100, 3, '玉泉区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150105, NULL, 150100, 3, '赛罕区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150121, NULL, 150100, 3, '土默特左旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150122, NULL, 150100, 3, '托克托县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150123, NULL, 150100, 3, '和林格尔县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150124, NULL, 150100, 3, '清水河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150125, NULL, 150100, 3, '武川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150200, NULL, 150000, 2, '包头市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150201, NULL, 150200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150202, NULL, 150200, 3, '东河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150203, NULL, 150200, 3, '昆都仑区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150204, NULL, 150200, 3, '青山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150205, NULL, 150200, 3, '石拐区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150206, NULL, 150200, 3, '白云鄂博矿区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150207, NULL, 150200, 3, '九原区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150221, NULL, 150200, 3, '土默特右旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150222, NULL, 150200, 3, '固阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150223, NULL, 150200, 3, '达尔罕茂明安联合旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150300, NULL, 150000, 2, '乌海市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150301, NULL, 150300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150302, NULL, 150300, 3, '海勃湾区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150303, NULL, 150300, 3, '海南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150304, NULL, 150300, 3, '乌达区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150400, NULL, 150000, 2, '赤峰市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150401, NULL, 150400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150402, NULL, 150400, 3, '红山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150403, NULL, 150400, 3, '元宝山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150404, NULL, 150400, 3, '松山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150421, NULL, 150400, 3, '阿鲁科尔沁旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150422, NULL, 150400, 3, '巴林左旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150423, NULL, 150400, 3, '巴林右旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150424, NULL, 150400, 3, '林西县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150425, NULL, 150400, 3, '克什克腾旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150426, NULL, 150400, 3, '翁牛特旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150428, NULL, 150400, 3, '喀喇沁旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150429, NULL, 150400, 3, '宁城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150430, NULL, 150400, 3, '敖汉旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150500, NULL, 150000, 2, '通辽市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150501, NULL, 150500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150502, NULL, 150500, 3, '科尔沁区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150521, NULL, 150500, 3, '科尔沁左翼中旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150522, NULL, 150500, 3, '科尔沁左翼后旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150523, NULL, 150500, 3, '开鲁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150524, NULL, 150500, 3, '库伦旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150525, NULL, 150500, 3, '奈曼旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150526, NULL, 150500, 3, '扎鲁特旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150581, NULL, 150500, 3, '霍林郭勒市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150600, NULL, 150000, 2, '鄂尔多斯市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150601, NULL, 150600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150602, NULL, 150600, 3, '东胜区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150603, NULL, 150600, 3, '康巴什区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150621, NULL, 150600, 3, '达拉特旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150622, NULL, 150600, 3, '准格尔旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150623, NULL, 150600, 3, '鄂托克前旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150624, NULL, 150600, 3, '鄂托克旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150625, NULL, 150600, 3, '杭锦旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150626, NULL, 150600, 3, '乌审旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150627, NULL, 150600, 3, '伊金霍洛旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150700, NULL, 150000, 2, '呼伦贝尔市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150701, NULL, 150700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150702, NULL, 150700, 3, '海拉尔区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150703, NULL, 150700, 3, '扎赉诺尔区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150721, NULL, 150700, 3, '阿荣旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150722, NULL, 150700, 3, '莫力达瓦达斡尔族自治旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150723, NULL, 150700, 3, '鄂伦春自治旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150724, NULL, 150700, 3, '鄂温克族自治旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150725, NULL, 150700, 3, '陈巴尔虎旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150726, NULL, 150700, 3, '新巴尔虎左旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150727, NULL, 150700, 3, '新巴尔虎右旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150781, NULL, 150700, 3, '满洲里市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150782, NULL, 150700, 3, '牙克石市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150783, NULL, 150700, 3, '扎兰屯市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150784, NULL, 150700, 3, '额尔古纳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150785, NULL, 150700, 3, '根河市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150800, NULL, 150000, 2, '巴彦淖尔市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150801, NULL, 150800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150802, NULL, 150800, 3, '临河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150821, NULL, 150800, 3, '五原县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150822, NULL, 150800, 3, '磴口县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150823, NULL, 150800, 3, '乌拉特前旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150824, NULL, 150800, 3, '乌拉特中旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150825, NULL, 150800, 3, '乌拉特后旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150826, NULL, 150800, 3, '杭锦后旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150900, NULL, 150000, 2, '乌兰察布市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150901, NULL, 150900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150902, NULL, 150900, 3, '集宁区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150921, NULL, 150900, 3, '卓资县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150922, NULL, 150900, 3, '化德县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150923, NULL, 150900, 3, '商都县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150924, NULL, 150900, 3, '兴和县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150925, NULL, 150900, 3, '凉城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150926, NULL, 150900, 3, '察哈尔右翼前旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150927, NULL, 150900, 3, '察哈尔右翼中旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150928, NULL, 150900, 3, '察哈尔右翼后旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150929, NULL, 150900, 3, '四子王旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (150981, NULL, 150900, 3, '丰镇市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152200, NULL, 150000, 2, '兴安盟', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152201, NULL, 152200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152202, NULL, 152200, 3, '阿尔山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152221, NULL, 152200, 3, '科尔沁右翼前旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152222, NULL, 152200, 3, '科尔沁右翼中旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152223, NULL, 152200, 3, '扎赉特旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152224, NULL, 152200, 3, '突泉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152500, NULL, 150000, 2, '锡林郭勒盟', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152501, NULL, 152500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152502, NULL, 152500, 3, '锡林浩特市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152522, NULL, 152500, 3, '阿巴嘎旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152523, NULL, 152500, 3, '苏尼特左旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152524, NULL, 152500, 3, '苏尼特右旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152525, NULL, 152500, 3, '东乌珠穆沁旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152526, NULL, 152500, 3, '西乌珠穆沁旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152527, NULL, 152500, 3, '太仆寺旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152528, NULL, 152500, 3, '镶黄旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152529, NULL, 152500, 3, '正镶白旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152530, NULL, 152500, 3, '正蓝旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152531, NULL, 152500, 3, '多伦县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152900, NULL, 150000, 2, '阿拉善盟', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152921, NULL, 152900, 3, '阿拉善左旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152922, NULL, 152900, 3, '阿拉善右旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (152923, NULL, 152900, 3, '额济纳旗', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210000, NULL, 0, 1, '辽宁省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210100, NULL, 210000, 2, '沈阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210101, NULL, 210100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210102, NULL, 210100, 3, '和平区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210103, NULL, 210100, 3, '沈河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210104, NULL, 210100, 3, '大东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210105, NULL, 210100, 3, '皇姑区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210106, NULL, 210100, 3, '铁西区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210111, NULL, 210100, 3, '苏家屯区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210112, NULL, 210100, 3, '浑南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210113, NULL, 210100, 3, '沈北新区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210114, NULL, 210100, 3, '于洪区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210115, NULL, 210100, 3, '辽中区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210123, NULL, 210100, 3, '康平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210124, NULL, 210100, 3, '法库县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210181, NULL, 210100, 3, '新民市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210200, NULL, 210000, 2, '大连市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210201, NULL, 210200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210202, NULL, 210200, 3, '中山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210203, NULL, 210200, 3, '西岗区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210204, NULL, 210200, 3, '沙河口区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210211, NULL, 210200, 3, '甘井子区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210212, NULL, 210200, 3, '旅顺口区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210213, NULL, 210200, 3, '金州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210214, NULL, 210200, 3, '普兰店区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210224, NULL, 210200, 3, '长海县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210281, NULL, 210200, 3, '瓦房店市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210283, NULL, 210200, 3, '庄河市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210300, NULL, 210000, 2, '鞍山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210301, NULL, 210300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210302, NULL, 210300, 3, '铁东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210303, NULL, 210300, 3, '铁西区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210304, NULL, 210300, 3, '立山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210311, NULL, 210300, 3, '千山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210321, NULL, 210300, 3, '台安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210323, NULL, 210300, 3, '岫岩满族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210381, NULL, 210300, 3, '海城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210400, NULL, 210000, 2, '抚顺市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210401, NULL, 210400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210402, NULL, 210400, 3, '新抚区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210403, NULL, 210400, 3, '东洲区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210404, NULL, 210400, 3, '望花区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210411, NULL, 210400, 3, '顺城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210421, NULL, 210400, 3, '抚顺县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210422, NULL, 210400, 3, '新宾满族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210423, NULL, 210400, 3, '清原满族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210500, NULL, 210000, 2, '本溪市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210501, NULL, 210500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210502, NULL, 210500, 3, '平山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210503, NULL, 210500, 3, '溪湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210504, NULL, 210500, 3, '明山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210505, NULL, 210500, 3, '南芬区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210521, NULL, 210500, 3, '本溪满族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210522, NULL, 210500, 3, '桓仁满族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210600, NULL, 210000, 2, '丹东市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210601, NULL, 210600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210602, NULL, 210600, 3, '元宝区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210603, NULL, 210600, 3, '振兴区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210604, NULL, 210600, 3, '振安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210624, NULL, 210600, 3, '宽甸满族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210681, NULL, 210600, 3, '东港市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210682, NULL, 210600, 3, '凤城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210700, NULL, 210000, 2, '锦州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210701, NULL, 210700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210702, NULL, 210700, 3, '古塔区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210703, NULL, 210700, 3, '凌河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210711, NULL, 210700, 3, '太和区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210726, NULL, 210700, 3, '黑山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210727, NULL, 210700, 3, '义县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210781, NULL, 210700, 3, '凌海市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210782, NULL, 210700, 3, '北镇市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210800, NULL, 210000, 2, '营口市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210801, NULL, 210800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210802, NULL, 210800, 3, '站前区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210803, NULL, 210800, 3, '西市区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210804, NULL, 210800, 3, '鲅鱼圈区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210811, NULL, 210800, 3, '老边区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210881, NULL, 210800, 3, '盖州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210882, NULL, 210800, 3, '大石桥市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210900, NULL, 210000, 2, '阜新市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210901, NULL, 210900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210902, NULL, 210900, 3, '海州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210903, NULL, 210900, 3, '新邱区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210904, NULL, 210900, 3, '太平区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210905, NULL, 210900, 3, '清河门区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210911, NULL, 210900, 3, '细河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210921, NULL, 210900, 3, '阜新蒙古族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (210922, NULL, 210900, 3, '彰武县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211000, NULL, 210000, 2, '辽阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211001, NULL, 211000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211002, NULL, 211000, 3, '白塔区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211003, NULL, 211000, 3, '文圣区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211004, NULL, 211000, 3, '宏伟区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211005, NULL, 211000, 3, '弓长岭区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211011, NULL, 211000, 3, '太子河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211021, NULL, 211000, 3, '辽阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211081, NULL, 211000, 3, '灯塔市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211100, NULL, 210000, 2, '盘锦市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211101, NULL, 211100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211102, NULL, 211100, 3, '双台子区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211103, NULL, 211100, 3, '兴隆台区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211104, NULL, 211100, 3, '大洼区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211122, NULL, 211100, 3, '盘山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211200, NULL, 210000, 2, '铁岭市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211201, NULL, 211200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211202, NULL, 211200, 3, '银州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211204, NULL, 211200, 3, '清河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211221, NULL, 211200, 3, '铁岭县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211223, NULL, 211200, 3, '西丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211224, NULL, 211200, 3, '昌图县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211281, NULL, 211200, 3, '调兵山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211282, NULL, 211200, 3, '开原市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211300, NULL, 210000, 2, '朝阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211301, NULL, 211300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211302, NULL, 211300, 3, '双塔区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211303, NULL, 211300, 3, '龙城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211321, NULL, 211300, 3, '朝阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211322, NULL, 211300, 3, '建平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211324, NULL, 211300, 3, '喀喇沁左翼蒙古族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211381, NULL, 211300, 3, '北票市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211382, NULL, 211300, 3, '凌源市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211400, NULL, 210000, 2, '葫芦岛市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211401, NULL, 211400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211402, NULL, 211400, 3, '连山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211403, NULL, 211400, 3, '龙港区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211404, NULL, 211400, 3, '南票区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211421, NULL, 211400, 3, '绥中县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211422, NULL, 211400, 3, '建昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (211481, NULL, 211400, 3, '兴城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220000, NULL, 0, 1, '吉林省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220100, NULL, 220000, 2, '长春市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220101, NULL, 220100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220102, NULL, 220100, 3, '南关区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220103, NULL, 220100, 3, '宽城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220104, NULL, 220100, 3, '朝阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220105, NULL, 220100, 3, '二道区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220106, NULL, 220100, 3, '绿园区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220112, NULL, 220100, 3, '双阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220113, NULL, 220100, 3, '九台区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220122, NULL, 220100, 3, '农安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220182, NULL, 220100, 3, '榆树市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220183, NULL, 220100, 3, '德惠市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220200, NULL, 220000, 2, '吉林市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220201, NULL, 220200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220202, NULL, 220200, 3, '昌邑区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220203, NULL, 220200, 3, '龙潭区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220204, NULL, 220200, 3, '船营区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220211, NULL, 220200, 3, '丰满区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220221, NULL, 220200, 3, '永吉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220281, NULL, 220200, 3, '蛟河市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220282, NULL, 220200, 3, '桦甸市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220283, NULL, 220200, 3, '舒兰市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220284, NULL, 220200, 3, '磐石市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220300, NULL, 220000, 2, '四平市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220301, NULL, 220300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220302, NULL, 220300, 3, '铁西区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220303, NULL, 220300, 3, '铁东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220322, NULL, 220300, 3, '梨树县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220323, NULL, 220300, 3, '伊通满族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220381, NULL, 220300, 3, '公主岭市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220382, NULL, 220300, 3, '双辽市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220400, NULL, 220000, 2, '辽源市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220401, NULL, 220400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220402, NULL, 220400, 3, '龙山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220403, NULL, 220400, 3, '西安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220421, NULL, 220400, 3, '东丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220422, NULL, 220400, 3, '东辽县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220500, NULL, 220000, 2, '通化市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220501, NULL, 220500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220502, NULL, 220500, 3, '东昌区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220503, NULL, 220500, 3, '二道江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220521, NULL, 220500, 3, '通化县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220523, NULL, 220500, 3, '辉南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220524, NULL, 220500, 3, '柳河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220581, NULL, 220500, 3, '梅河口市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220582, NULL, 220500, 3, '集安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220600, NULL, 220000, 2, '白山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220601, NULL, 220600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220602, NULL, 220600, 3, '浑江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220605, NULL, 220600, 3, '江源区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220621, NULL, 220600, 3, '抚松县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220622, NULL, 220600, 3, '靖宇县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220623, NULL, 220600, 3, '长白朝鲜族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220681, NULL, 220600, 3, '临江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220700, NULL, 220000, 2, '松原市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220701, NULL, 220700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220702, NULL, 220700, 3, '宁江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220721, NULL, 220700, 3, '前郭尔罗斯蒙古族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220722, NULL, 220700, 3, '长岭县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220723, NULL, 220700, 3, '乾安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220781, NULL, 220700, 3, '扶余市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220800, NULL, 220000, 2, '白城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220801, NULL, 220800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220802, NULL, 220800, 3, '洮北区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220821, NULL, 220800, 3, '镇赉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220822, NULL, 220800, 3, '通榆县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220881, NULL, 220800, 3, '洮南市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (220882, NULL, 220800, 3, '大安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (222400, NULL, 220000, 2, '延边朝鲜族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (222401, NULL, 222400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (222402, NULL, 222400, 3, '图们市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (222403, NULL, 222400, 3, '敦化市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (222404, NULL, 222400, 3, '珲春市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (222405, NULL, 222400, 3, '龙井市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (222406, NULL, 222400, 3, '和龙市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (222424, NULL, 222400, 3, '汪清县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (222426, NULL, 222400, 3, '安图县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230000, NULL, 0, 1, '黑龙江省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230100, NULL, 230000, 2, '哈尔滨市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230101, NULL, 230100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230102, NULL, 230100, 3, '道里区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230103, NULL, 230100, 3, '南岗区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230104, NULL, 230100, 3, '道外区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230108, NULL, 230100, 3, '平房区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230109, NULL, 230100, 3, '松北区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230110, NULL, 230100, 3, '香坊区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230111, NULL, 230100, 3, '呼兰区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230112, NULL, 230100, 3, '阿城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230113, NULL, 230100, 3, '双城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230123, NULL, 230100, 3, '依兰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230124, NULL, 230100, 3, '方正县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230125, NULL, 230100, 3, '宾县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230126, NULL, 230100, 3, '巴彦县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230127, NULL, 230100, 3, '木兰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230128, NULL, 230100, 3, '通河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230129, NULL, 230100, 3, '延寿县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230183, NULL, 230100, 3, '尚志市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230184, NULL, 230100, 3, '五常市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230200, NULL, 230000, 2, '齐齐哈尔市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230201, NULL, 230200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230202, NULL, 230200, 3, '龙沙区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230203, NULL, 230200, 3, '建华区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230204, NULL, 230200, 3, '铁锋区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230205, NULL, 230200, 3, '昂昂溪区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230206, NULL, 230200, 3, '富拉尔基区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230207, NULL, 230200, 3, '碾子山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230208, NULL, 230200, 3, '梅里斯达斡尔族区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230221, NULL, 230200, 3, '龙江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230223, NULL, 230200, 3, '依安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230224, NULL, 230200, 3, '泰来县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230225, NULL, 230200, 3, '甘南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230227, NULL, 230200, 3, '富裕县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230229, NULL, 230200, 3, '克山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230230, NULL, 230200, 3, '克东县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230231, NULL, 230200, 3, '拜泉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230281, NULL, 230200, 3, '讷河市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230300, NULL, 230000, 2, '鸡西市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230301, NULL, 230300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230302, NULL, 230300, 3, '鸡冠区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230303, NULL, 230300, 3, '恒山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230304, NULL, 230300, 3, '滴道区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230305, NULL, 230300, 3, '梨树区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230306, NULL, 230300, 3, '城子河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230307, NULL, 230300, 3, '麻山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230321, NULL, 230300, 3, '鸡东县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230381, NULL, 230300, 3, '虎林市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230382, NULL, 230300, 3, '密山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230400, NULL, 230000, 2, '鹤岗市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230401, NULL, 230400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230402, NULL, 230400, 3, '向阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230403, NULL, 230400, 3, '工农区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230404, NULL, 230400, 3, '南山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230405, NULL, 230400, 3, '兴安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230406, NULL, 230400, 3, '东山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230407, NULL, 230400, 3, '兴山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230421, NULL, 230400, 3, '萝北县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230422, NULL, 230400, 3, '绥滨县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230500, NULL, 230000, 2, '双鸭山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230501, NULL, 230500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230502, NULL, 230500, 3, '尖山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230503, NULL, 230500, 3, '岭东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230505, NULL, 230500, 3, '四方台区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230506, NULL, 230500, 3, '宝山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230521, NULL, 230500, 3, '集贤县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230522, NULL, 230500, 3, '友谊县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230523, NULL, 230500, 3, '宝清县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230524, NULL, 230500, 3, '饶河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230600, NULL, 230000, 2, '大庆市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230601, NULL, 230600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230602, NULL, 230600, 3, '萨尔图区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230603, NULL, 230600, 3, '龙凤区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230604, NULL, 230600, 3, '让胡路区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230605, NULL, 230600, 3, '红岗区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230606, NULL, 230600, 3, '大同区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230621, NULL, 230600, 3, '肇州县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230622, NULL, 230600, 3, '肇源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230623, NULL, 230600, 3, '林甸县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230624, NULL, 230600, 3, '杜尔伯特蒙古族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230700, NULL, 230000, 2, '伊春市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230701, NULL, 230700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230702, NULL, 230700, 3, '伊春区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230703, NULL, 230700, 3, '南岔区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230704, NULL, 230700, 3, '友好区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230705, NULL, 230700, 3, '西林区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230706, NULL, 230700, 3, '翠峦区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230707, NULL, 230700, 3, '新青区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230708, NULL, 230700, 3, '美溪区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230709, NULL, 230700, 3, '金山屯区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230710, NULL, 230700, 3, '五营区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230711, NULL, 230700, 3, '乌马河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230712, NULL, 230700, 3, '汤旺河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230713, NULL, 230700, 3, '带岭区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230714, NULL, 230700, 3, '乌伊岭区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230715, NULL, 230700, 3, '红星区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230716, NULL, 230700, 3, '上甘岭区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230722, NULL, 230700, 3, '嘉荫县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230781, NULL, 230700, 3, '铁力市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230800, NULL, 230000, 2, '佳木斯市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230801, NULL, 230800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230803, NULL, 230800, 3, '向阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230804, NULL, 230800, 3, '前进区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230805, NULL, 230800, 3, '东风区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230811, NULL, 230800, 3, '郊区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230822, NULL, 230800, 3, '桦南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230826, NULL, 230800, 3, '桦川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230828, NULL, 230800, 3, '汤原县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230881, NULL, 230800, 3, '同江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230882, NULL, 230800, 3, '富锦市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230883, NULL, 230800, 3, '抚远市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230900, NULL, 230000, 2, '七台河市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230901, NULL, 230900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230902, NULL, 230900, 3, '新兴区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230903, NULL, 230900, 3, '桃山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230904, NULL, 230900, 3, '茄子河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (230921, NULL, 230900, 3, '勃利县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231000, NULL, 230000, 2, '牡丹江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231001, NULL, 231000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231002, NULL, 231000, 3, '东安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231003, NULL, 231000, 3, '阳明区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231004, NULL, 231000, 3, '爱民区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231005, NULL, 231000, 3, '西安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231025, NULL, 231000, 3, '林口县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231081, NULL, 231000, 3, '绥芬河市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231083, NULL, 231000, 3, '海林市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231084, NULL, 231000, 3, '宁安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231085, NULL, 231000, 3, '穆棱市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231086, NULL, 231000, 3, '东宁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231100, NULL, 230000, 2, '黑河市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231101, NULL, 231100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231102, NULL, 231100, 3, '爱辉区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231121, NULL, 231100, 3, '嫩江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231123, NULL, 231100, 3, '逊克县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231124, NULL, 231100, 3, '孙吴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231181, NULL, 231100, 3, '北安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231182, NULL, 231100, 3, '五大连池市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231200, NULL, 230000, 2, '绥化市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231201, NULL, 231200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231202, NULL, 231200, 3, '北林区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231221, NULL, 231200, 3, '望奎县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231222, NULL, 231200, 3, '兰西县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231223, NULL, 231200, 3, '青冈县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231224, NULL, 231200, 3, '庆安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231225, NULL, 231200, 3, '明水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231226, NULL, 231200, 3, '绥棱县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231281, NULL, 231200, 3, '安达市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231282, NULL, 231200, 3, '肇东市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (231283, NULL, 231200, 3, '海伦市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (232700, NULL, 230000, 2, '大兴安岭地区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (232721, NULL, 232700, 3, '呼玛县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (232722, NULL, 232700, 3, '塔河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (232723, NULL, 232700, 3, '漠河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310000, NULL, 0, 1, '上海市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310100, NULL, 310000, 2, '上海市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310101, NULL, 310100, 3, '黄浦区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310104, NULL, 310100, 3, '徐汇区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310105, NULL, 310100, 3, '长宁区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310106, NULL, 310100, 3, '静安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310107, NULL, 310100, 3, '普陀区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310109, NULL, 310100, 3, '虹口区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310110, NULL, 310100, 3, '杨浦区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310112, NULL, 310100, 3, '闵行区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310113, NULL, 310100, 3, '宝山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310114, NULL, 310100, 3, '嘉定区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310115, NULL, 310100, 3, '浦东新区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310116, NULL, 310100, 3, '金山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310117, NULL, 310100, 3, '松江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310118, NULL, 310100, 3, '青浦区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310120, NULL, 310100, 3, '奉贤区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (310151, NULL, 310100, 3, '崇明区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320000, NULL, 0, 1, '江苏省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320100, NULL, 320000, 2, '南京市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320101, NULL, 320100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320102, NULL, 320100, 3, '玄武区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320104, NULL, 320100, 3, '秦淮区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320105, NULL, 320100, 3, '建邺区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320106, NULL, 320100, 3, '鼓楼区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320111, NULL, 320100, 3, '浦口区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320113, NULL, 320100, 3, '栖霞区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320114, NULL, 320100, 3, '雨花台区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320115, NULL, 320100, 3, '江宁区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320116, NULL, 320100, 3, '六合区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320117, NULL, 320100, 3, '溧水区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320118, NULL, 320100, 3, '高淳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320200, NULL, 320000, 2, '无锡市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320201, NULL, 320200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320205, NULL, 320200, 3, '锡山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320206, NULL, 320200, 3, '惠山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320211, NULL, 320200, 3, '滨湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320213, NULL, 320200, 3, '梁溪区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320214, NULL, 320200, 3, '新吴区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320281, NULL, 320200, 3, '江阴市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320282, NULL, 320200, 3, '宜兴市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320300, NULL, 320000, 2, '徐州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320301, NULL, 320300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320302, NULL, 320300, 3, '鼓楼区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320303, NULL, 320300, 3, '云龙区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320305, NULL, 320300, 3, '贾汪区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320311, NULL, 320300, 3, '泉山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320312, NULL, 320300, 3, '铜山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320321, NULL, 320300, 3, '丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320322, NULL, 320300, 3, '沛县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320324, NULL, 320300, 3, '睢宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320381, NULL, 320300, 3, '新沂市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320382, NULL, 320300, 3, '邳州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320400, NULL, 320000, 2, '常州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320401, NULL, 320400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320402, NULL, 320400, 3, '天宁区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320404, NULL, 320400, 3, '钟楼区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320411, NULL, 320400, 3, '新北区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320412, NULL, 320400, 3, '武进区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320413, NULL, 320400, 3, '金坛区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320481, NULL, 320400, 3, '溧阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320500, NULL, 320000, 2, '苏州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320501, NULL, 320500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320505, NULL, 320500, 3, '虎丘区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320506, NULL, 320500, 3, '吴中区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320507, NULL, 320500, 3, '相城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320508, NULL, 320500, 3, '姑苏区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320509, NULL, 320500, 3, '吴江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320581, NULL, 320500, 3, '常熟市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320582, NULL, 320500, 3, '张家港市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320583, NULL, 320500, 3, '昆山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320585, NULL, 320500, 3, '太仓市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320600, NULL, 320000, 2, '南通市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320601, NULL, 320600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320602, NULL, 320600, 3, '崇川区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320611, NULL, 320600, 3, '港闸区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320612, NULL, 320600, 3, '通州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320621, NULL, 320600, 3, '海安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320623, NULL, 320600, 3, '如东县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320681, NULL, 320600, 3, '启东市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320682, NULL, 320600, 3, '如皋市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320684, NULL, 320600, 3, '海门市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320700, NULL, 320000, 2, '连云港市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320701, NULL, 320700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320703, NULL, 320700, 3, '连云区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320706, NULL, 320700, 3, '海州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320707, NULL, 320700, 3, '赣榆区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320722, NULL, 320700, 3, '东海县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320723, NULL, 320700, 3, '灌云县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320724, NULL, 320700, 3, '灌南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320800, NULL, 320000, 2, '淮安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320803, NULL, 320800, 3, '淮安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320804, NULL, 320800, 3, '淮阴区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320812, NULL, 320800, 3, '清江浦区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320813, NULL, 320800, 3, '洪泽区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320826, NULL, 320800, 3, '涟水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320830, NULL, 320800, 3, '盱眙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320831, NULL, 320800, 3, '金湖县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320900, NULL, 320000, 2, '盐城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320901, NULL, 320900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320902, NULL, 320900, 3, '亭湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320903, NULL, 320900, 3, '盐都区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320904, NULL, 320900, 3, '大丰区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320921, NULL, 320900, 3, '响水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320922, NULL, 320900, 3, '滨海县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320923, NULL, 320900, 3, '阜宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320924, NULL, 320900, 3, '射阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320925, NULL, 320900, 3, '建湖县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (320981, NULL, 320900, 3, '东台市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321000, NULL, 320000, 2, '扬州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321001, NULL, 321000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321002, NULL, 321000, 3, '广陵区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321003, NULL, 321000, 3, '邗江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321012, NULL, 321000, 3, '江都区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321023, NULL, 321000, 3, '宝应县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321081, NULL, 321000, 3, '仪征市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321084, NULL, 321000, 3, '高邮市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321100, NULL, 320000, 2, '镇江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321101, NULL, 321100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321102, NULL, 321100, 3, '京口区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321111, NULL, 321100, 3, '润州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321112, NULL, 321100, 3, '丹徒区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321181, NULL, 321100, 3, '丹阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321182, NULL, 321100, 3, '扬中市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321183, NULL, 321100, 3, '句容市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321200, NULL, 320000, 2, '泰州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321201, NULL, 321200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321202, NULL, 321200, 3, '海陵区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321203, NULL, 321200, 3, '高港区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321204, NULL, 321200, 3, '姜堰区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321281, NULL, 321200, 3, '兴化市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321282, NULL, 321200, 3, '靖江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321283, NULL, 321200, 3, '泰兴市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321300, NULL, 320000, 2, '宿迁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321301, NULL, 321300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321302, NULL, 321300, 3, '宿城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321311, NULL, 321300, 3, '宿豫区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321322, NULL, 321300, 3, '沭阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321323, NULL, 321300, 3, '泗阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (321324, NULL, 321300, 3, '泗洪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330000, NULL, 0, 1, '浙江省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330100, NULL, 330000, 2, '杭州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330101, NULL, 330100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330102, NULL, 330100, 3, '上城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330103, NULL, 330100, 3, '下城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330104, NULL, 330100, 3, '江干区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330105, NULL, 330100, 3, '拱墅区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330106, NULL, 330100, 3, '西湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330108, NULL, 330100, 3, '滨江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330109, NULL, 330100, 3, '萧山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330110, NULL, 330100, 3, '余杭区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330111, NULL, 330100, 3, '富阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330122, NULL, 330100, 3, '桐庐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330127, NULL, 330100, 3, '淳安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330182, NULL, 330100, 3, '建德市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330185, NULL, 330100, 3, '临安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330200, NULL, 330000, 2, '宁波市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330201, NULL, 330200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330203, NULL, 330200, 3, '海曙区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330204, NULL, 330200, 3, '江东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330205, NULL, 330200, 3, '江北区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330206, NULL, 330200, 3, '北仑区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330211, NULL, 330200, 3, '镇海区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330212, NULL, 330200, 3, '鄞州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330225, NULL, 330200, 3, '象山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330226, NULL, 330200, 3, '宁海县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330281, NULL, 330200, 3, '余姚市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330282, NULL, 330200, 3, '慈溪市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330283, NULL, 330200, 3, '奉化市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330300, NULL, 330000, 2, '温州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330301, NULL, 330300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330302, NULL, 330300, 3, '鹿城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330303, NULL, 330300, 3, '龙湾区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330304, NULL, 330300, 3, '瓯海区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330305, NULL, 330300, 3, '洞头区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330324, NULL, 330300, 3, '永嘉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330326, NULL, 330300, 3, '平阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330327, NULL, 330300, 3, '苍南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330328, NULL, 330300, 3, '文成县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330329, NULL, 330300, 3, '泰顺县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330381, NULL, 330300, 3, '瑞安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330382, NULL, 330300, 3, '乐清市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330400, NULL, 330000, 2, '嘉兴市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330401, NULL, 330400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330402, NULL, 330400, 3, '南湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330411, NULL, 330400, 3, '秀洲区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330421, NULL, 330400, 3, '嘉善县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330424, NULL, 330400, 3, '海盐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330481, NULL, 330400, 3, '海宁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330482, NULL, 330400, 3, '平湖市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330483, NULL, 330400, 3, '桐乡市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330500, NULL, 330000, 2, '湖州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330501, NULL, 330500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330502, NULL, 330500, 3, '吴兴区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330503, NULL, 330500, 3, '南浔区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330521, NULL, 330500, 3, '德清县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330522, NULL, 330500, 3, '长兴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330523, NULL, 330500, 3, '安吉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330600, NULL, 330000, 2, '绍兴市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330601, NULL, 330600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330602, NULL, 330600, 3, '越城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330603, NULL, 330600, 3, '柯桥区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330604, NULL, 330600, 3, '上虞区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330624, NULL, 330600, 3, '新昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330681, NULL, 330600, 3, '诸暨市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330683, NULL, 330600, 3, '嵊州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330700, NULL, 330000, 2, '金华市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330701, NULL, 330700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330702, NULL, 330700, 3, '婺城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330703, NULL, 330700, 3, '金东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330723, NULL, 330700, 3, '武义县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330726, NULL, 330700, 3, '浦江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330727, NULL, 330700, 3, '磐安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330781, NULL, 330700, 3, '兰溪市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330782, NULL, 330700, 3, '义乌市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330783, NULL, 330700, 3, '东阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330784, NULL, 330700, 3, '永康市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330800, NULL, 330000, 2, '衢州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330801, NULL, 330800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330802, NULL, 330800, 3, '柯城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330803, NULL, 330800, 3, '衢江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330822, NULL, 330800, 3, '常山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330824, NULL, 330800, 3, '开化县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330825, NULL, 330800, 3, '龙游县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330881, NULL, 330800, 3, '江山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330900, NULL, 330000, 2, '舟山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330901, NULL, 330900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330902, NULL, 330900, 3, '定海区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330903, NULL, 330900, 3, '普陀区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330921, NULL, 330900, 3, '岱山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (330922, NULL, 330900, 3, '嵊泗县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331000, NULL, 330000, 2, '台州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331001, NULL, 331000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331002, NULL, 331000, 3, '椒江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331003, NULL, 331000, 3, '黄岩区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331004, NULL, 331000, 3, '路桥区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331021, NULL, 331000, 3, '玉环县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331022, NULL, 331000, 3, '三门县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331023, NULL, 331000, 3, '天台县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331024, NULL, 331000, 3, '仙居县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331081, NULL, 331000, 3, '温岭市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331082, NULL, 331000, 3, '临海市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331100, NULL, 330000, 2, '丽水市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331101, NULL, 331100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331102, NULL, 331100, 3, '莲都区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331121, NULL, 331100, 3, '青田县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331122, NULL, 331100, 3, '缙云县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331123, NULL, 331100, 3, '遂昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331124, NULL, 331100, 3, '松阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331125, NULL, 331100, 3, '云和县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331126, NULL, 331100, 3, '庆元县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331127, NULL, 331100, 3, '景宁畲族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (331181, NULL, 331100, 3, '龙泉市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340000, NULL, 0, 1, '安徽省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340100, NULL, 340000, 2, '合肥市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340101, NULL, 340100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340102, NULL, 340100, 3, '瑶海区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340103, NULL, 340100, 3, '庐阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340104, NULL, 340100, 3, '蜀山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340111, NULL, 340100, 3, '包河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340121, NULL, 340100, 3, '长丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340122, NULL, 340100, 3, '肥东县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340123, NULL, 340100, 3, '肥西县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340124, NULL, 340100, 3, '庐江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340181, NULL, 340100, 3, '巢湖市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340200, NULL, 340000, 2, '芜湖市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340201, NULL, 340200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340202, NULL, 340200, 3, '镜湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340203, NULL, 340200, 3, '弋江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340207, NULL, 340200, 3, '鸠江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340208, NULL, 340200, 3, '三山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340221, NULL, 340200, 3, '芜湖县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340222, NULL, 340200, 3, '繁昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340223, NULL, 340200, 3, '南陵县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340225, NULL, 340200, 3, '无为县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340300, NULL, 340000, 2, '蚌埠市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340301, NULL, 340300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340302, NULL, 340300, 3, '龙子湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340303, NULL, 340300, 3, '蚌山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340304, NULL, 340300, 3, '禹会区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340311, NULL, 340300, 3, '淮上区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340321, NULL, 340300, 3, '怀远县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340322, NULL, 340300, 3, '五河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340323, NULL, 340300, 3, '固镇县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340400, NULL, 340000, 2, '淮南市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340401, NULL, 340400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340402, NULL, 340400, 3, '大通区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340403, NULL, 340400, 3, '田家庵区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340404, NULL, 340400, 3, '谢家集区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340405, NULL, 340400, 3, '八公山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340406, NULL, 340400, 3, '潘集区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340421, NULL, 340400, 3, '凤台县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340422, NULL, 340400, 3, '寿县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340500, NULL, 340000, 2, '马鞍山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340501, NULL, 340500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340503, NULL, 340500, 3, '花山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340504, NULL, 340500, 3, '雨山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340506, NULL, 340500, 3, '博望区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340521, NULL, 340500, 3, '当涂县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340522, NULL, 340500, 3, '含山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340523, NULL, 340500, 3, '和县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340600, NULL, 340000, 2, '淮北市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340601, NULL, 340600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340602, NULL, 340600, 3, '杜集区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340603, NULL, 340600, 3, '相山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340604, NULL, 340600, 3, '烈山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340621, NULL, 340600, 3, '濉溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340700, NULL, 340000, 2, '铜陵市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340701, NULL, 340700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340705, NULL, 340700, 3, '铜官区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340706, NULL, 340700, 3, '义安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340711, NULL, 340700, 3, '郊区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340722, NULL, 340700, 3, '枞阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340800, NULL, 340000, 2, '安庆市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340801, NULL, 340800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340802, NULL, 340800, 3, '迎江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340803, NULL, 340800, 3, '大观区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340811, NULL, 340800, 3, '宜秀区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340822, NULL, 340800, 3, '怀宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340824, NULL, 340800, 3, '潜山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340825, NULL, 340800, 3, '太湖县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340826, NULL, 340800, 3, '宿松县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340827, NULL, 340800, 3, '望江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340828, NULL, 340800, 3, '岳西县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (340881, NULL, 340800, 3, '桐城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341000, NULL, 340000, 2, '黄山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341001, NULL, 341000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341002, NULL, 341000, 3, '屯溪区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341003, NULL, 341000, 3, '黄山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341004, NULL, 341000, 3, '徽州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341021, NULL, 341000, 3, '歙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341022, NULL, 341000, 3, '休宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341023, NULL, 341000, 3, '黟县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341024, NULL, 341000, 3, '祁门县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341100, NULL, 340000, 2, '滁州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341101, NULL, 341100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341102, NULL, 341100, 3, '琅琊区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341103, NULL, 341100, 3, '南谯区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341122, NULL, 341100, 3, '来安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341124, NULL, 341100, 3, '全椒县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341125, NULL, 341100, 3, '定远县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341126, NULL, 341100, 3, '凤阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341181, NULL, 341100, 3, '天长市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341182, NULL, 341100, 3, '明光市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341200, NULL, 340000, 2, '阜阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341201, NULL, 341200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341202, NULL, 341200, 3, '颍州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341203, NULL, 341200, 3, '颍东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341204, NULL, 341200, 3, '颍泉区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341221, NULL, 341200, 3, '临泉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341222, NULL, 341200, 3, '太和县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341225, NULL, 341200, 3, '阜南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341226, NULL, 341200, 3, '颍上县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341282, NULL, 341200, 3, '界首市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341300, NULL, 340000, 2, '宿州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341301, NULL, 341300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341302, NULL, 341300, 3, '埇桥区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341321, NULL, 341300, 3, '砀山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341322, NULL, 341300, 3, '萧县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341323, NULL, 341300, 3, '灵璧县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341324, NULL, 341300, 3, '泗县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341500, NULL, 340000, 2, '六安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341501, NULL, 341500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341502, NULL, 341500, 3, '金安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341503, NULL, 341500, 3, '裕安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341504, NULL, 341500, 3, '叶集区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341522, NULL, 341500, 3, '霍邱县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341523, NULL, 341500, 3, '舒城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341524, NULL, 341500, 3, '金寨县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341525, NULL, 341500, 3, '霍山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341600, NULL, 340000, 2, '亳州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341601, NULL, 341600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341602, NULL, 341600, 3, '谯城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341621, NULL, 341600, 3, '涡阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341622, NULL, 341600, 3, '蒙城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341623, NULL, 341600, 3, '利辛县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341700, NULL, 340000, 2, '池州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341701, NULL, 341700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341702, NULL, 341700, 3, '贵池区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341721, NULL, 341700, 3, '东至县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341722, NULL, 341700, 3, '石台县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341723, NULL, 341700, 3, '青阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341800, NULL, 340000, 2, '宣城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341801, NULL, 341800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341802, NULL, 341800, 3, '宣州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341821, NULL, 341800, 3, '郎溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341822, NULL, 341800, 3, '广德县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341823, NULL, 341800, 3, '泾县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341824, NULL, 341800, 3, '绩溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341825, NULL, 341800, 3, '旌德县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (341881, NULL, 341800, 3, '宁国市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350000, NULL, 0, 1, '福建省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350100, NULL, 350000, 2, '福州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350101, NULL, 350100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350102, NULL, 350100, 3, '鼓楼区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350103, NULL, 350100, 3, '台江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350104, NULL, 350100, 3, '仓山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350105, NULL, 350100, 3, '马尾区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350111, NULL, 350100, 3, '晋安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350121, NULL, 350100, 3, '闽侯县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350122, NULL, 350100, 3, '连江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350123, NULL, 350100, 3, '罗源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350124, NULL, 350100, 3, '闽清县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350125, NULL, 350100, 3, '永泰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350128, NULL, 350100, 3, '平潭县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350181, NULL, 350100, 3, '福清市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350182, NULL, 350100, 3, '长乐市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350200, NULL, 350000, 2, '厦门市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350201, NULL, 350200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350203, NULL, 350200, 3, '思明区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350205, NULL, 350200, 3, '海沧区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350206, NULL, 350200, 3, '湖里区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350211, NULL, 350200, 3, '集美区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350212, NULL, 350200, 3, '同安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350213, NULL, 350200, 3, '翔安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350300, NULL, 350000, 2, '莆田市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350301, NULL, 350300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350302, NULL, 350300, 3, '城厢区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350303, NULL, 350300, 3, '涵江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350304, NULL, 350300, 3, '荔城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350305, NULL, 350300, 3, '秀屿区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350322, NULL, 350300, 3, '仙游县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350400, NULL, 350000, 2, '三明市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350401, NULL, 350400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350402, NULL, 350400, 3, '梅列区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350403, NULL, 350400, 3, '三元区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350421, NULL, 350400, 3, '明溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350423, NULL, 350400, 3, '清流县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350424, NULL, 350400, 3, '宁化县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350425, NULL, 350400, 3, '大田县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350426, NULL, 350400, 3, '尤溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350427, NULL, 350400, 3, '沙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350428, NULL, 350400, 3, '将乐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350429, NULL, 350400, 3, '泰宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350430, NULL, 350400, 3, '建宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350481, NULL, 350400, 3, '永安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350500, NULL, 350000, 2, '泉州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350501, NULL, 350500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350502, NULL, 350500, 3, '鲤城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350503, NULL, 350500, 3, '丰泽区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350504, NULL, 350500, 3, '洛江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350505, NULL, 350500, 3, '泉港区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350521, NULL, 350500, 3, '惠安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350524, NULL, 350500, 3, '安溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350525, NULL, 350500, 3, '永春县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350526, NULL, 350500, 3, '德化县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350527, NULL, 350500, 3, '金门县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350581, NULL, 350500, 3, '石狮市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350582, NULL, 350500, 3, '晋江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350583, NULL, 350500, 3, '南安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350600, NULL, 350000, 2, '漳州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350601, NULL, 350600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350602, NULL, 350600, 3, '芗城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350603, NULL, 350600, 3, '龙文区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350622, NULL, 350600, 3, '云霄县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350623, NULL, 350600, 3, '漳浦县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350624, NULL, 350600, 3, '诏安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350625, NULL, 350600, 3, '长泰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350626, NULL, 350600, 3, '东山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350627, NULL, 350600, 3, '南靖县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350628, NULL, 350600, 3, '平和县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350629, NULL, 350600, 3, '华安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350681, NULL, 350600, 3, '龙海市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350700, NULL, 350000, 2, '南平市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350701, NULL, 350700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350702, NULL, 350700, 3, '延平区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350703, NULL, 350700, 3, '建阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350721, NULL, 350700, 3, '顺昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350722, NULL, 350700, 3, '浦城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350723, NULL, 350700, 3, '光泽县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350724, NULL, 350700, 3, '松溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350725, NULL, 350700, 3, '政和县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350781, NULL, 350700, 3, '邵武市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350782, NULL, 350700, 3, '武夷山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350783, NULL, 350700, 3, '建瓯市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350800, NULL, 350000, 2, '龙岩市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350801, NULL, 350800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350802, NULL, 350800, 3, '新罗区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350803, NULL, 350800, 3, '永定区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350821, NULL, 350800, 3, '长汀县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350823, NULL, 350800, 3, '上杭县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350824, NULL, 350800, 3, '武平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350825, NULL, 350800, 3, '连城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350881, NULL, 350800, 3, '漳平市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350900, NULL, 350000, 2, '宁德市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350901, NULL, 350900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350902, NULL, 350900, 3, '蕉城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350921, NULL, 350900, 3, '霞浦县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350922, NULL, 350900, 3, '古田县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350923, NULL, 350900, 3, '屏南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350924, NULL, 350900, 3, '寿宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350925, NULL, 350900, 3, '周宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350926, NULL, 350900, 3, '柘荣县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350981, NULL, 350900, 3, '福安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (350982, NULL, 350900, 3, '福鼎市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360000, NULL, 0, 1, '江西省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360100, NULL, 360000, 2, '南昌市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360101, NULL, 360100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360102, NULL, 360100, 3, '东湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360103, NULL, 360100, 3, '西湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360104, NULL, 360100, 3, '青云谱区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360105, NULL, 360100, 3, '湾里区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360111, NULL, 360100, 3, '青山湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360112, NULL, 360100, 3, '新建区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360121, NULL, 360100, 3, '南昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360123, NULL, 360100, 3, '安义县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360124, NULL, 360100, 3, '进贤县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360200, NULL, 360000, 2, '景德镇市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360201, NULL, 360200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360202, NULL, 360200, 3, '昌江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360203, NULL, 360200, 3, '珠山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360222, NULL, 360200, 3, '浮梁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360281, NULL, 360200, 3, '乐平市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360300, NULL, 360000, 2, '萍乡市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360301, NULL, 360300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360302, NULL, 360300, 3, '安源区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360313, NULL, 360300, 3, '湘东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360321, NULL, 360300, 3, '莲花县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360322, NULL, 360300, 3, '上栗县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360323, NULL, 360300, 3, '芦溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360400, NULL, 360000, 2, '九江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360401, NULL, 360400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360402, NULL, 360400, 3, '濂溪区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360403, NULL, 360400, 3, '浔阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360421, NULL, 360400, 3, '九江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360423, NULL, 360400, 3, '武宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360424, NULL, 360400, 3, '修水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360425, NULL, 360400, 3, '永修县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360426, NULL, 360400, 3, '德安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360428, NULL, 360400, 3, '都昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360429, NULL, 360400, 3, '湖口县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360430, NULL, 360400, 3, '彭泽县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360481, NULL, 360400, 3, '瑞昌市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360482, NULL, 360400, 3, '共青城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360483, NULL, 360400, 3, '庐山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360500, NULL, 360000, 2, '新余市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360501, NULL, 360500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360502, NULL, 360500, 3, '渝水区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360521, NULL, 360500, 3, '分宜县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360600, NULL, 360000, 2, '鹰潭市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360601, NULL, 360600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360602, NULL, 360600, 3, '月湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360622, NULL, 360600, 3, '余江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360681, NULL, 360600, 3, '贵溪市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360700, NULL, 360000, 2, '赣州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360701, NULL, 360700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360702, NULL, 360700, 3, '章贡区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360703, NULL, 360700, 3, '南康区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360721, NULL, 360700, 3, '赣县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360722, NULL, 360700, 3, '信丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360723, NULL, 360700, 3, '大余县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360724, NULL, 360700, 3, '上犹县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360725, NULL, 360700, 3, '崇义县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360726, NULL, 360700, 3, '安远县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360727, NULL, 360700, 3, '龙南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360728, NULL, 360700, 3, '定南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360729, NULL, 360700, 3, '全南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360730, NULL, 360700, 3, '宁都县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360731, NULL, 360700, 3, '于都县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360732, NULL, 360700, 3, '兴国县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360733, NULL, 360700, 3, '会昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360734, NULL, 360700, 3, '寻乌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360735, NULL, 360700, 3, '石城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360781, NULL, 360700, 3, '瑞金市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360800, NULL, 360000, 2, '吉安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360801, NULL, 360800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360802, NULL, 360800, 3, '吉州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360803, NULL, 360800, 3, '青原区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360821, NULL, 360800, 3, '吉安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360822, NULL, 360800, 3, '吉水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360823, NULL, 360800, 3, '峡江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360824, NULL, 360800, 3, '新干县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360825, NULL, 360800, 3, '永丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360826, NULL, 360800, 3, '泰和县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360827, NULL, 360800, 3, '遂川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360828, NULL, 360800, 3, '万安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360829, NULL, 360800, 3, '安福县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360830, NULL, 360800, 3, '永新县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360881, NULL, 360800, 3, '井冈山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360900, NULL, 360000, 2, '宜春市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360901, NULL, 360900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360902, NULL, 360900, 3, '袁州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360921, NULL, 360900, 3, '奉新县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360922, NULL, 360900, 3, '万载县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360923, NULL, 360900, 3, '上高县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360924, NULL, 360900, 3, '宜丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360925, NULL, 360900, 3, '靖安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360926, NULL, 360900, 3, '铜鼓县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360981, NULL, 360900, 3, '丰城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360982, NULL, 360900, 3, '樟树市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (360983, NULL, 360900, 3, '高安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361000, NULL, 360000, 2, '抚州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361001, NULL, 361000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361002, NULL, 361000, 3, '临川区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361021, NULL, 361000, 3, '南城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361022, NULL, 361000, 3, '黎川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361023, NULL, 361000, 3, '南丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361024, NULL, 361000, 3, '崇仁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361025, NULL, 361000, 3, '乐安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361026, NULL, 361000, 3, '宜黄县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361027, NULL, 361000, 3, '金溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361028, NULL, 361000, 3, '资溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361029, NULL, 361000, 3, '东乡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361030, NULL, 361000, 3, '广昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361100, NULL, 360000, 2, '上饶市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361101, NULL, 361100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361102, NULL, 361100, 3, '信州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361103, NULL, 361100, 3, '广丰区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361121, NULL, 361100, 3, '上饶县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361123, NULL, 361100, 3, '玉山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361124, NULL, 361100, 3, '铅山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361125, NULL, 361100, 3, '横峰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361126, NULL, 361100, 3, '弋阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361127, NULL, 361100, 3, '余干县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361128, NULL, 361100, 3, '鄱阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361129, NULL, 361100, 3, '万年县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361130, NULL, 361100, 3, '婺源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (361181, NULL, 361100, 3, '德兴市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370000, NULL, 0, 1, '山东省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370100, NULL, 370000, 2, '济南市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370101, NULL, 370100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370102, NULL, 370100, 3, '历下区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370103, NULL, 370100, 3, '市中区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370104, NULL, 370100, 3, '槐荫区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370105, NULL, 370100, 3, '天桥区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370112, NULL, 370100, 3, '历城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370113, NULL, 370100, 3, '长清区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370124, NULL, 370100, 3, '平阴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370125, NULL, 370100, 3, '济阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370126, NULL, 370100, 3, '商河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370181, NULL, 370100, 3, '章丘市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370200, NULL, 370000, 2, '青岛市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370201, NULL, 370200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370202, NULL, 370200, 3, '市南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370203, NULL, 370200, 3, '市北区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370211, NULL, 370200, 3, '黄岛区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370212, NULL, 370200, 3, '崂山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370213, NULL, 370200, 3, '李沧区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370214, NULL, 370200, 3, '城阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370281, NULL, 370200, 3, '胶州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370282, NULL, 370200, 3, '即墨市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370283, NULL, 370200, 3, '平度市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370285, NULL, 370200, 3, '莱西市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370300, NULL, 370000, 2, '淄博市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370301, NULL, 370300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370302, NULL, 370300, 3, '淄川区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370303, NULL, 370300, 3, '张店区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370304, NULL, 370300, 3, '博山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370305, NULL, 370300, 3, '临淄区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370306, NULL, 370300, 3, '周村区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370321, NULL, 370300, 3, '桓台县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370322, NULL, 370300, 3, '高青县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370323, NULL, 370300, 3, '沂源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370400, NULL, 370000, 2, '枣庄市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370401, NULL, 370400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370402, NULL, 370400, 3, '市中区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370403, NULL, 370400, 3, '薛城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370404, NULL, 370400, 3, '峄城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370405, NULL, 370400, 3, '台儿庄区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370406, NULL, 370400, 3, '山亭区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370481, NULL, 370400, 3, '滕州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370500, NULL, 370000, 2, '东营市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370501, NULL, 370500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370502, NULL, 370500, 3, '东营区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370503, NULL, 370500, 3, '河口区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370505, NULL, 370500, 3, '垦利区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370522, NULL, 370500, 3, '利津县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370523, NULL, 370500, 3, '广饶县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370600, NULL, 370000, 2, '烟台市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370601, NULL, 370600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370602, NULL, 370600, 3, '芝罘区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370611, NULL, 370600, 3, '福山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370612, NULL, 370600, 3, '牟平区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370613, NULL, 370600, 3, '莱山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370634, NULL, 370600, 3, '长岛县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370681, NULL, 370600, 3, '龙口市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370682, NULL, 370600, 3, '莱阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370683, NULL, 370600, 3, '莱州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370684, NULL, 370600, 3, '蓬莱市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370685, NULL, 370600, 3, '招远市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370686, NULL, 370600, 3, '栖霞市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370687, NULL, 370600, 3, '海阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370700, NULL, 370000, 2, '潍坊市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370701, NULL, 370700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370702, NULL, 370700, 3, '潍城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370703, NULL, 370700, 3, '寒亭区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370704, NULL, 370700, 3, '坊子区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370705, NULL, 370700, 3, '奎文区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370724, NULL, 370700, 3, '临朐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370725, NULL, 370700, 3, '昌乐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370781, NULL, 370700, 3, '青州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370782, NULL, 370700, 3, '诸城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370783, NULL, 370700, 3, '寿光市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370784, NULL, 370700, 3, '安丘市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370785, NULL, 370700, 3, '高密市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370786, NULL, 370700, 3, '昌邑市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370800, NULL, 370000, 2, '济宁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370801, NULL, 370800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370811, NULL, 370800, 3, '任城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370812, NULL, 370800, 3, '兖州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370826, NULL, 370800, 3, '微山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370827, NULL, 370800, 3, '鱼台县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370828, NULL, 370800, 3, '金乡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370829, NULL, 370800, 3, '嘉祥县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370830, NULL, 370800, 3, '汶上县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370831, NULL, 370800, 3, '泗水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370832, NULL, 370800, 3, '梁山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370881, NULL, 370800, 3, '曲阜市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370883, NULL, 370800, 3, '邹城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370900, NULL, 370000, 2, '泰安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370901, NULL, 370900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370902, NULL, 370900, 3, '泰山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370911, NULL, 370900, 3, '岱岳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370921, NULL, 370900, 3, '宁阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370923, NULL, 370900, 3, '东平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370982, NULL, 370900, 3, '新泰市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (370983, NULL, 370900, 3, '肥城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371000, NULL, 370000, 2, '威海市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371001, NULL, 371000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371002, NULL, 371000, 3, '环翠区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371003, NULL, 371000, 3, '文登区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371082, NULL, 371000, 3, '荣成市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371083, NULL, 371000, 3, '乳山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371100, NULL, 370000, 2, '日照市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371101, NULL, 371100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371102, NULL, 371100, 3, '东港区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371103, NULL, 371100, 3, '岚山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371121, NULL, 371100, 3, '五莲县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371122, NULL, 371100, 3, '莒县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371200, NULL, 370000, 2, '莱芜市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371201, NULL, 371200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371202, NULL, 371200, 3, '莱城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371203, NULL, 371200, 3, '钢城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371300, NULL, 370000, 2, '临沂市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371301, NULL, 371300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371302, NULL, 371300, 3, '兰山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371311, NULL, 371300, 3, '罗庄区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371312, NULL, 371300, 3, '河东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371321, NULL, 371300, 3, '沂南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371322, NULL, 371300, 3, '郯城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371323, NULL, 371300, 3, '沂水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371324, NULL, 371300, 3, '兰陵县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371325, NULL, 371300, 3, '费县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371326, NULL, 371300, 3, '平邑县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371327, NULL, 371300, 3, '莒南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371328, NULL, 371300, 3, '蒙阴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371329, NULL, 371300, 3, '临沭县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371400, NULL, 370000, 2, '德州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371401, NULL, 371400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371402, NULL, 371400, 3, '德城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371403, NULL, 371400, 3, '陵城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371422, NULL, 371400, 3, '宁津县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371423, NULL, 371400, 3, '庆云县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371424, NULL, 371400, 3, '临邑县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371425, NULL, 371400, 3, '齐河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371426, NULL, 371400, 3, '平原县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371427, NULL, 371400, 3, '夏津县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371428, NULL, 371400, 3, '武城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371481, NULL, 371400, 3, '乐陵市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371482, NULL, 371400, 3, '禹城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371500, NULL, 370000, 2, '聊城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371501, NULL, 371500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371502, NULL, 371500, 3, '东昌府区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371521, NULL, 371500, 3, '阳谷县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371522, NULL, 371500, 3, '莘县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371523, NULL, 371500, 3, '茌平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371524, NULL, 371500, 3, '东阿县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371525, NULL, 371500, 3, '冠县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371526, NULL, 371500, 3, '高唐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371581, NULL, 371500, 3, '临清市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371600, NULL, 370000, 2, '滨州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371601, NULL, 371600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371602, NULL, 371600, 3, '滨城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371603, NULL, 371600, 3, '沾化区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371621, NULL, 371600, 3, '惠民县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371622, NULL, 371600, 3, '阳信县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371623, NULL, 371600, 3, '无棣县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371625, NULL, 371600, 3, '博兴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371626, NULL, 371600, 3, '邹平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371700, NULL, 370000, 2, '菏泽市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371701, NULL, 371700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371702, NULL, 371700, 3, '牡丹区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371703, NULL, 371700, 3, '定陶区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371721, NULL, 371700, 3, '曹县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371722, NULL, 371700, 3, '单县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371723, NULL, 371700, 3, '成武县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371724, NULL, 371700, 3, '巨野县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371725, NULL, 371700, 3, '郓城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371726, NULL, 371700, 3, '鄄城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (371728, NULL, 371700, 3, '东明县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410000, NULL, 0, 1, '河南省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410100, NULL, 410000, 2, '郑州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410101, NULL, 410100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410102, NULL, 410100, 3, '中原区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410103, NULL, 410100, 3, '二七区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410104, NULL, 410100, 3, '管城回族区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410105, NULL, 410100, 3, '金水区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410106, NULL, 410100, 3, '上街区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410108, NULL, 410100, 3, '惠济区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410122, NULL, 410100, 3, '中牟县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410181, NULL, 410100, 3, '巩义市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410182, NULL, 410100, 3, '荥阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410183, NULL, 410100, 3, '新密市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410184, NULL, 410100, 3, '新郑市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410185, NULL, 410100, 3, '登封市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410200, NULL, 410000, 2, '开封市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410201, NULL, 410200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410202, NULL, 410200, 3, '龙亭区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410203, NULL, 410200, 3, '顺河回族区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410204, NULL, 410200, 3, '鼓楼区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410205, NULL, 410200, 3, '禹王台区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410211, NULL, 410200, 3, '金明区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410212, NULL, 410200, 3, '祥符区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410221, NULL, 410200, 3, '杞县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410222, NULL, 410200, 3, '通许县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410223, NULL, 410200, 3, '尉氏县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410225, NULL, 410200, 3, '兰考县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410300, NULL, 410000, 2, '洛阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410301, NULL, 410300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410302, NULL, 410300, 3, '老城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410303, NULL, 410300, 3, '西工区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410304, NULL, 410300, 3, '瀍河回族区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410305, NULL, 410300, 3, '涧西区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410306, NULL, 410300, 3, '吉利区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410311, NULL, 410300, 3, '洛龙区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410322, NULL, 410300, 3, '孟津县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410323, NULL, 410300, 3, '新安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410324, NULL, 410300, 3, '栾川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410325, NULL, 410300, 3, '嵩县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410326, NULL, 410300, 3, '汝阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410327, NULL, 410300, 3, '宜阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410328, NULL, 410300, 3, '洛宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410329, NULL, 410300, 3, '伊川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410381, NULL, 410300, 3, '偃师市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410400, NULL, 410000, 2, '平顶山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410401, NULL, 410400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410402, NULL, 410400, 3, '新华区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410403, NULL, 410400, 3, '卫东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410404, NULL, 410400, 3, '石龙区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410411, NULL, 410400, 3, '湛河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410421, NULL, 410400, 3, '宝丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410422, NULL, 410400, 3, '叶县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410423, NULL, 410400, 3, '鲁山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410425, NULL, 410400, 3, '郏县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410481, NULL, 410400, 3, '舞钢市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410482, NULL, 410400, 3, '汝州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410500, NULL, 410000, 2, '安阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410501, NULL, 410500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410502, NULL, 410500, 3, '文峰区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410503, NULL, 410500, 3, '北关区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410505, NULL, 410500, 3, '殷都区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410506, NULL, 410500, 3, '龙安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410522, NULL, 410500, 3, '安阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410523, NULL, 410500, 3, '汤阴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410526, NULL, 410500, 3, '滑县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410527, NULL, 410500, 3, '内黄县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410581, NULL, 410500, 3, '林州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410600, NULL, 410000, 2, '鹤壁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410601, NULL, 410600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410602, NULL, 410600, 3, '鹤山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410603, NULL, 410600, 3, '山城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410611, NULL, 410600, 3, '淇滨区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410621, NULL, 410600, 3, '浚县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410622, NULL, 410600, 3, '淇县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410700, NULL, 410000, 2, '新乡市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410701, NULL, 410700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410702, NULL, 410700, 3, '红旗区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410703, NULL, 410700, 3, '卫滨区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410704, NULL, 410700, 3, '凤泉区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410711, NULL, 410700, 3, '牧野区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410721, NULL, 410700, 3, '新乡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410724, NULL, 410700, 3, '获嘉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410725, NULL, 410700, 3, '原阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410726, NULL, 410700, 3, '延津县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410727, NULL, 410700, 3, '封丘县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410728, NULL, 410700, 3, '长垣县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410781, NULL, 410700, 3, '卫辉市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410782, NULL, 410700, 3, '辉县市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410800, NULL, 410000, 2, '焦作市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410801, NULL, 410800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410802, NULL, 410800, 3, '解放区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410803, NULL, 410800, 3, '中站区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410804, NULL, 410800, 3, '马村区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410811, NULL, 410800, 3, '山阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410821, NULL, 410800, 3, '修武县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410822, NULL, 410800, 3, '博爱县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410823, NULL, 410800, 3, '武陟县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410825, NULL, 410800, 3, '温县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410882, NULL, 410800, 3, '沁阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410883, NULL, 410800, 3, '孟州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410900, NULL, 410000, 2, '濮阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410901, NULL, 410900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410902, NULL, 410900, 3, '华龙区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410922, NULL, 410900, 3, '清丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410923, NULL, 410900, 3, '南乐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410926, NULL, 410900, 3, '范县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410927, NULL, 410900, 3, '台前县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (410928, NULL, 410900, 3, '濮阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411000, NULL, 410000, 2, '许昌市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411001, NULL, 411000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411002, NULL, 411000, 3, '魏都区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411023, NULL, 411000, 3, '许昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411024, NULL, 411000, 3, '鄢陵县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411025, NULL, 411000, 3, '襄城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411081, NULL, 411000, 3, '禹州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411082, NULL, 411000, 3, '长葛市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411100, NULL, 410000, 2, '漯河市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411101, NULL, 411100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411102, NULL, 411100, 3, '源汇区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411103, NULL, 411100, 3, '郾城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411104, NULL, 411100, 3, '召陵区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411121, NULL, 411100, 3, '舞阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411122, NULL, 411100, 3, '临颍县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411200, NULL, 410000, 2, '三门峡市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411201, NULL, 411200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411202, NULL, 411200, 3, '湖滨区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411203, NULL, 411200, 3, '陕州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411221, NULL, 411200, 3, '渑池县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411224, NULL, 411200, 3, '卢氏县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411281, NULL, 411200, 3, '义马市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411282, NULL, 411200, 3, '灵宝市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411300, NULL, 410000, 2, '南阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411301, NULL, 411300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411302, NULL, 411300, 3, '宛城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411303, NULL, 411300, 3, '卧龙区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411321, NULL, 411300, 3, '南召县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411322, NULL, 411300, 3, '方城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411323, NULL, 411300, 3, '西峡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411324, NULL, 411300, 3, '镇平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411325, NULL, 411300, 3, '内乡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411326, NULL, 411300, 3, '淅川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411327, NULL, 411300, 3, '社旗县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411328, NULL, 411300, 3, '唐河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411329, NULL, 411300, 3, '新野县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411330, NULL, 411300, 3, '桐柏县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411381, NULL, 411300, 3, '邓州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411400, NULL, 410000, 2, '商丘市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411401, NULL, 411400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411402, NULL, 411400, 3, '梁园区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411403, NULL, 411400, 3, '睢阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411421, NULL, 411400, 3, '民权县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411422, NULL, 411400, 3, '睢县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411423, NULL, 411400, 3, '宁陵县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411424, NULL, 411400, 3, '柘城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411425, NULL, 411400, 3, '虞城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411426, NULL, 411400, 3, '夏邑县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411481, NULL, 411400, 3, '永城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411500, NULL, 410000, 2, '信阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411501, NULL, 411500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411502, NULL, 411500, 3, '浉河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411503, NULL, 411500, 3, '平桥区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411521, NULL, 411500, 3, '罗山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411522, NULL, 411500, 3, '光山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411523, NULL, 411500, 3, '新县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411524, NULL, 411500, 3, '商城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411525, NULL, 411500, 3, '固始县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411526, NULL, 411500, 3, '潢川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411527, NULL, 411500, 3, '淮滨县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411528, NULL, 411500, 3, '息县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411600, NULL, 410000, 2, '周口市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411601, NULL, 411600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411602, NULL, 411600, 3, '川汇区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411621, NULL, 411600, 3, '扶沟县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411622, NULL, 411600, 3, '西华县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411623, NULL, 411600, 3, '商水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411624, NULL, 411600, 3, '沈丘县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411625, NULL, 411600, 3, '郸城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411626, NULL, 411600, 3, '淮阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411627, NULL, 411600, 3, '太康县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411628, NULL, 411600, 3, '鹿邑县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411681, NULL, 411600, 3, '项城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411700, NULL, 410000, 2, '驻马店市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411701, NULL, 411700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411702, NULL, 411700, 3, '驿城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411721, NULL, 411700, 3, '西平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411722, NULL, 411700, 3, '上蔡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411723, NULL, 411700, 3, '平舆县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411724, NULL, 411700, 3, '正阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411725, NULL, 411700, 3, '确山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411726, NULL, 411700, 3, '泌阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411727, NULL, 411700, 3, '汝南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411728, NULL, 411700, 3, '遂平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (411729, NULL, 411700, 3, '新蔡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (419000, NULL, 410000, 2, '省直辖县级行政区划', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (419001, NULL, 419000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420000, NULL, 0, 1, '湖北省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420100, NULL, 420000, 2, '武汉市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420101, NULL, 420100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420102, NULL, 420100, 3, '江岸区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420103, NULL, 420100, 3, '江汉区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420104, NULL, 420100, 3, '硚口区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420105, NULL, 420100, 3, '汉阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420106, NULL, 420100, 3, '武昌区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420107, NULL, 420100, 3, '青山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420111, NULL, 420100, 3, '洪山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420112, NULL, 420100, 3, '东西湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420113, NULL, 420100, 3, '汉南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420114, NULL, 420100, 3, '蔡甸区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420115, NULL, 420100, 3, '江夏区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420116, NULL, 420100, 3, '黄陂区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420117, NULL, 420100, 3, '新洲区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420200, NULL, 420000, 2, '黄石市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420201, NULL, 420200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420202, NULL, 420200, 3, '黄石港区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420203, NULL, 420200, 3, '西塞山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420204, NULL, 420200, 3, '下陆区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420205, NULL, 420200, 3, '铁山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420222, NULL, 420200, 3, '阳新县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420281, NULL, 420200, 3, '大冶市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420300, NULL, 420000, 2, '十堰市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420301, NULL, 420300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420302, NULL, 420300, 3, '茅箭区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420303, NULL, 420300, 3, '张湾区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420304, NULL, 420300, 3, '郧阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420322, NULL, 420300, 3, '郧西县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420323, NULL, 420300, 3, '竹山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420324, NULL, 420300, 3, '竹溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420325, NULL, 420300, 3, '房县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420381, NULL, 420300, 3, '丹江口市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420500, NULL, 420000, 2, '宜昌市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420501, NULL, 420500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420502, NULL, 420500, 3, '西陵区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420503, NULL, 420500, 3, '伍家岗区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420504, NULL, 420500, 3, '点军区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420505, NULL, 420500, 3, '猇亭区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420506, NULL, 420500, 3, '夷陵区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420525, NULL, 420500, 3, '远安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420526, NULL, 420500, 3, '兴山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420527, NULL, 420500, 3, '秭归县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420528, NULL, 420500, 3, '长阳土家族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420529, NULL, 420500, 3, '五峰土家族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420581, NULL, 420500, 3, '宜都市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420582, NULL, 420500, 3, '当阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420583, NULL, 420500, 3, '枝江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420600, NULL, 420000, 2, '襄阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420601, NULL, 420600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420602, NULL, 420600, 3, '襄城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420606, NULL, 420600, 3, '樊城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420607, NULL, 420600, 3, '襄州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420624, NULL, 420600, 3, '南漳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420625, NULL, 420600, 3, '谷城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420626, NULL, 420600, 3, '保康县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420682, NULL, 420600, 3, '老河口市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420683, NULL, 420600, 3, '枣阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420684, NULL, 420600, 3, '宜城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420700, NULL, 420000, 2, '鄂州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420701, NULL, 420700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420702, NULL, 420700, 3, '梁子湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420703, NULL, 420700, 3, '华容区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420704, NULL, 420700, 3, '鄂城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420800, NULL, 420000, 2, '荆门市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420801, NULL, 420800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420802, NULL, 420800, 3, '东宝区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420804, NULL, 420800, 3, '掇刀区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420821, NULL, 420800, 3, '京山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420822, NULL, 420800, 3, '沙洋县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420881, NULL, 420800, 3, '钟祥市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420900, NULL, 420000, 2, '孝感市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420901, NULL, 420900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420902, NULL, 420900, 3, '孝南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420921, NULL, 420900, 3, '孝昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420922, NULL, 420900, 3, '大悟县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420923, NULL, 420900, 3, '云梦县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420981, NULL, 420900, 3, '应城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420982, NULL, 420900, 3, '安陆市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (420984, NULL, 420900, 3, '汉川市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421000, NULL, 420000, 2, '荆州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421001, NULL, 421000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421002, NULL, 421000, 3, '沙市区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421003, NULL, 421000, 3, '荆州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421022, NULL, 421000, 3, '公安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421023, NULL, 421000, 3, '监利县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421024, NULL, 421000, 3, '江陵县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421081, NULL, 421000, 3, '石首市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421083, NULL, 421000, 3, '洪湖市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421087, NULL, 421000, 3, '松滋市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421100, NULL, 420000, 2, '黄冈市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421101, NULL, 421100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421102, NULL, 421100, 3, '黄州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421121, NULL, 421100, 3, '团风县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421122, NULL, 421100, 3, '红安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421123, NULL, 421100, 3, '罗田县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421124, NULL, 421100, 3, '英山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421125, NULL, 421100, 3, '浠水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421126, NULL, 421100, 3, '蕲春县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421127, NULL, 421100, 3, '黄梅县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421181, NULL, 421100, 3, '麻城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421182, NULL, 421100, 3, '武穴市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421200, NULL, 420000, 2, '咸宁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421201, NULL, 421200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421202, NULL, 421200, 3, '咸安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421221, NULL, 421200, 3, '嘉鱼县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421222, NULL, 421200, 3, '通城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421223, NULL, 421200, 3, '崇阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421224, NULL, 421200, 3, '通山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421281, NULL, 421200, 3, '赤壁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421300, NULL, 420000, 2, '随州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421301, NULL, 421300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421303, NULL, 421300, 3, '曾都区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421321, NULL, 421300, 3, '随县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (421381, NULL, 421300, 3, '广水市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (422800, NULL, 420000, 2, '恩施土家族苗族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (422801, NULL, 422800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (422802, NULL, 422800, 3, '利川市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (422822, NULL, 422800, 3, '建始县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (422823, NULL, 422800, 3, '巴东县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (422825, NULL, 422800, 3, '宣恩县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (422826, NULL, 422800, 3, '咸丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (422827, NULL, 422800, 3, '来凤县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (422828, NULL, 422800, 3, '鹤峰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (429000, NULL, 420000, 2, '省直辖县级行政区划', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (429004, NULL, 429000, 3, '仙桃市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (429005, NULL, 429000, 3, '潜江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (429006, NULL, 429000, 3, '天门市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (429021, NULL, 429000, 3, '神农架林区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430000, NULL, 0, 1, '湖南省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430100, NULL, 430000, 2, '长沙市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430101, NULL, 430100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430102, NULL, 430100, 3, '芙蓉区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430103, NULL, 430100, 3, '天心区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430104, NULL, 430100, 3, '岳麓区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430105, NULL, 430100, 3, '开福区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430111, NULL, 430100, 3, '雨花区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430112, NULL, 430100, 3, '望城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430121, NULL, 430100, 3, '长沙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430124, NULL, 430100, 3, '宁乡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430181, NULL, 430100, 3, '浏阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430200, NULL, 430000, 2, '株洲市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430201, NULL, 430200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430202, NULL, 430200, 3, '荷塘区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430203, NULL, 430200, 3, '芦淞区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430204, NULL, 430200, 3, '石峰区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430211, NULL, 430200, 3, '天元区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430221, NULL, 430200, 3, '株洲县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430223, NULL, 430200, 3, '攸县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430224, NULL, 430200, 3, '茶陵县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430225, NULL, 430200, 3, '炎陵县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430281, NULL, 430200, 3, '醴陵市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430300, NULL, 430000, 2, '湘潭市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430301, NULL, 430300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430302, NULL, 430300, 3, '雨湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430304, NULL, 430300, 3, '岳塘区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430321, NULL, 430300, 3, '湘潭县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430381, NULL, 430300, 3, '湘乡市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430382, NULL, 430300, 3, '韶山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430400, NULL, 430000, 2, '衡阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430401, NULL, 430400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430405, NULL, 430400, 3, '珠晖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430406, NULL, 430400, 3, '雁峰区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430407, NULL, 430400, 3, '石鼓区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430408, NULL, 430400, 3, '蒸湘区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430412, NULL, 430400, 3, '南岳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430421, NULL, 430400, 3, '衡阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430422, NULL, 430400, 3, '衡南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430423, NULL, 430400, 3, '衡山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430424, NULL, 430400, 3, '衡东县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430426, NULL, 430400, 3, '祁东县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430481, NULL, 430400, 3, '耒阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430482, NULL, 430400, 3, '常宁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430500, NULL, 430000, 2, '邵阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430501, NULL, 430500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430502, NULL, 430500, 3, '双清区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430503, NULL, 430500, 3, '大祥区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430511, NULL, 430500, 3, '北塔区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430521, NULL, 430500, 3, '邵东县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430522, NULL, 430500, 3, '新邵县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430523, NULL, 430500, 3, '邵阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430524, NULL, 430500, 3, '隆回县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430525, NULL, 430500, 3, '洞口县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430527, NULL, 430500, 3, '绥宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430528, NULL, 430500, 3, '新宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430529, NULL, 430500, 3, '城步苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430581, NULL, 430500, 3, '武冈市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430600, NULL, 430000, 2, '岳阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430601, NULL, 430600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430602, NULL, 430600, 3, '岳阳楼区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430603, NULL, 430600, 3, '云溪区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430611, NULL, 430600, 3, '君山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430621, NULL, 430600, 3, '岳阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430623, NULL, 430600, 3, '华容县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430624, NULL, 430600, 3, '湘阴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430626, NULL, 430600, 3, '平江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430681, NULL, 430600, 3, '汨罗市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430682, NULL, 430600, 3, '临湘市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430700, NULL, 430000, 2, '常德市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430701, NULL, 430700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430702, NULL, 430700, 3, '武陵区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430703, NULL, 430700, 3, '鼎城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430721, NULL, 430700, 3, '安乡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430722, NULL, 430700, 3, '汉寿县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430723, NULL, 430700, 3, '澧县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430724, NULL, 430700, 3, '临澧县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430725, NULL, 430700, 3, '桃源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430726, NULL, 430700, 3, '石门县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430781, NULL, 430700, 3, '津市市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430800, NULL, 430000, 2, '张家界市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430801, NULL, 430800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430802, NULL, 430800, 3, '永定区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430811, NULL, 430800, 3, '武陵源区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430821, NULL, 430800, 3, '慈利县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430822, NULL, 430800, 3, '桑植县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430900, NULL, 430000, 2, '益阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430901, NULL, 430900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430902, NULL, 430900, 3, '资阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430903, NULL, 430900, 3, '赫山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430921, NULL, 430900, 3, '南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430922, NULL, 430900, 3, '桃江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430923, NULL, 430900, 3, '安化县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (430981, NULL, 430900, 3, '沅江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431000, NULL, 430000, 2, '郴州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431001, NULL, 431000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431002, NULL, 431000, 3, '北湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431003, NULL, 431000, 3, '苏仙区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431021, NULL, 431000, 3, '桂阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431022, NULL, 431000, 3, '宜章县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431023, NULL, 431000, 3, '永兴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431024, NULL, 431000, 3, '嘉禾县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431025, NULL, 431000, 3, '临武县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431026, NULL, 431000, 3, '汝城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431027, NULL, 431000, 3, '桂东县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431028, NULL, 431000, 3, '安仁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431081, NULL, 431000, 3, '资兴市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431100, NULL, 430000, 2, '永州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431101, NULL, 431100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431102, NULL, 431100, 3, '零陵区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431103, NULL, 431100, 3, '冷水滩区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431121, NULL, 431100, 3, '祁阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431122, NULL, 431100, 3, '东安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431123, NULL, 431100, 3, '双牌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431124, NULL, 431100, 3, '道县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431125, NULL, 431100, 3, '江永县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431126, NULL, 431100, 3, '宁远县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431127, NULL, 431100, 3, '蓝山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431128, NULL, 431100, 3, '新田县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431129, NULL, 431100, 3, '江华瑶族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431200, NULL, 430000, 2, '怀化市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431201, NULL, 431200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431202, NULL, 431200, 3, '鹤城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431221, NULL, 431200, 3, '中方县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431222, NULL, 431200, 3, '沅陵县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431223, NULL, 431200, 3, '辰溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431224, NULL, 431200, 3, '溆浦县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431225, NULL, 431200, 3, '会同县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431226, NULL, 431200, 3, '麻阳苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431227, NULL, 431200, 3, '新晃侗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431228, NULL, 431200, 3, '芷江侗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431229, NULL, 431200, 3, '靖州苗族侗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431230, NULL, 431200, 3, '通道侗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431281, NULL, 431200, 3, '洪江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431300, NULL, 430000, 2, '娄底市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431301, NULL, 431300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431302, NULL, 431300, 3, '娄星区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431321, NULL, 431300, 3, '双峰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431322, NULL, 431300, 3, '新化县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431381, NULL, 431300, 3, '冷水江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (431382, NULL, 431300, 3, '涟源市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (433100, NULL, 430000, 2, '湘西土家族苗族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (433101, NULL, 433100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (433122, NULL, 433100, 3, '泸溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (433123, NULL, 433100, 3, '凤凰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (433124, NULL, 433100, 3, '花垣县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (433125, NULL, 433100, 3, '保靖县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (433126, NULL, 433100, 3, '古丈县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (433127, NULL, 433100, 3, '永顺县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (433130, NULL, 433100, 3, '龙山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440000, NULL, 0, 1, '广东省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440100, NULL, 440000, 2, '广州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440101, NULL, 440100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440103, NULL, 440100, 3, '荔湾区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440104, NULL, 440100, 3, '越秀区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440105, NULL, 440100, 3, '海珠区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440106, NULL, 440100, 3, '天河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440111, NULL, 440100, 3, '白云区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440112, NULL, 440100, 3, '黄埔区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440113, NULL, 440100, 3, '番禺区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440114, NULL, 440100, 3, '花都区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440115, NULL, 440100, 3, '南沙区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440117, NULL, 440100, 3, '从化区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440118, NULL, 440100, 3, '增城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440200, NULL, 440000, 2, '韶关市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440201, NULL, 440200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440203, NULL, 440200, 3, '武江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440204, NULL, 440200, 3, '浈江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440205, NULL, 440200, 3, '曲江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440222, NULL, 440200, 3, '始兴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440224, NULL, 440200, 3, '仁化县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440229, NULL, 440200, 3, '翁源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440232, NULL, 440200, 3, '乳源瑶族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440233, NULL, 440200, 3, '新丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440281, NULL, 440200, 3, '乐昌市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440282, NULL, 440200, 3, '南雄市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440300, NULL, 440000, 2, '深圳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440301, NULL, 440300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440303, NULL, 440300, 3, '罗湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440304, NULL, 440300, 3, '福田区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440305, NULL, 440300, 3, '南山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440306, NULL, 440300, 3, '宝安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440307, NULL, 440300, 3, '龙岗区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440308, NULL, 440300, 3, '盐田区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440400, NULL, 440000, 2, '珠海市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440401, NULL, 440400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440402, NULL, 440400, 3, '香洲区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440403, NULL, 440400, 3, '斗门区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440404, NULL, 440400, 3, '金湾区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440500, NULL, 440000, 2, '汕头市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440501, NULL, 440500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440507, NULL, 440500, 3, '龙湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440511, NULL, 440500, 3, '金平区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440512, NULL, 440500, 3, '濠江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440513, NULL, 440500, 3, '潮阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440514, NULL, 440500, 3, '潮南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440515, NULL, 440500, 3, '澄海区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440523, NULL, 440500, 3, '南澳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440600, NULL, 440000, 2, '佛山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440601, NULL, 440600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440604, NULL, 440600, 3, '禅城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440605, NULL, 440600, 3, '南海区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440606, NULL, 440600, 3, '顺德区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440607, NULL, 440600, 3, '三水区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440608, NULL, 440600, 3, '高明区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440700, NULL, 440000, 2, '江门市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440701, NULL, 440700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440703, NULL, 440700, 3, '蓬江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440704, NULL, 440700, 3, '江海区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440705, NULL, 440700, 3, '新会区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440781, NULL, 440700, 3, '台山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440783, NULL, 440700, 3, '开平市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440784, NULL, 440700, 3, '鹤山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440785, NULL, 440700, 3, '恩平市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440800, NULL, 440000, 2, '湛江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440801, NULL, 440800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440802, NULL, 440800, 3, '赤坎区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440803, NULL, 440800, 3, '霞山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440804, NULL, 440800, 3, '坡头区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440811, NULL, 440800, 3, '麻章区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440823, NULL, 440800, 3, '遂溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440825, NULL, 440800, 3, '徐闻县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440881, NULL, 440800, 3, '廉江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440882, NULL, 440800, 3, '雷州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440883, NULL, 440800, 3, '吴川市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440900, NULL, 440000, 2, '茂名市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440901, NULL, 440900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440902, NULL, 440900, 3, '茂南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440904, NULL, 440900, 3, '电白区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440981, NULL, 440900, 3, '高州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440982, NULL, 440900, 3, '化州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (440983, NULL, 440900, 3, '信宜市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441200, NULL, 440000, 2, '肇庆市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441201, NULL, 441200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441202, NULL, 441200, 3, '端州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441203, NULL, 441200, 3, '鼎湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441204, NULL, 441200, 3, '高要区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441223, NULL, 441200, 3, '广宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441224, NULL, 441200, 3, '怀集县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441225, NULL, 441200, 3, '封开县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441226, NULL, 441200, 3, '德庆县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441284, NULL, 441200, 3, '四会市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441300, NULL, 440000, 2, '惠州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441301, NULL, 441300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441302, NULL, 441300, 3, '惠城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441303, NULL, 441300, 3, '惠阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441322, NULL, 441300, 3, '博罗县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441323, NULL, 441300, 3, '惠东县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441324, NULL, 441300, 3, '龙门县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441400, NULL, 440000, 2, '梅州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441401, NULL, 441400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441402, NULL, 441400, 3, '梅江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441403, NULL, 441400, 3, '梅县区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441422, NULL, 441400, 3, '大埔县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441423, NULL, 441400, 3, '丰顺县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441424, NULL, 441400, 3, '五华县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441426, NULL, 441400, 3, '平远县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441427, NULL, 441400, 3, '蕉岭县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441481, NULL, 441400, 3, '兴宁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441500, NULL, 440000, 2, '汕尾市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441501, NULL, 441500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441502, NULL, 441500, 3, '城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441521, NULL, 441500, 3, '海丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441523, NULL, 441500, 3, '陆河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441581, NULL, 441500, 3, '陆丰市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441600, NULL, 440000, 2, '河源市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441601, NULL, 441600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441602, NULL, 441600, 3, '源城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441621, NULL, 441600, 3, '紫金县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441622, NULL, 441600, 3, '龙川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441623, NULL, 441600, 3, '连平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441624, NULL, 441600, 3, '和平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441625, NULL, 441600, 3, '东源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441700, NULL, 440000, 2, '阳江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441701, NULL, 441700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441702, NULL, 441700, 3, '江城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441704, NULL, 441700, 3, '阳东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441721, NULL, 441700, 3, '阳西县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441781, NULL, 441700, 3, '阳春市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441800, NULL, 440000, 2, '清远市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441801, NULL, 441800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441802, NULL, 441800, 3, '清城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441803, NULL, 441800, 3, '清新区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441821, NULL, 441800, 3, '佛冈县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441823, NULL, 441800, 3, '阳山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441825, NULL, 441800, 3, '连山壮族瑶族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441826, NULL, 441800, 3, '连南瑶族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441881, NULL, 441800, 3, '英德市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441882, NULL, 441800, 3, '连州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (441900, NULL, 440000, 2, '东莞市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (442000, NULL, 440000, 2, '中山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445100, NULL, 440000, 2, '潮州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445101, NULL, 445100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445102, NULL, 445100, 3, '湘桥区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445103, NULL, 445100, 3, '潮安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445122, NULL, 445100, 3, '饶平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445200, NULL, 440000, 2, '揭阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445201, NULL, 445200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445202, NULL, 445200, 3, '榕城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445203, NULL, 445200, 3, '揭东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445222, NULL, 445200, 3, '揭西县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445224, NULL, 445200, 3, '惠来县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445281, NULL, 445200, 3, '普宁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445300, NULL, 440000, 2, '云浮市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445301, NULL, 445300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445302, NULL, 445300, 3, '云城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445303, NULL, 445300, 3, '云安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445321, NULL, 445300, 3, '新兴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445322, NULL, 445300, 3, '郁南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (445381, NULL, 445300, 3, '罗定市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450000, NULL, 0, 1, '广西壮族自治区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450100, NULL, 450000, 2, '南宁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450101, NULL, 450100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450102, NULL, 450100, 3, '兴宁区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450103, NULL, 450100, 3, '青秀区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450105, NULL, 450100, 3, '江南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450107, NULL, 450100, 3, '西乡塘区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450108, NULL, 450100, 3, '良庆区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450109, NULL, 450100, 3, '邕宁区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450110, NULL, 450100, 3, '武鸣区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450123, NULL, 450100, 3, '隆安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450124, NULL, 450100, 3, '马山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450125, NULL, 450100, 3, '上林县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450126, NULL, 450100, 3, '宾阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450127, NULL, 450100, 3, '横县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450200, NULL, 450000, 2, '柳州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450201, NULL, 450200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450202, NULL, 450200, 3, '城中区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450203, NULL, 450200, 3, '鱼峰区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450204, NULL, 450200, 3, '柳南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450205, NULL, 450200, 3, '柳北区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450206, NULL, 450200, 3, '柳江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450222, NULL, 450200, 3, '柳城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450223, NULL, 450200, 3, '鹿寨县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450224, NULL, 450200, 3, '融安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450225, NULL, 450200, 3, '融水苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450226, NULL, 450200, 3, '三江侗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450300, NULL, 450000, 2, '桂林市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450301, NULL, 450300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450302, NULL, 450300, 3, '秀峰区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450303, NULL, 450300, 3, '叠彩区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450304, NULL, 450300, 3, '象山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450305, NULL, 450300, 3, '七星区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450311, NULL, 450300, 3, '雁山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450312, NULL, 450300, 3, '临桂区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450321, NULL, 450300, 3, '阳朔县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450323, NULL, 450300, 3, '灵川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450324, NULL, 450300, 3, '全州县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450325, NULL, 450300, 3, '兴安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450326, NULL, 450300, 3, '永福县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450327, NULL, 450300, 3, '灌阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450328, NULL, 450300, 3, '龙胜各族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450329, NULL, 450300, 3, '资源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450330, NULL, 450300, 3, '平乐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450331, NULL, 450300, 3, '荔浦县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450332, NULL, 450300, 3, '恭城瑶族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450400, NULL, 450000, 2, '梧州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450401, NULL, 450400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450403, NULL, 450400, 3, '万秀区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450405, NULL, 450400, 3, '长洲区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450406, NULL, 450400, 3, '龙圩区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450421, NULL, 450400, 3, '苍梧县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450422, NULL, 450400, 3, '藤县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450423, NULL, 450400, 3, '蒙山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450481, NULL, 450400, 3, '岑溪市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450500, NULL, 450000, 2, '北海市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450501, NULL, 450500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450502, NULL, 450500, 3, '海城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450503, NULL, 450500, 3, '银海区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450512, NULL, 450500, 3, '铁山港区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450521, NULL, 450500, 3, '合浦县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450600, NULL, 450000, 2, '防城港市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450601, NULL, 450600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450602, NULL, 450600, 3, '港口区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450603, NULL, 450600, 3, '防城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450621, NULL, 450600, 3, '上思县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450681, NULL, 450600, 3, '东兴市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450700, NULL, 450000, 2, '钦州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450701, NULL, 450700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450702, NULL, 450700, 3, '钦南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450703, NULL, 450700, 3, '钦北区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450721, NULL, 450700, 3, '灵山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450722, NULL, 450700, 3, '浦北县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450800, NULL, 450000, 2, '贵港市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450801, NULL, 450800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450802, NULL, 450800, 3, '港北区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450803, NULL, 450800, 3, '港南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450804, NULL, 450800, 3, '覃塘区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450821, NULL, 450800, 3, '平南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450881, NULL, 450800, 3, '桂平市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450900, NULL, 450000, 2, '玉林市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450901, NULL, 450900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450902, NULL, 450900, 3, '玉州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450903, NULL, 450900, 3, '福绵区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450921, NULL, 450900, 3, '容县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450922, NULL, 450900, 3, '陆川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450923, NULL, 450900, 3, '博白县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450924, NULL, 450900, 3, '兴业县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (450981, NULL, 450900, 3, '北流市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451000, NULL, 450000, 2, '百色市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451001, NULL, 451000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451002, NULL, 451000, 3, '右江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451021, NULL, 451000, 3, '田阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451022, NULL, 451000, 3, '田东县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451023, NULL, 451000, 3, '平果县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451024, NULL, 451000, 3, '德保县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451026, NULL, 451000, 3, '那坡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451027, NULL, 451000, 3, '凌云县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451028, NULL, 451000, 3, '乐业县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451029, NULL, 451000, 3, '田林县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451030, NULL, 451000, 3, '西林县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451031, NULL, 451000, 3, '隆林各族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451081, NULL, 451000, 3, '靖西市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451100, NULL, 450000, 2, '贺州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451101, NULL, 451100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451102, NULL, 451100, 3, '八步区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451103, NULL, 451100, 3, '平桂区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451121, NULL, 451100, 3, '昭平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451122, NULL, 451100, 3, '钟山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451123, NULL, 451100, 3, '富川瑶族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451200, NULL, 450000, 2, '河池市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451201, NULL, 451200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451202, NULL, 451200, 3, '金城江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451221, NULL, 451200, 3, '南丹县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451222, NULL, 451200, 3, '天峨县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451223, NULL, 451200, 3, '凤山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451224, NULL, 451200, 3, '东兰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451225, NULL, 451200, 3, '罗城仫佬族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451226, NULL, 451200, 3, '环江毛南族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451227, NULL, 451200, 3, '巴马瑶族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451228, NULL, 451200, 3, '都安瑶族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451229, NULL, 451200, 3, '大化瑶族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451281, NULL, 451200, 3, '宜州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451300, NULL, 450000, 2, '来宾市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451301, NULL, 451300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451302, NULL, 451300, 3, '兴宾区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451321, NULL, 451300, 3, '忻城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451322, NULL, 451300, 3, '象州县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451323, NULL, 451300, 3, '武宣县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451324, NULL, 451300, 3, '金秀瑶族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451381, NULL, 451300, 3, '合山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451400, NULL, 450000, 2, '崇左市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451401, NULL, 451400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451402, NULL, 451400, 3, '江州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451421, NULL, 451400, 3, '扶绥县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451422, NULL, 451400, 3, '宁明县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451423, NULL, 451400, 3, '龙州县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451424, NULL, 451400, 3, '大新县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451425, NULL, 451400, 3, '天等县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (451481, NULL, 451400, 3, '凭祥市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460000, NULL, 0, 1, '海南省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460100, NULL, 460000, 2, '海口市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460101, NULL, 460100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460105, NULL, 460100, 3, '秀英区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460106, NULL, 460100, 3, '龙华区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460107, NULL, 460100, 3, '琼山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460108, NULL, 460100, 3, '美兰区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460200, NULL, 460000, 2, '三亚市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460201, NULL, 460200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460202, NULL, 460200, 3, '海棠区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460203, NULL, 460200, 3, '吉阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460204, NULL, 460200, 3, '天涯区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460205, NULL, 460200, 3, '崖州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460300, NULL, 460000, 2, '三沙市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (460400, NULL, 460000, 2, '儋州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469000, NULL, 460000, 2, '省直辖县级行政区划', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469001, NULL, 469000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469002, NULL, 469000, 3, '琼海市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469005, NULL, 469000, 3, '文昌市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469006, NULL, 469000, 3, '万宁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469007, NULL, 469000, 3, '东方市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469021, NULL, 469000, 3, '定安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469022, NULL, 469000, 3, '屯昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469023, NULL, 469000, 3, '澄迈县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469024, NULL, 469000, 3, '临高县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469025, NULL, 469000, 3, '白沙黎族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469026, NULL, 469000, 3, '昌江黎族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469027, NULL, 469000, 3, '乐东黎族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469028, NULL, 469000, 3, '陵水黎族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469029, NULL, 469000, 3, '保亭黎族苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (469030, NULL, 469000, 3, '琼中黎族苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500000, NULL, 0, 1, '重庆市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500100, NULL, 500000, 2, '重庆市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500101, NULL, 500100, 3, '万州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500102, NULL, 500100, 3, '涪陵区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500103, NULL, 500100, 3, '渝中区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500104, NULL, 500100, 3, '大渡口区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500105, NULL, 500100, 3, '江北区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500106, NULL, 500100, 3, '沙坪坝区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500107, NULL, 500100, 3, '九龙坡区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500108, NULL, 500100, 3, '南岸区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500109, NULL, 500100, 3, '北碚区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500110, NULL, 500100, 3, '綦江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500111, NULL, 500100, 3, '大足区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500112, NULL, 500100, 3, '渝北区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500113, NULL, 500100, 3, '巴南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500114, NULL, 500100, 3, '黔江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500115, NULL, 500100, 3, '长寿区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500116, NULL, 500100, 3, '江津区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500117, NULL, 500100, 3, '合川区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500118, NULL, 500100, 3, '永川区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500119, NULL, 500100, 3, '南川区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500120, NULL, 500100, 3, '璧山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500151, NULL, 500100, 3, '铜梁区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500152, NULL, 500100, 3, '潼南区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500153, NULL, 500100, 3, '荣昌区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500154, NULL, 500100, 3, '开州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500200, NULL, 500000, 2, '县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500228, NULL, 500200, 3, '梁平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500229, NULL, 500200, 3, '城口县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500230, NULL, 500200, 3, '丰都县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500231, NULL, 500200, 3, '垫江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500232, NULL, 500200, 3, '武隆县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500233, NULL, 500200, 3, '忠县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500235, NULL, 500200, 3, '云阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500236, NULL, 500200, 3, '奉节县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500237, NULL, 500200, 3, '巫山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500238, NULL, 500200, 3, '巫溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500240, NULL, 500200, 3, '石柱土家族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500241, NULL, 500200, 3, '秀山土家族苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500242, NULL, 500200, 3, '酉阳土家族苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (500243, NULL, 500200, 3, '彭水苗族土家族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510000, NULL, 0, 1, '四川省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510100, NULL, 510000, 2, '成都市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510101, NULL, 510100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510104, NULL, 510100, 3, '锦江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510105, NULL, 510100, 3, '青羊区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510106, NULL, 510100, 3, '金牛区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510107, NULL, 510100, 3, '武侯区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510108, NULL, 510100, 3, '成华区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510112, NULL, 510100, 3, '龙泉驿区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510113, NULL, 510100, 3, '青白江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510114, NULL, 510100, 3, '新都区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510115, NULL, 510100, 3, '温江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510116, NULL, 510100, 3, '双流区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510121, NULL, 510100, 3, '金堂县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510124, NULL, 510100, 3, '郫县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510129, NULL, 510100, 3, '大邑县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510131, NULL, 510100, 3, '蒲江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510132, NULL, 510100, 3, '新津县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510181, NULL, 510100, 3, '都江堰市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510182, NULL, 510100, 3, '彭州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510183, NULL, 510100, 3, '邛崃市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510184, NULL, 510100, 3, '崇州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510185, NULL, 510100, 3, '简阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510300, NULL, 510000, 2, '自贡市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510301, NULL, 510300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510302, NULL, 510300, 3, '自流井区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510303, NULL, 510300, 3, '贡井区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510304, NULL, 510300, 3, '大安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510311, NULL, 510300, 3, '沿滩区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510321, NULL, 510300, 3, '荣县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510322, NULL, 510300, 3, '富顺县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510400, NULL, 510000, 2, '攀枝花市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510401, NULL, 510400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510402, NULL, 510400, 3, '东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510403, NULL, 510400, 3, '西区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510411, NULL, 510400, 3, '仁和区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510421, NULL, 510400, 3, '米易县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510422, NULL, 510400, 3, '盐边县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510500, NULL, 510000, 2, '泸州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510501, NULL, 510500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510502, NULL, 510500, 3, '江阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510503, NULL, 510500, 3, '纳溪区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510504, NULL, 510500, 3, '龙马潭区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510521, NULL, 510500, 3, '泸县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510522, NULL, 510500, 3, '合江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510524, NULL, 510500, 3, '叙永县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510525, NULL, 510500, 3, '古蔺县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510600, NULL, 510000, 2, '德阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510601, NULL, 510600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510603, NULL, 510600, 3, '旌阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510623, NULL, 510600, 3, '中江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510626, NULL, 510600, 3, '罗江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510681, NULL, 510600, 3, '广汉市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510682, NULL, 510600, 3, '什邡市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510683, NULL, 510600, 3, '绵竹市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510700, NULL, 510000, 2, '绵阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510701, NULL, 510700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510703, NULL, 510700, 3, '涪城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510704, NULL, 510700, 3, '游仙区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510705, NULL, 510700, 3, '安州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510722, NULL, 510700, 3, '三台县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510723, NULL, 510700, 3, '盐亭县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510725, NULL, 510700, 3, '梓潼县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510726, NULL, 510700, 3, '北川羌族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510727, NULL, 510700, 3, '平武县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510781, NULL, 510700, 3, '江油市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510800, NULL, 510000, 2, '广元市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510801, NULL, 510800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510802, NULL, 510800, 3, '利州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510811, NULL, 510800, 3, '昭化区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510812, NULL, 510800, 3, '朝天区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510821, NULL, 510800, 3, '旺苍县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510822, NULL, 510800, 3, '青川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510823, NULL, 510800, 3, '剑阁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510824, NULL, 510800, 3, '苍溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510900, NULL, 510000, 2, '遂宁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510901, NULL, 510900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510903, NULL, 510900, 3, '船山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510904, NULL, 510900, 3, '安居区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510921, NULL, 510900, 3, '蓬溪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510922, NULL, 510900, 3, '射洪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (510923, NULL, 510900, 3, '大英县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511000, NULL, 510000, 2, '内江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511001, NULL, 511000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511002, NULL, 511000, 3, '市中区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511011, NULL, 511000, 3, '东兴区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511024, NULL, 511000, 3, '威远县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511025, NULL, 511000, 3, '资中县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511028, NULL, 511000, 3, '隆昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511100, NULL, 510000, 2, '乐山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511101, NULL, 511100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511102, NULL, 511100, 3, '市中区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511111, NULL, 511100, 3, '沙湾区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511112, NULL, 511100, 3, '五通桥区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511113, NULL, 511100, 3, '金口河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511123, NULL, 511100, 3, '犍为县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511124, NULL, 511100, 3, '井研县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511126, NULL, 511100, 3, '夹江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511129, NULL, 511100, 3, '沐川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511132, NULL, 511100, 3, '峨边彝族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511133, NULL, 511100, 3, '马边彝族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511181, NULL, 511100, 3, '峨眉山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511300, NULL, 510000, 2, '南充市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511301, NULL, 511300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511302, NULL, 511300, 3, '顺庆区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511303, NULL, 511300, 3, '高坪区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511304, NULL, 511300, 3, '嘉陵区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511321, NULL, 511300, 3, '南部县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511322, NULL, 511300, 3, '营山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511323, NULL, 511300, 3, '蓬安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511324, NULL, 511300, 3, '仪陇县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511325, NULL, 511300, 3, '西充县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511381, NULL, 511300, 3, '阆中市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511400, NULL, 510000, 2, '眉山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511401, NULL, 511400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511402, NULL, 511400, 3, '东坡区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511403, NULL, 511400, 3, '彭山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511421, NULL, 511400, 3, '仁寿县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511423, NULL, 511400, 3, '洪雅县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511424, NULL, 511400, 3, '丹棱县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511425, NULL, 511400, 3, '青神县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511500, NULL, 510000, 2, '宜宾市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511501, NULL, 511500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511502, NULL, 511500, 3, '翠屏区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511503, NULL, 511500, 3, '南溪区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511521, NULL, 511500, 3, '宜宾县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511523, NULL, 511500, 3, '江安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511524, NULL, 511500, 3, '长宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511525, NULL, 511500, 3, '高县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511526, NULL, 511500, 3, '珙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511527, NULL, 511500, 3, '筠连县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511528, NULL, 511500, 3, '兴文县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511529, NULL, 511500, 3, '屏山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511600, NULL, 510000, 2, '广安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511601, NULL, 511600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511602, NULL, 511600, 3, '广安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511603, NULL, 511600, 3, '前锋区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511621, NULL, 511600, 3, '岳池县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511622, NULL, 511600, 3, '武胜县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511623, NULL, 511600, 3, '邻水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511681, NULL, 511600, 3, '华蓥市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511700, NULL, 510000, 2, '达州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511701, NULL, 511700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511702, NULL, 511700, 3, '通川区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511703, NULL, 511700, 3, '达川区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511722, NULL, 511700, 3, '宣汉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511723, NULL, 511700, 3, '开江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511724, NULL, 511700, 3, '大竹县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511725, NULL, 511700, 3, '渠县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511781, NULL, 511700, 3, '万源市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511800, NULL, 510000, 2, '雅安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511801, NULL, 511800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511802, NULL, 511800, 3, '雨城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511803, NULL, 511800, 3, '名山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511822, NULL, 511800, 3, '荥经县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511823, NULL, 511800, 3, '汉源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511824, NULL, 511800, 3, '石棉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511825, NULL, 511800, 3, '天全县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511826, NULL, 511800, 3, '芦山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511827, NULL, 511800, 3, '宝兴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511900, NULL, 510000, 2, '巴中市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511901, NULL, 511900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511902, NULL, 511900, 3, '巴州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511903, NULL, 511900, 3, '恩阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511921, NULL, 511900, 3, '通江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511922, NULL, 511900, 3, '南江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (511923, NULL, 511900, 3, '平昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (512000, NULL, 510000, 2, '资阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (512001, NULL, 512000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (512002, NULL, 512000, 3, '雁江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (512021, NULL, 512000, 3, '安岳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (512022, NULL, 512000, 3, '乐至县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513200, NULL, 510000, 2, '阿坝藏族羌族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513201, NULL, 513200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513221, NULL, 513200, 3, '汶川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513222, NULL, 513200, 3, '理县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513223, NULL, 513200, 3, '茂县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513224, NULL, 513200, 3, '松潘县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513225, NULL, 513200, 3, '九寨沟县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513226, NULL, 513200, 3, '金川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513227, NULL, 513200, 3, '小金县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513228, NULL, 513200, 3, '黑水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513230, NULL, 513200, 3, '壤塘县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513231, NULL, 513200, 3, '阿坝县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513232, NULL, 513200, 3, '若尔盖县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513233, NULL, 513200, 3, '红原县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513300, NULL, 510000, 2, '甘孜藏族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513301, NULL, 513300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513322, NULL, 513300, 3, '泸定县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513323, NULL, 513300, 3, '丹巴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513324, NULL, 513300, 3, '九龙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513325, NULL, 513300, 3, '雅江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513326, NULL, 513300, 3, '道孚县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513327, NULL, 513300, 3, '炉霍县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513328, NULL, 513300, 3, '甘孜县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513329, NULL, 513300, 3, '新龙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513330, NULL, 513300, 3, '德格县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513331, NULL, 513300, 3, '白玉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513332, NULL, 513300, 3, '石渠县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513333, NULL, 513300, 3, '色达县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513334, NULL, 513300, 3, '理塘县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513335, NULL, 513300, 3, '巴塘县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513336, NULL, 513300, 3, '乡城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513337, NULL, 513300, 3, '稻城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513338, NULL, 513300, 3, '得荣县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513400, NULL, 510000, 2, '凉山彝族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513401, NULL, 513400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513422, NULL, 513400, 3, '木里藏族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513423, NULL, 513400, 3, '盐源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513424, NULL, 513400, 3, '德昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513425, NULL, 513400, 3, '会理县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513426, NULL, 513400, 3, '会东县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513427, NULL, 513400, 3, '宁南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513428, NULL, 513400, 3, '普格县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513429, NULL, 513400, 3, '布拖县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513430, NULL, 513400, 3, '金阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513431, NULL, 513400, 3, '昭觉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513432, NULL, 513400, 3, '喜德县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513433, NULL, 513400, 3, '冕宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513434, NULL, 513400, 3, '越西县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513435, NULL, 513400, 3, '甘洛县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513436, NULL, 513400, 3, '美姑县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (513437, NULL, 513400, 3, '雷波县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520000, NULL, 0, 1, '贵州省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520100, NULL, 520000, 2, '贵阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520101, NULL, 520100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520102, NULL, 520100, 3, '南明区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520103, NULL, 520100, 3, '云岩区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520111, NULL, 520100, 3, '花溪区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520112, NULL, 520100, 3, '乌当区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520113, NULL, 520100, 3, '白云区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520115, NULL, 520100, 3, '观山湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520121, NULL, 520100, 3, '开阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520122, NULL, 520100, 3, '息烽县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520123, NULL, 520100, 3, '修文县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520181, NULL, 520100, 3, '清镇市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520200, NULL, 520000, 2, '六盘水市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520201, NULL, 520200, 3, '钟山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520203, NULL, 520200, 3, '六枝特区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520221, NULL, 520200, 3, '水城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520222, NULL, 520200, 3, '盘县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520300, NULL, 520000, 2, '遵义市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520301, NULL, 520300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520302, NULL, 520300, 3, '红花岗区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520303, NULL, 520300, 3, '汇川区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520304, NULL, 520300, 3, '播州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520322, NULL, 520300, 3, '桐梓县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520323, NULL, 520300, 3, '绥阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520324, NULL, 520300, 3, '正安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520325, NULL, 520300, 3, '道真仡佬族苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520326, NULL, 520300, 3, '务川仡佬族苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520327, NULL, 520300, 3, '凤冈县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520328, NULL, 520300, 3, '湄潭县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520329, NULL, 520300, 3, '余庆县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520330, NULL, 520300, 3, '习水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520381, NULL, 520300, 3, '赤水市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520382, NULL, 520300, 3, '仁怀市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520400, NULL, 520000, 2, '安顺市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520401, NULL, 520400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520402, NULL, 520400, 3, '西秀区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520403, NULL, 520400, 3, '平坝区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520422, NULL, 520400, 3, '普定县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520423, NULL, 520400, 3, '镇宁布依族苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520424, NULL, 520400, 3, '关岭布依族苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520425, NULL, 520400, 3, '紫云苗族布依族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520500, NULL, 520000, 2, '毕节市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520501, NULL, 520500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520502, NULL, 520500, 3, '七星关区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520521, NULL, 520500, 3, '大方县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520522, NULL, 520500, 3, '黔西县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520523, NULL, 520500, 3, '金沙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520524, NULL, 520500, 3, '织金县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520525, NULL, 520500, 3, '纳雍县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520526, NULL, 520500, 3, '威宁彝族回族苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520527, NULL, 520500, 3, '赫章县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520600, NULL, 520000, 2, '铜仁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520601, NULL, 520600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520602, NULL, 520600, 3, '碧江区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520603, NULL, 520600, 3, '万山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520621, NULL, 520600, 3, '江口县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520622, NULL, 520600, 3, '玉屏侗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520623, NULL, 520600, 3, '石阡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520624, NULL, 520600, 3, '思南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520625, NULL, 520600, 3, '印江土家族苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520626, NULL, 520600, 3, '德江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520627, NULL, 520600, 3, '沿河土家族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (520628, NULL, 520600, 3, '松桃苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522300, NULL, 520000, 2, '黔西南布依族苗族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522301, NULL, 522300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522322, NULL, 522300, 3, '兴仁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522323, NULL, 522300, 3, '普安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522324, NULL, 522300, 3, '晴隆县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522325, NULL, 522300, 3, '贞丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522326, NULL, 522300, 3, '望谟县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522327, NULL, 522300, 3, '册亨县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522328, NULL, 522300, 3, '安龙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522600, NULL, 520000, 2, '黔东南苗族侗族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522601, NULL, 522600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522622, NULL, 522600, 3, '黄平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522623, NULL, 522600, 3, '施秉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522624, NULL, 522600, 3, '三穗县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522625, NULL, 522600, 3, '镇远县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522626, NULL, 522600, 3, '岑巩县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522627, NULL, 522600, 3, '天柱县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522628, NULL, 522600, 3, '锦屏县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522629, NULL, 522600, 3, '剑河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522630, NULL, 522600, 3, '台江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522631, NULL, 522600, 3, '黎平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522632, NULL, 522600, 3, '榕江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522633, NULL, 522600, 3, '从江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522634, NULL, 522600, 3, '雷山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522635, NULL, 522600, 3, '麻江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522636, NULL, 522600, 3, '丹寨县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522700, NULL, 520000, 2, '黔南布依族苗族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522701, NULL, 522700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522702, NULL, 522700, 3, '福泉市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522722, NULL, 522700, 3, '荔波县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522723, NULL, 522700, 3, '贵定县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522725, NULL, 522700, 3, '瓮安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522726, NULL, 522700, 3, '独山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522727, NULL, 522700, 3, '平塘县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522728, NULL, 522700, 3, '罗甸县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522729, NULL, 522700, 3, '长顺县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522730, NULL, 522700, 3, '龙里县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522731, NULL, 522700, 3, '惠水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (522732, NULL, 522700, 3, '三都水族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530000, NULL, 0, 1, '云南省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530100, NULL, 530000, 2, '昆明市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530101, NULL, 530100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530102, NULL, 530100, 3, '五华区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530103, NULL, 530100, 3, '盘龙区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530111, NULL, 530100, 3, '官渡区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530112, NULL, 530100, 3, '西山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530113, NULL, 530100, 3, '东川区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530114, NULL, 530100, 3, '呈贡区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530122, NULL, 530100, 3, '晋宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530124, NULL, 530100, 3, '富民县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530125, NULL, 530100, 3, '宜良县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530126, NULL, 530100, 3, '石林彝族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530127, NULL, 530100, 3, '嵩明县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530128, NULL, 530100, 3, '禄劝彝族苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530129, NULL, 530100, 3, '寻甸回族彝族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530181, NULL, 530100, 3, '安宁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530300, NULL, 530000, 2, '曲靖市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530301, NULL, 530300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530302, NULL, 530300, 3, '麒麟区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530303, NULL, 530300, 3, '沾益区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530321, NULL, 530300, 3, '马龙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530322, NULL, 530300, 3, '陆良县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530323, NULL, 530300, 3, '师宗县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530324, NULL, 530300, 3, '罗平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530325, NULL, 530300, 3, '富源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530326, NULL, 530300, 3, '会泽县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530381, NULL, 530300, 3, '宣威市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530400, NULL, 530000, 2, '玉溪市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530401, NULL, 530400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530402, NULL, 530400, 3, '红塔区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530403, NULL, 530400, 3, '江川区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530422, NULL, 530400, 3, '澄江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530423, NULL, 530400, 3, '通海县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530424, NULL, 530400, 3, '华宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530425, NULL, 530400, 3, '易门县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530426, NULL, 530400, 3, '峨山彝族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530427, NULL, 530400, 3, '新平彝族傣族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530428, NULL, 530400, 3, '元江哈尼族彝族傣族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530500, NULL, 530000, 2, '保山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530501, NULL, 530500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530502, NULL, 530500, 3, '隆阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530521, NULL, 530500, 3, '施甸县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530523, NULL, 530500, 3, '龙陵县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530524, NULL, 530500, 3, '昌宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530581, NULL, 530500, 3, '腾冲市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530600, NULL, 530000, 2, '昭通市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530601, NULL, 530600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530602, NULL, 530600, 3, '昭阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530621, NULL, 530600, 3, '鲁甸县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530622, NULL, 530600, 3, '巧家县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530623, NULL, 530600, 3, '盐津县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530624, NULL, 530600, 3, '大关县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530625, NULL, 530600, 3, '永善县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530626, NULL, 530600, 3, '绥江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530627, NULL, 530600, 3, '镇雄县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530628, NULL, 530600, 3, '彝良县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530629, NULL, 530600, 3, '威信县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530630, NULL, 530600, 3, '水富县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530700, NULL, 530000, 2, '丽江市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530701, NULL, 530700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530702, NULL, 530700, 3, '古城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530721, NULL, 530700, 3, '玉龙纳西族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530722, NULL, 530700, 3, '永胜县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530723, NULL, 530700, 3, '华坪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530724, NULL, 530700, 3, '宁蒗彝族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530800, NULL, 530000, 2, '普洱市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530801, NULL, 530800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530802, NULL, 530800, 3, '思茅区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530821, NULL, 530800, 3, '宁洱哈尼族彝族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530822, NULL, 530800, 3, '墨江哈尼族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530823, NULL, 530800, 3, '景东彝族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530824, NULL, 530800, 3, '景谷傣族彝族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530825, NULL, 530800, 3, '镇沅彝族哈尼族拉祜族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530826, NULL, 530800, 3, '江城哈尼族彝族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530827, NULL, 530800, 3, '孟连傣族拉祜族佤族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530828, NULL, 530800, 3, '澜沧拉祜族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530829, NULL, 530800, 3, '西盟佤族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530900, NULL, 530000, 2, '临沧市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530901, NULL, 530900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530902, NULL, 530900, 3, '临翔区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530921, NULL, 530900, 3, '凤庆县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530922, NULL, 530900, 3, '云县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530923, NULL, 530900, 3, '永德县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530924, NULL, 530900, 3, '镇康县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530925, NULL, 530900, 3, '双江拉祜族佤族布朗族傣族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530926, NULL, 530900, 3, '耿马傣族佤族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (530927, NULL, 530900, 3, '沧源佤族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532300, NULL, 530000, 2, '楚雄彝族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532301, NULL, 532300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532322, NULL, 532300, 3, '双柏县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532323, NULL, 532300, 3, '牟定县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532324, NULL, 532300, 3, '南华县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532325, NULL, 532300, 3, '姚安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532326, NULL, 532300, 3, '大姚县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532327, NULL, 532300, 3, '永仁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532328, NULL, 532300, 3, '元谋县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532329, NULL, 532300, 3, '武定县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532331, NULL, 532300, 3, '禄丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532500, NULL, 530000, 2, '红河哈尼族彝族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532501, NULL, 532500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532502, NULL, 532500, 3, '开远市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532503, NULL, 532500, 3, '蒙自市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532504, NULL, 532500, 3, '弥勒市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532523, NULL, 532500, 3, '屏边苗族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532524, NULL, 532500, 3, '建水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532525, NULL, 532500, 3, '石屏县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532527, NULL, 532500, 3, '泸西县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532528, NULL, 532500, 3, '元阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532529, NULL, 532500, 3, '红河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532530, NULL, 532500, 3, '金平苗族瑶族傣族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532531, NULL, 532500, 3, '绿春县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532532, NULL, 532500, 3, '河口瑶族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532600, NULL, 530000, 2, '文山壮族苗族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532601, NULL, 532600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532622, NULL, 532600, 3, '砚山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532623, NULL, 532600, 3, '西畴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532624, NULL, 532600, 3, '麻栗坡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532625, NULL, 532600, 3, '马关县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532626, NULL, 532600, 3, '丘北县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532627, NULL, 532600, 3, '广南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532628, NULL, 532600, 3, '富宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532800, NULL, 530000, 2, '西双版纳傣族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532801, NULL, 532800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532822, NULL, 532800, 3, '勐海县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532823, NULL, 532800, 3, '勐腊县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532900, NULL, 530000, 2, '大理白族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532901, NULL, 532900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532922, NULL, 532900, 3, '漾濞彝族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532923, NULL, 532900, 3, '祥云县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532924, NULL, 532900, 3, '宾川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532925, NULL, 532900, 3, '弥渡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532926, NULL, 532900, 3, '南涧彝族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532927, NULL, 532900, 3, '巍山彝族回族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532928, NULL, 532900, 3, '永平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532929, NULL, 532900, 3, '云龙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532930, NULL, 532900, 3, '洱源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532931, NULL, 532900, 3, '剑川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (532932, NULL, 532900, 3, '鹤庆县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533100, NULL, 530000, 2, '德宏傣族景颇族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533102, NULL, 533100, 3, '瑞丽市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533103, NULL, 533100, 3, '芒市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533122, NULL, 533100, 3, '梁河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533123, NULL, 533100, 3, '盈江县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533124, NULL, 533100, 3, '陇川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533300, NULL, 530000, 2, '怒江傈僳族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533301, NULL, 533300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533323, NULL, 533300, 3, '福贡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533324, NULL, 533300, 3, '贡山独龙族怒族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533325, NULL, 533300, 3, '兰坪白族普米族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533400, NULL, 530000, 2, '迪庆藏族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533401, NULL, 533400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533422, NULL, 533400, 3, '德钦县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (533423, NULL, 533400, 3, '维西傈僳族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540000, NULL, 0, 1, '西藏自治区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540100, NULL, 540000, 2, '拉萨市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540101, NULL, 540100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540102, NULL, 540100, 3, '城关区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540103, NULL, 540100, 3, '堆龙德庆区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540121, NULL, 540100, 3, '林周县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540122, NULL, 540100, 3, '当雄县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540123, NULL, 540100, 3, '尼木县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540124, NULL, 540100, 3, '曲水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540126, NULL, 540100, 3, '达孜县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540127, NULL, 540100, 3, '墨竹工卡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540200, NULL, 540000, 2, '日喀则市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540202, NULL, 540200, 3, '桑珠孜区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540221, NULL, 540200, 3, '南木林县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540222, NULL, 540200, 3, '江孜县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540223, NULL, 540200, 3, '定日县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540224, NULL, 540200, 3, '萨迦县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540225, NULL, 540200, 3, '拉孜县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540226, NULL, 540200, 3, '昂仁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540227, NULL, 540200, 3, '谢通门县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540228, NULL, 540200, 3, '白朗县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540229, NULL, 540200, 3, '仁布县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540230, NULL, 540200, 3, '康马县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540231, NULL, 540200, 3, '定结县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540232, NULL, 540200, 3, '仲巴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540233, NULL, 540200, 3, '亚东县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540234, NULL, 540200, 3, '吉隆县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540235, NULL, 540200, 3, '聂拉木县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540236, NULL, 540200, 3, '萨嘎县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540237, NULL, 540200, 3, '岗巴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540300, NULL, 540000, 2, '昌都市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540302, NULL, 540300, 3, '卡若区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540321, NULL, 540300, 3, '江达县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540322, NULL, 540300, 3, '贡觉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540323, NULL, 540300, 3, '类乌齐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540324, NULL, 540300, 3, '丁青县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540325, NULL, 540300, 3, '察雅县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540326, NULL, 540300, 3, '八宿县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540327, NULL, 540300, 3, '左贡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540328, NULL, 540300, 3, '芒康县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540329, NULL, 540300, 3, '洛隆县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540330, NULL, 540300, 3, '边坝县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540400, NULL, 540000, 2, '林芝市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540402, NULL, 540400, 3, '巴宜区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540421, NULL, 540400, 3, '工布江达县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540422, NULL, 540400, 3, '米林县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540423, NULL, 540400, 3, '墨脱县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540424, NULL, 540400, 3, '波密县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540425, NULL, 540400, 3, '察隅县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540426, NULL, 540400, 3, '朗县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540500, NULL, 540000, 2, '山南市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540501, NULL, 540500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540502, NULL, 540500, 3, '乃东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540521, NULL, 540500, 3, '扎囊县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540522, NULL, 540500, 3, '贡嘎县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540523, NULL, 540500, 3, '桑日县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540524, NULL, 540500, 3, '琼结县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540525, NULL, 540500, 3, '曲松县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540526, NULL, 540500, 3, '措美县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540527, NULL, 540500, 3, '洛扎县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540528, NULL, 540500, 3, '加查县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540529, NULL, 540500, 3, '隆子县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540530, NULL, 540500, 3, '错那县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (540531, NULL, 540500, 3, '浪卡子县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542400, NULL, 540000, 2, '那曲地区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542421, NULL, 542400, 3, '那曲县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542422, NULL, 542400, 3, '嘉黎县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542423, NULL, 542400, 3, '比如县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542424, NULL, 542400, 3, '聂荣县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542425, NULL, 542400, 3, '安多县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542426, NULL, 542400, 3, '申扎县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542427, NULL, 542400, 3, '索县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542428, NULL, 542400, 3, '班戈县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542429, NULL, 542400, 3, '巴青县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542430, NULL, 542400, 3, '尼玛县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542431, NULL, 542400, 3, '双湖县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542500, NULL, 540000, 2, '阿里地区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542521, NULL, 542500, 3, '普兰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542522, NULL, 542500, 3, '札达县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542523, NULL, 542500, 3, '噶尔县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542524, NULL, 542500, 3, '日土县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542525, NULL, 542500, 3, '革吉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542526, NULL, 542500, 3, '改则县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (542527, NULL, 542500, 3, '措勤县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610000, NULL, 0, 1, '陕西省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610100, NULL, 610000, 2, '西安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610101, NULL, 610100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610102, NULL, 610100, 3, '新城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610103, NULL, 610100, 3, '碑林区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610104, NULL, 610100, 3, '莲湖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610111, NULL, 610100, 3, '灞桥区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610112, NULL, 610100, 3, '未央区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610113, NULL, 610100, 3, '雁塔区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610114, NULL, 610100, 3, '阎良区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610115, NULL, 610100, 3, '临潼区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610116, NULL, 610100, 3, '长安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610117, NULL, 610100, 3, '高陵区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610122, NULL, 610100, 3, '蓝田县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610124, NULL, 610100, 3, '周至县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610125, NULL, 610100, 3, '户县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610200, NULL, 610000, 2, '铜川市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610201, NULL, 610200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610202, NULL, 610200, 3, '王益区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610203, NULL, 610200, 3, '印台区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610204, NULL, 610200, 3, '耀州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610222, NULL, 610200, 3, '宜君县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610300, NULL, 610000, 2, '宝鸡市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610301, NULL, 610300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610302, NULL, 610300, 3, '渭滨区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610303, NULL, 610300, 3, '金台区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610304, NULL, 610300, 3, '陈仓区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610322, NULL, 610300, 3, '凤翔县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610323, NULL, 610300, 3, '岐山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610324, NULL, 610300, 3, '扶风县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610326, NULL, 610300, 3, '眉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610327, NULL, 610300, 3, '陇县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610328, NULL, 610300, 3, '千阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610329, NULL, 610300, 3, '麟游县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610330, NULL, 610300, 3, '凤县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610331, NULL, 610300, 3, '太白县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610400, NULL, 610000, 2, '咸阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610401, NULL, 610400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610402, NULL, 610400, 3, '秦都区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610403, NULL, 610400, 3, '杨陵区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610404, NULL, 610400, 3, '渭城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610422, NULL, 610400, 3, '三原县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610423, NULL, 610400, 3, '泾阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610424, NULL, 610400, 3, '乾县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610425, NULL, 610400, 3, '礼泉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610426, NULL, 610400, 3, '永寿县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610427, NULL, 610400, 3, '彬县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610428, NULL, 610400, 3, '长武县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610429, NULL, 610400, 3, '旬邑县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610430, NULL, 610400, 3, '淳化县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610431, NULL, 610400, 3, '武功县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610481, NULL, 610400, 3, '兴平市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610500, NULL, 610000, 2, '渭南市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610501, NULL, 610500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610502, NULL, 610500, 3, '临渭区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610503, NULL, 610500, 3, '华州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610522, NULL, 610500, 3, '潼关县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610523, NULL, 610500, 3, '大荔县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610524, NULL, 610500, 3, '合阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610525, NULL, 610500, 3, '澄城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610526, NULL, 610500, 3, '蒲城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610527, NULL, 610500, 3, '白水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610528, NULL, 610500, 3, '富平县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610581, NULL, 610500, 3, '韩城市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610582, NULL, 610500, 3, '华阴市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610600, NULL, 610000, 2, '延安市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610601, NULL, 610600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610602, NULL, 610600, 3, '宝塔区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610603, NULL, 610600, 3, '安塞区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610621, NULL, 610600, 3, '延长县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610622, NULL, 610600, 3, '延川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610623, NULL, 610600, 3, '子长县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610625, NULL, 610600, 3, '志丹县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610626, NULL, 610600, 3, '吴起县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610627, NULL, 610600, 3, '甘泉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610628, NULL, 610600, 3, '富县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610629, NULL, 610600, 3, '洛川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610630, NULL, 610600, 3, '宜川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610631, NULL, 610600, 3, '黄龙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610632, NULL, 610600, 3, '黄陵县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610700, NULL, 610000, 2, '汉中市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610701, NULL, 610700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610702, NULL, 610700, 3, '汉台区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610721, NULL, 610700, 3, '南郑县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610722, NULL, 610700, 3, '城固县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610723, NULL, 610700, 3, '洋县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610724, NULL, 610700, 3, '西乡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610725, NULL, 610700, 3, '勉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610726, NULL, 610700, 3, '宁强县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610727, NULL, 610700, 3, '略阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610728, NULL, 610700, 3, '镇巴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610729, NULL, 610700, 3, '留坝县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610730, NULL, 610700, 3, '佛坪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610800, NULL, 610000, 2, '榆林市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610801, NULL, 610800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610802, NULL, 610800, 3, '榆阳区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610803, NULL, 610800, 3, '横山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610821, NULL, 610800, 3, '神木县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610822, NULL, 610800, 3, '府谷县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610824, NULL, 610800, 3, '靖边县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610825, NULL, 610800, 3, '定边县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610826, NULL, 610800, 3, '绥德县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610827, NULL, 610800, 3, '米脂县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610828, NULL, 610800, 3, '佳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610829, NULL, 610800, 3, '吴堡县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610830, NULL, 610800, 3, '清涧县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610831, NULL, 610800, 3, '子洲县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610900, NULL, 610000, 2, '安康市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610901, NULL, 610900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610902, NULL, 610900, 3, '汉滨区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610921, NULL, 610900, 3, '汉阴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610922, NULL, 610900, 3, '石泉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610923, NULL, 610900, 3, '宁陕县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610924, NULL, 610900, 3, '紫阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610925, NULL, 610900, 3, '岚皋县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610926, NULL, 610900, 3, '平利县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610927, NULL, 610900, 3, '镇坪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610928, NULL, 610900, 3, '旬阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (610929, NULL, 610900, 3, '白河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (611000, NULL, 610000, 2, '商洛市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (611001, NULL, 611000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (611002, NULL, 611000, 3, '商州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (611021, NULL, 611000, 3, '洛南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (611022, NULL, 611000, 3, '丹凤县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (611023, NULL, 611000, 3, '商南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (611024, NULL, 611000, 3, '山阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (611025, NULL, 611000, 3, '镇安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (611026, NULL, 611000, 3, '柞水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620000, NULL, 0, 1, '甘肃省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620100, NULL, 620000, 2, '兰州市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620101, NULL, 620100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620102, NULL, 620100, 3, '城关区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620103, NULL, 620100, 3, '七里河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620104, NULL, 620100, 3, '西固区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620105, NULL, 620100, 3, '安宁区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620111, NULL, 620100, 3, '红古区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620121, NULL, 620100, 3, '永登县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620122, NULL, 620100, 3, '皋兰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620123, NULL, 620100, 3, '榆中县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620200, NULL, 620000, 2, '嘉峪关市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620201, NULL, 620200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620300, NULL, 620000, 2, '金昌市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620301, NULL, 620300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620302, NULL, 620300, 3, '金川区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620321, NULL, 620300, 3, '永昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620400, NULL, 620000, 2, '白银市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620401, NULL, 620400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620402, NULL, 620400, 3, '白银区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620403, NULL, 620400, 3, '平川区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620421, NULL, 620400, 3, '靖远县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620422, NULL, 620400, 3, '会宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620423, NULL, 620400, 3, '景泰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620500, NULL, 620000, 2, '天水市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620501, NULL, 620500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620502, NULL, 620500, 3, '秦州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620503, NULL, 620500, 3, '麦积区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620521, NULL, 620500, 3, '清水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620522, NULL, 620500, 3, '秦安县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620523, NULL, 620500, 3, '甘谷县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620524, NULL, 620500, 3, '武山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620525, NULL, 620500, 3, '张家川回族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620600, NULL, 620000, 2, '武威市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620601, NULL, 620600, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620602, NULL, 620600, 3, '凉州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620621, NULL, 620600, 3, '民勤县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620622, NULL, 620600, 3, '古浪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620623, NULL, 620600, 3, '天祝藏族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620700, NULL, 620000, 2, '张掖市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620701, NULL, 620700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620702, NULL, 620700, 3, '甘州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620721, NULL, 620700, 3, '肃南裕固族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620722, NULL, 620700, 3, '民乐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620723, NULL, 620700, 3, '临泽县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620724, NULL, 620700, 3, '高台县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620725, NULL, 620700, 3, '山丹县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620800, NULL, 620000, 2, '平凉市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620801, NULL, 620800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620802, NULL, 620800, 3, '崆峒区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620821, NULL, 620800, 3, '泾川县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620822, NULL, 620800, 3, '灵台县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620823, NULL, 620800, 3, '崇信县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620824, NULL, 620800, 3, '华亭县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620825, NULL, 620800, 3, '庄浪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620826, NULL, 620800, 3, '静宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620900, NULL, 620000, 2, '酒泉市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620901, NULL, 620900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620902, NULL, 620900, 3, '肃州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620921, NULL, 620900, 3, '金塔县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620922, NULL, 620900, 3, '瓜州县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620923, NULL, 620900, 3, '肃北蒙古族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620924, NULL, 620900, 3, '阿克塞哈萨克族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620981, NULL, 620900, 3, '玉门市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (620982, NULL, 620900, 3, '敦煌市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621000, NULL, 620000, 2, '庆阳市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621001, NULL, 621000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621002, NULL, 621000, 3, '西峰区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621021, NULL, 621000, 3, '庆城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621022, NULL, 621000, 3, '环县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621023, NULL, 621000, 3, '华池县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621024, NULL, 621000, 3, '合水县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621025, NULL, 621000, 3, '正宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621026, NULL, 621000, 3, '宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621027, NULL, 621000, 3, '镇原县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621100, NULL, 620000, 2, '定西市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621101, NULL, 621100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621102, NULL, 621100, 3, '安定区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621121, NULL, 621100, 3, '通渭县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621122, NULL, 621100, 3, '陇西县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621123, NULL, 621100, 3, '渭源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621124, NULL, 621100, 3, '临洮县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621125, NULL, 621100, 3, '漳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621126, NULL, 621100, 3, '岷县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621200, NULL, 620000, 2, '陇南市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621201, NULL, 621200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621202, NULL, 621200, 3, '武都区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621221, NULL, 621200, 3, '成县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621222, NULL, 621200, 3, '文县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621223, NULL, 621200, 3, '宕昌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621224, NULL, 621200, 3, '康县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621225, NULL, 621200, 3, '西和县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621226, NULL, 621200, 3, '礼县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621227, NULL, 621200, 3, '徽县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (621228, NULL, 621200, 3, '两当县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (622900, NULL, 620000, 2, '临夏回族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (622901, NULL, 622900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (622921, NULL, 622900, 3, '临夏县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (622922, NULL, 622900, 3, '康乐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (622923, NULL, 622900, 3, '永靖县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (622924, NULL, 622900, 3, '广河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (622925, NULL, 622900, 3, '和政县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (622926, NULL, 622900, 3, '东乡族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (622927, NULL, 622900, 3, '积石山保安族东乡族撒拉族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (623000, NULL, 620000, 2, '甘南藏族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (623001, NULL, 623000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (623021, NULL, 623000, 3, '临潭县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (623022, NULL, 623000, 3, '卓尼县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (623023, NULL, 623000, 3, '舟曲县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (623024, NULL, 623000, 3, '迭部县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (623025, NULL, 623000, 3, '玛曲县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (623026, NULL, 623000, 3, '碌曲县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (623027, NULL, 623000, 3, '夏河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630000, NULL, 0, 1, '青海省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630100, NULL, 630000, 2, '西宁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630101, NULL, 630100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630102, NULL, 630100, 3, '城东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630103, NULL, 630100, 3, '城中区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630104, NULL, 630100, 3, '城西区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630105, NULL, 630100, 3, '城北区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630121, NULL, 630100, 3, '大通回族土族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630122, NULL, 630100, 3, '湟中县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630123, NULL, 630100, 3, '湟源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630200, NULL, 630000, 2, '海东市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630202, NULL, 630200, 3, '乐都区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630203, NULL, 630200, 3, '平安区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630222, NULL, 630200, 3, '民和回族土族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630223, NULL, 630200, 3, '互助土族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630224, NULL, 630200, 3, '化隆回族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (630225, NULL, 630200, 3, '循化撒拉族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632200, NULL, 630000, 2, '海北藏族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632221, NULL, 632200, 3, '门源回族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632222, NULL, 632200, 3, '祁连县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632223, NULL, 632200, 3, '海晏县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632224, NULL, 632200, 3, '刚察县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632300, NULL, 630000, 2, '黄南藏族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632321, NULL, 632300, 3, '同仁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632322, NULL, 632300, 3, '尖扎县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632323, NULL, 632300, 3, '泽库县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632324, NULL, 632300, 3, '河南蒙古族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632500, NULL, 630000, 2, '海南藏族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632521, NULL, 632500, 3, '共和县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632522, NULL, 632500, 3, '同德县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632523, NULL, 632500, 3, '贵德县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632524, NULL, 632500, 3, '兴海县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632525, NULL, 632500, 3, '贵南县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632600, NULL, 630000, 2, '果洛藏族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632621, NULL, 632600, 3, '玛沁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632622, NULL, 632600, 3, '班玛县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632623, NULL, 632600, 3, '甘德县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632624, NULL, 632600, 3, '达日县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632625, NULL, 632600, 3, '久治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632626, NULL, 632600, 3, '玛多县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632700, NULL, 630000, 2, '玉树藏族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632701, NULL, 632700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632722, NULL, 632700, 3, '杂多县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632723, NULL, 632700, 3, '称多县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632724, NULL, 632700, 3, '治多县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632725, NULL, 632700, 3, '囊谦县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632726, NULL, 632700, 3, '曲麻莱县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632800, NULL, 630000, 2, '海西蒙古族藏族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632801, NULL, 632800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632802, NULL, 632800, 3, '德令哈市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632821, NULL, 632800, 3, '乌兰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632822, NULL, 632800, 3, '都兰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (632823, NULL, 632800, 3, '天峻县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640000, NULL, 0, 1, '宁夏回族自治区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640100, NULL, 640000, 2, '银川市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640101, NULL, 640100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640104, NULL, 640100, 3, '兴庆区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640105, NULL, 640100, 3, '西夏区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640106, NULL, 640100, 3, '金凤区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640121, NULL, 640100, 3, '永宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640122, NULL, 640100, 3, '贺兰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640181, NULL, 640100, 3, '灵武市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640200, NULL, 640000, 2, '石嘴山市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640201, NULL, 640200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640202, NULL, 640200, 3, '大武口区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640205, NULL, 640200, 3, '惠农区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640221, NULL, 640200, 3, '平罗县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640300, NULL, 640000, 2, '吴忠市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640301, NULL, 640300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640302, NULL, 640300, 3, '利通区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640303, NULL, 640300, 3, '红寺堡区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640323, NULL, 640300, 3, '盐池县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640324, NULL, 640300, 3, '同心县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640381, NULL, 640300, 3, '青铜峡市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640400, NULL, 640000, 2, '固原市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640401, NULL, 640400, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640402, NULL, 640400, 3, '原州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640422, NULL, 640400, 3, '西吉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640423, NULL, 640400, 3, '隆德县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640424, NULL, 640400, 3, '泾源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640425, NULL, 640400, 3, '彭阳县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640500, NULL, 640000, 2, '中卫市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640501, NULL, 640500, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640502, NULL, 640500, 3, '沙坡头区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640521, NULL, 640500, 3, '中宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (640522, NULL, 640500, 3, '海原县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650000, NULL, 0, 1, '新疆维吾尔自治区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650100, NULL, 650000, 2, '乌鲁木齐市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650101, NULL, 650100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650102, NULL, 650100, 3, '天山区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650103, NULL, 650100, 3, '沙依巴克区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650104, NULL, 650100, 3, '新市区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650105, NULL, 650100, 3, '水磨沟区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650106, NULL, 650100, 3, '头屯河区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650107, NULL, 650100, 3, '达坂城区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650109, NULL, 650100, 3, '米东区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650121, NULL, 650100, 3, '乌鲁木齐县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650200, NULL, 650000, 2, '克拉玛依市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650201, NULL, 650200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650202, NULL, 650200, 3, '独山子区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650203, NULL, 650200, 3, '克拉玛依区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650204, NULL, 650200, 3, '白碱滩区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650205, NULL, 650200, 3, '乌尔禾区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650400, NULL, 650000, 2, '吐鲁番市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650402, NULL, 650400, 3, '高昌区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650421, NULL, 650400, 3, '鄯善县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650422, NULL, 650400, 3, '托克逊县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650500, NULL, 650000, 2, '哈密市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650502, NULL, 650500, 3, '伊州区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650521, NULL, 650500, 3, '巴里坤哈萨克自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (650522, NULL, 650500, 3, '伊吾县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652300, NULL, 650000, 2, '昌吉回族自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652301, NULL, 652300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652302, NULL, 652300, 3, '阜康市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652323, NULL, 652300, 3, '呼图壁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652324, NULL, 652300, 3, '玛纳斯县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652325, NULL, 652300, 3, '奇台县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652327, NULL, 652300, 3, '吉木萨尔县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652328, NULL, 652300, 3, '木垒哈萨克自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652700, NULL, 650000, 2, '博尔塔拉蒙古自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652701, NULL, 652700, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652702, NULL, 652700, 3, '阿拉山口市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652722, NULL, 652700, 3, '精河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652723, NULL, 652700, 3, '温泉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652800, NULL, 650000, 2, '巴音郭楞蒙古自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652801, NULL, 652800, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652822, NULL, 652800, 3, '轮台县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652823, NULL, 652800, 3, '尉犁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652824, NULL, 652800, 3, '若羌县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652825, NULL, 652800, 3, '且末县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652826, NULL, 652800, 3, '焉耆回族自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652827, NULL, 652800, 3, '和静县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652828, NULL, 652800, 3, '和硕县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652829, NULL, 652800, 3, '博湖县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652900, NULL, 650000, 2, '阿克苏地区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652901, NULL, 652900, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652922, NULL, 652900, 3, '温宿县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652923, NULL, 652900, 3, '库车县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652924, NULL, 652900, 3, '沙雅县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652925, NULL, 652900, 3, '新和县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652926, NULL, 652900, 3, '拜城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652927, NULL, 652900, 3, '乌什县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652928, NULL, 652900, 3, '阿瓦提县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (652929, NULL, 652900, 3, '柯坪县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653000, NULL, 650000, 2, '克孜勒苏柯尔克孜自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653001, NULL, 653000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653022, NULL, 653000, 3, '阿克陶县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653023, NULL, 653000, 3, '阿合奇县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653024, NULL, 653000, 3, '乌恰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653100, NULL, 650000, 2, '喀什地区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653101, NULL, 653100, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653121, NULL, 653100, 3, '疏附县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653122, NULL, 653100, 3, '疏勒县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653123, NULL, 653100, 3, '英吉沙县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653124, NULL, 653100, 3, '泽普县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653125, NULL, 653100, 3, '莎车县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653126, NULL, 653100, 3, '叶城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653127, NULL, 653100, 3, '麦盖提县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653128, NULL, 653100, 3, '岳普湖县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653129, NULL, 653100, 3, '伽师县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653130, NULL, 653100, 3, '巴楚县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653131, NULL, 653100, 3, '塔什库尔干塔吉克自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653200, NULL, 650000, 2, '和田地区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653201, NULL, 653200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653221, NULL, 653200, 3, '和田县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653222, NULL, 653200, 3, '墨玉县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653223, NULL, 653200, 3, '皮山县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653224, NULL, 653200, 3, '洛浦县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653225, NULL, 653200, 3, '策勒县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653226, NULL, 653200, 3, '于田县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (653227, NULL, 653200, 3, '民丰县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654000, NULL, 650000, 2, '伊犁哈萨克自治州', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654002, NULL, 654000, 3, '伊宁市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654003, NULL, 654000, 3, '奎屯市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654004, NULL, 654000, 3, '霍尔果斯市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654021, NULL, 654000, 3, '伊宁县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654022, NULL, 654000, 3, '察布查尔锡伯自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654023, NULL, 654000, 3, '霍城县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654024, NULL, 654000, 3, '巩留县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654025, NULL, 654000, 3, '新源县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654026, NULL, 654000, 3, '昭苏县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654027, NULL, 654000, 3, '特克斯县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654028, NULL, 654000, 3, '尼勒克县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654200, NULL, 650000, 2, '塔城地区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654201, NULL, 654200, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654202, NULL, 654200, 3, '乌苏市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654221, NULL, 654200, 3, '额敏县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654223, NULL, 654200, 3, '沙湾县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654224, NULL, 654200, 3, '托里县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654225, NULL, 654200, 3, '裕民县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654226, NULL, 654200, 3, '和布克赛尔蒙古自治县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654300, NULL, 650000, 2, '阿勒泰地区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654301, NULL, 654300, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654321, NULL, 654300, 3, '布尔津县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654322, NULL, 654300, 3, '富蕴县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654323, NULL, 654300, 3, '福海县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654324, NULL, 654300, 3, '哈巴河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654325, NULL, 654300, 3, '青河县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (654326, NULL, 654300, 3, '吉木乃县', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (659000, NULL, 650000, 2, '自治区直辖县级行政区划', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (659001, NULL, 659000, 3, '市辖区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (659002, NULL, 659000, 3, '阿拉尔市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (659003, NULL, 659000, 3, '图木舒克市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (659004, NULL, 659000, 3, '五家渠市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (659006, NULL, 659000, 3, '铁门关市', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (710000, NULL, 0, 1, '台湾省', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (810000, NULL, 0, 1, '香港特别行政区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (820000, NULL, 0, 1, '澳门特别行政区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920000, NULL, 500200, 3, '静海县', '301600', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920001, NULL, 330100, 3, '富阳市', '311400', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920002, NULL, 410100, 3, '郑东新区', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920003, NULL, 440100, 3, '萝岗区', '510100', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920004, NULL, 441900, 3, '东城街道', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920005, NULL, 441900, 3, '南城街道', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920006, NULL, 441900, 3, '万江街道', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920007, NULL, 441900, 3, '莞城街道', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920008, NULL, 441900, 3, '石碣镇', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920009, NULL, 441900, 3, '石龙镇', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920010, NULL, 441900, 3, '茶山镇', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920011, NULL, 441900, 3, '石排镇', '0', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920012, NULL, 441900, 3, '企石镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920013, NULL, 441900, 3, '横沥镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920014, NULL, 441900, 3, '桥头镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920015, NULL, 441900, 3, '谢岗镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920016, NULL, 441900, 3, '东坑镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920017, NULL, 441900, 3, '常平镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920018, NULL, 441900, 3, '寮步镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920019, NULL, 441900, 3, '樟木头镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920020, NULL, 441900, 3, '大朗镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920021, NULL, 441900, 3, '黄江镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920022, NULL, 441900, 3, '清溪镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920023, NULL, 441900, 3, '塘厦镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920024, NULL, 441900, 3, '凤岗镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920025, NULL, 441900, 3, '大岭山镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920026, NULL, 441900, 3, '长安镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920027, NULL, 441900, 3, '虎门镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920028, NULL, 441900, 3, '厚街镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920029, NULL, 441900, 3, '沙田镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920030, NULL, 441900, 3, '道滘镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920031, NULL, 441900, 3, '洪梅镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920032, NULL, 441900, 3, '麻涌镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920033, NULL, 441900, 3, '望牛墩镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920034, NULL, 441900, 3, '中堂镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920035, NULL, 441900, 3, '高埗镇', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920036, NULL, 441900, 3, '松山湖管委会', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920037, NULL, 441900, 3, '虎门港管委会', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920038, NULL, 441900, 3, '东莞生态园', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920039, NULL, 442000, 3, '中山市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920040, NULL, 460300, 3, '三沙市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920041, NULL, 460400, 3, '儋州市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920042, NULL, 810000, 2, '香港特别行政区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920043, NULL, 920042, 3, '中西区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920044, NULL, 920042, 3, '东区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920045, NULL, 920042, 3, '九龙城区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920046, NULL, 920042, 3, '观塘区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920047, NULL, 920042, 3, '南区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920048, NULL, 920042, 3, '深水埗区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920049, NULL, 920042, 3, '湾仔区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920050, NULL, 920042, 3, '黄大仙区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920051, NULL, 920042, 3, '油尖旺区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920052, NULL, 920042, 3, '离岛区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920053, NULL, 920042, 3, '葵青区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920054, NULL, 920042, 3, '北区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920055, NULL, 920042, 3, '西贡区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920056, NULL, 920042, 3, '沙田区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920057, NULL, 920042, 3, '屯门区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920058, NULL, 920042, 3, '大埔区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920059, NULL, 920042, 3, '荃湾区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920060, NULL, 920042, 3, '元朗区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920061, NULL, 820000, 2, '澳门特别行政区', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920062, NULL, 920061, 3, '澳门半岛', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920063, NULL, 920061, 3, '凼仔', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920064, NULL, 920061, 3, '路凼城', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920065, NULL, 920061, 3, '路环', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920066, NULL, 710000, 2, '台北市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920067, NULL, 710000, 2, '高雄市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920068, NULL, 710000, 2, '台南市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920069, NULL, 710000, 2, '台中市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920070, NULL, 710000, 2, '南投县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920071, NULL, 710000, 2, '基隆市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920072, NULL, 710000, 2, '新竹市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920073, NULL, 710000, 2, '嘉义市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920074, NULL, 710000, 2, '新北市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920075, NULL, 710000, 2, '宜兰县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920076, NULL, 710000, 2, '新竹县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920077, NULL, 710000, 2, '桃园市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920078, NULL, 710000, 2, '苗栗县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920079, NULL, 710000, 2, '彰化县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920080, NULL, 710000, 2, '嘉义县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920081, NULL, 710000, 2, '云林县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920082, NULL, 710000, 2, '屏东县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920083, NULL, 710000, 2, '台东县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920084, NULL, 710000, 2, '花莲县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920085, NULL, 710000, 2, '澎湖县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920086, NULL, 920066, 3, '台北市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920087, NULL, 920067, 3, '高雄市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920088, NULL, 920068, 3, '台南市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920089, NULL, 920069, 3, '台中市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920090, NULL, 920070, 3, '南投县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920091, NULL, 920071, 3, '基隆市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920092, NULL, 920072, 3, '新竹市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920093, NULL, 920073, 3, '嘉义市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920094, NULL, 920074, 3, '新北市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920095, NULL, 920075, 3, '宜兰县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920096, NULL, 920076, 3, '新竹县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920097, NULL, 920077, 3, '桃园市', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920098, NULL, 920078, 3, '苗栗县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920099, NULL, 920079, 3, '彰化县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920100, NULL, 920080, 3, '嘉义县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920101, NULL, 920081, 3, '云林县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920102, NULL, 920082, 3, '屏东县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920103, NULL, 920083, 3, '台东县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920104, NULL, 920084, 3, '花莲县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920105, NULL, 920085, 3, ' 澎湖县', '0', 0, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920106, NULL, 442000, 3, '西区街道', '555555', 100, '', '', '2024-06-10 17:34:05', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_areas` VALUES (920119, NULL, 110000, 2, 'Yest2', '2', 0, '{}', '5ffbf91764d342f2a710797206866172', '2025-08-02 19:55:58', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 19:55:58.335629', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 19:55:58.334821');

-- ----------------------------
-- Table structure for gd_articles
-- ----------------------------
DROP TABLE IF EXISTS `gd_articles`;
CREATE TABLE `gd_articles`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Title` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Brief` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CoverImage` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ContentBody` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TypeId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Sort` int NOT NULL,
  `IsPub` tinyint(1) NOT NULL,
  `Pv` int NOT NULL,
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
  INDEX `IX_Gd_Articles_TypeId`(`TypeId`) USING BTREE,
  CONSTRAINT `FK_Gd_Articles_Gd_ArticleTypes_TypeId` FOREIGN KEY (`TypeId`) REFERENCES `gd_articletypes` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_articles
-- ----------------------------
INSERT INTO `gd_articles` VALUES ('3a0ee070-d867-78ee-5e12-82de6dd2e44a', NULL, '关于我们', '关于【Rex商城平台】的介绍。', '', '<h3 style=\"text-align: start;\"><span style=\"font-size: 12px;\">关于我们</span></h3><p style=\"text-align: start;\"><span style=\"font-size: 12px;\">欢迎来到【Rex商城平台】，一个致力于为您提供优质购物体验的商城平台。</span></p><p style=\"text-align: start;\"><span style=\"font-size: 12px;\">我们专注于打造一个便捷、安全、高效的在线购物环境，汇聚了丰富多样的商品，涵盖【如：家居、服饰、美妆、数码、食品等】多个品类，满足您日常生活中的各种需求。</span></p><h4 style=\"text-align: start;\"><span style=\"font-size: 12px;\">我们的使命</span></h4><p style=\"text-align: start;\"><span style=\"font-size: 12px;\">让优质商品触手可及，让每一次购物都变得轻松愉快。</span></p><h4 style=\"text-align: start;\"><span style=\"font-size: 12px;\">我们的优势</span></h4><p style=\"text-align: start;\"><span style=\"font-size: 12px;\">✅ 商品种类丰富，品质有保障<br><br>✅ 价格实惠，优惠活动不断<br><br>✅ 微信一键下单，支付便捷<br><br>✅ 快速物流配送，售后贴心服务</span></p><h4 style=\"text-align: start;\"><span style=\"font-size: 12px;\">联系我们</span></h4><p style=\"text-align: start;\"><span style=\"font-size: 12px;\">如有任何疑问或建议，欢迎随时与我们联系：<br><br>📧 邮箱：</span><a href=\"mailto:zhuyongzhengs@gmail.com\" target=\"_blank\"><span style=\"font-size: 12px;\">zhuyongzhengs@gmail.com</span></a><span style=\"font-size: 12px;\"><br><br>📞 客服微信：xxx（工作时间：9:00 - 18:00）<br><br>📍 公司地址：中国XX省XX市XX区XX路XX号</span></p><p style=\"text-align: start;\"><span style=\"font-size: 12px;\">感谢您选择【商城名称】，我们将持续努力，为您带来更好的服务与体验！</span></p><hr/><p style=\"text-align: start;\"><span style=\"font-size: 12px;\">如果你有具体的品牌理念、公司背景或团队介绍等内容，也可以告诉我，我可以帮你定制更个性化的内容。</span></p>', '3a0ee03b-0f92-bf1a-2849-ad1d65cac356', 1, 1, 1, '{}', 'ae15a5ae8c7e45b4a58f0505758d1da1', '2023-11-14 22:37:25.023527', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-05-09 18:57:19.910980', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_articles` VALUES ('3a19c66b-77d6-04f3-3432-78c3c212b5ac', NULL, '用户协议', '关于【Rex商城平台】的介绍。', NULL, '<h1 style=\"text-align: start;\"><span style=\"font-size: 12px;\">用户协议</span></h1><p style=\"text-align: start;\"><span style=\"font-size: 12px;\">欢迎使用【Rex商城平台】（以下简称“本平台”）。在您注册、登录或使用本平台提供的服务前，请仔细阅读并理解以下条款。一旦您点击“同意”或以其他方式使用本平台服务，即表示您已充分阅读、理解并接受本协议的所有内容，并与本平台达成协议。</span></p><h2 style=\"text-align: start;\"><span style=\"font-size: 12px;\">一、协议的生效与变更</span></h2><ol><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">本协议自您首次使用本平台服务之日起生效。</span></li><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">本平台有权在必要时对本协议进行修改或更新，修改后的内容将在平台上公布即视为通知，若您继续使用本平台服务，则视为您接受修订后的协议。</span></li></ol><h2 style=\"text-align: start;\"><span style=\"font-size: 12px;\">二、用户注册与账户管理</span></h2><ol><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">您在使用本平台服务前需通过微信授权登录，并确保提供真实、准确、完整的个人信息。</span></li><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">您应妥善保管账号及密码信息，因泄露导致的损失由您自行承担。</span></li><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">您不得将账号转让、出借或以其他方式交由他人使用。</span></li></ol><h2 style=\"text-align: start;\"><span style=\"font-size: 12px;\">三、商品与交易</span></h2><ol><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">本平台展示的商品信息仅供参考，具体价格、库存、描述等以订单提交时为准。</span></li><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">所有交易均通过微信支付或其他本平台支持的支付方式进行，交易成功后不可无理由取消订单。</span></li><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">若商品存在质量问题或与描述不符，您可在收到商品后依照本平台的退换货政策申请售后服务。</span></li></ol><h2 style=\"text-align: start;\"><span style=\"font-size: 12px;\">四、隐私政策</span></h2><ol><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">本平台尊重并保护用户的个人隐私。我们仅在为您提供服务和优化用户体验的过程中收集、使用您的相关信息。</span></li><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">我们不会向第三方出售或泄露您的个人信息，除非获得您的明确授权或法律法规另有规定。</span></li><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">您可通过《隐私政策》了解更多关于信息收集与使用的细节。</span></li></ol><h2 style=\"text-align: start;\"><span style=\"font-size: 12px;\">五、责任限制</span></h2><ol><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">本平台力求为用户提供准确、完整的信息和服务，但不保证所有内容的绝对准确性、完整性或可用性。</span></li><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">在法律允许的最大范围内，因使用本平台服务所造成的直接或间接损失，本平台不承担赔偿责任。</span></li></ol><h2 style=\"text-align: start;\"><span style=\"font-size: 12px;\">六、知识产权</span></h2><ol><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">本平台上的所有内容（包括但不限于文字、图片、视频、界面设计等）均属于本平台或其合法授权方所有，受相关知识产权法律保护。</span></li><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">未经许可，任何人不得复制、传播、修改或用于其他商业用途。</span></li></ol><h2 style=\"text-align: start;\"><span style=\"font-size: 12px;\">七、违约处理</span></h2><ol><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">若用户违反本协议任何条款，本平台有权采取警告、暂停或终止其账号使用权限等措施。</span></li><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">对于恶意行为（如刷单、虚假交易、攻击系统等），本平台保留依法追究法律责任的权利。</span></li></ol><h2 style=\"text-align: start;\"><span style=\"font-size: 12px;\">八、争议解决</span></h2><ol><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">本协议适用中华人民共和国法律。</span></li><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">因本协议引起的或与之相关的任何争议，双方应友好协商解决；协商不成的，任一方可向本平台所在地人民法院提起诉讼。</span></li></ol><h2 style=\"text-align: start;\"><span style=\"font-size: 12px;\">九、其他</span></h2><ol><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">本协议部分条款无效不影响其他条款的效力。</span></li><li style=\"text-align: start;\"><span style=\"font-size: 12px;\">本协议最终解释权归【XXX】所有。</span></li></ol><p style=\"text-align: start;\"><span style=\"font-size: 12px;\">感谢您对【Rex商城平台】的支持与信任！</span></p>', '3a10908c-7e48-88aa-9739-9c39787e2345', 1, 1, 1, '{}', 'b3cac9e409be4c1c8a0d5a94acd44295', '2025-05-09 16:52:05.210207', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-05-10 22:51:28.743794', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_articles` VALUES ('3a19c671-606b-71b3-8b38-c959bf8a9872', NULL, '隐私政策', '关于【Rex商城平台】的介绍。', NULL, '<p><span style=\"font-size: 12px;\"><strong>隐私政策</strong></span></p><p><span style=\"font-size: 12px;\">尊敬的用户：</span></p><p><span style=\"font-size: 12px;\">感谢您使用【Rex商城平台】（以下简称“本平台”或“我们”）。我们非常重视您的个人信息安全与隐私保护，并承诺遵守相关法律法规，采取合理措施保障您的信息安全。在您使用本平台服务前，请仔细阅读并理解本《隐私政策》的内容。</span></p><p><br></p><p><span style=\"font-size: 12px;\">一、适用范围</span></p><p><span style=\"font-size: 12px;\">本政策适用于您通过微信小程序访问和使用本平台提供的所有服务过程中所提供的个人信息及相关行为。</span></p><p><br></p><p><span style=\"font-size: 12px;\">二、我们收集的信息</span></p><p><span style=\"font-size: 12px;\">为向您提供商品展示、下单购买、售后服务等功能，我们可能会收集以下信息：</span></p><p><span style=\"font-size: 12px;\">1. 账户与身份信息：</span></p><p><span style=\"font-size: 12px;\">微信授权登录后的昵称、头像、OpenID；</span></p><p><span style=\"font-size: 12px;\">您注册或完善资料时填写的手机号、收货地址、姓名、性别等；</span></p><p><span style=\"font-size: 12px;\">实名认证信息（如涉及支付或开票功能）；</span></p><p><span style=\"font-size: 12px;\">2. 设备与网络信息：</span></p><p><span style=\"font-size: 12px;\">设备型号、操作系统版本、设备识别码（如IMEI、IDFA）、IP地址、网络类型等；</span></p><p><span style=\"font-size: 12px;\">使用本平台的时间、频率、浏览记录、点击行为等；</span></p><p><span style=\"font-size: 12px;\">3. 交易与订单信息：</span></p><p><span style=\"font-size: 12px;\">商品浏览记录、加入购物车、提交订单、支付状态、物流信息等；</span></p><p><span style=\"font-size: 12px;\">支付方式、交易金额、退款记录等；</span></p><p><span style=\"font-size: 12px;\">4. 其他信息：</span></p><p><span style=\"font-size: 12px;\">您通过客服咨询时提供的文字、语音、图片等内容；</span></p><p><span style=\"font-size: 12px;\">用户反馈、评价、投诉建议等；</span></p><p><br></p><p><span style=\"font-size: 12px;\">三、我们如何使用这些信息</span></p><p><span style=\"font-size: 12px;\">我们仅在为您提供服务和优化用户体验的前提下使用您的个人信息，具体用途包括：</span></p><p><span style=\"font-size: 12px;\">提供商品展示、下单、支付、配送、售后等服务；</span></p><p><span style=\"font-size: 12px;\">完成订单处理、物流跟踪、客户服务；</span></p><p><span style=\"font-size: 12px;\">向您发送订单状态、促销活动、通知公告等信息（您可选择退订）；</span></p><p><span style=\"font-size: 12px;\">进行数据分析和个性化推荐，提升服务质量；</span></p><p><span style=\"font-size: 12px;\">用于身份验证、账户安全、防止欺诈和恶意行为；</span></p><p><span style=\"font-size: 12px;\">遵守法律法规要求，配合司法机关调查取证（如适用）；</span></p><p><br></p><p><span style=\"font-size: 12px;\">四、我们是否会共享或披露您的信息</span></p><p><span style=\"font-size: 12px;\">我们不会向任何无关第三方出售或泄露您的个人信息，除非以下情形之一：</span></p><p><span style=\"font-size: 12px;\">获得您的明确同意：如参与第三方合作活动，我们会提前告知并征得您的同意；</span></p><p><span style=\"font-size: 12px;\">为履行服务所必需：如将订单信息提供给物流公司、支付机构等合作伙伴；</span></p><p><span style=\"font-size: 12px;\">法律要求或政府机关依法调取：如公安机关依法调取涉嫌犯罪的相关数据；</span></p><p><span style=\"font-size: 12px;\">为维护平台安全与合法权益：如发现异常交易、恶意刷单等行为时向有关机构报告；</span></p><p><br></p><p><span style=\"font-size: 12px;\">五、我们如何保护您的信息</span></p><p><span style=\"font-size: 12px;\">我们采用符合行业标准的技术手段（如SSL加密、数据库权限控制等）保护您的信息安全；</span></p><p><span style=\"font-size: 12px;\">对员工进行保密培训，严格限制内部人员对用户信息的访问权限；</span></p><p><span style=\"font-size: 12px;\">定期检查安全系统，防范信息泄露、篡改或丢失；</span></p><p><span style=\"font-size: 12px;\">若发生数据泄露事件，我们将第一时间通知受影响用户并上报监管部门；</span></p><p><br></p><p><span style=\"font-size: 12px;\">六、Cookie与同类技术</span></p><p><span style=\"font-size: 12px;\">本平台目前不主动使用Cookie或其他追踪技术，但可能因接入微信SDK或其他第三方服务而存在相关行为。具体请参考微信官方隐私政策或第三方服务条款。</span></p><p><br></p><p><span style=\"font-size: 12px;\">七、未成年人保护</span></p><p><span style=\"font-size: 12px;\">若您未满18周岁，请在监护人指导下使用本平台服务。我们不会主动收集未成年人的个人信息，若监护人发现孩子信息被非法收集，请联系我们删除。</span></p><p><br></p><p><span style=\"font-size: 12px;\">八、您对个人信息的权利</span></p><p><span style=\"font-size: 12px;\">您有权随时行使以下权利：</span></p><p><br></p><p><span style=\"font-size: 12px;\">查阅：查看您在本平台留存的个人信息；</span></p><p><span style=\"font-size: 12px;\">更正：修改不准确或不完整的个人信息；</span></p><p><span style=\"font-size: 12px;\">删除：申请删除您的账户信息（需满足一定条件，如无未完成订单）；</span></p><p><span style=\"font-size: 12px;\">撤回授权：可通过微信设置取消对本平台的部分权限；</span></p><p><span style=\"font-size: 12px;\">注销账号：联系客服或通过平台入口申请注销账户；</span></p><p><span style=\"font-size: 12px;\">如需行使上述权利，请通过以下方式联系我们：</span></p><p><br></p><p><span style=\"font-size: 12px;\">📧 邮箱：【填写客服邮箱】</span></p><p><br></p><p><span style=\"font-size: 12px;\">📞 客服电话：【填写联系电话】</span></p><p><br></p><p><span style=\"font-size: 12px;\">💬 微信小程序内“我的 - 设置 - 帮助与客服”中提交请求</span></p><p><br></p><p><span style=\"font-size: 12px;\">九、政策更新</span></p><p><span style=\"font-size: 12px;\">我们可能会根据业务发展、法律法规变化或监管要求更新本隐私政策。修订后的内容将在平台上公布即视为通知，若您继续使用本平台服务，则视为您接受修订后的政策。</span></p><p><br></p><p><span style=\"font-size: 12px;\">十、联系我们</span></p><p><span style=\"font-size: 12px;\">如有任何关于隐私问题的疑问、投诉或建议，请随时与我们联系：</span></p><p><span style=\"font-size: 12px;\">公司名称： XXXXX</span></p><p><span style=\"font-size: 12px;\">注册地址： XXXXX</span></p><p><span style=\"font-size: 12px;\">客服邮箱： XXXXX</span></p><p><span style=\"font-size: 12px;\">客服电话： XXXXX</span></p><p><br></p><p><span style=\"font-size: 12px;\">感谢您对【Rex商城平台】的信任与支持！</span></p>', '3a10bef6-254d-9fea-954d-7b99f3e8f0c4', 3, 1, 2, '{}', '44d054ee482a4247b5a481fe8c8beae1', '2025-05-09 16:58:32.428646', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-05-10 22:51:31.970040', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_articles` VALUES ('3a1b7cdf-93cb-e1bd-b449-5cadc37440cb', NULL, 'AGGG2', NULL, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250802201230/suspend_1754127487184.png', '<p>STSTSTST</p>', '3a0ee03b-0f92-bf1a-2849-ad1d65cac356', 1, 0, 0, '{}', 'f79d37491c49473aaa87ce283cffc462', '2025-08-02 20:12:35.154557', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 20:12:41.945713', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 20:12:41.945370');

-- ----------------------------
-- Table structure for gd_articletypes
-- ----------------------------
DROP TABLE IF EXISTS `gd_articletypes`;
CREATE TABLE `gd_articletypes`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ParentId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Sort` int NOT NULL,
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
-- Records of gd_articletypes
-- ----------------------------
INSERT INTO `gd_articletypes` VALUES ('3a0ee03b-0f92-bf1a-2849-ad1d65cac356', NULL, '关于我们', NULL, 1, '{}', '51544beb2659459ca4291197994cc291', '2023-11-14 21:38:40.500477', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-05-09 16:23:30.241054', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_articletypes` VALUES ('3a10908c-5c8f-1915-5e3b-6bd8eb6bcbd4', NULL, '小说', NULL, 2, '{}', '646ebfe86e1e41929d52af571d45da74', '2024-02-06 12:23:25.591296', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-05-09 12:22:36.195322', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-05-09 12:22:36.194909');
INSERT INTO `gd_articletypes` VALUES ('3a10908c-7e48-88aa-9739-9c39787e2345', NULL, '用户协议', NULL, 2, '{}', '8eb0e3de5a59401eaf44026ea3fe4f4f', '2024-02-06 12:23:34.218444', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-05-09 16:23:41.332090', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_articletypes` VALUES ('3a10bef6-254d-9fea-954d-7b99f3e8f0c4', NULL, '隐私政策', NULL, 3, '{}', '960bbf0fc2a14327a2f69b730266e423', '2024-02-15 12:41:30.211473', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-05-09 16:23:54.598359', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_articletypes` VALUES ('3a1b7cdf-caf0-4f61-9790-8849ff58e885', NULL, 'TT2', NULL, 0, '{}', 'c450f1bb865144608cda74d1c7d50a63', '2025-08-02 20:12:49.273270', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 20:12:54.142268', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 20:12:54.141967');

-- ----------------------------
-- Table structure for gd_brands
-- ----------------------------
DROP TABLE IF EXISTS `gd_brands`;
CREATE TABLE `gd_brands`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LogoImageUrl` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Sort` int NULL DEFAULT NULL,
  `IsShow` tinyint(1) NOT NULL,
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
  UNIQUE INDEX `IX_Gd_Brands_Name`(`Name`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_brands
-- ----------------------------
INSERT INTO `gd_brands` VALUES ('3a1be87b-a23f-dfd7-c7b1-c62667016f4b', NULL, '褚橙', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823174214/褚橙logo.jpg', 100, 1, '{}', '358a0aea671545d2ad51adec01b23c21', '2025-08-23 17:42:24.584679', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 17:46:00.226745', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_brands` VALUES ('3a1be87e-e02c-8dfb-832b-afa30fc1dc4b', NULL, '赣南脐橙', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823174550/赣南脐橙.jpg', 100, 1, '{}', 'e1542086649a41f0b40edd3791f0808f', '2025-08-23 17:45:57.038414', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 17:53:11.066646', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_brands` VALUES ('3a1be885-78f0-5d69-52af-4fdacd0dbaa7', NULL, '华圣苹果', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823175305/SH.png', 20, 1, '{}', '708f048f04d54306b85076d9abe077d8', '2025-08-23 17:53:09.361387', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 18:00:11.715589', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_brands` VALUES ('3a1be888-6a3f-8813-7e44-8508bcba0d82', NULL, '百果园', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823175618/百果园.jpg', 10, 1, '{}', 'c516b528154a4b6daec3fa66c2e4134b', '2025-08-23 17:56:22.208244', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 17:57:51.320837', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_brands` VALUES ('3a1be889-be75-488a-dda8-625246f9ea35', NULL, '欢乐果园', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823175748/c7158a5cd066f32e9f36bee5b96f5da7.jpg', 10, 1, '{}', 'd874e5b378124d20adbe411c6c643d96', '2025-08-23 17:57:49.302443', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 17:58:14.608856', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_brands` VALUES ('3a1be88b-d16d-82e9-45ca-cf14dd293c34', NULL, '鲜丰水果', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823175959/鲜丰水果.png', 20, 1, '{}', '7acc009462b8452aa1cdc4639c48f65b', '2025-08-23 18:00:05.229868', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 18:00:42.303577', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);

-- ----------------------------
-- Table structure for gd_formitems
-- ----------------------------
DROP TABLE IF EXISTS `gd_formitems`;
CREATE TABLE `gd_formitems`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Type` int NULL DEFAULT NULL,
  `ValidationType` int NULL DEFAULT NULL,
  `Value` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DefaultValue` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `FormId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Required` tinyint(1) NOT NULL,
  `Sort` int NOT NULL,
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
  UNIQUE INDEX `IX_Gd_FormItems_Name`(`Name`) USING BTREE,
  INDEX `IX_Gd_FormItems_FormId`(`FormId`) USING BTREE,
  CONSTRAINT `FK_Gd_FormItems_Gd_Forms_FormId` FOREIGN KEY (`FormId`) REFERENCES `gd_forms` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_formitems
-- ----------------------------

-- ----------------------------
-- Table structure for gd_forms
-- ----------------------------
DROP TABLE IF EXISTS `gd_forms`;
CREATE TABLE `gd_forms`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Type` int NOT NULL,
  `Sort` int NOT NULL,
  `Images` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `VideoPath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `HeadType` int NOT NULL,
  `HeadTypeValue` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `HeadTypeVideo` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ButtonName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ButtonColor` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsLogin` tinyint(1) NOT NULL,
  `Times` int NOT NULL,
  `Qrcode` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ReturnMsg` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `EndDateTime` datetime(6) NOT NULL,
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
-- Records of gd_forms
-- ----------------------------

-- ----------------------------
-- Table structure for gd_goodbrowsings
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodbrowsings`;
CREATE TABLE `gd_goodbrowsings`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `GoodId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GoodName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  `Image` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_goodbrowsings
-- ----------------------------
INSERT INTO `gd_goodbrowsings` VALUES ('3a1be9bf-e28b-a3df-8d1e-5b7131f2ec2f', NULL, '3a1be90a-804f-3ea5-ad0b-113f581c0d93', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', '烟台黄元帅苹果新鲜水果粉面苹果当季孕妇老人常备5斤中果【10-12个】', '{}', '51660ea82fd044c8958f98827f2c65f1', '2025-08-23 23:36:34.699902', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', NULL, NULL, 0, NULL, NULL, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200319/黄元帅.png');
INSERT INTO `gd_goodbrowsings` VALUES ('3a1be9c1-5526-0bb6-bfe9-d2dd6e73dfa2', NULL, '3a1be8f5-77c6-abfd-9f34-bbbc0419b30f', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', '华圣【筑梦太空礼盒】2.4kg 航天级生鲜礼盒 精品原生态红富士苹果 筑梦太空礼盒', '{}', '4fa97b9dfa984c66920a021daa115a40', '2025-08-23 23:38:09.574908', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', NULL, NULL, 0, NULL, NULL, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823195640/华圣苹果-1.png');
INSERT INTO `gd_goodbrowsings` VALUES ('3a1be9c5-2564-ffa6-8942-5c5a41733b36', NULL, '3a1be913-be5c-9884-7a18-17fb9b8a1860', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', '广西小米蕉当季新鲜应季水果日日鲜香甜小香蕉芭蕉孕妇水果整箱 净重9斤 酸甜口的小香蕉', '{}', '85cf79adab804ac48b74320c0662dae7', '2025-08-23 23:42:19.492397', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', NULL, NULL, 0, NULL, NULL, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202537/GXXMJ.png');

-- ----------------------------
-- Table structure for gd_goodcategoryextends
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodcategoryextends`;
CREATE TABLE `gd_goodcategoryextends`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `GoodId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `GoodCategroyId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
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
-- Records of gd_goodcategoryextends
-- ----------------------------
INSERT INTO `gd_goodcategoryextends` VALUES ('3a1be90a-8056-0316-5a15-f7cfb0ede9e1', NULL, '3a1be90a-804f-3ea5-ad0b-113f581c0d93', '3a1be88e-a156-a5ce-539e-e58782fe265f', '{}', '0a34e6bcc5244b22ae35d38647b3b74b', '2025-08-23 20:18:27.591204', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_goodcategoryextends` VALUES ('3a1be90a-8056-05a4-0f4c-8948e27c5049', NULL, '3a1be90a-804f-3ea5-ad0b-113f581c0d93', '3a1be8bd-f336-b051-ca4a-e21e38219a4f', '{}', 'ae5cfd4079bc498ea07ca063e6dab484', '2025-08-23 20:18:27.591913', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_goodcategoryextends` VALUES ('3a1be90a-8056-f2f0-ba41-660bd4d42d44', NULL, '3a1be90a-804f-3ea5-ad0b-113f581c0d93', '3a1be88d-9037-6c75-6233-625efe7004f7', '{}', '8d59183bcd2845a594d82be6d1b89d0f', '2025-08-23 20:18:27.589630', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for gd_goodcategorys
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodcategorys`;
CREATE TABLE `gd_goodcategorys`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `ParentId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TypeId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Sort` int NOT NULL,
  `ImageUrl` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsShow` tinyint(1) NOT NULL,
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
-- Records of gd_goodcategorys
-- ----------------------------
INSERT INTO `gd_goodcategorys` VALUES ('3a1be88d-9037-6c75-6233-625efe7004f7', NULL, NULL, '苹果', '00000000-0000-0000-0000-000000000000', 1, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823183723/PG2.jpg', 1, '{}', 'c21a18916a154007b9938dd458a21853', '2025-08-23 18:01:59.613692', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 18:37:24.656663', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be88d-e9e4-e9c2-5475-f22ade4bb224', NULL, NULL, '橙子', '00000000-0000-0000-0000-000000000000', 2, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823185725/CC.png', 1, '{}', '90354ba1ddc24d1197ce948223d84082', '2025-08-23 18:02:22.567947', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 18:57:26.312444', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be88e-00ad-6940-cc92-c2e63de6b84d', NULL, NULL, '香蕉', '00000000-0000-0000-0000-000000000000', 3, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823191222/XJ.png', 1, '{}', 'c1f895d7570840a09355752ad124ee59', '2025-08-23 18:02:28.400622', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 19:12:23.298817', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be88e-6994-a6b4-27b6-955245cd95ec', NULL, '3a1be88d-9037-6c75-6233-625efe7004f7', '红富士系', '00000000-0000-0000-0000-000000000000', 0, '', 1, '{}', '8a6dc6ae5b5f492da99c0780f6c1ae48', '2025-08-23 18:02:55.256746', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 19:09:03.212352', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be88e-a156-a5ce-539e-e58782fe265f', NULL, '3a1be88d-9037-6c75-6233-625efe7004f7', '金冠系', '00000000-0000-0000-0000-000000000000', 0, '', 1, '{}', '52e5e0c303b7400ea45bb7966e31c52b', '2025-08-23 18:03:09.528490', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 19:09:11.138323', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be88e-e58a-7068-936c-23353ef86521', NULL, '3a1be88e-6994-a6b4-27b6-955245cd95ec', '烟台红富士', '00000000-0000-0000-0000-000000000000', 0, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823185059/HFS.png', 1, '{}', 'eca1ef2585ba4b2ca1dc9b40d53b4413', '2025-08-23 18:03:26.990859', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 18:51:02.435207', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be88f-1aac-aff4-a5bb-514dcde763b7', NULL, '3a1be88e-6994-a6b4-27b6-955245cd95ec', '洛川红富士', '00000000-0000-0000-0000-000000000000', 0, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823185254/LCHFS.png', 1, '{}', '745e45b7b24d482c920501898538c0c2', '2025-08-23 18:03:40.593134', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 18:52:55.902457', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be890-d8ce-32fa-2aeb-e806b32110fc', NULL, '3a1be88d-e9e4-e9c2-5475-f22ade4bb224', '褚橙', '00000000-0000-0000-0000-000000000000', 0, NULL, 1, '{}', '497f948743904401abde4e054bf01e99', '2025-08-23 18:05:34.801282', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 18:58:13.594616', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be893-61e5-4943-6675-7ee2c13450e9', NULL, '3a1be890-d8ce-32fa-2aeb-e806b32110fc', '冰糖橙', '00000000-0000-0000-0000-000000000000', 0, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823190004/CC.png', 1, '{}', '10c8dcde55214691985733182b54cd6b', '2025-08-23 18:08:20.969578', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 19:19:00.893922', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be893-85fa-96e1-d086-bd13c1f9ceef', NULL, '3a1be88e-00ad-6940-cc92-c2e63de6b84d', '小米蕉', '00000000-0000-0000-0000-000000000000', 0, '', 1, '{}', '63024f3d03f948c69cc61e562fd033d7', '2025-08-23 18:08:30.205300', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 19:18:31.667494', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be893-be02-9fa4-a87e-6ec431768f82', NULL, '3a1be893-85fa-96e1-d086-bd13c1f9ceef', '红河小米蕉', '00000000-0000-0000-0000-000000000000', 0, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823212632/XMJ2.jpeg', 1, '{}', '39229950b8924487b22125a7fd96df45', '2025-08-23 18:08:44.549882', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 21:26:33.651782', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be8bd-f336-b051-ca4a-e21e38219a4f', NULL, '3a1be88e-a156-a5ce-539e-e58782fe265f', '黄元帅苹果', '00000000-0000-0000-0000-000000000000', 0, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823185448/HYS.png', 1, '{}', 'bdb361e2f95d4ef8855ed0130cbfba7e', '2025-08-23 18:54:50.680523', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 19:31:33.910506', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be8c2-7538-da77-e172-570c9ed96ae8', NULL, '3a1be88d-e9e4-e9c2-5475-f22ade4bb224', '赣南脐橙', '00000000-0000-0000-0000-000000000000', 0, '', 1, '{}', '34b5f6e5d81c4f4589c23824ce39bb52', '2025-08-23 18:59:46.108550', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 19:09:20.532876', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be8c9-78b0-8657-34f8-f81cb9088955', NULL, '3a1be8c2-7538-da77-e172-570c9ed96ae8', '赣州橙子', '00000000-0000-0000-0000-000000000000', 0, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823190724/CCc.png', 1, '{}', '5bfe3ef0426d44799dbb42fb5c2e258d', '2025-08-23 19:07:25.747751', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 19:31:31.827083', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be8d0-0761-ad83-12c7-bbf48a424ee1', NULL, '3a1be88e-00ad-6940-cc92-c2e63de6b84d', '皇帝蕉', '00000000-0000-0000-0000-000000000000', 0, '', 1, '{}', '251311f78e3f48449d7298ad9b5a8801', '2025-08-23 19:14:35.494050', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 19:33:37.021309', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be8d0-5d1c-3683-410f-d85789e36e1a', NULL, '3a1be88e-00ad-6940-cc92-c2e63de6b84d', '红香蕉', '00000000-0000-0000-0000-000000000000', 0, '', 1, '{}', '5079b09bcffd401c9955247fcad89b08', '2025-08-23 19:14:57.440080', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 19:36:08.088379', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be8e1-5dde-734e-4de3-5b32bda97725', NULL, '3a1be8d0-0761-ad83-12c7-bbf48a424ee1', '乐东皇蕉', '00000000-0000-0000-0000-000000000000', 0, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823193330/HDJ.png', 1, '{}', 'c19f251b29d04efb8b1887e3362342fd', '2025-08-23 19:33:31.744996', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 19:35:11.314682', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be8e3-a979-b26c-9532-8650515b5a32', NULL, '3a1be8d0-5d1c-3683-410f-d85789e36e1a', '三亚红蕉', '00000000-0000-0000-0000-000000000000', 0, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823193600/HXJ.png', 1, '{}', 'bc5b7dd602e74450b2b00eed299a2210', '2025-08-23 19:36:02.173988', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 19:36:04.531534', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodcategorys` VALUES ('3a1be941-43c7-fc38-b64f-996ef259479e', NULL, '3a1be893-85fa-96e1-d086-bd13c1f9ceef', '广西小米蕉', '00000000-0000-0000-0000-000000000000', 1, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823191825/XMJ.png', 1, '{}', '4583f944feb947d6a8c959f93d8fda03', '2025-08-23 21:18:16.544418', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 21:18:44.888186', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);

-- ----------------------------
-- Table structure for gd_goodcollections
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodcollections`;
CREATE TABLE `gd_goodcollections`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `GoodId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GoodName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  `Image` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_goodcollections
-- ----------------------------

-- ----------------------------
-- Table structure for gd_goodcomments
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodcomments`;
CREATE TABLE `gd_goodcomments`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `CommentId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Score` int NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GoodId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `OrderId` char(50) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Addon` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Images` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ContentBody` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `SellerContent` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `IsDisplay` tinyint(1) NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  `Avatar` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `UserName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_goodcomments
-- ----------------------------

-- ----------------------------
-- Table structure for gd_goodgrades
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodgrades`;
CREATE TABLE `gd_goodgrades`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `GoodId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GradeId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GradePrice` decimal(65, 30) NOT NULL,
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
-- Records of gd_goodgrades
-- ----------------------------
INSERT INTO `gd_goodgrades` VALUES ('3a1be8f5-77f1-183a-cfef-5ef69096e3d5', NULL, '3a1be8f5-77c6-abfd-9f34-bbbc0419b30f', '3a0d4972-872c-9198-d9f6-d4e3bba35f5e', 5.000000000000000000000000000000, '{}', '72f6ab969b934e0e95b00c80dff9c6f0', '2025-08-23 19:55:29.200930', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_goodgrades` VALUES ('3a1be8f5-77f1-a34a-bd8f-b82797b39c5e', NULL, '3a1be8f5-77c6-abfd-9f34-bbbc0419b30f', '3a0d4971-3e6d-9c9e-7027-a8e776cef762', 0.000000000000000000000000000000, '{}', 'b1012c603bc0406ebe3e9f60126ec975', '2025-08-23 19:55:29.203824', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_goodgrades` VALUES ('3a1be8f5-77f1-c073-2f33-e492a42e405d', NULL, '3a1be8f5-77c6-abfd-9f34-bbbc0419b30f', '3a0d4972-e6b6-93ef-45eb-bbca4ae40270', 10.000000000000000000000000000000, '{}', '545a7838075e407a833134119fa3660e', '2025-08-23 19:55:29.197729', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_goodgrades` VALUES ('3a1be90a-808d-2226-336f-184228e1650e', NULL, '3a1be90a-804f-3ea5-ad0b-113f581c0d93', '3a0d4972-e6b6-93ef-45eb-bbca4ae40270', 20.000000000000000000000000000000, '{}', 'ebf5b7a58e5149929bed9f8e9c63844b', '2025-08-23 20:18:27.597250', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_goodgrades` VALUES ('3a1be90a-808d-24b5-5f33-eb4ededb5f5f', NULL, '3a1be90a-804f-3ea5-ad0b-113f581c0d93', '3a0d4972-872c-9198-d9f6-d4e3bba35f5e', 10.000000000000000000000000000000, '{}', 'fa2c51f778804d9d9874a74c4bb15b5e', '2025-08-23 20:18:27.597573', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_goodgrades` VALUES ('3a1be90a-808d-df91-d123-40223620199a', NULL, '3a1be90a-804f-3ea5-ad0b-113f581c0d93', '3a0d4971-3e6d-9c9e-7027-a8e776cef762', 0.000000000000000000000000000000, '{}', 'd2f45b5a10fc43a1ad72d3c01c9e0d17', '2025-08-23 20:18:27.597779', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_goodgrades` VALUES ('3a1be913-be60-2d8f-c5ee-a8a1188f4cb8', NULL, '3a1be913-be5c-9884-7a18-17fb9b8a1860', '3a0d4972-e6b6-93ef-45eb-bbca4ae40270', 5.000000000000000000000000000000, '{}', '1cbc19e15c3941bf919be5674619613f', '2025-08-23 20:28:33.248633', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_goodgrades` VALUES ('3a1be913-be60-8cd8-ea79-a4358bea0a3c', NULL, '3a1be913-be5c-9884-7a18-17fb9b8a1860', '3a0d4971-3e6d-9c9e-7027-a8e776cef762', 0.000000000000000000000000000000, '{}', '4c84c1979be740369ad324d5ce63aaf6', '2025-08-23 20:28:33.249120', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_goodgrades` VALUES ('3a1be913-be60-9a7b-02e9-3c9726beb2a5', NULL, '3a1be913-be5c-9884-7a18-17fb9b8a1860', '3a0d4972-872c-9198-d9f6-d4e3bba35f5e', 2.000000000000000000000000000000, '{}', '84e47c41c2564be898a03e13d47b7de3', '2025-08-23 20:28:33.248870', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for gd_goodimages
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodimages`;
CREATE TABLE `gd_goodimages`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `GoodId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `ImageId` char(50) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Sort` int NOT NULL,
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
-- Records of gd_goodimages
-- ----------------------------

-- ----------------------------
-- Table structure for gd_goodparams
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodparams`;
CREATE TABLE `gd_goodparams`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Type` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
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
  UNIQUE INDEX `IX_Gd_GoodParams_Name`(`Name`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_goodparams
-- ----------------------------
INSERT INTO `gd_goodparams` VALUES ('3a1be915-10d3-a502-0304-263a07d5d162', NULL, '产地', NULL, 'Text', '{}', 'a7dd9914b123436181252eba9e22a51b', '2025-08-23 20:29:59.950221', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for gd_goods
-- ----------------------------
DROP TABLE IF EXISTS `gd_goods`;
CREATE TABLE `gd_goods`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `BarCode` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Brief` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Image` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Images` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Video` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ProductsDistributionType` int NOT NULL,
  `GoodCategoryId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GoodTypeId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GoodSkuIds` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `GoodParamsIds` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `BrandId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `IsNomalVirtual` tinyint(1) NOT NULL,
  `IsMarketable` tinyint(1) NOT NULL,
  `Unit` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Intro` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `SpesDesc` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Parameters` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CommentsCount` int NOT NULL,
  `ViewCount` int NOT NULL,
  `BuyCount` int NOT NULL,
  `Uptime` datetime(6) NULL DEFAULT NULL,
  `Downtime` datetime(6) NULL DEFAULT NULL,
  `Sort` int NOT NULL,
  `LabelIds` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `NewSpec` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `OpenSpec` int NOT NULL,
  `IsRecommend` tinyint(1) NOT NULL,
  `IsHot` tinyint(1) NOT NULL,
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
  UNIQUE INDEX `IX_Gd_Goods_BarCode`(`BarCode`) USING BTREE,
  INDEX `IX_Gd_Goods_BrandId`(`BrandId`) USING BTREE,
  INDEX `IX_Gd_Goods_GoodCategoryId`(`GoodCategoryId`) USING BTREE,
  CONSTRAINT `FK_Gd_Goods_Gd_Brands_BrandId` FOREIGN KEY (`BrandId`) REFERENCES `gd_brands` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_Gd_Goods_Gd_GoodCategorys_GoodCategoryId` FOREIGN KEY (`GoodCategoryId`) REFERENCES `gd_goodcategorys` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_goods
-- ----------------------------
INSERT INTO `gd_goods` VALUES ('3a1be8f5-77c6-abfd-9f34-bbbc0419b30f', NULL, 'GD1755949056511', '华圣【筑梦太空礼盒】2.4kg 航天级生鲜礼盒 精品原生态红富士苹果 筑梦太空礼盒', '中国载人航天工程鲜食水果保供单位。', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823195640/华圣苹果-1.png', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823194923/华圣苹果-1.png;http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823194925/华圣苹果-2.png;http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823194928/华圣苹果-3.png', '', 1, '3a1be88e-e58a-7068-936c-23353ef86521', '00000000-0000-0000-0000-000000000000', NULL, NULL, '3a1be885-78f0-5d69-52af-4fdacd0dbaa7', 0, 1, '斤', '<p><img src=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823195411/D01.png\" alt=\"\" data-href=\"\" style=\"\"/></p><p><img src=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823195431/D02.png\" alt=\"\" data-href=\"\" style=\"\"/></p><p><img src=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823195440/D03.png\" alt=\"\" data-href=\"\" style=\"\"/></p>', NULL, NULL, 0, 8, 0, NULL, NULL, 10, NULL, NULL, 0, 1, 1, '{}', '4614b281408e4e9487a1775e6b647e8e', '2025-08-23 19:55:29.095460', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 23:38:09.569903', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 0, NULL, NULL);
INSERT INTO `gd_goods` VALUES ('3a1be90a-804f-3ea5-ad0b-113f581c0d93', NULL, 'GD1755950492353', '烟台黄元帅苹果新鲜水果粉面苹果当季孕妇老人常备5斤中果【10-12个】', '新鲜水果粉面苹果。', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200319/黄元帅.png', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200535/黄元帅.png;http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200538/D01.png;http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200541/D02.png;http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200544/D03.png', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200553/HYS.mp4', 1, '3a1be8bd-f336-b051-ca4a-e21e38219a4f', '00000000-0000-0000-0000-000000000000', '3a1be903-ea0c-913c-6262-7424e391295e,3a1be904-f4eb-0545-795f-0d45f420a3f8', '', '3a1be888-6a3f-8813-7e44-8508bcba0d82', 0, 1, '斤', '<p><img src=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202019/A01.jpg\" alt=\"\" data-href=\"\" style=\"\"/></p><p><img src=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202025/A02.jpg\" alt=\"\" data-href=\"\" style=\"\"/></p><p><img src=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202035/A03.jpg\" alt=\"\" data-href=\"\" style=\"\"/></p><p><img src=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202041/A04.jpg\" alt=\"\" data-href=\"\" style=\"\"/></p><p><br></p>', '3a1be903-ea0c-0e4c-1a89-76ee0a638599.精选:5斤|3a1be903-ea0c-47a4-92b1-8956b4b72d16.精选:9斤|3a1be904-f4eb-2d9e-e767-8761ba495a7a.中果:15-20个|3a1be904-f4eb-70f4-df9b-a02da0f4e3ee.中果:10-12个', '', 0, 7, 0, NULL, NULL, 1, NULL, '3a1be903-ea0c-0e4c-1a89-76ee0a638599:5斤|3a1be903-ea0c-47a4-92b1-8956b4b72d16:9斤|3a1be904-f4eb-2d9e-e767-8761ba495a7a:15-20个|3a1be904-f4eb-70f4-df9b-a02da0f4e3ee:10-12个', 1, 1, 1, '{}', '8ec0209626a44fe9902a8002251a6232', '2025-08-23 20:18:27.536447', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 23:36:34.695231', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 0, NULL, NULL);
INSERT INTO `gd_goods` VALUES ('3a1be913-be5c-9884-7a18-17fb9b8a1860', NULL, 'GD1755951693837', '广西小米蕉当季新鲜应季水果日日鲜香甜小香蕉芭蕉孕妇水果整箱 净重9斤 酸甜口的小香蕉', '正宗广西小米蕉当季新鲜应季水果日日鲜香甜小香蕉芭蕉孕妇水果整。', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202537/GXXMJ.png', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202541/GXXMJ.png;http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202635/XMJ-2.jpg', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202545/XMJ.mp4', 1, '3a1be941-43c7-fc38-b64f-996ef259479e', '00000000-0000-0000-0000-000000000000', '3a1be903-ea0c-913c-6262-7424e391295e', '3a1be915-10d3-a502-0304-263a07d5d162', '3a1be88b-d16d-82e9-45ca-cf14dd293c34', 0, 1, '斤', '<p><img src=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202745/D01.jpg\" alt=\"\" data-href=\"\" style=\"\"/></p><p><img src=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202753/D02.jpg\" alt=\"\" data-href=\"\" style=\"\"/></p>', '3a1be903-ea0c-47a4-92b1-8956b4b72d16.精选:9斤', '3a1be915-10d3-a502-0304-263a07d5d162:广西', 0, 39, 0, NULL, NULL, 200, NULL, '3a1be903-ea0c-0e4c-1a89-76ee0a638599:5斤|3a1be903-ea0c-47a4-92b1-8956b4b72d16:9斤', 1, 0, 1, '{}', '69520790e2ad488fb082b0f066558b3c', '2025-08-23 20:28:33.245724', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 23:42:19.487431', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 0, NULL, NULL);

-- ----------------------------
-- Table structure for gd_goodtypespecs
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodtypespecs`;
CREATE TABLE `gd_goodtypespecs`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Sort` int NOT NULL,
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
  UNIQUE INDEX `IX_Gd_GoodTypeSpecs_Name`(`Name`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_goodtypespecs
-- ----------------------------
INSERT INTO `gd_goodtypespecs` VALUES ('3a1be900-58f8-4b9f-0a68-f14e425e6779', NULL, 'AA', 0, '{}', '0b80a039d9d545dd9cded4c61bf24055', '2025-08-23 20:07:22.174821', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 20:11:20.021487', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 20:11:20.018472');
INSERT INTO `gd_goodtypespecs` VALUES ('3a1be900-8434-ad74-6c0a-dc3f6bd3e74a', NULL, 'BB', 1, '{}', 'dfa078709f3a407dbcaafa93af45e75e', '2025-08-23 20:07:33.173082', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 20:11:23.630186', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 20:11:23.629422');
INSERT INTO `gd_goodtypespecs` VALUES ('3a1be903-ea0c-913c-6262-7424e391295e', NULL, '精选', 1, '{}', 'fd4c8360476b4a3cae0e11298379e1d2', '0001-01-01 00:00:00.000000', NULL, '2025-08-23 20:53:50.375260', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_goodtypespecs` VALUES ('3a1be904-f4eb-0545-795f-0d45f420a3f8', NULL, '中果', 1, '{}', '6fc6872b64e848529c60ce08153dba69', '2025-08-23 20:12:24.171764', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_goodtypespecs` VALUES ('3a1be906-5473-4d53-e21f-0bd856f1c1a4', NULL, '大果', 2, '{}', 'fb26817f00f149d2897e6dfe35716d9c', '0001-01-01 00:00:00.000000', NULL, '2025-08-23 21:00:45.922588', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);

-- ----------------------------
-- Table structure for gd_goodtypespecvalues
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodtypespecvalues`;
CREATE TABLE `gd_goodtypespecvalues`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `SpecId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Value` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Sort` int NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Gd_GoodTypeSpecValues_SpecId`(`SpecId`) USING BTREE,
  CONSTRAINT `FK_Gd_GoodTypeSpecValues_Gd_GoodTypeSpecs_SpecId` FOREIGN KEY (`SpecId`) REFERENCES `gd_goodtypespecs` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_goodtypespecvalues
-- ----------------------------
INSERT INTO `gd_goodtypespecvalues` VALUES ('3a1be900-58f9-c34b-e458-10ee7eeacd86', NULL, '3a1be900-58f8-4b9f-0a68-f14e425e6779', 'A01', 1);
INSERT INTO `gd_goodtypespecvalues` VALUES ('3a1be900-8434-3881-2919-dc0bb48bf99b', NULL, '3a1be900-8434-ad74-6c0a-dc3f6bd3e74a', 'B01', 1);
INSERT INTO `gd_goodtypespecvalues` VALUES ('3a1be900-8434-c3b4-cdc0-90da0ba8f9eb', NULL, '3a1be900-8434-ad74-6c0a-dc3f6bd3e74a', 'B02', 2);
INSERT INTO `gd_goodtypespecvalues` VALUES ('3a1be903-ea0c-0e4c-1a89-76ee0a638599', NULL, '3a1be903-ea0c-913c-6262-7424e391295e', '5斤', 1);
INSERT INTO `gd_goodtypespecvalues` VALUES ('3a1be903-ea0c-47a4-92b1-8956b4b72d16', NULL, '3a1be903-ea0c-913c-6262-7424e391295e', '9斤', 2);
INSERT INTO `gd_goodtypespecvalues` VALUES ('3a1be904-f4eb-2d9e-e767-8761ba495a7a', NULL, '3a1be904-f4eb-0545-795f-0d45f420a3f8', '15-20个', 2);
INSERT INTO `gd_goodtypespecvalues` VALUES ('3a1be904-f4eb-70f4-df9b-a02da0f4e3ee', NULL, '3a1be904-f4eb-0545-795f-0d45f420a3f8', '10-12个', 1);
INSERT INTO `gd_goodtypespecvalues` VALUES ('3a1be906-5473-43f2-bac0-03ed436d0ead', NULL, '3a1be906-5473-4d53-e21f-0bd856f1c1a4', '5-10斤', 1);
INSERT INTO `gd_goodtypespecvalues` VALUES ('3a1be931-3be7-0a04-7b36-f35dc52ba0e5', NULL, '3a1be906-5473-4d53-e21f-0bd856f1c1a4', '20斤', 2);

-- ----------------------------
-- Table structure for gd_labels
-- ----------------------------
DROP TABLE IF EXISTS `gd_labels`;
CREATE TABLE `gd_labels`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Style` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
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
-- Records of gd_labels
-- ----------------------------

-- ----------------------------
-- Table structure for gd_notices
-- ----------------------------
DROP TABLE IF EXISTS `gd_notices`;
CREATE TABLE `gd_notices`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Title` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ContentBody` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Type` int NULL DEFAULT NULL,
  `Sort` int NULL DEFAULT NULL,
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
-- Records of gd_notices
-- ----------------------------
INSERT INTO `gd_notices` VALUES ('3a1be925-7def-af93-69d7-a228aaf0a25d', NULL, '🎉通知！通知！今早刚到的**洛川红富士**与**海南皇帝蕉**已抢爆！脆甜苹果仅剩30箱，软糯香蕉余50把，售完无补！   速来抢购，手慢无～', '<p>🎉热销通知 🎉</p><p><img src=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200319/%E9%BB%84%E5%85%83%E5%B8%85.png\" alt=\"\" data-href=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200319/黄元帅.png\" style=\"\"/></p><p><img src=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823195640/%E5%8D%8E%E5%9C%A3%E8%8B%B9%E6%9E%9C-1.png\" alt=\"\" data-href=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823195640/华圣苹果-1.png\" style=\"\"/></p><p><img src=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202537/GXXMJ.png\" alt=\"\" data-href=\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202537/GXXMJ.png\" style=\"\"/></p>', NULL, 1, '{}', '7cac4004dc744351aefa7d7b59b82077', '2025-08-23 20:47:56.406508', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 22:48:15.645661', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);

-- ----------------------------
-- Table structure for gd_pagedesignitems
-- ----------------------------
DROP TABLE IF EXISTS `gd_pagedesignitems`;
CREATE TABLE `gd_pagedesignitems`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `WidgetCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PageCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PositionId` int NOT NULL,
  `Sort` int NOT NULL,
  `Parameters` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_pagedesignitems
-- ----------------------------
INSERT INTO `gd_pagedesignitems` VALUES ('3a19cd0f-22ce-6af4-bcf3-371147c4f7b9', NULL, 'search', 'ios_home', 0, 1, '{\"keywords\":\"请输入关键字搜索\",\"style\":\"radius\"}', '01ca7036f04343b5a81737735f726e2a');
INSERT INTO `gd_pagedesignitems` VALUES ('3a19cd0f-22ce-dc90-c4e4-80489db27495', NULL, 'imgSlide', 'ios_home', 1, 2, '{\"duration\":2500,\"height\":175,\"list\":[{\"image\":\"https://uview-plus.jiangruyi.com/uview/swiper/swiper1.png\",\"linkType\":2,\"linkValue\":\"3a106291-2516-c1b9-ad23-43c77684e518\",\"showLinkValue\":\"03.测试商品信息\",\"placeholder\":\"请选择商品\",\"readonly\":true},{\"image\":\"https://uview-plus.jiangruyi.com/uview/swiper/swiper2.png\",\"linkType\":2,\"linkValue\":\"3a0dfe30-2b73-77fc-1c76-ae102bfe5891\",\"showLinkValue\":\"01.南山田舍，海南黑金刚莲雾，脆甜多汁现摘现发，当季热带水果包邮\",\"placeholder\":\"请选择商品\",\"readonly\":true}]}', 'bb08cda527bb40c3bfacd8cf97da2a15');
INSERT INTO `gd_pagedesignitems` VALUES ('3a1b7cd9-f547-406e-8ec7-64b5f6634a6f', NULL, 'search', 'Test222', 0, 1, '{\"keywords\":\"请输入关键字搜索\",\"style\":\"radius\"}', '4ff658144bda44cea644f0002d06438c');
INSERT INTO `gd_pagedesignitems` VALUES ('3a1b7cd9-f547-5e29-54e2-fc8c31adaf13', NULL, 'goodTabBar', 'Test222', 1, 2, '{\"isFixedHead\":true,\"list\":[{\"title\":\"选项卡名称一\",\"subTitle\":\"子标题一\",\"type\":\"auto\",\"classifyId\":\"3a10a3c0-2010-49b3-816a-48269311a8c8\",\"brandId\":\"3a0e648b-9c2e-d106-cd54-c0a21d9ad54e\",\"limit\":10,\"column\":2,\"isShow\":true,\"list\":[{\"image\":\"/src/assets/design/empty-banner.png\",\"name\":\"\",\"price\":\"\"},{\"image\":\"/src/assets/design/empty-banner.png\",\"name\":\"\",\"price\":\"\"},{\"image\":\"/src/assets/design/empty-banner.png\",\"name\":\"\",\"price\":\"\"},{\"image\":\"/src/assets/design/empty-banner.png\",\"name\":\"\",\"price\":\"\"}],\"hasChooseGoods\":[]},{\"title\":\"选项卡名称二\",\"subTitle\":\"子标题二\",\"type\":\"auto\",\"classifyId\":\"\",\"brandId\":\"\",\"limit\":10,\"column\":2,\"isShow\":true,\"list\":[{\"image\":\"/src/assets/design/empty-banner.png\",\"name\":\"\",\"price\":\"\"},{\"image\":\"/src/assets/design/empty-banner.png\",\"name\":\"\",\"price\":\"\"},{\"image\":\"/src/assets/design/empty-banner.png\",\"name\":\"\",\"price\":\"\"},{\"image\":\"/src/assets/design/empty-banner.png\",\"name\":\"\",\"price\":\"\"}],\"hasChooseGoods\":[]}]}', 'e97b5a3e10624ae59f98329b41d09427');
INSERT INTO `gd_pagedesignitems` VALUES ('3a1be9ba-a78a-58c9-5047-ae8194e03cc7', NULL, 'coupon', 'mobile_home', 6, 7, '{\"limit\":2}', 'be4ee572686744d88e12802408739a5f');
INSERT INTO `gd_pagedesignitems` VALUES ('3a1be9ba-a78a-6876-529c-8b4e5851f022', NULL, 'goods', 'mobile_home', 4, 5, '{\"title\":\"热门推荐\",\"lookMore\":true,\"type\":\"choose\",\"classifyId\":\"3a10a3e1-f552-0963-4322-f9df3d6efff9\",\"brandId\":\"3a0d8c8e-e639-7d25-eeac-60820ac88352\",\"limit\":5,\"display\":\"list\",\"column\":3,\"list\":[{\"id\":\"3a1be90a-804f-3ea5-ad0b-113f581c0d93\",\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200319/黄元帅.png\",\"images\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200535/黄元帅.png;http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200538/D01.png;http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200541/D02.png;http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200544/D03.png\",\"name\":\"烟台黄元帅苹果新鲜水果粉面苹果当季孕妇老人黄香蕉软糯沙甜整箱 粉面5斤中果【10-12个】\",\"price\":150,\"stock\":200},{\"id\":\"3a1be8f5-77c6-abfd-9f34-bbbc0419b30f\",\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823195640/华圣苹果-1.png\",\"images\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823194923/华圣苹果-1.png;http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823194925/华圣苹果-2.png;http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823194928/华圣苹果-3.png\",\"name\":\"华圣【筑梦太空礼盒】2.4kg 航天级生鲜礼盒 精品原生态红富士苹果 筑梦太空礼盒\",\"price\":170,\"stock\":1000},{\"id\":\"3a1be913-be5c-9884-7a18-17fb9b8a1860\",\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202537/GXXMJ.png\",\"images\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202541/GXXMJ.png;http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202635/XMJ-2.jpg\",\"name\":\"广西小米蕉当季新鲜应季水果日日鲜香甜小香蕉芭蕉孕妇水果整箱 净重9斤 酸甜口的小香蕉\",\"price\":52,\"stock\":500}]}', '4961c70aa28644c4a8210be6e435131a');
INSERT INTO `gd_pagedesignitems` VALUES ('3a1be9ba-a78a-b35b-fdd4-d8885efb63cc', NULL, 'imgSlide', 'mobile_home', 0, 1, '{\"duration\":3500,\"height\":175,\"list\":[{\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823194923/%E5%8D%8E%E5%9C%A3%E8%8B%B9%E6%9E%9C-1.png\",\"linkType\":2,\"linkValue\":\"3a1be8f5-77c6-abfd-9f34-bbbc0419b30f\",\"showLinkValue\":\"华圣【筑梦太空礼盒】2.4kg 航天级生鲜礼盒 精品原生态红富士苹果 筑梦太空礼盒\",\"placeholder\":\"请选择商品\",\"readonly\":true},{\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823204356/H-01.png\",\"linkType\":2,\"linkValue\":\"3a1be90a-804f-3ea5-ad0b-113f581c0d93\",\"showLinkValue\":\"烟台黄元帅苹果新鲜水果粉面苹果当季孕妇老人黄香蕉软糯沙甜整箱 粉面5斤中果【10-12个】\",\"placeholder\":\"请选择商品\",\"readonly\":true},{\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202537/GXXMJ.png\",\"linkType\":2,\"linkValue\":\"3a1be913-be5c-9884-7a18-17fb9b8a1860\",\"showLinkValue\":\"广西小米蕉当季新鲜应季水果日日鲜香甜小香蕉芭蕉孕妇水果整箱 净重9斤 酸甜口的小香蕉\",\"placeholder\":\"请选择商品\",\"readonly\":true}]}', '1a62a3ee31e14926950fac456dbf9b8a');
INSERT INTO `gd_pagedesignitems` VALUES ('3a1be9ba-a78a-b9f0-7c96-f44c1a9b8b20', NULL, 'groupPurchase', 'mobile_home', 7, 8, '{\"title\":\"限时秒杀\",\"limit\":10,\"list\":[{\"tenantId\":null,\"name\":\"速来！小米蕉大甩卖！\",\"type\":4,\"weight\":100,\"parameters\":\"{\\\"goodsId\\\":\\\"3a1be913-be5c-9884-7a18-17fb9b8a1860\\\",\\\"nums\\\":1}\",\"maxNums\":1,\"maxGoodNums\":10,\"maxRecevieNums\":100,\"startTime\":\"2025-08-20 00:00:00\",\"endTime\":\"2025-11-25 00:00:00\",\"isExclusive\":false,\"isAutoReceive\":true,\"isEnable\":true,\"effectiveDays\":0,\"effectiveHours\":0,\"promotionConditions\":[{\"tenantId\":null,\"promotionId\":\"3a1be9a2-21a7-0ec0-3f7c-3b9d0b2719bd\",\"code\":\"GOODS_IDS\",\"parameters\":\"{\\\"goodsId\\\":\\\"3a1be913-be5c-9884-7a18-17fb9b8a1860\\\",\\\"nums\\\":1}\",\"concurrencyStamp\":\"238aa30d61d1470abc40a32e3d74338d\",\"creationTime\":null,\"id\":\"3a1be9a2-e48b-1ee2-8453-817b2d1d3e09\"}],\"promotionResults\":[{\"tenantId\":null,\"promotionId\":\"3a1be9a2-21a7-0ec0-3f7c-3b9d0b2719bd\",\"code\":\"GOODS_DISCOUNT\",\"parameters\":\"{\\\"discount\\\":\\\"7\\\"}\",\"concurrencyStamp\":\"d6a3ce83261146aeaad7a059b1bf48f5\",\"creationTime\":null,\"id\":\"3a1be9a2-e48b-0218-feac-7234a5f64ab0\"}],\"concurrencyStamp\":\"ec93f75b1c384a69b5039a1df69cb82a\",\"creationTime\":\"2025-08-23 23:04:04\",\"id\":\"3a1be9a2-21a7-0ec0-3f7c-3b9d0b2719bd\"}]}', 'fbe5351c150643e2ac13511688443036');
INSERT INTO `gd_pagedesignitems` VALUES ('3a1be9ba-a78a-bccc-329f-58c021d3bad7', NULL, 'search', 'mobile_home', 1, 2, '{\"keywords\":\"请输入商品信息\",\"style\":\"radius\"}', 'b2d5a648a3474684a34ed216b973f71e');
INSERT INTO `gd_pagedesignitems` VALUES ('3a1be9ba-a78a-e814-98d4-bf9623304298', NULL, 'notice', 'mobile_home', 2, 3, '{\"type\":\"choose\",\"list\":[{\"tenantId\":null,\"title\":\"🎉热销通知！   今早刚到的**洛川红富士**与**海南皇帝蕉**已抢爆！脆甜苹果仅剩30箱，软糯香蕉余50把，售完无补！   速来抢购，手慢无～\",\"contentBody\":\"<p>🎉热销通知！ &nbsp;</p><p>今早刚到的**洛川红富士**与**海南皇帝蕉**已抢爆！脆甜苹果仅剩30箱，软糯香蕉余50把，售完无补！ &nbsp;</p><p>速来抢购，手慢无～ 😁</p>\",\"type\":null,\"sort\":1,\"concurrencyStamp\":\"e448690d83ff41628ad137f84ac6d156\",\"creationTime\":\"2025-08-23 20:47:56\",\"id\":\"3a1be925-7def-af93-69d7-a228aaf0a25d\"}],\"color\":\"#606266\",\"bgColor\":\"#FFFFFF\"}', '48cca18fe91f467fa6aa5df84dc6d2c7');
INSERT INTO `gd_pagedesignitems` VALUES ('3a1be9ba-a78a-ea3e-29ce-cdf5001524b0', NULL, 'video', 'mobile_home', 5, 6, '{\"autoplay\":false,\"list\":[{\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823200535/%E9%BB%84%E5%85%83%E5%B8%85.png\",\"url\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823231332/黄元帅苹果视频.mp4\",\"linkType\":\"\",\"linkValue\":\"\"}]}', 'de8dbf074cf64d549e133acb62ce49f2');
INSERT INTO `gd_pagedesignitems` VALUES ('3a1be9ba-a78a-ea98-4246-8114df9dd48b', NULL, 'video', 'mobile_home', 8, 9, '{\"autoplay\":false,\"list\":[{\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202537/GXXMJ.png\",\"url\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823202545/XMJ.mp4\",\"linkType\":\"\",\"linkValue\":\"\"}]}', '1ba8abddf36449f19ce79c4340ec2c56');
INSERT INTO `gd_pagedesignitems` VALUES ('3a1be9ba-a78a-ed9c-fb94-c48c66136113', NULL, 'navBar', 'mobile_home', 3, 4, '{\"limit\":4,\"list\":[{\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823221632/HS_01.png\",\"text\":\"华圣苹果\",\"linkType\":2,\"linkValue\":\"3a1be8f5-77c6-abfd-9f34-bbbc0419b30f\",\"showLinkValue\":\"华圣【筑梦太空礼盒】2.4kg 航天级生鲜礼盒 精品原生态红富士苹果 筑梦太空礼盒\",\"placeholder\":\"请选择商品\",\"readonly\":true},{\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823221832/HYS_01.png\",\"text\":\"黄元帅苹果\",\"linkType\":2,\"linkValue\":\"3a1be90a-804f-3ea5-ad0b-113f581c0d93\",\"showLinkValue\":\"烟台黄元帅苹果新鲜水果粉面苹果当季孕妇老人常备5斤中果【10-12个】\",\"placeholder\":\"请选择商品\",\"readonly\":true},{\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823222040/XMXJ_01.png\",\"text\":\"小米香蕉\",\"linkType\":2,\"linkValue\":\"3a1be913-be5c-9884-7a18-17fb9b8a1860\",\"showLinkValue\":\"广西小米蕉当季新鲜应季水果日日鲜香甜小香蕉芭蕉孕妇水果整箱 净重9斤 酸甜口的小香蕉\",\"placeholder\":\"请选择商品\",\"readonly\":true},{\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823222239/HXJ_01.png\",\"text\":\"红香蕉\",\"linkType\":1,\"linkValue\":\"\",\"showLinkValue\":\"\",\"placeholder\":\"http开头为webview跳转，其他为站内页面跳转\",\"readonly\":false},{\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823223323/HTPT_01.png\",\"text\":\"红提葡萄\",\"linkType\":1,\"linkValue\":\"\",\"showLinkValue\":\"\",\"placeholder\":\"http开头为webview跳转，其他为站内页面跳转\",\"readonly\":false},{\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823223706/WHBPT_01.png\",\"text\":\"无核白葡萄\",\"linkType\":1,\"linkValue\":\"\",\"showLinkValue\":\"\",\"placeholder\":\"http开头为webview跳转，其他为站内页面跳转\",\"readonly\":false},{\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823224205/XHPT_01.png\",\"text\":\"夏黑葡萄\",\"linkType\":1,\"linkValue\":\"\",\"showLinkValue\":\"\",\"placeholder\":\"http开头为webview跳转，其他为站内页面跳转\",\"readonly\":false},{\"image\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823222503/CC_01.png\",\"text\":\"赣州橙子\",\"linkType\":1,\"linkValue\":\"\",\"showLinkValue\":\"\",\"placeholder\":\"http开头为webview跳转，其他为站内页面跳转\",\"readonly\":false}]}', 'dd84feb1884946dd933ae6cb932d382e');

-- ----------------------------
-- Table structure for gd_pagedesigns
-- ----------------------------
DROP TABLE IF EXISTS `gd_pagedesigns`;
CREATE TABLE `gd_pagedesigns`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Layout` int NULL DEFAULT NULL,
  `Type` int NULL DEFAULT NULL,
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
  UNIQUE INDEX `IX_Gd_PageDesigns_Code`(`Code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_pagedesigns
-- ----------------------------
INSERT INTO `gd_pagedesigns` VALUES ('3a0ed170-9ede-ea5b-0c98-e1c09122811a', NULL, 'mobile_home', '移动端页面布局', '包含【H5、小程序、Android、IOS】页面设计！', 1, 1, '{}', 'cebf0b838213463485e22cd7ab939c4a', '2023-11-12 00:42:52.183630', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 20:06:46.465266', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_pagedesigns` VALUES ('3a109588-a482-8f16-b7cc-3ae45e2880d8', NULL, 'ios_home', 'IOS端', '【IOS】页面布局！', 1, 2, '{}', '3d26e5f988f2488caf9cfa9971b4878a', '2024-02-07 11:37:27.941033', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 20:06:45.798668', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_pagedesigns` VALUES ('3a1b7cd4-4ce4-491e-becc-21de5de330cf', NULL, 'Test222', '测试222', NULL, 1, 2, '{}', '85a52b9603ce466d9057321a921bbb31', '2025-08-02 20:00:16.102019', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 20:06:30.082037', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 20:06:30.081757');

-- ----------------------------
-- Table structure for gd_productdistributions
-- ----------------------------
DROP TABLE IF EXISTS `gd_productdistributions`;
CREATE TABLE `gd_productdistributions`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `ProductId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `ProductSn` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LevelOne` decimal(65, 30) NOT NULL,
  `LevelTwo` decimal(65, 30) NOT NULL,
  `LevelThree` decimal(65, 30) NOT NULL,
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
  UNIQUE INDEX `IX_Gd_ProductDistributions_ProductId`(`ProductId`) USING BTREE,
  CONSTRAINT `FK_Gd_ProductDistributions_Gd_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `gd_products` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_productdistributions
-- ----------------------------
INSERT INTO `gd_productdistributions` VALUES ('3a1be8f5-77e4-81ae-32a6-256fc6e8e8b8', NULL, '3a1be8f5-77db-fbc0-6974-ea8afb90d743', 'SN1755949056611', 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, '{}', '6afbd4b91d3d4909b490161ff5466459', '2025-08-23 19:55:29.126727', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_productdistributions` VALUES ('3a1be90a-808b-0914-ba0f-f2ce97092c23', NULL, '3a1be90a-8088-902c-a6e7-e871d28af054', 'SN711828016715653', 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, '{}', '2cd7643e9486471da35ee235dbdec574', '2025-08-23 20:18:27.595569', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_productdistributions` VALUES ('3a1be90a-808b-80ad-b986-8cd91a781f1b', NULL, '3a1be90a-8089-d7dc-9e7b-99688c581768', 'SN711828016715654', 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, '{}', 'b29d4060591e43c69c3ec8c921859eb2', '2025-08-23 20:18:27.596087', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_productdistributions` VALUES ('3a1be90a-808b-c1a4-63d7-600ddac9aa25', NULL, '3a1be90a-8089-f048-dd81-0e869eca76e6', 'SN711828016715655', 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, '{}', 'ee8da1e4cac14afab3947a66fd687ea1', '2025-08-23 20:18:27.596466', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_productdistributions` VALUES ('3a1be90a-808b-e552-b5a3-26f463ee20a4', NULL, '3a1be90a-8089-362a-ad08-41063c8d8a7c', 'SN711828016715656', 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, '{}', '4d4a8d3188204cf7b3428046b6ef2e3f', '2025-08-23 20:18:27.596703', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_productdistributions` VALUES ('3a1be913-be5f-b869-b6f3-c50ec5aaddb8', NULL, '3a1be913-be5f-7b9d-59ca-895516d50bfb', 'SN1755951693860', 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, '{}', 'e717af45b80c4dc2add28a9aca353468', '2025-08-23 20:28:33.247948', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 20:31:09.114289', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 20:31:09.114204');
INSERT INTO `gd_productdistributions` VALUES ('3a1be916-1f40-dcfa-2e7a-434c111a29aa', NULL, '3a1be916-1f3f-89c7-7555-9f268a96f7ed', 'SN711831636338565', 0.000000000000000000000000000000, 0.000000000000000000000000000000, 0.000000000000000000000000000000, '{}', '8a3821d60ebe49318c2c9e22201509cc', '2025-08-23 20:31:09.120387', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for gd_products
-- ----------------------------
DROP TABLE IF EXISTS `gd_products`;
CREATE TABLE `gd_products`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `GoodId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `BarCode` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Sn` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Price` decimal(65, 30) NOT NULL,
  `CostPrice` decimal(65, 30) NOT NULL,
  `MktPrice` decimal(65, 30) NOT NULL,
  `Marketable` tinyint(1) NOT NULL,
  `Weight` decimal(65, 30) NOT NULL,
  `Stock` int NOT NULL,
  `FreezeStock` int NOT NULL,
  `SpesDesc` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `IsDefault` tinyint(1) NOT NULL,
  `Images` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
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
  UNIQUE INDEX `IX_Gd_Products_Sn`(`Sn`) USING BTREE,
  INDEX `IX_Gd_Products_GoodId`(`GoodId`) USING BTREE,
  CONSTRAINT `FK_Gd_Products_Gd_Goods_GoodId` FOREIGN KEY (`GoodId`) REFERENCES `gd_goods` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_products
-- ----------------------------
INSERT INTO `gd_products` VALUES ('3a1be8f5-77db-fbc0-6974-ea8afb90d743', NULL, '3a1be8f5-77c6-abfd-9f34-bbbc0419b30f', 'GD1755949056511', 'SN1755949056611', 170.000000000000000000000000000000, 50.000000000000000000000000000000, 175.000000000000000000000000000000, 0, 24.000000000000000000000000000000, 1000, 0, '', 1, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823195618/华圣苹果-1.png', '{}', 'cfdc7ba5feb8453d85a9224eefd3796a', '2025-08-23 19:55:29.118190', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 19:56:20.011599', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `gd_products` VALUES ('3a1be90a-8088-902c-a6e7-e871d28af054', NULL, '3a1be90a-804f-3ea5-ad0b-113f581c0d93', 'GD1755950492353', 'SN711828016715653', 150.000000000000000000000000000000, 50.000000000000000000000000000000, 160.000000000000000000000000000000, 0, 5.000000000000000000000000000000, 200, 0, '精选:5斤,中果:15-20个', 1, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823201801/黄元帅.png', '{}', 'd9e943a38d1047d29a132803b33145d7', '2025-08-23 20:18:27.593553', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_products` VALUES ('3a1be90a-8089-362a-ad08-41063c8d8a7c', NULL, '3a1be90a-804f-3ea5-ad0b-113f581c0d93', 'GD1755950492353', 'SN711828016715656', 160.000000000000000000000000000000, 60.000000000000000000000000000000, 170.000000000000000000000000000000, 0, 9.000000000000000000000000000000, 100, 0, '精选:9斤,中果:10-12个', 0, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823201810/D03.png', '{}', '3fb677652cc24c87b489a39713531e6d', '2025-08-23 20:18:27.594717', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_products` VALUES ('3a1be90a-8089-d7dc-9e7b-99688c581768', NULL, '3a1be90a-804f-3ea5-ad0b-113f581c0d93', 'GD1755950492353', 'SN711828016715654', 140.000000000000000000000000000000, 40.000000000000000000000000000000, 150.000000000000000000000000000000, 0, 5.000000000000000000000000000000, 100, 0, '精选:5斤,中果:10-12个', 0, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823201805/D01.png', '{}', '942d46d41ccd4937b304985a3d0a9922', '2025-08-23 20:18:27.594134', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_products` VALUES ('3a1be90a-8089-f048-dd81-0e869eca76e6', NULL, '3a1be90a-804f-3ea5-ad0b-113f581c0d93', 'GD1755950492353', 'SN711828016715655', 165.000000000000000000000000000000, 65.000000000000000000000000000000, 175.000000000000000000000000000000, 0, 9.000000000000000000000000000000, 100, 0, '精选:9斤,中果:15-20个', 0, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823201807/D02.png', '{}', '914bf40c442942fe96f8ae61935a9b0c', '2025-08-23 20:18:27.594516', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `gd_products` VALUES ('3a1be913-be5f-7b9d-59ca-895516d50bfb', NULL, '3a1be913-be5c-9884-7a18-17fb9b8a1860', 'GD1755951693837', 'SN1755951693860', 200.000000000000000000000000000000, 100.000000000000000000000000000000, 225.000000000000000000000000000000, 0, 9.000000000000000000000000000000, 500, 0, '', 1, '', '{}', 'd3c064fb43bc4e91b524ab4e7685f487', '2025-08-23 20:28:33.247334', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 20:31:09.086989', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-23 20:31:09.086904');
INSERT INTO `gd_products` VALUES ('3a1be916-1f3f-89c7-7555-9f268a96f7ed', NULL, '3a1be913-be5c-9884-7a18-17fb9b8a1860', 'GD1755951693837', 'SN711831636338565', 52.000000000000000000000000000000, 10.000000000000000000000000000000, 55.000000000000000000000000000000, 0, 9.000000000000000000000000000000, 500, 0, '精选:9斤', 1, 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823203034/GXXMJ.png', '{}', '848faaee942d44839a909d02829e52d7', '2025-08-23 20:31:09.119861', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for gd_servicedescriptions
-- ----------------------------
DROP TABLE IF EXISTS `gd_servicedescriptions`;
CREATE TABLE `gd_servicedescriptions`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Title` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Type` int NOT NULL,
  `Description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsShow` tinyint(1) NOT NULL,
  `Sort` int NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_servicedescriptions
-- ----------------------------
INSERT INTO `gd_servicedescriptions` VALUES ('3a10babe-5e9d-888d-68bd-df231fbf95a3', NULL, '异常问题', 1, '商品促销信息以商品详情页“促销”栏中的信息为准；商品的具体售价以订单结算页价格为准；如您发现活动商品售价或促销信息有异常，建议购买前先联系销售商咨询。', 1, 1, '2aa067881ca742298abc16f33c94b9d3');
INSERT INTO `gd_servicedescriptions` VALUES ('3a10bac0-4022-f269-cd1f-0c7a9c0af4f0', NULL, '折扣', 1, '如无特殊说明，折扣指销售商在原价、或划线价（如品牌专柜标价、商品吊牌价、厂商指导价、厂商建议零售价）等某一价格基础上计算出的优惠比例或优惠金额；如有疑问，您可在购买前联系销售商进行咨询。', 1, 2, 'e59b748f35704bb2b288aba784a34797');
INSERT INTO `gd_servicedescriptions` VALUES ('3a10bac0-a1a9-cefc-8e09-d84fb2d39aa9', NULL, '划线价', 1, '商品展示的划横线价格为参考价，并非原价，该价格可能是品牌专柜标价、商品吊牌价或由品牌供应商提供的正品零售价（如厂商指导价、建议零售价等）或该商品在平台上曾经展示过的销售价；由于地区、时间的差异性和市场行情波动，品牌专柜标价、商品吊牌价等可能会与您购物时展示的不一致，该价格仅供您参考。', 1, 3, 'e1f1b1b53e5a4e6bb2fe764f0ee4248a');
INSERT INTO `gd_servicedescriptions` VALUES ('3a10bac1-aaa8-5b77-80d2-3451f9c25e8c', NULL, '销售价', 1, '销售价为商品的销售价格，是您最终决定是否购买商品的依据。', 1, 4, 'bf1f3fd4d6de43dd8f524b04d1ff3d60');
INSERT INTO `gd_servicedescriptions` VALUES ('3a10bac1-fc5a-a057-d8fd-c0eacb4a3276', NULL, '权利声明', 1, '本站商品信息均来自于合作方，其真实性、准确性和合法性由信息拥有者（合作方）负责。本站不提供任何保证，并不承担任何法律责任。', 1, 5, 'd1b7d0e0d57e4da0a7e1eb652ada3660');
INSERT INTO `gd_servicedescriptions` VALUES ('3a10bac2-d0c8-5e62-ef8b-4e5405b4c4a8', NULL, '发货时间', 3, '17:00前下单，当日发货，快递包邮！', 1, 6, '633803a95edf4131a475d3abb5962906');
INSERT INTO `gd_servicedescriptions` VALUES ('3a10bac3-39f1-8213-73da-90611d1b9450', NULL, '7天无理由退换货', 2, '我们提供7天无理由退换货服务！', 1, 8, 'f8c71aa2b4d94d9e8a776fc918672dba');
INSERT INTO `gd_servicedescriptions` VALUES ('3a10bac3-9ac0-be98-817b-b7c398767114', NULL, '产地直销', 2, '缩短和优化了供应链流程，减少损耗，可以让利给消费者！', 1, 7, '240e9cbb47f7415db5f31f3a2d6870b6');

-- ----------------------------
-- Table structure for gd_servicegoods
-- ----------------------------
DROP TABLE IF EXISTS `gd_servicegoods`;
CREATE TABLE `gd_servicegoods`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Title` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Thumbnail` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ContentBody` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AllowedMembership` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConsumableStore` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Status` int NOT NULL,
  `MaxBuyNumber` int NOT NULL,
  `Amount` int NOT NULL,
  `StartTime` datetime(6) NOT NULL,
  `EndTime` datetime(6) NOT NULL,
  `ValidityType` int NOT NULL,
  `ValidityStartTime` datetime(6) NULL DEFAULT NULL,
  `ValidityEndTime` datetime(6) NULL DEFAULT NULL,
  `TicketNumber` int NOT NULL,
  `Money` decimal(65, 30) NOT NULL,
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
-- Records of gd_servicegoods
-- ----------------------------

-- ----------------------------
-- Table structure for gd_stocklogs
-- ----------------------------
DROP TABLE IF EXISTS `gd_stocklogs`;
CREATE TABLE `gd_stocklogs`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `SourceId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GoodId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `ProductId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Nums` int NOT NULL,
  `Sn` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Bn` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `GoodName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SpesDesc` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
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
-- Records of gd_stocklogs
-- ----------------------------

-- ----------------------------
-- Table structure for gd_stocks
-- ----------------------------
DROP TABLE IF EXISTS `gd_stocks`;
CREATE TABLE `gd_stocks`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `SourceId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Type` int NOT NULL,
  `OperatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Memo` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
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
-- Records of gd_stocks
-- ----------------------------

-- ----------------------------
-- Table structure for gd_storeclerks
-- ----------------------------
DROP TABLE IF EXISTS `gd_storeclerks`;
CREATE TABLE `gd_storeclerks`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `StoreId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
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
  INDEX `IX_Gd_StoreClerks_StoreId`(`StoreId`) USING BTREE,
  CONSTRAINT `FK_Gd_StoreClerks_Gd_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `gd_stores` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gd_storeclerks
-- ----------------------------

-- ----------------------------
-- Table structure for gd_stores
-- ----------------------------
DROP TABLE IF EXISTS `gd_stores`;
CREATE TABLE `gd_stores`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `StoreName` varchar(125) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Mobile` varchar(13) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LinkMan` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LogoImage` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AreaId` bigint NOT NULL,
  `Address` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Coordinate` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Latitude` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Longitude` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsDefault` tinyint(1) NOT NULL,
  `Distance` decimal(65, 30) NOT NULL,
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
-- Records of gd_stores
-- ----------------------------

SET FOREIGN_KEY_CHECKS = 1;
