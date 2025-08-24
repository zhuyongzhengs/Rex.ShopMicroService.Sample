<template>
  <div class="good-param-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="520px"
    >
      <el-form
        ref="paramDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="参数名称" prop="name">
              <el-input
                v-model="state.ruleForm.name"
                placeholder="请输入参数名称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="参数类型" prop="type">
              <el-select
                v-model="state.ruleForm.type"
                size="default"
                placeholder="请选择参数类型"
              >
                <el-option
                  v-for="(pType, index) in paramTypes"
                  :key="index"
                  :label="pType.description"
                  :value="pType.title"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="参数值" prop="values">
              <div class="item-tag">
                <el-tag
                  v-for="val in paramValTag.paramValues"
                  :key="val"
                  class="mr10"
                  size="large"
                  closable
                  :disable-transitions="false"
                  @close="handleGoodParamValClose(val)"
                >
                  {{ val }}
                </el-tag>
                <el-input
                  v-if="paramValTag.inputGoodParamVisible"
                  ref="inputGoodParamValRef"
                  v-model="paramValTag.inputGoodParamVal"
                  class="mr10 btn-input-tag"
                  size="large"
                  maxlength="30"
                  @keyup.enter="handleInputGoodParamValConfirm"
                  @blur="handleInputGoodParamValConfirm"
                />
                <el-button v-else size="default" @click="handleGoodParamValAdd">
                  + 添加
                </el-button>
              </div>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(paramDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="goodParamDialog">
import { nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElInput, ElMessage } from "element-plus";
import _ from "lodash";
import { useGoodParamApi } from "/@/api/good/param/index";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const paramApi = useGoodParamApi();

// 定义变量内容
const paramDialogFormRef = ref();
let paramTypes = ref<EnumValueType[]>([]);
const state = reactive({
  ruleForm: {
    name: null, // 参数[模型]名称
    values: [] as string[], // 参数[模型]值
    type: null, // 参数[模型]类型
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
  name: [{ required: true, message: "请输入参数名称！", trigger: "blur" }],
  // values: [{ required: true, message: "请添加参数值！", trigger: "blur" }],
  type: [{ required: true, message: "请选择参数类型！", trigger: "blur" }],
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
const openDialog = (type: string, row: RowGoodParamType, pTypes: any) => {
  paramTypes.value = pTypes.value;
  nextTick(() => {
    state.dialog.editId = "";
    formReset();
    if (type === "edit") {
      paramValTag.paramValues = row.values;
      Object.assign(state.ruleForm, row);
      state.dialog.editId = row.id;
      state.dialog.title = "修改参数模型";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增参数模型";
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

// 重置表单
const formReset = () => {
  paramValTag.paramValues = [];
  state.ruleForm.name = null;
  state.ruleForm.values = [];
  state.ruleForm.type = null;
  state.ruleForm.concurrencyStamp = "";
};

const inputGoodParamValRef = ref<InstanceType<typeof ElInput>>();
// 参数值标签定义
const paramValTag = reactive({
  paramValues: [] as string[], // 参数值数据
  inputGoodParamVal: "", // 绑定输入的参数值
  inputGoodParamVisible: false, // 显示输入参数值
});

// 删除参数值
const handleGoodParamValClose = (tag: string) => {
  paramValTag.paramValues.splice(paramValTag.paramValues.indexOf(tag), 1);
  state.ruleForm.values = paramValTag.paramValues;
};

// 新增参数值(显示参数值输入框)
const handleGoodParamValAdd = () => {
  paramValTag.inputGoodParamVisible = true;
  nextTick(() => {
    inputGoodParamValRef.value!.input!.focus();
  });
};

// 确认(保存)参数值
const handleInputGoodParamValConfirm = () => {
  if (paramValTag.inputGoodParamVal) {
    paramValTag.paramValues.push(paramValTag.inputGoodParamVal);
  }
  paramValTag.inputGoodParamVisible = false;
  paramValTag.inputGoodParamVal = "";
  state.ruleForm.values = paramValTag.paramValues;
};

// 提交
const onSubmit = () => {
  if (state.dialog.type == "edit") {
    // 修改
    paramApi
      .updateGoodParam(state.dialog.editId, state.ruleForm)
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
  paramApi
    .addGoodParam(state.ruleForm)
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

<style scoped lang="scss">
.item-tag {
  display: inline-block;
}

.btn-input-tag {
  width: 65px;
  height: 32px;
}
</style>
