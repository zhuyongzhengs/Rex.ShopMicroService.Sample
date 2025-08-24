<template>
  <div class="logistics-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="650px"
    >
      <el-form
        ref="logisticsDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="物流编码" prop="logiCode">
              <el-input
                v-model="state.ruleForm.logiCode"
                placeholder="请输入物流编码"
                maxlength="50"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="物流名称" prop="logiName">
              <el-input
                v-model="state.ruleForm.logiName"
                placeholder="请输入物流名称"
                maxlength="100"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="物流电话" prop="phone">
              <el-input
                v-model="state.ruleForm.phone"
                placeholder="请输入物流电话"
                maxlength="30"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="物流网址" prop="url">
              <el-input
                v-model="state.ruleForm.url"
                placeholder="请输入物流网址"
                maxlength="100"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="物流Logo" prop="imgUrl">
              <el-input
                v-model="state.ruleForm.imgUrl"
                :readonly="true"
                placeholder="请选择物流Logo"
                maxlength="1000"
              >
                <template #prepend>
                  <el-button
                    :icon="Picture"
                    title="预览"
                    @click="imageViewer.show = true"
                  />
                </template>
                <template #append>
                  <el-upload
                    class="upload-image-box"
                    accept=".jpg, .jpeg, .png, .gif"
                    :action="uploadFileConfig().action"
                    :headers="uploadFileConfig().headers"
                    :multiple="false"
                    :show-file-list="false"
                    :on-success="handleImageSuccess"
                  >
                    <template #trigger
                      ><el-button
                        type="primary"
                        plain
                        size="default"
                        :icon="Upload"
                      ></el-button
                    ></template>
                  </el-upload>
                </template>
              </el-input>
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
            @click="submitValidate(logisticsDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>

    <el-image-viewer
      v-if="imageViewer.show"
      :url-list="imageViewer.list"
      show-progress
      :initial-index="imageViewer.index"
      @close="imageViewer.show = false"
    />
  </div>
</template>

<script setup lang="ts" name="logisticsDialog">
import { nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules, UploadProps } from "element-plus";
import { ElMessage } from "element-plus";
import { Picture, Upload } from "@element-plus/icons-vue";
import _ from "lodash";
import { useLogisticsApi } from "/@/api/shopSetting/logistics/index";
import { getDefaultSubObject } from "/@/utils/other";
import { uploadFileConfig } from "/@/utils/other";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const logisticsApi = useLogisticsApi();

// 定义变量内容
const imageViewer = ref({
  show: false,
  index: 0,
  list: [] as string[],
});
const logisticsDialogFormRef = ref();
const state = reactive({
  ruleForm: {
    logiCode: "", // 物流编码
    logiName: "", // 物流名称
    imgUrl: "", // 物流Logo
    phone: "", // 物流电话
    url: "", // 物流网址
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

// 图片上传成功回调
const handleImageSuccess: UploadProps["onSuccess"] = (result: string, uploadFile) => {
  if (!result) return;
  state.ruleForm.imgUrl = result;
  imageViewer.value.list = [result];
};

// 表单验证规则
const formRules = reactive<FormRules>({
  logiCode: [{ required: true, message: "请输入物流编码！", trigger: "blur" }],
  logiName: [{ required: true, message: "请输入物流名称！", trigger: "blur" }],
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
const openDialog = (type: string, row: RowLogisticsType) => {
  state.ruleForm = getDefaultSubObject(state.ruleForm);
  nextTick(() => {
    state.dialog.editId = "";
    if (type === "edit") {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      imageViewer.value.list = [state.ruleForm.imgUrl];
      state.dialog.title = "修改物流";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增物流";
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
    logisticsApi
      .updateLogistics(state.dialog.editId, state.ruleForm)
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
  logisticsApi
    .addLogistics(state.ruleForm)
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
