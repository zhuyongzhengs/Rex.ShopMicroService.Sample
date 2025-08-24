/*
 Navicat Premium Data Transfer

 Source Server         : Local_MySQL
 Source Server Type    : MySQL
 Source Server Version : 80036
 Source Host           : localhost:3306
 Source Schema         : rex.shop.baseservice

 Target Server Type    : MySQL
 Target Server Version : 80036
 File Encoding         : 65001

 Date: 23/08/2025 23:47:32
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
-- Table structure for auth_applications
-- ----------------------------
DROP TABLE IF EXISTS `auth_applications`;
CREATE TABLE `auth_applications`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `ClientId` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ClientSecret` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ConsentType` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `DisplayNames` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Permissions` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `PostLogoutRedirectUris` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `RedirectUris` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Requirements` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ClientType` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ClientUri` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `LogoUri` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  `ApplicationType` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `JsonWebKeySet` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Settings` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Auth_Applications_ClientId`(`ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of auth_applications
-- ----------------------------
INSERT INTO `auth_applications` VALUES ('3a0d00d2-5c4a-f413-2d4e-b51a331b9898', 'AuthService_Swagger', NULL, 'implicit', 'AuthSwagger Application', NULL, '[\"ept:logout\",\"gt:authorization_code\",\"rst:code\",\"ept:authorization\",\"ept:token\",\"ept:revocation\",\"ept:introspection\",\"scp:address\",\"scp:email\",\"scp:phone\",\"scp:profile\",\"scp:roles\",\"scp:AuthServiceScope\"]', '[]', NULL, '[\"https://localhost:4466/swagger/oauth2-redirect.html\"]', NULL, 'public', 'https://localhost:4466', NULL, '{}', '37495b9601754312ad759c8ac3a79817', '2023-08-13 19:26:12.164050', NULL, '2024-03-23 18:44:32.913988', NULL, 0, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `auth_applications` VALUES ('3a0d00d2-a32e-799f-d6b7-36c7f8a5f4c5', 'BaseService_Swagger', NULL, 'implicit', 'BaseSwagger Application', NULL, '[\"ept:logout\",\"gt:authorization_code\",\"rst:code\",\"ept:authorization\",\"ept:token\",\"ept:revocation\",\"ept:introspection\",\"scp:address\",\"scp:email\",\"scp:phone\",\"scp:profile\",\"scp:roles\",\"scp:BaseServiceScope\"]', '[]', NULL, '[\"https://localhost:4455/swagger/oauth2-redirect.html\"]', NULL, 'public', 'https://localhost:4455', NULL, '{}', '0c491dcda79f4ab4ae624ccefb4d54d5', '2023-08-13 19:26:30.230202', NULL, '2024-03-23 18:44:33.047875', NULL, 0, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `auth_applications` VALUES ('3a0d39f3-6374-331f-cba8-7d40a0a05331', 'RexShopWebAdminClient', NULL, 'implicit', 'Rex商城后台管理客户端', NULL, '[\"ept:token\",\"ept:revocation\",\"ept:introspection\",\"gt:password\",\"scp:address\",\"scp:email\",\"scp:phone\",\"scp:profile\",\"scp:roles\",\"scp:AuthServiceScope\",\"scp:BaseServiceScope\",\"scp:GoodServiceScope\",\"scp:PromotionServiceScope\",\"scp:OrderServiceScope\",\"scp:PaymentServiceScope\"]', '[]', NULL, '[]', NULL, 'public', 'http://localhost:5120', '', '{}', 'edc99e102f734528804258ec9895bb85', '2023-08-24 21:40:37.957568', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-03-23 18:44:33.086395', NULL, 0, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `auth_applications` VALUES ('3a0d8c10-86d2-02b0-c768-3eede7a884cb', 'GoodService_Swagger', NULL, 'implicit', 'GoodSwagger Application', NULL, '[\"ept:logout\",\"gt:authorization_code\",\"rst:code\",\"ept:authorization\",\"ept:token\",\"ept:revocation\",\"ept:introspection\",\"scp:address\",\"scp:email\",\"scp:phone\",\"scp:profile\",\"scp:roles\",\"scp:GoodServiceScope\"]', '[]', NULL, '[\"https://localhost:4477/swagger/oauth2-redirect.html\"]', NULL, 'public', 'https://localhost:4477', NULL, '{}', '9c57ed7d17944b508becc1b8a63decbf', '2023-09-09 20:21:19.217201', NULL, '2024-03-23 18:44:33.070343', NULL, 0, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `auth_applications` VALUES ('3a0e1042-d4f3-7eed-0be8-2c347516bd14', 'BackendGoodAggregationServiceClient', 'AQAAAAEAACcQAAAAELOPokU+T4qCM2Yg46zEGf3NO70GusR/jv3A+kyKUZJFSjrMNRspqChpQVVG4kSA2g==', 'implicit', '后台商品聚合服务客户端', NULL, '[\"ept:token\",\"ept:revocation\",\"ept:introspection\",\"gt:client_credentials\",\"gt:refresh_token\",\"scp:AuthServiceScope\",\"scp:BaseServiceScope\",\"scp:GoodServiceScope\"]', NULL, NULL, NULL, NULL, 'confidential', '', '', '{}', 'fb6dbd293a5245cb8226098a408066d6', '2023-10-05 12:26:08.574660', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `auth_applications` VALUES ('3a0f11de-8407-5fa4-0bed-c9a38ba13da6', 'PromotionService_Swagger', NULL, 'implicit', 'PromotionSwagger Application', NULL, '[\"ept:logout\",\"gt:authorization_code\",\"rst:code\",\"ept:authorization\",\"ept:token\",\"ept:revocation\",\"ept:introspection\",\"scp:address\",\"scp:email\",\"scp:phone\",\"scp:profile\",\"scp:roles\",\"scp:PromotionServiceScope\"]', '[]', NULL, '[\"https://localhost:44385/swagger/oauth2-redirect.html\"]', NULL, 'public', 'https://localhost:4499', NULL, '{}', 'e8e04b7ea77043c8b14295df49032d2d', '2023-11-24 12:58:35.954202', NULL, '2024-03-23 18:44:33.079661', NULL, 0, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `auth_applications` VALUES ('3a118138-ec69-8903-f842-22ded68b3ede', 'RexShopMiniProgramWechatCodeClient', NULL, 'implicit', 'Rex商城微信小程序客户端', NULL, '[\"ept:token\",\"ept:revocation\",\"ept:introspection\",\"gt:wechat_code\",\"scp:address\",\"scp:email\",\"scp:phone\",\"scp:profile\",\"scp:roles\",\"scp:AuthServiceScope\",\"scp:BaseServiceScope\",\"scp:GoodServiceScope\",\"scp:PromotionServiceScope\",\"scp:OrderServiceScope\",\"scp:PaymentServiceScope\"]', NULL, NULL, NULL, NULL, 'public', 'http://localhost:5130', NULL, '{}', '8fb3503b2a564feb96cccfd9567cdd2f', '2024-03-24 14:00:46.520873', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `auth_applications` VALUES ('3a11a582-b371-412c-f509-6599f90015b7', 'OrderService_Swagger', NULL, 'implicit', 'OrderSwagger Application', NULL, '[\"ept:logout\",\"gt:authorization_code\",\"rst:code\",\"ept:authorization\",\"ept:token\",\"ept:revocation\",\"ept:introspection\",\"scp:address\",\"scp:email\",\"scp:phone\",\"scp:profile\",\"scp:roles\",\"scp:OrderServiceScope\"]', NULL, NULL, '[\"https://localhost:5500/swagger/oauth2-redirect.html\"]', NULL, 'public', 'https://localhost:5500', NULL, '{}', 'b3b0b33793e74f979fa04c4b57573a20', '2024-03-31 15:07:41.342071', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `auth_applications` VALUES ('3a12dc55-d9b5-0194-086e-fc709e4b4dce', 'PaymentService_Swagger', NULL, 'implicit', 'PaymentSwagger Application', NULL, '[\"ept:logout\",\"gt:authorization_code\",\"rst:code\",\"ept:authorization\",\"ept:token\",\"ept:revocation\",\"ept:introspection\",\"scp:address\",\"scp:email\",\"scp:phone\",\"scp:profile\",\"scp:roles\",\"scp:PaymentServiceScope\"]', NULL, NULL, '[\"https://localhost:5510/swagger/oauth2-redirect.html\"]', NULL, 'public', 'https://localhost:5510', NULL, '{}', '7fc71474647b48fd917e64f0ff651ee6', '2024-05-30 23:40:36.195265', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL);

-- ----------------------------
-- Table structure for auth_authorizations
-- ----------------------------
DROP TABLE IF EXISTS `auth_authorizations`;
CREATE TABLE `auth_authorizations`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `ApplicationId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `CreationDate` datetime(6) NULL DEFAULT NULL,
  `Properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Scopes` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Subject` varchar(400) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
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
  INDEX `IX_Auth_Authorizations_ApplicationId_Status_Subject_Type`(`ApplicationId`, `Status`, `Subject`, `Type`) USING BTREE,
  CONSTRAINT `FK_Auth_Authorizations_Auth_Applications_ApplicationId` FOREIGN KEY (`ApplicationId`) REFERENCES `auth_applications` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of auth_authorizations
-- ----------------------------
INSERT INTO `auth_authorizations` VALUES ('3a1380ff-31b7-8aca-691b-592cd66f32c4', '3a0d8c10-86d2-02b0-c768-3eede7a884cb', '2024-07-01 15:03:17.686940', NULL, '[\"GoodServiceScope\"]', 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'permanent', '{}', 'c125f065d9844bd38348eb63bce1f9ec', '2024-07-01 15:03:17.709949', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_authorizations` VALUES ('3a180162-1dbd-74d2-bcef-e8c9be829846', '3a11a582-b371-412c-f509-6599f90015b7', '2025-02-10 17:33:53.468845', NULL, '[\"OrderServiceScope\"]', 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'permanent', '{}', '3376e8a5f98e41f29d54d316a2bc0f67', '2025-02-10 17:33:53.496357', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_authorizations` VALUES ('3a182038-1708-0574-b80b-7df23da6649c', '3a12dc55-d9b5-0194-086e-fc709e4b4dce', '2025-02-16 17:16:12.934310', NULL, '[\"PaymentServiceScope\"]', 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'permanent', '{}', '8a1be7c5e11b49a1b80e8f7d6742b0ff', '2025-02-16 17:16:12.974848', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_authorizations` VALUES ('3a1a3daa-fd50-c2f7-cac1-76b5824a494c', '3a0d00d2-a32e-799f-d6b7-36c7f8a5f4c5', '2025-06-01 20:36:16.847328', NULL, '[\"BaseServiceScope\"]', 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'permanent', '{}', 'aa037c6858d1473ea86b98bd70e2cd6c', '2025-06-01 20:36:16.879070', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for auth_scopes
-- ----------------------------
DROP TABLE IF EXISTS `auth_scopes`;
CREATE TABLE `auth_scopes`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Descriptions` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `DisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `DisplayNames` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Resources` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
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
  INDEX `IX_Auth_Scopes_Name`(`Name`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of auth_scopes
-- ----------------------------
INSERT INTO `auth_scopes` VALUES ('3a0d00d1-e0c6-f214-faae-193c46b9f77d', NULL, NULL, 'AuthServiceScope API 接口服务', NULL, 'AuthServiceScope', NULL, '[\"AuthService\"]', '{}', '51525be097ce4f0d85dd66966baeb5c6', '2023-08-13 19:25:40.521308', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_scopes` VALUES ('3a0d00d1-f737-9da7-67b6-b817bffe92d1', NULL, NULL, 'BaseServiceScope API 接口服务', NULL, 'BaseServiceScope', NULL, '[\"BaseService\"]', '{}', '3beb43cfe4684c9a91caa9a23c6613de', '2023-08-13 19:25:46.190118', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_scopes` VALUES ('3a0d8c0e-c39b-a4f0-31fb-e937f202f985', NULL, NULL, 'GoodServiceScope API 接口服务', NULL, 'GoodServiceScope', NULL, '[\"GoodService\"]', '{}', 'e03439ed96db4f7f99d6d41d655b37b7', '2023-09-09 20:19:23.774985', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_scopes` VALUES ('3a0f11dd-5dd6-88a6-b2ee-adfc2d498161', NULL, NULL, 'PromotionServiceScope API 接口服务', NULL, 'PromotionServiceScope', NULL, '[\"PromotionService\"]', '{}', 'ddcdb4954b544de7af5d4e3688145756', '2023-11-24 12:57:20.623949', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_scopes` VALUES ('3a11a582-3e71-5fb4-8e81-47d52468b6df', NULL, NULL, 'OrderServiceScope API 接口服务', NULL, 'OrderServiceScope', NULL, '[\"OrderService\"]', '{}', '722ef4352bb34cc9954ba9ab17346477', '2024-03-31 15:07:11.390154', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_scopes` VALUES ('3a12dc55-5e5b-4f9c-f7b8-acb34b8e07d5', NULL, NULL, 'PaymentServiceScope API 接口服务', NULL, 'PaymentServiceScope', NULL, '[\"PaymentService\"]', '{}', '626b215a029b4318a491d4563e1e1615', '2024-05-30 23:40:04.605815', NULL, NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for auth_tokens
-- ----------------------------
DROP TABLE IF EXISTS `auth_tokens`;
CREATE TABLE `auth_tokens`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `ApplicationId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `AuthorizationId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `CreationDate` datetime(6) NULL DEFAULT NULL,
  `ExpirationDate` datetime(6) NULL DEFAULT NULL,
  `Payload` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `RedemptionDate` datetime(6) NULL DEFAULT NULL,
  `ReferenceId` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Subject` varchar(400) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
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
  INDEX `IX_Auth_Tokens_ApplicationId_Status_Subject_Type`(`ApplicationId`, `Status`, `Subject`, `Type`) USING BTREE,
  INDEX `IX_Auth_Tokens_AuthorizationId`(`AuthorizationId`) USING BTREE,
  INDEX `IX_Auth_Tokens_ReferenceId`(`ReferenceId`) USING BTREE,
  CONSTRAINT `FK_Auth_Tokens_Auth_Applications_ApplicationId` FOREIGN KEY (`ApplicationId`) REFERENCES `auth_applications` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Auth_Tokens_Auth_Authorizations_AuthorizationId` FOREIGN KEY (`AuthorizationId`) REFERENCES `auth_authorizations` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of auth_tokens
-- ----------------------------
INSERT INTO `auth_tokens` VALUES ('3a1b5de8-42d2-c6c0-ce53-0f24683ed479', '3a0d39f3-6374-331f-cba8-7d40a0a05331', NULL, '2025-07-27 19:53:50.000000', '2025-08-26 19:53:50.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'cb78090f87b34809afec66e838537614', '2025-07-27 19:53:50.648824', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b6b90-5e15-f5da-aec2-6169a9f6b28c', '3a0d39f3-6374-331f-cba8-7d40a0a05331', NULL, '2025-07-30 11:32:31.000000', '2025-08-29 11:32:31.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '3b726d7b072d45c18e6ac71da2922dd0', '2025-07-30 11:32:31.512653', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b6dcd-2484-bfda-42df-956c9341a63b', '3a0d39f3-6374-331f-cba8-7d40a0a05331', NULL, '2025-07-30 21:58:08.000000', '2025-08-29 21:58:08.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '9b229a72b6424f8d9cf7ba3bd989ecc0', '2025-07-30 21:58:08.904407', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7805-ac44-aa83-97eb-089ec65f4cdf', '3a0d39f3-6374-331f-cba8-7d40a0a05331', NULL, '2025-08-01 21:36:05.000000', '2025-08-31 21:36:05.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'f1abbfe1e07d432cbcfad4c90b4d788d', '2025-08-01 21:36:05.821055', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7b60-9373-7a9b-2fcf-d0301d00d4e8', '3a0d39f3-6374-331f-cba8-7d40a0a05331', NULL, '2025-08-02 13:14:14.000000', '2025-09-01 13:14:14.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '1a086da4d9314d07ae7f770b1e517951', '2025-08-02 13:14:14.903045', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7c2e-6fc8-9583-3fe6-c5a78228b4ef', '3a0d00d2-a32e-799f-d6b7-36c7f8a5f4c5', '3a1a3daa-fd50-c2f7-cac1-76b5824a494c', '2025-08-02 16:59:06.000000', '2025-09-01 16:59:06.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'cf4c8d00ca3a49258969bdad1514c222', '2025-08-02 16:59:06.059380', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7cba-8c63-c85d-0037-7ebb10e8c999', '3a0d39f3-6374-331f-cba8-7d40a0a05331', NULL, '2025-08-02 19:32:08.000000', '2025-09-01 19:32:08.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'd4b444122efa4a6bae70b0cc1993f745', '2025-08-02 19:32:08.421899', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7d16-b309-8079-726a-b24c7e353f83', '3a118138-ec69-8903-f842-22ded68b3ede', NULL, '2025-08-02 21:12:47.000000', '2025-09-01 21:12:47.000000', NULL, NULL, NULL, NULL, 'valid', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 'access_token', '{}', '442421c0e4fe4aefa00ff00982ac181c', '2025-08-02 21:12:47.628398', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7de9-8635-6091-895c-feb14406f043', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 01:03:04.000000', '2025-09-02 01:03:04.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'c6415a57eb9542629a7aa9bfbfa643d6', '2025-08-03 01:03:04.250894', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7dea-7db5-2e47-a19b-1596b513f137', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 01:04:07.000000', '2025-09-02 01:04:07.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '6cffaa8083ee4b5d8dc45bb17236e321', '2025-08-03 01:04:07.610095', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7e51-bf8f-3a4a-471c-6e6440a36bb0', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 02:56:54.000000', '2025-09-02 02:56:54.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'e202933e878e40b99bb2fcf9809b9655', '2025-08-03 02:56:54.677354', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7e6b-3f43-7797-8839-7bb899fdc0c7', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 03:24:45.000000', '2025-09-02 03:24:45.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '2cf6f67e25e648bcb4361be95545fb87', '2025-08-03 03:24:45.768339', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7e82-3de1-a85d-79f1-a77009bed1b1', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 03:49:52.000000', '2025-09-02 03:49:52.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'c2cdec50888d4bdca7c6bd210d3f0c50', '2025-08-03 03:49:52.740708', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7e82-e96e-d119-935b-7ef7f00a0075', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 03:50:36.000000', '2025-09-02 03:50:36.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'f39f66fc40e041d59318d9ed210fe183', '2025-08-03 03:50:36.657653', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7e85-c25b-f63c-2266-a26a64e54ecf', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 03:53:43.000000', '2025-09-02 03:53:43.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '961fad2404cc4840a2766b1c3bc92877', '2025-08-03 03:53:43.262337', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7e87-4ba3-9b28-c097-6e4688ae076f', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 03:55:23.000000', '2025-09-02 03:55:23.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '5bcb27ba206340b5888f5dbf02361c8f', '2025-08-03 03:55:23.942924', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7e89-3db3-a337-b2c7-aff1130b489f', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 03:57:31.000000', '2025-09-02 03:57:31.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '171be5fbaaa94a5695ed2a41645e0279', '2025-08-03 03:57:31.445891', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7e8f-5e41-e025-a18b-7005cf890d55', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:04:12.000000', '2025-09-02 04:04:12.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '08ee06d5f905430395a60c95a5f0aca4', '2025-08-03 04:04:12.995858', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7e95-61e6-02f2-f0f2-09d3af970772', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:10:47.000000', '2025-09-02 04:10:47.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '6af9b402b5e545649dbbda8e9ac4dc53', '2025-08-03 04:10:47.145590', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7e97-86f2-b085-69ae-6aa1f8ed2a43', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:13:07.000000', '2025-09-02 04:13:07.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'd32f68838c05460fbba191b1fcb6dae0', '2025-08-03 04:13:07.701800', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7e99-c62e-80ee-d3f3-ad2cd26c13e3', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:15:34.000000', '2025-09-02 04:15:34.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '3698d23a84ac44748e60f0824e69580e', '2025-08-03 04:15:34.960749', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7e9a-e4b9-dfb0-8ba7-89de00c670ab', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:16:48.000000', '2025-09-02 04:16:48.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '403444f23c764c98b0a651ba29d4fa08', '2025-08-03 04:16:48.316698', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7e9c-ae9d-b3d4-732f-2eae37981bce', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:18:45.000000', '2025-09-02 04:18:45.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '86943403ba714230b86db3156bc38d0d', '2025-08-03 04:18:45.536071', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7e9f-605a-63f9-3b25-78ff5b82e64f', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:21:42.000000', '2025-09-02 04:21:42.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '7566de8dea6c4032af13cd8550c6330e', '2025-08-03 04:21:42.109375', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7ea2-7b18-f4a1-abe6-1b14df6fe00e', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:25:05.000000', '2025-09-02 04:25:05.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '3e659881efdc421f9d1f8372de83b0c1', '2025-08-03 04:25:05.563074', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7eab-32e7-b60b-b33c-336050de377f', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:34:36.000000', '2025-09-02 04:34:36.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '386aac3e477549a4aec5ce316a2a7a9d', '2025-08-03 04:34:36.906463', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7eac-c8e6-6c1c-3008-79b0613caaf1', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:36:20.000000', '2025-09-02 04:36:20.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'c37a8991c5854913ba5f70d17ccf4ad3', '2025-08-03 04:36:20.841085', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7eae-8aa1-3c81-337a-4449465ca3d9', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:38:15.000000', '2025-09-02 04:38:15.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'cfebd8a6ef03496b880e9f8fe54cf58a', '2025-08-03 04:38:15.972003', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7eb3-4d90-998f-b19a-7e5c2c0e58de', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:43:28.000000', '2025-09-02 04:43:28.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '7f2840330e2f4ba5bb76aa21e563913e', '2025-08-03 04:43:28.019484', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7eb5-6357-5f67-25cc-20bb5dc836a5', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:45:44.000000', '2025-09-02 04:45:44.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '836a712c31114babb8948287a20040b6', '2025-08-03 04:45:44.666873', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7eba-e538-72f7-d1ce-6f0b857d7af1', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:51:45.000000', '2025-09-02 04:51:45.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '2c180d1818c746e1ad00397b185933a2', '2025-08-03 04:51:45.594719', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7ebb-ef80-d27e-9c0b-af8949a10a66', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:52:53.000000', '2025-09-02 04:52:53.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'ca99de356e0e453c846421f8ee06f185', '2025-08-03 04:52:53.762783', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7ebe-20d3-6717-a586-ae48845b1e61', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 04:55:17.000000', '2025-09-02 04:55:17.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'd75c69b1d1054e14a66758496834d558', '2025-08-03 04:55:17.461944', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7ec4-9793-5f26-2070-e9ac9e74621a', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 05:02:21.000000', '2025-09-02 05:02:21.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '3d7e2b5d34a24c51b0798a8a3da86937', '2025-08-03 05:02:21.077947', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7ec5-71cb-eeff-d87d-bdc699ac1ad4', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 05:03:16.000000', '2025-09-02 05:03:16.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '7ce1a941102f4139b1addbd0d6d73e1b', '2025-08-03 05:03:16.941897', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7ec6-c34d-708e-f3a3-aaf55cfd3964', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 05:04:43.000000', '2025-09-02 05:04:43.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '3e6d5c13094a430fad3394004d9e0d1d', '2025-08-03 05:04:43.344034', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7ecd-dbf4-bdbf-466b-290e2c288344', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 05:12:28.000000', '2025-09-02 05:12:28.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '1840ca3da593431b963eba78c4192f2c', '2025-08-03 05:12:28.406591', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7ed3-fad3-ef85-c00b-b9625617edd8', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 05:19:09.000000', '2025-09-02 05:19:09.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'a3efac16cf15421e9d9096351d37fd0e', '2025-08-03 05:19:09.525711', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7f5a-53ff-e9bb-00b2-d22fcbad8887', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-03 07:45:54.000000', '2025-09-02 07:45:54.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '85256ca2b11548f0a62232d18ab21e14', '2025-08-03 07:45:54.179647', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1b7f64-e068-b87e-a0b6-60f5458f5321', '3a0d39f3-6374-331f-cba8-7d40a0a05331', NULL, '2025-08-03 07:57:25.000000', '2025-09-02 07:57:25.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'd28b4726d29f4ff381336c87f59eb7e7', '2025-08-03 07:57:25.484933', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1bab92-cc43-47b2-8602-cb063130d3b6', '3a0d39f3-6374-331f-cba8-7d40a0a05331', NULL, '2025-08-11 21:50:52.000000', '2025-09-10 21:50:52.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'c3b753c2ce004fe8af94a62ad8e69305', '2025-08-11 21:50:52.622934', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1babc9-31fa-88b2-e47e-e1bb11d7d346', '3a118138-ec69-8903-f842-22ded68b3ede', NULL, '2025-08-11 22:50:17.000000', '2025-09-10 22:50:17.000000', NULL, NULL, NULL, NULL, 'valid', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 'access_token', '{}', '9f649a106d39412cb0a5afee20846043', '2025-08-11 22:50:17.591804', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1bb0f0-cb1c-e656-3a8c-41277185cec5', '3a0d39f3-6374-331f-cba8-7d40a0a05331', NULL, '2025-08-12 22:51:38.000000', '2025-09-11 22:51:38.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'b573e4af38ca4aa48d2b2f9ff761300d', '2025-08-12 22:51:38.771286', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1bb396-9038-6f85-4039-4a5b7f06e096', '3a0d39f3-6374-331f-cba8-7d40a0a05331', NULL, '2025-08-13 11:11:56.000000', '2025-09-12 11:11:56.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '8a032c98fa204d3f9d945e26707f12a3', '2025-08-13 11:11:57.114473', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1bc06c-22be-39ef-e982-063126380171', '3a0d39f3-6374-331f-cba8-7d40a0a05331', NULL, '2025-08-15 23:00:40.000000', '2025-09-14 23:00:40.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'f4dae0b057f348bca3f12d632072c681', '2025-08-15 23:00:40.385515', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1bc32c-5b92-a855-bbe2-fd99183a9973', '3a0d39f3-6374-331f-cba8-7d40a0a05331', NULL, '2025-08-16 11:49:52.000000', '2025-09-15 11:49:52.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '2c95fda5df034f84aec34e057d899965', '2025-08-16 11:49:52.291169', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1bc567-4d04-1f09-6cb2-9ecafde4699e', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-17 06:13:29.000000', '2025-08-17 06:18:29.000000', 'eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiJGMDMyNUUzOTFGMEQwRkMxNTRGRTZBQjBBRjIzOTJDQUY4QzE1RDE2IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.eC0jb4nDyNrWbTArdg_6kEytRLoMAH7B-CtfYN90vtTbqMgVU0WBLn24qu8zq5hSnZ62NdlnsMV7kKLzFJKT2co2wL18KfIB5Y2W_Kzq2Cm5aenwx16MsXOP1zGAr999PYiYEAxGTyTXRiBO0xY7zX5cqctSlKJ8KUHO_9Z5JDYpg_G2DdFq1AOKPTg7F7MFaND48BO-1G5eiTXXP61khQq6BbGj88DVuvzjw4LbQoQdo-P886uooAyOAC4j6G7IDqS221bwPyy0Bn0XuZ0TOpBvWCKiNSouzZOCE9y1DjA2uyVHat2LpQQq8Dq64NwuZ5hQjoy0Q_bZH8idl_zyKg.uJaGVw9rCEQlt3OzW6SOsQ.quTQsXfQLRfwwfU8-66UZCd1fbWJwvonjOwZHSJ5PQbYlttTT2E7aPVeXh_F9Q2wwScbY1IJoSZ4S4MIU9II7S-zjGD-zJ7wR6R2ZQyoVQDbjs4IYhxMox1s7xomHjLkIEYfaRtDR13maOLxbhaT3zvNLbESHSPNsdbUYGNr3SqaOgBh6kjVLzELu0H2poOF6-BdxxR44C_PvCXxl1SGqFAP8xBk_jkSCgU30ckCrRP5CeLgJkraoWf9x8T7vOY2Rf_LzOZl39UBGFEXija46ntZevRThfGAmQfbm5tylgPqLoPRBz3uVoNnMseA5yzvIfwGt_qFFrWhujzuFcTMN7-Pg8Dp8XKbgH2It1IEbAfV10weEeOKtS_WUJbYCNeYVsHQhrbRnTYd3ulrJ7FpUH8m-nC1vTF9Vp7j8DCMOcUL0qZfW4p0L3LdLCSBlQQhT3W986mOa8R-gu4JL5wjw_yWlZLpUWvuF519L7jusGWFA1bcW_TY1E9pDZA5ebvmI7CespC-Gc5bVCZnefeheKt09AZx98wiomwDoh9IQD43VMvBhgSggP9DPiU6ezCpqZ76FYkpmTTB0_upbUKsFf_DwsJCzBmmibJZVmLIs_yJZHtvtQSoJ9zzGja58TYU7ZTUymycRd-dmLsrgtKmHffikXhPsnzcNaLiucdm8yTtLbpQMncd_97Z-9BpHw3TI6Tw9r4V_eHGntc9ILLRGVVkmCqW7ZeVb4V-bngKeOQ_NaZcTWh_7HDJ2_Lb5F7kLmSe0dltg32nXwBNQoSOThvaYDBE2fkDK6U2lhWlNJbIl9aJPlZGoUkGV-MC23fCVPpV2tEIFsbWTeC4KtPdoqkQJdJzuiC2MpjKvkNxA0rsZlyOvrtMhIJ1ANaM5mZxLy8Fumr14abGVkZk35A2gB9xuo0dAZsVZ7EWNx-AYDtl6bIFYs0qRXUkTKs5lYc4tYoWHe-tIPAU8sJP1vlnjH_Dh7reaPTDEfvZMLmtjyFBVjAEJUSr7Jz0AXpu3mu2fj3sWYcd1v79Wqbd5nTmKAblK9ywBq3udPDDBLTaNLlrIL-VaB6RvTAijNFbggYh1W-Uu1LR4M-jtCdL5AyYN9_mYByhYVSpxE8sj_3IifIFnxk3AC4U5hflRSAqGgQezA2-85bw2SoPyKztZ7knjbIMcooFPITXPR61aQF18b3YPhUL14GRoEf015geafO-pAs54SP6ACGQvwXEUoNYeAqHW5wN1E1DYLK1iUzNieuSHqnme7Un9DjXSiUvrOoxddQypa_Aqabtt7E4TmvGudFdr_qfCumcSpN8xzdm2oJLtQjpt2AvdvKRs6iXMV1DHrnBBe93dH9hE4PFpiLtpUdGPl2gMhh_Yb_tfQdNGFi33vz3yXLz4lfxHXu6W1PdFdYyqb-ElVD6407eCURQHPJnuMpCAX73WbB5uJekSzd7HwLCHl6DcW_ACccnnk-WyWKQAJo-WD7SzhXYhHVZIgkeIslDDa9WMFjpbZ6BgfwXOHeiKpX7yowaJiUDX8N8AEh7o64LnDQHOzr9XV4w2RuhKnF8_zZl-L5z0R668VwiCAgYhvsPrJHKb0I_wrIu7lBTSbr-6GzMrZi4PrmRmuypkkmYPaCfZXzOTAVO9OQCyBI8cXGhZKvr2zC-2aXlr0eNPRbvg7DlWjaSKMLPQVisdz5EK1fVkJNvSLOWz7BbpsjcZ2IItjMZwHdwyld6ybba2p8doKBIZGuVZVrTssaW8I7_o5YMPxjXils75_blV8wMClfvubYzrgzedd5JUHeS2h9S9N2R7P-WFraw5Ver9tyxuZJg0f4I4BAJszUkmpJ_VpdflS5ct-zVWQGpZs1dj6nkgJNEuxD5gF_xY15g6V-nRqeIBxZghlNnmSnV8ruqyGhNdo04Rw2TH8kogSTrjnEQonLHOOjWUR-nsXLsQelAgh5CakAG4F4JVvrK7FXvgMOEQXjlZxIXm2cI8SaZm0Or9Tqt4zvdb58RGWEE2yiwJNyCyjmXl25KY8g6rhbLnnuSfVJlojGNXZBD-YWHb49eQdQoAHLqV0dUsqRSy4hFfP-v563F1MJkkVVUTzVZFaDw5zW3ibJ020zpBUB_a0xAeB72WRsO2ksph2j-NUSxRaCpoDQ1lTtAHUpSk8zWLyG4w2JWgQz8jmGDmnUIuxDbRh2XhoAmbALexvgS0FKVeuNBEIBv1qhG87tk1_cZByBOtVhO_8WWHyENYIZAzup4UoMCAvGWN-6osfsGDy67IDyTWxbXqBqSdMkcfF5cd_EVsjp0UbixSptc-n5FGcey6xA1wgYHuDgpo_8QSK6wAaV430hAnd4ZKjfy2y5vRlwuEeuv_3M-Y24J-mIc9RFu_vxuas2yyIcMIDPVzdwulMTZf1p7APDIFwzJFNbhh5ftCL4KoYDkCTxjZgrHaVPZK-nP3rhgDoZNsZ9sfGSPoIf_FWLEAiH0Hpb5XpQtB0x8L90giqscHTft-WnjuwrKOggUJKYmdQGRPhm9u6yaNNGPDih-C13TGDjZVgwIxDdvSgYgtqwGq4yL8gSemmSsl2U6oRHXnTZ7ag.WBuRwDA7X-oMMiP9QE8rDqsNGlNcBqrqgY4sK0fyTx0', NULL, '2025-08-16 22:13:30.365165', 'jShApU+oeQTL1bZ9pdR+7L9aaY/jqFYSP0ut/BWYedo=', 'redeemed', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'authorization_code', '{}', 'be9535a5d18642158827ab8419e5e7a4', '2025-08-16 22:13:29.640387', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-16 22:13:30.388671', NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1bc567-50b8-2dde-fe42-caed5cdab49d', '3a11a582-b371-412c-f509-6599f90015b7', '3a180162-1dbd-74d2-bcef-e8c9be829846', '2025-08-16 22:13:30.000000', '2025-09-15 22:13:30.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '7dd981cc86304516bf266c47f980806c', '2025-08-16 22:13:30.430226', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1be861-d62c-aeb9-d91f-c3d4ec29df08', '3a0d39f3-6374-331f-cba8-7d40a0a05331', NULL, '2025-08-23 17:14:13.000000', '2025-09-22 17:14:13.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', 'd0f9d7659b6e40f19460e8b196f888b8', '2025-08-23 17:14:14.113266', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `auth_tokens` VALUES ('3a1be938-7040-8b0a-7fc8-cf3b0952a6c1', '3a0d39f3-6374-331f-cba8-7d40a0a05331', NULL, '2025-08-23 21:08:38.000000', '2025-08-30 21:08:38.000000', NULL, NULL, NULL, NULL, 'valid', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'access_token', '{}', '11c6e83615f74ece96dca35b884e4254', '2025-08-23 21:08:38.265388', NULL, NULL, NULL, 0, NULL, NULL);

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
INSERT INTO `cap.published` VALUES (9027127011260612609, 'v1', 'User.Order.Create', '{\"Headers\":{\"cap-callback-name\":null,\"cap-msg-id\":\"9027127011260612609\",\"cap-corr-id\":\"9027127011260612609\",\"cap-corr-seq\":\"0\",\"cap-msg-name\":\"User.Order.Create\",\"cap-msg-type\":\"OrderCreateEto\",\"cap-senttime\":\"2025/8/23 23:36:35 \\u002B08:00\"},\"Value\":{\"No\":\"117559633286796\",\"UserId\":\"3a118d4a-e735-1846-9ae3-c4ced1efcd87\",\"GoodAmount\":280.00,\"PayedAmount\":0,\"OrderAmount\":112.00,\"PayStatus\":1,\"ShipStatus\":1,\"Status\":1,\"OrderType\":1,\"ReceiptType\":1,\"PaymentCode\":\"1\",\"PaymentTime\":null,\"LogisticsId\":\"3a121376-9297-88cc-48f5-c20675ad08b5\",\"LogisticsName\":\"\\u9ED8\\u8BA4\\u914D\\u9001\\u65B9\\u5F0F\",\"CostFreight\":10.00,\"SellerId\":\"00000000-0000-0000-0000-000000000000\",\"ConfirmStatus\":1,\"ConfirmTime\":null,\"StoreId\":null,\"ShipAreaId\":130202,\"DisplayArea\":\"\\u6CB3\\u5317\\u7701 - \\u5510\\u5C71\\u5E02 - \\u8DEF\\u5357\\u533A\",\"ShipAddress\":\"\\u6D4B\\u8BD5\\u5730\\u5740\\u96F6\\u58F9\",\"ShipName\":\"Tom\",\"ShipMobile\":\"18888888888\",\"Weight\":0,\"Point\":1860,\"PointMoney\":18,\"GradeDiscountAmount\":20.00,\"SeckillDiscountAmount\":0,\"OrderDiscountAmount\":0,\"GoodsDiscountAmount\":84.00,\"CouponDiscountAmount\":56.00,\"Coupon\":\"[\\u0022QfCtj8qW8C\\u0022]\",\"PromotionList\":\"[{\\u0022IsUseCoupon\\u0022:false,\\u0022CouponCode\\u0022:\\u0022QfCtj8qW8C\\u0022,\\u0022CouponName\\u0022:\\u0022\\\\u82F9\\\\u679C\\\\u4F18\\\\u60E0\\\\u52B5\\u0022,\\u0022Code\\u0022:\\u0022GOODS_DISCOUNT\\u0022,\\u0022Parameters\\u0022:\\u0022{\\\\u0022discount\\\\u0022:\\\\u00228\\\\u0022}\\u0022,\\u0022Money\\u0022:56.00,\\u0022StartTime\\u0022:\\u00222025-08-23T23:01:36\\u002B08:00\\u0022,\\u0022EndTime\\u0022:\\u00222025-09-13T04:01:36\\u002B08:00\\u0022,\\u0022Id\\u0022:\\u00223a1be99f-ddfa-4a40-5e6f-dbb170ff2682\\u0022}]\",\"Memo\":\"\\u7528\\u987A\\u4E30\\u5FEB\\u9012\\u54E6\\uFF01\",\"Ip\":\"0.0.0.1\",\"Mark\":null,\"Source\":3,\"IsComment\":false,\"ObjectId\":null,\"IsUsePoint\":true,\"CartIds\":[\"3a1be9be-d92e-f026-5a62-f309d7a18d16\"],\"OrderItems\":[{\"OrderId\":\"00000000-0000-0000-0000-000000000000\",\"GoodId\":\"3a1be90a-804f-3ea5-ad0b-113f581c0d93\",\"ProductId\":\"3a1be90a-8089-d7dc-9e7b-99688c581768\",\"Sn\":\"SN711828016715654\",\"Bn\":\"GD1755950492353\",\"Name\":\"\\u70DF\\u53F0\\u9EC4\\u5143\\u5E05\\u82F9\\u679C\\u65B0\\u9C9C\\u6C34\\u679C\\u7C89\\u9762\\u82F9\\u679C\\u5F53\\u5B63\\u5B55\\u5987\\u8001\\u4EBA\\u5E38\\u59075\\u65A4\\u4E2D\\u679C\\u301010-12\\u4E2A\\u3011\",\"Price\":140.00000000000000000000000000,\"CostPrice\":40.000000000000000000000000000,\"MktPrice\":150.00000000000000000000000000,\"ImageUrl\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823201805/D01.png\",\"Nums\":2,\"Amount\":280.00,\"PromotionAmount\":0,\"PromotionList\":\"\",\"Weight\":0,\"SendNums\":0,\"Addon\":\"\\u7CBE\\u9009:5\\u65A4,\\u4E2D\\u679C:10-12\\u4E2A\",\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}],\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:36:35', '2025-08-24 23:36:35', 'Succeeded');
INSERT INTO `cap.published` VALUES (9027127011260612610, 'v1', 'User.Balance.Payment', '{\"Headers\":{\"cap-callback-name\":null,\"cap-msg-id\":\"9027127011260612610\",\"cap-corr-id\":\"9027127011260612610\",\"cap-corr-seq\":\"0\",\"cap-msg-name\":\"User.Balance.Payment\",\"cap-msg-type\":\"UserBalancePaymentEto\",\"cap-senttime\":\"2025/8/23 23:36:56 \\u002B08:00\"},\"Value\":{\"BillPaymentNo\":\"217559634159954\",\"Remark\":\"\\u4F59\\u989D\\u652F\\u4ED8\\u3002\",\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:36:56', '2025-08-24 23:36:56', 'Succeeded');
INSERT INTO `cap.published` VALUES (9027127011260612611, 'v1', 'User.Order.Create', '{\"Headers\":{\"cap-callback-name\":null,\"cap-msg-id\":\"9027127011260612611\",\"cap-corr-id\":\"9027127011260612611\",\"cap-corr-seq\":\"0\",\"cap-msg-name\":\"User.Order.Create\",\"cap-msg-type\":\"OrderCreateEto\",\"cap-senttime\":\"2025/8/23 23:38:09 \\u002B08:00\"},\"Value\":{\"No\":\"117559634812452\",\"UserId\":\"3a118d4a-e735-1846-9ae3-c4ced1efcd87\",\"GoodAmount\":170.00,\"PayedAmount\":0,\"OrderAmount\":112.00,\"PayStatus\":1,\"ShipStatus\":1,\"Status\":1,\"OrderType\":1,\"ReceiptType\":1,\"PaymentCode\":\"1\",\"PaymentTime\":null,\"LogisticsId\":\"3a121376-9297-88cc-48f5-c20675ad08b5\",\"LogisticsName\":\"\\u9ED8\\u8BA4\\u914D\\u9001\\u65B9\\u5F0F\",\"CostFreight\":10.00,\"SellerId\":\"00000000-0000-0000-0000-000000000000\",\"ConfirmStatus\":1,\"ConfirmTime\":null,\"StoreId\":null,\"ShipAreaId\":130202,\"DisplayArea\":\"\\u6CB3\\u5317\\u7701 - \\u5510\\u5C71\\u5E02 - \\u8DEF\\u5357\\u533A\",\"ShipAddress\":\"\\u6D4B\\u8BD5\\u5730\\u5740\\u96F6\\u58F9\",\"ShipName\":\"Tom\",\"ShipMobile\":\"18888888888\",\"Weight\":0,\"Point\":1240,\"PointMoney\":12,\"GradeDiscountAmount\":5.00,\"SeckillDiscountAmount\":0,\"OrderDiscountAmount\":0,\"GoodsDiscountAmount\":51.00,\"CouponDiscountAmount\":0,\"Coupon\":null,\"PromotionList\":null,\"Memo\":\"\",\"Ip\":\"0.0.0.1\",\"Mark\":null,\"Source\":3,\"IsComment\":false,\"ObjectId\":null,\"IsUsePoint\":true,\"CartIds\":[\"3a1be9c1-3264-77cf-a96a-5f6cc6a799ee\"],\"OrderItems\":[{\"OrderId\":\"00000000-0000-0000-0000-000000000000\",\"GoodId\":\"3a1be8f5-77c6-abfd-9f34-bbbc0419b30f\",\"ProductId\":\"3a1be8f5-77db-fbc0-6974-ea8afb90d743\",\"Sn\":\"SN1755949056611\",\"Bn\":\"GD1755949056511\",\"Name\":\"\\u534E\\u5723\\u3010\\u7B51\\u68A6\\u592A\\u7A7A\\u793C\\u76D2\\u30112.4kg \\u822A\\u5929\\u7EA7\\u751F\\u9C9C\\u793C\\u76D2 \\u7CBE\\u54C1\\u539F\\u751F\\u6001\\u7EA2\\u5BCC\\u58EB\\u82F9\\u679C \\u7B51\\u68A6\\u592A\\u7A7A\\u793C\\u76D2\",\"Price\":170.00000000000000000000000000,\"CostPrice\":50.000000000000000000000000000,\"MktPrice\":175.00000000000000000000000000,\"ImageUrl\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823195618/\\u534E\\u5723\\u82F9\\u679C-1.png\",\"Nums\":1,\"Amount\":170.00,\"PromotionAmount\":0,\"PromotionList\":\"\",\"Weight\":0,\"SendNums\":0,\"Addon\":\"\",\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}],\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:38:10', '2025-08-24 23:38:10', 'Succeeded');
INSERT INTO `cap.published` VALUES (9027127011260612612, 'v1', 'User.Balance.Payment', '{\"Headers\":{\"cap-callback-name\":null,\"cap-msg-id\":\"9027127011260612612\",\"cap-corr-id\":\"9027127011260612612\",\"cap-corr-seq\":\"0\",\"cap-msg-name\":\"User.Balance.Payment\",\"cap-msg-type\":\"UserBalancePaymentEto\",\"cap-senttime\":\"2025/8/23 23:38:17 \\u002B08:00\"},\"Value\":{\"BillPaymentNo\":\"217559634970746\",\"Remark\":\"\\u4F59\\u989D\\u652F\\u4ED8\\u3002\",\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:38:17', '2025-08-24 23:38:17', 'Succeeded');
INSERT INTO `cap.published` VALUES (9027127011260612613, 'v1', 'User.Order.Create', '{\"Headers\":{\"cap-callback-name\":null,\"cap-msg-id\":\"9027127011260612613\",\"cap-corr-id\":\"9027127011260612613\",\"cap-corr-seq\":\"0\",\"cap-msg-name\":\"User.Order.Create\",\"cap-msg-type\":\"OrderCreateEto\",\"cap-senttime\":\"2025/8/23 23:40:10 \\u002B08:00\"},\"Value\":{\"No\":\"117559636078978\",\"UserId\":\"3a118d4a-e735-1846-9ae3-c4ced1efcd87\",\"GoodAmount\":104.00,\"PayedAmount\":0,\"OrderAmount\":78.80,\"PayStatus\":1,\"ShipStatus\":1,\"Status\":1,\"OrderType\":1,\"ReceiptType\":1,\"PaymentCode\":\"1\",\"PaymentTime\":null,\"LogisticsId\":\"3a121376-9297-88cc-48f5-c20675ad08b5\",\"LogisticsName\":\"\\u9ED8\\u8BA4\\u914D\\u9001\\u65B9\\u5F0F\",\"CostFreight\":10.00,\"SellerId\":\"00000000-0000-0000-0000-000000000000\",\"ConfirmStatus\":1,\"ConfirmTime\":null,\"StoreId\":null,\"ShipAreaId\":130202,\"DisplayArea\":\"\\u6CB3\\u5317\\u7701 - \\u5510\\u5C71\\u5E02 - \\u8DEF\\u5357\\u533A\",\"ShipAddress\":\"\\u6D4B\\u8BD5\\u5730\\u5740\\u96F6\\u58F9\",\"ShipName\":\"Tom\",\"ShipMobile\":\"18888888888\",\"Weight\":0,\"Point\":0,\"PointMoney\":0,\"GradeDiscountAmount\":4.00,\"SeckillDiscountAmount\":0,\"OrderDiscountAmount\":0,\"GoodsDiscountAmount\":31.20,\"CouponDiscountAmount\":0,\"Coupon\":null,\"PromotionList\":null,\"Memo\":\"\",\"Ip\":\"0.0.0.1\",\"Mark\":null,\"Source\":3,\"IsComment\":false,\"ObjectId\":null,\"IsUsePoint\":false,\"CartIds\":[\"3a1be9c3-2155-fe9c-a9d9-77f2577dd574\"],\"OrderItems\":[{\"OrderId\":\"00000000-0000-0000-0000-000000000000\",\"GoodId\":\"3a1be913-be5c-9884-7a18-17fb9b8a1860\",\"ProductId\":\"3a1be916-1f3f-89c7-7555-9f268a96f7ed\",\"Sn\":\"SN711831636338565\",\"Bn\":\"GD1755951693837\",\"Name\":\"\\u5E7F\\u897F\\u5C0F\\u7C73\\u8549\\u5F53\\u5B63\\u65B0\\u9C9C\\u5E94\\u5B63\\u6C34\\u679C\\u65E5\\u65E5\\u9C9C\\u9999\\u751C\\u5C0F\\u9999\\u8549\\u82AD\\u8549\\u5B55\\u5987\\u6C34\\u679C\\u6574\\u7BB1 \\u51C0\\u91CD9\\u65A4 \\u9178\\u751C\\u53E3\\u7684\\u5C0F\\u9999\\u8549\",\"Price\":52.000000000000000000000000000,\"CostPrice\":10.000000000000000000000000000,\"MktPrice\":55.000000000000000000000000000,\"ImageUrl\":\"http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250823203034/GXXMJ.png\",\"Nums\":2,\"Amount\":104.00,\"PromotionAmount\":0,\"PromotionList\":\"\",\"Weight\":0,\"SendNums\":0,\"Addon\":\"\\u7CBE\\u9009:9\\u65A4\",\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}],\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:40:10', '2025-08-24 23:40:10', 'Succeeded');

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
INSERT INTO `cap.received` VALUES (9027127011265449985, 'v1', 'User.Change.Point', 'Rex.BaseService.HttpApi.Host.v1', '{\"Headers\":{\"cap-callback-name\":null,\"cap-corr-id\":\"9027127011262185475\",\"cap-corr-seq\":\"0\",\"cap-msg-id\":\"9027127011262185475\",\"cap-msg-name\":\"User.Change.Point\",\"cap-msg-type\":\"UserChangePointEto\",\"cap-senttime\":\"2025/8/23 23:36:35 \\u002B08:00\",\"cap-msg-group\":\"Rex.BaseService.HttpApi.Host.v1\",\"cap-exec-instance-id\":\"LAPTOP-A4AM01CM\"},\"Value\":{\"UserId\":\"3a118d4a-e735-1846-9ae3-c4ced1efcd87\",\"PointType\":3,\"Point\":-1860,\"Remark\":\"\\u8BA2\\u5355\\u3010117559633286796\\u3011\\u4F7F\\u7528\\u79EF\\u5206\\uFF1A-1860\\u3002\",\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:36:36', '2025-08-24 23:36:37', 'Succeeded');
INSERT INTO `cap.received` VALUES (9027127011265449986, 'v1', 'Change.User.Balance', 'Rex.BaseService.HttpApi.Host.v1', '{\"Headers\":{\"cap-callback-name\":null,\"cap-corr-id\":\"9027127011347533827\",\"cap-corr-seq\":\"0\",\"cap-msg-id\":\"9027127011347533827\",\"cap-msg-name\":\"Change.User.Balance\",\"cap-msg-type\":\"ChangeUserBalanceEto\",\"cap-senttime\":\"2025/8/23 23:36:56 \\u002B08:00\",\"cap-msg-group\":\"Rex.BaseService.HttpApi.Host.v1\",\"cap-exec-instance-id\":\"LAPTOP-A4AM01CM\"},\"Value\":{\"UserId\":\"3a118d4a-e735-1846-9ae3-c4ced1efcd87\",\"Type\":1,\"Money\":112.00000000000000000000000000,\"SourceId\":\"3a1be9c0-35c8-5914-37cd-6eeaacfa2c64\",\"CateMoney\":null,\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:36:56', '2025-08-24 23:36:57', 'Succeeded');
INSERT INTO `cap.received` VALUES (9027127011265449987, 'v1', 'User.Change.Point', 'Rex.BaseService.HttpApi.Host.v1', '{\"Headers\":{\"cap-callback-name\":null,\"cap-corr-id\":\"9027127011262185479\",\"cap-corr-seq\":\"0\",\"cap-msg-id\":\"9027127011262185479\",\"cap-msg-name\":\"User.Change.Point\",\"cap-msg-type\":\"UserChangePointEto\",\"cap-senttime\":\"2025/8/23 23:38:09 \\u002B08:00\",\"cap-msg-group\":\"Rex.BaseService.HttpApi.Host.v1\",\"cap-exec-instance-id\":\"LAPTOP-A4AM01CM\"},\"Value\":{\"UserId\":\"3a118d4a-e735-1846-9ae3-c4ced1efcd87\",\"PointType\":3,\"Point\":-1240,\"Remark\":\"\\u8BA2\\u5355\\u3010117559634812452\\u3011\\u4F7F\\u7528\\u79EF\\u5206\\uFF1A-1240\\u3002\",\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:38:10', '2025-08-24 23:38:10', 'Succeeded');
INSERT INTO `cap.received` VALUES (9027127011265449988, 'v1', 'Change.User.Balance', 'Rex.BaseService.HttpApi.Host.v1', '{\"Headers\":{\"cap-callback-name\":null,\"cap-corr-id\":\"9027127011347533830\",\"cap-corr-seq\":\"0\",\"cap-msg-id\":\"9027127011347533830\",\"cap-msg-name\":\"Change.User.Balance\",\"cap-msg-type\":\"ChangeUserBalanceEto\",\"cap-senttime\":\"2025/8/23 23:38:17 \\u002B08:00\",\"cap-msg-group\":\"Rex.BaseService.HttpApi.Host.v1\",\"cap-exec-instance-id\":\"LAPTOP-A4AM01CM\"},\"Value\":{\"UserId\":\"3a118d4a-e735-1846-9ae3-c4ced1efcd87\",\"Type\":1,\"Money\":112.00000000000000000000000000,\"SourceId\":\"3a1be9c1-7285-0d23-8a8f-d60cc76eb2fc\",\"CateMoney\":null,\"EntityType\":null,\"KeysAsString\":null,\"Properties\":{}}}', 0, '2025-08-23 23:38:17', '2025-08-24 23:38:17', 'Succeeded');

-- ----------------------------
-- Table structure for sys_claimtypes
-- ----------------------------
DROP TABLE IF EXISTS `sys_claimtypes`;
CREATE TABLE `sys_claimtypes`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Required` tinyint(1) NOT NULL,
  `IsStatic` tinyint(1) NOT NULL,
  `Regex` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `RegexDescription` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Description` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ValueType` int NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_claimtypes
-- ----------------------------

-- ----------------------------
-- Table structure for sys_featuregroups
-- ----------------------------
DROP TABLE IF EXISTS `sys_featuregroups`;
CREATE TABLE `sys_featuregroups`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DisplayName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_Sys_FeatureGroups_Name`(`Name`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_featuregroups
-- ----------------------------
INSERT INTO `sys_featuregroups` VALUES ('3a0d00e1-e16e-66ae-cc84-f1267bf50446', 'SettingManagement', 'L:AbpSettingManagement,Feature:SettingManagementGroup', '{}');

-- ----------------------------
-- Table structure for sys_features
-- ----------------------------
DROP TABLE IF EXISTS `sys_features`;
CREATE TABLE `sys_features`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GroupName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ParentName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DisplayName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DefaultValue` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsVisibleToClients` tinyint(1) NOT NULL,
  `IsAvailableToHost` tinyint(1) NOT NULL,
  `AllowedProviders` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ValueType` varchar(2048) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_Sys_Features_Name`(`Name`) USING BTREE,
  INDEX `IX_Sys_Features_GroupName`(`GroupName`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_features
-- ----------------------------
INSERT INTO `sys_features` VALUES ('3a0d00e1-e170-98de-244c-94a9c1f04b5c', 'SettingManagement', 'SettingManagement.Enable', NULL, 'L:AbpSettingManagement,Feature:SettingManagementEnable', 'L:AbpSettingManagement,Feature:SettingManagementEnableDescription', 'true', 1, 0, NULL, '{\"name\":\"ToggleStringValueType\",\"properties\":{},\"validator\":{\"name\":\"BOOLEAN\",\"properties\":{}}}', '{}');
INSERT INTO `sys_features` VALUES ('3a0d00e1-e181-b9d2-13ae-4e09b433ebfe', 'SettingManagement', 'SettingManagement.AllowChangingEmailSettings', 'SettingManagement.Enable', 'L:AbpSettingManagement,Feature:AllowChangingEmailSettings', NULL, 'false', 1, 0, NULL, '{\"name\":\"ToggleStringValueType\",\"properties\":{},\"validator\":{\"name\":\"BOOLEAN\",\"properties\":{}}}', '{}');

-- ----------------------------
-- Table structure for sys_featurevalues
-- ----------------------------
DROP TABLE IF EXISTS `sys_featurevalues`;
CREATE TABLE `sys_featurevalues`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderName` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ProviderKey` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_Sys_FeatureValues_Name_ProviderName_ProviderKey`(`Name`, `ProviderName`, `ProviderKey`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_featurevalues
-- ----------------------------

-- ----------------------------
-- Table structure for sys_linkusers
-- ----------------------------
DROP TABLE IF EXISTS `sys_linkusers`;
CREATE TABLE `sys_linkusers`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `SourceUserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `SourceTenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `TargetUserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TargetTenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_Sys_LinkUsers_SourceUserId_SourceTenantId_TargetUserId_Targe~`(`SourceUserId`, `SourceTenantId`, `TargetUserId`, `TargetTenantId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_linkusers
-- ----------------------------

-- ----------------------------
-- Table structure for sys_menus
-- ----------------------------
DROP TABLE IF EXISTS `sys_menus`;
CREATE TABLE `sys_menus`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `PId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `MenuType` int NULL DEFAULT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Component` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ComponentAlias` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsLink` tinyint(1) NOT NULL,
  `MenuSort` int NOT NULL,
  `Path` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Redirect` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PermissionIdentifying` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `MetaInfo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
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
-- Records of sys_menus
-- ----------------------------
INSERT INTO `sys_menus` VALUES ('3a0c421b-e2db-0819-2cee-ee3429285f4f', NULL, '3fa85f64-5717-4562-b3fc-2c963f66afa6', 1, 'systemMenu', 'system/menu/index', '', 0, 4, '/system/menu', '', '', '{\"Title\":\"message.router.systemMenu\",\"Icon\":\"iconfont icon-caidan\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '88bdd8dd12144fdfbcb44536a2fb87ce', '2023-07-07 18:39:02.388441', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-08-03 22:44:09.362054', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c421c-60a9-a76f-16f9-f3c6e4fa3661', NULL, '3fa85f64-5717-4562-b3fc-2c963f66afa6', 1, 'systemUser', 'system/user/index', '', 0, 2, '/system/user', '', '', '{\"Title\":\"message.router.systemUser\",\"Icon\":\"iconfont icon-yonghu\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '9c40c64cea4449f08f9ca307ab309e4b', '2023-07-07 18:39:34.585787', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-11-17 22:53:58.675310', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c47a8-c51e-b9a6-11b4-1c6d2d529865', NULL, NULL, 1, 'home', 'home/index', '', 0, 0, '/home', '', 'BaseService.Homes', '{\"Title\":\"message.router.home\",\"Icon\":\"ele-House\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":true,\"Link\":\"\",\"IsIframe\":false}', '{}', '081d34bd4689440480c6a727739da044', '2023-07-08 20:31:01.434879', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-08-06 15:58:46.968923', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c887c-daff-ffae-bc60-babce28a20a5', NULL, '3fa85f64-5717-4562-b3fc-2c963f66afa6', 1, 'systemRole', 'system/role/index', '', 0, 3, '/system/role', '', '', '{\"Title\":\"message.router.systemRole\",\"Icon\":\"iconfont icon-quanxianziyuan\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '7cced4ba15b84151aaeee16a2c3a7734', '2023-07-21 10:38:22.496927', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-11-17 22:54:14.388284', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c8ad3-a02e-4f05-10b7-e6ce623c9034', NULL, '3a0c421b-e2db-0819-2cee-ee3429285f4f', 2, '', '', '', 0, 1, '', '', 'BaseService.Menus.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'dcc3ac710a344f768ecc3e4450d45064', '2023-07-21 21:32:23.497471', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-07-22 11:04:57.620496', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c8adf-5cf2-bf58-1407-bb458c8cb244', NULL, '3a0c421b-e2db-0819-2cee-ee3429285f4f', 2, '', '', '', 0, 2, '', '', 'BaseService.Menus.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'f6f7760c0e704bad83d36d5a3decd8c3', '2023-07-21 21:45:12.703266', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-07-22 11:04:49.597077', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c8ae0-0757-2788-f99c-d17368e7bb08', NULL, '3a0c421b-e2db-0819-2cee-ee3429285f4f', 2, '', '', '', 0, 3, '', '', 'BaseService.Menus.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '0e1f4a415cff4bc28e4fb2d828a09736', '2023-07-21 21:45:56.327663', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-07-22 11:04:53.438628', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c8ae5-6d66-b37a-1d74-26e051f263a0', NULL, '3a0c421c-60a9-a76f-16f9-f3c6e4fa3661', 2, '', '', '', 0, 0, '', '', 'AbpIdentity.Users.ManagePermissions', '{\"Title\":\"message.router.systemManagePermissions\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'c3e96c7bee56474faef0a7b72a238b24', '2023-07-21 21:51:50.126113', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-07-22 09:42:04.198357', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c8ae6-0ea6-f0b8-86b2-d4a62847bef5', NULL, '3a0c421c-60a9-a76f-16f9-f3c6e4fa3661', 2, '', '', '', 0, 3, '', '', 'AbpIdentity.Users.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '9623035a8c1f47b3b19804f18e724183', '2023-07-21 21:52:31.413496', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-07-22 11:05:54.596734', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c8ae6-c836-9aae-3384-03df68ad8bde', NULL, '3a0c421c-60a9-a76f-16f9-f3c6e4fa3661', 2, '', '', '', 0, 4, '', '', 'AbpIdentity.Users.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '2b1a0c70686341b5998c5336b339b117', '2023-07-21 21:53:18.913157', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-07-22 11:05:59.403063', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c8ae7-86dd-8e3c-3919-c6a42647ceea', NULL, '3a0c887c-daff-ffae-bc60-babce28a20a5', 2, '', '', '', 0, 2, '', '', 'AbpIdentity.Roles.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '4ede19a33ef343619eef75d5a926ea38', '2023-07-21 21:54:07.729263', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-07-22 11:07:18.197675', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c8ae8-b7cc-04c1-2d78-6793eaf631b5', NULL, '3a0c887c-daff-ffae-bc60-babce28a20a5', 2, '', '', '', 0, 3, '', '', 'AbpIdentity.Roles.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '422bdf9856654856840164c3d3ab442a', '2023-07-21 21:55:25.787701', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-07-22 11:07:23.406816', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c8ae9-8a9a-1171-41a7-27bd80527fbe', NULL, '3a0c887c-daff-ffae-bc60-babce28a20a5', 2, '', '', '', 0, 4, '', '', 'AbpIdentity.Roles.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'e3197ee3610b4a96b1787ca048c01fec', '2023-07-21 21:56:19.750917', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-07-22 11:07:27.653419', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c8d70-ba54-d956-fdf5-24cd9ac3872f', NULL, '3a0c421c-60a9-a76f-16f9-f3c6e4fa3661', 2, '', '', '', 0, 2, '', '', 'AbpIdentity.Users.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'c8fca08a9bcd44309b1f3126525a7dc3', '2023-07-22 09:43:13.760968', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-07-22 11:05:48.778751', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c8d83-867f-e855-78b5-3126a7a47a4b', NULL, '3a0c887c-daff-ffae-bc60-babce28a20a5', 2, '', '', '', 0, 0, '', '', 'AbpIdentity.Roles.ManagePermissions', '{\"Title\":\"message.router.systemManagePermissions\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '0e71eacfb1e64c86999b11418b807e63', '2023-07-22 10:03:45.672015', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c8dbb-4f12-9d3e-7726-a6c5bf19a144', NULL, '3a0c421b-e2db-0819-2cee-ee3429285f4f', 2, '', '', '', 0, 0, '', '', 'BaseService.Menus', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '5c5398facb02415091f671426eb41740', '2023-07-22 11:04:41.508339', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c8dbd-2260-24fc-5d82-909fa4c960ed', NULL, '3a0c421c-60a9-a76f-16f9-f3c6e4fa3661', 2, '', '', '', 0, 1, '', '', 'AbpIdentity.Users', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '6f0ccefbb6754bfe86811df109362e3d', '2023-07-22 11:06:41.128796', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0c8dbe-c751-346c-5d62-aef59661964d', NULL, '3a0c887c-daff-ffae-bc60-babce28a20a5', 2, '', '', '', 0, 1, '', '', 'AbpIdentity.Roles', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'f6d47ee77ac34c6c9706be114b8bd298', '2023-07-22 11:08:28.892610', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0cb7e8-b1f2-50d0-20c8-81746d2799c8', NULL, '3fa85f64-5717-4562-b3fc-2c963f66afa6', 1, 'systemOrganizationUnit', 'system/organizationUnit/index', '', 0, 1, '/system/organizationUnit', '', '', '{\"Title\":\"message.router.systemOrganizationUnit\",\"Icon\":\"iconfont icon-zuzhijigou\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '7865935e1689428ab76dc2866fd0d965', '2023-07-30 15:38:19.044997', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-11-17 22:52:48.896349', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0cb7ef-3a02-d126-4672-fd6977b739c5', NULL, '3a0cb7e8-b1f2-50d0-20c8-81746d2799c8', 2, '', '', '', 0, 6, '', '', 'BaseService.OrganizationUnits.ManagingRole', '{\"Title\":\"message.router.systemOrganizationUnitManagingRole\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'e7a82bdbc78e415283f4348c82bf8367', '2023-07-30 15:45:27.049378', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-07-31 14:19:38.284026', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0cb7f3-be28-b6bd-be6f-4d2a2cea69ba', NULL, '3a0cb7e8-b1f2-50d0-20c8-81746d2799c8', 2, '', '', '', 0, 5, '', '', 'BaseService.OrganizationUnits.ManagingUser', '{\"Title\":\"message.router.systemOrganizationUnitManagingUser\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '764d1dab6b1541039ef68c849e51d847', '2023-07-30 15:50:23.022539', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-07-31 14:19:49.675959', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0cb7f5-f34d-f43f-4afb-9697e2c62b1e', NULL, '3a0cb7e8-b1f2-50d0-20c8-81746d2799c8', 2, '', '', '', 0, 1, '', '', 'BaseService.OrganizationUnits', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '10b613e7b4ed4b72a92c5fb5c61c58da', '2023-07-30 15:52:47.698022', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0cb946-fc3c-6fff-1fbf-177d2b2f0d42', NULL, '3a0cb7e8-b1f2-50d0-20c8-81746d2799c8', 2, '', '', '', 0, 2, '', '', 'BaseService.OrganizationUnits.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '96becf7caea045feb9a6a987c721bd3e', '2023-07-30 22:00:55.645039', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0cb947-d038-2411-26df-486f3c978c68', NULL, '3a0cb7e8-b1f2-50d0-20c8-81746d2799c8', 2, '', '', '', 0, 3, '', '', 'BaseService.OrganizationUnits.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '4b508c838ad647388a4ce49ba3429e38', '2023-07-30 22:01:49.891959', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0cb948-ba53-eea4-1f44-82de1e55a47b', NULL, '3a0cb7e8-b1f2-50d0-20c8-81746d2799c8', 2, '', '', '', 0, 4, '', '', 'BaseService.OrganizationUnits.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'a21eb75167254af8955b76bbdad6ef37', '2023-07-30 22:02:49.819845', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-04d5-abc6-0f70c7cd7be0', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', NULL, 1, 'home', 'home/index', '', 0, 0, '/home', '', 'BaseService.Homes', '{\"Title\":\"message.router.home\",\"Icon\":\"ele-House\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":true,\"Link\":\"\",\"IsIframe\":false}', '{}', '081d34bd4689440480c6a727739da044', '2023-08-08 01:17:48.286901', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-18f4-0039-e9302eff037f', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179c-18ec-22e6-c218356bd07f', 1, 'systemOrganizationUnit', 'system/organization-unit/index', '', 0, 1, '/system/organizationUnit', '', '', '{\"Title\":\"message.router.systemOrganizationUnit\",\"Icon\":\"iconfont icon-shuxingtu\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '4f5cbb34c6354de183560a15ce68fe80', '2023-08-08 01:17:48.287786', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-1a8f-a89d-f16be913b3bd', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-fad0-5f1c-a060b450786c', 2, '', '', '', 0, 3, '', '', 'AbpIdentity.Roles.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '422bdf9856654856840164c3d3ab442a', '2023-08-08 01:17:48.287418', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-1fd3-ab43-f5ee2dc888d8', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-18f4-0039-e9302eff037f', 2, '', '', '', 0, 2, '', '', 'BaseService.OrganizationUnits.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '96becf7caea045feb9a6a987c721bd3e', '2023-08-08 01:17:48.287974', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-2de9-f38e-73dec603810f', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-fad0-5f1c-a060b450786c', 2, '', '', '', 0, 4, '', '', 'AbpIdentity.Roles.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'e3197ee3610b4a96b1787ca048c01fec', '2023-08-08 01:17:48.287492', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-4892-0c6c-e92667aa2a0b', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-a557-44c2-bf3380d99174', 2, '', '', '', 0, 1, '', '', 'AbpIdentity.Users', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '6f0ccefbb6754bfe86811df109362e3d', '2023-08-08 01:17:48.287708', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-5366-e9d1-7820bb1b939f', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-18f4-0039-e9302eff037f', 2, '', '', '', 0, 5, '', '', 'BaseService.OrganizationUnits.ManagingUser', '{\"Title\":\"message.router.systemOrganizationUnitManagingUser\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '764d1dab6b1541039ef68c849e51d847', '2023-08-08 01:17:48.287870', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-5c30-95e3-2d1c8319d895', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-a557-44c2-bf3380d99174', 2, '', '', '', 0, 0, '', '', 'AbpIdentity.Users.ManagePermissions', '{\"Title\":\"message.router.systemManagePermissions\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'c3e96c7bee56474faef0a7b72a238b24', '2023-08-08 01:17:48.287093', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-5d72-97e9-eb6e79680741', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-18f4-0039-e9302eff037f', 2, '', '', '', 0, 3, '', '', 'BaseService.OrganizationUnits.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '4b508c838ad647388a4ce49ba3429e38', '2023-08-08 01:17:48.288079', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-7b13-14a9-0dec0a0077f6', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-aabe-0790-e6ce5df72ef3', 2, '', '', '', 0, 1, '', '', 'FormManagement.Form', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"ele-Search\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'ec1bbfe0c2f14436b7030cb297443cb8', '2023-08-08 01:17:48.288387', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-7caa-bc10-188e3da500c5', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-18f4-0039-e9302eff037f', 2, '', '', '', 0, 6, '', '', 'BaseService.OrganizationUnits.ManagingRole', '{\"Title\":\"message.router.systemOrganizationUnitManagingRole\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'e7a82bdbc78e415283f4348c82bf8367', '2023-08-08 01:17:48.287827', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-9317-2ab4-8b7949d64186', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-fad0-5f1c-a060b450786c', 2, '', '', '', 0, 2, '', '', 'AbpIdentity.Roles.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '4ede19a33ef343619eef75d5a926ea38', '2023-08-08 01:17:48.287339', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-a059-a229-d716b34a2e73', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-a557-44c2-bf3380d99174', 2, '', '', '', 0, 4, '', '', 'AbpIdentity.Users.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '2b1a0c70686341b5998c5336b339b117', '2023-08-08 01:17:48.287253', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-a557-44c2-bf3380d99174', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179c-18ec-22e6-c218356bd07f', 1, 'systemUser', 'system/user/index', '', 0, 2, '/system/user', '', '', '{\"Title\":\"message.router.systemUser\",\"Icon\":\"iconfont icon-icon-\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'e45a61ac5c3f4770b964ce47d3196773', '2023-08-08 01:17:48.286762', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-b178-51aa-15191ce173bf', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-fad0-5f1c-a060b450786c', 2, '', '', '', 0, 1, '', '', 'AbpIdentity.Roles', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'f6d47ee77ac34c6c9706be114b8bd298', '2023-08-08 01:17:48.287747', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-bf09-1479-109122e0bc50', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-fad0-5f1c-a060b450786c', 2, '', '', '', 0, 0, '', '', 'AbpIdentity.Roles.ManagePermissions', '{\"Title\":\"message.router.systemManagePermissions\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '0e71eacfb1e64c86999b11418b807e63', '2023-08-08 01:17:48.287665', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-cd3e-bd0d-41e0601637c8', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-18f4-0039-e9302eff037f', 2, '', '', '', 0, 4, '', '', 'BaseService.OrganizationUnits.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'a21eb75167254af8955b76bbdad6ef37', '2023-08-08 01:17:48.288180', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-cfc2-d9fb-f33cfa3c0235', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-a557-44c2-bf3380d99174', 2, '', '', '', 0, 3, '', '', 'AbpIdentity.Users.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '9623035a8c1f47b3b19804f18e724183', '2023-08-08 01:17:48.287174', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-e13a-123e-22ed02d0822c', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-18f4-0039-e9302eff037f', 2, '', '', '', 0, 1, '', '', 'BaseService.OrganizationUnits', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '10b613e7b4ed4b72a92c5fb5c61c58da', '2023-08-08 01:17:48.287910', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-ec85-cc55-4e5139a34574', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-a557-44c2-bf3380d99174', 2, '', '', '', 0, 2, '', '', 'AbpIdentity.Users.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'c8fca08a9bcd44309b1f3126525a7dc3', '2023-08-08 01:17:48.287591', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179b-fad0-5f1c-a060b450786c', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179c-18ec-22e6-c218356bd07f', 1, 'systemRole', 'system/role/index', '', 0, 3, '/system/role', '', '', '{\"Title\":\"message.router.systemRole\",\"Icon\":\"iconfont icon-quanxian\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '64180371ddc241749301a9e8190041ed', '2023-08-08 01:17:48.287004', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179c-18ec-22e6-c218356bd07f', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', NULL, 1, 'system', 'layout/routerView/parent', '', 0, 2, '/system', '/system/user', '', '{\"Title\":\"message.router.system\",\"Icon\":\"iconfont icon-xitongshezhi\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '531bfa4e6631459aaecb1362c910dea4', '2023-08-08 01:17:48.288570', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179c-45a3-8b72-de567688afaa', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-aabe-0790-e6ce5df72ef3', 1, 'formCreate', 'workflow/form/create', '', 0, 2, '/workflow/form/create', '', 'FormManagement.Form.Create', '{\"Title\":\"message.router.workflowFormAdd\",\"Icon\":\"ele-Plus\",\"IsHide\":true,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '651ffca650134aa08cd491f45f7b6ee5', '2023-08-08 01:17:48.288431', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179c-5084-a936-5240e217e0ac', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-aabe-0790-e6ce5df72ef3', 2, '', '', '', 0, 4, '', '', 'FormManagement.Form.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'b33ad6c7c0714ac8a3e17a85011ce8fc', '2023-08-08 01:17:48.288527', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce32e-179c-b220-0b58-10dc9a540c0a', '3a0ce30c-2fa1-eb65-6c0e-29bf1daadab4', '3a0ce32e-179b-aabe-0790-e6ce5df72ef3', 2, '', '', '', 0, 3, '', '', 'FormManagement.Form.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'cc992aae0cf7467d826453dd628ecb50', '2023-08-08 01:17:48.288479', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce5fb-faa2-62c7-e10e-b2468d748b35', NULL, '3a0d1646-12c1-0235-aff7-7e0e4b3d16b3', 1, 'systemAuditLogging', 'logManage/auditLogging/index', '', 0, 1, '/logManage/systemAuditLogging', '', '', '{\"Title\":\"message.router.systemAuditLogging\",\"Icon\":\"iconfont icon-templatedefault\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '32bd87774138427f862f8afb8f0d991f', '2023-08-08 14:21:54.809399', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-11-17 23:08:32.660527', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ce5fc-e2f3-aa6e-8b57-4016a1505e90', NULL, '3a0ce5fb-faa2-62c7-e10e-b2468d748b35', 2, '', '', '', 0, 1, '', '', 'BaseService.AuditLogging', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '5c77544538e64a41990547cbf3a1a41b', '2023-08-08 14:22:54.210064', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-08-08 15:05:54.659752', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d1646-12c1-0235-aff7-7e0e4b3d16b3', NULL, NULL, 1, 'logManage', 'layout/routerView/parent', '', 0, 30, '/logManage', '/logManage/systemAuditLogging', '', '{\"Title\":\"message.router.sysLogManagement\",\"Icon\":\"iconfont icon--_xitongrizhi\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '0b37b35b434646a9b904f4ced3d6748b', '2023-08-17 23:24:37.011095', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-11-17 23:06:47.829352', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d164f-8d50-9772-1136-9adb118ef5f5', NULL, '3a0d1646-12c1-0235-aff7-7e0e4b3d16b3', 1, 'systemSecurityLog', 'logManage/securityLog/index', '', 0, 2, '/logManage/systemSecurityLog', '', '', '{\"Title\":\"message.router.systemSecurityLog\",\"Icon\":\"iconfont icon-jiansheyanshou\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '10b9aa28084640fc91ec4a018a6bb9e9', '2023-08-17 23:34:58.133138', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-11-17 23:08:26.740310', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d1651-5048-73ff-aa78-0120e9d8717d', NULL, '3a0d164f-8d50-9772-1136-9adb118ef5f5', 2, '', '', '', 0, 1, '', '', 'BaseService.SecurityLog', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '4a441299db2c4ef59817d41c48fdc87d', '2023-08-17 23:36:53.593367', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d4760-34fd-af48-e765-085a03ef028f', NULL, NULL, 1, 'member', 'layout/routerView/parent', '', 0, 2, '/member', '/member/userGrade', '', '{\"Title\":\"message.router.member\",\"Icon\":\"iconfont icon-Member\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '695bfd1387aa4df399e9a85b87782ba9', '2023-08-27 12:14:33.273667', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-17 22:54:42.191121', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d4766-4067-51c7-ef8d-15a6f401b8c8', NULL, '3a0d4760-34fd-af48-e765-085a03ef028f', 1, 'memberUserGrade', 'member/userGrade/index', '', 0, 1, '/member/userGrade', '', '', '{\"Title\":\"message.router.memberUserGrade\",\"Icon\":\"iconfont icon-huiyuan\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '8fff7661a5bd4e0ba85c4f3f4b183fa0', '2023-08-27 12:21:09.355130', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-17 23:10:06.239057', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d4768-d354-7d6f-dd9c-b8e55848dab2', NULL, '3a0d4766-4067-51c7-ef8d-15a6f401b8c8', 2, '', '', '', 0, 1, '', '', 'BaseService.UserGrades', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'a9c8c88dbdae4e3cabfb6b34547ae05e', '2023-08-27 12:23:58.040356', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d476a-a86c-f576-a2c9-abf08bf4aca9', NULL, '3a0d4766-4067-51c7-ef8d-15a6f401b8c8', 2, '', '', '', 0, 2, '', '', 'BaseService.UserGrades.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'fd30151dba07405e9c201e2f9952e2fd', '2023-08-27 12:25:58.129819', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d476b-ebd0-142f-f26f-9dc94b1f06b5', NULL, '3a0d4766-4067-51c7-ef8d-15a6f401b8c8', 2, '', '', '', 0, 3, '', '', 'BaseService.UserGrades.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '9348609899a642f6bbe63a903612a19e', '2023-08-27 12:27:20.914591', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d476c-9dc2-a291-7452-870d458e3893', NULL, '3a0d4766-4067-51c7-ef8d-15a6f401b8c8', 2, '', '', '', 0, 4, '', '', 'BaseService.UserGrades.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'bba23705a8c04e8fbd3c0156da5a3fe6', '2023-08-27 12:28:06.470553', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d4e96-f752-74fb-27c1-ea3fe6565f22', NULL, '3a0d4760-34fd-af48-e765-085a03ef028f', 1, 'memberUserWeChat', 'member/userWeChat/index', '', 0, 2, '/member/userWeChat', '', '', '{\"Title\":\"message.router.memberUserWeChat\",\"Icon\":\"iconfont icon-weixin\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'e05f97e0dc314ddb859c7d90565bc2fd', '2023-08-28 21:51:42.481547', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-17 22:55:31.092540', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d4e98-97cf-176e-95d8-5e21472df991', NULL, '3a0d4e96-f752-74fb-27c1-ea3fe6565f22', 2, '', '', '', 0, 1, '', '', 'BaseService.UserWeChats', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '12469eb756fb4d348f0beeb79bc7c720', '2023-08-28 21:53:29.043479', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d53d8-acdd-ab98-65bf-ed4a21ae5ced', NULL, '3a0d4760-34fd-af48-e765-085a03ef028f', 1, 'memberSysUser', 'member/sysUser/index', '', 0, 3, '/member/sysUser', '', '', '{\"Title\":\"message.router.memberSysUser\",\"Icon\":\"iconfont icon-24gl-userGroupPlus\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '219fdfe39a5647c0a4cb7b263cdb1c0e', '2023-08-29 22:21:34.870296', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-17 22:55:46.183222', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d53e0-4569-ba10-2a19-7bc878d96b84', NULL, '3a0d53d8-acdd-ab98-65bf-ed4a21ae5ced', 2, '', '', '', 0, 1, '', '', 'BaseService.SysUsers', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '0577f6974119426baf5196ebd10027d6', '2023-08-29 22:29:53.278587', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d5922-ec5b-7232-d7a2-d153f5e53af1', NULL, '3a0d53d8-acdd-ab98-65bf-ed4a21ae5ced', 2, '', '', '', 0, 2, '', '', 'BaseService.SysUsers.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'fa17bb12ce984f6e8dcf5aa11c808b28', '2023-08-30 23:00:46.863047', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d5923-7d6b-51f7-1aae-6f50feac8f2d', NULL, '3a0d53d8-acdd-ab98-65bf-ed4a21ae5ced', 2, '', '', '', 0, 3, '', '', 'BaseService.SysUsers.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '69a84a6c3e0946d486371edfa56d2233', '2023-08-30 23:01:24.002004', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d5924-2776-8e74-b9fe-a894b63eb7fc', NULL, '3a0d53d8-acdd-ab98-65bf-ed4a21ae5ced', 2, '', '', '', 0, 4, '', '', 'BaseService.SysUsers.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'bb728922436148fb9b3223cd0af2dc4c', '2023-08-30 23:02:07.494002', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d8c35-182b-82c2-6d95-6e2445c31cbf', NULL, NULL, 1, 'good', 'layout/routerView/parent	', '', 0, 3, '/good', '/good/goods', '', '{\"Title\":\"message.router.good\",\"Icon\":\"iconfont icon-shangpin\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'ae2a467d2dcd4caa82662a2b643e05e9', '2023-09-09 21:01:15.803579', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-17 22:56:20.229253', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d8c3b-1fc5-6cfa-6359-c951ca20f5fd', NULL, '3a0d8c35-182b-82c2-6d95-6e2445c31cbf', 1, 'brand', 'good/brand/index', '', 0, 3, '/good/brand', '', '', '{\"Title\":\"message.router.brand\",\"Icon\":\"iconfont icon-pinpai\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '7535bbec09e44955b1dc738997de2f76', '2023-09-09 21:07:50.857568', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-17 22:57:16.017786', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d8c3c-53bc-3a7f-0656-1df0f4ead69e', NULL, '3a0d8c3b-1fc5-6cfa-6359-c951ca20f5fd', 2, '', '', '', 0, 1, '', '', 'GoodService.GoodBrands', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '116a7c07a1e5458e869f620f76a78e3f', '2023-09-09 21:09:09.694749', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d8c3c-f215-53a1-4752-f157bbcf794e', NULL, '3a0d8c3b-1fc5-6cfa-6359-c951ca20f5fd', 2, '', '', '', 0, 2, '', '', 'GoodService.GoodBrands.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '49307c054eb34c4697a945b3497ffd0c', '2023-09-09 21:09:50.231927', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d8c3d-69ea-3d6b-3e41-517efa421c82', NULL, '3a0d8c3b-1fc5-6cfa-6359-c951ca20f5fd', 2, '', '', '', 0, 3, '', '', 'GoodService.GoodBrands.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'f113c44a3c8b4458b524a0d3d1438098', '2023-09-09 21:10:20.908588', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d8c3e-0133-f8db-5954-469130d06bb0', NULL, '3a0d8c3b-1fc5-6cfa-6359-c951ca20f5fd', 2, '', '', '', 0, 4, '', '', 'GoodService.GoodBrands.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'b01ce441fbcb493280aaa8263d4b6f71', '2023-09-09 21:10:59.646621', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d8fb9-94cd-844b-2c49-9ec1b4113523', NULL, '3a0d8c35-182b-82c2-6d95-6e2445c31cbf', 1, 'goodCategory', 'good/category/index', '', 0, 4, '/good/category', '', '', '{\"Title\":\"message.router.goodCategory\",\"Icon\":\"iconfont icon-shangpinfenlei1\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'e0fda51cefeb421098b044e7cf7eb035', '2023-09-10 13:24:50.021138', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-17 23:11:56.438192', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d8fbb-e2a5-bbc8-f42c-11da7c443cfd', NULL, '3a0d8fb9-94cd-844b-2c49-9ec1b4113523', 2, '', '', '', 0, 1, '', '', 'GoodService.GoodCategorys', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'fa28b907f62241298cf0a5fb2e330305', '2023-09-10 13:27:21.001197', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d8fbc-d7ef-44d1-afe0-23d61e0ae3e9', NULL, '3a0d8fb9-94cd-844b-2c49-9ec1b4113523', 2, '', '', '', 0, 2, '', '', 'GoodService.GoodCategorys.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '7736479980fb4eef99282d14924a8377', '2023-09-10 13:28:23.794264', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d8fbd-a0d7-1379-41fd-bc1e86dfb1ee', NULL, '3a0d8fb9-94cd-844b-2c49-9ec1b4113523', 2, '', '', '', 0, 3, '', '', 'GoodService.GoodCategorys.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '168824dbf1724d1c86e0e960e2d32292', '2023-09-10 13:29:15.227277', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d8fbe-5954-1dda-d7d1-98461a6059d7', NULL, '3a0d8fb9-94cd-844b-2c49-9ec1b4113523', 2, '', '', '', 0, 4, '', '', 'GoodService.GoodCategorys.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '9bf4bc08c71d41c3b6827a6100b4bbe0', '2023-09-10 13:30:02.455397', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d90c5-b577-0557-f634-d794895ff921', NULL, '3a0d8c35-182b-82c2-6d95-6e2445c31cbf', 1, 'goodParam', 'good/param/index', '', 0, 5, '/good/param', '', '', '{\"Title\":\"message.router.goodParam\",\"Icon\":\"iconfont icon-canshuchaxun\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '9377937628834583ac139f8dfe4fd870', '2023-09-10 18:17:42.064704', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-02-10 02:04:22.446725', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d90c6-9db1-6568-c23c-8a5a1a6941e5', NULL, '3a0d90c5-b577-0557-f634-d794895ff921', 2, '', '', '', 0, 1, '', '', 'GoodService.GoodParams', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '9fbdc643e7894d4bbf5f0facc5609d3c', '2023-09-10 18:18:41.461408', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d90c7-2620-a03b-4238-bcec973c155d', NULL, '3a0d90c5-b577-0557-f634-d794895ff921', 2, '', '', '', 0, 2, '', '', 'GoodService.GoodParams.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '82b9c67867ca414ba0cf582efc3d44e3', '2023-09-10 18:19:16.386900', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d90c7-be28-e464-6432-6834338ff00d', NULL, '3a0d90c5-b577-0557-f634-d794895ff921', 2, '', '', '', 0, 3, '', '', 'GoodService.GoodParams.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'b1b702ae3cac4acd997873482e6cea59', '2023-09-10 18:19:55.307448', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d90c8-50b4-e83c-4f30-e4c3bac8f8fb', NULL, '3a0d90c5-b577-0557-f634-d794895ff921', 2, '', '', '', 0, 4, '', '', 'GoodService.GoodParams.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '2aa340a684364ef3bfe84b7df727c7a4', '2023-09-10 18:20:32.823394', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d9671-5393-66a1-75fe-134a29bc0822', NULL, '3a0d8c35-182b-82c2-6d95-6e2445c31cbf', 1, 'goodTypeSpec', 'good/typeSpec/index', '', 0, 6, '/good/typeSpec', '', '', '{\"Title\":\"message.router.goodTypeSpec\",\"Icon\":\"ele-ScaleToOriginal\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '736f2552e1784dd697e44daf186a9475', '2023-09-11 20:43:15.299147', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-09-14 21:18:43.811023', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d9672-78b1-0dbb-a764-35991f0f826c', NULL, '3a0d9671-5393-66a1-75fe-134a29bc0822', 2, '', '', '', 0, 1, '', '', 'GoodService.GoodTypeSpecs', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'de0a096d6ae041ca9778c44068112b8a', '2023-09-11 20:44:30.263078', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d9672-ec03-c1a0-52df-dae9b0e14e7d', NULL, '3a0d9671-5393-66a1-75fe-134a29bc0822', 2, '', '', '', 0, 2, '', '', 'GoodService.GoodTypeSpecs.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '76159c8922bd478b8838372c70507c97', '2023-09-11 20:44:59.782164', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d9673-7ef3-0b3c-89a4-c1d6ec97fdbb', NULL, '3a0d9671-5393-66a1-75fe-134a29bc0822', 2, '', '', '', 0, 3, '', '', 'GoodService.GoodTypeSpecs.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '68c6634c65b0460d8b42b3f4c166e689', '2023-09-11 20:45:37.398809', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0d9673-f3a2-5d97-c51f-b1f9c94cba29', NULL, '3a0d9671-5393-66a1-75fe-134a29bc0822', 2, '', '', '', 0, 4, '', '', 'GoodService.GoodTypeSpecs.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '7fbb6aab450c40acbabb24b0fdf4b50f', '2023-09-11 20:46:07.269176', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0da603-bcb0-f640-afe8-15891470f8f4', NULL, '3a0d8c35-182b-82c2-6d95-6e2445c31cbf', 1, 'goods', 'good/goods/index', '', 0, 0, '/good/goods', '', '', '{\"Title\":\"message.router.goods\",\"Icon\":\"iconfont icon-shangpinguanli\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '14bfcefe8ea24a5dbaee68f2f8151250', '2023-09-14 21:17:28.640648', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-17 22:56:45.445397', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0da606-074e-7080-44bf-de36c022c19e', NULL, '3a0da603-bcb0-f640-afe8-15891470f8f4', 2, '', '', '', 0, 1, '', '', 'GoodService.Goods', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '95e0cb1f3b1a4f19b22878e16ee58075', '2023-09-14 21:19:58.804941', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0da606-94da-b786-38af-b98806eb8772', NULL, '3a0da603-bcb0-f640-afe8-15891470f8f4', 1, 'goodsCreate', 'good/goods/create', '', 0, 2, '/good/goods/create', '', 'GoodService.Goods.Create', '{\"Title\":\"message.router.goodsCreate\",\"Icon\":\"\",\"IsHide\":true,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '64857161d034401c90bdcdfe9e388b63', '2023-09-14 21:20:35.037030', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-09-15 19:52:12.886338', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0da606-ecef-e187-da76-120d68a63de1', NULL, '3a0da603-bcb0-f640-afe8-15891470f8f4', 1, 'goodsUpdate', 'good/goods/update', '', 0, 3, '/good/goods/update', '', 'GoodService.Goods.Update', '{\"Title\":\"message.router.goodsUpdate\",\"Icon\":\"\",\"IsHide\":true,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '0891c0043fbc46c2beab17cddcdc424c', '2023-09-14 21:20:57.585959', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-10-02 11:58:16.603329', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0da607-8b45-fe0c-4204-4a81ec977eb7', NULL, '3a0da603-bcb0-f640-afe8-15891470f8f4', 2, '', '', '', 0, 5, '', '', 'GoodService.Goods.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '6382ae5fdf8144d19a30dd9614d0acb0', '2023-09-14 21:21:38.119703', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-10-04 13:25:34.873756', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e0b51-6ce2-7d80-8665-cb46f2b61587', NULL, '3a0da603-bcb0-f640-afe8-15891470f8f4', 1, 'goodsLook', 'good/goods/look', '', 0, 4, '/good/goods/look', '', 'GoodService.Goods.Look', '{\"Title\":\"message.router.goodsLook\",\"Icon\":\"\",\"IsHide\":true,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'eadc28bb80354f2c9595d2c510f7bdfa', '2023-10-04 13:23:58.853339', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-10-04 13:30:57.036231', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e87be-ea1f-b087-8dbc-f40fc5cefd47', NULL, '3a0d8c35-182b-82c2-6d95-6e2445c31cbf', 1, 'serviceGood', '/good/serviceGood/index', '', 0, 1, '/good/serviceGood', '', '', '{\"Title\":\"message.router.serviceGood\",\"Icon\":\"iconfont icon-fuwu\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'c165e890584c426fbc35b90d40ba353a', '2023-10-28 17:16:29.182616', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-17 22:56:58.510972', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e87c2-29e7-79ab-35ad-d609266e5052', NULL, '3a0e87be-ea1f-b087-8dbc-f40fc5cefd47', 2, '', '', '', 0, 1, '', '', 'GoodService.ServiceGoods', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '3f6a34ca83e74080bbe425fee750cc42', '2023-10-28 17:20:02.031283', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-10-28 17:37:55.326446', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e87c4-8c0f-1f69-9545-6baef849b6db', NULL, '3a0e87be-ea1f-b087-8dbc-f40fc5cefd47', 1, 'serviceGoodCreate', 'good/serviceGood/create', '', 0, 2, '/good/serviceGood/create', '', 'GoodService.ServiceGoods.Create', '{\"Title\":\"message.router.serviceGoodCreate\",\"Icon\":\"\",\"IsHide\":true,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '43e564f6ca2b4944ab2c98ee9af377b8', '2023-10-28 17:22:38.227479', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-10-28 17:29:02.899815', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e87cb-a475-0c59-a4af-17123e28b873', NULL, '3a0e87be-ea1f-b087-8dbc-f40fc5cefd47', 1, 'serviceGoodUpdate', 'good/serviceGood/update', '', 0, 3, '/good/serviceGood/update', '', 'GoodService.ServiceGoods.Update', '{\"Title\":\"message.router.serviceGoodUpdate\",\"Icon\":\"\",\"IsHide\":true,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'c49aba31307d45be87ab1b6a1b9a4c88', '2023-10-28 17:30:23.225437', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-10-28 17:30:43.436526', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e87cd-1ac3-960a-f46c-f82c00b4efe3', NULL, '3a0e87be-ea1f-b087-8dbc-f40fc5cefd47', 1, 'serviceGoodLook', 'good/serviceGood/look', '', 0, 4, '/good/serviceGood/look', '', 'GoodService.ServiceGoods.Look', '{\"Title\":\"message.router.serviceGoodLook\",\"Icon\":\"\",\"IsHide\":true,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'ad7159ebb07b412290eec3d703dc3d5d', '2023-10-28 17:31:59.045410', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-10-28 17:36:34.572794', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e87cf-5869-87e5-eab9-10bf91ed3737', NULL, '3a0e87be-ea1f-b087-8dbc-f40fc5cefd47', 2, '', '', '', 0, 5, '', '', 'GoodService.ServiceGoods.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '0f9c6cbed13f4b0298a8ad41cfc92b77', '2023-10-28 17:34:25.899852', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e981a-27a6-7cd1-e84f-f2d6ece390fd', NULL, NULL, 1, 'shopSetting', 'layout/routerView/parent', '', 0, 8, '/shopSetting', '', '', '{\"Title\":\"message.router.shopSetting\",\"Icon\":\"iconfont icon-shangchengshezhi\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'b607ac8339e74b7c895ef836ba7eb71f', '2023-10-31 21:30:04.084943', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-13 13:43:58.343073', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e981f-e467-9c4b-7a9f-79e58762f53d', NULL, '3a0e981a-27a6-7cd1-e84f-f2d6ece390fd', 1, 'area', 'shopSetting/area/index', '', 0, 1, '/shopSetting/area', '', '', '{\"Title\":\"message.router.area\",\"Icon\":\"iconfont icon-quyupeizhi\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '57d3a188589841479e3f999d4f2a5f9c', '2023-10-31 21:36:20.077092', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-17 23:11:14.086929', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e9823-05ef-09cb-ea3f-5b25ae405ce4', NULL, '3a0e981f-e467-9c4b-7a9f-79e58762f53d', 2, '', '', '', 0, 1, '', '', 'GoodService.Areas', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '7d0e9ee5bd6243e4ae32a5d58224f411', '2023-10-31 21:39:45.265859', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e9823-ae4e-d6e7-2d0d-622a43aea35b', NULL, '3a0e981f-e467-9c4b-7a9f-79e58762f53d', 2, '', '', '', 0, 2, '', '', 'GoodService.Areas.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'b0efab44b7354e94bf6f8b09a1d6c09a', '2023-10-31 21:40:28.369575', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e9824-203e-c604-5a6c-235cd1923570', NULL, '3a0e981f-e467-9c4b-7a9f-79e58762f53d', 2, '', '', '', 0, 3, '', '', 'GoodService.Areas.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '5bdf6f35a81549908c4187cca5435c79', '2023-10-31 21:40:57.536236', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e9824-a17a-ac09-8538-b8b34359ded0', NULL, '3a0e981f-e467-9c4b-7a9f-79e58762f53d', 2, '', '', '', 0, 4, '', '', 'GoodService.Areas.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '35f1458f6eaf4e48b71601a7e4064aaf', '2023-10-31 21:41:30.621600', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e9d65-528a-ed87-34b8-ec56d87c4772', NULL, '3a0e981a-27a6-7cd1-e84f-f2d6ece390fd', 1, 'store', 'shopSetting/store/index', '', 0, 2, '/shopSetting/store', '', '', '{\"Title\":\"message.router.store\",\"Icon\":\"iconfont icon-8\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'ceda9f24df684628b02dfab33a248003', '2023-11-01 22:10:16.366138', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-17 23:01:04.540922', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e9d68-8a0f-d12e-7713-de606afa5c50', NULL, '3a0e9d65-528a-ed87-34b8-ec56d87c4772', 2, '', '', '', 0, 1, '', '', 'GoodService.Stores', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '63d29395a4d547d98a8439f0d08e21e1', '2023-11-01 22:13:47.156280', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e9d69-4e6d-4bb7-a6e8-ac9299b5c5f7', NULL, '3a0e9d65-528a-ed87-34b8-ec56d87c4772', 2, '', '', '', 0, 2, '', '', 'GoodService.Stores.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'e08b8f93fdb54de6b47b7ea40633fe10', '2023-11-01 22:14:37.424019', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e9d69-d689-9721-508b-0ae9f99ab32f', NULL, '3a0e9d65-528a-ed87-34b8-ec56d87c4772', 2, '', '', '', 0, 3, '', '', 'GoodService.Stores.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '370a71dce95d4c91a1c8f7f3cf93aae3', '2023-11-01 22:15:12.267977', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0e9d6a-9019-4530-fd88-dac80407f425', NULL, '3a0e9d65-528a-ed87-34b8-ec56d87c4772', 2, '', '', '', 0, 4, '', '', 'GoodService.Stores.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '32df47482aaf477b938cb5ae854eff2f', '2023-11-01 22:15:59.772445', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0eaad1-e19b-36c7-7dc4-25a80a75932f', NULL, '3a0e981a-27a6-7cd1-e84f-f2d6ece390fd', 1, 'storeClerk', 'shopSetting/storeClerk/index', '', 0, 3, '/shopSetting/storeClerk', '', '', '{\"Title\":\"message.router.storeClerk\",\"Icon\":\"fa fa-puzzle-piece\",\"IsHide\":true,\"IsKeepAlive\":false,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '644ee2fcf1754f9798096e257cdf980a', '2023-11-04 12:43:54.732834', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-04 12:45:09.241790', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0eaad4-6d67-9656-222b-9d7706b5e873', NULL, '3a0eaad1-e19b-36c7-7dc4-25a80a75932f', 2, '', '', '', 0, 1, '', '', 'GoodService.StoreClerks', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '12be861f09dc411da6cc734b7c51ea02', '2023-11-04 12:46:41.517413', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0eaad5-11cd-2f1b-199f-542827540f74', NULL, '3a0eaad1-e19b-36c7-7dc4-25a80a75932f', 2, '', '', '', 0, 2, '', '', 'GoodService.StoreClerks.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'ab684f5cbebb4dc58dae096e269232a0', '2023-11-04 12:47:23.601830', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0eaad5-d1c9-c645-8dc3-02a98d465553', NULL, '3a0eaad1-e19b-36c7-7dc4-25a80a75932f', 2, '', '', '', 0, 3, '', '', 'GoodService.StoreClerks.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '81e295fd6dbf43c3b11aa0d2eb00e7d3', '2023-11-04 12:48:12.749778', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0eaad6-4471-d698-3fa3-f4f28bd36594', NULL, '3a0eaad1-e19b-36c7-7dc4-25a80a75932f', 2, '', '', '', 0, 4, '', '', 'GoodService.StoreClerks.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'cd0e195f37ac4c06a7dbfe6150f28470', '2023-11-04 12:48:42.100432', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ed142-abf0-d056-1913-568e01539b73', NULL, '3a0e981a-27a6-7cd1-e84f-f2d6ece390fd', 1, 'pageDesign', 'shopSetting/pageDesign/index', '', 0, 4, '/shopSetting/pageDesign', '', '', '{\"Title\":\"message.router.pageDesign\",\"Icon\":\"iconfont icon-webyemiansheji\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '034a4a4a3d414de1af96af7eb1ecbd79', '2023-11-11 23:52:40.701251', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-02-15 09:24:44.824031', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ed144-b9cf-d65e-d1a9-0c1074b512ef', NULL, '3a0ed142-abf0-d056-1913-568e01539b73', 2, '', '', '', 0, 1, '', '', 'GoodService.PageDesigns', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '43ca9decb7d64d66a966e08116362a89', '2023-11-11 23:54:55.319962', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ed145-3af7-a2c4-ddc5-13e62997e98c', NULL, '3a0ed142-abf0-d056-1913-568e01539b73', 2, '', '', '', 0, 2, '', '', 'GoodService.PageDesigns.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '59b1d55e22b141e2a4c0eaf679fead7e', '2023-11-11 23:55:28.379422', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ed145-947e-e097-4a99-30f11962ccbe', NULL, '3a0ed142-abf0-d056-1913-568e01539b73', 2, '', '', '', 0, 3, '', '', 'GoodService.PageDesigns.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '990cf368e0fc468b9a23afbcf4b04499', '2023-11-11 23:55:51.299809', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ed146-0086-114c-57b7-6630a268c624', NULL, '3a0ed142-abf0-d056-1913-568e01539b73', 2, '', '', '', 0, 4, '', '', 'GoodService.PageDesigns.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '8f2df2ad707c4fe98e337f67ea9c83e8', '2023-11-11 23:56:18.957114', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ed14b-0f96-5d03-b52d-6459f4889441', NULL, '3a0ed142-abf0-d056-1913-568e01539b73', 2, '', '', '', 0, 5, '', '', 'GoodService.PageDesigns.Preview', '{\"Title\":\"message.router.preview\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'a832aa39fdbc466babf8857ed6b0eb51', '2023-11-12 00:01:50.491902', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ed14b-ed79-9420-b1b4-b0f243afd7a9', NULL, '3a0ed142-abf0-d056-1913-568e01539b73', 1, 'layout', 'shopSetting/pageDesign/layout', '', 0, 6, '/shopSetting/pageDesign/layout', '', 'GoodService.PageDesigns.Layout', '{\"Title\":\"message.router.layout\",\"Icon\":\"ele-Cellphone\",\"IsHide\":true,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'a99d526f782c4862b0133c692329a989', '2023-11-12 00:02:47.298897', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-12 21:49:53.253685', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ed17f-cc4f-806e-7c18-ba37b7290e92', NULL, '3a0ed142-abf0-d056-1913-568e01539b73', 2, '', '', '', 0, 7, '', '', 'GoodService.PageDesigns.Copy', '{\"Title\":\"message.router.copy\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '44d3a6f918da4d959bf329f880a91ec8', '2023-11-12 00:59:26.683807', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-12 00:59:53.139587', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ee00f-a0da-e6ca-f973-cd8da7e43bd4', NULL, NULL, 1, 'operationManage', 'layout/routerView/parent', '', 0, 5, '/operationManage', '', '', '{\"Title\":\"message.router.operationManage\",\"Icon\":\"iconfont icon-yunyingguanli1\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '97e17376375e4aa4810dc6895688c1ec', '2023-11-14 20:51:13.801546', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-17 23:04:05.894133', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ee013-79ff-9a60-a04c-30ae5553edf6', NULL, '3a0ee00f-a0da-e6ca-f973-cd8da7e43bd4', 1, 'articleManage', 'layout/routerView/parent', '', 0, 4, '/operationManage/articleManage', '', '', '{\"Title\":\"message.router.articleManage\",\"Icon\":\"iconfont icon-xitongrizhi\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'ea5550af9fd44ff4a2a1222f9ef1cc6c', '2023-11-14 20:55:25.954125', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-17 23:13:31.101541', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ee018-2722-889f-0a9d-aedece8c0659', NULL, '3a0ee013-79ff-9a60-a04c-30ae5553edf6', 1, 'articles', 'operationManage/articles/index', '', 0, 1, '/operationManage/articleManage/index', '', '', '{\"Title\":\"message.router.articles\",\"Icon\":\"ele-Document\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'abd4a5f6cd0e48539563c3e16a5de389', '2023-11-14 21:00:32.422152', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-14 21:18:15.426086', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ee01d-9114-5b37-26bc-09557e3b7666', NULL, '3a0ee018-2722-889f-0a9d-aedece8c0659', 2, '', '', '', 0, 1, '', '', 'GoodService.Articles', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '32cb13ff155c4b04b4e83bea31f02291', '2023-11-14 21:06:27.222942', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ee01e-544f-f4db-e705-80e7c66dd153', NULL, '3a0ee018-2722-889f-0a9d-aedece8c0659', 2, '', '', '', 0, 2, '', '', 'GoodService.Articles.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '9ccff4c3e4c94835a5de6239b1955c6c', '2023-11-14 21:07:17.202596', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ee01e-c2a5-440d-99d8-4b12af8461bd', NULL, '3a0ee018-2722-889f-0a9d-aedece8c0659', 2, '', '', '', 0, 3, '', '', 'GoodService.Articles.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '69257546d9324308a2af0f7f7d8dec9b', '2023-11-14 21:07:45.447754', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ee01f-3d7b-da0f-504d-5331f41d470c', NULL, '3a0ee018-2722-889f-0a9d-aedece8c0659', 2, '', '', '', 0, 4, '', '', 'GoodService.Articles.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '8ec3e048b44f4a1f91caf88af8fe9503', '2023-11-14 21:08:16.895151', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ee020-cffe-d096-0ea1-0cdc99673d66', NULL, '3a0ee013-79ff-9a60-a04c-30ae5553edf6', 1, 'articleType', 'operationManage/articles/type', '', 0, 1, '/operationManage/articleManage/type', '', '', '{\"Title\":\"message.router.articleType\",\"Icon\":\"iconfont icon-wenzhangfenlei\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '6464aa98a7d549078571f074f2e7263e', '2023-11-14 21:09:59.947796', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-17 23:06:09.313585', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ee023-20a2-64d3-1e0b-9cb990c7b05e', NULL, '3a0ee020-cffe-d096-0ea1-0cdc99673d66', 2, '', '', '', 0, 1, '', '', 'GoodService.ArticleTypes', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '7154fa05269148969494e398190bddb6', '2023-11-14 21:12:31.652595', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ee023-8ff5-724f-bd52-fa22d7570aed', NULL, '3a0ee020-cffe-d096-0ea1-0cdc99673d66', 2, '', '', '', 0, 2, '', '', 'GoodService.ArticleTypes.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '69e23031f7d14b41b811304be01fc9ab', '2023-11-14 21:13:00.150920', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ee024-0840-eff6-5478-ac18ad8fd267', NULL, '3a0ee020-cffe-d096-0ea1-0cdc99673d66', 2, '', '', '', 0, 3, '', '', 'GoodService.ArticleTypes.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '6fc818e3ce6c4be9b467eaa155b2cc7f', '2023-11-14 21:13:30.946860', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0ee024-8886-ced2-96bd-708aabf6f2c4', NULL, '3a0ee020-cffe-d096-0ea1-0cdc99673d66', 2, '', '', '', 0, 4, '', '', 'GoodService.ArticleTypes.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '3bf52d38f14045a78e274d045f656022', '2023-11-14 21:14:03.784069', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f1cb5-3508-5797-ebcd-0a8fa390bd85', NULL, NULL, 1, 'promotion', 'layout/routerView/parent', '', 0, 6, '/promotion', '', '', '{\"Title\":\"message.router.promotion\",\"Icon\":\"iconfont icon-cuxiaozhongxin\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'ef94c329bd6f4c468efadb2c1edb5f11', '2023-11-26 15:29:18.161793', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-26 16:27:42.838106', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f1cb6-bbdc-cec1-ea20-1c45ac21d981', NULL, '3a0f1cb5-3508-5797-ebcd-0a8fa390bd85', 1, 'globalPromotion', 'promotion/global/index', '', 0, 1, '/promotion/global', '', '', '{\"Title\":\"message.router.globalPromotion\",\"Icon\":\"iconfont icon-dianpuyingxiao\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '87efdf44e48a4a67b9b2de577c4571d7', '2023-11-26 15:30:58.144781', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-11-26 16:28:04.070353', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f1cba-0bbf-f96e-8212-c7207916cc0c', NULL, '3a0f1cb6-bbdc-cec1-ea20-1c45ac21d981', 2, '', '', '', 0, 1, '', '', 'PromotionService.Promotions', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '43080dcb18d3486da66658514b63a01d', '2023-11-26 15:34:35.204797', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f1cba-692c-1f3d-78ef-80e8e68a025d', NULL, '3a0f1cb6-bbdc-cec1-ea20-1c45ac21d981', 2, '', '', '', 0, 2, '', '', 'PromotionService.Promotions.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '2ebc88fcbd354ccdbf2544a2d3e7c18c', '2023-11-26 15:34:59.119631', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f1cbb-7b8d-68e9-fe9a-6376ed3034ec', NULL, '3a0f1cb6-bbdc-cec1-ea20-1c45ac21d981', 2, '', '', '', 0, 3, '', '', 'PromotionService.Promotions.Update', '{\"Title\":\"message.router.setParameters\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'fcdbc9c16f904abd8f81bc0cc6abf942', '2023-11-26 15:36:09.362500', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-12-03 23:28:42.244963', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f1cbb-ead9-58b3-868e-25dbd062fd6b', NULL, '3a0f1cb6-bbdc-cec1-ea20-1c45ac21d981', 2, '', '', '', 0, 4, '', '', 'PromotionService.Promotions.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '7c8a5f05255b4d4a8d6076e48ec2004a', '2023-11-26 15:36:37.852396', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f4793-db0d-8261-3c1d-ec85433b6d89', NULL, '3a0f1cb5-3508-5797-ebcd-0a8fa390bd85', 1, 'coupon', 'promotion/coupon/index', '', 0, 2, '/promotion/coupon', '', '', '{\"Title\":\"message.router.coupon\",\"Icon\":\"iconfont icon-youhuijuan\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '2930ea0f8ba249cc806e6fe9f7f96ab0', '2023-12-04 23:16:32.810835', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-12-04 23:27:41.061929', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f4795-300c-e757-7f85-bdbeff7d215a', NULL, '3a0f4793-db0d-8261-3c1d-ec85433b6d89', 2, '', '', '', 0, 1, '', '', 'PromotionService.Coupons', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '3ba15edd1cd7496ebc1d9164d120e70a', '2023-12-04 23:17:59.954653', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f4795-c504-e860-a434-f30ba1d18caf', NULL, '3a0f4793-db0d-8261-3c1d-ec85433b6d89', 2, '', '', '', 0, 2, '', '', 'PromotionService.Coupons.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '73dbea33c88f4af8a545ffe15bcd56ff', '2023-12-04 23:18:38.088547', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f4796-3d71-9ea6-6350-e8facbd9e01a', NULL, '3a0f4793-db0d-8261-3c1d-ec85433b6d89', 2, '', '', '', 0, 3, '', '', 'PromotionService.Coupons.Update', '{\"Title\":\"message.router.setParameters\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '88bfc41d06354478bf55fab8f38fda5d', '2023-12-04 23:19:08.917920', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-12-04 23:53:45.167089', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f4796-e923-bdba-a3db-d6b83458e3bd', NULL, '3a0f4793-db0d-8261-3c1d-ec85433b6d89', 2, '', '', '', 0, 4, '', '', 'PromotionService.Coupons.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '164df9c780e54f84aaae0893faaef220', '2023-12-04 23:19:52.870377', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f4c84-2db5-00d6-b7c2-9f00c141cd15', NULL, '3a0f4793-db0d-8261-3c1d-ec85433b6d89', 2, '', '', '', 0, 5, '', '', 'PromotionService.Coupons.Couponlist', '{\"Title\":\"message.router.couponlist\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'dda28efbec814d4a806c8cc9d34dab81', '2023-12-05 22:17:31.448481', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f516b-c07a-fe9b-1799-5ec61470bb77', NULL, '3a0f1cb5-3508-5797-ebcd-0a8fa390bd85', 1, 'groupSeckill', 'promotion/groupSeckill/index', '', 0, 3, '/promotion/groupSeckill', '', '', '{\"Title\":\"message.router.groupSeckill\",\"Icon\":\"iconfont icon-yingxiaogongju-tuangoumiaosha1\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '67d3d5bdbf864cfbbb610c43f154ec95', '2023-12-06 21:08:56.617040', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f516d-3630-c1ae-d175-58d1dfe50d6a', NULL, '3a0f516b-c07a-fe9b-1799-5ec61470bb77', 2, '', '', '', 0, 1, '', '', 'PromotionService.GroupSeckills', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'e060bfa5ca0e407f9938a7ebcc4551b1', '2023-12-06 21:10:32.244363', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f516d-b981-34f5-d316-84ab2a5d78a7', NULL, '3a0f516b-c07a-fe9b-1799-5ec61470bb77', 2, '', '', '', 0, 2, '', '', 'PromotionService.GroupSeckills.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'b6266d770ada43c9ac63dcb648afe003', '2023-12-06 21:11:05.861889', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f516e-64e4-8fd4-6eae-deeb08661a6c', NULL, '3a0f516b-c07a-fe9b-1799-5ec61470bb77', 2, '', '', '', 0, 3, '', '', 'PromotionService.GroupSeckills.Update', '{\"Title\":\"message.router.setParameters\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '11389dc83d004b5389f30e1767b44760', '2023-12-06 21:11:49.736964', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-12-06 23:41:42.456917', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f516e-ec22-c319-70a9-69bafc303098', NULL, '3a0f516b-c07a-fe9b-1799-5ec61470bb77', 2, '', '', '', 0, 4, '', '', 'PromotionService.GroupSeckills.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'c8906531a8dd46a4bdd65d0a84c5ccac', '2023-12-06 21:12:24.356988', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f5ffd-f9a7-9ce6-a2f2-8156cab0fda1', NULL, '3a0f1cb5-3508-5797-ebcd-0a8fa390bd85', 1, 'pinTuanManage', 'layout/routerView/parent', '', 0, 4, '/promotion/pinTuan', '', '', '{\"Title\":\"message.router.pinTuanManage\",\"Icon\":\"iconfont icon-tuangou\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'c8b7b4410b2a4c2fac228fb31d3e403e', '2023-12-09 17:03:20.632678', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-12-10 10:59:01.004556', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f6001-13dd-e1f0-07f3-0871c2bfd4eb', NULL, '3a0f5ffd-f9a7-9ce6-a2f2-8156cab0fda1', 1, 'pinTuanList', '/promotion/pinTuanManage/pinTuanList/index', '', 0, 1, '/promotion/pinTuan/rule', '', '', '{\"Title\":\"message.router.pinTuanList\",\"Icon\":\"iconfont icon-pintuan\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'b443a34445474ed5b574edcf78798151', '2023-12-09 17:06:43.812460', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-12-10 10:59:09.060009', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f6002-7ad7-feab-3ef1-f91d889cb016', NULL, '3a0f6001-13dd-e1f0-07f3-0871c2bfd4eb', 2, '', '', '', 0, 1, '', '', 'PromotionService.PinTuanRules', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'deeeaea67ba34941b26f40e1ba745f4e', '2023-12-09 17:08:15.708749', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f6003-025b-2096-9bc2-580abc29703c', NULL, '3a0f6001-13dd-e1f0-07f3-0871c2bfd4eb', 2, '', '', '', 0, 2, '', '', 'PromotionService.PinTuanRules.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '59ec28dcf9784ae19907e63f7104d786', '2023-12-09 17:08:50.400146', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f6003-b2f1-8231-3fb4-26a1e4c29eba', NULL, '3a0f6001-13dd-e1f0-07f3-0871c2bfd4eb', 2, '', '', '', 0, 3, '', '', 'PromotionService.PinTuanRules.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'dc40e48c17634251918a46867f602eaa', '2023-12-09 17:09:35.605040', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f6004-30d2-a905-2ca3-c982e9be6dba', NULL, '3a0f6001-13dd-e1f0-07f3-0871c2bfd4eb', 2, '', '', '', 0, 4, '', '', 'PromotionService.PinTuanRules.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '6d50e66ec4ff4ac18e4a6fef8b34bf40', '2023-12-09 17:10:07.829038', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f6984-1a03-fb39-3701-760ee5fb75cb', NULL, '3a0ee00f-a0da-e6ca-f973-cd8da7e43bd4', 1, 'noticeList', 'operationManage/notices/index', '', 0, 1, '/operationManage/notices', '', '', '{\"Title\":\"message.router.noticeList\",\"Icon\":\"iconfont icon-gonggao\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '6a996db625584cbfab8c5a77260f08ba', '2023-12-11 13:26:25.600423', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f6986-12d0-78cd-a471-0dbf8af7dcae', NULL, '3a0f6984-1a03-fb39-3701-760ee5fb75cb', 2, '', '', '', 0, 1, '', '', 'GoodService.Notices', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '5f8dac1a56134240af0788f62d2a8db5', '2023-12-11 13:28:34.773886', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f6986-8548-1797-33ab-ef09de34ab93', NULL, '3a0f6984-1a03-fb39-3701-760ee5fb75cb', 2, '', '', '', 0, 2, '', '', 'GoodService.Notices.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'fd0e59820f644caeacc0a75bce213a1b', '2023-12-11 13:29:04.077405', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f6987-1c1f-3ce8-e70c-5b2ce0c68311', NULL, '3a0f6984-1a03-fb39-3701-760ee5fb75cb', 2, '', '', '', 0, 3, '', '', 'GoodService.Notices.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '369179910a684e7cac8d0bf574af208a', '2023-12-11 13:29:42.691754', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f6987-a01c-e841-f801-ca590e3f7c45', NULL, '3a0f6984-1a03-fb39-3701-760ee5fb75cb', 2, '', '', '', 0, 4, '', '', 'GoodService.Notices.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '906ecce26f4140d489fbd551ef99a238', '2023-12-11 13:30:16.479923', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f70b5-677c-5060-42ae-616d8395ce0f', NULL, '3a0ee00f-a0da-e6ca-f973-cd8da7e43bd4', 1, 'customForm', 'layout/routerView/parent', '', 0, 2, '/operationManage/customForms', '', '', '{\"Title\":\"message.router.customForm\",\"Icon\":\"iconfont icon-browser1\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '0f95a67ebd404b6b90b894acaa757c14', '2023-12-12 22:57:37.236998', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-12-12 23:06:50.576278', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f70b7-9442-eeaa-d428-78e9845fdfe4', NULL, '3a0f70b5-677c-5060-42ae-616d8395ce0f', 1, 'formList', 'operationManage/customForms/index', '', 0, 1, '/operationManage/customForms/index', '', '', '{\"Title\":\"message.router.formList\",\"Icon\":\"iconfont icon-fuwuhao\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'b3582e905e764fe7bf40afc5e8bde20a', '2023-12-12 22:59:59.689825', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-12-13 20:35:59.318630', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f70b9-f021-3072-2559-af45724480b7', NULL, '3a0f70b7-9442-eeaa-d428-78e9845fdfe4', 2, '', '', '', 0, 1, '', '', 'GoodService.Forms', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '2c12e99600014aacbeb6dfb305844be4', '2023-12-12 23:02:34.280519', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f70ba-64ba-32da-9695-6db3aa08d603', NULL, '3a0f70b7-9442-eeaa-d428-78e9845fdfe4', 2, '', '', '', 0, 2, '', '', 'GoodService.Forms.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '547ddee90903496fbb96229277fe49bb', '2023-12-12 23:03:04.126228', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f70bb-3914-3442-5e3a-5b862ed46363', NULL, '3a0f70b7-9442-eeaa-d428-78e9845fdfe4', 2, '', '', '', 0, 3, '', '', 'GoodService.Forms.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '870ef97d59aa458caf9ff2fca76f4945', '2023-12-12 23:03:58.488227', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a0f70bb-ba1f-e349-302d-dc17bdfc26bc', NULL, '3a0f70b7-9442-eeaa-d428-78e9845fdfe4', 2, '', '', '', 0, 4, '', '', 'GoodService.Forms.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '5a3040dbb8304e5096f5de1a882fa394', '2023-12-12 23:04:31.522931', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a10ba85-006b-e7ac-a3df-e36662e4480a', NULL, '3a0e981a-27a6-7cd1-e84f-f2d6ece390fd', 1, 'serviceDescription', 'shopSetting/serviceDescription/index', '', 0, 5, '/shopSetting/serviceDescription', '', '', '{\"Title\":\"message.router.serviceDescription\",\"Icon\":\"iconfont icon-icon\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'd41cfbd7181640c5a504378e458611a2', '2024-02-14 15:59:26.340010', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-02-14 16:13:34.789083', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a10ba87-2e9e-44a1-aaf5-d77e3df69ec6', NULL, '3a10ba85-006b-e7ac-a3df-e36662e4480a', 2, '', '', '', 0, 1, '', '', 'GoodService.ServiceDescriptions', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'b3a97ab1afdb4d318dfaf465592ad9e3', '2024-02-14 16:01:49.215808', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a10ba87-a7b9-0214-5eeb-b480d33bca3d', NULL, '3a10ba85-006b-e7ac-a3df-e36662e4480a', 2, '', '', '', 0, 3, '', '', 'GoodService.ServiceDescriptions.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'a653631cb4124620ab3d51ba7445279b', '2024-02-14 16:02:20.219613', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-02-14 16:02:29.332772', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a10ba88-2df7-b613-93e1-20dc8dbc0d5c', NULL, '3a10ba85-006b-e7ac-a3df-e36662e4480a', 2, '', '', '', 0, 2, '', '', 'GoodService.ServiceDescriptions.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'f90e7cd2c8cd4af48ec42ace6fc3bc19', '2024-02-14 16:02:54.585966', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-02-14 16:03:00.899020', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a10ba88-9c63-1418-4310-ac33c8c037e3', NULL, '3a10ba85-006b-e7ac-a3df-e36662e4480a', 2, '', '', '', 0, 4, '', '', 'GoodService.ServiceDescriptions.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'd6a5c1d0028a4cac821ecd7c6645632f', '2024-02-14 16:03:22.852901', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-02-14 16:03:30.036279', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a10be41-a84c-4381-b3b1-231f9a8540fa', NULL, '3a0e981a-27a6-7cd1-e84f-f2d6ece390fd', 1, 'platformSetting', 'shopSetting/platform/index', '', 0, 0, '/shopSetting/platform', '', '', '{\"Title\":\"message.router.platformSetting\",\"Icon\":\"iconfont icon-pingtaishezhi\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '68d58be9cdde4632b8e9c1c0b193c3f4', '2024-02-15 09:24:21.720214', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a10be44-91c6-ed89-9349-5314d2a55c49', NULL, '3a10be41-a84c-4381-b3b1-231f9a8540fa', 2, '', '', '', 0, 1, '', '', 'BaseService.PlatformSettings', '{\"Title\":\"message.router.platformManage\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '40995a6b1bb449debcee96befce1685e', '2024-02-15 09:27:32.553303', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a1208c9-c25d-cf23-3b17-106f82f2d5c9', NULL, '3a0e981a-27a6-7cd1-e84f-f2d6ece390fd', 1, 'logistics', 'shopSetting/logistics/index', '', 0, 6, '/shopSetting/logistics', '', '', '{\"Title\":\"message.router.logistics\",\"Icon\":\"iconfont icon-wuliu\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'a3ba1b931863408491395bad8bb2eeed', '2024-04-19 13:47:42.573955', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-04-19 13:51:14.422412', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a1208cf-b42f-c72d-fd1a-22334877150d', NULL, '3a1208c9-c25d-cf23-3b17-106f82f2d5c9', 2, '', '', '', 0, 1, '', '', 'OrderService.Logisticss', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '15533623f9d94dc49e130fd2456cae33', '2024-04-19 13:54:12.145256', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a1208d0-55c4-2cba-617b-2f2b71ffa8c1', NULL, '3a1208c9-c25d-cf23-3b17-106f82f2d5c9', 2, '', '', '', 0, 2, '', '', 'OrderService.Logisticss.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '9787cbc09552404396a5a7a9bacdd40e', '2024-04-19 13:54:53.509770', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a1208d0-d514-3baa-763b-6bc37dca1fc7', NULL, '3a1208c9-c25d-cf23-3b17-106f82f2d5c9', 2, '', '', '', 0, 3, '', '', 'OrderService.Logisticss.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'a5553ecb6eda4eea9bdab4b59f11da99', '2024-04-19 13:55:26.101969', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a1208d1-74a8-8240-ecab-7824b99675bd', NULL, '3a1208c9-c25d-cf23-3b17-106f82f2d5c9', 2, '', '', '', 0, 4, '', '', 'OrderService.Logisticss.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'ed264ce7396246dda815206f76718b28', '2024-04-19 13:56:06.954210', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a1211b6-0ed7-c0d5-86d6-a5d731d4e762', NULL, '3a0e981a-27a6-7cd1-e84f-f2d6ece390fd', 1, 'ship', 'shopSetting/ships/index', '', 0, 7, '/shopSetting/ships', '', '', '{\"Title\":\"message.router.ship\",\"Icon\":\"iconfont icon-peisongfangshi\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '0bbdab9f3aee45a980a85de36318ff2d', '2024-04-21 07:22:46.375707', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-04-21 07:28:33.337241', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a1211b7-72ad-7741-af61-f413b63bb1c4', NULL, '3a1211b6-0ed7-c0d5-86d6-a5d731d4e762', 2, '', '', '', 0, 1, '', '', 'OrderService.Ships', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '580c848595fc4b3c80eb9bfda1296684', '2024-04-21 07:24:17.455546', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a1211b8-037d-0c2b-1cb8-f145a3e902c9', NULL, '3a1211b6-0ed7-c0d5-86d6-a5d731d4e762', 2, '', '', '', 0, 2, '', '', 'OrderService.Ships.Create', '{\"Title\":\"message.router.systemCreate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '8b89d342d6234b19a85e7926a2d77db8', '2024-04-21 07:24:54.526468', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a1211b8-7d56-e651-92b8-2f9c9d89c594', NULL, '3a1211b6-0ed7-c0d5-86d6-a5d731d4e762', 2, '', '', '', 0, 3, '', '', 'OrderService.Ships.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'c263ca784df84969a6d2c611574676be', '2024-04-21 07:25:25.720820', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-04-21 07:25:40.474750', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a1211b9-44ea-b8a4-7658-4f7d1baa2535', NULL, '3a1211b6-0ed7-c0d5-86d6-a5d731d4e762', 2, '', '', '', 0, 4, '', '', 'OrderService.Ships.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'a10ed12eb626470da7cb6690a7aebb4d', '2024-04-21 07:26:16.811873', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a135635-6f21-a37b-35d4-9123c6579a61', NULL, NULL, 1, 'financialManage', 'layout/routerView/parent', '', 0, 7, '/financialManage', '/financialManage/payment', '', '{\"Title\":\"message.router.financialManage\",\"Icon\":\"iconfont icon-caiwuguanli\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'c1b9e1608d8649728f6c37c21479b5d9', '2024-06-23 07:38:52.069463', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-06-23 11:20:05.161983', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a135638-63a6-e9fd-d48d-04ea692b15c6', NULL, '3a135635-6f21-a37b-35d4-9123c6579a61', 1, 'payment', 'financialManage/payment/index', '', 0, 1, '/financialManage/payment', '', '', '{\"Title\":\"message.router.payment\",\"Icon\":\"iconfont icon-zhifufangshi\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'f59948a446fa4d91b638f1f8256db4f3', '2024-06-23 07:42:05.736973', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-06-23 11:20:14.579555', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a13563c-663c-e6f3-9616-9152ef5345e5', NULL, '3a135638-63a6-e9fd-d48d-04ea692b15c6', 2, '', '', '', 0, 1, '', '', 'PaymentService.Payments', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '84275c4f33aa4dbe88e1670761d83fc2', '2024-06-23 07:46:28.544023', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a13568b-9464-8297-404e-df2e3567526f', NULL, '3a135635-6f21-a37b-35d4-9123c6579a61', 1, 'billPayment', 'financialManage/bill/index', '', 0, 2, '/financialManage/billPayment', '', '', '{\"Title\":\"message.router.billPayment\",\"Icon\":\"iconfont icon-zhifudanju\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'fbf53797408845c3a0ea3d715dd637bc', '2024-06-23 09:12:57.704713', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-06-23 11:20:24.286848', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a13568c-bc49-64f0-5d00-7b7f49413f68', NULL, '3a13568b-9464-8297-404e-df2e3567526f', 2, '', '', '', 0, 1, '', '', 'PaymentService.BillPayments', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'ef54993b6d1d4f428338f58e9e5c41ae', '2024-06-23 09:14:13.450988', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a135ca3-6fbe-2e44-c73c-c23bd824e54a', NULL, NULL, 1, 'orderManage', 'layout/routerView/parent', '', 0, 4, '/orderManage', '', '', '{\"Title\":\"message.router.orderManage\",\"Icon\":\"iconfont icon-dingdanguanli\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '2cc8ca6935ae4f6ca0a8cad7b62768c3', '2024-06-24 13:36:44.488047', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-13 13:43:49.691934', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a135ca4-9803-5bdb-a3bb-4dc4339d315c', NULL, '3a135ca3-6fbe-2e44-c73c-c23bd824e54a', 1, 'order', 'orderManage/order/index', '', 0, 1, '/orderManage/order', '', '', '{\"Title\":\"message.router.order\",\"Icon\":\"iconfont icon-shangpindingdan\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '82bcc8ceace74ab2b19f4bfa99c3a3b4', '2024-06-24 13:38:00.325435', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-06-24 14:21:29.819083', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a135caa-9cd9-8e4f-b5f9-68cb4dcb9b66', NULL, '3a135ca4-9803-5bdb-a3bb-4dc4339d315c', 2, '', '', '', 0, 1, '', '', 'OrderService.Orders', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'a64301a30dc6464ca55b903e621b9592', '2024-06-24 13:44:34.778835', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-06-24 13:46:36.949460', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a170b18-e522-69b4-fef1-31c51680451a', NULL, '3a135ca4-9803-5bdb-a3bb-4dc4339d315c', 2, '', '', '', 0, 2, '', '', 'OrderService.Orders.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'fa171416f6de49c5a7b2d49aeafbc6e6', '2024-12-24 21:47:19.726956', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a170b19-ad98-21c8-12ec-42cc386f6982', NULL, '3a135ca4-9803-5bdb-a3bb-4dc4339d315c', 2, '', '', '', 0, 3, '', '', 'OrderService.Orders.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '3c99e5fbea3a46a6875468a6aa99ac7b', '2024-12-24 21:48:11.035301', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a170b1d-4c5a-7e6e-8bb2-9aafd33db9ca', NULL, '3a135ca4-9803-5bdb-a3bb-4dc4339d315c', 2, '', '', '', 0, 4, '', '', 'OrderService.Orders.Cancel', '{\"Title\":\"message.router.cancel\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '682bef389e3e4dc18972415827f0863d', '2024-12-24 21:52:08.283641', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a170b1e-7257-e508-86bf-009a53bbefa1', NULL, '3a135ca4-9803-5bdb-a3bb-4dc4339d315c', 2, '', '', '', 0, 5, '', '', 'OrderService.Orders.Pay', '{\"Title\":\"message.router.pay\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'ba560480b356451284f3ffc56d2026ab', '2024-12-24 21:53:23.545253', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a170b1f-a430-4d77-ef38-25bd37ec65d0', NULL, '3a135ca4-9803-5bdb-a3bb-4dc4339d315c', 2, '', '', '', 0, 6, '', '', 'OrderService.Orders.Delivery', '{\"Title\":\"message.router.delivery\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '587098ae4b144b7680eda8b0724f5298', '2024-12-24 21:54:41.842552', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a170b1f-f7f0-0cab-f02a-7d62781ca1d1', NULL, '3a135ca4-9803-5bdb-a3bb-4dc4339d315c', 2, '', '', '', 0, 7, '', '', 'OrderService.Orders.Print', '{\"Title\":\"message.router.print\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '54f441d7f41141c69f1de891c2b78dfa', '2024-12-24 21:55:03.282687', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a170b20-4496-a5c2-7d36-40f035b983c7', NULL, '3a135ca4-9803-5bdb-a3bb-4dc4339d315c', 2, '', '', '', 0, 8, '', '', 'OrderService.Orders.Complete', '{\"Title\":\"message.router.complete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '2a8cafa8e30241b9b29fb7a21c873363', '2024-12-24 21:55:22.904358', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a171529-a3c4-3597-3e0d-4cd0823c75dc', NULL, '3a135ca3-6fbe-2e44-c73c-c23bd824e54a', 1, 'billDelivery', 'orderManage/billDelivery/index', '', 0, 2, '/orderManage/billDelivery', '', '', '{\"Title\":\"message.router.billDelivery\",\"Icon\":\"ele-Van\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'bc3cf06edc5b481685a5228ba7771b83', '2024-12-26 20:41:49.261558', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-12-26 20:42:30.500237', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a17152b-79d7-82d0-e6a3-b85dcf73b3cc', NULL, '3a171529-a3c4-3597-3e0d-4cd0823c75dc', 2, '', '', '', 0, 1, '', '', 'OrderService.BillDeliverys', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'c68395f0331c4d329eb2a2e478eff1d4', '2024-12-26 20:43:49.593165', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a17152c-af2f-8be4-9277-f0c27714f5a9', NULL, '3a171529-a3c4-3597-3e0d-4cd0823c75dc', 2, '', '', '', 0, 2, '', '', 'OrderService.BillDeliverys.Update', '{\"Title\":\"message.router.systemUpdate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '7f36434330fc48249fed467cdd2e3b78', '2024-12-26 20:45:08.785100', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a180ba8-ef19-894d-9c9c-d8a7218d1313', NULL, '3a135ca3-6fbe-2e44-c73c-c23bd824e54a', 1, 'billAftersales', 'orderManage/billAftersales/index', '', 0, 3, '/orderManage/billAftersales', '', '', '{\"Title\":\"message.router.billAftersales\",\"Icon\":\"iconfont icon-fuwu2\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '5ed589d906d542d89ecd68d5f8aee330', '2025-02-12 17:27:26.756909', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-02-13 14:49:28.426429', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a180baa-f71d-a993-da67-cf40641bc99a', NULL, '3a180ba8-ef19-894d-9c9c-d8a7218d1313', 2, '', '', '', 0, 1, '', '', 'OrderService.BillAftersales', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '61c9d918b3a446d0a28b678989dbc213', '2025-02-12 17:29:39.870992', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a180bae-beab-1fe4-c2dd-170435202ccd', NULL, '3a180ba8-ef19-894d-9c9c-d8a7218d1313', 2, '', '', '', 0, 2, '', '', 'OrderService.BillAftersales.Operate', '{\"Title\":\"message.router.operate\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '9e7f5131b81e41ab9962786d9ce16d25', '2025-02-12 17:33:47.564787', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a182521-728f-9acd-047d-ecc25b2f693c', NULL, '3a135ca3-6fbe-2e44-c73c-c23bd824e54a', 1, 'billReship', 'orderManage/billReship/index', '', 0, 4, '/orderManage/billReship', '', '', '{\"Title\":\"message.router.billReship\",\"Icon\":\"ele-SoldOut\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '60a8abf2479243ce9bb71bf44fe56b1e', '2025-02-17 16:09:35.130070', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-02-17 16:10:49.539913', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a182525-e9fb-8ac7-f551-2809b76b760b', NULL, '3a182521-728f-9acd-047d-ecc25b2f693c', 2, '', '', '', 0, 1, '', '', 'OrderService.BillReships', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '3af8849dd5d4476aa9f342d76712faf4', '2025-02-17 16:14:27.839536', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a182526-6a66-1226-0572-ec6c687e7498', NULL, '3a182521-728f-9acd-047d-ecc25b2f693c', 2, '', '', '', 0, 1, '', '', 'OrderService.BillReships.ConfirmDelivery', '{\"Title\":\"message.router.confirmDelivery\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'dcd168bbb39542ef9e28e1d2c0e49410', '2025-02-17 16:15:00.712726', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-02-17 16:15:08.305728', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a182a36-023f-d783-c03f-7564dcb1bd71', NULL, '3a135635-6f21-a37b-35d4-9123c6579a61', 1, 'billRefund', 'financialManage/billRefund/index', '', 0, 3, '/financialManage/billRefund', '', '', '{\"Title\":\"message.router.billRefund\",\"Icon\":\"ele-Memo\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'ff39fc725ec84d7cae7086ae16b11fd2', '2025-02-18 15:50:08.707560', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-02-18 15:50:50.755224', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a182a38-12a9-4280-7641-24418e7387a0', NULL, '3a182a36-023f-d783-c03f-7564dcb1bd71', 2, '', '', '', 0, 1, '', '', 'PaymentService.BillRefunds', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '7f5860882971463fade9f6c40ade5b01', '2025-02-18 15:52:23.978870', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a182a38-aa7d-1d3d-7e2f-4ef0fb9cb012', NULL, '3a182a36-023f-d783-c03f-7564dcb1bd71', 2, '', '', '', 0, 2, '', '', 'PaymentService.BillRefunds.AuditRefund', '{\"Title\":\"message.router.auditRefund\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '064b310815b0495f9713293c7997ed97', '2025-02-18 15:53:02.847788', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a18a5e4-91e3-d178-a101-f8aa3e4c1696', NULL, '3a0d8c35-182b-82c2-6d95-6e2445c31cbf', 1, 'goodComment', 'good/comment/index', '', 0, 7, '/good/comment', '', '', '{\"Title\":\"message.router.goodComment\",\"Icon\":\"ele-Pointer\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'ce193c0a4dcd42719be36cbac52944a8', '2025-03-14 16:14:06.334341', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-03-14 16:41:39.376054', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a18a5e7-d953-cd23-0b36-f66e9e3ca7e0', NULL, '3a18a5e4-91e3-d178-a101-f8aa3e4c1696', 2, '', '', '', 0, 2, '', '', 'GoodService.GoodComments.SellerReply', '{\"Title\":\"message.router.sellerReply\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '541e8bf04a2d4cfdb3ade084d0e74bd5', '2025-03-14 16:17:41.205666', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a18a5e8-b034-8d2a-de8f-67d45f686734', NULL, '3a18a5e4-91e3-d178-a101-f8aa3e4c1696', 2, '', '', '', 0, 1, '', '', 'GoodService.GoodComments', '{\"Title\":\"message.router.systemSelect\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '123a8c29f2f64073b82cc96b954f977a', '2025-03-14 16:18:36.216432', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a18a5e9-581a-0393-e7fe-bda0fd476cd6', NULL, '3a18a5e4-91e3-d178-a101-f8aa3e4c1696', 2, '', '', '', 0, 3, '', '', 'GoodService.GoodComments.Delete', '{\"Title\":\"message.router.systemDelete\",\"Icon\":\"\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '64c3e791daea423a8019f67a8c24d088', '2025-03-14 16:19:19.196567', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-03-14 16:19:31.891514', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_menus` VALUES ('3a1b7c1c-c2d3-80cf-51d1-fe60b0da9dba', NULL, NULL, 1, 'test', '/', '', 0, 0, '/', '/', '', '{\"Title\":\"message.router.xxx\",\"Icon\":\"fa fa-eercast\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', '5a46834bbbb34db5a31a8f9a34096387', '2025-08-02 16:39:47.680109', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 16:40:18.167607', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 16:40:18.167516');
INSERT INTO `sys_menus` VALUES ('3fa85f64-5717-4562-b3fc-2c963f66afa6', NULL, NULL, 1, 'system', 'layout/routerView/parent', '', 0, 1, '/system', '/system/user', '', '{\"Title\":\"message.router.system\",\"Icon\":\"ele-Setting\",\"IsHide\":false,\"IsKeepAlive\":true,\"IsAffix\":false,\"Link\":\"\",\"IsIframe\":false}', '{}', 'e63589dd5bca408090eecb653a4eb123', '2023-07-07 18:29:27.897652', '3a0c0381-2b83-5125-2df8-9f50c0e3d11a', '2023-11-17 22:52:18.339318', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);

-- ----------------------------
-- Table structure for sys_organizationunitroles
-- ----------------------------
DROP TABLE IF EXISTS `sys_organizationunitroles`;
CREATE TABLE `sys_organizationunitroles`  (
  `RoleId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `OrganizationUnitId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`OrganizationUnitId`, `RoleId`) USING BTREE,
  INDEX `IX_Sys_OrganizationUnitRoles_RoleId_OrganizationUnitId`(`RoleId`, `OrganizationUnitId`) USING BTREE,
  CONSTRAINT `FK_Sys_OrganizationUnitRoles_Sys_OrganizationUnits_Organization~` FOREIGN KEY (`OrganizationUnitId`) REFERENCES `sys_organizationunits` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_Sys_OrganizationUnitRoles_Sys_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `sys_roles` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_organizationunitroles
-- ----------------------------
INSERT INTO `sys_organizationunitroles` VALUES ('3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0d3a68-ed07-48dd-178d-0f4d07d34c62', NULL, '2023-08-24 23:54:09.339313', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534');

-- ----------------------------
-- Table structure for sys_organizationunits
-- ----------------------------
DROP TABLE IF EXISTS `sys_organizationunits`;
CREATE TABLE `sys_organizationunits`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `ParentId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Code` varchar(95) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DisplayName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EntityVersion` int NOT NULL,
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
  INDEX `IX_Sys_OrganizationUnits_Code`(`Code`) USING BTREE,
  INDEX `IX_Sys_OrganizationUnits_ParentId`(`ParentId`) USING BTREE,
  CONSTRAINT `FK_Sys_OrganizationUnits_Sys_OrganizationUnits_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `sys_organizationunits` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_organizationunits
-- ----------------------------
INSERT INTO `sys_organizationunits` VALUES ('3a0d3a4c-22cd-89a3-d56a-aa82513c1ad4', NULL, NULL, '00001', '华南总部', 1, '{}', '99dff6d30a8344bbb1ad1495373392c7', '2023-08-24 23:17:34.101265', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-08-24 23:48:21.505487', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_organizationunits` VALUES ('3a0d3a68-a4f1-76f7-6f55-08d80497bb42', NULL, NULL, '00002', '华北总部', 0, '{}', 'af9f4b69bcae48b487e3066a981854d7', '2023-08-24 23:48:42.359479', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_organizationunits` VALUES ('3a0d3a68-ed07-48dd-178d-0f4d07d34c62', NULL, '3a0d3a4c-22cd-89a3-d56a-aa82513c1ad4', '00001.00001', '南正集团', 0, '{}', '2cabb55cbe514b329fa9b6d1cd60e69d', '2023-08-24 23:49:00.808098', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_organizationunits` VALUES ('3a0e7e31-5b34-7ef9-79ba-057526388a93', NULL, NULL, '00003', 'Test', 1, '{}', '610071e0847e4ab1a532c8c323a40fb4', '2023-10-26 20:45:16.994953', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-10-26 20:46:18.336610', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-10-26 20:46:18.335564');
INSERT INTO `sys_organizationunits` VALUES ('3a0e7e31-7fa6-b0ca-cd9c-987774f2f6e2', NULL, '3a0e7e31-5b34-7ef9-79ba-057526388a93', '00003.00001', 'Test-0', 2, '{}', '8de764d1c25443849394a4d4d8d6dbb8', '2023-10-26 20:45:26.311359', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-10-26 20:46:15.450260', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2023-10-26 20:46:15.448177');
INSERT INTO `sys_organizationunits` VALUES ('3a1b7806-062b-f21d-789c-05749545ea01', NULL, NULL, '00003', 'A', 2, '{}', '7626ae8f5f7e43a7a0e88858b8500062', '2025-08-01 21:36:28.739614', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-01 21:36:45.837060', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-01 21:36:45.835621');
INSERT INTO `sys_organizationunits` VALUES ('3a1b7b60-de7f-1e61-8cb9-84a73cdd0246', NULL, NULL, '00003', 'Test1', 2, '{}', '70f727b9dc684769b39f5dc597cd69a1', '2025-08-02 13:14:34.003402', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 16:37:21.370977', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 16:37:21.365286');

-- ----------------------------
-- Table structure for sys_permissiongrants
-- ----------------------------
DROP TABLE IF EXISTS `sys_permissiongrants`;
CREATE TABLE `sys_permissiongrants`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderName` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_Sys_PermissionGrants_TenantId_Name_ProviderName_ProviderKey`(`TenantId`, `Name`, `ProviderName`, `ProviderKey`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_permissiongrants
-- ----------------------------
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5e9-a049-6d88-319764698f8b', NULL, 'AbpIdentity.Roles', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6b-f023-8093-2cfe-8b2dbb328d39', NULL, 'AbpIdentity.Roles', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f3-d422-ab42-e618b6b0a8ca', NULL, 'AbpIdentity.Roles.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6b-f028-0082-93a8-38175b3bbc31', NULL, 'AbpIdentity.Roles.Create', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f4-3d13-9cab-24cfb814b83b', NULL, 'AbpIdentity.Roles.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6b-f032-6e51-890e-f318d5515094', NULL, 'AbpIdentity.Roles.Delete', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f4-47b0-dff5-9196eeb9094f', NULL, 'AbpIdentity.Roles.ManagePermissions', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6b-f01c-b4ec-2c72-bc3d63404795', NULL, 'AbpIdentity.Roles.ManagePermissions', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f4-277a-3dc3-b90e5b292393', NULL, 'AbpIdentity.Roles.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6b-f02f-d26e-d07b-4df9985419ca', NULL, 'AbpIdentity.Roles.Update', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f4-47d6-693c-0fb896879a25', NULL, 'AbpIdentity.Users', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6b-f022-d135-bae3-d84dbc189de7', NULL, 'AbpIdentity.Users', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f4-bba7-98c3-9f40f46f5d28', NULL, 'AbpIdentity.Users.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6b-f02a-a3fa-3b8d-3df2982dd598', NULL, 'AbpIdentity.Users.Create', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f5-fa6f-e6eb-69b4c2e617c4', NULL, 'AbpIdentity.Users.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6b-f031-4a6c-b0d7-e5c69793deff', NULL, 'AbpIdentity.Users.Delete', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f5-149e-7980-2b7bc3797709', NULL, 'AbpIdentity.Users.ManagePermissions', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6b-f017-9c86-9d69-0c7563da50aa', NULL, 'AbpIdentity.Users.ManagePermissions', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f4-10f4-9dac-6bb7a968bff9', NULL, 'AbpIdentity.Users.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6b-f02e-3ae0-c13c-a1de170c62c4', NULL, 'AbpIdentity.Users.Update', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f5-5e9d-e018-93c252fd8bdc', NULL, 'AbpTenantManagement.Tenants', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f5-7365-6d4b-f08ff1158189', NULL, 'AbpTenantManagement.Tenants.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f5-aa08-eb93-161ce87c12be', NULL, 'AbpTenantManagement.Tenants.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f5-b651-d9b4-131ebcedbdb9', NULL, 'AbpTenantManagement.Tenants.ManageConnectionStrings', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f5-ebe5-8697-f6574b608ee9', NULL, 'AbpTenantManagement.Tenants.ManageFeatures', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f5-ebec-caa3-f5c54331d0a0', NULL, 'AbpTenantManagement.Tenants.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f7-07e6-b66b-c0ac8749c0de', NULL, 'BaseService.AuditLogging', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-173b-53d0-30e5-80e12851dc26', NULL, 'BaseService.AuditLogging', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f6-0310-9cc0-6f652e8e045c', NULL, 'BaseService.Homes', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6c-2bad-5e7e-0ab1-644ed2f0274e', NULL, 'BaseService.Homes', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f6-9106-91eb-3c9a91d94b27', NULL, 'BaseService.Menus', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6b-f01f-9b51-c30c-1f75679c09a7', NULL, 'BaseService.Menus', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f6-1487-8d35-2ae1e6f3c2e6', NULL, 'BaseService.Menus.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6b-f020-9d89-88f7-1cb82e12a341', NULL, 'BaseService.Menus.Create', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f6-3e24-0528-50f9eb30aad7', NULL, 'BaseService.Menus.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6b-f02d-f74c-7f0f-556fa1dc765d', NULL, 'BaseService.Menus.Delete', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f6-b544-bc2b-e7c4bfce7675', NULL, 'BaseService.Menus.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a6b-f026-d28b-0634-8e16e2b62460', NULL, 'BaseService.Menus.Update', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f6-a748-bcb9-924c5954af03', NULL, 'BaseService.OrganizationUnits', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f6-9b60-3011-30ea1821f07a', NULL, 'BaseService.OrganizationUnits.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f7-a345-2947-3b198c67b898', NULL, 'BaseService.OrganizationUnits.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f7-8b3f-8ab9-0b80e8f563ce', NULL, 'BaseService.OrganizationUnits.ManagingRole', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f7-b1e1-903a-7a93edcb20d3', NULL, 'BaseService.OrganizationUnits.ManagingUser', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f7-ffe7-4f8a-1147cfd2f0cc', NULL, 'BaseService.OrganizationUnits.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a10be44-ffc6-98fc-2617-bd009203d75b', NULL, 'BaseService.PlatformSettings', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-1780-93c3-1dc7-fb29156d48d7', NULL, 'BaseService.PlatformSettings', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d3a3a-4b7d-fa53-06f2-f4ca7e35ae94', NULL, 'BaseService.SecurityLog', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-173f-9a78-479a-ae4d7b3973ef', NULL, 'BaseService.SecurityLog', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d53e0-ff07-9b0b-52bb-877020cd8e78', NULL, 'BaseService.SysUsers', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d5924-be4b-125d-044d-993ced576a39', NULL, 'BaseService.SysUsers.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d5924-be5d-8da3-0bd3-ed7b4b5990f1', NULL, 'BaseService.SysUsers.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d5924-be57-adca-1a3f-3b253032b516', NULL, 'BaseService.SysUsers.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e1063-9952-1789-7b8c-5ce897d8ced0', NULL, 'BaseService.UserGrades', 'C', 'BackendGoodAggregationServiceClient');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d46f5-54e0-7c65-fe1f-8bdf091028c1', NULL, 'BaseService.UserGrades', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d46f5-54e0-838c-ba9b-49b154e32ab3', NULL, 'BaseService.UserGrades.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d46f5-54e0-ed0e-3d81-0d1e87da1ffb', NULL, 'BaseService.UserGrades.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d46f5-54e0-a69c-a1ff-f71fab894e24', NULL, 'BaseService.UserGrades.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d46f5-54c2-153b-b2ae-a7b1fbad58e5', NULL, 'BaseService.UserWeChats', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d46f5-54df-6097-27d6-8eaad704d399', NULL, 'BaseService.UserWeChats.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d46f5-54e0-a9aa-122a-cbbe5563def4', NULL, 'BaseService.UserWeChats.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d46f5-54dd-56d2-d0f4-90960b61b6b3', NULL, 'BaseService.UserWeChats.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f5-6aea-644d-516e57d3dc16', NULL, 'FeatureManagement.ManageHostFeatures', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e9825-1d4f-90d2-0978-e94239a38a0b', NULL, 'GoodService.Areas', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-1758-c247-f60a-69e89222d950', NULL, 'GoodService.Areas', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e9825-1d60-0a99-8529-4adeb5f04eed', NULL, 'GoodService.Areas.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17a6-be8f-d3b8-f0828ba7906b', NULL, 'GoodService.Areas.Create', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e9825-1d7d-d866-05a2-935cb27d80e9', NULL, 'GoodService.Areas.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17f2-f0ab-3655-9bf86964e980', NULL, 'GoodService.Areas.Delete', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e9825-1d6a-e83a-d670-b587e42e627b', NULL, 'GoodService.Areas.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17ca-ed89-4f4f-30ba3226fc60', NULL, 'GoodService.Areas.Update', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ee025-1802-ff6e-1623-080b38e53622', NULL, 'GoodService.Articles', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ee025-1826-3401-1775-3604807589fd', NULL, 'GoodService.Articles.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ee025-1842-b345-c4a5-b61c8aaf101e', NULL, 'GoodService.Articles.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ee025-1834-87ff-099d-26bc99925aec', NULL, 'GoodService.Articles.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ee025-1817-d01c-2e67-50efe2a6325f', NULL, 'GoodService.ArticleTypes', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ee025-1827-5a05-49ea-233a646d5c42', NULL, 'GoodService.ArticleTypes.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ee025-1844-bdc1-6fec-af4c412d3610', NULL, 'GoodService.ArticleTypes.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ee025-1835-bbc5-b0f0-8eed63ca8890', NULL, 'GoodService.ArticleTypes.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f70bc-56da-50c8-4c43-10b837b29ca2', NULL, 'GoodService.Forms', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f70bc-56f5-9854-f6aa-d44f42625796', NULL, 'GoodService.Forms.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f70bc-571e-10e8-41b7-c39a5e58830c', NULL, 'GoodService.Forms.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f70bc-570a-5cca-20e9-4f727cf3501c', NULL, 'GoodService.Forms.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d8c3e-8bcc-f1e5-b1b8-1a60bdffd874', NULL, 'GoodService.GoodBrands', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d8c3e-8be3-32da-25c4-c92e3e568ea5', NULL, 'GoodService.GoodBrands.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d8c3e-8bf0-a8d2-deae-73d436861428', NULL, 'GoodService.GoodBrands.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d8c3e-8bea-70b7-e72e-72dc8247e696', NULL, 'GoodService.GoodBrands.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d903e-2415-4974-ae99-1b1734a1ecfb', NULL, 'GoodService.GoodCategorys', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d903e-241f-d7ee-51b4-2c879482cd44', NULL, 'GoodService.GoodCategorys.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d903e-242c-60a7-26ed-f7956d9f31cc', NULL, 'GoodService.GoodCategorys.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d903e-2427-c5fb-9e5e-0f3efc74de38', NULL, 'GoodService.GoodCategorys.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a18a5fb-0916-fde2-e567-e7aad658256f', NULL, 'GoodService.GoodComments', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a18a5fb-0926-5119-c6cb-3167e5420727', NULL, 'GoodService.GoodComments.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a18a5fb-091f-50ea-a32a-4436d6f3e2e9', NULL, 'GoodService.GoodComments.SellerReply', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d90c8-c661-66c0-f21b-b8d8a983ae94', NULL, 'GoodService.GoodParams', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d90c8-c66a-67e3-1718-52d2c51412b3', NULL, 'GoodService.GoodParams.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d90c8-c67c-2e77-c102-733c054118a2', NULL, 'GoodService.GoodParams.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d90c8-c674-a0f4-6526-94e3acdc79f1', NULL, 'GoodService.GoodParams.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0da609-08fb-2770-6326-c5f8a19925fe', NULL, 'GoodService.Goods', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0da609-090d-29af-fabc-d9a7ccb42514', NULL, 'GoodService.Goods.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0da609-0921-6eee-5ccd-4f4221cce1ac', NULL, 'GoodService.Goods.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e0b5b-7d56-9d21-5e4d-c444aceec2be', NULL, 'GoodService.Goods.Look', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0da609-091a-e3d4-abec-6242e28ed70e', NULL, 'GoodService.Goods.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d9674-a4bc-6891-098e-1a5e184f70ac', NULL, 'GoodService.GoodTypeSpecs', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d9674-a4c9-047d-c9b7-317762eb1b48', NULL, 'GoodService.GoodTypeSpecs.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d9674-a4e6-6a27-402d-4386868464fd', NULL, 'GoodService.GoodTypeSpecs.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d9674-a4df-ec8f-676a-5f07f5d50ff9', NULL, 'GoodService.GoodTypeSpecs.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f6987-ecbd-04bb-dc6c-2609a2e2dd3a', NULL, 'GoodService.Notices', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f6987-ecce-d913-6fec-b6d1c37236ea', NULL, 'GoodService.Notices.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f6987-ece6-f985-fdec-f8d5b6aa1fb5', NULL, 'GoodService.Notices.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f6987-ecda-4d5e-a7f1-bef23b3c00d7', NULL, 'GoodService.Notices.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ed14d-c3ce-f2e4-1f06-0c9b77b0d7dc', NULL, 'GoodService.PageDesigns', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-1767-357e-decd-5918e13421d1', NULL, 'GoodService.PageDesigns', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ed180-def7-7caf-b9cf-cf74947c0692', NULL, 'GoodService.PageDesigns.Copy', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-1822-35bb-d77a-7f6fdadee2be', NULL, 'GoodService.PageDesigns.Copy', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ed14d-c3df-ce32-f0fa-5af5f9aead4f', NULL, 'GoodService.PageDesigns.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17ab-35c8-78b6-4e659f053e01', NULL, 'GoodService.PageDesigns.Create', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ed14d-c400-a66b-f3ad-751c3f8d58f9', NULL, 'GoodService.PageDesigns.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17f7-c87b-e389-4bd227dd98b7', NULL, 'GoodService.PageDesigns.Delete', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ed14d-c41e-cd27-2b10-a3bd0bc3c757', NULL, 'GoodService.PageDesigns.Layout', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-181d-8a30-79a8-de7214745ae6', NULL, 'GoodService.PageDesigns.Layout', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ed14d-c404-9bb9-d0c6-a6f30b2145c0', NULL, 'GoodService.PageDesigns.Preview', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-1815-696c-1aad-575e9a7e320c', NULL, 'GoodService.PageDesigns.Preview', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0ed14d-c3f0-fc04-7b0e-4a770e929116', NULL, 'GoodService.PageDesigns.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17d2-4d40-a1b7-28d2f1ad302f', NULL, 'GoodService.PageDesigns.Update', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a10ba8a-1b95-a340-a52a-f45f9a8f49e6', NULL, 'GoodService.ServiceDescriptions', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-1779-83b4-a426-d04fa7ce2611', NULL, 'GoodService.ServiceDescriptions', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a10ba8a-1b9f-f3b9-ab45-299577809437', NULL, 'GoodService.ServiceDescriptions.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17b4-feda-6b45-90135a011eac', NULL, 'GoodService.ServiceDescriptions.Create', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a10ba8a-1baf-23c0-bcbb-2d696168b4d2', NULL, 'GoodService.ServiceDescriptions.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-1808-b5f2-2eac-1dd59f854e58', NULL, 'GoodService.ServiceDescriptions.Delete', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a10ba8a-1ba7-6aaa-6440-ea59dba26898', NULL, 'GoodService.ServiceDescriptions.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17da-7b3e-b7f5-8884b5f24ef0', NULL, 'GoodService.ServiceDescriptions.Update', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e87d2-cb40-317c-7835-383b97aff951', NULL, 'GoodService.ServiceGoods', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e87d1-deaf-f71e-7da9-817f005f138f', NULL, 'GoodService.ServiceGoods.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e87d1-decc-ba22-d041-40a041ade1f2', NULL, 'GoodService.ServiceGoods.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e87d1-dec9-f685-5c54-eb5e281323a5', NULL, 'GoodService.ServiceGoods.Look', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e87d1-debf-b654-d88c-0ec43be71e32', NULL, 'GoodService.ServiceGoods.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0eaad6-9646-9bb3-1b76-aa739e7a4c2e', NULL, 'GoodService.StoreClerks', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-175f-acff-e3ba-7402833e8572', NULL, 'GoodService.StoreClerks', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0eaad6-9654-79b5-a7b0-49ea645b0f86', NULL, 'GoodService.StoreClerks.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17aa-7a12-4f0c-0618e6969495', NULL, 'GoodService.StoreClerks.Create', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0eaad6-9691-40b4-938f-2306b7f96618', NULL, 'GoodService.StoreClerks.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17f5-f2f0-e0b1-08c3fbf70437', NULL, 'GoodService.StoreClerks.Delete', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0eaad6-9662-9090-7320-2a1a13ee045d', NULL, 'GoodService.StoreClerks.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17cf-dc42-a06a-1aae465ae8a5', NULL, 'GoodService.StoreClerks.Update', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e9d72-4c3a-dcba-5812-04b8a9212dbb', NULL, 'GoodService.Stores', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-175a-8a3e-ef95-b2f23f4254db', NULL, 'GoodService.Stores', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e9d72-4c47-a7d7-a387-ce73f864bffd', NULL, 'GoodService.Stores.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17a8-6d8f-3259-24ef157f3f96', NULL, 'GoodService.Stores.Create', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e9d72-4c63-ce63-9244-e7f5648a2905', NULL, 'GoodService.Stores.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17f3-193d-32f4-3ea27360674c', NULL, 'GoodService.Stores.Delete', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a0e9d72-4c51-4322-0eb9-cae70ec8549f', NULL, 'GoodService.Stores.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17cc-a17b-2dc4-f7d39de5407a', NULL, 'GoodService.Stores.Update', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a180baf-2b4a-b7b9-6264-8c6cc6e92a50', NULL, 'OrderService.BillAftersales', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a180baf-2b8b-474f-df8f-8ab82d4afc4f', NULL, 'OrderService.BillAftersales.Operate', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a17152d-309d-71d9-8321-1af8b6b13b7c', NULL, 'OrderService.BillDeliverys', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a17152d-30a7-fe99-4d2b-770a009a123e', NULL, 'OrderService.BillDeliverys.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a182526-da10-207b-8149-20b49a0c4240', NULL, 'OrderService.BillReships', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a182526-da14-57dd-afbe-43476b52e94c', NULL, 'OrderService.BillReships.ConfirmDelivery', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1208d1-c595-8fa9-57a8-9212d9fe2307', NULL, 'OrderService.Logisticss', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-1785-550d-5c9c-6caa6f8c73c4', NULL, 'OrderService.Logisticss', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a1208d1-c5a0-695c-8211-665c2b4fe0c4', NULL, 'OrderService.Logisticss.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17b6-2e79-db11-5af62c222bc2', NULL, 'OrderService.Logisticss.Create', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a1208d1-c5b0-97d6-b835-fbb82fb3170c', NULL, 'OrderService.Logisticss.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-180a-af15-524e-0cf55c0546e9', NULL, 'OrderService.Logisticss.Delete', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a1208d1-c5a9-5ad5-291e-a4242dbb9069', NULL, 'OrderService.Logisticss.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17dc-0c2e-358a-55ef7bd527e1', NULL, 'OrderService.Logisticss.Update', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a135cac-c188-7cf0-9445-332b7f33547a', NULL, 'OrderService.Orders', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a170b25-8ced-3e19-e513-d74c97c3c0cc', NULL, 'OrderService.Orders.Cancel', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a170b25-8cf2-197c-6a70-e20c59f10dd5', NULL, 'OrderService.Orders.Complete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a170b25-8ce6-6d7a-6dc9-9165fd63094e', NULL, 'OrderService.Orders.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a170b25-8cf0-704a-2745-432743dca811', NULL, 'OrderService.Orders.Delivery', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a170b25-8cef-d2e4-0ad0-846edaf37763', NULL, 'OrderService.Orders.Pay', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a170b25-8cf1-b914-bf62-9f5df8fbc478', NULL, 'OrderService.Orders.Print', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a170b25-8cdb-c179-9c01-9047cd19ec37', NULL, 'OrderService.Orders.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1211b9-e31b-46b0-2804-7ec785a332cb', NULL, 'OrderService.Ships', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-1788-0669-edc0-648713af8af6', NULL, 'OrderService.Ships', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a1211b9-e324-0ca9-39d1-f7cc14f6c5f2', NULL, 'OrderService.Ships.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17b7-b293-7a3a-3c94213be5aa', NULL, 'OrderService.Ships.Create', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a1211b9-e32f-018b-040d-edcded7a99f5', NULL, 'OrderService.Ships.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-180e-a4c8-51c7-8b013c1b5907', NULL, 'OrderService.Ships.Delete', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a1211b9-e32a-ae79-95b9-6dc0b40037f5', NULL, 'OrderService.Ships.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a1be864-17de-1206-df4c-29f6845087e0', NULL, 'OrderService.Ships.Update', 'R', '工作人员');
INSERT INTO `sys_permissiongrants` VALUES ('3a135690-f386-63c4-356b-be2d95cbe609', NULL, 'PaymentService.BillPayments', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a182a3a-039b-382e-7931-ef8c0ccc99ec', NULL, 'PaymentService.BillRefunds', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a182a41-3093-b4a3-66ce-015c4a92da43', NULL, 'PaymentService.BillRefunds.AuditRefund', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a13563f-38b0-6a7f-710d-270e52fe9e1d', NULL, 'PaymentService.Payments', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f479c-c91c-22f9-ad9d-dae944df9bd6', NULL, 'PromotionService.Coupons', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f4c84-f35f-ee0a-2fa6-15fc2ab10eac', NULL, 'PromotionService.Coupons.Couponlist', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f479c-c932-c5de-9541-ac689a11f45b', NULL, 'PromotionService.Coupons.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f479c-c962-0bdf-b863-74fa72a50f0a', NULL, 'PromotionService.Coupons.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f479c-c94a-e122-b2ed-f5ebf25031f2', NULL, 'PromotionService.Coupons.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f516f-a8f7-53bc-3031-7f1f4600ade2', NULL, 'PromotionService.GroupSeckills', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f516f-a90f-3377-2f2a-4fdf1012b97b', NULL, 'PromotionService.GroupSeckills.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f516f-a934-ade3-61ea-cc4845b74954', NULL, 'PromotionService.GroupSeckills.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f516f-a922-20e7-9a9e-a465e33bb089', NULL, 'PromotionService.GroupSeckills.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f18e0-e7b2-a623-53f0-0d2243ca798f', NULL, 'PromotionService.PinTuanRecords', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f18e1-0b4f-2e10-709a-00cc60ec0166', NULL, 'PromotionService.PinTuanRules', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f18e1-54a1-f658-bf59-33a1b525dfc6', NULL, 'PromotionService.PinTuanRules.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f18e1-2458-f0a5-5665-5c5e6f0489ef', NULL, 'PromotionService.PinTuanRules.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f18e1-3d62-981c-8c88-41427b1e7c4e', NULL, 'PromotionService.PinTuanRules.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f1ce8-2243-59c8-0523-4d646865bcf9', NULL, 'PromotionService.Promotions', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f1ce8-2255-a6e9-7324-4b84c846add1', NULL, 'PromotionService.Promotions.Create', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f1ce8-2273-82ab-bf93-8fd239e5bc26', NULL, 'PromotionService.Promotions.Delete', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0f1ce8-2265-3902-792e-a67823380679', NULL, 'PromotionService.Promotions.Update', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f5-e02f-526b-5acec4f5daa6', NULL, 'SettingManagement.Emailing', 'R', 'admin');
INSERT INTO `sys_permissiongrants` VALUES ('3a0d00cb-a5f5-a637-2366-847b83350210', NULL, 'SettingManagement.Emailing.Test', 'R', 'admin');

-- ----------------------------
-- Table structure for sys_permissiongroups
-- ----------------------------
DROP TABLE IF EXISTS `sys_permissiongroups`;
CREATE TABLE `sys_permissiongroups`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DisplayName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_Sys_PermissionGroups_Name`(`Name`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_permissiongroups
-- ----------------------------
INSERT INTO `sys_permissiongroups` VALUES ('3a0d00e1-e097-0f16-e3ce-c5575e202839', 'AbpIdentity', 'L:AbpIdentity,Permission:IdentityManagement', '{}');
INSERT INTO `sys_permissiongroups` VALUES ('3a0d00e1-e0b1-0d6a-ee53-d67e211ae8be', 'FeatureManagement', 'L:AbpFeatureManagement,Permission:FeatureManagement', '{}');
INSERT INTO `sys_permissiongroups` VALUES ('3a0d00e1-e0b1-b7d3-f8c7-597a0518cf29', 'SettingManagement', 'L:AbpSettingManagement,Permission:SettingManagement', '{}');
INSERT INTO `sys_permissiongroups` VALUES ('3a0d00e1-e0b9-8815-09b7-0b8636889bb0', 'BaseService', 'F:BaseService', '{}');
INSERT INTO `sys_permissiongroups` VALUES ('3a0d00e1-e0b9-ed3d-6226-0f4af2a4a2ff', 'AbpTenantManagement', 'L:AbpTenantManagement,Permission:TenantManagement', '{}');
INSERT INTO `sys_permissiongroups` VALUES ('3a0d8c2a-f27c-8a84-a2d7-37e20423c6f7', 'GoodService', 'F:GoodService', '{}');
INSERT INTO `sys_permissiongroups` VALUES ('3a0f425e-f533-5ad4-427b-e307078f48e8', 'PromotionService', 'F:PromotionService', '{}');
INSERT INTO `sys_permissiongroups` VALUES ('3a11a58e-59f1-ca11-4372-d2af16baf9d1', 'OrderService', 'F:OrderService', '{}');
INSERT INTO `sys_permissiongroups` VALUES ('3a12dc57-5496-a1ce-f435-4573809f964f', 'PaymentService', 'F:PaymentService', '{}');

-- ----------------------------
-- Table structure for sys_permissions
-- ----------------------------
DROP TABLE IF EXISTS `sys_permissions`;
CREATE TABLE `sys_permissions`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GroupName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ParentName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DisplayName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsEnabled` tinyint(1) NOT NULL,
  `MultiTenancySide` tinyint UNSIGNED NOT NULL,
  `Providers` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `StateCheckers` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_Sys_Permissions_Name`(`Name`) USING BTREE,
  INDEX `IX_Sys_Permissions_GroupName`(`GroupName`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_permissions
-- ----------------------------
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0a9-c9be-83ce-4d2f7a327dc2', 'AbpIdentity', 'AbpIdentity.Roles', NULL, 'L:AbpIdentity,Permission:RoleManagement', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b1-0ad4-b47d-2cc6662ad7b3', 'AbpIdentity', 'AbpIdentity.Users.ManagePermissions', 'AbpIdentity.Users', 'L:AbpIdentity,Permission:ChangePermissions', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b1-26fa-7728-d9c7e27542d6', 'FeatureManagement', 'FeatureManagement.ManageHostFeatures', NULL, 'L:AbpFeatureManagement,Permission:FeatureManagement.ManageHostFeatures', 1, 2, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b1-345c-7d6f-34a25d780f4e', 'AbpIdentity', 'AbpIdentity.Users.Update', 'AbpIdentity.Users', 'L:AbpIdentity,Permission:Edit', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b1-4af9-064c-42a2d4dfa6c7', 'AbpIdentity', 'AbpIdentity.Users.Delete', 'AbpIdentity.Users', 'L:AbpIdentity,Permission:Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b1-699b-61e7-475e46489ee7', 'AbpIdentity', 'AbpIdentity.Roles.Delete', 'AbpIdentity.Roles', 'L:AbpIdentity,Permission:Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b1-6b75-a3de-acdaa26bcdc7', 'AbpIdentity', 'AbpIdentity.Roles.ManagePermissions', 'AbpIdentity.Roles', 'L:AbpIdentity,Permission:ChangePermissions', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b1-6cfb-66f4-8972e1814285', 'AbpIdentity', 'AbpIdentity.Roles.Create', 'AbpIdentity.Roles', 'L:AbpIdentity,Permission:Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b1-7e06-02fc-d6b7d248c0c7', 'SettingManagement', 'SettingManagement.Emailing', NULL, 'L:AbpSettingManagement,Permission:Emailing', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b1-881b-d52f-00f587fbd225', 'AbpIdentity', 'AbpIdentity.Roles.Update', 'AbpIdentity.Roles', 'L:AbpIdentity,Permission:Edit', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b1-e4bd-bf23-0b0c27bdc8cf', 'AbpIdentity', 'AbpIdentity.Users.Create', 'AbpIdentity.Users', 'L:AbpIdentity,Permission:Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b1-ecb9-fce8-1409f26aa677', 'AbpIdentity', 'AbpIdentity.UserLookup', NULL, 'L:AbpIdentity,Permission:UserLookup', 1, 3, 'C', NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b1-fa87-8859-10a79e117662', 'AbpIdentity', 'AbpIdentity.Users', NULL, 'L:AbpIdentity,Permission:UserManagement', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-011d-1dd5-f17c905b50cd', 'AbpTenantManagement', 'AbpTenantManagement.Tenants.Delete', 'AbpTenantManagement.Tenants', 'L:AbpTenantManagement,Permission:Delete', 1, 2, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-0186-4e48-0dee3dd8f594', 'BaseService', 'BaseService.Menus.Delete', 'BaseService.Menus', 'L:BaseService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-07c2-4984-393948180d0c', 'BaseService', 'BaseService.OrganizationUnits.Create', 'BaseService.OrganizationUnits', 'L:BaseService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-10d3-5988-a6eb77bde0a7', 'AbpTenantManagement', 'AbpTenantManagement.Tenants.ManageFeatures', 'AbpTenantManagement.Tenants', 'L:AbpTenantManagement,Permission:ManageFeatures', 1, 2, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-37c8-b172-8aa77f0abdfb', 'SettingManagement', 'SettingManagement.Emailing.Test', 'SettingManagement.Emailing', 'L:AbpSettingManagement,Permission:EmailingTest', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-6f29-0947-e3d8167399f9', 'BaseService', 'BaseService.OrganizationUnits.Update', 'BaseService.OrganizationUnits', 'L:BaseService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-847f-e06c-aeb3660b21c8', 'BaseService', 'BaseService.Menus.Update', 'BaseService.Menus', 'L:BaseService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-9170-3a49-24fc02807305', 'AbpTenantManagement', 'AbpTenantManagement.Tenants.ManageConnectionStrings', 'AbpTenantManagement.Tenants', 'L:AbpTenantManagement,Permission:ManageConnectionStrings', 1, 2, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-9847-916a-3f1179ffc5eb', 'BaseService', 'BaseService.Menus', NULL, 'L:BaseService,Menu', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-a728-55f6-c43c3fe17b3b', 'BaseService', 'BaseService.OrganizationUnits.Delete', 'BaseService.OrganizationUnits', 'L:BaseService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-bce5-12e2-9c8a223abc75', 'BaseService', 'BaseService.AuditLogging', NULL, 'L:BaseService,AuditLogging', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-c340-f48f-c708787faf93', 'AbpTenantManagement', 'AbpTenantManagement.Tenants', NULL, 'L:AbpTenantManagement,Permission:TenantManagement', 1, 2, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-c473-c367-67dacc2db22d', 'BaseService', 'BaseService.Menus.Create', 'BaseService.Menus', 'L:BaseService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-c871-847e-d6cca1189dde', 'BaseService', 'BaseService.OrganizationUnits.ManagingUser', 'BaseService.OrganizationUnits', 'L:BaseService,OrganizationUnit:ManagingUser', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-d0d6-69df-1e5179905aa0', 'AbpTenantManagement', 'AbpTenantManagement.Tenants.Create', 'AbpTenantManagement.Tenants', 'L:AbpTenantManagement,Permission:Create', 1, 2, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-d587-e994-34553ffd6b82', 'BaseService', 'BaseService.Homes', NULL, 'L:BaseService,Home', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-ea84-856b-60bc616466b9', 'BaseService', 'BaseService.OrganizationUnits', NULL, 'L:BaseService,OrganizationUnit', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-eb72-21bb-8ec9be2d7329', 'AbpTenantManagement', 'AbpTenantManagement.Tenants.Update', 'AbpTenantManagement.Tenants', 'L:AbpTenantManagement,Permission:Edit', 1, 2, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d00e1-e0b9-f8dd-f63a-0e961af06f93', 'BaseService', 'BaseService.OrganizationUnits.ManagingRole', 'BaseService.OrganizationUnits', 'L:BaseService,OrganizationUnit:ManagingRole', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d1a4b-6ce4-af98-8898-6ff8c5988e88', 'BaseService', 'BaseService.SecurityLog', NULL, 'L:BaseService,SecurityLog', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d4706-3cc6-01bc-7175-b9f58e9a1cf3', 'BaseService', 'BaseService.UserGrades.Create', 'BaseService.UserGrades', 'L:BaseService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d4706-3cc6-09ed-8a4f-0449edfd9b36', 'BaseService', 'BaseService.UserGrades', NULL, 'L:BaseService,UserGrade', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d4706-3cc6-3fca-701e-82773b23f928', 'BaseService', 'BaseService.UserGrades.Update', 'BaseService.UserGrades', 'L:BaseService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d4706-3cc6-98e5-cee4-c9ce167f7baa', 'BaseService', 'BaseService.UserWeChats.Create', 'BaseService.UserWeChats', 'L:BaseService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d4706-3cc6-a64d-dfff-50d640bb3be6', 'BaseService', 'BaseService.UserWeChats.Delete', 'BaseService.UserWeChats', 'L:BaseService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d4706-3cc6-b945-5efb-6092f3cea77a', 'BaseService', 'BaseService.UserGrades.Delete', 'BaseService.UserGrades', 'L:BaseService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d4706-3cc6-c938-2877-86c914483b3b', 'BaseService', 'BaseService.UserWeChats', NULL, 'L:BaseService,UserWeChat', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d4706-3cc6-da05-0408-11097284f761', 'BaseService', 'BaseService.UserWeChats.Update', 'BaseService.UserWeChats', 'L:BaseService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d53df-8a13-49fc-699b-ad70d53eaa9b', 'BaseService', 'BaseService.SysUsers.Create', 'BaseService.SysUsers', 'L:BaseService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d53df-8a13-59ef-74f9-64d7d8e2f1cb', 'BaseService', 'BaseService.SysUsers.Update', 'BaseService.SysUsers', 'L:BaseService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d53df-8a13-9c44-c947-de1ba071498e', 'BaseService', 'BaseService.SysUsers.Delete', 'BaseService.SysUsers', 'L:BaseService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d53df-8a13-b13a-9c04-822a5fb1ce55', 'BaseService', 'BaseService.SysUsers', NULL, 'L:BaseService,SysUser', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d8c2a-f27c-2353-a9e1-90b33eee7eb2', 'GoodService', 'GoodService.Brands.Delete', 'GoodService.GoodBrands', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d8c2a-f27c-4fb7-64b1-7402b26761bb', 'GoodService', 'GoodService.Brands', NULL, 'L:GoodService,GoodBrands', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d8c2a-f27c-7ced-a326-9de82f4b07dc', 'GoodService', 'GoodService.Brands.Create', 'GoodService.GoodBrands', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d8c2a-f27c-f57c-e12b-e29657916bd4', 'GoodService', 'GoodService.Brands.Update', 'GoodService.GoodBrands', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d8fb2-f990-061d-6dc5-ca9dd7f14c28', 'GoodService', 'GoodService.GoodCategorys', NULL, 'L:GoodService,Good:Category', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d8fb2-f990-1423-c743-75859649f863', 'GoodService', 'GoodService.GoodCategorys.Update', 'GoodService.GoodCategorys', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d8fb2-f990-eda2-9b36-a4b01829b164', 'GoodService', 'GoodService.GoodCategorys.Delete', 'GoodService.GoodCategorys', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d8fb2-f990-fb77-40f0-07a457bb5853', 'GoodService', 'GoodService.GoodCategorys.Create', 'GoodService.GoodCategorys', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d90bb-da7b-420b-c7e7-630b487f1bdf', 'GoodService', 'GoodService.GoodParams.Update', 'GoodService.GoodParams', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d90bb-da7b-cf87-cc12-f941ceaa97ea', 'GoodService', 'GoodService.GoodParams.Create', 'GoodService.GoodParams', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d90bb-da7b-d89a-4b20-2b7098321158', 'GoodService', 'GoodService.GoodParams', NULL, 'L:GoodService,Good:Param', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d90bb-da7b-e90d-fd62-7c07f073a4c6', 'GoodService', 'GoodService.GoodParams.Delete', 'GoodService.GoodParams', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d966b-dd06-12e8-6742-0e1064d2f653', 'GoodService', 'GoodService.GoodTypeSpecs', NULL, 'L:GoodService,Good:TypeSpec', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d966b-dd06-8662-09fd-3d9c521535ba', 'GoodService', 'GoodService.GoodTypeSpecs.Update', 'GoodService.GoodTypeSpecs', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d966b-dd06-ab8b-7b3b-c46796e85263', 'GoodService', 'GoodService.GoodTypeSpecs.Delete', 'GoodService.GoodTypeSpecs', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d966b-dd06-c3b2-8c03-8b095e86b78f', 'GoodService', 'GoodService.GoodTypeSpecs.Create', 'GoodService.GoodTypeSpecs', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d9a00-951b-0c1e-834e-e4f11e1096b3', 'GoodService', 'GoodService.GoodBrands.Create', 'GoodService.GoodBrands', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d9a00-951b-1940-35b9-b5d3aff8d02d', 'GoodService', 'GoodService.GoodBrands.Delete', 'GoodService.GoodBrands', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d9a00-951b-b7bb-5b9e-66abcb70cdb0', 'GoodService', 'GoodService.GoodBrands.Update', 'GoodService.GoodBrands', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0d9a00-951b-bf88-3a65-ef40522638a8', 'GoodService', 'GoodService.GoodBrands', NULL, 'L:GoodService,Brand', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0da600-2bab-1ba1-5c5c-7dac07aea249', 'GoodService', 'GoodService.Goods.Delete', 'GoodService.Goods', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0da600-2bab-3e88-6687-96535c5cc043', 'GoodService', 'GoodService.Goods.Create', 'GoodService.Goods', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0da600-2bab-8711-6378-beba81923d21', 'GoodService', 'GoodService.Goods', NULL, 'L:GoodService,Goods', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0da600-2bab-d39e-55bd-2a8e9847923e', 'GoodService', 'GoodService.Goods.Update', 'GoodService.Goods', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e0b5a-06c7-e8ff-4aa6-c3557236c819', 'GoodService', 'GoodService.Goods.Look', 'GoodService.Goods', 'L:GoodService,Look', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e8684-915b-072c-5a5a-6d3b5a4e6494', 'GoodService', 'GoodService.ServiceGoods.Delete', 'GoodService.ServiceGoods', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e8684-915b-07de-c899-792cdd285294', 'GoodService', 'GoodService.ServiceGoods.Create', 'GoodService.ServiceGoods', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e8684-915b-e955-7ac4-67bc1c93e1ac', 'GoodService', 'GoodService.ServiceGoods', NULL, 'L:GoodService,ServiceGood', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e8684-915b-fb79-df02-762e7478fddb', 'GoodService', 'GoodService.ServiceGoods.Update', 'GoodService.ServiceGoods', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e87d0-0008-2b76-e6ac-8e366fc35164', 'GoodService', 'GoodService.ServiceGoods.Look', 'GoodService.ServiceGoods', 'L:GoodService,Look', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e88d5-687e-0e92-6822-95332ffccd84', 'GoodService', 'GoodService.Stores.Create', 'GoodService.Stores', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e88d5-687e-168a-699f-5c53d92bcd74', 'GoodService', 'GoodService.Stores.Update', 'GoodService.Stores', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e88d5-687e-1846-dc02-f433793d5eb5', 'GoodService', 'GoodService.StoreClerks.Delete', 'GoodService.StoreClerks', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e88d5-687e-7b3b-5b71-7092b2b6dcb5', 'GoodService', 'GoodService.StoreClerks', NULL, 'L:GoodService,StoreClerk', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e88d5-687e-7d04-168a-c82942363b1a', 'GoodService', 'GoodService.Stores.Delete', 'GoodService.Stores', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e88d5-687e-c687-f6fd-1e4a21e62483', 'GoodService', 'GoodService.StoreClerks.Update', 'GoodService.StoreClerks', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e88d5-687e-e681-d082-61027af36e56', 'GoodService', 'GoodService.StoreClerks.Create', 'GoodService.StoreClerks', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e88d5-687e-feb2-ba96-c1dc9f05b743', 'GoodService', 'GoodService.Stores', NULL, 'L:GoodService,Store', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e8c4f-a8b4-23d2-3cf1-77df54c98ad4', 'GoodService', 'GoodService.Areas.Create', 'GoodService.Areas', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e8c4f-a8b4-5299-9142-b85a3f51a958', 'GoodService', 'GoodService.Areas.Update', 'GoodService.Areas', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e8c4f-a8b4-83ad-a494-fefe37b3ed37', 'GoodService', 'GoodService.Areas', NULL, 'L:GoodService,Area', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0e8c4f-a8b4-c69c-4221-1c27a7cb8d69', 'GoodService', 'GoodService.Areas.Delete', 'GoodService.Areas', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0ed13f-ae08-073f-6c20-665e1aabac3c', 'GoodService', 'GoodService.PageDesigns', NULL, 'L:GoodService,PageDesign', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0ed13f-ae08-1b31-7c5d-e3d48b5e91e7', 'GoodService', 'GoodService.PageDesigns.Delete', 'GoodService.PageDesigns', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0ed13f-ae08-2b24-99da-ff0f818899e4', 'GoodService', 'GoodService.PageDesigns.Create', 'GoodService.PageDesigns', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0ed13f-ae08-c581-0604-616f5c3eeeeb', 'GoodService', 'GoodService.PageDesigns.Update', 'GoodService.PageDesigns', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0ed14c-6e8c-40ac-295f-94d03f0f2231', 'GoodService', 'GoodService.PageDesigns.Layout', 'GoodService.PageDesigns', 'L:GoodService,Layout', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0ed14c-6e8c-e07e-b1d5-f67740feda8b', 'GoodService', 'GoodService.PageDesigns.Preview', 'GoodService.PageDesigns', 'L:GoodService,Preview', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0ed17d-6664-8bfb-195c-96c152c44e20', 'GoodService', 'GoodService.PageDesigns.Copy', 'GoodService.PageDesigns', 'L:GoodService,Copy', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0eddf8-ea73-0327-5076-3f944972563b', 'GoodService', 'GoodService.ArticleTypes.Create', 'GoodService.ArticleTypes', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0eddf8-ea73-08dc-43ec-15883f88d37b', 'GoodService', 'GoodService.ArticleTypes.Delete', 'GoodService.ArticleTypes', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0eddf8-ea73-3b8d-dd28-f853d74e532f', 'GoodService', 'GoodService.ArticleTypes', NULL, 'L:GoodService,ArticleType', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0eddf8-ea73-6fa2-d758-7c239ea26588', 'GoodService', 'GoodService.ArticleTypes.Update', 'GoodService.ArticleTypes', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0eddf8-ea73-71ed-4dea-82229a79f289', 'GoodService', 'GoodService.Articles.Delete', 'GoodService.Articles', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0eddf8-ea73-ba98-b1e6-852c3ac01a23', 'GoodService', 'GoodService.Articles.Update', 'GoodService.Articles', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0eddf8-ea73-e43e-5599-c4122a110046', 'GoodService', 'GoodService.Articles.Create', 'GoodService.Articles', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0eddf8-ea73-f38b-02ca-2a45d4a9918a', 'GoodService', 'GoodService.Articles', NULL, 'L:GoodService,Article', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f425e-f533-05bc-d838-209acbdc3da9', 'PromotionService', 'PromotionService.Promotions.Delete', 'PromotionService.Promotions', 'L:AuthService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f425e-f533-70dd-f03c-54ed586acc23', 'PromotionService', 'PromotionService.Promotions.Create', 'PromotionService.Promotions', 'L:AuthService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f425e-f533-8dc4-bf20-a834a0d73ced', 'PromotionService', 'PromotionService.Promotions', NULL, 'L:AuthService,Promotion', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f425e-f533-d82e-7d04-ba5b4f169e87', 'PromotionService', 'PromotionService.Promotions.Update', 'PromotionService.Promotions', 'L:AuthService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f4717-2ffc-5498-fbd0-52f3f3fcd4d6', 'PromotionService', 'PromotionService.PinTuanRules', NULL, 'L:AuthService,PinTuanRule', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f4717-2ffc-d765-90aa-6dd9ce3ee953', 'PromotionService', 'PromotionService.PinTuanRules.Create', 'PromotionService.PinTuanRules', 'L:AuthService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f4717-2ffd-0242-4373-d2d6539406b2', 'PromotionService', 'PromotionService.PinTuanRecords', NULL, 'L:AuthService,PinTuanRecord', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f4717-2ffd-3140-9744-0417d68c2c90', 'PromotionService', 'PromotionService.PinTuanRules.Delete', 'PromotionService.PinTuanRules', 'L:AuthService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f4717-2ffd-a5a6-ff1f-38cddee4e12c', 'PromotionService', 'PromotionService.PinTuanRules.Update', 'PromotionService.PinTuanRules', 'L:AuthService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f4772-09ac-0d0a-7a5f-1a0fa098ec4d', 'PromotionService', 'PromotionService.Coupons', NULL, 'L:AuthService,Coupon', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f4772-09ac-6bb6-9f54-2d73c077f348', 'PromotionService', 'PromotionService.Coupons.Create', 'PromotionService.Coupons', 'L:AuthService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f4772-09ac-9306-ff37-f1b0649d5c95', 'PromotionService', 'PromotionService.Coupons.Delete', 'PromotionService.Coupons', 'L:AuthService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f4772-09ac-c536-60b2-3e0bc8592e1e', 'PromotionService', 'PromotionService.Coupons.Update', 'PromotionService.Coupons', 'L:AuthService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f4c82-471f-b503-8461-b6d52233c5ef', 'PromotionService', 'PromotionService.Coupons.Couponlist', 'PromotionService.Coupons', 'L:AuthService,Couponlist', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f515c-062e-5154-edb7-b76dbb99b7dd', 'PromotionService', 'PromotionService.GroupSeckills.Create', 'PromotionService.GroupSeckills', 'L:AuthService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f515c-062e-b2c0-67d6-7c1866df49db', 'PromotionService', 'PromotionService.GroupSeckills', NULL, 'L:AuthService,GroupSeckill', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f515c-062e-b6f9-f33a-17b65369bdda', 'PromotionService', 'PromotionService.GroupSeckills.Delete', 'PromotionService.GroupSeckills', 'L:AuthService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f515c-062e-cee1-f30b-e6bc0e3716a9', 'PromotionService', 'PromotionService.GroupSeckills.Update', 'PromotionService.GroupSeckills', 'L:AuthService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f6967-5004-23bd-3e0d-54f5c6d20835', 'GoodService', 'GoodService.Notices', NULL, 'L:GoodService,Notice', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f6967-5004-5c14-2090-1b7d46b174f8', 'GoodService', 'GoodService.Notices.Update', 'GoodService.Notices', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f6967-5004-9323-0ea6-9eb44f4ef51e', 'GoodService', 'GoodService.Notices.Create', 'GoodService.Notices', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f6967-5004-bc01-58b1-5040cb9d2f13', 'GoodService', 'GoodService.Notices.Delete', 'GoodService.Notices', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f7080-84ad-6d10-840d-cffabe8ddfd0', 'GoodService', 'GoodService.Forms.Create', 'GoodService.Forms', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f7080-84ad-9000-5995-47627393da55', 'GoodService', 'GoodService.Forms', NULL, 'L:GoodService,Form', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f7080-84ad-a4a2-971f-bfa4929c052f', 'GoodService', 'GoodService.Forms.Delete', 'GoodService.Forms', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a0f7080-84ad-fd64-9fa8-09937d52a4d7', 'GoodService', 'GoodService.Forms.Update', 'GoodService.Forms', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a10ba60-b425-1ebd-16a6-9aa9434c60d8', 'GoodService', 'GoodService.ServiceDescriptions.Update', 'GoodService.ServiceDescriptions', 'L:GoodService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a10ba60-b425-645f-4532-7abc21777fb9', 'GoodService', 'GoodService.ServiceDescriptions.Create', 'GoodService.ServiceDescriptions', 'L:GoodService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a10ba60-b425-c34e-d79b-46d64cec1512', 'GoodService', 'GoodService.ServiceDescriptions.Delete', 'GoodService.ServiceDescriptions', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a10ba60-b425-fe47-c9e6-8329004b6837', 'GoodService', 'GoodService.ServiceDescriptions', NULL, 'L:GoodService,ServiceDescription', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a10be3d-6443-9a59-75d1-d22be2ce1286', 'BaseService', 'BaseService.PlatformSettings', NULL, 'L:BaseService,PlatformSetting', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a1208b3-3c95-2170-c220-99f251883356', 'OrderService', 'OrderService.Logisticss.Delete', 'OrderService.Logisticss', 'L:AuthService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a1208b3-3c95-5805-cad2-99c5d052d19c', 'OrderService', 'OrderService.Logisticss', NULL, 'L:AuthService,Logistics', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a1208b3-3c95-996a-cc6c-0d74e2ca5310', 'OrderService', 'OrderService.Ships', NULL, 'L:AuthService,Ship', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a1208b3-3c95-a3c8-9f3e-ff45f1deb15b', 'OrderService', 'OrderService.Logisticss.Create', 'OrderService.Logisticss', 'L:AuthService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a1208b3-3c95-a7ff-d752-f75abb551383', 'OrderService', 'OrderService.Ships.Create', 'OrderService.Ships', 'L:AuthService,Create', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a1208b3-3c95-a802-24ea-6c7f57690f91', 'OrderService', 'OrderService.Logisticss.Update', 'OrderService.Logisticss', 'L:AuthService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a1208b3-3c95-bb21-1115-115a590caff9', 'OrderService', 'OrderService.Ships.Delete', 'OrderService.Ships', 'L:AuthService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a1208b3-3c95-dbc0-fb4e-9ed30b1ed25d', 'OrderService', 'OrderService.Ships.Update', 'OrderService.Ships', 'L:AuthService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a1313ae-cfe8-e1c9-af16-65d14da0e1f7', 'AbpIdentity', 'AbpIdentity.Users.Update.ManageRoles', 'AbpIdentity.Users', 'L:AbpIdentity,Permission:ManageRoles', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a1313ae-cfe9-2746-50a5-32d1dead9773', 'SettingManagement', 'SettingManagement.TimeZone', NULL, 'L:AbpSettingManagement,Permission:TimeZone', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a13563d-8c5b-9a14-0d1c-7462e5346565', 'PaymentService', 'PaymentService.Payments', NULL, 'L:AuthService,Payment', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a13568e-2c2d-7100-4059-5d1c797ca0bd', 'PaymentService', 'PaymentService.BillPayments', NULL, 'L:AuthService,BillPayment', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a135cab-18d8-e9e6-3de2-9f07ca5a6767', 'OrderService', 'OrderService.Orders', NULL, 'L:AuthService,Order', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a137ada-5e9a-5b6a-3b2c-817b2a6ea39d', 'PaymentService', 'PaymentService.BillRefunds', NULL, 'L:AuthService,BillRefund', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a170b09-7e7e-1c15-3a85-71b6a3ffd665', 'OrderService', 'OrderService.Orders.Delivery', 'OrderService.Orders', 'L:AuthService,Delivery', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a170b09-7e7e-2d7f-daf3-1fdab273b0f0', 'OrderService', 'OrderService.Orders.Pay', 'OrderService.Orders', 'L:AuthService,Pay', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a170b09-7e7e-422f-8a73-c5b3000b3406', 'OrderService', 'OrderService.Orders.Complete', 'OrderService.Orders', 'L:AuthService,Complete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a170b09-7e7e-6620-3c41-5ee4747f4fb8', 'OrderService', 'OrderService.Orders.Print', 'OrderService.Orders', 'L:AuthService,Print', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a170b09-7e7e-6fca-cdd7-8b90be368e26', 'OrderService', 'OrderService.Orders.Update', 'OrderService.Orders', 'L:AuthService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a170b09-7e7e-e68c-a9b1-eced4c50de5d', 'OrderService', 'OrderService.Orders.Delete', 'OrderService.Orders', 'L:AuthService,Delete', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a170b09-7e7e-f1f1-74c3-9e19938646bc', 'OrderService', 'OrderService.Orders.Cancel', 'OrderService.Orders', 'L:AuthService,Cancel', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a17151c-6429-2436-e3ef-48928efb692c', 'OrderService', 'OrderService.BillDeliverys.Update', 'OrderService.BillDeliverys', 'L:AuthService,Update', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a17151c-6429-86c5-3089-16d00fa94b69', 'OrderService', 'OrderService.BillDeliverys', NULL, 'L:AuthService,BillDelivery', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a180b8c-e92f-016f-8beb-73c8971c2da5', 'OrderService', 'OrderService.BillAftersales', NULL, 'L:AuthService,BillAftersales', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a180b8c-e92f-27b4-417d-7d273fdd23cc', 'OrderService', 'OrderService.BillAftersales.Operate', 'OrderService.BillAftersales', 'L:AuthService,Operate', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a18251b-9dd5-3f22-5592-d93f923a12ab', 'OrderService', 'OrderService.BillReships.ConfirmDelivery', 'OrderService.BillReships', 'L:AuthService,ConfirmDelivery', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a18251b-9dd5-7da2-c5bd-3d2207fe92cc', 'OrderService', 'OrderService.BillReships', NULL, 'L:AuthService,BillReships', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a182a40-bb84-9699-d4f9-98110201d7d9', 'PaymentService', 'PaymentService.BillRefunds.AuditRefund', 'PaymentService.BillRefunds', 'L:AuthService,AuditRefund', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a18a5dc-4998-9a32-ccd4-a78abfe39bd1', 'GoodService', 'GoodService.GoodComments', NULL, 'L:GoodService,GoodComment', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a18a5dc-4998-ac32-e135-c1ed36bc6c08', 'GoodService', 'GoodService.GoodComments.SellerReply', 'GoodService.GoodComments', 'L:GoodService,SellerReply', 1, 3, NULL, NULL, '{}');
INSERT INTO `sys_permissions` VALUES ('3a18a5dc-4998-fe7c-cf48-cc2784888f2a', 'GoodService', 'GoodService.GoodComments.Delete', 'GoodService.GoodComments', 'L:GoodService,Delete', 1, 3, NULL, NULL, '{}');

-- ----------------------------
-- Table structure for sys_roleclaims
-- ----------------------------
DROP TABLE IF EXISTS `sys_roleclaims`;
CREATE TABLE `sys_roleclaims`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `RoleId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `ClaimType` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimValue` varchar(1024) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Sys_RoleClaims_RoleId`(`RoleId`) USING BTREE,
  CONSTRAINT `FK_Sys_RoleClaims_Sys_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `sys_roles` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_roleclaims
-- ----------------------------

-- ----------------------------
-- Table structure for sys_rolemenus
-- ----------------------------
DROP TABLE IF EXISTS `sys_rolemenus`;
CREATE TABLE `sys_rolemenus`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `RoleId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `MenuId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_rolemenus
-- ----------------------------
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-1afa-d3f0-4fd9601142ea', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c8ae9-8a9a-1171-41a7-27bd80527fbe', '{}', 'a83b2bc8c3a84c75b73897fd1540e2fa');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-2703-0adf-e0c1f33a9f5e', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c8ae7-86dd-8e3c-3919-c6a42647ceea', '{}', 'cf337e689fda4adf8779b990ce362e82');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-4c1a-e632-3c19d212da01', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c47a8-c51e-b9a6-11b4-1c6d2d529865', '{}', '42d87fe30a454419ad37bd60b8c24ea2');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-5df0-b500-785b0ec57145', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c8ae6-c836-9aae-3384-03df68ad8bde', '{}', '196b82b9b8bc42ce9fc720650eddcccc');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-7010-c2fe-5fbdbc64f467', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c8ae0-0757-2788-f99c-d17368e7bb08', '{}', '99f38c907b084ac2acad037445df2b8c');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-865e-9fb6-5a2c72c609a5', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c8ae5-6d66-b37a-1d74-26e051f263a0', '{}', 'edd00300b2fd43d5a1ebb2d8d4382e1e');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-8a5a-1091-d9d2c2675d1d', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c8dbe-c751-346c-5d62-aef59661964d', '{}', 'ed2a71dd642f4025838d6edcdb0aef26');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-8c81-f318-6b6a08488e2c', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c8d83-867f-e855-78b5-3126a7a47a4b', '{}', 'd1a37ee318d64b23bfd685a562f3abc6');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-9170-30a8-8152ddb9f559', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3fa85f64-5717-4562-b3fc-2c963f66afa6', '{}', '8739b96336494d868f7b3e12b6369cf4');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-9680-2335-f91e8e1dd948', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c8d70-ba54-d956-fdf5-24cd9ac3872f', '{}', '7a7696c943774411ad2ea219f5f6b09a');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-a9f5-4a06-e1f9061ad633', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c8ae6-0ea6-f0b8-86b2-d4a62847bef5', '{}', '1df2d2872fd24ed78a19c15997f101ea');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-be16-eaf6-ec50bd570c65', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c8dbd-2260-24fc-5d82-909fa4c960ed', '{}', '1cd73100588f4eb4ba91c5c628ea5e21');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-c932-3633-f92c88e1229e', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c421b-e2db-0819-2cee-ee3429285f4f', '{}', '0ac10f3a90164f82a10ca06280e2e23c');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-d734-4021-27c862b33e93', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c8ae8-b7cc-04c1-2d78-6793eaf631b5', '{}', 'b909a6a05d884fbeae7f60a9f0a02c0d');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-e0e9-865b-533c3ae8dea8', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c8dbb-4f12-9d3e-7726-a6c5bf19a144', '{}', '745336d2b0214c6d8bc47e59d6b61de0');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-ee6b-e857-10790a1aacde', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c8adf-5cf2-bf58-1407-bb458c8cb244', '{}', 'd51aa46db2a441568056f6e8ee67e8d9');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-f834-227f-78207551acf3', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c8ad3-a02e-4f05-10b7-e6ce623c9034', '{}', '991aaf6aa88a422f91d310897e04a306');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-f974-15a6-cb4ea029f198', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c421c-60a9-a76f-16f9-f3c6e4fa3661', '{}', '38762b55f7344537aebcf1297790d70f');
INSERT INTO `sys_rolemenus` VALUES ('3a0d3a78-9b6f-fcf2-538c-cb8c01d6ab31', NULL, '3a0d3a78-99ca-1ead-d07b-1d7dbc2d8403', '3a0c887c-daff-ffae-bc60-babce28a20a5', '{}', '23e9f6e688a7488789925e808d9f78ea');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-00b2-8efd-6c9d1b5f7c26', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ee018-2722-889f-0a9d-aedece8c0659', '{}', 'd83b75e22bba4bba99223c86fa3046e8');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-0381-19c7-ea9b510fd12d', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f4795-300c-e757-7f85-bdbeff7d215a', '{}', 'bfc9d0d7abb543c9b0de03e852cb26dd');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-0428-b109-44898c0fbc7c', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d4e96-f752-74fb-27c1-ea3fe6565f22', '{}', '6b96cc64f4f2424fb9a2022505538337');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-0453-c3b0-8e421ec80a12', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c8ae9-8a9a-1171-41a7-27bd80527fbe', '{}', '8eee0777a40744ef9eaad6b8ea26faf8');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-0525-6217-ac195fe2d4a8', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d90c7-2620-a03b-4238-bcec973c155d', '{}', '3323c86f2d34468d81ee3772ce846915');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-0645-06da-76d7bcf13827', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d5924-2776-8e74-b9fe-a894b63eb7fc', '{}', '9e0fa837293f44be877d4c98d6a64d16');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-07c5-f3f8-4b3c9e3bd42f', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0da606-94da-b786-38af-b98806eb8772', '{}', '6464906d869645b0bbd5b03214057858');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-08dc-3e49-420261d8cb45', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a17152b-79d7-82d0-e6a3-b85dcf73b3cc', '{}', '2be1773ba0084acb96dd124ca8039a78');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-0c8f-b81a-dbf952019e19', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0e9824-a17a-ac09-8538-b8b34359ded0', '{}', 'c903d3da1e334916a9aa78e7d0bb7be4');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-0d41-e1c8-cbc6c4002cf7', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d9673-7ef3-0b3c-89a4-c1d6ec97fdbb', '{}', '52f96e102ea6412ea125ba182a10d8ee');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-1022-cdfc-51c6497388f4', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0e9d69-d689-9721-508b-0ae9f99ab32f', '{}', '1226fe85b35a4d9e860e469703262e17');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-102b-30b7-4e7eb6652d21', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0e981f-e467-9c4b-7a9f-79e58762f53d', '{}', 'ef23ac948602436d957fe2af71914651');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-1451-d643-8ee0650473d5', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a10ba87-2e9e-44a1-aaf5-d77e3df69ec6', '{}', '28812be012fd418abaee80ee2fc8cf3c');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-1491-9f39-15e2a859fc0d', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0cb946-fc3c-6fff-1fbf-177d2b2f0d42', '{}', '0fbf5513b12845c2ab68ae8c3cec2e9a');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-14a5-3764-f56194dbe964', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a10ba85-006b-e7ac-a3df-e36662e4480a', '{}', 'e5285a5564054945a4e2f520f1856bfb');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-16a0-f764-3b3facb10b4d', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ee024-0840-eff6-5478-ac18ad8fd267', '{}', '6eaa3a0fb451407cbd69450a9f5c75d2');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-16ca-19e8-7dcf79211545', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ed14b-0f96-5d03-b52d-6459f4889441', '{}', '7343c6087a754190942eba6be9925251');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-17f5-e012-ec48ecb23bd9', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0e9823-ae4e-d6e7-2d0d-622a43aea35b', '{}', '057eca29274c4ec3a95303ba70d43673');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-1cc4-044d-c1c1a2334ba2', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a1211b6-0ed7-c0d5-86d6-a5d731d4e762', '{}', '763dd80942ff4c45bc66532e8b443cbd');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-1e12-553e-28d37d1eff5e', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d8fbd-a0d7-1379-41fd-bc1e86dfb1ee', '{}', '660bd6c5146d4370ad04ead63b0334b5');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-1f65-6b65-37661864c982', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d8c3b-1fc5-6cfa-6359-c951ca20f5fd', '{}', 'b92673db1ed9485d9fe3306e3a23c51b');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-2090-52b1-dd00cab1fb2c', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0e9d6a-9019-4530-fd88-dac80407f425', '{}', '2faf013215eb482f824908574116d166');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-215e-a90f-9ab82b851280', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0e9824-203e-c604-5a6c-235cd1923570', '{}', 'a747dfe81158491ab24069e1dffc074b');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-26f1-3a7d-3f547e23b2e0', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0eaad5-11cd-2f1b-199f-542827540f74', '{}', '21e839e4b0964cb08818f7522b3e638b');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-2cb3-e76c-72933b5c081f', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ed145-3af7-a2c4-ddc5-13e62997e98c', '{}', '775e3f1d287e468684095474ee8b996d');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-2d55-6f88-24bd95b91eea', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a17152c-af2f-8be4-9277-f0c27714f5a9', '{}', '9459ad5aa963436c9758112f5173c568');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-2f03-bc30-8ad023d98cfd', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f1cba-0bbf-f96e-8212-c7207916cc0c', '{}', '330d1fa9c30c450382d8346d2ba22858');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-2f9d-0379-3ab4c13f6d31', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0cb948-ba53-eea4-1f44-82de1e55a47b', '{}', '28ab38d11ea3441b8d503d1d4cac5247');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-30b8-e43c-8f5bb88609ec', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ed17f-cc4f-806e-7c18-ba37b7290e92', '{}', '14b2143d41fb4a5f8f40358906523509');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-3128-6650-b1700c498811', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0cb7ef-3a02-d126-4672-fd6977b739c5', '{}', '467aebf8bbb9469d8a6fa8659bc5a15b');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-31bd-3ff4-b8e06f797a71', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f6987-a01c-e841-f801-ca590e3f7c45', '{}', 'afc04517f21d48579ec45810df662437');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-3377-ab10-a726b9c6dce7', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0eaad5-d1c9-c645-8dc3-02a98d465553', '{}', '7ed364694a6c442caf9cc58ede288af6');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-3431-bc30-de8849b4e9c8', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ee01e-c2a5-440d-99d8-4b12af8461bd', '{}', '20558cfdaa0d44f4a6d018c802894f67');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-3493-88d6-c6be5dee441f', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a170b18-e522-69b4-fef1-31c51680451a', '{}', 'ed4f18184a524f4e90705d21a94e7ca3');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-36f0-1f33-754faace2c96', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d5923-7d6b-51f7-1aae-6f50feac8f2d', '{}', 'd9a0237fe88f434f81b6d65eaa32cf5f');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-395a-bfc1-fc12c985040f', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d8fbe-5954-1dda-d7d1-98461a6059d7', '{}', '2579f5870f2941e5af2234cf137e14ac');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-3981-e401-3e4e893e3df1', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0da603-bcb0-f640-afe8-15891470f8f4', '{}', '0d6dba4db8a3419cb66533e66a6c86ee');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-3e88-29a0-17fc5fcb8aae', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a180bae-beab-1fe4-c2dd-170435202ccd', '{}', 'ed8a2f6542ab4be0840347fb51b2ae7b');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-40a3-f8fc-94b7ddb2275d', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d9672-78b1-0dbb-a764-35991f0f826c', '{}', 'e217c529927d42569c0c23612686402b');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-442c-d7a8-7c80d451518e', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ed142-abf0-d056-1913-568e01539b73', '{}', 'dbb1dcf835414190acd13156e22934a6');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-45ee-2551-2d64617e3b22', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a1208d1-74a8-8240-ecab-7824b99675bd', '{}', '8ab9bb9390144329ac1ffcce68b9f25e');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-46c3-c29f-706877a7af7c', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a170b1f-a430-4d77-ef38-25bd37ec65d0', '{}', '01ad562147984969a84b86781eb99559');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-4b74-4ef2-c0126b85381c', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a1211b8-7d56-e651-92b8-2f9c9d89c594', '{}', 'a89e8b13548343459a02d3becaba9f9f');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-4b7f-2331-ca1c7ca49bb4', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a135ca3-6fbe-2e44-c73c-c23bd824e54a', '{}', '629f0e030d9e4b58aa067edbc57c6021');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-4baa-8b77-5e81a6a3ce18', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c8ae8-b7cc-04c1-2d78-6793eaf631b5', '{}', '1dee77b063b5467e990762b99f8d9521');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-4bfd-4eba-ed1d5aafb943', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a182521-728f-9acd-047d-ecc25b2f693c', '{}', '38deacc36924400d99ab3f0be161eb66');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-4d67-db2b-558383ebae93', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a135ca4-9803-5bdb-a3bb-4dc4339d315c', '{}', '8554e9fd3b8948ab99cd296a0e682506');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-4f3c-d1e1-6142c71da980', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a182526-6a66-1226-0572-ec6c687e7498', '{}', '0e4ca0b9def2424a85ac293e21072ec2');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-4f57-ea58-c73d7378c443', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f516b-c07a-fe9b-1799-5ec61470bb77', '{}', '00bca7c2b1e548318e531fc2dc2f6242');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-5299-817a-4ccb663bd662', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d8c3e-0133-f8db-5954-469130d06bb0', '{}', '1e0f6ff4a0f14784af667fc5303f3e36');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-52b5-b100-df2f31a2b5e8', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c8dbd-2260-24fc-5d82-909fa4c960ed', '{}', '7ff8392857d64dcc836a6606baf9fbc3');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-5379-e801-dcdd10c2d1b9', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a13563c-663c-e6f3-9616-9152ef5345e5', '{}', 'c27f04506cd841a98c53002800d057c3');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-547e-d271-da2c280fdf72', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c8d83-867f-e855-78b5-3126a7a47a4b', '{}', '21d062bdefdb4e3c81568ee53e613aa5');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-5626-3e36-b184623ff4ce', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a135caa-9cd9-8e4f-b5f9-68cb4dcb9b66', '{}', '9b63b0e7bd3f456c90b569d5bd81e9a9');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-563f-7dae-1d0ae846e9fc', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c421b-e2db-0819-2cee-ee3429285f4f', '{}', '035109b73ac242829ee35eced031d18a');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-5845-8cb0-bb096885bb50', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f516e-ec22-c319-70a9-69bafc303098', '{}', '5b009e5179604466be10954604688353');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-5c73-d3c8-7c8d83281847', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a170b1f-f7f0-0cab-f02a-7d62781ca1d1', '{}', '84661b8829904a7fbfaf03ff3b781186');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-5cfb-9b86-5550c2566e7f', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a1211b8-037d-0c2b-1cb8-f145a3e902c9', '{}', '0803be0b099b4b6cb6b0bb466805095d');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-5dec-6bef-79356ef89694', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a10be41-a84c-4381-b3b1-231f9a8540fa', '{}', '7a2c017e268f40edb3cc92c2510e5649');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-5e52-ff59-fec10da2414d', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d53d8-acdd-ab98-65bf-ed4a21ae5ced', '{}', '818fc464c9764b9f97b404ce5f127e4d');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-6040-5f59-29519cd53533', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d4e98-97cf-176e-95d8-5e21472df991', '{}', 'b93a3d4916114987a5cd96ea80fc25ca');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-609a-4517-5e6af3bbf65a', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c8ae6-c836-9aae-3384-03df68ad8bde', '{}', '5ca1c52f334f438eb6898d5702b50f54');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-61bf-3911-ec93916f72de', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d8c3d-69ea-3d6b-3e41-517efa421c82', '{}', '786f47bb8cd74e4a839d2d6843830d78');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-6369-4db0-4cab12625a25', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ee013-79ff-9a60-a04c-30ae5553edf6', '{}', '1f1f99e62ed14d93ba410a56a6fac8e5');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-649c-69a8-f132292a1e2b', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c8d70-ba54-d956-fdf5-24cd9ac3872f', '{}', '91278db41ad6490a8306f58704ba5378');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-65aa-e5aa-4b252eae7077', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d8c3c-f215-53a1-4752-f157bbcf794e', '{}', '270c55fe3e9a4ce79cff30f24d25e9eb');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-66ba-d7c3-b20c69008c56', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d8fbc-d7ef-44d1-afe0-23d61e0ae3e9', '{}', '9a20358fb536413eb08aac6ca863f8e1');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-673e-16af-961d66cc3595', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d5922-ec5b-7232-d7a2-d153f5e53af1', '{}', 'f617a187da89442b9618a5b8dc064c3d');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-67b3-2184-bf977ba7b7f2', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a182a38-aa7d-1d3d-7e2f-4ef0fb9cb012', '{}', '25d20b4bb3a144a28eb6b097e5d5795b');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-6998-4388-c53cf3b74c03', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f1cbb-ead9-58b3-868e-25dbd062fd6b', '{}', '76d17351214e4b658535a6eab66ec5ce');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-6a2b-2f31-af79a4387696', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a1208d0-d514-3baa-763b-6bc37dca1fc7', '{}', '64676700c6b84436afd3e1537a4e3aaf');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-6be4-cc74-1ce4a30e653f', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ed14b-ed79-9420-b1b4-b0f243afd7a9', '{}', '8e6429bc4e0142bb824fdd31904c2d6e');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-6c41-dd2e-c1850d42118d', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ed146-0086-114c-57b7-6630a268c624', '{}', 'fece91de0ea7441cbf28783afb73bab6');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-6d5a-2436-b2fffe272d63', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d1651-5048-73ff-aa78-0120e9d8717d', '{}', 'b1574a81f7b34238a738f712186be1b2');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-6ef8-bc3c-a1f9ec457195', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f516e-64e4-8fd4-6eae-deeb08661a6c', '{}', '884f46789a2c4159aec601e59fb75f85');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-6f26-5406-4f485f634832', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a18a5e8-b034-8d2a-de8f-67d45f686734', '{}', '15887db1acac430583ace6776c610554');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-7057-fb48-fa8f0415450c', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a18a5e4-91e3-d178-a101-f8aa3e4c1696', '{}', '521b7428a97b45898b90a123453bdee8');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-7164-61b7-f1ec9c72987a', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a1208cf-b42f-c72d-fd1a-22334877150d', '{}', '77c6047aa75f490e8545d4d945a7d194');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-724d-5ba5-07a9379c0552', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a180ba8-ef19-894d-9c9c-d8a7218d1313', '{}', '493911a80aed48f5a21d7110a9dde600');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-7286-e855-bca9cf49f9c0', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c887c-daff-ffae-bc60-babce28a20a5', '{}', '22c918fda5044802abc08e72f1a37746');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-75e8-dd42-89548b3d7f18', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0da606-074e-7080-44bf-de36c022c19e', '{}', 'aa3a025e5c404db98132d2b290c02e4c');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-76d3-3f05-8b7923827940', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f4796-3d71-9ea6-6350-e8facbd9e01a', '{}', '84b02ca7fc9749d1b2cc5063c1f1c8b7');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-77e2-60ed-202f2589d7f8', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d90c7-be28-e464-6432-6834338ff00d', '{}', '2577fb1130664c078f214d06a4aad718');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-7aae-38d7-80c4061a6737', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d90c6-9db1-6568-c23c-8a5a1a6941e5', '{}', 'edf67e47917f4f3f9632fa9650183ba0');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-7ac2-eb4e-e5506cdeef4b', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a135638-63a6-e9fd-d48d-04ea692b15c6', '{}', 'd6ca5e0b17b440be9c3a6336b2e830a8');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-7b1b-56cf-416eda786b14', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d53e0-4569-ba10-2a19-7bc878d96b84', '{}', 'ee1b9111626d41f899ba0ef30331be16');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-7c5a-1e91-d35f4dc8dc3c', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d90c5-b577-0557-f634-d794895ff921', '{}', 'e7a2e21619054075896dc659692272f3');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-7d9a-c9c4-0063b4a6bd60', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0cb7e8-b1f2-50d0-20c8-81746d2799c8', '{}', '0621f1916c444cc29e48d3bd3664631d');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-7ef9-7a5c-2ed2b81d409d', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a182a38-12a9-4280-7641-24418e7387a0', '{}', '03d21ca904584066bf5a95a8cbe94117');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-805e-0679-d1e74dbee759', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0e9d65-528a-ed87-34b8-ec56d87c4772', '{}', '1cee6a9329584b6982c8655543f45bda');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-8191-5a49-138be7d12108', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0eaad4-6d67-9656-222b-9d7706b5e873', '{}', '3a5e3def88fc4790b583e9baa6dc314f');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-830a-b957-ce5e4b5ff7a8', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c8ad3-a02e-4f05-10b7-e6ce623c9034', '{}', '9fc5ba51fea6484fb435bec18a2ff3e3');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-8435-c794-f0cdf0c616a8', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d476b-ebd0-142f-f26f-9dc94b1f06b5', '{}', 'd989639e6a5f45de939e7e5c3874eeaf');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-8486-a89f-0cb514951624', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a1211b7-72ad-7741-af61-f413b63bb1c4', '{}', '6b917c47b0654337b7a04cf3e2afebaf');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-8519-517e-0e12bf56434d', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a170b20-4496-a5c2-7d36-40f035b983c7', '{}', '2d90aeac2ed64ff287e8398f7cbd408e');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-85e7-2868-025fb0cb7431', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a10ba88-2df7-b613-93e1-20dc8dbc0d5c', '{}', 'dad92d80f34e46fa8df772944aaa4923');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-874d-efe1-f8dc613060fb', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a13568b-9464-8297-404e-df2e3567526f', '{}', '3d97f7a0c03a46069e62cdc406a71457');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-87f3-1b02-f3be2db4dc8e', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0e9823-05ef-09cb-ea3f-5b25ae405ce4', '{}', '4ad57fff99db4da38c4d343eb0c8afc1');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-8bb8-91b4-1d4df1b7ca69', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3fa85f64-5717-4562-b3fc-2c963f66afa6', '{}', '8c1ad6e9a2db4d1494b1ab2fa7023f58');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-8bc6-ae55-3b2f740e0494', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0e9d69-4e6d-4bb7-a6e8-ac9299b5c5f7', '{}', '4996d59037e942b09a341139115683a0');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-8bd1-e50b-322d472ca850', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0e0b51-6ce2-7d80-8665-cb46f2b61587', '{}', '9d50fdcb89bd401294f95e32d6a93063');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-8dc4-425d-a93f4d881d8f', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a180baa-f71d-a993-da67-cf40641bc99a', '{}', '409dffe2d75845ada0cd847fd7e8bd9f');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-8e1a-304d-ceff80dc6b27', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a182525-e9fb-8ac7-f551-2809b76b760b', '{}', '7e81c745d54a4c249e1139ee822c33b1');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-8ec2-f8ce-87a42b0c5312', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ee01e-544f-f4db-e705-80e7c66dd153', '{}', '037e8f089066419cb7902fa0a66ff27c');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-8f4d-9149-73a05ceb1e60', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f6987-1c1f-3ce8-e70c-5b2ce0c68311', '{}', '8cd23bbb2b764377bf848fba626b35d9');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-9060-cdff-3d9bedda7822', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f4796-e923-bdba-a3db-d6b83458e3bd', '{}', '848d86813d17412790a55b60ac2a9dd6');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-934a-e1ea-b7e62b3e73d3', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d9671-5393-66a1-75fe-134a29bc0822', '{}', 'f5dbb09ee38e4cbeb787964fe7a6f48d');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-9447-1b67-d2ed31e5768c', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d8fbb-e2a5-bbc8-f42c-11da7c443cfd', '{}', 'b5fdf5568f884bf2a95f281a76d10609');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-962c-96a0-fcefe1fa35b6', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c47a8-c51e-b9a6-11b4-1c6d2d529865', '{}', '99c37db3a72f439abc6c70f3c7e0c3b8');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-96dc-8f41-7c5f015ec732', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f6984-1a03-fb39-3701-760ee5fb75cb', '{}', '6e51e448b5114492bcaebb8c3c3bef43');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-97c2-9580-f556ea681340', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d9673-f3a2-5d97-c51f-b1f9c94cba29', '{}', '024c691a78e446a28d3ee36b1c8f2b77');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-99f4-f822-2b57eb14ae86', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d90c8-50b4-e83c-4f30-e4c3bac8f8fb', '{}', '11acc24037394b52af49e64fe2dd4193');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-9b72-e752-7b13a0287bde', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f516d-3630-c1ae-d175-58d1dfe50d6a', '{}', '234fbbc936fc4c4dafca611e9935bf31');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-9bd3-acb4-95a6f5e87a60', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a10be44-91c6-ed89-9349-5314d2a55c49', '{}', '3595a55dfcd04fbe924b43275187c342');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-9d26-ffdd-04a03ff66be3', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d476c-9dc2-a291-7452-870d458e3893', '{}', '3619a7e75e9e4947a61221c089f9e92b');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-9e5a-f417-25de232fe4f6', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f1cb6-bbdc-cec1-ea20-1c45ac21d981', '{}', '32b30af880b146ceb1dedba579b95533');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-a0e5-0ee1-76dc31b0a59f', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d9672-ec03-c1a0-52df-dae9b0e14e7d', '{}', 'f30bb9de201f4762965b2f5e650abda4');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-a2d1-e046-9051bb2f19e6', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a171529-a3c4-3597-3e0d-4cd0823c75dc', '{}', '921744b7149c4aeb90d164c20141dc5d');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-a32f-0c72-a385599a7ca3', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f1cb5-3508-5797-ebcd-0a8fa390bd85', '{}', '8cfbac8b42de416190ce196a4c9fd63d');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-a3f5-aee4-0a2a93f47446', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0e9d68-8a0f-d12e-7713-de606afa5c50', '{}', '4ee2e499efa04a84bf782a59f993c08d');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-a532-1257-c11087fad682', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d8c35-182b-82c2-6d95-6e2445c31cbf', '{}', '4b7da72abde64979b18cddbdbf512838');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-a719-2892-c580d11ecbd7', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ee01f-3d7b-da0f-504d-5331f41d470c', '{}', '3eeee9eecc4b40218ebbf801cb87ce2a');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-a81a-c9d4-e3301da0dcf6', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ce5fb-faa2-62c7-e10e-b2468d748b35', '{}', '6abcb01b926c4784a8eba929942dee3b');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-a9cb-ff4b-a916e45d1470', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f6986-8548-1797-33ab-ef09de34ab93', '{}', '30fe63888fe94aac8231ac33b761c3bb');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-ab9a-cc3e-e856c62d7609', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d4760-34fd-af48-e765-085a03ef028f', '{}', '57d95eee291e4289b3a4a8a0a4de6e1d');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-af1b-5350-de60da27e248', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c421c-60a9-a76f-16f9-f3c6e4fa3661', '{}', '560586b0a743422697b97f9ea1d752a5');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-afd6-b59e-5e7708b21c75', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0da606-ecef-e187-da76-120d68a63de1', '{}', '1ffc21fb0bca481ca4e008b03b3a1beb');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-b1a5-b3bf-33fe77a6b4f3', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d1646-12c1-0235-aff7-7e0e4b3d16b3', '{}', '796f150b7af14dfb8ba6b4187c25c344');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-b61e-193d-3e501ae68ecb', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a10ba88-9c63-1418-4310-ac33c8c037e3', '{}', 'd1977cb9f9314b808b5a18c097c8aad7');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-b6af-8ffe-c5292e67e989', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a1211b9-44ea-b8a4-7658-4f7d1baa2535', '{}', 'c9ba930da2c84982aa03938a4c4a3f99');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-bd7c-1a6a-1bf23a79949f', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a170b1e-7257-e508-86bf-009a53bbefa1', '{}', '589646b957cb4ff1bca532f852695306');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-bea1-ce2b-38263f40d48e', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c8ae0-0757-2788-f99c-d17368e7bb08', '{}', 'c1b2be14056540d9b88d028d3c34307f');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-c293-7688-4699cc7d67d5', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a1208c9-c25d-cf23-3b17-106f82f2d5c9', '{}', '3e864bbedf3a430b8ed8d86cbbc35046');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-c86d-165e-bec48af6f47b', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d164f-8d50-9772-1136-9adb118ef5f5', '{}', '6ff5193a8a2d4ca882d56fa2b4ccfd1b');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-c8d5-1cf4-00a5357c85df', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a18a5e7-d953-cd23-0b36-f66e9e3ca7e0', '{}', '8860a3aaefa34a4da3459c4c050220e2');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-c8d5-b0d7-cc8110fc8c0a', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f516d-b981-34f5-d316-84ab2a5d78a7', '{}', 'aed63abcb8264e2d866d7ac93e0bfb1a');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-c944-8df1-3acca7c807d0', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c8ae7-86dd-8e3c-3919-c6a42647ceea', '{}', '444a60f68a3c46ac92dbc53f4c5fd4f3');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-cf1b-6fed-137935448f9d', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ed145-947e-e097-4a99-30f11962ccbe', '{}', '635c4d18169444e09b251d70be44e21a');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-cf53-5956-e4d406f4b675', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d8c3c-53bc-3a7f-0656-1df0f4ead69e', '{}', '8a01a0e5019a4d9498bd49215e65709f');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-d10a-c44b-e7be0d7839a9', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0eaad6-4471-d698-3fa3-f4f28bd36594', '{}', '8a19b0fbc3024002a79c9d87d7bd6919');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-d187-d732-19c0a85a5575', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c8dbb-4f12-9d3e-7726-a6c5bf19a144', '{}', 'bfe2b4547f90426699fccad6d5b5009e');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-d395-7314-2ef9de8b1523', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ee01d-9114-5b37-26bc-09557e3b7666', '{}', '25511f59d3ab4ae2bffe8eb59113d84b');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-d684-b7e9-48ac69cc6de9', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0cb7f5-f34d-f43f-4afb-9697e2c62b1e', '{}', 'a05e4c66e32a47349524b4190733409b');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-d890-9ae4-68b67b083f0e', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a18a5e9-581a-0393-e7fe-bda0fd476cd6', '{}', 'c4ece2072c94484fb8e72ea669ddc1a7');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-d8db-c5c8-23ac97c96708', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a1208d0-55c4-2cba-617b-2f2b71ffa8c1', '{}', '32388e4fe9f64be988aebc112f34f528');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-dc52-f748-519b3601cc93', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a135635-6f21-a37b-35d4-9123c6579a61', '{}', 'f8399e2b563d4e92b0e791ecebd87dc9');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-dd5d-9771-94a08882e803', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a170b19-ad98-21c8-12ec-42cc386f6982', '{}', '7dfdc75e4a7f44c3b80570a7923f5597');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-ddb3-869b-e847f5ba5d45', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a13568c-bc49-64f0-5d00-7b7f49413f68', '{}', 'ef7a04b3ef364d73b8fd06a27bf72ba0');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-ddd9-03c8-0574c2bdbf46', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ed144-b9cf-d65e-d1a9-0c1074b512ef', '{}', '23818037195245bbb1ecd0635c552adf');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-df9f-d7e3-d7bb20fa1b65', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ce5fc-e2f3-aa6e-8b57-4016a1505e90', '{}', '4e0e998813ef4664af3a75781377682d');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-e056-6731-691790603923', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ee023-8ff5-724f-bd52-fa22d7570aed', '{}', '63772a77ebdc4389b16169453b4fe288');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-e20f-3cc2-1fe7e2f3bf50', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f4795-c504-e860-a434-f30ba1d18caf', '{}', '8a41c71bf2734236876f08873902716b');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-e35d-8547-c688588aec54', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c8dbe-c751-346c-5d62-aef59661964d', '{}', '2f628525b5d84013a7cdc0bee8982d81');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-e391-4a1b-3ed8410d9807', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f1cbb-7b8d-68e9-fe9a-6376ed3034ec', '{}', '1d2514ea00784042b49cd236534145c1');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-e3be-d62c-54d755c50578', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c8ae5-6d66-b37a-1d74-26e051f263a0', '{}', 'c653dc90f87745c79f976b0c779d5958');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-e61f-6c61-0dc7a9692f77', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d476a-a86c-f576-a2c9-abf08bf4aca9', '{}', 'a67a3b3841f04ba5a11694c4a97bdbc2');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-e685-3f3d-9b41052e30d7', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ee00f-a0da-e6ca-f973-cd8da7e43bd4', '{}', 'ae5b86657955403e81d561148834dba4');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-e831-e69a-d662d84bb73e', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a182a36-023f-d783-c03f-7564dcb1bd71', '{}', 'd52b6a2ef2ac49d1bedd57ef38f4431a');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-e8d2-979c-86694d833cb5', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0e981a-27a6-7cd1-e84f-f2d6ece390fd', '{}', '5488dd2231de482ebaba3af08adb2df3');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-e918-4025-13a34e695c25', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c8adf-5cf2-bf58-1407-bb458c8cb244', '{}', '7fa01f6c1d9f492f9c79c0be9a1c427d');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-edb9-020c-a8e887d70920', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0cb947-d038-2411-26df-486f3c978c68', '{}', 'c1332ade290b4c90be31d951bfddfb05');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-edd2-77a8-af1a78055cb5', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0cb7f3-be28-b6bd-be6f-4d2a2cea69ba', '{}', 'a5e4816a55944cdfbf295b9dab3c0852');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-edea-397f-71c755a1bd12', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a10ba87-a7b9-0214-5eeb-b480d33bca3d', '{}', '080e228c84434c078f309f6dc38f7716');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-eeb8-fbda-402fb2db4dfb', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ee020-cffe-d096-0ea1-0cdc99673d66', '{}', '49e3c1fc365d40288407597d3e24a4b8');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-efb9-145a-111b65b5fc73', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d4768-d354-7d6f-dd9c-b8e55848dab2', '{}', 'a7ee609eec3c4a4ea9f4d9dd677338c0');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-f0de-1916-bfb065beccbd', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a170b1d-4c5a-7e6e-8bb2-9aafd33db9ca', '{}', 'cad1df7aa2fc49ec9956e11fa718e14c');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-f547-fe9f-319cf812d356', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ee024-8886-ced2-96bd-708aabf6f2c4', '{}', '72843253656a4e84a6ae157448411aaf');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-f612-0a44-b2f672387373', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d4766-4067-51c7-ef8d-15a6f401b8c8', '{}', 'dd63dc63f9284b0e98e39a4016774428');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-f856-14a2-5a82a1f9f3ce', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f4c84-2db5-00d6-b7c2-9f00c141cd15', '{}', '7aaae0bd73b540339df9edf9a0f59ab0');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-f8ad-523b-b5e2cffcad26', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0da607-8b45-fe0c-4204-4a81ec977eb7', '{}', '15c28dc24c494305967b86735a22b1ff');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-fc6c-ab86-02b5d4a093be', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0ee023-20a2-64d3-1e0b-9cb990c7b05e', '{}', '79f9c7373def453f8df88d5f0e6faaa7');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-fd35-7001-7158c20ad195', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f4793-db0d-8261-3c1d-ec85433b6d89', '{}', 'd533deaeb763443d8eaff93ae29c558a');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-fe6a-2146-b818a1d51401', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f6986-12d0-78cd-a471-0dbf8af7dcae', '{}', '49c69fcffe9f4cb6aae343f2bc1fa0c6');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-fed4-20c5-80bd0d05d6fb', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0d8fb9-94cd-844b-2c49-9ec1b4113523', '{}', '011bdab3007647c88836f099943b6ae2');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-ff12-a1aa-ee9ca38fbca1', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0f1cba-692c-1f3d-78ef-80e8e68a025d', '{}', '0445219fb8b7436b920327d13cb6bc8a');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-ff48-0e7f-09f8284a6a44', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0eaad1-e19b-36c7-7dc4-25a80a75932f', '{}', '615ae21fe94d46eb99584e7279530ed4');
INSERT INTO `sys_rolemenus` VALUES ('3a1a3242-4a9e-ffba-80cc-9a509f873af8', NULL, '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', '3a0c8ae6-0ea6-f0b8-86b2-d4a62847bef5', '{}', '23600907f0114829857a9646dd0d184f');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-1aa6-6f66-3a9860b7b4fb', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c8ae9-8a9a-1171-41a7-27bd80527fbe', '{}', 'c25a00be95014c7e8c56b4bae1d064ea');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-262d-ab13-d71f2f3447be', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c8ae6-c836-9aae-3384-03df68ad8bde', '{}', '9d751d36b5254d89bd2bc9196ad94752');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-4142-7d8a-b36fb4cc5242', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0cb947-d038-2411-26df-486f3c978c68', '{}', '79aaf0e69c57493783d71e444b7539e2');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-43ed-d9a7-a950dcb21aa0', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0cb946-fc3c-6fff-1fbf-177d2b2f0d42', '{}', '7cca33ce49264d079abba23c21d92abc');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-4814-d9e5-7741891de225', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c8dbb-4f12-9d3e-7726-a6c5bf19a144', '{}', '07a2bf5ca8eb40aeb137b8e3b9ff802b');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-4c94-b6b7-c7982141d476', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c8ae7-86dd-8e3c-3919-c6a42647ceea', '{}', '64617d14df984650b93844eb936378f3');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-5366-b765-3aa3ddbee808', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c421b-e2db-0819-2cee-ee3429285f4f', '{}', 'ff4963498f6b43a7b6710368eea74e88');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-60d4-ecec-0115784a41b9', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0cb948-ba53-eea4-1f44-82de1e55a47b', '{}', '24db5795f07e497a98852d75f2a7a78e');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-6ea0-9559-3b638ea3c3a4', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0cb7f3-be28-b6bd-be6f-4d2a2cea69ba', '{}', '4a82637d03144f498a4abe5072d6dd48');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-6f82-91a3-6bd435c29b58', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c887c-daff-ffae-bc60-babce28a20a5', '{}', 'f88e6302cae84cf2a31d102f2cadaef6');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-77bc-d9e9-61836c98a1ad', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0cb7ef-3a02-d126-4672-fd6977b739c5', '{}', '5edea71de6f34d1eaac0fc19d9f74fb1');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-7be5-3929-e376c85a23ff', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0cb7e8-b1f2-50d0-20c8-81746d2799c8', '{}', 'f74bf32998d54b52b0f714b8b2baeb43');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-8068-031a-6497c9c76172', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c8d83-867f-e855-78b5-3126a7a47a4b', '{}', '95f5328adf694b6a983cfadda91223d7');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-8dfe-ed05-d5c3b55f1cf7', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3fa85f64-5717-4562-b3fc-2c963f66afa6', '{}', '569c9e3c1b3c47ff92d7011dead1c92d');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-8ea3-3992-9fce512de2f1', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c421c-60a9-a76f-16f9-f3c6e4fa3661', '{}', '3d46156ff66b4df38baa14bed597b122');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-98a6-6dbb-535e192dbf38', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0cb7f5-f34d-f43f-4afb-9697e2c62b1e', '{}', 'ff1ca0992b954648a9399954712f96b3');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-abfc-3379-e7b26a092d1d', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c8ae8-b7cc-04c1-2d78-6793eaf631b5', '{}', '30abddaa538742d7851fdfe3535280b6');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-ac2c-b19a-971ffc53b83a', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c8ad3-a02e-4f05-10b7-e6ce623c9034', '{}', '8a0a810c6b9842018f6f9c8e59258a4d');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-b130-940c-3be1fb49afce', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c8d70-ba54-d956-fdf5-24cd9ac3872f', '{}', 'b8a5b3923415477db356c672470a5716');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-b399-92a6-0541436b2264', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c8ae5-6d66-b37a-1d74-26e051f263a0', '{}', '744405a6ace84479a8edf8837764dadb');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-beda-26a6-d8911241f284', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c8ae6-0ea6-f0b8-86b2-d4a62847bef5', '{}', 'c8caf8e582f64496850b9cca5f17ac2b');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-c0fe-19af-615c43302a86', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c8ae0-0757-2788-f99c-d17368e7bb08', '{}', '377c4f169b2f4ab197209760d612e785');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-e2d0-4609-2e225c75c95e', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c8dbe-c751-346c-5d62-aef59661964d', '{}', '7e1582b6457f468da478cdfbb17374de');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-f800-a77b-86aed2e88018', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c8adf-5cf2-bf58-1407-bb458c8cb244', '{}', '5ffd57517150462d99210271511fdab0');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e54-fe75-270f-403fddc5acc9', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0c8dbd-2260-24fc-5d82-909fa4c960ed', '{}', 'da8423785183476f99edbd28311ea3f1');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e55-0aa6-9534-9189d99a955a', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0d53d8-acdd-ab98-65bf-ed4a21ae5ced', '{}', '46a53c529be74b8c84298a53aa8a4569');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e55-1535-36ec-471771ec87ea', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0d5924-2776-8e74-b9fe-a894b63eb7fc', '{}', '710a7b4577fa4f33ae09e020fdc81086');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e55-16b4-97b6-7cd5457d1d17', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0d476b-ebd0-142f-f26f-9dc94b1f06b5', '{}', '627de3c36ebd4331bcabb7d665a41867');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e55-1803-1413-808f9f71419e', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0d4e96-f752-74fb-27c1-ea3fe6565f22', '{}', 'b5fca6eaf3594060b0fd1e9ec5d3854a');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e55-1c7a-1d7f-b484553e59a1', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0d5923-7d6b-51f7-1aae-6f50feac8f2d', '{}', 'fcc61d9e17f94a30b7615c7bcbe5af51');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e55-2c7b-aa08-2c75a19519e4', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0d5922-ec5b-7232-d7a2-d153f5e53af1', '{}', 'a63268d6434143a7812ba54ab05ac344');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e55-3a0e-2306-5263c63d6a90', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0d4e98-97cf-176e-95d8-5e21472df991', '{}', 'b23daa9e06c64050923c1e42ee5b01dd');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e55-6543-835f-682cc347df94', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0d4768-d354-7d6f-dd9c-b8e55848dab2', '{}', 'dcc69ead277340aabe1150f61683b5f6');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e55-7357-2691-e114f00581f5', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0d4766-4067-51c7-ef8d-15a6f401b8c8', '{}', 'fe154356b1cf4273a016d08cc128dd28');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e55-9f37-ea2a-d599c067d5f6', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0d53e0-4569-ba10-2a19-7bc878d96b84', '{}', '7163a4064bc0469cb87a7cfd77b4b9d2');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e55-cdef-5fe7-5c4908c17427', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0d4760-34fd-af48-e765-085a03ef028f', '{}', 'a402b7ef8d5742eaa49c6da6762e10ee');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e55-d28c-7c34-ab23ec6dde4e', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0d476a-a86c-f576-a2c9-abf08bf4aca9', '{}', '8651294c0d4e41ecb3deb02aa4576d7e');
INSERT INTO `sys_rolemenus` VALUES ('3a1b7c1c-3e55-e9c6-4de5-94e891fea677', NULL, '3a1b7c1c-2668-3ab2-ca5e-5a9986e11aa9', '3a0d476c-9dc2-a291-7452-870d458e3893', '{}', '2caf1d42222e4606b892f75bec3048ea');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1900-36be-5b86-6d1cae054db4', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c421c-60a9-a76f-16f9-f3c6e4fa3661', '{}', '2e7aabd5aa2a4bcbbe103002568d9269');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1900-38d4-adc2-534caae86165', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c47a8-c51e-b9a6-11b4-1c6d2d529865', '{}', 'c0fc236dd0714ab4a3e67196bf5f0be2');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1900-c6d7-85ca-ff6e12fcddbd', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3fa85f64-5717-4562-b3fc-2c963f66afa6', '{}', 'a52f26d09c7b4586b897408aa9f8d920');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-0740-debd-7928a9d575fb', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0e9d65-528a-ed87-34b8-ec56d87c4772', '{}', 'a94193a71190460789ca2d4a88a3c8e2');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-08c9-c046-ec668e63e28c', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c8ae0-0757-2788-f99c-d17368e7bb08', '{}', 'b7e6ad4f1a8f45e595dd57cfa121ece8');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-1eb0-3874-219968c5bab2', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0ed14b-0f96-5d03-b52d-6459f4889441', '{}', 'eadcfcc8ef374b80940534395d33aa75');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-2146-ea6e-2e72fe49a771', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a10ba88-9c63-1418-4310-ac33c8c037e3', '{}', 'eb803ea886024e619d8c296169257051');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-2486-a88c-ccaf5fd64a1f', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0eaad1-e19b-36c7-7dc4-25a80a75932f', '{}', 'ac95a6409def44a6b33c4e88c33ec232');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-2600-66c9-0925508b433a', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0ed145-3af7-a2c4-ddc5-13e62997e98c', '{}', '9bf743d6f60a4412ac607eaed27aa32f');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-26cf-4176-a555213a7d6f', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a10ba87-2e9e-44a1-aaf5-d77e3df69ec6', '{}', '854c2576bda9426aa82eaf94e392e562');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-288b-7f48-6c60784960a3', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a10ba88-2df7-b613-93e1-20dc8dbc0d5c', '{}', 'f8effe1b2a0340eca20b6df4f80b71bf');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-2973-443c-11e4513d6409', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a1208d0-55c4-2cba-617b-2f2b71ffa8c1', '{}', 'b33613321b98407db6c6c066851ee21b');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-2d2b-c212-4b39a7176abf', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0e9823-ae4e-d6e7-2d0d-622a43aea35b', '{}', '982db44ebbe34d769faf4249af051a54');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-2db0-5dfd-c7b35af420f2', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0e9823-05ef-09cb-ea3f-5b25ae405ce4', '{}', 'e9e4e8c7d08c4510af7e2ca98ee2ffc0');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-311f-e418-2e30e79fbcfb', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0e9824-203e-c604-5a6c-235cd1923570', '{}', '00b92a0e90b0452abf35c2e7135c3a74');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-36a4-bc91-5012a90879b9', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0eaad5-d1c9-c645-8dc3-02a98d465553', '{}', '382907d492354777aad794421946cfe5');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-3d55-65fc-2304a1970c42', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c8ae6-c836-9aae-3384-03df68ad8bde', '{}', 'e9359d07ca0543e58a4836ce6a9d8de3');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-3f0b-766b-dfb7c2f0e71c', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c8dbe-c751-346c-5d62-aef59661964d', '{}', 'fdf8ad7419a14c93aee8d6878398bd41');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-431e-51ae-c1e0b20af0da', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0eaad5-11cd-2f1b-199f-542827540f74', '{}', '3f924c2f08764a6eb91a7a6e48dffa95');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-4b4c-dafa-54fc165b7c44', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c8dbd-2260-24fc-5d82-909fa4c960ed', '{}', '18966729ce28440589ad4cd3cc2c556d');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-4d11-deb3-4840dbdabb9e', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a1211b8-7d56-e651-92b8-2f9c9d89c594', '{}', '74d85e21f6884d468ea2beaf79e22f4a');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-4ed5-46b0-be2604cfc2ad', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0e9d69-4e6d-4bb7-a6e8-ac9299b5c5f7', '{}', '92ae677b64b44274bd1f0e4930d27153');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-588c-9fb2-24446f025324', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c8d83-867f-e855-78b5-3126a7a47a4b', '{}', '667b701fd13e423b9e1523e103189b53');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-5b62-690e-5f9ae70c9dc3', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a10be44-91c6-ed89-9349-5314d2a55c49', '{}', 'b6f1ebc865e84b7aaf420c4554e208c2');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-6279-9163-6b44e0484647', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0ed142-abf0-d056-1913-568e01539b73', '{}', '9a645875178447c5be43edb7efbdb3da');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-64f5-66f9-841a5478e7d5', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a10be41-a84c-4381-b3b1-231f9a8540fa', '{}', '1ee566fc68824568af7c3ceb39ab1799');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-6547-07d4-4090656af2d7', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0e981a-27a6-7cd1-e84f-f2d6ece390fd', '{}', 'c0d6ec8724914d7bbedb0ca1733af471');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-6c8b-5bb4-b8f1a7472d86', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0ed14b-ed79-9420-b1b4-b0f243afd7a9', '{}', 'adc84150685142ffbd8b977976b45a05');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-6ff0-0037-887918fd0f9f', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0ed144-b9cf-d65e-d1a9-0c1074b512ef', '{}', '1890eb03c76b448ebdd7fbccbf62e909');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-7230-740a-d96d62205e91', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c8ad3-a02e-4f05-10b7-e6ce623c9034', '{}', '822a215fbf7649cfa882379b3cda2dcf');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-7233-0ff0-25e2403ab20e', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a1208c9-c25d-cf23-3b17-106f82f2d5c9', '{}', '65f1d73cabd54761ba065f7fc60a0486');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-745e-c8e7-c92dab92d7eb', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0e9d6a-9019-4530-fd88-dac80407f425', '{}', '9a2da09825a7478c83a9649f97b219d8');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-780d-a5aa-7002d8acff4d', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0ed146-0086-114c-57b7-6630a268c624', '{}', 'a691c85f7e8244adbed12d5dbe3355fc');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-7914-070e-69803b0337d7', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0d1646-12c1-0235-aff7-7e0e4b3d16b3', '{}', '9cdf592e13c04300b2f4946bc03e14c1');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-7a82-eace-14957c7c91fe', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c8adf-5cf2-bf58-1407-bb458c8cb244', '{}', 'eedd55eb574b47a8a9db0e472f404918');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-7b3f-34e0-3507066fd997', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c8ae7-86dd-8e3c-3919-c6a42647ceea', '{}', '753ec4b084fc44c5b6cb8cc6d0613e1e');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-7b6f-3fa4-532543190bd7', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a10ba85-006b-e7ac-a3df-e36662e4480a', '{}', '5188f8ce758f4386952e68d3b06aa984');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-85d8-b836-188161321cbb', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a1208d0-d514-3baa-763b-6bc37dca1fc7', '{}', '505af2598b754b0398294504090f8149');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-8679-704e-dcc6e28997ce', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a1208cf-b42f-c72d-fd1a-22334877150d', '{}', '58549b71f0b148e59b18ff342bc67fe3');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-88fa-f758-8933d9cb966c', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0eaad6-4471-d698-3fa3-f4f28bd36594', '{}', '250a3ea589d64bf192b0919b2736538b');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-8d09-2968-02902b861884', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0eaad4-6d67-9656-222b-9d7706b5e873', '{}', 'c528afafb6bb46b691943b0e166d15f9');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-9e7f-5436-d8f2949763af', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c421b-e2db-0819-2cee-ee3429285f4f', '{}', '391991d71abd4d678eee1d8ce1dbe4ed');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-a25a-db95-05a1af70c46e', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a10ba87-a7b9-0214-5eeb-b480d33bca3d', '{}', '13d765c510d4451aa214e3faa0324f6f');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-a411-cc4f-6f22e3cccd91', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a1208d1-74a8-8240-ecab-7824b99675bd', '{}', '15f08a942a454b4f873b489c1676c0fa');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-aa19-d266-f8277eede99d', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c8ae5-6d66-b37a-1d74-26e051f263a0', '{}', '7dff6daf15f340678622c7aa48104d01');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-ac70-6410-d623d63c555e', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0ed17f-cc4f-806e-7c18-ba37b7290e92', '{}', '2f22175739cf494591701b3eecbe7ed8');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-b66c-1819-3771f52364b4', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c8d70-ba54-d956-fdf5-24cd9ac3872f', '{}', 'f3d94f9805da486abf939dbd26ee36ff');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-b931-bfcd-c3f84ef977bf', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0d1651-5048-73ff-aa78-0120e9d8717d', '{}', '43689f7564fb431096d506f02bc324ae');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-bf43-9bfe-8b244e2deb2c', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c887c-daff-ffae-bc60-babce28a20a5', '{}', '9bb5c65f12614445b273ad712e12cc23');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-c20d-c631-788256cedd56', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0ce5fc-e2f3-aa6e-8b57-4016a1505e90', '{}', '19ea9aa613164762942fa2a7b0984b0a');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-c29e-0d35-d7c3fa422e6f', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a1211b8-037d-0c2b-1cb8-f145a3e902c9', '{}', '0e9b36da3ae049cabb4bd5c6f5c9ab39');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-c43b-5ce8-481941caa538', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c8dbb-4f12-9d3e-7726-a6c5bf19a144', '{}', '9e8a796986254db5b0a0a763a527274b');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-c46e-b439-4e166f897271', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0e9d68-8a0f-d12e-7713-de606afa5c50', '{}', 'c3e4025fd33f4f74b228618e17f2ea7d');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-c5fa-ffad-8f3b10f43fd5', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0e9d69-d689-9721-508b-0ae9f99ab32f', '{}', 'f3f417555273490098179cfb917b8a5b');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-c861-ae43-6f6809907d9d', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a1211b9-44ea-b8a4-7658-4f7d1baa2535', '{}', '0f1a3a8e94154bf4a02bc9686fba6868');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-c900-a3a7-3aaf3e2d8c54', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c8ae8-b7cc-04c1-2d78-6793eaf631b5', '{}', 'de9cdee288544768b106afb49a6dce0d');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-cbc8-70a8-8bed87bf269b', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a1211b7-72ad-7741-af61-f413b63bb1c4', '{}', 'f7aee4e418e34c62af942420924c19fb');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-d090-47cb-cffc123e981f', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a1211b6-0ed7-c0d5-86d6-a5d731d4e762', '{}', '05035681e3c446d99bb610c6341f5d6a');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-d4d3-2c9a-65965c5a6811', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0e9824-a17a-ac09-8538-b8b34359ded0', '{}', '3cbde83f214c4d63b6ebce0a49b47b69');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-d4ef-9a81-c7193d643528', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0d164f-8d50-9772-1136-9adb118ef5f5', '{}', 'e08665552769436b94ace8e4c5d4995e');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-d8c9-4c8d-5dc513ff95a2', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0ed145-947e-e097-4a99-30f11962ccbe', '{}', 'f4cdeb1c49df479b9cb21dbfe5e4741d');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-ecd2-c818-591b083016f0', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c8ae6-0ea6-f0b8-86b2-d4a62847bef5', '{}', 'a05dd5bda89e4f54ad80982ac863bd73');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-f473-ad6c-cbe5d9bf5ff8', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0ce5fb-faa2-62c7-e10e-b2468d748b35', '{}', '5dd9ba124d6f48fb978de0eaf4964aac');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-f565-5500-a6c613033a43', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0e981f-e467-9c4b-7a9f-79e58762f53d', '{}', '0de2a01024bb4a47823422831b76f4a7');
INSERT INTO `sys_rolemenus` VALUES ('3a1be864-1901-f5c7-e1f5-9650e1a3f738', NULL, '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', '3a0c8ae9-8a9a-1171-41a7-27bd80527fbe', '{}', '5075a16c23bd4f34af3c89f10d651271');

-- ----------------------------
-- Table structure for sys_roles
-- ----------------------------
DROP TABLE IF EXISTS `sys_roles`;
CREATE TABLE `sys_roles`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsDefault` tinyint(1) NOT NULL,
  `IsStatic` tinyint(1) NOT NULL,
  `IsPublic` tinyint(1) NOT NULL,
  `EntityVersion` int NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Sys_Roles_NormalizedName`(`NormalizedName`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_roles
-- ----------------------------
INSERT INTO `sys_roles` VALUES ('3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', NULL, 'admin', 'ADMIN', 0, 1, 0, 40, '{}', 'dbb0995010304d0d8ab9ed403ce61301');
INSERT INTO `sys_roles` VALUES ('3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', NULL, '工作人员', '工作人员', 0, 0, 0, 3, '{}', 'f2884d42e1e14e7c9a6a8a3ba45f6f5e');

-- ----------------------------
-- Table structure for sys_securitylogs
-- ----------------------------
DROP TABLE IF EXISTS `sys_securitylogs`;
CREATE TABLE `sys_securitylogs`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `ApplicationName` varchar(96) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Identity` varchar(96) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Action` varchar(96) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `TenantName` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ClientId` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CorrelationId` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ClientIpAddress` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `BrowserInfo` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Sys_SecurityLogs_TenantId_Action`(`TenantId`, `Action`) USING BTREE,
  INDEX `IX_Sys_SecurityLogs_TenantId_ApplicationName`(`TenantId`, `ApplicationName`) USING BTREE,
  INDEX `IX_Sys_SecurityLogs_TenantId_Identity`(`TenantId`, `Identity`) USING BTREE,
  INDEX `IX_Sys_SecurityLogs_TenantId_UserId`(`TenantId`, `UserId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_securitylogs
-- ----------------------------
INSERT INTO `sys_securitylogs` VALUES ('3a1be938-6e1e-b7a4-cb2f-216e55d11596', NULL, '认证授权应用服务', 'OpenIddict', 'LoginSucceeded', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 'admin', NULL, 'RexShopWebAdminClient', 'e77462fdf9984fb9a616c51b7b9591e3', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/139.0.0.0 Safari/537.36', '2025-08-23 21:08:37.531126', '{}', '3ddd8333439146a89904cb9ab2a713fc');

-- ----------------------------
-- Table structure for sys_settingdefinitions
-- ----------------------------
DROP TABLE IF EXISTS `sys_settingdefinitions`;
CREATE TABLE `sys_settingdefinitions`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DisplayName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DefaultValue` varchar(2048) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsVisibleToClients` tinyint(1) NOT NULL,
  `Providers` varchar(1024) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsInherited` tinyint(1) NOT NULL,
  `IsEncrypted` tinyint(1) NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_Sys_SettingDefinitions_Name`(`Name`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_settingdefinitions
-- ----------------------------
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d000-24b9-9acb-18fe5b0dea50', 'Abp.Localization.DefaultLanguage', 'L:AbpLocalization,DisplayName:Abp.Localization.DefaultLanguage', 'L:AbpLocalization,Description:Abp.Localization.DefaultLanguage', 'en', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-048d-90bf-af07642b4db9', 'BaseService.OtherSetting.EntId', 'L:BaseService,DisplayName:BaseService.FileStorageSetting.EntId', 'L:BaseService,Description:BaseService.FileStorageSetting.EntId', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-05d0-4aba-2d2e6a0dd157', 'BaseService.FileStorageSetting.StoragePath', 'L:OrderService,DisplayName:BaseService.FileStorageSetting.StoragePath', 'L:OrderService,Description:BaseService.FileStorageSetting.StoragePath', '/upload/', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-0703-7680-f41ec8069819', 'AuthService.WeChatPlatform.MiniProgram.WxOpenEncodingAESKey', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.MiniProgram.WxOpenEncodingAESKey', 'L:AuthService,Description:AuthService.WeChatPlatform.MiniProgram.WxOpenEncodingAESKey', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-0d5e-dc82-356277c48ef7', 'BaseService.PlatformSetting.StoreSwitch', 'L:BaseService,DisplayName:BaseService.PlatformSetting.StoreSwitch', 'L:BaseService,Description:BaseService.PlatformSetting.StoreSwitch', 'False', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-0f10-3583-8e67bc8a9bac', 'BaseService.OtherSetting.StatisticsCode', 'L:BaseService,DisplayName:BaseService.FileStorageSetting.StatisticsCode', 'L:BaseService,Description:BaseService.FileStorageSetting.StatisticsCode', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-0f7e-88d1-e43a1f04eca5', 'BaseService.OrderSetting.StoreOrderAutomaticDelivery', 'L:BaseService,DisplayName:BaseService.OrderSetting.StoreOrderAutomaticDelivery', 'L:BaseService,Description:BaseService.OrderSetting.StoreOrderAutomaticDelivery', 'False', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-1be3-1c0c-7fa88b651d9c', 'AuthService.WeChatPlatform.WeChatPay.AppId', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.WeChatPay.AppId', 'L:AuthService,Description:AuthService.WeChatPlatform.WeChatPay.AppId', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-1d60-a845-9d5f4b4a2cb7', 'BaseService.PlatformSetting.ShopBeiAn', 'L:BaseService,DisplayName:BaseService.PlatformSetting.ShopBeiAn', 'L:BaseService,Description:BaseService.PlatformSetting.ShopBeiAn', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-20df-4cc4-4f3c5fcf4858', 'BaseService.OtherSetting.Kuaidi100Key', 'L:BaseService,DisplayName:BaseService.FileStorageSetting.Kuaidi100Key', 'L:BaseService,Description:BaseService.FileStorageSetting.Kuaidi100Key', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-271f-7aea-218f7883f77f', 'BaseService.PointsSetting.PointSwitch', 'L:BaseService,DisplayName:BaseService.PointsSetting.PointSwitch', 'L:BaseService,Description:BaseService.PointsSetting.PointSwitch', 'True', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-287e-97e0-f9dc860bd07f', 'BaseService.GoodsSetting.GoodsStocksWarn', 'L:BaseService,DisplayName:BaseService.GoodsSetting.GoodsStocksWarn', 'L:BaseService,Description:BaseService.GoodsSetting.GoodsStocksWarn', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-298d-1298-4746c7984e30', 'BaseService.InviteFriendSetting.CommissionType', 'L:BaseService,DisplayName:BaseService.InviteFriendSetting.CommissionType', 'L:BaseService,Description:BaseService.InviteFriendSetting.CommissionType', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-303b-b07f-9931a2c85106', 'BaseService.PlatformSetting.PlatformLogo', 'L:BaseService,DisplayName:BaseService.PlatformSetting.PlatformLogo', 'L:BaseService,Description:BaseService.PlatformSetting.PlatformLogo', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-30ab-26fb-9920838b82fb', 'BaseService.OrderSetting.OrderAutoEvalTime', 'L:BaseService,DisplayName:BaseService.OrderSetting.OrderAutoEvalTime', 'L:BaseService,Description:BaseService.OrderSetting.OrderAutoEvalTime', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-31eb-d248-b5245608300c', 'BaseService.OtherSetting.ShowApiAppId', 'L:BaseService,DisplayName:BaseService.FileStorageSetting.ShowApiAppId', 'L:BaseService,Description:BaseService.FileStorageSetting.ShowApiAppId', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-33cb-f9de-3435e62e26c3', 'BaseService.OrderSetting.OrderAutoSignTime', 'L:BaseService,DisplayName:BaseService.OrderSetting.OrderAutoSignTime', 'L:BaseService,Description:BaseService.OrderSetting.OrderAutoSignTime', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-351d-246d-a5de9c5a6c71', 'BaseService.UsersSetting.ShopMobile', 'L:BaseService,DisplayName:BaseService.UsersSetting.ShopMobile', 'L:BaseService,Description:BaseService.UsersSetting.ShopMobile', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-379c-7724-53626ba2e1b3', 'BaseService.PointsSetting.SignRandomMin', 'L:BaseService,DisplayName:BaseService.PointsSetting.SignRandomMin', 'L:BaseService,Description:BaseService.PointsSetting.SignRandomMin', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-3a34-9cc6-1a63b8fad3ba', 'BaseService.PlatformSetting.AboutArticle', 'L:BaseService,DisplayName:BaseService.PlatformSetting.AboutArticle', 'L:BaseService,Description:BaseService.PlatformSetting.AboutArticle', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-3e33-e106-d49741213448', 'BaseService.InviteFriendSetting.CommissionSecond', 'L:BaseService,DisplayName:BaseService.InviteFriendSetting.CommissionSecond', 'L:BaseService,Description:BaseService.InviteFriendSetting.CommissionSecond', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-3fe9-ae3d-fd12fe8030c1', 'BaseService.OrderSetting.OrderCancelTime', 'L:BaseService,DisplayName:BaseService.OrderSetting.OrderCancelTime', 'L:BaseService,Description:BaseService.OrderSetting.OrderCancelTime', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-43c5-1715-9f30765ccf83', 'Abp.Identity.Password.RequireNonAlphanumeric', 'L:AbpIdentity,DisplayName:Abp.Identity.Password.RequireNonAlphanumeric', 'L:AbpIdentity,Description:Abp.Identity.Password.RequireNonAlphanumeric', 'True', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-44eb-5d7a-13bdf6feb640', 'AuthService.WeChatPlatform.WeChatPay.WeChatRefundUrl', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.WeChatPay.WeChatRefundUrl', 'L:AuthService,Description:AuthService.WeChatPlatform.WeChatPay.WeChatRefundUrl', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-471f-b396-54881963977c', 'Abp.Identity.Lockout.MaxFailedAccessAttempts', 'L:AbpIdentity,DisplayName:Abp.Identity.Lockout.MaxFailedAccessAttempts', 'L:AbpIdentity,Description:Abp.Identity.Lockout.MaxFailedAccessAttempts', '5', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-4a7d-df54-e56ed410b561', 'AuthService.WeChatPlatform.OfficialAccount.WeiXinAppSecret', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.OfficialAccount.WeiXinAppSecret', 'L:AuthService,Description:AuthService.WeChatPlatform.OfficialAccount.WeiXinAppSecret', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-522e-0aa1-067b460555ef', 'BaseService.UsersSetting.IsBindMobile', 'L:BaseService,DisplayName:BaseService.UsersSetting.IsBindMobile', 'L:BaseService,Description:BaseService.UsersSetting.IsBindMobile', 'True', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-522e-1a8e-bc3fbf6365e7', 'BaseService.InviteFriendSetting.CommissionFirst', 'L:BaseService,DisplayName:BaseService.InviteFriendSetting.CommissionFirst', 'L:BaseService,Description:BaseService.InviteFriendSetting.CommissionFirst', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-5274-062b-0a89e0b4974d', 'BaseService.FileStorageSetting.StorageMaxSize', 'L:OrderService,DisplayName:BaseService.FileStorageSetting.StorageMaxSize', 'L:OrderService,Description:BaseService.FileStorageSetting.StorageMaxSize', '20', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-5507-ba9f-8e5402098f83', 'BaseService.PlatformSetting.RecommendKeys', 'L:BaseService,DisplayName:BaseService.PlatformSetting.RecommendKeys', 'L:BaseService,Description:BaseService.PlatformSetting.RecommendKeys', '商品|内容|管理|平台', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-599d-d6a4-347e838e7b0f', 'Abp.Identity.Password.RequiredLength', 'L:AbpIdentity,DisplayName:Abp.Identity.Password.RequiredLength', 'L:AbpIdentity,Description:Abp.Identity.Password.RequiredLength', '6', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-5c1b-f5cf-53598e8ab67b', 'AuthService.WeChatPlatform.MiniProgram.WxOpenToken', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.MiniProgram.WxOpenToken', 'L:AuthService,Description:AuthService.WeChatPlatform.MiniProgram.WxOpenToken', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-5eb2-b885-323a7555bd5b', 'Abp.Identity.User.IsEmailUpdateEnabled', 'L:AbpIdentity,DisplayName:Abp.Identity.User.IsEmailUpdateEnabled', 'L:AbpIdentity,Description:Abp.Identity.User.IsEmailUpdateEnabled', 'True', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-5ec7-a40e-5e10e952abc7', 'Abp.Mailing.DefaultFromAddress', 'L:AbpEmailing,DisplayName:Abp.Mailing.DefaultFromAddress', 'L:AbpEmailing,Description:Abp.Mailing.DefaultFromAddress', 'noreply@abp.io', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-608e-7ea8-5d0cfb185031', 'AuthService.WeChatPlatform.MiniProgram.WxOpenAppId', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.MiniProgram.WxOpenAppId', 'L:AuthService,Description:AuthService.WeChatPlatform.MiniProgram.WxOpenAppId', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-6090-8278-7420e5bd6f7c', 'BaseService.OtherSetting.QqMapKey', 'L:BaseService,DisplayName:BaseService.FileStorageSetting.QqMapKey', 'L:BaseService,Description:BaseService.FileStorageSetting.QqMapKey', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-67c3-2a17-bc2d74b92039', 'AuthService.WeChatPlatform.MiniProgram.WxOpenAppSecret', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.MiniProgram.WxOpenAppSecret', 'L:AuthService,Description:AuthService.WeChatPlatform.MiniProgram.WxOpenAppSecret', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-6acd-a603-630fa07ba1d7', 'BaseService.PlatformSetting.InvoiceSwitch', 'L:BaseService,DisplayName:BaseService.PlatformSetting.InvoiceSwitch', 'L:BaseService,Description:BaseService.PlatformSetting.InvoiceSwitch', 'True', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-6d71-67ea-f3405b8b0d13', 'Abp.Identity.SignIn.EnablePhoneNumberConfirmation', 'L:AbpIdentity,DisplayName:Abp.Identity.SignIn.EnablePhoneNumberConfirmation', 'L:AbpIdentity,Description:Abp.Identity.SignIn.EnablePhoneNumberConfirmation', 'True', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-6ef8-6afb-ac15ff57b0b9', 'BaseService.OrderSetting.ReshipAreaId', 'L:BaseService,DisplayName:BaseService.OrderSetting.ReshipAreaId', 'L:BaseService,Description:BaseService.OrderSetting.ReshipAreaId', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-7383-21db-843d2f4c6764', 'BaseService.ShareSetting.ShareDesc', 'L:BaseService,DisplayName:BaseService.ShareSetting.ShareDesc', 'L:BaseService,Description:BaseService.ShareSetting.ShareDesc', '优质好店邀您共享', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-7556-a853-4d824f7c18d1', 'Abp.Identity.Lockout.LockoutDuration', 'L:AbpIdentity,DisplayName:Abp.Identity.Lockout.LockoutDuration', 'L:AbpIdentity,Description:Abp.Identity.Lockout.LockoutDuration', '300', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-7597-cfc6-605bba568cd3', 'Abp.Mailing.Smtp.UseDefaultCredentials', 'L:AbpEmailing,DisplayName:Abp.Mailing.Smtp.UseDefaultCredentials', 'L:AbpEmailing,Description:Abp.Mailing.Smtp.UseDefaultCredentials', 'true', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-7599-c421-fc41d2791ab1', 'Abp.Mailing.Smtp.Domain', 'L:AbpEmailing,DisplayName:Abp.Mailing.Smtp.Domain', 'L:AbpEmailing,Description:Abp.Mailing.Smtp.Domain', NULL, 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-7d8e-dce9-7dac6a538b92', 'BaseService.OrderSetting.ReshipCoordinate', 'L:BaseService,DisplayName:BaseService.OrderSetting.ReshipCoordinate', 'L:BaseService,Description:BaseService.OrderSetting.ReshipCoordinate', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-7fff-83b0-8444dfc0d8aa', 'AuthService.WeChatPlatform.OfficialAccount.WeiXinAppId', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.OfficialAccount.WeiXinAppId', 'L:AuthService,Description:AuthService.WeChatPlatform.OfficialAccount.WeiXinAppId', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-8065-3f5c-58209557e04e', 'BaseService.CashSetting.TocashMoneyRate', 'L:BaseService,DisplayName:BaseService.CashSetting.TocashMoneyRate', 'L:BaseService,Description:BaseService.CashSetting.TocashMoneyRate', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-80da-ca51-944dcfb20e03', 'BaseService.OrderSetting.ReshipName', 'L:BaseService,DisplayName:BaseService.OrderSetting.ReshipName', 'L:BaseService,Description:BaseService.OrderSetting.ReshipName', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-80e4-2ee4-62f917422571', 'BaseService.PlatformSetting.UserAgreement', 'L:BaseService,DisplayName:BaseService.PlatformSetting.UserAgreement', 'L:BaseService,Description:BaseService.PlatformSetting.UserAgreement', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-853d-26e5-d8ed39780af7', 'Abp.Identity.SignIn.RequireConfirmedPhoneNumber', 'L:AbpIdentity,DisplayName:Abp.Identity.SignIn.RequireConfirmedPhoneNumber', 'L:AbpIdentity,Description:Abp.Identity.SignIn.RequireConfirmedPhoneNumber', 'False', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-860e-fa4c-a42262554471', 'Abp.Account.EnableLocalLogin', 'L:AbpAccount,DisplayName:Abp.Account.EnableLocalLogin', 'L:AbpAccount,Description:Abp.Account.EnableLocalLogin', 'true', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-8eec-bcbe-b73b75bfaead', 'BaseService.PlatformSetting.PrivacyPolicy', 'L:BaseService,DisplayName:BaseService.PlatformSetting.PrivacyPolicy', 'L:BaseService,Description:BaseService.PlatformSetting.PrivacyPolicy', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-8ef3-4400-99cf311d23be', 'BaseService.PlatformSetting.DefaultPic', 'L:BaseService,DisplayName:BaseService.PlatformSetting.DefaultPic', 'L:BaseService,Description:BaseService.PlatformSetting.DefaultPic', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-904d-4110-f7329ef27b88', 'Abp.Mailing.Smtp.Host', 'L:AbpEmailing,DisplayName:Abp.Mailing.Smtp.Host', 'L:AbpEmailing,Description:Abp.Mailing.Smtp.Host', '127.0.0.1', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-9185-c82a-11d5d41ad46b', 'Abp.Identity.Password.RequiredUniqueChars', 'L:AbpIdentity,DisplayName:Abp.Identity.Password.RequiredUniqueChars', 'L:AbpIdentity,Description:Abp.Identity.Password.RequiredUniqueChars', '1', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-9935-2560-4b5c819bc754', 'Abp.Mailing.Smtp.UserName', 'L:AbpEmailing,DisplayName:Abp.Mailing.Smtp.UserName', 'L:AbpEmailing,Description:Abp.Mailing.Smtp.UserName', NULL, 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-9e6e-d17c-8eaf1dadaa97', 'Abp.Identity.User.IsUserNameUpdateEnabled', 'L:AbpIdentity,DisplayName:Abp.Identity.User.IsUserNameUpdateEnabled', 'L:AbpIdentity,Description:Abp.Identity.User.IsUserNameUpdateEnabled', 'True', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-9fea-21fe-ad378f11ac49', 'BaseService.PointsSetting.OrdersPointProportion', 'L:BaseService,DisplayName:BaseService.PointsSetting.OrdersPointProportion', 'L:BaseService,Description:BaseService.PointsSetting.OrdersPointProportion', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-a064-90b8-f226451bff12', 'Abp.Mailing.Smtp.Port', 'L:AbpEmailing,DisplayName:Abp.Mailing.Smtp.Port', 'L:AbpEmailing,Description:Abp.Mailing.Smtp.Port', '25', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-a0c1-4fbc-d4e35e1bff06', 'AuthService.WeChatPlatform.WeChatPay.APIv3Key', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.WeChatPay.APIv3Key', 'L:AuthService,Description:AuthService.WeChatPlatform.WeChatPay.APIv3Key', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-a229-aef7-834d9e1bbbb5', 'BaseService.OtherSetting.Kuaidi100Customer', 'L:BaseService,DisplayName:BaseService.FileStorageSetting.Kuaidi100Customer', 'L:BaseService,Description:BaseService.FileStorageSetting.Kuaidi100Customer', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-a622-28d7-65bdcc074b54', 'AuthService.WeChatPlatform.WeChatPay.WeChatPayUrl', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.WeChatPay.WeChatPayUrl', 'L:AuthService,Description:AuthService.WeChatPlatform.WeChatPay.WeChatPayUrl', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-a629-5035-5ead1ace8efe', 'Abp.Identity.SignIn.RequireConfirmedEmail', 'L:AbpIdentity,DisplayName:Abp.Identity.SignIn.RequireConfirmedEmail', 'L:AbpIdentity,Description:Abp.Identity.SignIn.RequireConfirmedEmail', 'False', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-a71a-c947-ccf60c81883c', 'BaseService.FileStorageSetting.StorageType', 'L:OrderService,DisplayName:BaseService.FileStorageSetting.StorageType', 'L:OrderService,Description:BaseService.FileStorageSetting.StorageType', 'LocalStorage', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-a769-48ae-3b8410eb45b3', 'AuthService.WeChatPlatform.WeChatPay.RsaPublicKey', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.WeChatPay.RsaPublicKey', 'L:AuthService,Description:AuthService.WeChatPlatform.WeChatPay.RsaPublicKey', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-aaa8-662b-9d49aab13a76', 'Abp.Identity.Password.RequireUppercase', 'L:AbpIdentity,DisplayName:Abp.Identity.Password.RequireUppercase', 'L:AbpIdentity,Description:Abp.Identity.Password.RequireUppercase', 'True', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-af4e-59d0-eadd51dd69af', 'BaseService.OrderSetting.ReshipAddress', 'L:BaseService,DisplayName:BaseService.OrderSetting.ReshipAddress', 'L:BaseService,Description:BaseService.OrderSetting.ReshipAddress', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-b0cb-759b-c7d41392fd16', 'BaseService.PointsSetting.SignRandomMax', 'L:BaseService,DisplayName:BaseService.PointsSetting.SignRandomMax', 'L:BaseService,Description:BaseService.PointsSetting.SignRandomMax', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-b22c-0f7b-7c2d5b4e42cf', 'BaseService.CashSetting.TocashMoneyLimit', 'L:BaseService,DisplayName:BaseService.CashSetting.TocashMoneyLimit', 'L:BaseService,Description:BaseService.CashSetting.TocashMoneyLimit', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-b8a9-43e5-40b9ef9865ed', 'BaseService.ShareSetting.ShareTitle', 'L:BaseService,DisplayName:BaseService.ShareSetting.ShareTitle', 'L:BaseService,Description:BaseService.ShareSetting.ShareTitle', '优质好店邀您共享', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-bb6b-a755-d2e6be156021', 'BaseService.PointsSetting.OrdersRewardProportion', 'L:BaseService,DisplayName:BaseService.PointsSetting.OrdersRewardProportion', 'L:BaseService,Description:BaseService.PointsSetting.OrdersRewardProportion', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-bd47-20f6-7485096e8311', 'Abp.Identity.Password.ForceUsersToPeriodicallyChangePassword', 'L:AbpIdentity,DisplayName:Abp.Identity.Password.ForceUsersToPeriodicallyChangePassword', 'L:AbpIdentity,Description:Abp.Identity.Password.ForceUsersToPeriodicallyChangePassword', 'False', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-bd8d-9ab4-401861f65b57', 'BaseService.OrderSetting.RemindOrderTime', 'L:BaseService,DisplayName:BaseService.OrderSetting.RemindOrderTime', 'L:BaseService,Description:BaseService.OrderSetting.RemindOrderTime', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-bea0-5b8c-a2b26cdf2fcb', 'Abp.Identity.Lockout.AllowedForNewUsers', 'L:AbpIdentity,DisplayName:Abp.Identity.Lockout.AllowedForNewUsers', 'L:AbpIdentity,Description:Abp.Identity.Lockout.AllowedForNewUsers', 'True', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-c109-f871-ecea1002f61e', 'AuthService.WeChatPlatform.OfficialAccount.EncodingAESKey', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.OfficialAccount.EncodingAESKey', 'L:AuthService,Description:AuthService.WeChatPlatform.OfficialAccount.EncodingAESKey', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-c48f-0060-9661b4b235bc', 'BaseService.FileStorageSetting.StorageServerJson', 'L:OrderService,DisplayName:BaseService.FileStorageSetting.StorageServerJson', 'L:OrderService,Description:BaseService.FileStorageSetting.StorageServerJson', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-c84c-f269-051d1fbdfff9', 'AuthService.WeChatPlatform.WeChatPay.APIKey', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.WeChatPay.APIKey', 'L:AuthService,Description:AuthService.WeChatPlatform.WeChatPay.APIKey', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-ca3a-c1a9-e04807f58dee', 'AuthService.WeChatPlatform.WeChatPay.Certificate', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.WeChatPay.Certificate', 'L:AuthService,Description:AuthService.WeChatPlatform.WeChatPay.Certificate', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-ce51-60b1-c8d5f9e9ba5e', 'BaseService.SpecialSwitch.ShowCharge', 'L:BaseService,DisplayName:BaseService.SpecialSwitch.ShowCharge', 'L:BaseService,Description:BaseService.SpecialSwitch.ShowCharge', 'False', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-d319-879b-98aa56b6de6a', 'BaseService.FileStorageSetting.StorageSuffix', 'L:OrderService,DisplayName:BaseService.FileStorageSetting.StorageSuffix', 'L:OrderService,Description:BaseService.FileStorageSetting.StorageSuffix', 'gif,jpg,jpeg,png,bmp,xls,xlsx,doc,pdf,mp4,WebM,Ogv', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-d825-b20d-0299741b3306', 'Abp.Identity.Password.RequireDigit', 'L:AbpIdentity,DisplayName:Abp.Identity.Password.RequireDigit', 'L:AbpIdentity,Description:Abp.Identity.Password.RequireDigit', 'True', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-d87a-0929-eb065c4e67e0', 'Abp.Identity.OrganizationUnit.MaxUserMembershipCount', 'L:AbpIdentity,Identity.OrganizationUnit.MaxUserMembershipCount', 'L:AbpIdentity,Identity.OrganizationUnit.MaxUserMembershipCount', '2147483647', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-dbaa-8600-ff8a2d0b3542', 'BaseService.SpecialSwitch.ShowStore', 'L:BaseService,DisplayName:BaseService.SpecialSwitch.ShowStore', 'L:BaseService,Description:BaseService.SpecialSwitch.ShowStore', 'False', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-df72-3f55-fabafdf5cf70', 'BaseService.ShareSetting.ShareImage', 'L:BaseService,DisplayName:BaseService.ShareSetting.ShareImage', 'L:BaseService,Description:BaseService.ShareSetting.ShareImage', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-e06e-b0f8-0c051fd806a0', 'Abp.Mailing.Smtp.Password', 'L:AbpEmailing,DisplayName:Abp.Mailing.Smtp.Password', 'L:AbpEmailing,Description:Abp.Mailing.Smtp.Password', NULL, 0, NULL, 1, 1, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-e092-c56a-df41eb97ece8', 'BaseService.PlatformSetting.ShopDesc', 'L:BaseService,DisplayName:BaseService.PlatformSetting.ShopDesc', 'L:BaseService,Description:BaseService.PlatformSetting.ShopDesc', '平台描述会展示在前台及微信分享描述', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-e238-f12b-35a934ce482e', 'BaseService.OtherSetting.ShowApiSecret', 'L:BaseService,DisplayName:BaseService.FileStorageSetting.ShowApiSecret', 'L:BaseService,Description:BaseService.FileStorageSetting.ShowApiSecret', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-e42e-5f2a-e10145478cec', 'Abp.Mailing.Smtp.EnableSsl', 'L:AbpEmailing,DisplayName:Abp.Mailing.Smtp.EnableSsl', 'L:AbpEmailing,Description:Abp.Mailing.Smtp.EnableSsl', 'false', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-e482-002d-0b0868b1d47b', 'BaseService.PlatformSetting.ShopName', 'L:BaseService,DisplayName:BaseService.PlatformSetting.ShopName', 'L:BaseService,Description:BaseService.PlatformSetting.ShopName', 'Rex商城管理平台', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-e7b4-66d6-263f745d8f3d', 'BaseService.PointsSetting.PointDiscountedProportion', 'L:BaseService,DisplayName:BaseService.PointsSetting.PointDiscountedProportion', 'L:BaseService,Description:BaseService.PointsSetting.PointDiscountedProportion', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-e8ef-1422-84f4af043c81', 'BaseService.CashSetting.TocashMoneyLow', 'L:BaseService,DisplayName:BaseService.CashSetting.TocashMoneyLow', 'L:BaseService,Description:BaseService.CashSetting.TocashMoneyLow', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-e99b-cccc-0abff26e1c39', 'Abp.Identity.Password.PasswordChangePeriodDays', 'L:AbpIdentity,DisplayName:Abp.Identity.Password.PasswordChangePeriodDays', 'L:AbpIdentity,Description:Abp.Identity.Password.PasswordChangePeriodDays', '0', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-ed99-8bff-fa74d8980325', 'Abp.Identity.Password.RequireLowercase', 'L:AbpIdentity,DisplayName:Abp.Identity.Password.RequireLowercase', 'L:AbpIdentity,Description:Abp.Identity.Password.RequireLowercase', 'True', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-f26f-db36-977b24ba2be1', 'BaseService.OrderSetting.ReshipMobile', 'L:BaseService,DisplayName:BaseService.OrderSetting.ReshipMobile', 'L:BaseService,Description:BaseService.OrderSetting.ReshipMobile', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-f41d-81c0-f795d46d9f7d', 'BaseService.PointsSetting.SignPointType', 'L:BaseService,DisplayName:BaseService.PointsSetting.SignPointType', 'L:BaseService,Description:BaseService.PointsSetting.SignPointType', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-f4a3-e589-822b9b1d6669', 'Abp.Account.IsSelfRegistrationEnabled', 'L:AbpAccount,DisplayName:Abp.Account.IsSelfRegistrationEnabled', 'L:AbpAccount,Description:Abp.Account.IsSelfRegistrationEnabled', 'true', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-f531-5ee1-4fb20c3f2ae0', 'Abp.Timing.TimeZone', 'L:AbpTiming,DisplayName:Abp.Timing.Timezone', 'L:AbpTiming,Description:Abp.Timing.Timezone', 'UTC', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-f9d2-ca15-304feb318927', 'BaseService.InviteFriendSetting.CommissionThird', 'L:BaseService,DisplayName:BaseService.InviteFriendSetting.CommissionThird', 'L:BaseService,Description:BaseService.InviteFriendSetting.CommissionThird', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-fa56-e169-fdf045705a61', 'AuthService.WeChatPlatform.WeChatPay.MchId', 'L:AuthService,DisplayName:AuthService.WeChatPlatform.WeChatPay.MchId', 'L:AuthService,Description:AuthService.WeChatPlatform.WeChatPay.MchId', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-fe19-3978-40ddce898241', 'Abp.Mailing.DefaultFromDisplayName', 'L:AbpEmailing,DisplayName:Abp.Mailing.DefaultFromDisplayName', 'L:AbpEmailing,Description:Abp.Mailing.DefaultFromDisplayName', 'ABP application', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-d001-fe55-4073-2e60162748b5', 'BaseService.OrderSetting.OrderCompleteTime', 'L:BaseService,DisplayName:BaseService.OrderSetting.OrderCompleteTime', 'L:BaseService,Description:BaseService.OrderSetting.OrderCompleteTime', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-ec0d-4340-229c-f6599aa7c8d8', 'EasyAbp.Abp.WeChat.MiniProgram.AppId', 'L:BaseService,DisplayName:EasyAbp.Abp.WeChat.MiniProgram.AppId', 'L:BaseService,Description:EasyAbp.Abp.WeChat.MiniProgram.AppId', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-ec0d-4fe2-ead6-407686854a82', 'EasyAbp.Abp.WeChat.MiniProgram.AppSecret', 'L:BaseService,DisplayName:EasyAbp.Abp.WeChat.MiniProgram.AppSecret', 'L:BaseService,Description:EasyAbp.Abp.WeChat.MiniProgram.AppSecret', 'D7jlhiW2l48GdoCa+78EKg==', 0, NULL, 1, 1, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-ec0d-9078-54e5-17db8c94d1da', 'EasyAbp.Abp.WeChat.MiniProgram.EncodingAesKey', 'L:BaseService,DisplayName:EasyAbp.Abp.WeChat.MiniProgram.EncodingAesKey', 'L:BaseService,Description:EasyAbp.Abp.WeChat.MiniProgram.EncodingAesKey', 'D7jlhiW2l48GdoCa+78EKg==', 0, NULL, 1, 1, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1313ae-ec0d-f91c-9109-4574235a0bdc', 'EasyAbp.Abp.WeChat.MiniProgram.Token', 'L:BaseService,DisplayName:EasyAbp.Abp.WeChat.MiniProgram.Token', 'L:BaseService,Description:EasyAbp.Abp.WeChat.MiniProgram.Token', 'D7jlhiW2l48GdoCa+78EKg==', 0, NULL, 1, 1, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1314ba-494a-2486-80e6-8aedf0945603', 'EasyAbp.Abp.WeChat.Pay.IsSandBox', 'L:BaseService,DisplayName:EasyAbp.Abp.WeChat.Pay.IsSandBox', 'L:BaseService,Description:EasyAbp.Abp.WeChat.Pay.IsSandBox', 'False', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1314ba-494a-32f3-f336-12768a648157', 'EasyAbp.Abp.WeChat.Pay.NotifyUrl', 'L:BaseService,DisplayName:EasyAbp.Abp.WeChat.Pay.NotifyUrl', 'L:BaseService,Description:EasyAbp.Abp.WeChat.Pay.NotifyUrl', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1314ba-494a-7040-c0aa-b0a73fdc55a6', 'EasyAbp.Abp.WeChat.Pay.ApiKey', 'L:BaseService,DisplayName:EasyAbp.Abp.WeChat.Pay.ApiKey', 'L:BaseService,Description:EasyAbp.Abp.WeChat.Pay.ApiKey', 'D7jlhiW2l48GdoCa+78EKg==', 0, NULL, 1, 1, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1314ba-494a-7687-1c9e-2bad9f938321', 'EasyAbp.Abp.WeChat.Pay.AcceptLanguage', 'L:BaseService,DisplayName:EasyAbp.Abp.WeChat.Pay.AcceptLanguage', 'L:BaseService,Description:EasyAbp.Abp.WeChat.Pay.AcceptLanguage', NULL, 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1314ba-494a-a362-a52b-33839f5d5779', 'EasyAbp.Abp.WeChat.Pay.MchId', 'L:BaseService,DisplayName:EasyAbp.Abp.WeChat.Pay.MchId', 'L:BaseService,Description:EasyAbp.Abp.WeChat.Pay.MchId', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1314ba-494a-a9cf-a783-8ecabc752c94', 'EasyAbp.Abp.WeChat.Pay.CertificateSecret', 'L:BaseService,DisplayName:EasyAbp.Abp.WeChat.Pay.CertificateSecret', 'L:BaseService,Description:EasyAbp.Abp.WeChat.Pay.CertificateSecret', 'D7jlhiW2l48GdoCa+78EKg==', 0, NULL, 1, 1, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1314ba-494a-ae28-7482-f1f30238c289', 'EasyAbp.Abp.WeChat.Pay.CertificateBlobContainerName', 'L:BaseService,DisplayName:EasyAbp.Abp.WeChat.Pay.CertificateBlobContainerName', 'L:BaseService,Description:EasyAbp.Abp.WeChat.Pay.CertificateBlobContainerName', 'rex.shop', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1314ba-494a-e1fc-c8ad-e8394f3e75bd', 'EasyAbp.Abp.WeChat.Pay.RefundNotifyUrl', 'L:BaseService,DisplayName:EasyAbp.Abp.WeChat.Pay.RefundNotifyUrl', 'L:BaseService,Description:EasyAbp.Abp.WeChat.Pay.RefundNotifyUrl', '', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a1314ba-494a-e2d9-be81-5ef67bcda7ab', 'EasyAbp.Abp.WeChat.Pay.CertificateBlobName', 'L:BaseService,DisplayName:EasyAbp.Abp.WeChat.Pay.CertificateBlobName', 'L:BaseService,Description:EasyAbp.Abp.WeChat.Pay.CertificateBlobName', 'apiclient_cert.p12', 0, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a18d468-62fa-35d1-5eda-51dca0d2e42a', 'BaseService.PointsSetting.FirstSignPoint', 'L:BaseService,DisplayName:BaseService.PointsSetting.FirstSignPoint', 'L:BaseService,Description:BaseService.PointsSetting.FirstSignPoint', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a18d468-62fa-83e5-9d83-1cbaed5b22b3', 'BaseService.PointsSetting.ContinuitySignAdditional', 'L:BaseService,DisplayName:BaseService.PointsSetting.ContinuitySignAdditional', 'L:BaseService,Description:BaseService.PointsSetting.ContinuitySignAdditional', '', 1, NULL, 1, 0, '{}');
INSERT INTO `sys_settingdefinitions` VALUES ('3a18d468-62fa-f6f6-0be0-6bf80fd0f65a', 'BaseService.PointsSetting.SignMostPoint', 'L:BaseService,DisplayName:BaseService.PointsSetting.SignMostPoint', 'L:BaseService,Description:BaseService.PointsSetting.SignMostPoint', '', 1, NULL, 1, 0, '{}');

-- ----------------------------
-- Table structure for sys_settings
-- ----------------------------
DROP TABLE IF EXISTS `sys_settings`;
CREATE TABLE `sys_settings`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(2048) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderName` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ProviderKey` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_Sys_Settings_Name_ProviderName_ProviderKey`(`Name`, `ProviderName`, `ProviderKey`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_settings
-- ----------------------------
INSERT INTO `sys_settings` VALUES ('3a10e835-1dc5-5487-1313-306250460639', 'BaseService.PlatformSetting.ShopBeiAn', '湘ICP备15004965号-2', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1314e0-e4a2-fced-0fd6-853431f32e7e', 'EasyAbp.Abp.WeChat.MiniProgram.AppId', 'wx2710320b2b4ccd71', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1314e0-e4af-90df-7c19-ff2425cfa9ef', 'EasyAbp.Abp.WeChat.MiniProgram.AppSecret', 'Ktr4gZgZIeKTBxoA5r7OLf2PQulTtLJosyyS+qdLq6tmHBxStZ+xoteIHzhBMgKR', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a131980-c6d5-7170-4abe-5a726110751b', 'EasyAbp.Abp.WeChat.Pay.MchId', '1578574771', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a131980-c6e2-4797-4224-1600fc9f2405', 'EasyAbp.Abp.WeChat.Pay.ApiKey', 'rmcd0VC54Si6cF/A9bA9rFE+r+QP5e1qvnw4fYY4tRURE6Rh/dYOJhnswu03/Jpf', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a13241d-333c-9834-e4c3-2da038152989', 'EasyAbp.Abp.WeChat.Pay.CertificateSecret', '/VBqU++1iwRugPPQs41UuQ==', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a132443-d28b-eaaa-94b2-5e4abee250e4', 'EasyAbp.Abp.WeChat.Pay.NotifyUrl', 'https://4x7mhvxt-5510.inc1.devtunnels.ms/api/payment/common/wechat/notify', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1326d7-f1d2-4190-f283-5c358e636c92', 'EasyAbp.Abp.WeChat.Pay.AcceptLanguage', 'zh', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a182fd4-a543-246f-e054-abb0446beaa0', 'EasyAbp.Abp.WeChat.Pay.RefundNotifyUrl', 'https://4x7mhvxt-5510.inc1.devtunnels.ms/api/payment/common/wechat/refundNotify', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a18393b-76ca-a355-330d-6b98b533608e', 'BaseService.PointsSetting.SignPointType', '1', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a19652a-d576-4d72-3b48-ef5cf2f6a656', 'BaseService.SpecialSwitch.ShowCharge', 'True', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a196584-9551-13c3-9f5d-85c3363b16ee', 'BaseService.SpecialSwitch.ShowStore', 'True', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a19c67b-e8e7-660f-9592-1ec5a737ad0e', 'BaseService.PlatformSetting.AboutArticle', '{\"title\":\"关于我们\",\"id\":\"3a0ee070-d867-78ee-5e12-82de6dd2e44a\"}', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a19c67b-e8f2-12f9-6e9e-4aafd7664d78', 'BaseService.PlatformSetting.UserAgreement', '{\"title\":\"用户协议\",\"id\":\"3a19c66b-77d6-04f3-3432-78c3c212b5ac\"}', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a19c67b-e8f3-9915-898c-7c49bab64d66', 'BaseService.PlatformSetting.PrivacyPolicy', '{\"title\":\"隐私政策\",\"id\":\"3a19c671-606b-71b3-8b38-c959bf8a9872\"}', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a19c68b-dee8-6599-bffc-e4f0ce025218', 'BaseService.PlatformSetting.ShopName', 'Rex商城平台', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a52bb-7e59-fd63-d8b4-f1df47550e58', 'BaseService.PlatformSetting.PlatformLogo', 'http://localhost:9000/rex.shop/host/3a0d00cb-a16d-8c75-c7a7-27cd029a9534/20250605224618/logo-mini.png', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a70e6-0b1b-a021-3002-dfe23df9352e', 'BaseService.OtherSetting.QqMapKey', 'QRBBZ-UGWCJ-YYOF5-DVQCO-UGAFE-DAFRI', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a80a0-d1be-3144-c761-9acd0f18801d', 'BaseService.CashSetting.TocashMoneyLimit', '0', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a80a0-d1dd-8adf-c644-c0c8425cde44', 'BaseService.OrderSetting.OrderCancelTime', '10', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a80a0-d1e5-d436-7a29-7db31757347f', 'BaseService.OrderSetting.OrderCompleteTime', '3', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a80a0-d1ec-6fb7-0091-5c2bb79801e9', 'BaseService.OrderSetting.OrderAutoSignTime', '12', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a80a0-d1f5-6097-886e-ff6229b25e92', 'BaseService.OrderSetting.OrderAutoEvalTime', '3', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a80a0-d1fb-6563-1834-b692e0a56aba', 'BaseService.OrderSetting.RemindOrderTime', '10', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a838f-e9a1-ebfb-e7f1-894ff632bd91', 'BaseService.GoodsSetting.GoodsStocksWarn', '10', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a838f-e9af-80dc-8fc5-8bfc37c7b38c', 'BaseService.PointsSetting.PointDiscountedProportion', '100', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a838f-e9b0-ba38-0159-4d58a3f879df', 'BaseService.PointsSetting.OrdersPointProportion', '10', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a838f-e9b1-a00e-53f5-b85889a01d45', 'BaseService.PointsSetting.OrdersRewardProportion', '1', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a838f-e9b4-4e85-3e26-236fd87decbd', 'BaseService.PointsSetting.SignRandomMin', '1', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a838f-e9b4-8271-7bde-143b84ff2850', 'BaseService.PointsSetting.SignMostPoint', '1', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a838f-e9b5-2c95-2856-56ee66c01545', 'BaseService.PointsSetting.SignRandomMax', '10', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a838f-e9b5-6283-8cfb-bc07d01bb5ad', 'BaseService.CashSetting.TocashMoneyLow', '0.01', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a838f-e9b6-1068-0440-775ea5155c03', 'BaseService.CashSetting.TocashMoneyRate', '0', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a8391-e410-bb08-fef8-de6f34cdbf42', 'BaseService.OrderSetting.ReshipName', 'Rex', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a8391-e411-3d5f-a904-d210d9fa9db4', 'BaseService.OrderSetting.ReshipMobile', '18888888888', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a8391-e412-2f38-9cb0-f72475f7e9b5', 'BaseService.OrderSetting.ReshipAddress', 'xxx街道xxx栋xxx室', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a8391-e412-4efe-8d9b-210e4eb8d250', 'BaseService.OrderSetting.ReshipAreaId', '440304', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a839d-0771-8e11-aef1-21eb090d62b0', 'BaseService.OrderSetting.ReshipCoordinate', '22.52593071194191,114.0594833813227', 'T', NULL);
INSERT INTO `sys_settings` VALUES ('3a1a83a8-5b58-20df-7ca9-d7bb7c9a32ad', 'BaseService.UsersSetting.ShopMobile', '18888888888', 'T', NULL);

-- ----------------------------
-- Table structure for sys_tenantconnectionstrings
-- ----------------------------
DROP TABLE IF EXISTS `sys_tenantconnectionstrings`;
CREATE TABLE `sys_tenantconnectionstrings`  (
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(1024) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`TenantId`, `Name`) USING BTREE,
  CONSTRAINT `FK_Sys_TenantConnectionStrings_Sys_Tenants_TenantId` FOREIGN KEY (`TenantId`) REFERENCES `sys_tenants` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_tenantconnectionstrings
-- ----------------------------

-- ----------------------------
-- Table structure for sys_tenants
-- ----------------------------
DROP TABLE IF EXISTS `sys_tenants`;
CREATE TABLE `sys_tenants`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EntityVersion` int NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  `NormalizedName` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Sys_Tenants_Name`(`Name`) USING BTREE,
  INDEX `IX_Sys_Tenants_NormalizedName`(`NormalizedName`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_tenants
-- ----------------------------

-- ----------------------------
-- Table structure for sys_userbalances
-- ----------------------------
DROP TABLE IF EXISTS `sys_userbalances`;
CREATE TABLE `sys_userbalances`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Type` int NOT NULL,
  `Money` decimal(65, 30) NOT NULL,
  `Balance` decimal(65, 30) NOT NULL,
  `SourceId` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
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
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Sys_UserBalances_UserId`(`UserId`) USING BTREE,
  CONSTRAINT `FK_Sys_UserBalances_Sys_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `sys_users` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_userbalances
-- ----------------------------
INSERT INTO `sys_userbalances` VALUES ('3a19bc3e-b06d-b5bd-f974-deb22f08f432', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 3, 0.010000000000000000000000000000, 660.010000000000000000000000000000, '3a19bc30-bbe8-d090-9f4c-58d52020b36a', '充值了0.01元', '{}', '622b23d47173435083754170a556bd3b', '2025-05-07 17:26:58.413912', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a19bc57-994d-d79d-bdbe-7a62a72899fc', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 3, 0.010000000000000000000000000000, 660.020000000000000000000000000000, '3a19bc53-9325-8858-9498-cd1812131ca7', '充值了0.01元', '{}', '83bafec5d8ee4319a767cc9a58efdff8', '2025-05-07 17:54:10.894028', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a19bc5a-155b-a6c8-6548-4bb6ba70b7f8', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 3, 0.010000000000000000000000000000, 660.030000000000000000000000000000, '3a19bc1e-f5e8-ebd0-b083-97c131920a3e', '充值了0.01元', '{}', '1a2d82bc5e954b24839e9875ee0aa592', '2025-05-07 17:56:53.723798', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a19bc60-bd2b-61a1-82d2-8017ace87f66', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 3, 0.010000000000000000000000000000, 660.040000000000000000000000000000, '3a19bc41-1d9b-d932-2609-363b3f8e7c69', '充值了0.01元', '{}', 'f8a979ad338d492bb09002864f7b8997', '2025-05-07 18:04:09.900019', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a19bc63-e5bb-8c32-8da7-471f3f54607e', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 3, 0.010000000000000000000000000000, 660.050000000000000000000000000000, '3a19bc28-dc3b-d5dc-f105-af675ae894ba', '充值了0.01元', '{}', 'be8b912a4b494724a8feea056180edfe', '2025-05-07 18:07:36.891318', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a19d16e-cb6c-3d86-ce81-ff573958011b', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -485.000000000000000000000000000000, 175.050000000000000000000000000000, '3a19d16e-bee2-ead9-f577-5490ed1f81cb', '消费了485.00元', '{}', 'd06560f0b8a742389eaca993ea93740a', '2025-05-11 20:11:32.588950', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a19d17f-676e-0abb-fd16-9ed9259cfb15', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -20.000000000000000000000000000000, 155.050000000000000000000000000000, '3a19d17d-f3d4-b700-80b3-3d45ea7f16a6', '消费了20.00元', '{}', '4688176eb8654365b74afa184273289f', '2025-05-11 20:29:41.102828', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a19d186-80a0-f171-0fbc-c62eb2cb6ea9', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -20.000000000000000000000000000000, 135.050000000000000000000000000000, '3a19d186-4a13-6e48-ed0f-3186af421674', '消费了20.00元', '{}', 'cf20cc3ecc314e3eb5cf6a81bd9258c7', '2025-05-11 20:37:26.304448', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a19d189-f3ea-d31c-a45c-c0a2aed5440b', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -30.000000000000000000000000000000, 105.050000000000000000000000000000, '3a19d189-6d17-b115-a320-65277a921fb1', '消费了30.00元', '{}', '47326fc43c224c9aac9d8e1dbd5c8665', '2025-05-11 20:41:12.426605', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a19d18d-29b1-3d0e-ab9e-4e72826df117', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -20.000000000000000000000000000000, 85.050000000000000000000000000000, '3a19d18d-0569-f6aa-6e0d-de276198ee38', '消费了20.00元', '{}', '94d6333355fc448088e277ba2e7604eb', '2025-05-11 20:44:42.801184', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a19d19b-83b3-ff9d-3241-97eca23282ee', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -30.000000000000000000000000000000, 55.050000000000000000000000000000, '3a19d19b-7428-35c8-ea14-4ec98652d671', '消费了30.00元', '{}', 'e7f8e18b077b4f63bbbdd1ad880875cf', '2025-05-11 21:00:23.347157', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a19f5d4-a2d2-880d-a695-fdeeb2437310', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -20.000000000000000000000000000000, 35.050000000000000000000000000000, '3a19f5d4-9339-74e7-0dbd-109b051e8eae', '消费了20.00元', '{}', '2b7290722da545c88fa7528ed4b13320', '2025-05-18 21:49:06.642959', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a1b7d50-ca40-2791-da0c-c1287d2bc286', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -485.000000000000000000000000000000, 515.000000000000000000000000000000, '3a1b7d50-c851-01dc-bd27-8392986eaefc', '消费了485.00元', '{}', 'c14e35ced5c7461ba9cb1c4383c29886', '2025-08-02 22:16:14.657114', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a1b7da9-c9db-c142-5f89-7334197febf4', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -16.000000000000000000000000000000, 499.000000000000000000000000000000, '3a1b7da9-c97f-f26f-5580-0aa1b48f2af6', '消费了16.00元', '{}', '8a9985d541134aee85f7d78f49c63159', '2025-08-02 23:53:27.260092', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a1b7dac-62b9-f6e7-cac5-2c333feaf5c6', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -16.000000000000000000000000000000, 483.000000000000000000000000000000, '3a1b7dac-6288-ee26-9350-f155dba703e3', '消费了16.00元', '{}', '41bafbf1bd9e49c6ae82ee4ef0a04653', '2025-08-02 23:56:17.465454', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a1bb0f6-0cee-3458-ae9f-1f00521e7a1e', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -30.000000000000000000000000000000, 453.000000000000000000000000000000, '3a1bb0f6-0745-a0b0-7347-931918f7c052', '消费了30.00元', '{}', '55b38aea24f14f58a0a1360c2ea21a51', '2025-08-12 22:57:23.186256', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a1bb0fd-933d-5d5b-d556-6a809279324b', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -20.000000000000000000000000000000, 433.000000000000000000000000000000, '3a1bb0fd-92f2-47ac-e392-cff07df547d6', '消费了20.00元', '{}', '5c257f5909834e74a9d5ff01f004df25', '2025-08-12 23:05:36.317616', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a1bb398-caf0-b868-5857-4424bbcf5d46', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -583.000000000000000000000000000000, 9417.000000000000000000000000000000, '3a1bb398-c6de-42df-5e06-bc57b2ea1010', '消费了583.00元', '{}', '75c62e05db054b608df4f5191600412d', '2025-08-13 11:14:23.090953', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a1bb41d-82b4-38b4-504b-508f6b8063ce', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 2, 20.000000000000000000000000000000, 9437.000000000000000000000000000000, '3a1b7dac-6288-ee26-9350-f155dba703e3', '收到了退款20.00元', '{}', '1a201ccb1a59453a92d807b5b971acf6', '2025-08-13 13:39:20.885397', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a1bb425-adf3-971e-844c-9b15d2d58c4d', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 2, 20.000000000000000000000000000000, 9457.000000000000000000000000000000, '3a19d186-4a13-6e48-ed0f-3186af421674', '收到了退款20.00元', '{}', '8c74b5c8012b4f60b6d9bbf2690ec297', '2025-08-13 13:48:16.244122', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a1bc06b-bd14-6730-b7f2-60a6af282f47', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -1200.000000000000000000000000000000, 8257.000000000000000000000000000000, '3a1bc06b-b71c-0741-4a6a-61b457c3a078', '消费了1200.00元', '{}', '5260517b9b9b42de9206a75ba0562268', '2025-08-15 23:00:14.231293', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a1bd50f-732a-d331-a95d-c6d0b8e773b2', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -194.000000000000000000000000000000, 8063.000000000000000000000000000000, '3a1bd50f-6b6c-0b44-421a-7a79e7e9a0a7', '消费了194.00元', '{}', '0bb2797826144fa29f5d5327d01afc41', '2025-08-19 23:11:27.534096', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a1be9c0-3848-892e-a637-c2ec45beb8d6', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -112.000000000000000000000000000000, 9888.000000000000000000000000000000, '3a1be9c0-35c8-5914-37cd-6eeaacfa2c64', '消费了112.00元', '{}', '625e77d475424e268e7e8df6dab26543', '2025-08-23 23:36:56.649151', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userbalances` VALUES ('3a1be9c1-72af-f6aa-bec7-fc321d03681c', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 1, -112.000000000000000000000000000000, 9776.000000000000000000000000000000, '3a1be9c1-7285-0d23-8a8f-d60cc76eb2fc', '消费了112.00元', '{}', 'f494eb2687414961985c354775f0b8a8', '2025-08-23 23:38:17.136078', NULL, NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for sys_userclaims
-- ----------------------------
DROP TABLE IF EXISTS `sys_userclaims`;
CREATE TABLE `sys_userclaims`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `ClaimType` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimValue` varchar(1024) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Sys_UserClaims_UserId`(`UserId`) USING BTREE,
  CONSTRAINT `FK_Sys_UserClaims_Sys_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `sys_users` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_userclaims
-- ----------------------------

-- ----------------------------
-- Table structure for sys_userdelegations
-- ----------------------------
DROP TABLE IF EXISTS `sys_userdelegations`;
CREATE TABLE `sys_userdelegations`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `SourceUserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TargetUserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `StartTime` datetime(6) NOT NULL,
  `EndTime` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_userdelegations
-- ----------------------------

-- ----------------------------
-- Table structure for sys_usergrade
-- ----------------------------
DROP TABLE IF EXISTS `sys_usergrade`;
CREATE TABLE `sys_usergrade`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Title` varchar(60) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsDefault` tinyint(1) NOT NULL,
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
  UNIQUE INDEX `IX_Sys_UserGrade_Title`(`Title`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_usergrade
-- ----------------------------
INSERT INTO `sys_usergrade` VALUES ('3a0d4971-3e6d-9c9e-7027-a8e776cef762', NULL, '普通用户', 1, '{}', '0b03461417fe441aafcaa6d496343807', '2023-08-27 21:52:24.305933', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-02-06 13:49:48.883549', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_usergrade` VALUES ('3a0d4972-872c-9198-d9f6-d4e3bba35f5e', NULL, '尊享VIP', 0, '{}', 'f1ae72f4e71c440c8d77c79822cbec96', '2023-08-27 21:53:48.351387', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2024-02-06 13:49:48.883961', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL);
INSERT INTO `sys_usergrade` VALUES ('3a0d4972-e6b6-93ef-45eb-bbca4ae40270', NULL, '豪华SVIP', 0, '{}', 'bc84ea0704f643a98d58ac24be85a5fa', '2023-08-27 21:54:12.796873', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_usergrade` VALUES ('3a1b7c1d-6279-853f-2f84-121d2d568e2f', NULL, 'TEST', 0, '{}', '49e54232a56743d88a5ec459f82119f7', '2025-08-02 16:40:28.539958', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 16:40:37.903439', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 1, '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 16:40:37.903348');

-- ----------------------------
-- Table structure for sys_userlogins
-- ----------------------------
DROP TABLE IF EXISTS `sys_userlogins`;
CREATE TABLE `sys_userlogins`  (
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `LoginProvider` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `ProviderKey` varchar(196) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`UserId`, `LoginProvider`) USING BTREE,
  INDEX `IX_Sys_UserLogins_LoginProvider_ProviderKey`(`LoginProvider`, `ProviderKey`) USING BTREE,
  CONSTRAINT `FK_Sys_UserLogins_Sys_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `sys_users` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_userlogins
-- ----------------------------

-- ----------------------------
-- Table structure for sys_userorganizationunits
-- ----------------------------
DROP TABLE IF EXISTS `sys_userorganizationunits`;
CREATE TABLE `sys_userorganizationunits`  (
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `OrganizationUnitId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`OrganizationUnitId`, `UserId`) USING BTREE,
  INDEX `IX_Sys_UserOrganizationUnits_UserId_OrganizationUnitId`(`UserId`, `OrganizationUnitId`) USING BTREE,
  CONSTRAINT `FK_Sys_UserOrganizationUnits_Sys_OrganizationUnits_Organization~` FOREIGN KEY (`OrganizationUnitId`) REFERENCES `sys_organizationunits` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_Sys_UserOrganizationUnits_Sys_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `sys_users` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_userorganizationunits
-- ----------------------------
INSERT INTO `sys_userorganizationunits` VALUES ('3a0d3a6b-4305-7729-d4b8-213b3dbd8454', '3a0d3a68-ed07-48dd-178d-0f4d07d34c62', NULL, '2023-08-24 23:53:57.677503', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534');

-- ----------------------------
-- Table structure for sys_userpointlogs
-- ----------------------------
DROP TABLE IF EXISTS `sys_userpointlogs`;
CREATE TABLE `sys_userpointlogs`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Type` int NOT NULL,
  `Num` int NOT NULL,
  `Balance` int NOT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
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
-- Records of sys_userpointlogs
-- ----------------------------
INSERT INTO `sys_userpointlogs` VALUES ('3a18e054-a5e7-f5fa-cb90-b09adc6e4a21', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 3, -1000, 0, '订单【117429204681540】使用积分。', '{}', '4c6a23d308e14a1abecaff25d54595f4', '2025-03-26 00:34:29.992843', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userpointlogs` VALUES ('3a1a8065-4a62-2e6a-75bb-93d2cf464f8d', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 8, 1000, 1500, '取消订单【117429204681540】积分返还：+1000。', '{}', '4a986485d1b14cec92e39c94ad1e4e9e', '2025-06-14 19:34:42.554485', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userpointlogs` VALUES ('3a1a83b4-78a1-e44b-c127-b22d84279e90', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 2, 1226, 2726, '订单【117149196297237】积分奖励：0。', '{}', '68180432674a4e48b2f05e0beccd5aff', '2025-06-15 11:00:03.376987', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userpointlogs` VALUES ('3a1a83b4-78d1-8431-e1d5-f6eb0fb700c8', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 2, 760, 3486, '订单【117169973610143】积分奖励：0。', '{}', '8c84d0aadd2b45189c75a4960d02369e', '2025-06-15 11:00:03.409958', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userpointlogs` VALUES ('3a1ac3a6-16f1-4c5e-d317-3af46d73283c', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 2, 20, 3506, '订单【117438788003013】积分奖励：0。', '{}', '1dc04236b69748699afc706f4aab6406', '2025-06-27 21:00:02.695288', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userpointlogs` VALUES ('3a1b7d41-ee4a-33e7-14de-3aae021bea10', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 2, 595, 4101, '订单【117505972539610】积分奖励：0。', '{}', '32806c606fe845e2aef5bebd78f27ead', '2025-08-02 22:00:00.934164', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userpointlogs` VALUES ('3a1b7daf-c84c-88cf-0fb0-519ee368591d', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 2, 20, 4121, '订单【117475761280734】积分奖励：0。', '{}', '0be67d20b6a94127a064e8d20b2d9759', '2025-08-03 00:00:00.077567', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userpointlogs` VALUES ('3a1be9bf-e916-3c05-43ff-0087282f2fc2', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 3, -1860, 3140, '订单【117559633286796】使用积分：-1860。', '{}', 'a7d48de99a7d43f7bc5140829124e481', '2025-08-23 23:36:36.490726', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_userpointlogs` VALUES ('3a1be9c1-55bf-54a9-6457-c53f907e7e96', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 3, -1240, 1900, '订单【117559634812452】使用积分：-1240。', '{}', 'e0476fe27bf44b23b5979e0a6decf43b', '2025-08-23 23:38:09.730312', NULL, NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for sys_userroles
-- ----------------------------
DROP TABLE IF EXISTS `sys_userroles`;
CREATE TABLE `sys_userroles`  (
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `RoleId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`UserId`, `RoleId`) USING BTREE,
  INDEX `IX_Sys_UserRoles_RoleId_UserId`(`RoleId`, `UserId`) USING BTREE,
  CONSTRAINT `FK_Sys_UserRoles_Sys_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `sys_roles` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_Sys_UserRoles_Sys_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `sys_users` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_userroles
-- ----------------------------
INSERT INTO `sys_userroles` VALUES ('3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '3a0d00cb-a40f-d2ec-e486-855f99d4e5ba', NULL);
INSERT INTO `sys_userroles` VALUES ('3a0d3a6b-4305-7729-d4b8-213b3dbd8454', '3a0d3a6b-eefa-ac07-4411-44f8f9f4bc86', NULL);

-- ----------------------------
-- Table structure for sys_users
-- ----------------------------
DROP TABLE IF EXISTS `sys_users`;
CREATE TABLE `sys_users`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Surname` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL DEFAULT 0,
  `PasswordHash` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `SecurityStamp` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsExternal` tinyint(1) NOT NULL DEFAULT 0,
  `PhoneNumber` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL DEFAULT 0,
  `IsActive` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL DEFAULT 0,
  `LockoutEnd` datetime(6) NULL DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL DEFAULT 0,
  `AccessFailedCount` int NOT NULL DEFAULT 0,
  `ShouldChangePasswordOnNextLogin` tinyint(1) NOT NULL,
  `EntityVersion` int NOT NULL,
  `LastPasswordChangeTime` datetime(6) NULL DEFAULT NULL,
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
  `Balance` decimal(10, 2) NULL DEFAULT NULL,
  `Discriminator` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT '',
  `ParentId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Point` int NULL DEFAULT NULL,
  `Gender` smallint NULL DEFAULT NULL,
  `GradeId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `BirthDate` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Sys_Users_Email`(`Email`) USING BTREE,
  INDEX `IX_Sys_Users_NormalizedEmail`(`NormalizedEmail`) USING BTREE,
  INDEX `IX_Sys_Users_NormalizedUserName`(`NormalizedUserName`) USING BTREE,
  INDEX `IX_Sys_Users_UserName`(`UserName`) USING BTREE,
  INDEX `IX_Sys_Users_GradeId`(`GradeId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_users
-- ----------------------------
INSERT INTO `sys_users` VALUES ('3a0d00cb-a16d-8c75-c7a7-27cd029a9534', NULL, 'admin', 'ADMIN', '系统管理员', NULL, 'admin@abp.io', 'ADMIN@ABP.IO', 0, 'AQAAAAIAAYagAAAAEDEzwcKj9sGwHXomfeIlK1vqSKvGuZLjKmUGyLLZYbQOxnRRl+ysLHaZFeboLEBS7w==', 'J3IBBNDZCLST26NTSNKM7Z5B67V37KCO', 0, NULL, 0, 1, 0, NULL, 1, 0, 0, 1, '2023-08-13 11:18:51.151851', '{}', 'a9e13e0a4d4e4065be87f5fda6965993', '2023-08-13 19:18:51.338288', NULL, '2023-08-13 19:18:51.960615', NULL, 0, NULL, NULL, NULL, NULL, '', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `sys_users` VALUES ('3a0d3a6b-4305-7729-d4b8-213b3dbd8454', NULL, 'Rex', 'REX', '雷克斯', '', 'rex@gmail.com', 'REX@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAEK69dT2XG/MS/j29Z0x3noYOUijmrKBIpOHbm9QnCAlo0rkGgiGAyh05g+roF/3qCw==', 'FAE4PSD565XHWXQ6PJXJY3YW2VGYXDKG', 0, '18888888888', 0, 1, 0, NULL, 1, 0, 0, 40, '2025-08-02 15:38:25.145712', '{}', '7154e4c464524739af4ab463aec1b9f5', '2023-08-24 23:51:34.020182', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', '2025-08-02 23:38:25.150950', '3a0d00cb-a16d-8c75-c7a7-27cd029a9534', 0, NULL, NULL, NULL, NULL, '', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `sys_users` VALUES ('3a118d4a-e735-1846-9ae3-c4ced1efcd87', NULL, 'MP-WEIXIN2015', 'MP-WEIXIN2015', 'Rex', NULL, '18302687365@139.com', '18302687365@139.COM', 0, 'AQAAAAIAAYagAAAAEEP/zDzxrdTYad51nsmJsHYtGHCdc2ljnHSCUTpVUYg83gtOdoZaeDx+QsE8h7oihg==', 'I23YN66Z2QRGNYPYNVOEN64NLCUTHWD7', 0, '18302687365', 0, 1, 0, NULL, 1, 0, 0, 55, '2025-08-02 15:38:35.850218', '{}', 'fe9e6601708b409da79ed041a119b171', '2024-03-26 22:15:53.378875', NULL, '2025-08-23 23:38:17.140472', NULL, 0, NULL, NULL, 'https://thirdwx.qlogo.cn/mmopen/vi_32/POgEwh4mIHO4nibH0KlMECNjjGxQUq24ZEaGT4poC6icRiccVGKSyXwibcPq4BWmiaIGuG1icwxaQX6grC9VemZoJ8rg/132', 9776.00, 'SysUser', NULL, 1900, 0, '3a0d4972-872c-9198-d9f6-d4e3bba35f5e', '2002-02-01 00:00:00.000000');

-- ----------------------------
-- Table structure for sys_userships
-- ----------------------------
DROP TABLE IF EXISTS `sys_userships`;
CREATE TABLE `sys_userships`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `AreaId` bigint NOT NULL,
  `Address` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Mobile` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsDefault` tinyint(1) NOT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  `DisplayArea` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_userships
-- ----------------------------
INSERT INTO `sys_userships` VALUES ('3a11ede4-3ad2-31bb-8ec1-dd43defdf6eb', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 130202, '测试地址零壹', 'Tom', '18888888888', 1, '{}', '697e7c7d69654c12a4746b18e84f28ea', '2024-04-14 08:26:52.532613', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', '2024-04-14 08:56:30.564982', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 0, NULL, NULL, '河北省 - 唐山市 - 路南区');
INSERT INTO `sys_userships` VALUES ('3a11edfa-79a6-7305-c2c1-35d3cd2b31fe', NULL, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 140105, '测试地址零贰', 'Mo', '19999999998', 0, '{}', 'db38a3ca426f43d3a16395d2bf01a07e', '2024-04-14 08:51:10.375930', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', '2025-05-08 18:49:07.694456', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 0, NULL, NULL, '山西省 - 太原市 - 小店区');

-- ----------------------------
-- Table structure for sys_usertokens
-- ----------------------------
DROP TABLE IF EXISTS `sys_usertokens`;
CREATE TABLE `sys_usertokens`  (
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `LoginProvider` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`UserId`, `LoginProvider`, `Name`) USING BTREE,
  CONSTRAINT `FK_Sys_UserTokens_Sys_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `sys_users` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_usertokens
-- ----------------------------

-- ----------------------------
-- Table structure for sys_userwechat
-- ----------------------------
DROP TABLE IF EXISTS `sys_userwechat`;
CREATE TABLE `sys_userwechat`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TenantId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Type` int NULL DEFAULT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
  `OpenId` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SessionKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UnionId` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Avatar` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `NickName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Gender` smallint NULL DEFAULT NULL,
  `City` varchar(80) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Province` varchar(80) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Country` varchar(80) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CountryCode` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Mobile` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  `BirthDate` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Sys_UserWeChat_UserId`(`UserId`) USING BTREE,
  CONSTRAINT `FK_Sys_UserWeChat_Sys_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `sys_users` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_userwechat
-- ----------------------------
INSERT INTO `sys_userwechat` VALUES ('3a118d4a-ef23-7ad8-22fb-4c7288a84014', NULL, 2, '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 'oVnfz5FU1nKJ3dqpwjUh8CgpJJVA', '66cC43JXxCPIxAxTFA/k2g==', '', 'https://thirdwx.qlogo.cn/mmopen/vi_32/POgEwh4mIHO4nibH0KlMECNjjGxQUq24ZEaGT4poC6icRiccVGKSyXwibcPq4BWmiaIGuG1icwxaQX6grC9VemZoJ8rg/132', 'Rex', 0, NULL, NULL, NULL, '86', '18302687365', '', '13ee000c65d04d3eaa9626dd7f935e37', '2024-03-26 22:15:53.379138', NULL, '2025-08-02 23:41:17.853198', '3a118d4a-e735-1846-9ae3-c4ced1efcd87', 0, NULL, NULL, '2002-02-01 00:00:00.000000');

-- ----------------------------
-- Procedure structure for POMELO_AFTER_ADD_PRIMARY_KEY
-- ----------------------------
DROP PROCEDURE IF EXISTS `POMELO_AFTER_ADD_PRIMARY_KEY`;
delimiter ;;
CREATE PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255), IN `COLUMN_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID INT(11);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
			AND `COLUMN_TYPE` LIKE '%int%'
			AND `COLUMN_KEY` = 'PRI';
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL AUTO_INCREMENT;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for POMELO_BEFORE_DROP_PRIMARY_KEY
-- ----------------------------
DROP PROCEDURE IF EXISTS `POMELO_BEFORE_DROP_PRIMARY_KEY`;
delimiter ;;
CREATE PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID TINYINT(1);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `Extra` = 'auto_increment'
			AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
