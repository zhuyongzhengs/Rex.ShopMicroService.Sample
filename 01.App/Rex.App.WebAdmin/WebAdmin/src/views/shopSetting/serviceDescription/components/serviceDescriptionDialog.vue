<template>
  <div class="service-description-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="700px"
    >
      <el-form
        ref="serviceDescriptionDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="70px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="名称" prop="title">
              <el-input
                v-model="state.ruleForm.title"
                placeholder="请输入名称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24" class="mb20">
            <el-form-item label="类型" prop="type">
              <el-select
                v-model="state.ruleForm.type"
                size="default"
                placeholder="选择类型"
              >
                <el-option
                  v-for="type in serviceNoteTypes"
                  :key="type.value"
                  :label="type.description"
                  :value="type.value"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="10" :xl="24" class="mb20">
            <el-form-item label="排序" prop="sort">
              <el-input-number v-model="state.ruleForm.sort" :precision="0" :min="0" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="6" :xl="24" class="mb20">
            <el-form-item label="是否显示" prop="isShow">
              <el-switch
                v-model="state.ruleForm.isShow"
                inline-prompt
                active-text="开启"
                inactive-text="关闭"
              />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="描述" prop="description">
              <el-input
                v-model="state.ruleForm.description"
                type="textarea"
                maxlength="450"
                placeholder="请输入描述信息…"
                clearable
                :rows="4"
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
            @click="submitValidate(serviceDescriptionDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" title="serviceDescriptionDialog">
import { nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useServiceDescriptionApi } from "/@/api/shopSetting/serviceDescription/index";
import { getDefaultSubObject } from "/@/utils/other";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 定义父组件传过来的值
const props = defineProps({
  serviceNoteTypes: {
    type: Object,
    default: () => [],
  },
});

// 引入用户 Api 请求接口
const serviceDescriptionApi = useServiceDescriptionApi();

// 定义变量内容
const serviceDescriptionDialogFormRef = ref();
const state = reactive({
  ruleForm: {
    title: null,
    type: null,
    description: null,
    isShow: false,
    sort: 0,
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
  title: [{ required: true, message: "请输入名称！", trigger: "blur" }],
  type: [{ required: true, message: "请选择类型！", trigger: "blur" }],
  sort: [{ required: true, message: "请输入排序！", trigger: "blur" }],
  description: [{ required: true, message: "请输入描述信息！", trigger: "blur" }],
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
const openDialog = (type: string, row: RowServiceDescriptionType) => {
  state.ruleForm = getDefaultSubObject(state.ruleForm);
  nextTick(() => {
    state.dialog.editId = "";
    if (type === "edit") {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      state.dialog.title = "修改商城服务描述";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增商城服务描述";
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
    serviceDescriptionApi
      .updateServiceDescription(state.dialog.editId, state.ruleForm)
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
  serviceDescriptionApi
    .addServiceDescription(state.ruleForm)
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
