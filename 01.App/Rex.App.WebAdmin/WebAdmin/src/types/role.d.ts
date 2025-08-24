// role
declare type RowRoleType = {
	id: string;
	name: string;
	isDefault: boolean;
	isStatic: boolean;
	isPublic: boolean;
	concurrencyStamp: string;
}

interface SysRoleTableType extends TableType {
	data: RowRoleType[];
}

declare interface SysRoleState {
	tableData: SysRoleTableType;
}

