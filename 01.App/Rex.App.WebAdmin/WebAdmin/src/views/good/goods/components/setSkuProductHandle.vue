<template>
  <div class="setSkuProduct-handle-container">
    <el-form
      ref="setSkuProductFormRef"
      :model="setSkuProductForm"
      :rules="formRules"
      :disabled="props.isLook"
      size="default"
      label-width="90px"
    >
      <div class="sku-form-box" :style="`height: ${formBoxHeight}px;`">
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb10">
            <el-form-item label="佣金设置" prop="productsDistributionType">
              <el-radio-group v-model="setSkuProductForm.productsDistributionType">
                <el-radio :label="1">全局设置</el-radio>
                <el-radio :label="2">单独设置</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb10">
            <el-form-item label="开启SKU">
              <el-switch
                v-model="isOpenSku"
                inline-prompt
                active-text="开启"
                inactive-text="关闭"
              />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" v-show="isOpenSku">
            <el-form-item label="SKU模型">
              <div class="sku-tag-content">
                <el-tag
                  size="default"
                  class="mr10"
                  effect="dark"
                  :closable="!props.isLook"
                  v-for="(sku, index) in selSkuData"
                  :key="index"
                  :disable-transitions="false"
                  @close="handleSkuClose(sku)"
                  >{{ sku.name }}</el-tag
                >
              </div>
              <el-button class="open-sku-btn" text size="small" @click="selOpenSku">
                + 选择SKU</el-button
              >
            </el-form-item>
            <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb10">
              <el-card
                class="mb10"
                shadow="never"
                v-for="(sku, index) in selSkuData"
                :key="index"
              >
                <template #header>
                  <div class="card-sku-name">{{ sku.name }}</div>
                </template>
                <div class="card-sku-content" v-if="sku.specValues">
                  <div
                    class="sku-val-model"
                    v-for="(specVal, index) in sku.specValues"
                    :key="index"
                  >
                    <div class="sku-val-switch">
                      <el-switch
                        v-model="specVal.isChecked"
                        size="large"
                        inline-prompt
                        active-text="启用"
                        inactive-text="关闭"
                      />
                    </div>
                    <div class="sku-val-input">
                      <el-input size="default" v-model="specVal.value" />
                    </div>
                    <el-divider
                      v-if="index < sku.specValues.length - 1"
                      direction="vertical"
                      border-style="dashed"
                      class="mr18 ml18"
                    />
                  </div>
                </div>
              </el-card>
            </el-col>
            <el-col v-if="selSkuData.length" :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
              <div>
                <el-button
                  type="success"
                  size="default"
                  plain
                  :icon="RefreshRight"
                  @click="onGenerateSkuProduct"
                  >生成SKU货品列表</el-button
                >
              </div>
            </el-col>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb10">
            <el-divider content-position="left">货品信息</el-divider>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb10">
            <el-form-item
              prop="goodProducts"
              label-width="0"
              v-show="setSkuProductForm.products && setSkuProductForm.products.length > 0"
            >
              <el-table :data="setSkuProductForm.products" style="width: 100%">
                <el-table-column fixed prop="isDefault" label="是否默认" width="80">
                  <template #default="scope">
                    <div class="default-column">
                      <el-checkbox
                        v-model="scope.row.isDefault"
                        size="large"
                        @change="changeIsDefault($event, scope.row)"
                      />
                    </div>
                  </template>
                </el-table-column>
                <el-table-column prop="images" label="图片" width="100">
                  <template #default="scope">
                    <el-upload
                      accept=".jpg, .jpeg, .png, .gif"
                      :action="uploadFileConfig().action"
                      :headers="uploadFileConfig().headers"
                      :multiple="false"
                      :show-file-list="false"
                      :on-success="(response: string, file: UploadFile, fileList: UploadFiles) => handleImageSuccess(response, file, fileList, scope.row)"
                    >
                      <el-image
                        class="img-logo"
                        :src="scope.row.images"
                        :zoom-rate="1.2"
                        fit="cover"
                      >
                        <template #error>
                          <div class="image-slot">
                            <el-icon><icon-picture /></el-icon>
                          </div>
                        </template>
                      </el-image>
                    </el-upload>
                  </template>
                </el-table-column>
                <el-table-column prop="sn" label="货号" width="185">
                  <template #default="scope">
                    <el-input v-model="scope.row.sn" size="default" placeholder="货号" />
                  </template>
                </el-table-column>
                <el-table-column prop="spesDesc" label="SKU" />
                <el-table-column prop="weight" label="重量(克)" width="120">
                  <template #default="scope">
                    <el-input
                      v-model="scope.row.weight"
                      size="default"
                      placeholder="重量(克)"
                      @input="
                        scope.row.weight = verifyNumberIntegerAndFloat(scope.row.weight)
                      "
                    />
                  </template>
                </el-table-column>
                <el-table-column prop="stock" label="库存" width="120">
                  <template #default="scope">
                    <el-input
                      v-model="scope.row.stock"
                      size="default"
                      placeholder="库存"
                      @input="
                        scope.row.stock = verifyNumberIntegerAndFloat(scope.row.stock)
                      "
                    />
                  </template>
                </el-table-column>
                <el-table-column prop="costPrice" label="成本价" width="120">
                  <template #default="scope">
                    <el-input
                      v-model="scope.row.costPrice"
                      size="default"
                      placeholder="成本价"
                      @input="
                        scope.row.costPrice = verifyNumberIntegerAndFloat(
                          scope.row.costPrice
                        )
                      "
                    />
                  </template>
                </el-table-column>
                <el-table-column prop="price" label="销售价" width="120">
                  <template #default="scope">
                    <el-input
                      v-model="scope.row.price"
                      size="default"
                      placeholder="销售价"
                      @input="
                        scope.row.price = verifyNumberIntegerAndFloat(scope.row.price)
                      "
                    />
                  </template>
                </el-table-column>
                <el-table-column prop="mktPrice" label="市场价" width="120">
                  <template #default="scope">
                    <el-input
                      v-model="scope.row.mktPrice"
                      size="default"
                      placeholder="市场价"
                      @input="
                        scope.row.mktPrice = verifyNumberIntegerAndFloat(
                          scope.row.mktPrice
                        )
                      "
                    />
                  </template>
                </el-table-column>
                <!--
                <el-table-column prop="levelOne" label="一级返现" width="120">
                  <template #default="scope">
                    <el-input
                      v-model="scope.row.levelOne"
                      size="default"
                      placeholder="一级返现"
                      @input="
                        scope.row.levelOne = verifyNumberIntegerAndFloat(
                          scope.row.levelOne
                        )
                      "
                    />
                  </template>
                </el-table-column>
                <el-table-column prop="levelTwo" label="二级返现" width="120">
                  <template #default="scope">
                    <el-input
                      v-model="scope.row.levelTwo"
                      size="default"
                      placeholder="二级返现"
                      @input="
                        scope.row.levelTwo = verifyNumberIntegerAndFloat(
                          scope.row.levelTwo
                        )
                      "
                    />
                  </template>
                </el-table-column>
                <el-table-column prop="levelThree" label="三级佣金" width="120">
                  <template #default="scope">
                    <el-input
                      v-model="scope.row.levelThree"
                      size="default"
                      placeholder="三级佣金"
                      @input="
                        scope.row.levelThree = verifyNumberIntegerAndFloat(
                          scope.row.levelThree
                        )
                      "
                    />
                  </template>
                </el-table-column>
                -->
                <el-table-column fixed="right" label="操作" width="60">
                  <template #default="scope">
                    <el-button
                      size="small"
                      text
                      type="danger"
                      v-if="isOpenSku"
                      @click="onRowDel(scope.row)"
                      >删除</el-button
                    >
                  </template>
                </el-table-column>
              </el-table>
            </el-form-item>
          </el-col>
        </el-row>
      </div>
    </el-form>

    <select-type-spec ref="selectTypeSpecRef" @selSkuResult="selTypeSpecResultData" />
  </div>
</template>

<script setup lang="ts" name="setSkuProductHandleDialog">
import {
  reactive,
  ref,
  watch,
  nextTick,
  defineEmits,
  onMounted,
  defineAsyncComponent,
  defineProps,
} from "vue";
import { ElMessage, FormInstance, FormRules } from "element-plus";
import _ from "lodash";
import type { UploadFile, UploadFiles } from "element-plus";
import { RefreshRight, Picture as IconPicture } from "@element-plus/icons-vue";
import { verifyNumberIntegerAndFloat } from "/@/utils/toolsValidate";
import { getCode, getGuidEmpty } from "/@/utils/other";
import { useGoodTypeSpecApi } from "/@/api/good/typeSpec/index";
import { uploadFileConfig } from "/@/utils/other";

// 引入[SKU模型]组件
const SelectTypeSpec = defineAsyncComponent(
  () => import("/@/views/good/typeSpec/components/selectTypeSpec.vue")
);

const goodTypeSpecApi = useGoodTypeSpecApi();

// 定义父组件传过来的值
const props = defineProps({
  // 是否查看
  isLook: {
    type: Boolean,
    default: () => false,
  },
});

// 定义变量内容
const emit = defineEmits(["submitFormData", "openSkuChange"]);
const selectTypeSpecRef = ref();
const setSkuProductFormRef = ref();
const isOpenSku = ref(false); // 是否开启SKU
const selSkuData = ref([] as any); // 选择的SKU数据
const formBoxHeight = ref(700); // 表单高度
const productNoSkuTableData = ref([
  {
    id: getGuidEmpty(),
    goodId: getGuidEmpty(),
    barCode: null,
    isDefault: true,
    images: "",
    sn: getCode("SN"),
    spesDesc: "",
    weight: 0,
    stock: 1000,
    freezeStock: 0,
    price: 0,
    costPrice: 0,
    mktPrice: 0,
    marketable: false,
    levelOne: 0,
    levelTwo: 0,
    levelThree: 0,
  },
]);
const productSkuTableData = ref<RowProductType[]>([]);
const setSkuProductForm = ref({
  goodSkuIds: "", // 商品SKU
  spesDesc: "", // 商品规格序列号存储
  newSpec: "", // 自定义规格名称
  productsDistributionType: 1, // 佣金设置[默认:全局设置]
  openSpec: 0, // 开启规则
  products: [] as RowProductType[],
});

watch(isOpenSku, (newVal, oldVal) => {
  productTableChange(newVal);
});

const productTableChange = (val: boolean) => {
  if (val) {
    setSkuProductForm.value.openSpec = 1;
    setSkuProductForm.value.products = productSkuTableData.value as any;
  } else {
    setSkuProductForm.value.openSpec = 0;
    setSkuProductForm.value.products = productNoSkuTableData.value as any;
  }
  emit("openSkuChange", val);
};

// 上传图片成功回调
const handleImageSuccess = (
  result: string,
  uploadFile: UploadFile,
  uploadFiles: UploadFiles,
  product: RowProductType
) => {
  if (!result) return;
  product.images = result;
};

// 表单验证规则
const formRules = reactive<FormRules>({
  goodProducts: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          !setSkuProductForm.value.products ||
          setSkuProductForm.value.products.length < 1
        ) {
          callback(new Error("无货品信息！"));
          return;
        }
        // 验证货品信息
        for (let i = 0; i < setSkuProductForm.value.products.length; i++) {
          const product = setSkuProductForm.value.products[i];
          if (!product.sn) {
            callback(new Error(`货品号[${product.sn}]不能为空！`));
            return;
          }
          product.sn = product.sn.toUpperCase();
          if (product.sn.indexOf("SN") != 0) {
            callback(new Error(`货品号[${product.sn}]必须以“SN”英文字母开头！`));
            return;
          }
          if (Number(product.stock) <= 0) {
            callback(new Error("库存不能为0！"));
            return;
          }
          if (Number(product.costPrice) <= 0) {
            callback(new Error("成本价格不能为0！"));
            return;
          }
          if (Number(product.price) <= 0) {
            callback(new Error("销售价不能为0！"));
            return;
          }
          if (
            Number(product.costPrice) <= 0 ||
            Number(product.costPrice) > Number(product.price)
          ) {
            callback(new Error(`【${product.sn}】成本价格不能为0，且不能大于销售价格！`));
            return;
          }
          if (
            Number(product.mktPrice) <= 0 ||
            Number(product.mktPrice) < Number(product.price)
          ) {
            callback(new Error(`【${product.sn}】市场价格不能为0，且不能小于销售价格！`));
            return;
          }
          // if (product.levelOne < 0 || product.levelTwo < 0 || product.levelThree < 0) {
          //   callback(new Error(`【${product.sn}】返现金额(一/二/三级佣金)不能小于0！`));
          //   return;
          // }
        }
        callback();
      },
      trigger: "blur",
    },
  ],
});

// 提交验证
const submitValidateAsync = async (formEl: FormInstance | undefined) => {
  let generateSkus = loadGenerateSkus();
  if (generateSkus.length > 0) {
    loadGoodTypeSpec(generateSkus);
  }
  if (!formEl) return false;
  let validResult = false;
  await formEl.validate((valid, fields) => {
    if (valid) {
      validResult = true;
      emit("submitFormData", setSkuProductForm.value);
    } else {
      console.warn("未验证通过!", fields);
    }
  });
  return validResult;
};

// 选择SKU
const selOpenSku = () => {
  selectTypeSpecRef.value.openDialog("选择SKU模型");
};

// 移除选择的Sku
const handleSkuClose = (tag: string) => {
  selSkuData.value.splice(selSkuData.value.indexOf(tag), 1);
  calcFormBoxHeight();
};

// 选择SKU结果
const selTypeSpecResultData = (typeSpec: any) => {
  // 默认不选中
  for (let i = 0; i < typeSpec.length; i++) {
    let typeSpecValues = typeSpec[i].specValues;
    if (!typeSpecValues || typeSpecValues.length < 1) continue;
    for (let j = 0; j < typeSpecValues.length; j++) {
      typeSpecValues[j].isChecked = false;
    }
  }

  // 筛选数据
  if (!selSkuData.value || selSkuData.value.length < 1) {
    selSkuData.value = typeSpec;
    return;
  }
  for (let i = 0; i < typeSpec.length; i++) {
    const tSpec = typeSpec[i];
    let skuVal = _.cloneDeep(tSpec);
    for (let j = 0; j < selSkuData.value.length; j++) {
      const sku = selSkuData.value[j];
      if (tSpec.name == sku.name) {
        skuVal = null;
        break;
      }
    }
    if (skuVal) selSkuData.value.push(skuVal);
  }
  calcFormBoxHeight();
};

// 生成SKU货品列表
const onGenerateSkuProduct = () => {
  // Start：验证是否选择SKU模型
  if (!selSkuData.value || selSkuData.value.length < 1) {
    ElMessage.warning("请先选择SKU模型！");
    return;
  }
  let generateSkus = loadGenerateSkus();
  if (generateSkus.length < 1) {
    ElMessage.warning("请选择要[启用]的SKU模型明细！");
    return;
  }
  // End：验证是否选择SKU模型

  goodTypeSpecApi
    .generateGoodTypeSpecSku(generateSkus)
    .then((result) => {
      if (result) {
        ElMessage.success("生成成功！");
        productSkuTableData.value = result;
        productTableChange(true);
        loadGoodTypeSpec(generateSkus);
        // 滚动到底部
        nextTick(() => {
          calcFormBoxHeight();
          let tabElement = document.getElementById("good_tabs") as any;
          tabElement.scrollTop = tabElement.scrollHeight;
        });
      }
    })
    .catch((err: any) => {
      console.error("生成出错：", err);
    });
};

// 得到生成的SKU
const loadGenerateSkus = () => {
  let generateSkus = new Array();
  for (let i = 0; i < selSkuData.value.length; i++) {
    const skuModel = selSkuData.value[i];
    if (!skuModel.specValues || skuModel.specValues.length < 1) continue;
    let skuVal = {
      id: skuModel.id,
      name: skuModel.name,
      specValues: [] as any,
    };
    for (let j = 0; j < skuModel.specValues.length; j++) {
      const specValue = skuModel.specValues[j];
      if (specValue.isChecked) {
        skuVal.specValues.push({
          id: specValue.id,
          specId: skuVal.id,
          value: specValue.value,
        });
      }
    }
    if (skuVal.specValues.length > 0) generateSkus.push(skuVal);
  }
  return generateSkus;
};

// 加载商品(SKU)类型规格
const loadGoodTypeSpec = (generateSkus: any) => {
  setSkuProductForm.value.goodSkuIds = generateSkus.map((p: any) => p.id).join(",");
  let spesDescArr = new Array();
  generateSkus.forEach((gSku: any) => {
    if (gSku.specValues && gSku.specValues.length > 0) {
      gSku.specValues.forEach((specVal: any) => {
        spesDescArr.push(`${specVal.id}.${gSku.name}:${specVal.value}`);
      });
    }
  });
  setSkuProductForm.value.spesDesc = spesDescArr.join("|");
  let newSpecArr = new Array();
  selSkuData.value.forEach((sku: any) => {
    if (sku.specValues && sku.specValues.length > 0) {
      sku.specValues.forEach((skuVal: any) => {
        newSpecArr.push(`${skuVal.id}:${skuVal.value}`);
      });
    }
  });
  setSkuProductForm.value.newSpec = newSpecArr.join("|");
};

// 默认切换
const changeIsDefault = (val: any, productTable: any) => {
  for (let i = 0; i < setSkuProductForm.value.products.length; i++) {
    const pTable = setSkuProductForm.value.products[i];
    if (pTable.sn == productTable.sn) {
      pTable.isDefault = val;
      continue;
    }
    pTable.isDefault = false;
  }
};

// 删除货品
const onRowDel = (productTable: any) => {
  for (let i = 0; i < setSkuProductForm.value.products.length; i++) {
    const pTable = setSkuProductForm.value.products[i];
    if (pTable.sn == productTable.sn) {
      setSkuProductForm.value.products.splice(i, 1);
      break;
    }
  }
};

// 设置初始值
const setInitValue = (skuVal: any) => {
  let val = _.cloneDeep(skuVal);
  if (val.products) {
    val.products.forEach((product: any) => {
      // product.good = null;
      product.productDistribution = null;
    });
  }
  if (val.openSpec == 1) {
    productSkuTableData.value = val.products;
  } else {
    productNoSkuTableData.value = val.products;
  }
  isOpenSku.value = Boolean(val.openSpec == 1);
  setSkuProductForm.value = val;
  if (isOpenSku.value && setSkuProductForm.value.goodSkuIds) {
    let skuIds = setSkuProductForm.value.goodSkuIds.split(",");
    getGoodTypeSpecType(skuIds);
  }
};

// 查询商品SKU模型
const getGoodTypeSpecType = (ids: string[]) => {
  goodTypeSpecApi
    .getGoodTypeSpecTypeByIds(ids)
    .then((result) => {
      if (result) {
        selTypeSpecResultData(result);

        // 默认选中
        if (setSkuProductForm.value.spesDesc) {
          const spesDescArr = setSkuProductForm.value.spesDesc.split("|");
          spesDescArr.forEach((sDesc: any) => {
            const valItem = sDesc.split(":")[0];
            const name = valItem.split(".")[1];
            const valId = valItem.split(".")[0];
            const val = sDesc.split(":")[1];
            selSkuData.value.forEach((selSku: any) => {
              if (name == selSku.name) {
                selSku.specValues.forEach((specVal: any) => {
                  if (valId == specVal.id) {
                    specVal.isChecked = true;
                    specVal.value = val;
                  }
                });
              }
            });
          });
        }

        //SKU货品列表
        productSkuTableData.value = setSkuProductForm.value.products as any;
      }
    })
    .catch((err: any) => {
      console.error("查询商品SKU模型出错：", err);
    });
};

const calcFormBoxHeight = () => {
  const height = document.querySelector(".good-handle-box")?.clientHeight;
  if (height) {
    formBoxHeight.value = Number(height) - 54;
  }
};

// 暴露变量
defineExpose({
  setSkuProductFormRef,
  setInitValue,
  submitValidateAsync,
});

// 页面加载完时
onMounted(() => {
  productTableChange(isOpenSku.value);
  calcFormBoxHeight();
});
</script>
<style lang="scss">
.mb10 .el-card__header {
  background-color: #f5f7fa;
}
</style>
<style scoped lang="scss">
.setSkuProduct-handle-container {
  .sku-form-box {
    overflow-y: auto;
    overflow-x: hidden;
  }

  .img-logo {
    width: 50px;
    height: 50px;
  }

  .img-logo .image-slot {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
    height: 100%;
    background: var(--el-fill-color-light);
    color: var(--el-text-color-secondary);
    font-size: 30px;
  }
  .img-logo .image-slot .el-icon {
    font-size: 30px;
  }

  .sku-tag-content {
    padding: 0px;
  }

  .open-sku-btn {
    border: 1px #dcdfe6 dashed;
  }

  .sku-val-model {
    display: inline-block;

    .sku-val-switch {
      display: inline-block;
      margin-right: 10px;
    }
    .sku-val-input {
      display: inline-block;
    }
  }
}

.default-column {
  text-align: center;
}
</style>
