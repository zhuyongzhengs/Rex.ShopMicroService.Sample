<template>
  <div class="promotion-global-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="520px"
    >
      <el-form
        ref="promotionDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="促销名称" prop="name">
              <el-input
                v-model="state.ruleForm.name"
                placeholder="请输入促销名称"
                maxlength="40"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="起止时间" prop="startTime">
              <el-date-picker
                v-model="startAndEndTime"
                type="datetimerange"
                range-separator="~"
                start-placeholder="开始时间"
                end-placeholder="结束时间"
                size="default"
                format="YYYY-MM-DD HH:mm:ss"
                value-format="YYYY-MM-DD HH:mm:ss"
                @change="onStartAndEndTimeChange"
              />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="6" :xl="24" class="mb20">
            <el-form-item label="是否开启" prop="isEnable">
              <el-switch
                v-model="state.ruleForm.isEnable"
                inline-prompt
                active-text="是"
                inactive-text="否"
                size="default"
              />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="6" :xl="24" class="mb20">
            <el-form-item label="是否排他" prop="isExclusive">
              <el-switch
                v-model="state.ruleForm.isExclusive"
                inline-prompt
                active-text="是"
                inactive-text="否"
              />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="权重" prop="weight">
              <el-tooltip placement="bottom">
                <template #content>数字越小，权重越大</template>
                <el-input-number
                  v-model="state.ruleForm.weight"
                  :precision="0"
                  :min="0"
                />
              </el-tooltip>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(promotionDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="promotionDialog">
import { nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { usePromotionApi } from "/@/api/promotion/index";
import { getDefaultSubObject } from "/@/utils/other";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const promotionApi = usePromotionApi();

// 定义变量内容
const promotionDialogFormRef = ref();
const startAndEndTime = ref([] as string[]);
const state = reactive({
  ruleForm: {
    name: "",
    type: 1,
    weight: 0,
    parameters: "",
    maxNums: 0,
    maxGoodNums: 0,
    maxRecevieNums: 0,
    startTime: "",
    endTime: "",
    isExclusive: false,
    isAutoReceive: false,
    isEnable: true,
    effectiveDays: 0,
    effectiveHours: 0,
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
  name: [{ required: true, message: "请输入促销名称！", trigger: "blur" }],
  startTime: [{ required: true, message: "请选择起止时间！", trigger: "blur" }],
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
const openDialog = (type: string, row: RowPromotionType) => {
  state.ruleForm = getDefaultSubObject(state.ruleForm);
  state.ruleForm.weight = 100;
  nextTick(() => {
    state.dialog.editId = "";
    if (type === "edit") {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      if (state.ruleForm.startTime && state.ruleForm.endTime) {
        startAndEndTime.value = [state.ruleForm.startTime, state.ruleForm.endTime];
      }
      state.dialog.title = "修改全局促销";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增全局促销";
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

// 起止时间切换
const onStartAndEndTimeChange = (date: string[]) => {
  state.ruleForm.startTime = date[0];
  state.ruleForm.endTime = date[1];
};

// 提交
const onSubmit = () => {
  state.ruleForm.type = 1;
  if (state.dialog.type == "edit") {
    // 修改
    promotionApi
      .updatePromotion(state.dialog.editId, state.ruleForm)
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
  promotionApi
    .addPromotion(state.ruleForm)
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
