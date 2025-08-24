// goodGrade
declare type RowGoodGradeType = {
	id: string | null;
	tenantId: string | null;

	goodId: string | null;
	gradeId: string;
	gradeTitle: string;
	gradePrice: number | null;
	
	concurrencyStamp: string | null;
	creationTime: string | null;
};

interface GoodGradeTableType extends TableType {
	data: RowGoodGradeType[];
}

declare interface GoodGradeState {
	tableData: GoodGradeTableType;
}
