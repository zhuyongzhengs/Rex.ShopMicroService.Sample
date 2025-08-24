<template>
  <div class="article-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="750px"
    >
      <el-form
        ref="articleDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="5">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="标题" prop="title">
              <el-input
                v-model="state.ruleForm.title"
                placeholder="请输入标题"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="10" :xl="24" class="mb20">
            <el-form-item label="文章分类" prop="typeId">
              <el-select
                v-model="state.ruleForm.typeId"
                size="default"
                placeholder="请选择文章分类"
              >
                <el-option
                  v-for="articleType in articleTypeItems"
                  :key="articleType.id"
                  :label="articleType.name"
                  :value="articleType.id"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="14" :xl="24" class="mb20">
            <el-form-item label="封面图片" prop="coverImage">
              <el-input
                v-model="state.ruleForm.coverImage"
                placeholder="请选择封面图片"
                :readonly="true"
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
            <el-form-item label="简介" prop="brief">
              <el-input
                v-model="state.ruleForm.brief"
                placeholder="请输入简介"
                :autosize="{ minRows: 2, maxRows: 4 }"
                type="textarea"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="文章内容" prop="contentBody">
              <editor height="240px" v-model:get-html="state.ruleForm.contentBody" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="10" :xl="24" class="mb20">
            <el-form-item label="排序" prop="sort">
              <el-input-number v-model="state.ruleForm.sort" :precision="0" :min="0" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="6" :xl="24" class="mb20">
            <el-form-item label="是否发布" prop="isPub">
              <el-switch
                v-model="state.ruleForm.isPub"
                inline-prompt
                active-text="是"
                inactive-text="否"
              />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24" class="mb20">
            <el-form-item label="访问量" prop=" ">
              <el-input-number v-model="state.ruleForm.pv" :precision="0" :min="0" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(articleDialogFormRef)"
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

<script setup lang="ts" title="articleDialog">
import { nextTick, onMounted, reactive, ref, defineAsyncComponent } from "vue";
import type { FormInstance, FormRules, UploadProps } from "element-plus";
import { Picture, Upload } from "@element-plus/icons-vue";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useArticleApi } from "/@/api/operationManage/article/index";
import { useArticleTypeApi } from "/@/api/operationManage/articleType/index";
import { getDefaultSubObject } from "/@/utils/other";
import { uploadFileConfig } from "/@/utils/other";

const Editor = defineAsyncComponent(() => import("/@/components/editor/index.vue"));

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const articleApi = useArticleApi();
const articleTypeApi = useArticleTypeApi();

// 定义变量内容
const imageViewer = ref({
  show: false,
  index: 0,
  list: [] as string[],
});
const articleDialogFormRef = ref();
const articleTypeItems = ref([] as RowArticleTypeType[]);
const state = reactive({
  ruleForm: {
    title: "", // 标题
    brief: "", // 简介
    coverImage: "", // 封面图片
    contentBody: "", // 文章内容
    typeId: "", // 文章分类ID
    sort: 0, // 排序
    isPub: false, // 是否发布
    pv: 0, // 访问量
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
  title: [{ required: true, message: "请输入标题！", trigger: "blur" }],
  // brief: [{ required: true, message: "请输入简介！", trigger: "blur" }],
  // coverImage: [{ required: true, message: "请上传封面图片！", trigger: "blur" }],
  contentBody: [{ required: true, message: "请输入文章内容！", trigger: "blur" }],
  typeId: [{ required: true, message: "请选择文章分类！", trigger: "blur" }],
});

// 图片上传成功回调
const handleImageSuccess: UploadProps["onSuccess"] = (result: string, uploadFile) => {
  if (!result) return;
  state.ruleForm.coverImage = result;
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
const openDialog = (type: string, row: RowArticleType) => {
  nextTick(() => {
    state.dialog.editId = "";
    if (type === "edit") {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      imageViewer.value.list = [state.ruleForm.coverImage];
      state.dialog.title = "修改文章";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增文章";
      state.dialog.submitTxt = "新 增";
      state.ruleForm = getDefaultSubObject(state.ruleForm);
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
    articleApi
      .updateArticle(state.dialog.editId, state.ruleForm)
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
  articleApi
    .addArticle(state.ruleForm)
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

// 获取文章分类
const getArticleTypeData = () => {
  articleTypeApi
    .getArticleTypeList({
      name: null,
      sorting: "Sort",
      skipCount: 0,
      maxResultCount: 1000,
    })
    .then((result) => {
      articleTypeItems.value = result.items;
    })
    .catch((err: any) => {
      console.error("查询文章分类列表出错：", err);
    });
};

// 页面加载完时
onMounted(() => {
  getArticleTypeData();
});

// 暴露变量
defineExpose({
  openDialog,
});
</script>
