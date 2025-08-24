// tenant
declare type RowTenantType = {
	id: string;
	name: string;
	concurrencyStamp: string;
};

interface SysTenantTableType extends TableType {
	data: RowTenantType[];
}

declare interface SysTenantState {
	tableData: SysTenantTableType;
}
