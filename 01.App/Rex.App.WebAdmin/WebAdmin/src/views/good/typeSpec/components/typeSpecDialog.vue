<template>
  <div class="good-type-spec-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="520px"
    >
      <el-form
        ref="goodTypeSpecDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="模型名称" prop="name">
              <el-input
                v-model="state.ruleForm.name"
                placeholder="请输入SKU模型名称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="模型值" prop="specValues">
              <div class="item-tag">
                <el-tag
                  v-for="(val, index) in goodTypeSpecValTag.goodTypeSpecValues"
                  :key="index"
                  class="mr10"
                  size="large"
                  closable
                  :disable-transitions="false"
                  @close="handleGoodTypeSpecValClose(index)"
                >
                  {{ val.value }}
                </el-tag>
                <el-input
                  v-if="goodTypeSpecValTag.inputGoodTypeSpecVisible"
                  ref="inputGoodTypeSpecValRef"
                  v-model="goodTypeSpecValTag.inputGoodTypeSpecVal"
                  class="mr10 btn-input-tag"
                  size="large"
                  maxlength="30"
                  @keyup.enter="handleInputGoodTypeSpecValConfirm"
                  @blur="handleInputGoodTypeSpecValConfirm"
                />
                <el-button v-else size="default" @click="handleGoodTypeSpecValAdd">
                  + 添加
                </el-button>
              </div>
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
            @click="submitValidate(goodTypeSpecDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="goodGoodTypeSpecDialog">
import { nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElInput, ElMessage } from "element-plus";
import _ from "lodash";
import { useGoodTypeSpecApi } from "/@/api/good/typeSpec/index";
import { getGuidEmpty } from "/@/utils/other";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const goodTypeSpecApi = useGoodTypeSpecApi();

// 定义变量内容
const goodTypeSpecDialogFormRef = ref();
const state = reactive({
  ruleForm: {
    name: null, // SKU模型名称
    specValues: [] as RowGoodTypeSpecValueType[], // SKU模型值
    sort: 0, // SKU模型类型
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
  name: [{ required: true, message: "请输入SKU模型名称！", trigger: "blur" }],
  specValues: [{ required: true, message: "请添加模型值！", trigger: "blur" }],
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
const openDialog = (type: string, row: RowGoodTypeSpecType) => {
  nextTick(() => {
    state.dialog.editId = "";
    formReset();
    if (type === "edit") {
      goodTypeSpecValTag.goodTypeSpecValues = row.specValues;
      Object.assign(state.ruleForm, row);
      state.dialog.editId = row.id;
      state.dialog.title = "修改SKU模型";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增SKU模型";
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
  goodTypeSpecValTag.goodTypeSpecValues = [];
  state.ruleForm.name = null;
  state.ruleForm.specValues = [];
  state.ruleForm.sort = 0;
  state.ruleForm.concurrencyStamp = "";
};

const inputGoodTypeSpecValRef = ref<InstanceType<typeof ElInput>>();
// 模型值标签定义
const goodTypeSpecValTag = reactive({
  goodTypeSpecValues: [] as RowGoodTypeSpecValueType[], // 模型值数据
  inputGoodTypeSpecVal: "", // 绑定输入的模型值
  inputGoodTypeSpecVisible: false, // 显示输入模型值
});

// 删除模型值
const handleGoodTypeSpecValClose = (index: number) => {
  goodTypeSpecValTag.goodTypeSpecValues.splice(index, 1);
  state.ruleForm.specValues = goodTypeSpecValTag.goodTypeSpecValues;
};

// 新增模型值(显示模型值输入框)
const handleGoodTypeSpecValAdd = () => {
  goodTypeSpecValTag.inputGoodTypeSpecVisible = true;
  nextTick(() => {
    inputGoodTypeSpecValRef.value!.input!.focus();
  });
};

// 确认(保存)模型值
const handleInputGoodTypeSpecValConfirm = () => {
  if (goodTypeSpecValTag.inputGoodTypeSpecVal) {
    let sortVal = goodTypeSpecValTag.goodTypeSpecValues.length;
    let tsVal: RowGoodTypeSpecValueType = {
      id: getGuidEmpty(),
      tenantId: "",
      specId: state.dialog.editId,
      value: goodTypeSpecValTag.inputGoodTypeSpecVal,
      sort: sortVal + 1,
    };
    goodTypeSpecValTag.goodTypeSpecValues.push(tsVal);
  }
  goodTypeSpecValTag.inputGoodTypeSpecVisible = false;
  goodTypeSpecValTag.inputGoodTypeSpecVal = "";
  state.ruleForm.specValues = goodTypeSpecValTag.goodTypeSpecValues;
};

// 提交
const onSubmit = () => {
  // 排序
  for (let i = 0; i < state.ruleForm.specValues.length; i++) {
    let specValue = state.ruleForm.specValues[i];
    specValue.sort = i + 1;
  }

  if (state.dialog.type == "edit") {
    // 修改
    goodTypeSpecApi
      .updateGoodTypeSpec(state.dialog.editId, state.ruleForm)
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
  goodTypeSpecApi
    .addGoodTypeSpec(state.ruleForm)
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
  width: 80px;
  height: 32px;
}
</style>
