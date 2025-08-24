<template>
  <div class="imageOrVideo-handle-container">
    <el-form
      ref="imageOrVideoFormRef"
      label-position="top"
      :model="imageOrVideoForm"
      :rules="formRules"
      :disabled="props.isLook"
      size="default"
      label-width="90px"
    >
      <el-row :gutter="35">
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
          <el-form-item label="封面图" prop="image">
            <!--
            <el-input
              placeholder="请输入封面图链接(后续换成上传)"
              v-model="imageOrVideoForm.image"
              clearable
            ></el-input>
            -->
            <div class="image-uploader-container">
              <el-upload
                accept=".jpg, .jpeg, .png, .gif"
                :action="uploadFileConfig().action"
                :headers="uploadFileConfig().headers"
                :multiple="false"
                :show-file-list="false"
                :on-success="handleImageSuccess"
                :before-upload="beforeImageUpload"
                v-loading="uploadImageLoading"
                :element-loading-text="uploadProgressProps.tip"
              >
                <el-image
                  v-if="imageOrVideoForm.image"
                  style="width: 200px; height: 180px"
                  :src="imageOrVideoForm.image"
                  fit="fill"
                />
                <el-icon class="image-uploader-icon" v-else><Plus /></el-icon>
              </el-upload>
              <div class="image-delete-btn" v-if="imageOrVideoForm.image">
                <el-button
                  type="danger"
                  size="small"
                  :icon="Delete"
                  @click="imageOrVideoForm.image = ''"
                  >移除</el-button
                >
              </div>
            </div>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
          <el-form-item label="图集" prop="images">
            <!--
            <el-input
              placeholder="请输入图集链接(后续换成上传)"
              v-model="imageOrVideoForm.images"
              clearable
            ></el-input>
            -->
            <div class="upload-images-list">
              <el-upload
                v-model:file-list="uploadImagesFileList"
                list-type="picture"
                accept=".jpg, .jpeg, .png, .gif"
                :action="uploadFileConfig().action"
                :headers="uploadFileConfig().headers"
                :multiple="true"
                :on-success="handleImagesSuccess"
                :limit="20"
              >
                <el-button type="primary" size="default" :icon="Picture"
                  >选择图集</el-button
                >
              </el-upload>
            </div>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
          <el-divider content-position="left">媒体</el-divider>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
          <el-form-item label="视频" prop="video">
            <!--
            <el-input
              placeholder="请输入视频链接(后续换成上传)"
              v-model="imageOrVideoForm.video"
              clearable
            ></el-input>
            -->
            <div class="upload-video-box">
              <el-input
                :readonly="true"
                placeholder="请选择视频"
                v-model="imageOrVideoForm.video"
                maxlength="1000"
              >
                <template #append>
                  <el-upload
                    ref="uploadVideoRef"
                    accept=".mp4"
                    :action="uploadFileConfig().action"
                    :headers="uploadFileConfig().headers"
                    :show-file-list="false"
                    :limit="1"
                    :before-upload="beforeVideoUpload"
                    :on-exceed="handleVideoExceed"
                    :on-success="handleVideoSuccess"
                    v-loading="uploadVideoLoading"
                    :element-loading-text="uploadProgressProps.tip"
                  >
                    <template #trigger>
                      <el-button
                        type="primary"
                        plain
                        size="default"
                        :icon="Film"
                      ></el-button
                    ></template>
                  </el-upload>
                </template>
              </el-input>
            </div>
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
  </div>
</template>

<script setup lang="ts" name="imageOrVideoHandleDialog">
import { reactive, ref, defineEmits, onMounted, defineProps } from "vue";
import { FormInstance, FormRules, genFileId } from "element-plus";
import { Plus, Delete, Picture, Film } from "@element-plus/icons-vue";
import type { UploadProps, UploadUserFile, UploadRawFile } from "element-plus";
import _ from "lodash";
import { uploadFileConfig } from "/@/utils/other";

// 定义父组件传过来的值
const props = defineProps({
  // 是否查看
  isLook: {
    type: Boolean,
    default: () => false,
  },
});

// 定义变量内容
const emit = defineEmits(["submitFormData"]);
const imageOrVideoFormRef = ref();
const uploadProgressProps = ref({
  percent: 0,
  tip: "上传中",
});
const uploadImageLoading = ref(false);
const uploadVideoLoading = ref(false);
const imageOrVideoForm = ref({
  image: "", // 封面图
  images: "", // 图集
  video: "", // 视频
});
const uploadImagesFileList = ref<UploadUserFile[]>();
const uploadVideoRef = ref();

// 表单验证规则
const formRules = reactive<FormRules>({
  image: [{ required: true, message: "请选择封面图！", trigger: "blur" }],
  images: [
    { required: true, message: "请选择图集！", trigger: "blur" },
    {
      validator: (rule: any, value: any, callback: any) => {
        if (!uploadImagesFileList.value) {
          callback(new Error("请选择图集！"));
          return;
        }
        for (let i = 0; i < uploadImagesFileList.value.length; i++) {
          const uploadFile = uploadImagesFileList.value[i];
          if (!uploadFile.status || uploadFile.status !== "success") {
            callback(new Error("选择的图集存在未上传完成的图片，请稍等！"));
            return;
          }
        }
        callback();
      },
      trigger: ["change", "blur"],
    },
  ],
  // video: [{ required: true, message: "请选择视频！", trigger: "blur" }],
});

// 上传视频成功回调
const handleVideoSuccess: UploadProps["onSuccess"] = (result: string, uploadFile) => {
  if (!result || result.length < 1) return;
  imageOrVideoForm.value.video = result;
  uploadVideoLoading.value = false;
};
// 上传视频之前的函数
const beforeVideoUpload: UploadProps["beforeUpload"] = (rawFile) => {
  uploadVideoLoading.value = true;
  imageOrVideoForm.value.video = "";
  return true;
};

// 上传视频超过限制回调
const handleVideoExceed: UploadProps["onExceed"] = (files) => {
  uploadVideoRef.value!.clearFiles();
  const file = files[0] as UploadRawFile;
  file.uid = genFileId();
  uploadVideoRef.value!.handleStart(file);
};

// 上传图集成功回调
const handleImagesSuccess: UploadProps["onSuccess"] = (result: string, uploadFile) => {
  if (!result || !uploadImagesFileList.value) return;
  let fileUrls = new Array<string>();
  for (let i = 0; i < uploadImagesFileList.value.length; i++) {
    const uploadFile = uploadImagesFileList.value[i];
    if (!uploadFile.status || uploadFile.status !== "success") continue;
    let uploadResult = uploadFile.response as string;
    fileUrls.push(uploadResult);
  }
  if (fileUrls.length < 1) return;
  imageOrVideoForm.value.images = fileUrls.join(";");
};

// 上传图片封面成功回调
const handleImageSuccess: UploadProps["onSuccess"] = (result: string, uploadFile) => {
  if (!result) return;
  imageOrVideoForm.value.image = result;
  uploadImageLoading.value = false;
};

// 上传图片封面之前的函数
const beforeImageUpload: UploadProps["beforeUpload"] = (rawFile) => {
  uploadImageLoading.value = true;
  imageOrVideoForm.value.image = "";
  return true;
};

// 提交验证
const submitValidateAsync = async (formEl: FormInstance | undefined) => {
  if (!formEl) return false;
  let validResult = false;
  await formEl.validate((valid, fields) => {
    if (valid) {
      validResult = true;
      emit("submitFormData", imageOrVideoForm.value);
    } else {
      console.warn("未验证通过!", fields);
    }
  });
  return validResult;
};

// 设置初始值
const setInitValue = (val: any) => {
  imageOrVideoForm.value = val;
  if (imageOrVideoForm.value.images) {
    let images = imageOrVideoForm.value.images.split(";");
    let fileList = new Array<UploadUserFile>();
    for (let i = 0; i < images.length; i++) {
      let imageUrl = images[i];
      if (!imageUrl) continue;
      let fileName = imageUrl.split("/").pop() as string;
      let file: UploadUserFile = {
        name: fileName,
        url: imageUrl,
        status: "success",
        response: imageUrl,
      };
      fileList.push(file);
    }
    uploadImagesFileList.value = fileList;
  }
};

// 暴露变量
defineExpose({
  imageOrVideoFormRef,
  setInitValue,
  submitValidateAsync,
});

// 页面加载完时
onMounted(() => {
  // ...
});
</script>
<style lang="scss" scoped>
.imageOrVideo-handle-container {
  height: 100vh;
  overflow-y: auto;
  .image-uploader-container {
    position: relative;
    border: 1px dashed var(--el-border-color);
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
    transition: var(--el-transition-duration-fast);
  }

  .image-uploader-container:hover {
    border-color: var(--el-color-primary);
  }

  .image-delete-btn {
    position: absolute;
    width: 100%;
    bottom: 0;
    text-align: center;
    background-color: RGBA(0, 0, 0, 0.5);
  }

  .image-uploader-icon {
    font-size: 28px;
    color: #8c939d;
    width: 178px;
    height: 178px;
    text-align: center;
  }

  .upload-images-list {
    width: 100%;
    max-height: 290px;
    overflow-y: auto;
  }

  .upload-video-box {
    min-width: 540px;
  }
}
</style>
