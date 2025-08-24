<template>
  <div class="grade-handle-container">
    <blockquote class="quote-content">
      会员价为0时，使用默认价格。大于0时，商品实际价格=货品销售价-会员价
    </blockquote>
    <el-form
      ref="gradeFormRef"
      :model="gradeForm"
      :rules="formRules"
      :disabled="props.isLook"
      size="default"
      label-width="90px"
    >
      <el-form-item
        class="grade-item"
        v-for="(gPrice, index) in gradeForm.goodGrades"
        :key="index"
        :label="gPrice.gradeTitle"
        prop="grades"
      >
        <el-input
          placeholder="会员优惠"
          v-model="gPrice.gradePrice"
          @input="
            gPrice.gradePrice = Number(
              verifyNumberIntegerAndFloat(String(gPrice.gradePrice))
            )
          "
          clearable
        ></el-input>
      </el-form-item>
    </el-form>
  </div>
</template>

<script setup lang="ts" name="gradeHandleDialog">
import { reactive, ref, defineEmits, onMounted } from "vue";
import { FormInstance, FormRules } from "element-plus";
import _ from "lodash";
import { getGuidEmpty } from "/@/utils/other";
import { verifyNumberIntegerAndFloat } from "/@/utils/toolsValidate";
import { useUserGradeApi } from "/@/api/system/userGrade/index";

// 定义父组件传过来的值
const props = defineProps({
  // 是否查看
  isLook: {
    type: Boolean,
    default: () => false,
  },
});

// 定义变量内容
const userGradeApi = useUserGradeApi();
const emit = defineEmits(["submitFormData"]);
const gradeFormRef = ref();
const gradeForm = ref({
  goodGrades: [] as RowGoodGradeType[], // 会员价格
});

// 表单验证规则
const formRules = reactive<FormRules>({
  grades: [
    {
      required: true,
      message: "请填写会员折扣！",
      validator: (rule: any, value: any, callback: any) => {
        let isFailed = false;
        if (!gradeForm.value.goodGrades || gradeForm.value.goodGrades.length < 1)
          isFailed = true;
        if (!isFailed) {
          for (let i = 0; i < gradeForm.value.goodGrades.length; i++) {
            const price = gradeForm.value.goodGrades[i].gradePrice;
            if (price == null || typeof price == "undefined") {
              isFailed = true;
              break;
            }
          }
        }
        if (isFailed) callback(new Error("请填写会员折扣！"));
        else callback();
      },
      trigger: "blur",
    },
  ],
});

// 提交验证
const submitValidateAsync = async (formEl: FormInstance | undefined) => {
  if (!formEl) return false;
  let validResult = false;
  await formEl.validate((valid, fields) => {
    if (valid) {
      validResult = true;
      emit("submitFormData", gradeForm.value.goodGrades);
    } else {
      console.warn("未验证通过!", fields);
    }
  });
  return validResult;
};

// 查询用户等级
const getUserGrade = () => {
  userGradeApi
    .getUserGradeList({
      title: "",
      sorting: "creationTime desc",
      skipCount: 0,
      maxResultCount: 100,
    })
    .then((result) => {
      gradeForm.value.goodGrades = [];
      for (let i = 0; i < result.items.length; i++) {
        const grade = result.items[i];
        gradeForm.value.goodGrades.push({
          id: getGuidEmpty(),
          tenantId: null,
          goodId: getGuidEmpty(),
          gradeId: grade.id,
          gradeTitle: grade.title,
          gradePrice: 0,
          concurrencyStamp: null,
          creationTime: null,
        });
      }
    })
    .catch((err: any) => {
      console.error("查询用户等级出错：", err);
    });
};

// 设置初始值
const setInitValue = (val: any) => {
  gradeForm.value.goodGrades.forEach((grade: any) => {
    val.forEach((selGade: any) => {
      if (grade.gradeId == selGade.gradeId) {
        grade.id = selGade.id;
        grade.goodId = selGade.goodId;
        grade.gradePrice = selGade.gradePrice;
        grade.concurrencyStamp = selGade.concurrencyStamp;
        grade.creationTime = selGade.creationTime;
      }
    });
  });
};

// 暴露变量
defineExpose({
  gradeFormRef,
  setInitValue,
  submitValidateAsync,
});

// 页面加载完时
onMounted(() => {
  // 查询用户等级
  getUserGrade();
});
</script>

<style scoped lang="scss">
.quote-content {
  padding: 15px;
  background-color: #ecf5ff;
  border-radius: 4px;
  border-left: 5px solid var(--el-color-primary);
  margin-bottom: 20px;
}

.grade-item {
  display: inline-flex;
  margin-right: 10px;
  margin-bottom: 10px;
  width: 260px;
}
</style>
