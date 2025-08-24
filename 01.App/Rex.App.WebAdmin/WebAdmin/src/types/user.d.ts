// user
declare type RowUserType<T = any> = {
	id: string;
	tenantId: string;
	userName: string;
	name: string;
	surname: string;
	email: string;
	emailConfirmed: boolean;
	phoneNumber: string;
	phoneNumberConfirmed: boolean;
	isActive: boolean;
	lockoutEnabled: boolean;
	lockoutEnd: T;
	concurrencyStamp: string;
	entityVersion: number;
};

interface SysUserTableType extends TableType {
	data: RowUserType[];
}

declare interface SysUserState {
	tableData: SysUserTableType;
}
