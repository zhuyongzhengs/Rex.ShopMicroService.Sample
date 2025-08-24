// formItem
declare type RowFormItemType = {
	id: string;
	tenantId: string;
	
	name: string;
	type: number;
	validationType: number;
	value: string;
	defaultValue: string;
	formId: string;
	form: RowFormType?;
	required: boolean;
	sort: number;

	concurrencyStamp: string;
	creationTime: string;
};

// form
declare type RowFormType = {
	id: string;
	tenantId: string;
	
	name: string;
	type: number;
	sort: number;
	images: string;
	videoPath: string;
	description: string;
	headType: number;
	headTypeValue: string;
	headTypeVideo: string;
	buttonName: string;
	buttonColor: string;
	isLogin: boolean;
	times: number;
	qrcode: string;
	returnMsg: string;
	endDateTime: string;
	formItems: RowFormItemType[];

	concurrencyStamp: string;
	creationTime: string;
	lastModificationTime: string;
};

interface FormTableType extends TableType {
	data: RowFormType[];
}

declare interface FormState {
	tableData: FormTableType;
}
