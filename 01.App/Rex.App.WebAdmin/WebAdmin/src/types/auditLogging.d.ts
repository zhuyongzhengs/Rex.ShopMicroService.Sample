// auditLogAction
declare type RowAuditLogActionType<T = any> = {
	id: string;
	tenantId: string;
	auditLogId: string;
	serviceName: string;
	methodName: string;
	parameters: string;
	executionTime: T;
	executionDuration: number;
}

// entityPropertyChange
declare type RowEntityPropertyChangeType = {
	id: string;
	tenantId: string;
	entityChangeId: string;
	newValue: string;
	originalValue: string;
	propertyName: string;
	propertyTypeFullName: string;
}

// entityChange
declare type RowEntityChangeType<T = any> = {
	id: string;
	tenantId: string;
	auditLogId: string;
	changeTime: T;
	changeType: number;
	entityTenantId: string;
	entityId: string;
	entityTypeFullName: string;
	propertyChanges: RowEntityPropertyChangeType[];
}

// auditLogging
declare type RowAuditLoggingType<T = any> = {
	id: string;
	applicationName: string;
	userId: string;
	userName: string;
	tenantId: string;
	tenantName: string;
	impersonatorUserId: string;
	impersonatorTenantId: string;
	executionTime: T;
	executionDuration: number;
	clientIpAddress: string;
	clientName: string;
	clientId: string;
	correlationId: string;
	browserInfo: string;
	httpMethod: string;
	url: string;
	exceptions: string;
	comments: string;
	httpStatusCode: string;
	entityChanges: RowEntityChangeType[];
	actions: RowAuditLogActionType[];
};

interface SysAuditLoggingTableType extends TableType {
	data: RowAuditLoggingType[];
}

declare interface SysAuditLoggingState {
	tableData: SysAuditLoggingTableType;
}
