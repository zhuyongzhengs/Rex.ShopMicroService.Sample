// pinTuanGood
declare type RowPinTuanGoodType = {
	id: string;
	tenantId: string;
	
	ruleId: string;
	pinTuanRule: RowPinTuanRuleType;
	goodId: string;

	/* start：Ext */
	goodName: string;
	goodImage: string;
	goodImages: string;
	goodPrice: number;
	/* end：Ext */

	concurrencyStamp: string;
	creationTime: string;
};

// pinTuanRule
declare type RowPinTuanRuleType = {
	id: string;
	tenantId: string;
	
	name: string;
	startTime: string;
	endTime: string;
	peopleNumber: number;
	significantInterval: number;
	discountAmount: number;
	maxNums: number;
	maxGoodsNums: number;
	sort: number;
	isStatusOpen: boolean;
	pinTuanGoods: RowPinTuanGoodType[];

	concurrencyStamp: string;
	creationTime: string;
};

interface PinTuanRuleTableType extends TableType {
	data: RowPinTuanRuleType[];
}

declare interface PinTuanRuleState {
	tableData: PinTuanRuleTableType;
}
