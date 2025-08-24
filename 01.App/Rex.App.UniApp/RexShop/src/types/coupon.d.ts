// 用户优惠劵
declare type UserCouponType = {
	id: string;
	tenantId: string;

	couponCode: string;
	promotionId: string;
	promotion: PromotionType | null;
	isUsed: boolean;
	userId: string;
	usedId: string | null;
	remark: string;
	startTime: string;
	endTime: string;

	creationTime: string;
	lastModificationTime: string;
}

// 优惠劵
declare type CouponType = {
	id: string;
	tenantId: string;
	
	name: string;
	type: number;
	weight: number;
	parameters: string | null;
	maxNums: number;
	maxGoodNums: number;
	maxRecevieNums: number;
	startTime: string;
	endTime: string;
	isExclusive: boolean;
	isAutoReceive: boolean;
	isEnable: boolean;
	effectiveDays: number;
	effectiveHours: number;
	effectiveHours: number;
	promotionConditions: CouponConditionType[] | null;
	promotionResults: CouponResultType[] | null;

	concurrencyStamp: string;
	creationTime: string;
};

// 优惠条件
declare type CouponConditionType = {
	id: string;
	tenantId: string;

	promotionId: string;
	code: string;
	parameters: string;

	concurrencyStamp: string;
	creationTime: string;
}

// 优惠结果
declare type CouponResultType = {
	id: string;
	tenantId: string;

	promotionId: string;
	code: string;
	parameters: string;

	concurrencyStamp: string;
	creationTime: string;
}