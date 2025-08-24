// userGrade
declare type RowUserGradeType = {
	id: string;
	tenantId: string;
	title: string;
	isDefault: boolean;
	concurrencyStamp: string;
};

interface MemberUserGradeTableType extends TableType {
	data: RowUserGradeType[];
}

declare interface MemberUserGradeState {
	tableData: MemberUserGradeTableType;
}
