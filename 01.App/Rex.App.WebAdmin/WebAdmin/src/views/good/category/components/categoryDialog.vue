<template>
  <div class="good-category-dialog-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      :lock-scroll="false"
      width="769px"
    >
      <el-form
        ref="categoryDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="80px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="上级分类" prop="parentId">
              <el-cascader
                :options="state.categoryData"
                :props="{ checkStrictly: true, value: 'id', label: 'name' }"
                placeholder="请选择上级分类"
                clearable
                class="w100"
                v-model="state.ruleForm.pGoodCategory"
                @change="pGoodCategoryChange"
              >
                <template #default="{ node, data }">
                  <span>{{ data.name }}</span>
                  <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
                </template>
              </el-cascader>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="分类名称" prop="name">
              <el-input
                v-model="state.ruleForm.name"
                maxlength="20"
                placeholder="请输入分类名称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="类型" prop="name">
              <el-select
                v-model="state.ruleForm.typeId"
                size="default"
                placeholder="选择类型"
              >
                <el-option
                  label="通用类型"
                  value="00000000-0000-0000-0000-000000000000"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="分类图片" prop="imageUrl">
              <el-input
                placeholder="请选择分类图片"
                v-model="state.ruleForm.imageUrl"
                maxlength="1000"
                clearable
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
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="排序" prop="sort">
              <el-input-number
                min="0"
                precision="0"
                v-model="state.ruleForm.sort"
                controls-position="right"
                placeholder="请输入排序"
                class="w100"
              />
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
            @click="submitValidate(categoryDialogFormRef)"
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

<script setup lang="ts" name="goodCategoryDialog">
import { reactive, ref, nextTick } from "vue";
import type { FormInstance, FormRules, UploadProps } from "element-plus";
import { ElMessage } from "element-plus";
import { Picture, Upload } from "@element-plus/icons-vue";
import _ from "lodash";
import { useGoodCategoryApi } from "/@/api/good/category/index";
import { getDefaultSubObject } from "/@/utils/other";
import { uploadFileConfig } from "/@/utils/other";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入商品分类 Api 请求接口
const goodCategoryApi = useGoodCategoryApi();

// 定义变量内容
const imageViewer = ref({
  show: false,
  index: 0,
  list: [] as string[],
});
const categoryDialogFormRef = ref();
const state = reactive({
  ruleForm: {
    pGoodCategory: [""], // 上级商品分类
    parentId: null, // 上级商品分类ID
    name: null, // 分类名称
    typeId: "", // 分类名称
    sort: 0, // 排序
    imageUrl: "", // 分类图片
    isShow: false, // 是否显示
  },
  categoryData: [] as RowGoodCategoryType[], // 上级商品分类数据
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
  name: [{ required: true, message: "请输入分类名称！", trigger: "blur" }],
  /*
  imageUrl: [
    { required: true, message: "请输入分类图片（后续加上传功能）！", trigger: "blur" },
  ],
  */
});

// 图片上传成功回调
const handleImageSuccess: UploadProps["onSuccess"] = (result: string, uploadFile) => {
  if (!result) return;
  state.ruleForm.imageUrl = result;
  imageViewer.value.list = [result];
};

// 上级商品分类切换
function pGoodCategoryChange(val: []): void {
  if (!val || val.length < 1) {
    state.ruleForm.parentId = null;
    return;
  }
  state.ruleForm.parentId = val[val.length - 1];
}

// 打开弹窗
const openDialog = (type: string, row?: any, pGoodCategory: string[] = []) => {
  getgoodCategoryTreeData();
  state.ruleForm = getDefaultSubObject(state.ruleForm);
  nextTick(() => {
    state.ruleForm.pGoodCategory = [""];
    if (type === "edit") {
      if (state.ruleForm.parentId) {
        state.ruleForm.pGoodCategory = pGoodCategory;
      }
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      imageViewer.value.list = [state.ruleForm.imageUrl];
      state.dialog.title = "修改商品分类";
      state.dialog.submitTxt = "修 改";
    } else {
      state.ruleForm.typeId = "00000000-0000-0000-0000-000000000000";
      state.dialog.title = "新增商品分类";
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

// 提交
const onSubmit = () => {
  if (state.dialog.type == "edit") {
    // 修改
    goodCategoryApi
      .updateGoodCategory(state.dialog.editId, state.ruleForm)
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
  goodCategoryApi
    .addGoodCategory(state.ruleForm)
    .then((result) => {
      if (result) {
        ElMessage.success("添加成功！");
        closeDialog(); // 关闭弹窗
        emit("refresh");
      }
    })
    .catch((err) => {
      console.error("提交出错：", err);
    });
};

// 取消
const onCancel = () => {
  closeDialog();
};

// 初始化上级商品分类
const getgoodCategoryTreeData = () => {
  goodCategoryApi
    .getGoodCategoryTree()
    .then((result) => {
      state.categoryData = result;
    })
    .catch((err) => {
      console.error("查询(选择)上级商品分类出错：", err);
    });
};

// 暴露变量
defineExpose({
  openDialog,
});
</script>
