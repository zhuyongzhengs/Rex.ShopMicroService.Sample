/*
 Navicat Premium Data Transfer

 Source Server         : Local_MySQL
 Source Server Type    : MySQL
 Source Server Version : 80036
 Source Host           : localhost:3306
 Source Schema         : rex.shop.paymentservice

 Target Server Type    : MySQL
 Target Server Version : 80036
 File Encoding         : 65001

 Date: 23/08/2025 23:47:57
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
INSERT INTO `cap.published` VALUES (9027127011347533826, 'v1', 'Change.Order.Status', '{\"Headers\":{\"cap-callback-name\":null,\"cap-msg-id\":\"9027127011347533826\",\"cap-corr-id\":\"9027127011347533826\",\"cap-corr-seq\":\"0\",\"cap-msg-name\":\"Change.Order.Status\",\"cap-msg-type\":\"OrderChangeStatusEto\",\"cap-senttime\":\"2025/8/23 23:36:56 \\u002B08:00\"},\"Value\":{\"OrderIds\":[\"3a1be9bf-e6a1-4c82-399e-d821cc554718\"],\"PaymentCode\":\"BalancePay\",\"Description\":\"\\u4F59\\u989D\\u652F\\u4ED8\\u6210\\u529F\\uFF01\",\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:36:56', '2025-08-24 23:36:56', 'Succeeded');
INSERT INTO `cap.published` VALUES (9027127011347533827, 'v1', 'Change.User.Balance', '{\"Headers\":{\"cap-callback-name\":null,\"cap-msg-id\":\"9027127011347533827\",\"cap-corr-id\":\"9027127011347533827\",\"cap-corr-seq\":\"0\",\"cap-msg-name\":\"Change.User.Balance\",\"cap-msg-type\":\"ChangeUserBalanceEto\",\"cap-senttime\":\"2025/8/23 23:36:56 \\u002B08:00\"},\"Value\":{\"UserId\":\"3a118d4a-e735-1846-9ae3-c4ced1efcd87\",\"Type\":1,\"Money\":112.00000000000000000000000000,\"SourceId\":\"3a1be9c0-35c8-5914-37cd-6eeaacfa2c64\",\"CateMoney\":null,\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:36:56', '2025-08-24 23:36:56', 'Succeeded');
INSERT INTO `cap.published` VALUES (9027127011347533829, 'v1', 'Change.Order.Status', '{\"Headers\":{\"cap-callback-name\":null,\"cap-msg-id\":\"9027127011347533829\",\"cap-corr-id\":\"9027127011347533829\",\"cap-corr-seq\":\"0\",\"cap-msg-name\":\"Change.Order.Status\",\"cap-msg-type\":\"OrderChangeStatusEto\",\"cap-senttime\":\"2025/8/23 23:38:17 \\u002B08:00\"},\"Value\":{\"OrderIds\":[\"3a1be9c1-55a8-b456-8136-5a91b44c3590\"],\"PaymentCode\":\"BalancePay\",\"Description\":\"\\u4F59\\u989D\\u652F\\u4ED8\\u6210\\u529F\\uFF01\",\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:38:17', '2025-08-24 23:38:17', 'Succeeded');
INSERT INTO `cap.published` VALUES (9027127011347533830, 'v1', 'Change.User.Balance', '{\"Headers\":{\"cap-callback-name\":null,\"cap-msg-id\":\"9027127011347533830\",\"cap-corr-id\":\"9027127011347533830\",\"cap-corr-seq\":\"0\",\"cap-msg-name\":\"Change.User.Balance\",\"cap-msg-type\":\"ChangeUserBalanceEto\",\"cap-senttime\":\"2025/8/23 23:38:17 \\u002B08:00\"},\"Value\":{\"UserId\":\"3a118d4a-e735-1846-9ae3-c4ced1efcd87\",\"Type\":1,\"Money\":112.00000000000000000000000000,\"SourceId\":\"3a1be9c1-7285-0d23-8a8f-d60cc76eb2fc\",\"CateMoney\":null,\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:38:17', '2025-08-24 23:38:17', 'Succeeded');

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
INSERT INTO `cap.received` VALUES (9027127011347533825, 'v1', 'User.Balance.Payment', 'Rex.PaymentService.HttpApi.Host.v1', '{\"Headers\":{\"cap-callback-name\":null,\"cap-corr-id\":\"9027127011260612610\",\"cap-corr-seq\":\"0\",\"cap-msg-id\":\"9027127011260612610\",\"cap-msg-name\":\"User.Balance.Payment\",\"cap-msg-type\":\"UserBalancePaymentEto\",\"cap-senttime\":\"2025/8/23 23:36:56 \\u002B08:00\",\"cap-msg-group\":\"Rex.PaymentService.HttpApi.Host.v1\",\"cap-exec-instance-id\":\"LAPTOP-A4AM01CM\"},\"Value\":{\"BillPaymentNo\":\"217559634159954\",\"Remark\":\"\\u4F59\\u989D\\u652F\\u4ED8\\u3002\",\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:36:56', '2025-08-24 23:36:56', 'Succeeded');
INSERT INTO `cap.received` VALUES (9027127011347533828, 'v1', 'User.Balance.Payment', 'Rex.PaymentService.HttpApi.Host.v1', '{\"Headers\":{\"cap-callback-name\":null,\"cap-corr-id\":\"9027127011260612612\",\"cap-corr-seq\":\"0\",\"cap-msg-id\":\"9027127011260612612\",\"cap-msg-name\":\"User.Balance.Payment\",\"cap-msg-type\":\"UserBalancePaymentEto\",\"cap-senttime\":\"2025/8/23 23:38:17 \\u002B08:00\",\"cap-msg-group\":\"Rex.PaymentService.HttpApi.Host.v1\",\"cap-exec-instance-id\":\"LAPTOP-A4AM01CM\"},\"Value\":{\"BillPaymentNo\":\"217559634970746\",\"Remark\":\"\\u4F59\\u989D\\u652F\\u4ED8\\u3002\",\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:38:17', '2025-08-24 23:38:17', 'Succeeded');

-- ----------------------------
-- Table structure for pm_billpayments
-- ----------------------------
DROP TABLE IF EXISTS `pm_billpayments`;
CREATE TABLE `pm_billpayments`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `No` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SourceId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Money` decimal(65, 30) NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Type` int NOT NULL,
  `Status` int NOT NULL,
  `PaymentCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Ip` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Parameters` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `PayedMsg` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `TradeNo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
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
  UNIQUE INDEX `IX_Pm_BillPayments_No`(`No`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pm_billpayments
-- ----------------------------
INSERT INTO `pm_billpayments` VALUES ('3a1be9c0-05a5-3de4-4d94-aa363679a96d', NULL, '217559634033988', '3a1be9bf-e6a1-4c82-399e-d821cc554718', 112.000000000000000000000000000000, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, 1, 'WechatPay', '0.0.0.1', '{}', NULL, NULL, '{}', '1f9fe2f9d4924f0eb36bebcf8a0de733', '2025-08-23 23:36:43.816918', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', NULL, NULL, 0, NULL, NULL);
INSERT INTO `pm_billpayments` VALUES ('3a1be9c0-35c8-5914-37cd-6eeaacfa2c64', NULL, '217559634159954', '3a1be9bf-e6a1-4c82-399e-d821cc554718', 112.000000000000000000000000000000, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, 2, 'BalancePay', '0.0.0.1', '{}', '', '', '{}', 'b180a12bb21a400697bb6d26f5381300', '2025-08-23 23:36:56.008948', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', '2025-08-23 23:36:56.437605', NULL, 0, NULL, NULL);
INSERT INTO `pm_billpayments` VALUES ('3a1be9c1-7285-0d23-8a8f-d60cc76eb2fc', NULL, '217559634970746', '3a1be9c1-55a8-b456-8136-5a91b44c3590', 112.000000000000000000000000000000, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, 2, 'BalancePay', '0.0.0.1', '{}', '', '', '{}', 'bdf0804cb5e84cfe8ce26daf1f578303', '2025-08-23 23:38:17.094664', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', '2025-08-23 23:38:17.126168', NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for pm_billrefunds
-- ----------------------------
DROP TABLE IF EXISTS `pm_billrefunds`;
CREATE TABLE `pm_billrefunds`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `No` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `BillAftersalesId` char(50) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Money` decimal(65, 30) NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `SourceId` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Type` int NOT NULL,
  `PaymentCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `TradeNo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Status` int NOT NULL,
  `Memo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
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
  UNIQUE INDEX `IX_Pm_BillRefunds_No`(`No`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pm_billrefunds
-- ----------------------------

-- ----------------------------
-- Table structure for pm_payments
-- ----------------------------
DROP TABLE IF EXISTS `pm_payments`;
CREATE TABLE `pm_payments`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsOnline` tinyint(1) NOT NULL,
  `Parameters` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Sort` int NOT NULL,
  `Memo` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsEnable` tinyint(1) NOT NULL,
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
-- Records of pm_payments
-- ----------------------------
INSERT INTO `pm_payments` VALUES ('0b67a0fe-bee5-436b-9615-1b69331831f4', NULL, '微信支付', 'WechatPay', 1, NULL, 1, '点击去微信支付', 1, '', 'a9e13e0a4d4e4065be87f5fda6965993', '2024-01-01 19:18:51.338288', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `pm_payments` VALUES ('2d538005-064c-4255-ba02-b28f555f7fbd', NULL, '支付宝支付', 'AliPay', 1, NULL, 2, '点击去支付宝支付', 0, '', 'fc567608fe2949028abd2f9363d6a1fe', '2024-01-01 19:18:51.338288', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 20:54:10.554085', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `pm_payments` VALUES ('7463fcce-443c-4dfd-91d4-dae59e13a9c7', NULL, '余额支付', 'BalancePay', 1, NULL, 4, '账户余额支付', 1, '', 'd9e13e0a4d4e4065be87f5fda6965993', '2024-01-01 19:18:51.338288', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `pm_payments` VALUES ('f7524b09-2e42-4139-8bf2-5d64f4f1e4cd', NULL, '线下支付', 'Offline', 0, NULL, 3, '联系客服进行线下付款', 0, '', 'a65813412a9e4f2d9e287dc732c31240', '2024-01-01 19:18:51.338288', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-02-20 21:15:49.806766', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);

SET FOREIGN_KEY_CHECKS = 1;
