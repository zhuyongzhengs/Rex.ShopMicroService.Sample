// 用户余额
declare type UserBalanceType = {
	id: string;

	userId: string;
	type: number;
	typeDisplay: string;
	money: string;
	balance: number;
	sourceId: string;
	memo: string;

	creationTime: string;
};

interface UserBalanceTableType extends TableType {
	data: UserBalanceType[];
}

declare interface UserBalanceTypeState {
	tableData: UserBalanceTableType;
}