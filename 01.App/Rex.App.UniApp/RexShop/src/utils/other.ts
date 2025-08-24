import _ from "lodash";
import { http } from "@/utils/http";
import { useUserLoginStore } from "@/stores/userLogin/index";

/**
 * 删除对象为空的属性
 * @param obj 对象
 */
export function removeNullObject(obj: any) {
	if(!obj) return null;
	let newObj = _.cloneDeep(obj);
	for (const key in newObj) {
		if (newObj.hasOwnProperty(key) && newObj[key] === null) {
		  delete newObj[key];
		}
	}
	return newObj;
}


/**
 * 获取随机编码
 * @param prefix 前缀
 */
export function getCode(prefix: string = "") {
	var timestamp = new Date().getTime();
	var randomnVal = Math.floor(Math.random() * 30 + timestamp + 1);
	return prefix + randomnVal;
}

/**
 * 获取GUID空值
 */
export function getGuidEmpty() {
	return "00000000-0000-0000-0000-000000000000";
}

/**
 * 验证电子邮箱格式
 */
export function isEmail(value: string) {
    return /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/.test(value)
}

/**
 * 验证手机格式
 */
export function isMobile(value: string) {
    return /^1[23456789]\d{9}$/.test(value)
}

/**
 * 根据结果类型返回相应的参数数据
 * @param code 编码
 * @param json Json参数
 */
export function getResultMsg(code: string, json: string) {
	var msg = "";
	var param = JSON.parse(json);
	if (!param) return msg;
	switch (code)
	{
		case "GOODS_REDUCE":
			msg = "减" + param["money"] + "元 ";
			break;
		case "GOODS_DISCOUNT":
			msg = "打" + param["discount"] + "折 ";
			break;
		case "GOODS_ONE_PRICE":
			msg = "一口价" + param["money"] + "元 ";
			break;
		case "ORDER_REDUCE":
			msg = "订单减" + param["money"] + "元 ";
			break;
		case "ORDER_DISCOUNT":
			msg = "订单打" + param["discount"] + "折 ";
			break;
		case "GOODS_HALF_PRICE":
			msg = "第" + param["num"] + "件,优惠" + param["money"] + "元";
			break;
	}
	return msg;
}

/**
 * 根据条件类型返回相应的参数数据
 * @param code 编码
 * @param json Json参数
 */
export function getConditionMsg(code: string, json: string) {
	var msg = "";
	var param = JSON.parse(json);
	if (!param) return msg;
	switch (code)
    {
        case "GOODS_ALL":
            msg = "购买任意商品 ";
            break;
        case "GOODS_IDS":
            msg = "购买指定商品 ";
            break;
        case "GOODS_CATS":
            msg = "购买指定分类商品 ";
            break;
        case "GOODS_BRANDS":
            msg = "购买指定品牌商品 ";
            break;
        case "ORDER_FULL":
            msg = "购买订单满" + param["money"] + "元 ";
            break;
        case "USER_GRADE":
            msg = "用户符合指定等级";
            break;
    }
	return msg;
}

/**
 * 获取商品规格信息
 * @param spesDesc 商品规格
 * @param selectedProduct 选中的商品
 */
export function getWithSpecDesc(spesDesc: string, selectedProduct: ProductType | null) {
	let specNames = new Array();
	specDescParse(spesDesc, (spec: any) => {
		specNames.push({
			name: spec.specName,
			values: [],
		});
	});
	let uniqSpecNames = _.uniqWith(specNames, _.isEqual);
	specDescParse(spesDesc, (spec: any) => {
		let specItem = uniqSpecNames.find((p: any) => p.name == spec.specName);
		if (specItem) {
			let isSelected = false;
			if(selectedProduct) {
				let selProducts = selectedProduct.spesDesc.split(',');
				for (let i = 0; i < selProducts.length; i++) {
				  const sName = selProducts[i].split(':')[0];
				  const sValue = selProducts[i].split(':')[1];
				  if(specItem.name == sName && spec.specValueVal == sValue) {
					isSelected = true;
					break;
				  }
				}
			}
			specItem.values.push({
				specValueId: spec.specValueId,
				specValueVal: spec.specValueVal,
				isSelected
			});
		}
	});
	return uniqSpecNames;
}

/**
 * 商品规格解析
 * @param spesDesc 规格字符串
 * @param callback [回调]解析结果
 */
export function specDescParse(spesDesc: string, callback: Function) {
	const specs = spesDesc.split("|");
	for (let i = 0; i < specs.length; i++) {
	  const specDescs = specs[i].split(".");
	  const specValueId = specDescs[0];
	  const specValueVal = specDescs[1].split(":")[1];
	  const specName = specDescs[1].split(":")[0];
	  callback({
		specValueId,
		specValueVal,
		specName,
	  });
	}
}

/**
 * 订单类型枚举值
 * @param orderType 订单类型
 */
export function formatOrderType(orderType: number | undefined) {
	var orderTypeVal = "";
	switch (orderType)
    {
        case 1:
            orderTypeVal = "商品订单"; // 普通
            break;
        case 2:
            orderTypeVal = "拼团";
            break;
        case 3:
            orderTypeVal = "团购";
            break;
        case 4:
            orderTypeVal = "秒杀";
            break;
        case 5:
            orderTypeVal = "积分兑换";
            break;
        case 6:
            orderTypeVal = "砍价";
            break;
		case 7:
            orderTypeVal = "赠品";
            break;
		case 8:
			orderTypeVal = "接龙";
			break;
    }
	return orderTypeVal;
}

/**
 * 支付状态枚举值
 * @param payStatus 支付状态
 */
export function formatPayStatus(payStatus: number | undefined) {
	var payStatusVal = "";
	switch (payStatus)
    {
        case 1:
            payStatusVal = "未付款"; 
            break;
        case 2:
            payStatusVal = "已付款";
            break;
        case 3:
            payStatusVal = "部分付款";
            break;
        case 4:
            payStatusVal = "部分退款";
            break;
        case 5:
            payStatusVal = "已付款";
            break;
    }
	return payStatusVal;
}

/**
 * 发货状态枚举值
 * @param shipStatus 发货状态
 */
export function formatShipStatus(shipStatus: number | undefined) {
	var shipStatusVal = "";
	switch (shipStatus)
    {
        case 1:
            shipStatusVal = "未发货"; 
            break;
        case 2:
            shipStatusVal = "部分发货";
            break;
        case 3:
            shipStatusVal = "已收货";
            break;
        case 4:
            shipStatusVal = "部分退货";
            break;
        case 5:
            shipStatusVal = "已退货";
            break;
    }
	return shipStatusVal;
}

/**
 * 订单状态枚举值
 * @param status 订单状态
 */
export function formatStatus(status: number | undefined) {
	var statusVal = "";
	switch (status)
    {
        case 1:
            statusVal = "订单正常"; 
            break;
        case 2:
            statusVal = "订单完成";
            break;
        case 3:
            statusVal = "订单取消";
            break;
    }
	return statusVal;
}

/**
 * 支付方式枚举值
 * @param paymentCode 支付编码
 */
export function formatPaymentType(paymentCode: string | undefined) {
	var paymentName = "";
	switch (paymentCode)
    {
        case "WechatPay":
            paymentName = "微信支付"; 
            break;
        case "AliPay":
            paymentName = "支付宝支付";
            break;
        case "Offline":
            paymentName = "线下支付";
            break;
		case "BalancePay":
			paymentName = "余额支付";
			break;
    }
	return paymentName;
}

/**
 * 订单状态
 * @param uOrder 用户订单
 */
export function formatOrderStatus(uOrder: UserOrderType | undefined) {
	let orderStatusName = "";
	switch (uOrder?.status) {
	  case 1:
		if (uOrder.payStatus === 1) {
		  	orderStatusName = "待付款";
		} else if (uOrder.payStatus >= 2 && uOrder.shipStatus == 1) {
		  	orderStatusName = "待发货";
		} else if (uOrder.payStatus >= 2 && uOrder.shipStatus == 2) {
		  	orderStatusName = "部分发货";
		} else if (uOrder.payStatus >= 2 && uOrder.shipStatus >= 3 && uOrder.confirmStatus == 1) {
		  	orderStatusName = "已发货";
		} else if (
			uOrder.payStatus >= 2 &&
			uOrder.shipStatus >= 3 &&
			uOrder.confirmStatus >= 2 &&
			uOrder.isComment == false
		) {
		  orderStatusName = "待评价";
		} else if (
			uOrder.payStatus >= 2 &&
			uOrder.shipStatus >= 3 &&
			uOrder.confirmStatus >= 2 &&
		  	uOrder.isComment == true
		) {
		  	orderStatusName = "已评价";
		}
		break;
	  case 2:
			orderStatusName = "已完成";
		break;
	  case 3:
			orderStatusName = "已取消";
		break;
	}
	return orderStatusName;
};

/**
 * 同步微信信息
 */
export function syncWeChatInfo() {
	uni.getUserProfile({
		desc: "获取你的昵称、头像、地区及性别",
		success: (e) => {
		  const uInfo: any = e.userInfo;
		  http<boolean>({
			method: "PUT",
			url: "/api/base/common/sys-user-information",
			data: {
			  avatar: uInfo.avatarUrl,
			  name: uInfo.nickName,
			  gender: uInfo.gender,
			},
		  })
			.then((result) => {
			  if (!result) return;
			  useUserLoginStore().refreshCurrentSysUser();
			  uni.showToast({
				title: "同步(微信)信息成功。",
				icon: "none"
			  });
			})
			.catch((err: any) => {
			  console.error("同步(微信)信息出错：", err);
			});
		},
		fail: (res) => {
		  //拒绝授权
		  console.warn("您拒绝了请求。", res);
		},
	  });
}

/**
 * 获取应用配置信息
 * @param node 节点
 */
export async function getApplicationConfigurationAsync(node: string | undefined) {
	let appConfig = await http<any>({
		method: "GET",
		url: "/api/abp/application-configuration"
	});
	if (!appConfig) return null;
	if (node) return appConfig[node];
	return appConfig;
}

/**
 * 获取上传文件配置
 * @returns 上传配置
 */
export function uploadFileConfig(endPath: string = 'upload')  {
	let accessToken = useUserLoginStore().userAuthToken?.access_token;
	let uploadConfig: UploadFileType = {
		action: `${import.meta.env.VITE_API_URL}/api/base/blobStoring/${endPath}`,
		headers: {
			Authorization: `Bearer ${accessToken}`
		}
	}
	return uploadConfig;
}

/**
 * 统一批量导出
 * @method removeNullObject 删除对象为空的属性
 * @method getCode 获取编码
 * @method getGuidEmpty 获取GUID空值
 * @method getResultMsg 获取结果信息
 * @method getConditionMsg 获取条件信息
 * @method specDescParse 商品规格解析
 * @method getWithSpecDesc 获取商品规格信息
 * @method isEmail 验证是否为电子邮件格式
 * @method isMobile 验证是否为手机号码格式
 * @method formatOrderType 订单类型枚举值
 * @method formatPayStatus 支付状态枚举值
 * @method formatShipStatus 发货状态枚举值
 * @method formatStatus 订单状态枚举值
 * @method formatPaymentType 支付方式枚举值
 * @method formatOrderStatus 订单状态
 * @method syncWeChatInfo 同步微信信息
 * @method getApplicationConfigurationAsync 获取应用配置信息
 * @method uploadFileConfig 获取文件配置信息
 */
const other = {
	removeNullObject: (obj: any) => {
		return removeNullObject(obj);
	},
	getCode: (prefix: string = "") => {
		return getCode(prefix);
	},
	getGuidEmpty: () => {
		return getGuidEmpty();
	},
	getResultMsg: (code: string, json: string) => {
		return getResultMsg(code, json);
	},
	getConditionMsg: (code: string, json: string) => {
		return getConditionMsg(code, json);
	},
	specDescParse: (spesDesc: string, callback: Function) => {
		return specDescParse(spesDesc, callback);
	},
	getWithSpecDesc: (spesDesc: string, selectedProduct: ProductType | null) => {
		return getWithSpecDesc(spesDesc, selectedProduct);
	},
	isEmail: (val: string) => {
		return isEmail(val);
	},
	isMobile: (val: string) => {
		return isMobile(val);
	},
	formatOrderType: (orderType: number) => {
		return formatOrderType(orderType);
	},
	formatPayStatus: (payStatus: number) => {
		return formatPayStatus(payStatus);
	},
	formatShipStatus: (shipStatus: number) => {
		return formatShipStatus(shipStatus);
	},
	formatStatus: (status: number) => {
		return formatStatus(status);
	},
	formatPaymentType: (paymentCode: string) => {
		return formatPaymentType(paymentCode);
	},
	formatOrderStatus: (uOrder: UserOrderType) => {
		return formatOrderStatus(uOrder);
	},
	syncWeChatInfo: () => {
		syncWeChatInfo();
	},
	getApplicationConfigurationAsync: async (node: string | undefined) => {
		return await getApplicationConfigurationAsync(node);
	},
	uploadFileConfig: (endPath: string = 'upload')  => {
		return uploadFileConfig(endPath);
	}
};

// 统一批量导出
export default other;
