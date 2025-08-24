<template>
  <div class="page-design-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="520px"
    >
      <el-form
        ref="pageDesignDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="设计编码" prop="code">
              <el-input
                v-model="state.ruleForm.code"
                placeholder="请输入设计编码"
                maxlength="20"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="设计名称" prop="name">
              <el-input
                v-model="state.ruleForm.name"
                placeholder="请输入设计名称"
                maxlength="80"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="布局样式" prop="layout">
              <el-select size="default" v-model="state.ruleForm.layout">
                <el-option label="移动端" :value="1"></el-option>
                <el-option label="PC端" :value="2"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="是否默认" prop="type">
              <el-radio-group v-model="state.ruleForm.type" class="ml-4">
                <el-radio :label="1" size="default">是</el-radio>
                <el-radio :label="2" size="default">否</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="描述" prop="description">
              <el-input
                v-model="state.ruleForm.description"
                :autosize="{ minRows: 2, maxRows: 4 }"
                type="textarea"
                placeholder="请输入描述"
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
            @click="submitValidate(pageDesignDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="pageDesignDialog">
import { nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { usePageDesignApi } from "/@/api/shopSetting/pageDesign/index";
import { getDefaultSubObject } from "/@/utils/other";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const pageDesignApi = usePageDesignApi();

// 定义变量内容
const pageDesignDialogFormRef = ref();
const state = reactive({
  ruleForm: {
    code: "", // 页面设计编码
    name: "", // 页面设计名称
    description: "", // 描述
    layout: null, // 版面
    type: 2, // 是否默认
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
  code: [{ required: true, message: "请输入设计编码！", trigger: "blur" }],
  name: [{ required: true, message: "请输入设计名称！", trigger: "blur" }],
  layout: [{ required: true, message: "请选中布局样式！", trigger: "blur" }],
  type: [{ required: true, message: "请选择默认！", trigger: "blur" }],
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
const openDialog = (type: string, row: RowPageDesignType) => {
  state.ruleForm = getDefaultSubObject(state.ruleForm);
  state.ruleForm.type = 2;
  nextTick(() => {
    state.dialog.editId = "";
    if (type === "edit") {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      state.dialog.title = "修改页面设计";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增页面设计";
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
    pageDesignApi
      .updatePageDesign(state.dialog.editId, state.ruleForm)
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
  pageDesignApi
    .addPageDesign(state.ruleForm)
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
