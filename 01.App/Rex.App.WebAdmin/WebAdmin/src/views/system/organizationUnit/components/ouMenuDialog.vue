<template>
  <div class="system-ou-dialog-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="520px"
    >
      <el-form
        ref="ouDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="35">
          <el-col
            v-show="state.dialog.type === 'addChildren'"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="上级组织" prop="parentName">
              <el-input v-model="state.ruleForm.parentName" :disabled="true"></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="名称" prop="displayName">
              <el-input
                v-model="state.ruleForm.displayName"
                placeholder="请输入名称"
                clearable
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
            @click="submitValidate(ouDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="systemOuDialog">
import { nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useOrganizationUnitApi } from "/@/api/system/organizationUnit/index";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入角色 Api 请求接口
const ouApi = useOrganizationUnitApi();

// 定义变量内容
const ouDialogFormRef = ref();
const state = reactive({
  ruleForm: {
    parentId: null, // 上级ID
    parentName: null, // 上级名称
    id: null,
    displayName: "", // 名称
    concurrencyStamp: "",
  },
  dialog: {
    isShowDialog: false,
    type: "",
    title: "",
    submitTxt: "",
  },
});

// 表单验证规则
const formRules = reactive<FormRules>({
  displayName: [{ required: true, message: "请输入名称！", trigger: "blur" }],
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

// 重置表单
const resetForm = () => {
  state.ruleForm.parentId = null;
  state.ruleForm.parentName = null;
  state.ruleForm.id = null;
  state.ruleForm.displayName = "";
};

// 打开弹窗
const openDialog = (type: string, row: any) => {
  nextTick(() => {
    resetForm();
    if (type === "addChildren") {
      state.ruleForm.parentId = row.id;
      state.ruleForm.parentName = row.displayName;

      state.dialog.title = "添加下级组织";
      state.dialog.submitTxt = "确 定";
    } else if (type === "edit") {
      state.ruleForm.id = row.id;
      state.ruleForm.parentId = row.parentId;
      state.ruleForm.displayName = row.displayName;
      state.ruleForm.concurrencyStamp = row.concurrencyStamp;

      state.dialog.title = "修改：" + row.displayName;
      state.dialog.submitTxt = "确 定";
    } else {
      state.dialog.title = "添加组织";
      state.dialog.submitTxt = "确 定";
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
    let updateId = String(state.ruleForm.id);
    let updateForm = Object.assign({
      displayName: state.ruleForm.displayName,
      concurrencyStamp: state.ruleForm.concurrencyStamp,
    });
    ouApi
      .updateOrganizationUnit(updateId, updateForm)
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
  let addForm = Object.assign({
    parentId: state.ruleForm.parentId,
    displayName: state.ruleForm.displayName,
  });
  ouApi
    .addOrganizationUnit(addForm)
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
