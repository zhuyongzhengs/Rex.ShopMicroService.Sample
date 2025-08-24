// table 数据格式公共类型
declare interface TableType<T = any> {
	total: number;
	loading: string;
	currentPage: number;
	param: {
		skipCount: number;
		maxResultCount: number;
		[key: string]: T;
	};
}

// table 数据查询结果格式公共类型
declare interface TableResultType<T = any> {
	items: T;
	totalCount: number;
}

// 枚举值类型
declare type EnumValueType = {
	title: string;
	value: string;
	description: string;
};

// 常用Key/Value类型
declare type CommonKeyValueType = {
	key: string;
	value: string;
	description: string;
};

// 微信登录表单
declare type WechatLoginFormType = {
	client_id: string;
	grant_type: string;
	scope: string;
	openid: string;
	session_Key: string;
	unionid: string;
	code: string;
	invitecode: string;
};

// 微信返回数据
declare type Code2SessionResultType = {
	openid: string;
	session_key: string;
	unionid: string;
	errorCode: number;
	errorMessage: string;
};

// 登录结果类型
declare type LoginResultType = {
	access_token: string;
	expires_in: number;
	token_type: string;
};

// 用户等级
declare type GradeType = {
	id: string;
	title: string;
};

// 用户信息类型
declare type CurrentSysUserType = {
	id: string;
	tenantId: string;
	userName: string;
	name: string;
	nickName: string;
	email: string;
	phoneNumber: string;
	avatar: string;
	gender: number | null;
	balance: number;
	point: number;
	grade: GradeType;
	parentId: string;
	birthDate: string;
};

// 文章选择类型
declare type SelectArticleType = {
	id: string;
	title: string;
};

// 选择文件类型
declare type SelFileType = {
	name: string;
	size: number;
	thumb: string;
	type: string;
	url: string;
}

// 文件列表类型
declare type FileListType = SelFileType & {
	status: string;
	message: string;
}

// 文件读取类型
declare type FileReadType = {
	index: number;
	file: SelFileType[];
}

// 上传文件类型
declare type UploadFileType = {
	action: string;
	headers: { 
		[key: string]: string 
	};
};

// 用户通知数量类型
declare type UserNoticeNumberType = {
	totalGoodBrowsing: number;
	totalCoupon: number;
	totalGoodCollection: number;
	totalBillAftersale: number;
	totalPendingPayment: number;
	totalPendingShipment: number;
	totalPendingDelivery: number;
	totalPendingEvaluate: number;
};