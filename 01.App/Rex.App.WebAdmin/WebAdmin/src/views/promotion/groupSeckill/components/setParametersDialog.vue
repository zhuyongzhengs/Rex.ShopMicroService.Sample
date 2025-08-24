<template>
  <div class="promotion-groupSeckill-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="800px"
    >
      <el-form
        ref="groupSeckillDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="100px"
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
              <el-radio-group :disabled="true" v-model="state.ruleForm.type" class="ml-4">
                <el-radio :label="3" size="default">团购</el-radio>
                <el-radio :label="4" size="default">秒杀</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          -->
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb15">
            <el-form-item label="商品">
              <el-button
                type="success"
                :icon="Sell"
                size="small"
                @click="onOpenSelectGoods"
                class="mr10"
                >选择商品</el-button
              >
              <el-tag
                effect="light"
                v-if="selectedGoodItem.id"
                closable
                @close="handleSelGoodClose"
              >
                {{ selectedGoodItem.name }}
              </el-tag>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
            <el-form-item label="促销结果">
              <el-button
                type="success"
                :icon="DocumentAdd"
                size="small"
                @click="onOpenGroupSeckillResult(null)"
                >添加结果</el-button
              >
            </el-form-item>
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
                    @click="onOpenGroupSeckillResult(scope.row)"
                    >编辑</el-button
                  >
                  <el-button
                    type="danger"
                    size="small"
                    @click="onDelGroupSeckillResult(scope.row)"
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
            @click="submitValidate(groupSeckillDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
    <groupSeckill-result-dialog
      :resultTypes="groupSeckillResultTypeItem"
      ref="groupSeckillResultDialogRef"
      @result-change="onResultChange"
    />
    <select-goods
      selectionType="single"
      ref="selectGoodsRef"
      @selGoodsResult="onSelectGoodsResult"
    />
  </div>
</template>

<script setup lang="ts" name="groupSeckillDialog">
import { defineAsyncComponent, onMounted, nextTick, reactive, ref } from "vue";
import { ElMessageBox, FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { Sell, DocumentAdd } from "@element-plus/icons-vue";
import { useGroupSeckillApi } from "/@/api/promotion/groupSeckill/index";

const SelectGoods = defineAsyncComponent(
  () => import("/@/views/good/goods/components/selectGoodsDialog.vue")
);

const GroupSeckillResultDialog = defineAsyncComponent(
  () => import("/@/views/promotion/groupSeckill/components/groupSeckillResultDialog.vue")
);

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const groupSeckillApi = useGroupSeckillApi();
import { useGoodsApi } from "/@/api/good/goods/index";

// 定义变量内容
const goodsApi = useGoodsApi();
const selectGoodsRef = ref();
const groupSeckillDialogFormRef = ref();
const groupSeckillResultDialogRef = ref();
const startAndEndTime = ref([] as string[]);
const selectedGoodItem = ref({} as RowGoodsType);
const groupSeckillResultTypeItem = ref<CommonKeyValueType[]>([]);
const resultCurrCode = ref("");
const state = reactive({
  ruleForm: {
    id: null,
    name: "",
    type: 4,
    weight: 100,
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
  // 参数：商品ID
  goodsIdsParameter: {
    goodsId: "",
    nums: 1,
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
  name: [{ required: true, message: "请输入团购|秒杀名称！", trigger: "blur" }],
  startTime: [{ required: true, message: "请选择起止时间！", trigger: "blur" }],
});

// 提交验证
const submitValidate = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;
  state.ruleForm.parameters = JSON.stringify(state.goodsIdsParameter);
  await formEl.validate((valid, fields) => {
    if (valid) {
      onSubmit();
    } else {
      console.warn("未验证通过!", fields);
    }
  });
};

// 打开选择商品
const onOpenSelectGoods = () => {
  selectGoodsRef.value.openDialog("选择商品");
};

// 选择的商品返回结果
const onSelectGoodsResult = (goods: RowGoodsType) => {
  selectedGoodItem.value = goods;
  state.goodsIdsParameter.goodsId = selectedGoodItem.value.id;
};

// 移除选择的商品
const handleSelGoodClose = () => {
  selectedGoodItem.value = {} as RowGoodsType;
  state.goodsIdsParameter.goodsId = "";
};

// 打开团购|秒杀结果
const onOpenGroupSeckillResult = (groupSeckillResult: any) => {
  if (!groupSeckillResult && state.ruleForm.promotionResults.length == 1) {
    ElMessage.warning("商品秒杀只能应用一种秒杀结果!");
    return;
  }
  groupSeckillResultDialogRef.value.openDialog(state.ruleForm.id, groupSeckillResult);
  resultCurrCode.value = groupSeckillResult == null ? "" : groupSeckillResult.code;
};

// 删除团购|秒杀结果
const onDelGroupSeckillResult = (groupSeckillResult: RowPromotionResultType) => {
  ElMessageBox.confirm("您确定要删除该团购|秒杀结果吗?", "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    for (let i = 0; i < state.ruleForm.promotionResults.length; i++) {
      const result = state.ruleForm.promotionResults[i];
      if (_.isEqual(result, groupSeckillResult)) {
        state.ruleForm.promotionResults.splice(i, 1);
        break;
      }
    }
  });
};

// 打开弹窗
const openDialog = (row: RowPromotionType) => {
  resetForm();
  Object.assign(state.ruleForm, row);
  nextTick(() => {
    if (row.parameters) {
      state.goodsIdsParameter = JSON.parse(row.parameters);
      if (state.goodsIdsParameter.goodsId) {
        getGoodByIds([state.goodsIdsParameter.goodsId]);
      }
    }
    if (state.ruleForm.startTime && state.ruleForm.endTime) {
      startAndEndTime.value = [state.ruleForm.startTime, state.ruleForm.endTime];
    }
    state.dialog.editId = row.id;
    state.dialog.title = "设置参数";
    state.dialog.submitTxt = "修 改";
    state.dialog.isShowDialog = true;
  });
};

// 重置表单
const resetForm = () => {
  Object.assign(state.ruleForm, {
    id: null,
    name: "",
    type: 4,
    weight: 100,
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
  });
  handleSelGoodClose();
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
};

// 根据商品ID查询商品
const getGoodByIds = (ids: string[]) => {
  goodsApi
    .getGoodByIds(ids)
    .then((result) => {
      if (result) {
        if (result && result.length == 1) {
          selectedGoodItem.value = result[0];
        }
      }
    })
    .catch((err: any) => {
      console.error("查询商品信息出错：", err);
    });
};

// 获取团购|秒杀结果
const getGroupSeckillResultTypes = () => {
  groupSeckillApi
    .getGroupSeckillResultType()
    .then((result) => {
      if (result) {
        groupSeckillResultTypeItem.value = result.filter(
          (p: any) => p.value == "Goods"
        ) as CommonKeyValueType[];
      }
    })
    .catch((err: any) => {
      console.error("获取团购|秒杀结果类型出错：", err);
    });
};

// 显示结果名称
const showResultTypeDesc = (key: string) => {
  let commonKeyValueType = groupSeckillResultTypeItem.value.find((p) => p.key == key);
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
  getGroupSeckillResultTypes();
});
</script>
<style lang="scss" scoped>
.condition-table-list,
.result-table-list {
  margin: 15px auto;
}
</style>
