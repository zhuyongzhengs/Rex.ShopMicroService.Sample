// Coupon
declare type RowCouponType = {
	id: string;
	tenantId: string;

	couponCode: string;
	promotionId: string;
	isUsed: boolean;
	userId: string;
	userName: string;
	usedId: string;
	remark: string;
	startTime: string;
	endTime: string;

	concurrencyStamp: string;
	creationTime: string;
	lastModificationTime: string;
};

interface CouponTableType extends TableType {
	data: RowCouponType[];
}

declare interface CouponState {
	tableData: CouponTableType;
}
