// sysUser
declare type RowSysUserType = {
	id: string;
	tenantId: string;
	userName: string;
	name: string;
	email: string;
	gender: number;
	balance: number;
	point: number;
	gradeId: string;
	grade: RowUserGradeType;
	isActive: boolean;
	parentId: string;
	creationTime: string;
	concurrencyStamp: string;
};

interface MemberSysUserTableType extends TableType {
	data: RowSysUserType[];
}

declare interface MemberSysUserState {
	tableData: MemberSysUserTableType;
}
