<template>
  <div class="promotion-groupSeckill-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="750px"
    >
      <el-form
        ref="groupSeckillDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="80px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="名称" prop="name">
              <el-input
                v-model="state.ruleForm.name"
                placeholder="请输入名称"
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
          <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24" class="mb20">
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
          <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24" class="mb20">
            <el-form-item label="限购数量" prop="maxNums">
              <el-tooltip placement="bottom">
                <template #content>每人限购数量，0为不限制</template>
                <el-input-number
                  v-model="state.ruleForm.maxNums"
                  :precision="0"
                  :min="0"
                  :max="999999"
                />
              </el-tooltip>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24" class="mb20">
            <el-form-item label="总量" prop="maxGoodNums" label-width="60">
              <el-tooltip placement="bottom">
                <template #content>活动商品总量。0为不限制，以商品库存准</template>
                <el-input-number
                  v-model="state.ruleForm.maxGoodNums"
                  :precision="0"
                  :min="0"
                  :max="999999"
                />
              </el-tooltip>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24" class="mb20">
            <el-form-item label="是否开启" prop="isEnable">
              <el-select
                v-model="state.ruleForm.isEnable"
                size="default"
                placeholder="是否开启"
              >
                <el-option label="是" :value="true" />
                <el-option label="否" :value="false" />
              </el-select>
            </el-form-item>
          </el-col>
          <!--
          <el-col :xs="24" :sm="24" :md="24" :lg="16" :xl="24" class="mb20">
            <el-form-item label="类型" prop="type">
              <el-radio-group v-model="state.ruleForm.type" class="ml-4">
                <el-radio :label="3" size="default">团购</el-radio>
                <el-radio :label="4" size="default">秒杀</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          -->
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(groupSeckillDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="groupSeckillDialog">
import { nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useGroupSeckillApi } from "/@/api/promotion/groupSeckill/index";
import { getDefaultSubObject } from "/@/utils/other";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const groupSeckillApi = useGroupSeckillApi();

// 定义变量内容
const groupSeckillDialogFormRef = ref();
const startAndEndTime = ref([] as string[]);
const state = reactive({
  ruleForm: {
    name: "",
    type: 4,
    weight: 0,
    parameters: "",
    maxNums: 0,
    maxGoodNums: 0,
    maxRecevieNums: 0,
    startTime: "",
    endTime: "",
    isExclusive: false,
    isAutoReceive: false,
    isEnable: false,
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
  name: [{ required: true, message: "请输入团购|秒杀名称！", trigger: "blur" }],
  startTime: [{ required: true, message: "请选择起止时间！", trigger: "blur" }],
  effectiveDays: [{ required: true, message: "请输入有效天数！", trigger: "blur" }],
  effectiveHours: [{ required: true, message: "请输入有效小时！", trigger: "blur" }],
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
  state.ruleForm.type = 4;
  state.ruleForm.weight = 100;
  state.ruleForm.maxRecevieNums = 100;
  state.ruleForm.isAutoReceive = true;
  state.ruleForm.isEnable = true;
  nextTick(() => {
    state.dialog.editId = "";
    if (type === "edit") {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      if (state.ruleForm.startTime && state.ruleForm.endTime) {
        startAndEndTime.value = [state.ruleForm.startTime, state.ruleForm.endTime];
      }
      state.dialog.title = "修改商品秒杀";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增商品秒杀";
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
  if (state.dialog.type == "edit") {
    // 修改
    groupSeckillApi
      .updateGroupSeckill(state.dialog.editId, state.ruleForm)
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
  groupSeckillApi
    .addGroupSeckill(state.ruleForm)
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
<style lang="scss" scoped>
// ...
</style>
