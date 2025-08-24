<template>
  <div class="param-handle-container">
    <el-form
      ref="paramFormRef"
      :model="paramForm"
      :rules="formRules"
      :disabled="props.isLook"
      size="default"
      label-width="120px"
    >
      <el-row :gutter="35">
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
          <el-form-item label="参数模型选择" prop="goodParamsIds">
            <div class="sku-tag-content">
              <el-tag
                size="default"
                class="mr10"
                effect="dark"
                :closable="!props.isLook"
                v-for="(param, index) in selParamData"
                :key="index"
                :disable-transitions="false"
                @close="handleParamClose(index)"
                >{{ param.name }}</el-tag
              >
            </div>
            <el-button class="open-sku-btn" text size="small" @click="selOpenParam">
              + 选择参数模型</el-button
            >
          </el-form-item>
        </el-col>
        <el-divider />
        <el-col
          v-for="(param, index) in selParamData"
          :key="index"
          :xs="24"
          :sm="24"
          :md="24"
          :lg="24"
          :xl="24"
          class="mb20"
        >
          <el-form-item
            class="param-item"
            :label="param.name"
            :rules="[
              { required: true, message: `${param.name}不能为空`, trigger: 'blur' },
            ]"
          >
            <template
              v-if="param.values && param.values.length > 0 && param.type == 'Checkbox'"
            >
              <el-checkbox
                v-for="(val, vIndex) in param.values"
                :key="vIndex"
                :label="val.value"
                v-model="val.isCheckBox"
              />
            </template>
            <template
              v-if="param.values && param.values.length > 0 && param.type == 'Radio'"
            >
              <el-radio-group v-model="param.value">
                <el-radio
                  v-for="(val, vIndex) in param.values"
                  :key="vIndex"
                  :label="val"
                  >{{ val }}</el-radio
                >
              </el-radio-group>
            </template>
            <template v-if="param.type == 'Text'">
              <el-input
                v-model="param.value"
                :placeholder="'请输入' + param.name"
                clearable
              ></el-input>
            </template>
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
    <select-set-param ref="selectSetParamRef" @selParamResult="selParamResultData" />
  </div>
</template>

<script setup lang="ts" name="paramHandleDialog">
import {
  reactive,
  ref,
  defineEmits,
  onMounted,
  defineAsyncComponent,
  defineProps,
} from "vue";
import { ElMessage, FormInstance, FormRules } from "element-plus";
import _ from "lodash";
import { useGoodParamApi } from "/@/api/good/param/index";

// 引入[SKU模型]组件
const SelectSetParam = defineAsyncComponent(
  () => import("/@/views/good/param/components/selectParam.vue")
);

// 定义父组件传过来的值
const props = defineProps({
  // 是否查看
  isLook: {
    type: Boolean,
    default: () => false,
  },
});

// 定义变量内容
const paramApi = useGoodParamApi();
const emit = defineEmits(["submitFormData"]);
const paramFormRef = ref();
const selectSetParamRef = ref();
const selParamData = ref([] as any); // 参数模型数据
const isOpenSku = ref(false); // 是否开启SKU
const paramForm = ref({
  goodParamsIds: "", // 参数ID
  parameters: "", // 参数
});

// 表单验证规则
const formRules = reactive<FormRules>({
  goodParamsIds: [
    // { required: true, message: "请选择参数模型！", trigger: "blur" },
    {
      validator: (rule: any, value: any, callback: any) => {
        let isFailed = false;
        // if (!paramForm.value.parameters) isFailed = true;
        if (paramForm.value.parameters) {
          let paramArr = paramForm.value.parameters.split("|");
          for (let i = 0; i < paramArr.length; i++) {
            if (paramArr[i].split(":").length <= 1 || !paramArr[i].split(":")[1]) {
              isFailed = true;
              break;
            }
          }
        }
        if (isFailed) callback(new Error("参数模型信息未填写完！"));
        else callback();
      },
      trigger: "blur",
    },
  ],
});

// 提交验证
const submitValidateAsync = async (formEl: FormInstance | undefined) => {
  loadParamForm();
  if (!formEl) return false;
  let validResult = false;
  await formEl.validate((valid, fields) => {
    if (valid) {
      validResult = true;
      emit("submitFormData", paramForm.value);
    } else {
      console.warn("未验证通过!", fields);
    }
  });
  return validResult;
};

// 移除选择的参数模型
const handleParamClose = (index: number) => {
  selParamData.value.splice(index, 1);
  loadParamForm();
};

// 选择参数模型
const selOpenParam = () => {
  if (!isOpenSku.value) {
    ElMessage.warning("请先在【SKU/货品设置】中[启用SKU]！");
    return;
  }
  selectSetParamRef.value.openDialog("选择参数模型");
};

// 选择参数模型结果
const selParamResultData = (params: any) => {
  for (let i = 0; i < params.length; i++) {
    let param = params[i];
    param.value = "";
    if (!param.values || param.values.length < 1) continue;

    // 复选框
    if (param.type == "Checkbox") {
      for (let j = 0; j < param.values.length; j++) {
        let pValue = _.cloneDeep(param.values[j]);
        param.values[j] = {
          isCheckBox: false,
          value: pValue,
        };
      }
    }

    // 单选框
    if (param.type == "Radio") {
      param.value = param.values[0];
    }
  }
  if (!selParamData.value && selParamData.value.length < 1) {
    selParamData.value = params;
    return;
  }
  for (let i = 0; i < params.length; i++) {
    const param = params[i];
    let isAddParam = true;
    for (let j = 0; j < selParamData.value.length; j++) {
      const selParam = selParamData.value[j];
      if (param.name == selParam.name) {
        isAddParam = false;
        break;
      }
    }
    if (isAddParam) selParamData.value.push(param);
  }
};

// 加载值
const loadParamForm = () => {
  if (!selParamData.value || selParamData.value.length < 1) return;
  paramForm.value.goodParamsIds = selParamData.value.map((p: any) => p.id).join(",");
  let paramArr = new Array();
  selParamData.value.forEach((param: any) => {
    if (param.type == "Checkbox" && param.values && param.values.length > 0) {
      let pVals = new Array();
      param.values.forEach((val: any) => {
        if (val.isCheckBox) {
          pVals.push(val.value);
        }
      });
      paramArr.push(`${param.id}:${pVals.join(",")}`);
    } else {
      paramArr.push(`${param.id}:${param.value}`);
    }
  });
  paramForm.value.parameters = paramArr.join("|");
};

// 查询参数模型
const getParamInfo = (ids: string[]) => {
  paramApi
    .getGoodParamTypeByIds(ids)
    .then((result) => {
      if (result) {
        selParamResultData(result);

        // 默认值
        if (paramForm.value.parameters) {
          const params = paramForm.value.parameters.split("|");
          params.forEach((param: any) => {
            const valItem = param.split(":");
            const valId = valItem[0];
            selParamData.value.forEach((selParam: any) => {
              if (selParam.id == valId) {
                selParam.value = "";
                if (selParam.type == "Checkbox") {
                  const vals = valItem[1].split(",");
                  vals.forEach((val: any) => {
                    selParam.values.forEach((selVal: any) => {
                      if (val == selVal.value) {
                        selVal.isCheckBox = true;
                      }
                    });
                  });
                } else {
                  selParam.value = valItem[1];
                }
              }
            });
          });
        }
      }
    })
    .catch((err: any) => {
      console.error("查询参数类型出错：", err);
    });
};

// 设置初始值
const setInitValue = (val: any) => {
  paramForm.value = val;
  if (paramForm.value.goodParamsIds) {
    const gParamIds = paramForm.value.goodParamsIds.split(",");
    getParamInfo(gParamIds);
  }
};

// 设置OpenSku
const setIsOpenSku = (val: boolean) => {
  isOpenSku.value = val;
};

// 暴露变量
defineExpose({
  paramFormRef,
  setInitValue,
  submitValidateAsync,
  setIsOpenSku,
});

// 页面加载完时
onMounted(() => {
  // ...
});
</script>
<style scoped lang="scss">
.sku-tag-content {
  padding: 0px;
}

.open-sku-btn {
  border: 1px #dcdfe6 dashed;
}

.param-item {
  width: 100%;
  background-color: #f5f7fa;
  padding: 10px;
  border-radius: 5px;
}
</style>
