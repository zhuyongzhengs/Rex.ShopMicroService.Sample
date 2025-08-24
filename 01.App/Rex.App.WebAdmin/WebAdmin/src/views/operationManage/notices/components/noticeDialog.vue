<template>
  <div class="notice-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="850px"
    >
      <el-form
        ref="noticeDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="公告标题" prop="title">
              <el-input
                v-model="state.ruleForm.title"
                placeholder="请输入公告标题"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="公告内容" prop="contentBody">
              <editor height="240px" v-model:get-html="state.ruleForm.contentBody" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="排序" prop="sort">
              <el-input-number v-model="state.ruleForm.sort" :precision="0" :min="0" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(noticeDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" title="noticeDialog">
import { defineAsyncComponent, nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useNoticeApi } from "/@/api/operationManage/notice/index";
import { getDefaultSubObject } from "/@/utils/other";

// 引入组件
const Editor = defineAsyncComponent(() => import("/@/components/editor/index.vue"));

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入 Api 请求接口
const noticeApi = useNoticeApi();

// 定义变量内容
const noticeDialogFormRef = ref();
const state = reactive({
  ruleForm: {
    title: "", // 公告标题
    contentBody: "", // 公告内容
    sort: 0, // 排序
    concurrencyStamp: "",
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
  title: [{ required: true, message: "请输入公告标题！", trigger: "blur" }],
  contentBody: [{ required: true, message: "请输入公告内容！", trigger: "blur" }],
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
const openDialog = (type: string, row: RowNoticeType) => {
  state.ruleForm.sort = 1;
  state.dialog.editId = "";
  nextTick(() => {
    if (type === "edit") {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      state.dialog.title = "修改公告";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增公告";
      state.dialog.submitTxt = "新 增";
      state.ruleForm.title = "";
      state.ruleForm.contentBody = "";
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
    noticeApi
      .updateNotice(state.dialog.editId, state.ruleForm)
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
  noticeApi
    .addNotice(state.ruleForm)
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
