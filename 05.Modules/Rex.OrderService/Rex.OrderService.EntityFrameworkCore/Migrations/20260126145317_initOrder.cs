using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rex.OrderService.Migrations
{
    /// <inheritdoc />
    public partial class initOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Od_Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nums = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Od_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Od_Logistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    LogiCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LogiName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ImgUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Url = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: true),
                    Sort = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Od_Logistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Od_OrderLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Msg = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Data = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Od_OrderLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Od_Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    No = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    GoodAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PayedAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PayStatus = table.Column<int>(type: "integer", nullable: false),
                    ShipStatus = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    OrderType = table.Column<int>(type: "integer", nullable: false),
                    ReceiptType = table.Column<int>(type: "integer", nullable: false),
                    PaymentCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    PaymentTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LogisticsId = table.Column<Guid>(type: "uuid", nullable: false),
                    LogisticsName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CostFreight = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConfirmStatus = table.Column<int>(type: "integer", nullable: false),
                    ConfirmTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: true),
                    ShipAreaId = table.Column<long>(type: "bigint", nullable: false),
                    DisplayArea = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    ShipAddress = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: false),
                    ShipName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShipMobile = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false),
                    Point = table.Column<int>(type: "integer", nullable: false),
                    PointMoney = table.Column<decimal>(type: "numeric", nullable: false),
                    GradeDiscountAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    SeckillDiscountAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderDiscountAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    GoodsDiscountAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    CouponDiscountAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Coupon = table.Column<string>(type: "text", nullable: true),
                    PromotionList = table.Column<string>(type: "text", nullable: true),
                    Memo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Mark = table.Column<string>(type: "character varying(510)", maxLength: 510, nullable: true),
                    Source = table.Column<int>(type: "integer", nullable: false),
                    IsComment = table.Column<bool>(type: "boolean", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Od_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Od_Ships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsCashOnDelivery = table.Column<bool>(type: "boolean", nullable: false),
                    FirstUnit = table.Column<int>(type: "integer", nullable: false),
                    ContinueUnit = table.Column<int>(type: "integer", nullable: false),
                    IsdefaultAreaFee = table.Column<bool>(type: "boolean", nullable: false),
                    AreaType = table.Column<int>(type: "integer", nullable: false),
                    FirstunitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    ContinueunitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Exp = table.Column<string>(type: "text", nullable: true),
                    LogiName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LogiCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    Sort = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsfreePostage = table.Column<bool>(type: "boolean", nullable: false),
                    AreaFee = table.Column<string>(type: "text", nullable: true),
                    GoodsMoney = table.Column<decimal>(type: "numeric", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Od_Ships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Od_BillAftersales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    No = table.Column<string>(type: "text", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    RefundAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Mark = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Od_BillAftersales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Od_BillAftersales_Od_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Od_Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Od_BillDeliverys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    No = table.Column<string>(type: "text", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    LogiCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LogiName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LogiNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LogiInformation = table.Column<string>(type: "text", nullable: true),
                    LogiStatus = table.Column<bool>(type: "boolean", nullable: false),
                    ShipAreaId = table.Column<long>(type: "bigint", nullable: false),
                    DisplayArea = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ShipAddress = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ShipName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ShipMobile = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Memo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ConfirmTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Od_BillDeliverys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Od_BillDeliverys_Od_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Od_Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Od_OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    GoodId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Sn = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Bn = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CostPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    MktPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Nums = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PromotionAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PromotionList = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false),
                    SendNums = table.Column<int>(type: "integer", nullable: false),
                    Addon = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Od_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Od_OrderItems_Od_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Od_Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Od_BillAftersalesImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    AftersalesId = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Sort = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Od_BillAftersalesImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Od_BillAftersalesImages_Od_BillAftersales_AftersalesId",
                        column: x => x.AftersalesId,
                        principalTable: "Od_BillAftersales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Od_BillAftersalesItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    AftersalesId = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: false),
                    OrderItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    GoodId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Sn = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Bn = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Nums = table.Column<int>(type: "integer", nullable: false),
                    Addon = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Od_BillAftersalesItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Od_BillAftersalesItems_Od_BillAftersales_AftersalesId",
                        column: x => x.AftersalesId,
                        principalTable: "Od_BillAftersales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Od_BillReships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    No = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: false),
                    AftersalesId = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LogiCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LogiNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Memo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Od_BillReships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Od_BillReships_Od_BillAftersales_AftersalesId",
                        column: x => x.AftersalesId,
                        principalTable: "Od_BillAftersales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Od_BillReships_Od_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Od_Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Od_BillDeliveryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uuid", nullable: false),
                    GoodId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Sn = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Bn = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Nums = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false),
                    Addon = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Od_BillDeliveryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Od_BillDeliveryItems_Od_BillDeliverys_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Od_BillDeliverys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Od_BillReshipItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReshipId = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: false),
                    OrderItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    GoodId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Sn = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Bn = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Nums = table.Column<int>(type: "integer", nullable: false),
                    Addon = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Od_BillReshipItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Od_BillReshipItems_Od_BillReships_ReshipId",
                        column: x => x.ReshipId,
                        principalTable: "Od_BillReships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Od_BillAftersales_OrderId",
                table: "Od_BillAftersales",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Od_BillAftersalesImages_AftersalesId",
                table: "Od_BillAftersalesImages",
                column: "AftersalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Od_BillAftersalesItems_AftersalesId",
                table: "Od_BillAftersalesItems",
                column: "AftersalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Od_BillDeliveryItems_DeliveryId",
                table: "Od_BillDeliveryItems",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Od_BillDeliverys_OrderId",
                table: "Od_BillDeliverys",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Od_BillReshipItems_ReshipId",
                table: "Od_BillReshipItems",
                column: "ReshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Od_BillReships_AftersalesId",
                table: "Od_BillReships",
                column: "AftersalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Od_BillReships_OrderId",
                table: "Od_BillReships",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Od_OrderItems_OrderId",
                table: "Od_OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Od_Orders_No",
                table: "Od_Orders",
                column: "No",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Od_BillAftersalesImages");

            migrationBuilder.DropTable(
                name: "Od_BillAftersalesItems");

            migrationBuilder.DropTable(
                name: "Od_BillDeliveryItems");

            migrationBuilder.DropTable(
                name: "Od_BillReshipItems");

            migrationBuilder.DropTable(
                name: "Od_Carts");

            migrationBuilder.DropTable(
                name: "Od_Logistics");

            migrationBuilder.DropTable(
                name: "Od_OrderItems");

            migrationBuilder.DropTable(
                name: "Od_OrderLogs");

            migrationBuilder.DropTable(
                name: "Od_Ships");

            migrationBuilder.DropTable(
                name: "Od_BillDeliverys");

            migrationBuilder.DropTable(
                name: "Od_BillReships");

            migrationBuilder.DropTable(
                name: "Od_BillAftersales");

            migrationBuilder.DropTable(
                name: "Od_Orders");
        }
    }
}
