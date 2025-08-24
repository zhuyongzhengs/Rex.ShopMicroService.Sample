<template>
  <div class="personal layout-pd">
    <el-row :gutter="15">
      <el-col :span="10">
        <el-card shadow="hover" header="个人信息">
          <div class="personal-user">
            <div class="personal-user-left">
              <el-upload
                class="personal-user-left-upload"
                action="https://jsonplaceholder.typicode.com/posts/"
                multiple
                :limit="1"
              >
                <img
                  src="/src/assets/logo-mini.png"
                />
              </el-upload>
            </div>
            <div class="personal-user-right">
              <el-row>
                <el-col :span="24" class="personal-title mb18"
                  >{{ currentTime }}，admin ！
                </el-col>
                <el-col :span="24">
                  <el-row>
                    <el-col :xs="24" :sm="8" class="personal-item mb6">
                      <div class="personal-item-label">昵称：</div>
                      <div class="personal-item-value">小柒</div>
                    </el-col>
                    <el-col :xs="24" :sm="16" class="personal-item mb6">
                      <div class="personal-item-label">身份：</div>
                      <div class="personal-item-value">超级管理</div>
                    </el-col>
                  </el-row>
                </el-col>
                <el-col :span="24">
                  <el-row>
                    <el-col :xs="24" :sm="10" class="personal-item mb6">
                      <div class="personal-item-label">登录IP：</div>
                      <div class="personal-item-value">192.168.x.x</div>
                    </el-col>
                    <el-col :xs="24" :sm="14" class="personal-item mb6">
                      <div class="personal-item-label">登录时间：</div>
                      <div class="personal-item-value">2021-02-05 18:47:26</div>
                    </el-col>
                  </el-row>
                </el-col>
              </el-row>
            </div>
          </div>
        </el-card>
      </el-col>
      <el-col :span="14">
        <el-card shadow="hover" class="personal-edit" header="更新信息">
          <div class="personal-edit-title">基本信息</div>
          <el-form
            :model="state.personalForm"
            size="default"
            label-width="40px"
            class="mt35 mb35"
          >
            <el-row :gutter="35">
              <el-col :xs="24" :sm="12" :md="8" :lg="8" :xl="4" class="mb20">
                <el-form-item label="昵称">
                  <el-input
                    v-model="state.personalForm.name"
                    placeholder="请输入昵称"
                    clearable
                  ></el-input>
                </el-form-item>
              </el-col>
              <el-col :xs="24" :sm="12" :md="8" :lg="8" :xl="4" class="mb20">
                <el-form-item label="邮箱">
                  <el-input
                    v-model="state.personalForm.email"
                    placeholder="请输入邮箱"
                    clearable
                  ></el-input>
                </el-form-item>
              </el-col>
              <el-col :xs="24" :sm="12" :md="8" :lg="8" :xl="4" class="mb20">
                <el-form-item label="手机">
                  <el-input
                    v-model="state.personalForm.phone"
                    placeholder="请输入手机"
                    clearable
                  ></el-input>
                </el-form-item>
              </el-col>
              <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                <el-form-item>
                  <el-button type="primary" @click="updateUser">
                    <el-icon>
                      <ele-Position />
                    </el-icon>
                    更新个人信息
                  </el-button>
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script setup lang="ts" name="personal">
import { reactive, computed } from "vue";
import { formatAxis } from "/@/utils/formatTime";
import { ElMessage } from "element-plus";

// 定义变量内容
const state = reactive<PersonalState>({
  personalForm: {
    name: "",
    email: "",
    autograph: "",
    occupation: "",
    phone: "",
    sex: "",
  },
});

// 更新用户信息
const updateUser = () => {
  ElMessage.warning("别点了，还没有实现~");
};

// 当前时间提示语
const currentTime = computed(() => {
  return formatAxis(new Date());
});
</script>

<style scoped lang="scss">
@import "../../theme/mixins/index.scss";
.personal {
  .personal-user {
    min-height: 175px;
    display: flex;
    align-items: center;
    .personal-user-left {
      width: 100px;
      height: 130px;
      border-radius: 3px;
      :deep(.el-upload) {
        height: 100%;
      }
      .personal-user-left-upload {
        img {
          width: 100%;
          height: 100%;
          border-radius: 3px;
        }
        &:hover {
          img {
            animation: logoAnimation 0.3s ease-in-out;
          }
        }
      }
    }
    .personal-user-right {
      flex: 1;
      padding: 0 15px;
      .personal-title {
        font-size: 18px;
        @include text-ellipsis(1);
      }
      .personal-item {
        display: flex;
        align-items: center;
        font-size: 13px;
        .personal-item-label {
          color: var(--el-text-color-secondary);
          @include text-ellipsis(1);
        }
        .personal-item-value {
          @include text-ellipsis(1);
        }
      }
    }
  }
  .personal-info {
    .personal-info-more {
      float: right;
      color: var(--el-text-color-secondary);
      font-size: 13px;
      &:hover {
        color: var(--el-color-primary);
        cursor: pointer;
      }
    }
    .personal-info-box {
      height: 130px;
      overflow: hidden;
      .personal-info-ul {
        list-style: none;
        .personal-info-li {
          font-size: 13px;
          padding-bottom: 10px;
          .personal-info-li-title {
            display: inline-block;
            @include text-ellipsis(1);
            color: var(--el-text-color-secondary);
            text-decoration: none;
          }
          & a:hover {
            color: var(--el-color-primary);
            cursor: pointer;
          }
        }
      }
    }
  }
  .personal-recommend-row {
    .personal-recommend-col {
      .personal-recommend {
        position: relative;
        height: 100px;
        border-radius: 3px;
        overflow: hidden;
        cursor: pointer;
        &:hover {
          i {
            right: 0px !important;
            bottom: 0px !important;
            transition: all ease 0.3s;
          }
        }
        i {
          position: absolute;
          right: -10px;
          bottom: -10px;
          font-size: 70px;
          transform: rotate(-30deg);
          transition: all ease 0.3s;
        }
        .personal-recommend-auto {
          padding: 15px;
          position: absolute;
          left: 0;
          top: 5%;
          color: var(--next-color-white);
          .personal-recommend-msg {
            font-size: 12px;
            margin-top: 10px;
          }
        }
      }
    }
  }
  .personal-edit {
    min-height: 175px;
    .personal-edit-title {
      position: relative;
      padding-left: 10px;
      color: var(--el-text-color-regular);
      &::after {
        content: "";
        width: 2px;
        height: 10px;
        position: absolute;
        left: 0;
        top: 50%;
        transform: translateY(-50%);
        background: var(--el-color-primary);
      }
    }
    .personal-edit-safe-box {
      border-bottom: 1px solid var(--el-border-color-light, #ebeef5);
      padding: 15px 0;
      .personal-edit-safe-item {
        width: 100%;
        display: flex;
        align-items: center;
        justify-content: space-between;
        .personal-edit-safe-item-left {
          flex: 1;
          overflow: hidden;
          .personal-edit-safe-item-left-label {
            color: var(--el-text-color-regular);
            margin-bottom: 5px;
          }
          .personal-edit-safe-item-left-value {
            color: var(--el-text-color-secondary);
            @include text-ellipsis(1);
            margin-right: 15px;
          }
        }
      }
      &:last-of-type {
        padding-bottom: 0;
        border-bottom: none;
      }
    }
  }
}
</style>
