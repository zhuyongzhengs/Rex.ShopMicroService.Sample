// promotionCondition
declare type RowPromotionConditionType = {
	id: string;
	tenantId: string;

	promotionId: string;
	code: string;
	parameters: string;

	concurrencyStamp: string;
	creationTime: string;
};

// promotionResult
declare type RowPromotionResultType = {
	id: string;
	tenantId: string;

	promotionId: string;
	code: string;
	parameters: string;

	concurrencyStamp: string;
	creationTime: string;
};

// promotion
declare type RowPromotionType = {
	id: string;
	tenantId: string;

	name: string;
	type: number;
	weight: number;
	parameters: string;
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
	promotionConditions: RowPromotionConditionType[];
	promotionResults: RowPromotionResultType[];

	concurrencyStamp: string;
	creationTime: string;
};

interface PromotionTableType extends TableType {
	data: RowPromotionType[];
}

declare interface PromotionState {
	tableData: PromotionTableType;
}
