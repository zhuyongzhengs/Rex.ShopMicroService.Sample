<template>
  <div class="system-user-dialog-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="769px"
    >
      <el-form
        ref="userDialogFormRef"
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
                :disabled="!!state.dialog.editId && state.ruleForm.userName == 'admin'"
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
            <el-form-item label="关联角色" prop="roleNames">
              <el-select
                v-model="state.ruleForm.roleNames"
                multiple
                placeholder="请选择"
                clearable
                class="w100"
              >
                <el-option
                  v-for="(role, rIndex) in state.roleData"
                  :label="role"
                  :value="role"
                  :key="rIndex"
                ></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="手机号" prop="phoneNumber">
              <el-input
                v-model="state.ruleForm.phoneNumber"
                placeholder="请输入手机号"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
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
            @click="submitValidate(userDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="systemUserDialog">
import { nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage, ElMessageBox } from "element-plus";
import _ from "lodash";
import { getDefaultSubObject } from "/@/utils/other";
import { useRoleApi } from "/@/api/system/role/index";
import { useUserApi } from "/@/api/system/user/index";
import { verifyPhone, verifyEmail, verifyCnAndSpace } from "/@/utils/toolsValidate";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入角色 Api 请求接口
const roleApi = useRoleApi();

// 引入用户 Api 请求接口
const userApi = useUserApi();

// 定义变量内容
const userDialogFormRef = ref();
const state = reactive({
  ruleForm: {
    userName: "", // 账户名称
    name: "", // 用户昵称
    surname: "", // 姓
    email: "", // 邮箱
    phoneNumber: "", // 电话号码
    isActive: true, // 是否激活
    lockoutEnabled: true, // 锁定启用
    roleNames: [], // 角色名称
    password: import.meta.env.VITE_USER_INIT_PWD, // 密码
    concurrencyStamp: "",
  },
  roleData: [""],
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
  // roleNames: [
  // 	{ required: true, message: '请选择关联角色！', trigger: 'blur' }
  // ],
  phoneNumber: [
    // { required: true, message: '请输入手机号码！', trigger: 'blur' },
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
const openDialog = (type: string, row: RowUserType) => {
  getRoleData();
  state.ruleForm = getDefaultSubObject(state.ruleForm);
  state.ruleForm.isActive = true;
  state.ruleForm.lockoutEnabled = true;
  state.ruleForm.password = import.meta.env.VITE_USER_INIT_PWD;
  nextTick(() => {
    state.dialog.editId = "";
    if (type === "edit") {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      getUserRoles(row.id);
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

// 查询用户角色信息
const getUserRoles = (id: string) => {
  userApi
    .getUserRoles(id)
    .then((result) => {
      if (result) {
        state.ruleForm.roleNames = result.items.map((r: any) => r.name);
      }
    })
    .catch((err) => {
      console.error("查询用户角色信息出错：", err);
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
    userApi
      .updateUser(state.dialog.editId, state.ruleForm)
      .then((result) => {
        if (result) {
          ElMessage.success("修改成功！");
          closeDialog(); // 关闭弹窗
          emit("refresh");
        }
      })
      .catch((err) => {
        console.error("提交出错：", err);
      });
    return;
  }

  // 添加
  userApi
    .addUser(state.ruleForm)
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
    .catch((err) => {
      console.error("提交出错：", err);
    });
};

// 初始化角色
const getRoleData = () => {
  roleApi
    .getRoleAll()
    .then((result) => {
      state.roleData = result.items.map((r: any) => r.name);
    })
    .catch((err) => {
      console.error("查询(选择)用户关联角色出错：", err);
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
