// userOrganizationUnit
declare type RowUserOrganizationUnitType<T = any> = {
	id: string;
	tenantId: string;
	userId: string;
	userName: string;
	organizationUnitId: string;
	creationTime: T;
};

interface SysUserOrganizationUnitTableType extends TableType {
	data: RowUserOrganizationUnitType[];
}

declare interface SysUserOrganizationUnitState {
	tableData: SysUserOrganizationUnitTableType;
}

// organizationUnitRole
declare type RowOrganizationUnitRoleType<T = any> = {
	id: string;
	tenantId: string;
	roleId: string;
	organizationUnitId: string;
	creationTime: T;
};

interface SysOrganizationUnitRoleTableType extends TableType {
	data: RowOrganizationUnitRoleType[];
}

declare interface SysOrganizationUnitRoleState {
	tableData: SysOrganizationUnitRoleTableType;
}