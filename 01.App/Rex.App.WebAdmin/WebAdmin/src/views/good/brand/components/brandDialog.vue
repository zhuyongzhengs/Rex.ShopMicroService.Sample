<template>
  <div class="good-brand-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="520px"
    >
      <el-form
        ref="brandDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="品牌名称" prop="name">
              <el-input
                v-model="state.ruleForm.name"
                placeholder="请输入品牌名称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="品牌Logo" prop="logoImageUrl">
              <!--
              <el-input
                v-model="state.ruleForm.logoImageUrl"
                placeholder="请输入品牌Logo（后续加上传功能）"
                clearable
              ></el-input>
              -->
              <el-input
                :readonly="true"
                placeholder="请选择图片"
                v-model="state.ruleForm.logoImageUrl"
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
                        title="上传"
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
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="是否显示" prop="isShow">
              <el-switch
                v-model="state.ruleForm.isShow"
                inline-prompt
                active-text="开启"
                inactive-text="关闭"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(brandDialogFormRef)"
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

<script setup lang="ts" name="goodBrandDialog">
import { nextTick, reactive, ref } from "vue";
import { ElMessage } from "element-plus";
import type { FormInstance, FormRules, UploadProps } from "element-plus";
import { Picture, Upload } from "@element-plus/icons-vue";
import _ from "lodash";
import { useGoodBrandApi } from "/@/api/good/brand/index";
import { getDefaultSubObject } from "/@/utils/other";
import { uploadFileConfig } from "/@/utils/other";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const goodBrandApi = useGoodBrandApi();

// 定义变量内容
const imageViewer = ref({
  show: false,
  index: 0,
  list: [] as string[],
});
const brandDialogFormRef = ref();
const state = reactive({
  ruleForm: {
    name: "", // 品牌名称
    logoImageUrl: "", // 品牌Logo
    sort: 0, // 排序
    isShow: false, // 是否显示
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
  name: [{ required: true, message: "请输入品牌名称！", trigger: "blur" }],
  logoImageUrl: [{ required: true, message: "请选择品牌Logo！", trigger: "blur" }],
});

// 图片上传成功回调
const handleImageSuccess: UploadProps["onSuccess"] = (result: string, uploadFile) => {
  if (!result) return;
  state.ruleForm.logoImageUrl = result;
  imageViewer.value.list = [result];
};

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
const openDialog = (type: string, row: RowGoodBrandType) => {
  state.ruleForm = getDefaultSubObject(state.ruleForm);
  nextTick(() => {
    state.dialog.editId = "";
    if (type === "edit") {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      imageViewer.value.list = [state.ruleForm.logoImageUrl];
      state.dialog.title = "修改品牌";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增品牌";
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
    goodBrandApi
      .updateGoodBrand(state.dialog.editId, state.ruleForm)
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
  goodBrandApi
    .addGoodBrand(state.ruleForm)
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

<style lang="scss">
.upload-image-box .el-upload {
  width: 100%;
}
</style>

<style scoped lang="scss">
.good-brand-container {
  .upload-image-box {
    width: 100%;
    .img-logo {
      width: 32px;
      height: 32px;
    }
  }
}
</style>
