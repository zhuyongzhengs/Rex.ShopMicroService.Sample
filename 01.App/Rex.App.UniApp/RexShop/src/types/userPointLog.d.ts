// 用户积分记录
declare type UserPointLogType = {
	id: string;

	userId: string;
	type: number;
	typeDisplay: string;
	num: number;
	balance: number;
	remark: string;

	creationTime: string;
};

interface UserPointLogTableType extends TableType {
	data: UserPointLogType[];
}

declare interface UserPointLogTypeState {
	tableData: UserPointLogTableType;
}