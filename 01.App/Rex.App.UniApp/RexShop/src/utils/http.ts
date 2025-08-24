import { useUserLoginStore } from "@/stores/userLogin/index";

// 请求基地址
const baseURL = import.meta.env.VITE_API_URL;
 
// 1.拦截器配置
const httpInterceptor = {
  // 拦截前触发
  invoke(options: UniApp.RequestOptions) {
    // 非 http 开头需拼接地址
    if (!options.url.startsWith('http')) {
      options.url = baseURL + options.url;
    }

    // 请求超时
    options.timeout = 20 * 1000

    // 添加小程序端请求头标识 => 1：PC端、2：H5端、3：微信小程序端、4：支付宝小程序端、5：APP端
    let sourceClient = 2;
    // #ifdef MP-WEIXIN
    sourceClient = 3;
    // #endif
    // #ifdef MP-ALIPAY
    sourceClient = 4;
    // #endif
    // #ifdef APP
    sourceClient = 5;
    // #endif
    options.header = {
        'source-client': sourceClient,
      ...options.header,
    }

    // 添加 token 请求头标识
    let accessToken = useUserLoginStore().userAuthToken?.access_token;
    if (accessToken) {
      options.header.Authorization = `Bearer ${accessToken}`;
    }
  },
}
// 拦截 request 请求
uni.addInterceptor('request', httpInterceptor);
// 拦截 uploadFile 文件上传
uni.addInterceptor('uploadFile', httpInterceptor);

/**
 * 请求函数
 * @param  UniApp.RequestOptions
 * @returns Promise<T>
 */
// 2.添加类型，支持泛型
export const http = <T>(options: UniApp.RequestOptions) => {
    // 1. 返回 Promise 对象
    return new Promise<T>((resolve, reject) => {
        uni.request({
            ...options,
            // 响应成功
            success(res) {
                if (res.statusCode >= 200 && res.statusCode < 300) {
                    if(res.statusCode == 204) res.data = Object(true);
                    resolve(res.data as T);
                } else {
                    const errData = res.data as any;
                    let errMsg = errData?.error?.message;
                    if(!errMsg) errMsg = res.errMsg;
                    if(res.statusCode === 401 || errMsg == "Unauthorized") {
                        useUserLoginStore().currentSysUser = undefined;
                        useUserLoginStore().hasLogin = false;
                        uni.showToast({
                            icon: 'none',
                            title: "登录已失效，请重新登录！",
                            duration: 3500
                        });
                    } else if (options.method !== "GET") {
                        // 其他错误 -> 根据后端错误信息轻提示
                        uni.showToast({
                            icon: 'none',
                            title: errMsg,
                            duration: 4 * 1000
                        });
                    }
                    console.error(res.statusCode, errMsg);
                    reject(res);
                }
                uni.hideLoading();
            },
            // 响应失败
            fail(err) {
                uni.showToast({
                    icon: 'none',
                    title: '网络错误，换个网络试试！',
                });
                
                reject(err);
                uni.hideLoading();
            },
        });
    });
}