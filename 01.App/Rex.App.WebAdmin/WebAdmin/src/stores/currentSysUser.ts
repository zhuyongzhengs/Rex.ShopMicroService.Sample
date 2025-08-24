import { defineStore } from 'pinia';
import { Session } from '/@/utils/storage';
import { useLoginApi } from '/@/api/login/index';

/**
 * 用户信息
 * @methods setCurrentSysUsers 设置用户信息
 */
export const useCurrentSysUser = defineStore('currentSysUser', {
    state: (): CurrentSysUsersState => ({
        currentSysUsers: {
            tenantId: '',
            userId: '',
            userName: '',
            phoneNumber: '',
            phoneNumberVerified: false,
            photo: '',
            time: 0,
            roles: [],
            authBtnList: [],
        },
    }),
    actions: {
        async setCurrentSysUsers() {
            // 存储用户信息到浏览器缓存
            if (Session.get('currentSysUser')) {
                this.currentSysUsers = Session.get('currentSysUser');
            } else {
                const currentSysUsers = <CurrentSysUsers>await this.getApiCurrentSysUser();
                this.currentSysUsers = currentSysUsers;
            }
        },
        // 获取用户信息
        async getApiCurrentSysUser() {
            return new Promise((resolve) => {
                setTimeout(async () => {
                    // 获取用户配置信息
                    const appConfig = await <any>this.findApplicationConfig();

                    // 权限
                    let authBtnList: Array<string> = [];
                    for (var role in appConfig.auth.grantedPolicies) {
                        if (appConfig.auth.grantedPolicies[role] != true) {
                            continue;
                        }
                        authBtnList.push(role);
                    }

                    // 用户信息
                    const currentSysUsers = {
                        tenantId: appConfig.currentUser.tenantId,
                        userId: appConfig.currentUser.id,
                        userName: appConfig.currentUser.userName,
                        phoneNumber: appConfig.currentUser.phoneNumber,
                        phoneNumberVerified: appConfig.currentUser.phoneNumberVerified,
                        photo: '/src/assets/logo-mini.png',
                        time: new Date().getTime(),
                        roles: appConfig.currentUser.roles,
                        authBtnList
                    }

                    // 存储
                    Session.set('currentSysUser', currentSysUsers);
                    resolve(currentSysUsers);
                }, 50);
            });
        },
        // 查询用户配置信息
        async findApplicationConfig() {
            return new Promise((resolve, reject) => {
                useLoginApi().getApplicationConfig().then((result) => {
                    resolve(result);
                }).catch((err: any) => {
                    reject(err);
                });
            });
        }
    }
});