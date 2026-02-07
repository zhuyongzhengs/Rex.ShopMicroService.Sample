<template>
  <div class="member-sys-user-dialog-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="769px"
    >
      <el-form
        ref="memberSysUserDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="账户名称" prop="userName">
              <el-input
                v-model="state.ruleForm.userName"
                @input="onVerifyCnAndSpace($event)"
                placeholder="请输入账户名称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="用户昵称" prop="name">
              <el-input
                v-model="state.ruleForm.name"
                placeholder="请输入用户昵称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="邮箱" prop="email">
              <el-input
                v-model="state.ruleForm.email"
                placeholder="请输入邮箱"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="用户等级" prop="gradeId">
              <el-select
                v-model="state.ruleForm.gradeId"
                placeholder="请选择"
                clearable
                class="w100"
              >
                <el-option
                  v-for="(grade, gIndex) in state.gradeData"
                  :label="grade.title"
                  :value="grade.id"
                  :key="gIndex"
                ></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="性别" prop="gender">
              <el-select
                v-model="state.ruleForm.gender"
                placeholder="请选择"
                clearable
                class="w100"
              >
                <el-option label="男" :value="1"></el-option>
                <el-option label="女" :value="2"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <!--
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="手机号" prop="phoneNumber">
              <el-input
                v-model="state.ruleForm.phoneNumber"
                placeholder="请输入手机号"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          -->
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="是否激活" prop="isActive">
              <el-switch
                v-model="state.ruleForm.isActive"
                inline-prompt
                active-text="已激活"
                inactive-text="未激活"
              ></el-switch>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(memberSysUserDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="systemSysUserDialog">
import { nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage, ElMessageBox } from "element-plus";
import _ from "lodash";
import { useUserGradeApi } from "/@/api/system/userGrade/index";
import { useSysUserApi } from "/@/api/system/sysUser/index";
import { verifyPhone, verifyEmail, verifyCnAndSpace } from "/@/utils/toolsValidate";
import { getDefaultSubObject } from "/@/utils/other";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入角色 Api 请求接口
const userGradeApi = useUserGradeApi();

// 引入用户 Api 请求接口
const sysUserApi = useSysUserApi();

// 定义变量内容
const memberSysUserDialogFormRef = ref();
const state = reactive({
  ruleForm: {
    userName: "", // 账户名称
    normalizedUserName: null,
    name: "", // 用户昵称
    surname: null, // 姓
    email: "", // 邮箱
    normalizedEmail: null,
    phoneNumber: "", // 电话号码
    avatar: null, // 头像
    gender: null, // 性别
    gradeId: "", // 用户等级
    ParentId: null, // 推荐人
    isActive: true, // 是否激活
    lockoutEnabled: true, // 锁定启用
    roleNames: [], // 角色名称
    password: "", // 密码
    concurrencyStamp: "",
  },
  gradeData: [] as any,
  dialog: {
    isShowDialog: false,
    type: "",
    editId: "",
    title: "",
    submitTxt: "",
  },
});

// 表单验证规则
const formRules = reactive<FormRules>({
  userName: [{ required: true, message: "请输入账户名称！", trigger: "blur" }],
  name: [{ required: true, message: "请输入用户昵称！", trigger: "blur" }],
  phoneNumber: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (state.ruleForm.phoneNumber && !verifyPhone(value)) {
          callback(new Error("手机号码格式不正确！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  email: [
    { required: true, message: "请输入邮箱！", trigger: "blur" },
    {
      validator: (rule: any, value: any, callback: any) => {
        if (state.ruleForm.email && !verifyEmail(value)) {
          callback(new Error("邮箱格式不正确！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
});

// 提交验证
const submitValidate = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;
  await formEl.validate((valid, fields) => {
    if (valid) {
      onSubmit();
    } else {
      console.warn("未验证通过!", fields);
    }
  });
};

// 打开弹窗
const openDialog = (type: string, row: RowSysUserType) => {
  getGradeData();
  state.ruleForm = getDefaultSubObject(state.ruleForm);
  state.ruleForm.password = import.meta.env.VITE_USER_INIT_PWD;
  nextTick(() => {
    state.dialog.editId = "";
    if (type === "edit") {
      state.dialog.editId = row.id;
      debugger;
      Object.assign(state.ruleForm, row);
      state.dialog.title = "修改用户";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增用户";
      state.dialog.submitTxt = "新 增";
    }
    state.dialog.type = type;
    state.dialog.isShowDialog = true;
  });
};

// 关闭弹窗
const closeDialog = () => {
  state.dialog.isShowDialog = false;
};

// 取消
const onCancel = () => {
  closeDialog();
};

// 提交
const onSubmit = () => {
  if (state.dialog.type == "edit") {
    // 修改
    sysUserApi
      .updateSysUser(state.dialog.editId, state.ruleForm)
      .then((result) => {
        if (result) {
          ElMessage.success("修改成功！");
          closeDialog(); // 关闭弹窗
          emit("refresh");
        }
      })
      .catch((err: any) => {
        console.error("提交出错：", err);
      });
    return;
  }

  // 添加
  sysUserApi
    .addSysUser(state.ruleForm)
    .then((result) => {
      if (result) {
        closeDialog(); // 关闭弹窗
        ElMessageBox.alert(
          `添加成功！初始密码为：<strong class="init-pwd">${state.ruleForm.password}</strong>。`,
          "提示",
          {
            dangerouslyUseHTMLString: true,
          }
        );
        emit("refresh");
      }
    })
    .catch((err: any) => {
      console.error("提交出错：", err);
    });
};

// 初始化角色
const getGradeData = () => {
  userGradeApi
    .getUserGradeAll()
    .then((result) => {
      state.gradeData = [];
      for (let i = 0; i < result.length; i++) {
        const grade = result[i];
        state.gradeData.push({
          id: grade.id,
          title: grade.title,
        });
        if (grade.isDefault && !state.dialog.editId) {
          state.ruleForm.gradeId = grade.id;
        }
      }
    })
    .catch((err) => {
      console.error("查询(选择)用户等级出错：", err);
    });
};

// 去掉中文及空格
const onVerifyCnAndSpace = (val: string) => {
  state.ruleForm.userName = verifyCnAndSpace(val);
};

// 暴露变量
defineExpose({
  openDialog,
});
</script>

<style lang="scss">
.init-pwd {
  color: green;
  background-color: azure;
  display: inline-block;
  padding: 2px 5px;
  margin-right: 10px;
}
</style>
