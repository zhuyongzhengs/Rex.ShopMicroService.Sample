// 促销
declare type PromotionType = {
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
	promotionConditions: PromotionConditionType[] | null;
	promotionResults: PromotionResultType[] | null;

	concurrencyStamp: string;
	creationTime: string;
};

// 促销条件
declare type PromotionConditionType = {
	id: string;
	tenantId: string;

	promotionId: string;
	code: string;
	parameters: string;

	concurrencyStamp: string;
	creationTime: string;
}

// 促销结果
declare type PromotionResultType = {
	id: string;
	tenantId: string;

	promotionId: string;
	code: string;
	parameters: string;

	concurrencyStamp: string;
	creationTime: string;
}