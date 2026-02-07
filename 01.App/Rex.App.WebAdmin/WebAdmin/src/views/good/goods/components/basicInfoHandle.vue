<template>
  <div class="basicInfo-handle-container">
    <el-form
      ref="basicInfoFormRef"
      :model="basicInfoForm"
      :rules="formRules"
      :disabled="props.isLook"
      size="default"
      label-width="90px"
    >
      <el-row :gutter="35">
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
          <el-form-item label="商品名称" prop="name">
            <el-input
              placeholder="请输入商品名称(名称不得超过200个字符)"
              v-model="basicInfoForm.name"
              maxlength="200"
              clearable
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
          <el-form-item label="商品编码" prop="barCode">
            <el-input
              placeholder="请输入商品编码"
              v-model="basicInfoForm.barCode"
              clearable
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
          <el-form-item label="品牌" prop="brandId">
            <el-select
              v-model="basicInfoForm.brandId"
              placeholder="请选择品牌"
              class="brand-column"
            >
              <el-option
                v-for="(brand, index) in goodBrandData"
                :key="index"
                :label="brand.name"
                :value="brand.id"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
          <el-form-item label="商品分类" prop="goodCategoryId">
            <el-cascader
              :options="goodCategorData"
              :props="goodCategorProp"
              placeholder="请选择商品分类"
              clearable
              class="w100"
              v-model="basicInfoForm.goodCategoryId"
              @change="goodCategoryChange"
            >
              <template #default="{ node, data }">
                <span>{{ data.name }}</span>
                <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
              </template>
            </el-cascader>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
          <el-form-item label="扩展分类" prop="goodCategoryIdExtends">
            <el-cascader
              :options="goodCategoryExtData"
              :props="goodCategoryIdExtProp"
              placeholder="请选择扩展分类"
              clearable
              class="w100"
              v-model="goodCategoryIdExt"
              @change="goodCategoryIdExtChange"
            >
              <template #default="{ node, data }">
                <span>{{ data.name }}</span>
                <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
              </template>
            </el-cascader>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
          <el-form-item label="商品简介" prop="brief">
            <el-input
              type="textarea"
              placeholder="简单的介绍，控制在100字以内即可，不得超过250个字符~"
              maxlength="250"
              v-model="basicInfoForm.brief"
              :autosize="{ minRows: 3, maxRows: 5 }"
              clearable
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
          <el-divider content-position="left">其他参数</el-divider>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="4" :xl="24" class="mb20">
          <el-form-item label="单位" prop="unit">
            <el-input
              placeholder="单位"
              v-model="basicInfoForm.unit"
              maxlength="10"
              clearable
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="4" :xl="24" class="mb20">
          <el-form-item label="排序" prop="sort" label-width="50">
            <el-input-number v-model="basicInfoForm.sort" :precision="0" :min="0" />
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="4" :xl="24" class="mb20">
          <el-form-item label="是否上架" prop="isMarketable">
            <el-switch
              v-model="basicInfoForm.isMarketable"
              inline-prompt
              active-text="开启"
              inactive-text="关闭"
            />
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="4" :xl="24" class="mb20">
          <el-form-item label="虚拟商品" prop="isNomalVirtual">
            <el-switch
              v-model="basicInfoForm.isNomalVirtual"
              inline-prompt
              active-text="开启"
              inactive-text="关闭"
            />
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="4" :xl="24" class="mb20">
          <el-form-item label="是否推荐" prop="isRecommend">
            <el-switch
              v-model="basicInfoForm.isRecommend"
              inline-prompt
              active-text="开启"
              inactive-text="关闭"
            />
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="4" :xl="24" class="mb20">
          <el-form-item label="是否热门" prop="isHot">
            <el-switch
              v-model="basicInfoForm.isHot"
              inline-prompt
              active-text="开启"
              inactive-text="关闭"
            />
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
  </div>
</template>

<script setup lang="ts" name="basicInfoHandleDialog">
import { reactive, ref, onMounted } from "vue";
import { FormInstance, FormRules } from "element-plus";
import _ from "lodash";
import { getCode, getGuidEmpty } from "/@/utils/other";
import { useGoodCategoryApi } from "/@/api/good/category/index";
import { useGoodBrandApi } from "/@/api/good/brand/index";

// 商品分类API
const goodCategoryApi = useGoodCategoryApi();
// 引入品牌 Api 请求接口
const brandApi = useGoodBrandApi();

// 定义父组件传过来的值
const props = defineProps({
  // 是否查看
  isLook: {
    type: Boolean,
    default: () => false,
  },
});

// 定义变量内容
const emit = defineEmits(["submitFormData"]);
const basicInfoFormRef = ref();
const goodCategorProp = ref({
  checkStrictly: true,
  value: "id",
  label: "name",
});
const goodCategoryIdExtProp = ref({
  multiple: true,
  checkStrictly: true,
  value: "id",
  label: "name",
});
const goodCategorData = ref<RowGoodCategoryType[]>();
const goodCategoryExtData = ref<RowGoodCategoryType[]>();
const goodBrandData = ref<RowGoodBrandType[]>();
const goodCategoryIdExt = ref([]);
const basicInfoForm = ref({
  id: getGuidEmpty(),
  name: null, // 商品名称
  goodCategoryId: null, // 商品分类
  goodCategoryIdExtends: "", // 扩展分类
  brandId: null, // 品牌
  barCode: getCode("GD"), // 商品编码
  brief: null, // 商品简介
  unit: null, // 单位
  sort: 0, // 排序
  isMarketable: false, // 是否上架
  isNomalVirtual: false, // 虚拟商品
  isRecommend: false, // 是否推荐
  isHot: false, // 是否热门
});

// 表单验证规则
const formRules = reactive<FormRules>({
  name: [{ required: true, message: "请输入品牌名称！", trigger: "blur" }],
  barCode: [{ required: true, message: "请输入编码！", trigger: "blur" }],
  brandId: [{ required: true, message: "请选择品牌！", trigger: "blur" }],
  goodCategoryId: [{ required: true, message: "请输入商品分类！", trigger: "blur" }],
  brief: [{ required: true, message: "请输入商品简介！", trigger: "blur" }],
  unit: [{ required: true, message: "请输入单位！", trigger: "blur" }],
  sort: [{ required: true, message: "请选择排序！", trigger: "blur" }],
});

// 提交验证
const submitValidateAsync = async (formEl: FormInstance | undefined) => {
  if (!formEl) return false;
  let validResult = false;
  await formEl.validate((valid, fields) => {
    if (valid) {
      validResult = true;
      emit("submitFormData", basicInfoForm.value);
    } else {
      console.warn("未验证通过!", fields);
    }
  });
  return validResult;
};

// 获取品牌
const getGoodBrands = () => {
  brandApi
    .getGoodBrandShow({
      isShow: true,
    })
    .then((result) => {
      goodBrandData.value = result;
    })
    .catch((err: any) => {
      console.error("查询品牌出错：", err);
    });
};

// 获取商品分类
const getGoodCategorys = () => {
  goodCategoryApi
    .getGoodCategoryTree()
    .then((result) => {
      goodCategorData.value = result;
      goodCategoryExtData.value = result;
    })
    .catch((err) => {
      console.error("查询商品分类列表出错：", err);
    });
};

// 商品分类切换
function goodCategoryChange(val: []): void {
  if (!val || val.length < 1) {
    basicInfoForm.value.goodCategoryId = null;
    return;
  }
  basicInfoForm.value.goodCategoryId = val[val.length - 1];
}

// 商品分类扩展切换
function goodCategoryIdExtChange(val: []): void {
  if (!val || val.length < 1) {
    basicInfoForm.value.goodCategoryIdExtends = "";
    return;
  }
  let gCategoryArr = val.reduce((prev, cur) => prev.concat(cur), []);
  let gcArr = [...new Set(gCategoryArr)];
  basicInfoForm.value.goodCategoryIdExtends = gcArr.join(",");
}

// 设置初始值
const setInitValue = (val: any) => {
  basicInfoForm.value = val;
  if (basicInfoForm.value.goodCategoryIdExtends) {
    goodCategoryIdExt.value = basicInfoForm.value.goodCategoryIdExtends.split(",") as any;
  }
};

// 暴露变量
defineExpose({
  basicInfoFormRef,
  setInitValue,
  submitValidateAsync,
});

// 页面加载完时
onMounted(() => {
  // 获取品牌
  getGoodBrands();
  // 获取商品分类
  getGoodCategorys();
});
</script>
<style lang="scss" scoped>
.brand-column {
  width: 100%;
}
</style>
