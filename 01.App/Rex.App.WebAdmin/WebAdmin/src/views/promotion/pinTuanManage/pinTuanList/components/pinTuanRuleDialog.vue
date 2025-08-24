<template>
  <div class="promotion-pinTuanRule-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="750px"
    >
      <el-form
        ref="pinTuanRuleDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="80px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="活动名称" prop="name">
              <el-input
                v-model="state.ruleForm.name"
                placeholder="请输入活动名称"
                maxlength="240"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="商品" prop="goodIds">
              <el-button
                :loading="selGoodLoading"
                type="info"
                size="default"
                plain
                :icon="Sell"
                @click="onOpenSelectGoods"
              >
                选择商品
              </el-button>
              <div class="ml15" v-if="selectGoodName">
                <el-tag closable @close="handleSelGoodClose">{{ selectGoodName }}</el-tag>
              </div>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="16" :xl="24" class="mb20">
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
            <el-form-item label="是否开启" prop="isStatusOpen">
              <el-switch
                v-model="state.ruleForm.isStatusOpen"
                inline-prompt
                active-text="是"
                inactive-text="否"
                size="default"
              />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24" class="mb20">
            <el-form-item label="优惠金额" prop="discountAmount">
              <el-input
                v-model="state.ruleForm.discountAmount"
                size="default"
                placeholder="请输入优惠金额"
                @input="
                  state.ruleForm.discountAmount = verifyNumberIntegerAndFloat(
                    state.ruleForm.discountAmount
                  )
                "
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24" class="mb20">
            <el-form-item label="开团人数" prop="peopleNumber">
              <el-tooltip placement="bottom">
                <template #content>2~10人</template>
                <el-input-number
                  v-model="state.ruleForm.peopleNumber"
                  :precision="0"
                  :min="2"
                  :max="10"
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
                />
              </el-tooltip>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24" class="mb20">
            <el-form-item label="总量" prop="maxGoodsNums">
              <el-tooltip placement="bottom">
                <template #content>每个活动商品总量！0为不限制，以商品库存准。</template>
                <el-input-number
                  v-model="state.ruleForm.maxGoodsNums"
                  :precision="0"
                  :min="0"
                  :max="999999"
                />
              </el-tooltip>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24" class="mb20">
            <el-form-item label="有效时长" prop="significantInterval">
              <el-tooltip placement="bottom">
                <template #content>单位：分钟</template>
                <el-input-number
                  v-model="state.ruleForm.significantInterval"
                  :precision="0"
                  :min="0"
                  :max="999999"
                />
              </el-tooltip>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24" class="mb20">
            <el-form-item label="排序" prop="sort">
              <el-input-number
                v-model="state.ruleForm.sort"
                :precision="0"
                :min="0"
                :max="999999"
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
            @click="submitValidate(pinTuanRuleDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
    <select-goods
      ref="selectGoodsRef"
      selectionType="single"
      @selGoodsResult="onSelectGoodsResult"
    />
  </div>
</template>

<script setup lang="ts" name="pinTuanRuleDialog">
import { defineAsyncComponent, nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import { Sell } from "@element-plus/icons-vue";
import _ from "lodash";
import { verifyNumberIntegerAndFloat } from "/@/utils/toolsValidate";
import { usePinTuanRuleApi } from "/@/api/promotion/pinTuanRule/index";
import { getDefaultSubObject } from "/@/utils/other";

const SelectGoods = defineAsyncComponent(
  () => import("/@/views/good/goods/components/selectGoodsDialog.vue")
);

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const pinTuanRuleApi = usePinTuanRuleApi();

// 定义变量内容
const pinTuanRuleDialogFormRef = ref();
const selectGoodsRef = ref();
const selectGoodName = ref("");
const startAndEndTime = ref([] as string[]);
const selGoodLoading = ref(false);
const state = reactive({
  ruleForm: {
    name: "",
    startTime: "",
    endTime: "",
    maxNums: 0,
    maxGoodsNums: 0,
    peopleNumber: 0,
    significantInterval: 0,
    discountAmount: "",
    sort: 0,
    isStatusOpen: true,
    goodIds: [] as string[],
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
  name: [{ required: true, message: "请输入活动名称！", trigger: "blur" }],
  startTime: [{ required: true, message: "请选择起止时间！", trigger: "blur" }],
  goodIds: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (!selectGoodName) {
          callback(new Error("请选择商品！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  peopleNumber: [
    { required: true, message: "请输入开团人数！", trigger: "blur" },
    {
      validator: (rule: any, value: number, callback: any) => {
        if (value < 2 && value > 10) {
          callback(new Error("开团人数为2~10人！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  discountAmount: [{ required: true, message: "请输入优惠金额！", trigger: "blur" }],
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
const openDialog = (type: string, row: RowPinTuanRuleType) => {
  state.ruleForm = getDefaultSubObject(state.ruleForm);
  state.ruleForm.peopleNumber = 2;
  state.ruleForm.significantInterval = 2;
  state.ruleForm.sort = 1;
  state.ruleForm.isStatusOpen = true;
  nextTick(() => {
    state.dialog.editId = "";
    if (type === "edit") {
      if (state.ruleForm.startTime && state.ruleForm.endTime) {
        startAndEndTime.value = [state.ruleForm.startTime, state.ruleForm.endTime];
      }
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      getPinTuanGood(row.id);
      state.dialog.title = "修改拼团活动";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增拼团活动";
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

// 查询拼团商品信息
const getPinTuanGood = (ruleId: string) => {
  selGoodLoading.value = true;
  pinTuanRuleApi
    .getPinTuanGoodByRuleId([ruleId])
    .then((result) => {
      if (result && result.length > 0) {
        selectGoodName.value = result[0].goodName;
      }
      selGoodLoading.value = false;
    })
    .catch((err: any) => {
      console.error("查询拼团商品信息出错：", err);
      selGoodLoading.value = false;
    });
};

// 提交
const onSubmit = () => {
  if (state.dialog.type == "edit") {
    // 修改
    pinTuanRuleApi
      .updatePinTuanRule(state.dialog.editId, state.ruleForm)
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
  pinTuanRuleApi
    .addPinTuanRule(state.ruleForm)
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

// 打开选择商品
const onOpenSelectGoods = () => {
  selectGoodsRef.value.openDialog("选择商品");
};

// 选择的商品返回结果
const onSelectGoodsResult = (goods: RowGoodsType) => {
  state.ruleForm.goodIds = [];
  state.ruleForm.goodIds = [goods.id];
  selectGoodName.value = goods.name;
};

// 移除选择的商品
const handleSelGoodClose = () => {
  selectGoodName.value = "";
  state.ruleForm.goodIds = [];
};

// 暴露变量
defineExpose({
  openDialog,
});
</script>
<style lang="scss" scoped>
// ...
</style>
