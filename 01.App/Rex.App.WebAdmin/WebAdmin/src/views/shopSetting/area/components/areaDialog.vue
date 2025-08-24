<template>
  <div class="good-area-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="520px"
    >
      <el-form
        ref="areaDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="上级名称" prop="parentId">
              <el-input
                v-model="state.ruleForm.parentName"
                placeholder="请输入上级名称"
                disabled
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="地区名称" prop="name">
              <el-input
                v-model="state.ruleForm.name"
                placeholder="请输入地区名称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="邮编" prop="postalCode">
              <el-input
                v-model="state.ruleForm.postalCode"
                placeholder="请输入邮编"
                maxlength="8"
                clearable
                @input="
                  state.ruleForm.postalCode = verifiyNumber(
                    String(state.ruleForm.postalCode)
                  )
                "
              ></el-input>
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
            @click="submitValidate(areaDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="areaDialog">
import { nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { verifiyNumber } from "/@/utils/toolsValidate";
import { useGoodAreaApi } from "/@/api/shopSetting/area/index";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入区域 Api 请求接口
const areaApi = useGoodAreaApi();

// 定义变量内容
const areaDialogFormRef = ref();
const state = reactive({
  ruleForm: {
    parentId: 0,
    parentName: "",
    depth: 0,
    name: null,
    postalCode: "",
    sort: 0, // 排序
    concurrencyStamp: "",
  },
  dialog: {
    isShowDialog: false,
    type: "",
    editId: 0,
    title: "",
    submitTxt: "",
  },
});

// 表单验证规则
const formRules = reactive<FormRules>({
  name: [{ required: true, message: "请输入区域名称！", trigger: "blur" }],
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
const openDialog = (type: string, row: RowAreaType, parentArea: RowAreaType) => {
  nextTick(() => {
    state.dialog.editId = 0;
    onReset();
    if (type === "edit") {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      state.dialog.title = "修改区域";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增区域";
      state.dialog.submitTxt = "新 增";
    }
    state.ruleForm.parentId = parentArea.id;
    state.ruleForm.parentName = parentArea.name;
    state.ruleForm.depth = parentArea.depth + 1;
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

// 重置
const onReset = () => {
  state.ruleForm = {
    parentId: 0,
    parentName: "",
    depth: 0,
    name: null,
    postalCode: "",
    sort: 0, // 排序
    concurrencyStamp: "",
  };
};

// 提交
const onSubmit = () => {
  if (state.dialog.type == "edit") {
    // 修改
    areaApi
      .updateGoodArea(state.dialog.editId, state.ruleForm)
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
  areaApi
    .addGoodArea(state.ruleForm)
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
