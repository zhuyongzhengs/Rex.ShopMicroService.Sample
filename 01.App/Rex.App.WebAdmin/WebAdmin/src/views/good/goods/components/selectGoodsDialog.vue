<template>
  <div class="select-goods-container layout-padding">
    <el-dialog
      :title="dialog.title"
      v-model="dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="1000px"
    >
      <el-card shadow="never" class="mb15">
        <div class="select-goods-search">
          <el-row :gutter="15">
            <el-col :span="6">
              <el-input
                v-model="state.tableData.param.name"
                size="default"
                placeholder="请输入商品名称"
                clearable
              >
              </el-input>
            </el-col>
            <el-col :span="4">
              <el-select
                size="default"
                v-model="state.tableData.param.brandId"
                placeholder="请选择品牌"
              >
                <el-option
                  v-for="(brand, index) in goodBrandData"
                  :key="index"
                  :label="brand.name"
                  :value="brand.id"
                />
              </el-select>
            </el-col>
            <el-col :span="6">
              <el-cascader
                :options="goodCategorData"
                :props="goodCategorProp"
                placeholder="请选择商品分类"
                clearable
                class="w100"
                size="default"
                v-model="state.tableData.param.goodCategoryId"
                @change="goodCategoryChange"
              >
                <template #default="{ node, data }">
                  <span>{{ data.name }}</span>
                  <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
                </template>
              </el-cascader>
            </el-col>
            <el-col :span="8">
              <el-button
                id="good_search_btn"
                size="default"
                type="primary"
                @click="searchGoods"
              >
                <el-icon>
                  <ele-Search />
                </el-icon>
                查询
              </el-button>
              <el-button size="default" class="ml10" @click="onReset">
                <el-icon>
                  <ele-RefreshLeft />
                </el-icon>
                重置
              </el-button>
            </el-col>
          </el-row>
        </div>
      </el-card>
      <el-card shadow="never" class="layout-padding-auto">
        <el-table
          ref="multipleGoodTableRef"
          :data="state.tableData.data"
          v-loading="state.tableData.loading"
          style="width: 100%"
          :highlight-current-row="props.selectionType == 'single'"
          @current-change="onSelectedCurrChange"
        >
          <el-table-column
            v-if="props.selectionType == 'selection'"
            fixed
            type="selection"
            width="55"
          />
          <el-table-column prop="sort" label="排序" width="60" />
          <el-table-column
            prop="isMarketable"
            label="上架"
            show-overflow-tooltip
            width="90"
            disabled
          >
            <template #default="scope">
              <el-switch
                v-model="scope.row.isMarketable"
                inline-prompt
                active-text="开启"
                inactive-text="关闭"
                size="default"
                disabled
              />
            </template>
          </el-table-column>
          <el-table-column
            prop="isRecommend"
            label="推荐"
            show-overflow-tooltip
            width="90"
          >
            <template #default="scope">
              <el-switch
                v-model="scope.row.isRecommend"
                inline-prompt
                active-text="开启"
                inactive-text="关闭"
                size="default"
                disabled
              />
            </template>
          </el-table-column>
          <el-table-column prop="isHot" label="热门" show-overflow-tooltip width="90">
            <template #default="scope">
              <el-switch
                v-model="scope.row.isHot"
                inline-prompt
                active-text="开启"
                inactive-text="关闭"
                size="default"
                disabled
              />
            </template>
          </el-table-column>
          <el-table-column prop="brand" label="品牌" show-overflow-tooltip width="150">
            <template #default="scope">
              <span>{{ scope.row.brand.name }}</span>
            </template>
          </el-table-column>
          <el-table-column prop="images" label="封面图" show-overflow-tooltip width="150">
            <template #default="scope">
              <el-image
                class="img-logo"
                :src="scope.row.image"
                :zoom-rate="1.2"
                fit="cover"
              >
                <template #error>
                  <div class="image-slot">
                    <el-icon><icon-picture /></el-icon>
                  </div>
                </template>
              </el-image>
            </template>
          </el-table-column>
          <el-table-column
            prop="name"
            label="商品名称"
            show-overflow-tooltip
            min-width="300"
          />
          <el-table-column
            prop="goodCategory"
            label="分类"
            show-overflow-tooltip
            width="150"
          >
            <template #default="scope">
              <span>{{ scope.row.goodCategory.name }}</span>
            </template>
          </el-table-column>
          <el-table-column
            prop="productsDistributionType"
            label="佣金方式"
            show-overflow-tooltip
            width="150"
          >
            <template #default="scope">
              <el-tag size="default" v-if="scope.row.productsDistributionType == 1"
                >全局设置</el-tag
              >
              <el-tag size="default" type="info" v-else>单独设置</el-tag>
            </template>
          </el-table-column>
          <el-table-column
            prop="costPrice"
            label="成本价"
            show-overflow-tooltip
            width="120"
          >
            <template #default="scope">
              <span>￥{{ scope.row.costPrice }}</span>
            </template>
          </el-table-column>
          <el-table-column prop="price" label="销售价" show-overflow-tooltip width="120">
            <template #default="scope">
              <span class="good-price">￥{{ scope.row.price }}</span>
            </template>
          </el-table-column>
          <el-table-column
            prop="mktPrice"
            label="市场价"
            show-overflow-tooltip
            width="120"
          >
            <template #default="scope">
              <span class="mkt-price">￥{{ scope.row.mktPrice }}</span>
            </template>
          </el-table-column>
          <el-table-column
            prop="lastModificationTime"
            label="更新时间"
            show-overflow-tooltip
            width="165"
          />
        </el-table>
        <el-pagination
          @size-change="onHandleSizeChange"
          @current-change="onHandleCurrentChange"
          class="mt15"
          v-model:current-page="state.tableData.currentPage"
          background
          v-model:page-size="state.tableData.param.maxResultCount"
          layout="total, sizes, prev, pager, next, jumper"
          :total="state.tableData.total"
        >
        </el-pagination>
      </el-card>
      <template #footer>
        <span class="dialog-footer">
          <el-button size="default" @click="onCancel">取 消</el-button>
          <el-button type="primary" size="default" @click="onConfirm">确 定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="selectGoods">
import { reactive, onMounted, ref, nextTick, defineProps } from "vue";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useGoodsApi } from "/@/api/good/goods/index";
import { useGoodCategoryApi } from "/@/api/good/category/index";
import { useGoodBrandApi } from "/@/api/good/brand/index";

// 引入Api请求接口
const goodsApi = useGoodsApi();
const goodCategoryApi = useGoodCategoryApi();
const brandApi = useGoodBrandApi();

const emit = defineEmits(["selGoodsResult"]);
const props = defineProps({
  selectionType: {
    type: String,
    default: () => "selection", // 默认多选
  },
});

// 定义变量内容
const multipleGoodTableRef = ref();
const goodBrandData = ref<RowGoodBrandType[]>();
const goodCategorData = ref<RowGoodCategoryType[]>();
const goodCategorProp = ref({
  checkStrictly: true,
  value: "id",
  label: "name",
});
const currentGoods = ref({} as RowGoodsType);
const state = reactive<GoodsState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      isMarketable: null,
      name: null,
      goodCategoryId: null,
      brandId: null,
      sorting: "Sort",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  goodsApi
    .getGoodsList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询商品列表出错：", err);
    });
};

// 搜索
const searchGoods = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.name = null;
  state.tableData.param.goodCategoryId = null;
  state.tableData.param.brandId = null;
};

// 分页改变
const onHandleSizeChange = (val: number) => {
  state.tableData.param.maxResultCount = val;
  getTableData();
};

// 分页改变
const onHandleCurrentChange = (val: number) => {
  state.tableData.currentPage = val;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  getTableData();
};

// 获取品牌
const getGoodBrands = () => {
  brandApi
    .getGoodBrandShow()
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
    })
    .catch((err) => {
      console.error("查询商品分类列表出错：", err);
    });
};

// 商品分类切换
function goodCategoryChange(val: []): void {
  if (!val || val.length < 1) {
    state.tableData.param.goodCategoryId = null;
    return;
  }
  state.tableData.param.goodCategoryId = val[val.length - 1];
}

// 选择(单选)当前
const onSelectedCurrChange = (good: RowGoodsType) => {
  currentGoods.value = good;
};

// 对话框
const dialog = reactive({
  isShowDialog: false,
  title: "选择商品信息",
});

// 打开弹窗
const openDialog = (title: string) => {
  if (title) dialog.title = title;
  nextTick(() => {
    dialog.isShowDialog = true;
    // 查询商品信息
    getTableData();
  });
};

// 关闭弹窗
const closeDialog = () => {
  dialog.isShowDialog = false;
};

// 确定
const onConfirm = () => {
  const selRows = multipleGoodTableRef.value.getSelectionRows() as RowGoodsType[];
  if (selRows.length < 1 && props.selectionType == "selection") {
    ElMessage.warning("您还未选择商品！");
    return;
  }
  if (!currentGoods.value?.id && props.selectionType == "single") {
    ElMessage.warning("您还未选择商品！");
    return;
  }
  emit("selGoodsResult", props.selectionType == "single" ? currentGoods.value : selRows);
  closeDialog();
};

// 取消
const onCancel = () => {
  closeDialog();
};

// 暴露变量
defineExpose({
  openDialog,
});

// 页面加载完时
onMounted(() => {
  getGoodBrands();
  getGoodCategorys();
  getTableData();
});
</script>

<style scoped lang="scss">
.select-goods-container {
  :deep(.el-card__body) {
    display: flex;
    flex-direction: column;
    flex: 1;
    overflow: auto;
    .el-table {
      flex: 1;
    }
  }
}

.img-logo {
  width: 35px;
  height: 35px;
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

.good-price {
  color: #f56c6c;
}

.mkt-price {
  color: #999999;
  text-decoration: line-through;
}
</style>
