import axios, { AxiosInstance, AxiosResponse } from 'axios';
import { ElMessage, ElMessageBox } from 'element-plus';
import { Session } from '/@/utils/storage';
import qs from 'qs';

const baseUrl: string = import.meta.env.VITE_API_URL;

// 配置新建一个 axios 实例
const service: AxiosInstance = axios.create({
	baseURL: baseUrl,
	timeout: 50000,
	headers: { 'Content-Type': 'application/json' },
	paramsSerializer: {
		serialize(params) {
			return qs.stringify(params, { allowDots: true });
		}
	}
});

// 添加请求拦截器
service.interceptors.request.use(
	(config) => {
		// 在发送请求之前做些什么 token
		if (Session.get('token')) {
			config.headers!['Authorization'] = `Bearer ${Session.get('token')}`;
		}
		// 国际化（先写死）
		config.headers!['Accept-Language'] = 'zh-Hans';
		return config;
	},
	(error) => {
		// 对请求错误做些什么
		return Promise.reject(error);
	}
);

// 添加响应拦截器
service.interceptors.response.use(
	(response: AxiosResponse) => {
		if(response.status >= 200 && response.status < 300) {
			if(!response.data) return true;
		} else {
			return Promise.reject(new Error(response.message || 'Error'))
		}
		return response.data;
	},
	(error) => {
		const errStatus = error.response?.status;
		if (error.message.indexOf('timeout') != -1) {
			ElMessage.error('网络超时');
		} else if (error.message == 'Network Error') {
			ElMessage.error('网络连接错误');
		} else if(errStatus === 401) {
			/*
			setTimeout(() => {
				Session.clear();
				window.location.href = '/';
			}, 3 * 1000);
			*/
			Session.clear();
			ElMessageBox.alert('401：你还未登录系统(或已失效)，请重新登录！', '提示');
		} else {			
			let errMsg = error.response.data?.error?.message;
			if(!errMsg) {
				errMsg = error.response.statusText;
			}
			errMsg = errMsg || '系统繁忙，请稍后重试！';
			ElMessage.error(`${errStatus}：${errMsg}`);
		}
		return Promise.reject(error);
	}
);

// 导出 axios 实例
export default service;
