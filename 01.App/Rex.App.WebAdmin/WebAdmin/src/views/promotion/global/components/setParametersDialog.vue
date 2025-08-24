<template>
  <div class="promotion-global-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="650px"
    >
      <el-form
        ref="promotionDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb15">
            <el-form-item label="促销名称" prop="name">
              <el-input
                v-model="state.ruleForm.name"
                placeholder="请输入促销名称"
                maxlength="40"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb15">
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
          <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24" class="mb15">
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
          <el-col :xs="24" :sm="24" :md="24" :lg="6" :xl="24" class="mb15">
            <el-form-item label="是否排他" prop="isExclusive">
              <el-tooltip placement="bottom">
                <template #content>【开启】后，会排除该促销之后的所有促销</template>
                <el-switch
                  v-model="state.ruleForm.isExclusive"
                  inline-prompt
                  active-text="是"
                  inactive-text="否"
                />
              </el-tooltip>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="10" :xl="24" class="mb15">
            <el-form-item label="权重" prop="weight" label-width="65">
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
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
            <el-form-item label="促销条件">
              <el-tooltip placement="right" effect="light">
                <template #content
                  >促销条件当有多项的情况下，为所有条件必须满足。如相同条件有重复设置，请开设新促销。</template
                >
                <el-button
                  type="success"
                  :icon="CirclePlus"
                  size="small"
                  @click="onOpenPromotionCondition(null)"
                  >添加条件</el-button
                >
              </el-tooltip>
            </el-form-item>
            <el-table
              class="condition-table-list"
              :data="state.ruleForm.promotionConditions"
              style="width: 100%"
              :border="true"
            >
              <el-table-column prop="code" label="条件代码" width="150" />
              <el-table-column
                prop="codeName"
                label="条件名称"
                width="200"
                show-overflow-tooltip
              >
                <template #default="scope">
                  <label>{{ showConditionTypeDesc(scope.row.code) }}</label>
                </template>
              </el-table-column>
              <el-table-column
                min-width="240"
                prop="parameters"
                label="参数"
                show-overflow-tooltip
              />
              <el-table-column fixed="right" label="操作" width="140">
                <template #default="scope">
                  <el-button
                    type="warning"
                    size="small"
                    @click="onOpenPromotionCondition(scope.row)"
                    >编辑</el-button
                  >
                  <el-button
                    type="danger"
                    size="small"
                    @click="onDelPromotionCondition(scope.row)"
                    >删除</el-button
                  >
                </template>
              </el-table-column>
            </el-table>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
            <el-form-item label="促销结果"
              ><el-button
                type="success"
                :icon="DocumentAdd"
                size="small"
                @click="onOpenPromotionResult(null)"
                >添加结果</el-button
              ></el-form-item
            >
            <el-table
              class="result-table-list"
              :data="state.ruleForm.promotionResults"
              style="width: 100%"
              :border="true"
            >
              <el-table-column prop="code" label="结果代码" width="200" />
              <el-table-column
                prop="codeName"
                label="条件名称"
                width="200"
                show-overflow-tooltip
              >
                <template #default="scope">
                  <label>{{ showResultTypeDesc(scope.row.code) }}</label>
                </template>
              </el-table-column>
              <el-table-column
                min-width="240"
                prop="parameters"
                label="参数"
                show-overflow-tooltip
              />
              <el-table-column fixed="right" label="操作" width="140">
                <template #default="scope">
                  <el-button
                    type="warning"
                    size="small"
                    @click="onOpenPromotionResult(scope.row)"
                    >编辑</el-button
                  >
                  <el-button
                    type="danger"
                    size="small"
                    @click="onDelPromotionResult(scope.row)"
                    >删除</el-button
                  >
                </template>
              </el-table-column>
            </el-table>
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
    <promotion-condition-dialog
      :conditionTypes="promotionConditionTypeItem"
      ref="promotionConditionDialogRef"
      @condition-change="onConditionChange"
    />
    <promotion-result-dialog
      :resultTypes="promotionResultTypeItem"
      ref="promotionResultDialogRef"
      @result-change="onResultChange"
    />
  </div>
</template>

<script setup lang="ts" name="promotionDialog">
import { defineAsyncComponent, onMounted, nextTick, reactive, ref } from "vue";
import { ElMessageBox, FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { CirclePlus, DocumentAdd } from "@element-plus/icons-vue";
import { usePromotionApi } from "/@/api/promotion/index";
import { getDefaultSubObject } from "/@/utils/other";

const PromotionConditionDialog = defineAsyncComponent(
  () => import("/@/views/promotion/global/components/promotionConditionDialog.vue")
);
const PromotionResultDialog = defineAsyncComponent(
  () => import("/@/views/promotion/global/components/promotionResultDialog.vue")
);

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const promotionApi = usePromotionApi();

// 定义变量内容
const promotionDialogFormRef = ref();
const promotionConditionDialogRef = ref();
const promotionResultDialogRef = ref();
const startAndEndTime = ref([] as string[]);
const promotionConditionTypeItem = ref([] as CommonKeyValueType[]);
const promotionResultTypeItem = ref([] as CommonKeyValueType[]);
const conditionCurrCode = ref("");
const resultCurrCode = ref("");
const state = reactive({
  ruleForm: {
    id: null,
    name: "",
    type: 0,
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
    promotionConditions: [] as RowPromotionConditionType[],
    promotionResults: [] as RowPromotionResultType[],
    concurrencyStamp: "",
  },
  dialog: {
    isShowDialog: false,
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

// 打开促销条件
const onOpenPromotionCondition = (promotionCondition: any) => {
  promotionConditionDialogRef.value.openDialog(state.ruleForm.id, promotionCondition);
  conditionCurrCode.value = promotionCondition == null ? "" : promotionCondition.code;
};

// 删除促销条件
const onDelPromotionCondition = (promotionCondition: RowPromotionConditionType) => {
  ElMessageBox.confirm("您确定要删除该促销条件吗?", "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    for (let i = 0; i < state.ruleForm.promotionConditions.length; i++) {
      const condition = state.ruleForm.promotionConditions[i];
      if (_.isEqual(condition, promotionCondition)) {
        state.ruleForm.promotionConditions.splice(i, 1);
        break;
      }
    }
  });
};

// 打开促销结果
const onOpenPromotionResult = (promotionResult: any) => {
  if (!promotionResult && state.ruleForm.promotionResults.length == 1) {
    ElMessage.warning("全局促销只能应用一种促销结果!");
    return;
  }
  promotionResultDialogRef.value.openDialog(state.ruleForm.id, promotionResult);
  resultCurrCode.value = promotionResult == null ? "" : promotionResult.code;
};

// 删除促销结果
const onDelPromotionResult = (promotionResult: RowPromotionResultType) => {
  ElMessageBox.confirm("您确定要删除该促销结果吗?", "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    for (let i = 0; i < state.ruleForm.promotionResults.length; i++) {
      const result = state.ruleForm.promotionResults[i];
      if (_.isEqual(result, promotionResult)) {
        state.ruleForm.promotionResults.splice(i, 1);
        break;
      }
    }
  });
};

// 打开弹窗
const openDialog = (row: RowPromotionType) => {
  // state.ruleForm = getDefaultSubObject(state.ruleForm);
  state.ruleForm.weight = 100;
  nextTick(() => {
    state.dialog.editId = row.id;
    Object.assign(state.ruleForm, row);
    if (state.ruleForm.startTime && state.ruleForm.endTime) {
      startAndEndTime.value = [state.ruleForm.startTime, state.ruleForm.endTime];
    }
    state.dialog.title = "设置参数";
    state.dialog.submitTxt = "修 改";
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

// 选择条件结果
const onConditionChange = (conditionType: RowPromotionConditionType) => {
  if (
    !conditionCurrCode.value &&
    state.ruleForm.promotionConditions.filter(
      (p) => p.code == conditionType.code && p.code != conditionCurrCode.value
    ).length > 0
  ) {
    ElMessage.warning(`代码“${conditionType.code}”已存在！`);
    return;
  }
  if (conditionCurrCode.value) {
    let condition = state.ruleForm.promotionConditions.find(
      (p) => p.code == conditionType.code
    );
    if (condition != null) {
      condition.parameters = conditionType.parameters;
    }
    return;
  }
  state.ruleForm.promotionConditions.push(conditionType);
};

// 选择条件结果
const onResultChange = (resultType: RowPromotionResultType) => {
  if (
    !resultCurrCode.value &&
    state.ruleForm.promotionResults.filter(
      (p) => p.code == resultType.code && p.code != resultCurrCode.value
    ).length > 0
  ) {
    ElMessage.warning(`代码“${resultType.code}”已存在！`);
    return;
  }
  if (resultCurrCode.value) {
    let result = state.ruleForm.promotionResults.find((p) => p.code == resultType.code);
    if (result != null) {
      result.parameters = resultType.parameters;
    }
    return;
  }
  state.ruleForm.promotionResults.push(resultType);
};

// 提交
const onSubmit = () => {
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
};

// 获取促销条件类型
const getPromotionConditionTypes = () => {
  promotionApi
    .getPromotionConditionType()
    .then((result) => {
      if (result) {
        promotionConditionTypeItem.value = result;
      }
    })
    .catch((err: any) => {
      console.error("获取促销条件类型出错：", err);
    });
};

// 获取促销结果
const getPromotionResultTypes = () => {
  promotionApi
    .getPromotionResultType()
    .then((result) => {
      if (result) {
        promotionResultTypeItem.value = result;
      }
    })
    .catch((err: any) => {
      console.error("获取促销结果类型出错：", err);
    });
};

// 显示条件名称
const showConditionTypeDesc = (key: string) => {
  let commonKeyValueType = promotionConditionTypeItem.value.find((p) => p.key == key);
  if (commonKeyValueType) {
    return commonKeyValueType.description;
  }
  return null;
};

// 显示结果名称
const showResultTypeDesc = (key: string) => {
  let commonKeyValueType = promotionResultTypeItem.value.find((p) => p.key == key);
  if (commonKeyValueType) {
    return commonKeyValueType.description;
  }
  return null;
};

// 暴露变量
defineExpose({
  openDialog,
});

// 页面加载完时
onMounted(() => {
  getPromotionConditionTypes();
  getPromotionResultTypes();
});
</script>
<style lang="scss" scoped>
.condition-table-list,
.result-table-list {
  margin: 15px auto;
}
</style>
