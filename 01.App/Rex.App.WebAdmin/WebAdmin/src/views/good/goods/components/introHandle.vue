<template>
  <div class="intro-handle-container">
    <el-form ref="introFormRef" :model="state.editor" :rules="formRules" size="default">
      <el-row :gutter="35">
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
          <el-form-item prop="intro">
            <editor
              v-model:get-html="state.editor.htmlVal"
              :disable="state.editor.disable"
              :height="editorHeight + 'px'"
            />
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
  </div>
</template>

<script setup lang="ts" name="introHandleDialog">
import { reactive, ref, defineEmits, onMounted, defineAsyncComponent } from "vue";
import { FormInstance, FormRules } from "element-plus";
import _ from "lodash";

// 引入组件
const Editor = defineAsyncComponent(() => import("/@/components/editor/index.vue"));

/*
// 定义父组件传过来的值
const props = defineProps({
  // 是否查看
  isLook: {
    type: Boolean,
    default: () => false,
  },
});
*/

// 定义变量内容
const emit = defineEmits(["submitFormData"]);
const introFormRef = ref();
const editorHeight = ref(0);

// 定义变量内容
const state = reactive({
  editor: {
    htmlVal: "",
    disable: false,
  },
});

// 表单验证规则
const formRules = reactive<FormRules>({
  // intro: [{ required: true, message: "请输入商品明细！", trigger: "blur" }],
});

// 提交验证
const submitValidateAsync = async (formEl: FormInstance | undefined) => {
  if (!formEl) return false;
  let validResult = false;
  await formEl.validate((valid, fields) => {
    if (valid) {
      validResult = true;
      emit("submitFormData", state.editor.htmlVal);
    } else {
      console.warn("未验证通过!", fields);
    }
  });
  return validResult;
};

// 设置初始值
const setInitValue = (val: any) => {
  state.editor.htmlVal = val;
};

// 暴露变量
defineExpose({
  introFormRef,
  setInitValue,
  submitValidateAsync,
});

// 页面加载完时
onMounted(() => {
  editorHeight.value = window.innerHeight - 300;
});
</script>
