// securityLog
declare type RowSecurityLogType<T = any> = {
	id: string;
	applicationName: string;
	userId: string;
	userName: string;
	tenantId: string;
	tenantName: string;
	action: string;
	clientIpAddress: string;
	clientName: string;
	clientId: string;
	browserInfo: string;
	creationTime: string;
};

interface SysSecurityLogTableType extends TableType {
	data: RowSecurityLogType[];
}

declare interface SysSecurityLogState {
	tableData: SysSecurityLogTableType;
}
