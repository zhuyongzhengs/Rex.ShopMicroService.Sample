<template>
  <div class="good-storeClerk-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="450px"
    >
      <el-form
        ref="storeClerkDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        label-position="top"
        size="default"
        label-width="110px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="已注册用户的手机号码" prop="phoneNumber">
              <el-input
                v-model="state.ruleForm.phoneNumber"
                placeholder="请输入已注册用户手机号码"
                clearable
                maxlength="12"
              ></el-input>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(storeClerkDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="storeClerkDialog">
import { nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useGoodStoreClerkApi } from "/@/api/shopSetting/storeClerk/index";
import { verifyPhone } from "/@/utils/toolsValidate";
import { getDefaultSubObject } from "/@/utils/other";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入 Api 请求接口
const storeClerkApi = useGoodStoreClerkApi();

// 定义变量内容
const storeClerkDialogFormRef = ref();
const state = reactive({
  ruleForm: {
    storeId: "",
    phoneNumber: "",
  },
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
  phoneNumber: [
    { required: true, message: "请输入已注册用户的手机号码！", trigger: "blur" },
    {
      validator: (rule: any, value: any, callback: any) => {
        if (!verifyPhone(state.ruleForm.phoneNumber)) {
          callback(new Error("请输入正确的手机号码！"));
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
const openDialog = (type: string, storeId: string, row: RowStoreClerkType) => {
  state.ruleForm = getDefaultSubObject(state.ruleForm);
  state.ruleForm.storeId = storeId;
  nextTick(() => {
    if (type === "edit") {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      state.dialog.title = "修改店员";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "创建店员";
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
    storeClerkApi
      .updateGoodStoreClerk(state.dialog.editId, state.ruleForm)
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
  storeClerkApi
    .addGoodStoreClerk(state.ruleForm)
    .then((result) => {
      if (result) {
        ElMessage.success("添加成功！");
        closeDialog(); // 关闭弹窗
        emit("refresh");
      }
    })
    .catch((err: any) => {
      console.error("提交出错：", err);
    });
};

// 暴露变量
defineExpose({
  openDialog,
});
</script>

<style lang="scss" scoped>
// ...
</style>
