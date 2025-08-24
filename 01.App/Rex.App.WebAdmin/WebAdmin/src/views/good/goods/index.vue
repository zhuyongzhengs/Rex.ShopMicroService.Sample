<template>
  <div class="good-goods-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="good-goods-search">
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
          <el-col :span="1.5">
            <div class="good-search-toggle" @click="goodCount.isShow = !goodCount.isShow">
              <span>{{ goodCount.isShow ? "收筛选" : "展筛选" }}</span>
              <SvgIcon :name="goodCount.isShow ? 'ele-ArrowUp' : 'ele-ArrowDown'" />
            </div>
          </el-col>
          <el-col :span="6.5">
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
        <el-row :gutter="15" class="mt10" v-show="goodCount.isShow">
          <el-col :span="24">
            <el-radio-group v-model="activeGood" size="default">
              <el-radio-button label="全部商品">
                全部商品
                <el-tag type="info" size="small">{{ goodCount.allCount }}</el-tag>
              </el-radio-button>
              <el-radio-button label="上架商品">
                上架商品
                <el-tag type="success" size="small">{{
                  goodCount.upMarketableCount
                }}</el-tag>
              </el-radio-button>
              <el-radio-button label="下架商品">
                下架商品
                <el-tag type="warning" size="small">{{
                  goodCount.downMarketableCount
                }}</el-tag>
              </el-radio-button>
              <el-radio-button label="库存报警">
                库存报警
                <el-tag type="danger" size="small">{{
                  goodCount.stockWarningCount
                }}</el-tag>
              </el-radio-button>
            </el-radio-group>
          </el-col>
        </el-row>
      </div>
    </el-card>
    <el-card shadow="hover" class="layout-padding-auto">
      <template #header>
        <el-button
          size="default"
          v-auth="'GoodService.Goods.Create'"
          type="success"
          @click="onOpenAddGoods"
          class="mr10"
        >
          <el-icon>
            <ele-Plus />
          </el-icon>
          新增
        </el-button>
        <el-button-group>
          <el-button
            v-auth="'GoodService.Goods.Update'"
            text
            type="warning"
            size="default"
            @click="onUpdatePrice"
            class="group-btn"
          >
            修改价格
          </el-button>
          <el-button
            v-auth="'GoodService.Goods.Update'"
            text
            type="warning"
            size="default"
            @click="onUpdateStock"
            class="group-btn"
          >
            调整库存
          </el-button>
          <el-button
            v-auth="'GoodService.Goods.Update'"
            text
            type="warning"
            size="default"
            @click="onUpdateIsMarketableMany(true)"
            class="group-btn"
          >
            批量上架
          </el-button>
          <el-button
            v-auth="'GoodService.Goods.Update'"
            text
            type="warning"
            size="default"
            @click="onUpdateIsMarketableMany(false)"
            class="group-btn"
          >
            批量下架
          </el-button>
          <el-button
            v-auth="'GoodService.Goods.Update'"
            text
            type="warning"
            size="default"
            @click="onSetLabel"
            class="group-btn"
          >
            设置标签
          </el-button>
          <el-button
            v-auth="'GoodService.Goods.Delete'"
            text
            type="danger"
            size="default"
            @click="onDeleteMany"
            class="group-btn"
          >
            批量删除
          </el-button>
        </el-button-group>
      </template>
      <el-table
        ref="multipleGoodTableRef"
        :data="state.tableData.data"
        v-loading="state.tableData.loading"
        style="width: 100%"
      >
        <el-table-column fixed type="selection" width="55" />
        <el-table-column
          fixed
          prop="isMarketable"
          label="上架"
          show-overflow-tooltip
          width="90"
        >
          <template #default="scope">
            <el-switch
              v-model="scope.row.isMarketable"
              inline-prompt
              active-text="开启"
              inactive-text="关闭"
              size="default"
              @change="onIsMarketable($event, scope.row)"
            />
          </template>
        </el-table-column>
        <el-table-column
          fixed
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
              @change="onIsRecommend($event, scope.row)"
            />
          </template>
        </el-table-column>
        <el-table-column fixed prop="isHot" label="热门" show-overflow-tooltip width="90">
          <template #default="scope">
            <el-switch
              v-model="scope.row.isHot"
              inline-prompt
              active-text="开启"
              inactive-text="关闭"
              size="default"
              @change="onIsHot($event, scope.row)"
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
              :max-scale="7"
              :min-scale="0.2"
              :preview-src-list="[scope.row.image]"
              :preview-teleported="true"
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
          min-width="350"
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
        <el-table-column prop="mktPrice" label="市场价" show-overflow-tooltip width="120">
          <template #default="scope">
            <span class="mkt-price">￥{{ scope.row.mktPrice }}</span>
          </template>
        </el-table-column>
        <el-table-column prop="barCode" label="商品条码" width="165" />
        <el-table-column prop="sort" label="排序" show-overflow-tooltip width="60" />
        <el-table-column
          prop="lastModificationTime"
          label="更新时间"
          show-overflow-tooltip
          width="165"
        />
        <el-table-column fixed="right" label="操作" width="190">
          <template #default="scope">
            <el-button
              v-auth="'GoodService.Goods.Look'"
              size="small"
              text
              type="info"
              @click="onOpenLookGoods(scope.row)"
              >查看</el-button
            >
            <el-button
              v-auth="'GoodService.Goods.Update'"
              size="small"
              text
              type="primary"
              @click="onProductDetails(scope.row.id)"
              >货品信息</el-button
            >
            <el-button
              v-auth="'GoodService.Goods.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditGoods(scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'GoodService.Goods.Delete'"
              size="small"
              text
              type="danger"
              @click="onRowDel(scope.row)"
              >删除</el-button
            >
          </template>
        </el-table-column>
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
    <update-goods-price ref="updateGoodsPriceRef" @refresh="getTableData()" />
    <update-goods-stock ref="updateGoodsStockRef" @refresh="getTableData()" />
    <set-good-labels ref="setGoodLabelsRef" @refresh="getTableData()" />
    <product-detail-dialog ref="productDetailDialogRef" />
  </div>
</template>

<script setup lang="ts" name="goods">
import { watch, reactive, onMounted, ref, defineAsyncComponent } from "vue";
import { useRoute } from "vue-router";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import { useRouter } from "vue-router";
import { useGoodsApi } from "/@/api/good/goods/index";
import { useGoodCategoryApi } from "/@/api/good/category/index";
import { useGoodBrandApi } from "/@/api/good/brand/index";
import { usePlatformSettingApi } from "/@/api/shopSetting/platformSetting/index";

const UpdateGoodsPrice = defineAsyncComponent(
  () => import("/@/views/good/goods/components/updateGoodsPrice.vue")
);
const UpdateGoodsStock = defineAsyncComponent(
  () => import("/@/views/good/goods/components/updateGoodsStock.vue")
);
const SetGoodLabels = defineAsyncComponent(
  () => import("/@/views/good/goods/components/setGoodLabels.vue")
);
const ProductDetailDialog = defineAsyncComponent(
  () => import("/@/views/good/goods/components/productDetailDialog.vue")
);

// 引入商品 Api 请求接口
const goodsApi = useGoodsApi();
// 商品分类API
const goodCategoryApi = useGoodCategoryApi();
// 引入品牌 Api 请求接口
const brandApi = useGoodBrandApi();

// 定义变量内容
const route = useRoute();
const updateGoodsPriceRef = ref();
const updateGoodsStockRef = ref();
const multipleGoodTableRef = ref();
const setGoodLabelsRef = ref();
const productDetailDialogRef = ref();
const router = useRouter();
const activeGood = ref("全部商品");
const goodBrandData = ref<RowGoodBrandType[]>();
const goodCategorData = ref([] as any);
const goodCategorProp = ref({
  checkStrictly: true,
  value: "id",
  label: "name",
});
const goodCount = ref({
  isShow: false,
  allCount: 0,
  upMarketableCount: 0,
  downMarketableCount: 0,
  stockWarningCount: 0,
});
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
      isStockWarn: null,
      goodStockWarn: 0,
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

// 监听[商品]选中
watch(activeGood, (newVal, oldVal) => {
  state.tableData.param.isStockWarn = null;
  state.tableData.param.isMarketable = null;
  switch (newVal) {
    // case "全部商品":
    //   state.tableData.param.isMarketable = null;
    //   break;
    case "上架商品":
      state.tableData.param.isMarketable = true;
      break;
    case "下架商品":
      state.tableData.param.isMarketable = false;
      break;
    case "库存报警":
      state.tableData.param.isStockWarn = true;
      break;
    default:
      break;
  }
  searchGoods();
});

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

// 打开新增商品弹窗
const onOpenAddGoods = () => {
  router.push({ path: "/good/goods/create" });
};

// 打开商品库存详情
const onProductDetails = (goodId: string) => {
  if (!goodId) return;
  productDetailDialogRef.value.openDialog(goodId);
};

// 打开修改商品弹窗
const onOpenEditGoods = (row: RowGoodsType) => {
  const params: EmptyObjectType = { id: row.id };
  router.push({
    path: "/good/goods/update",
    query: params,
  });
};

// 打开查看商品弹窗
const onOpenLookGoods = (row: RowGoodsType) => {
  const params: EmptyObjectType = { id: row.id };
  router.push({
    path: "/good/goods/look",
    query: params,
  });
};

// 删除商品
const onRowDel = (row: RowGoodsType) => {
  ElMessageBox.confirm(`此操作将永久删除商品名称：“${row.name}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    goodsApi
      .deleteGoods(row.id)
      .then((result) => {
        if (result) {
          getTableData();
          ElMessage.success("删除成功");
        }
      })
      .catch((err: any) => {
        console.error("提交出错：", err);
      });
  });
};

// 获取选择的商品
const getSelectionGoods = () => {
  const selRows = multipleGoodTableRef.value.getSelectionRows() as RowGoodsType[];
  if (!selRows || selRows.length < 1) {
    return [];
  }
  return selRows;
};

// 修改价格
const onUpdatePrice = () => {
  const selRows = getSelectionGoods();
  if (selRows.length < 1) {
    ElMessage.warning("请先选择要处理的数据.");
    return;
  }
  updateGoodsPriceRef.value.openDialog(selRows);
};

// 修改库存
const onUpdateStock = () => {
  const selRows = getSelectionGoods();
  if (selRows.length < 1) {
    ElMessage.warning("请先选择要处理的数据.");
    return;
  }
  updateGoodsStockRef.value.openDialog(selRows);
};

// 批量上下架
const onUpdateIsMarketableMany = (isMarketable: boolean) => {
  const selRows = getSelectionGoods();
  if (selRows.length < 1) {
    ElMessage.warning("请先选择要处理的数据.");
    return;
  }
  ElMessageBox.confirm(`您选择了 ${selRows.length} 条数据，确定要执行该操作吗?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    goodsApi
      .updateMarketableMany({
        goodIds: selRows.map((p) => p.id),
        isMarketable,
      })
      .then((result) => {
        if (result) {
          getTableData();
          ElMessage.success("操作成功");
        }
      })
      .catch((err: any) => {
        console.error("提交出错：", err);
      });
  });
};

// 批量打标签
const onSetLabel = () => {
  const selRows = getSelectionGoods();
  if (selRows.length < 1) {
    ElMessage.warning("请先选择要处理的数据.");
    return;
  }
  setGoodLabelsRef.value.openDialog(selRows);
};

// 批量删除
const onDeleteMany = () => {
  const selRows = getSelectionGoods();
  if (selRows.length < 1) {
    ElMessage.warning("请先选择要删除的数据.");
    return;
  }
  ElMessageBox.confirm(`您选择了 ${selRows.length} 条数据，确定要删除吗?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    goodsApi
      .deleteGoodsMany(selRows.map((p) => p.id))
      .then((result) => {
        if (result) {
          getTableData();
          ElMessage.success("删除成功");
        }
      })
      .catch((err: any) => {
        console.error("提交出错：", err);
      });
  });
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

// 修改是否上架
const onIsMarketable = (val: boolean, good: RowGoodsType) => {
  goodsApi
    .updateGoodIsMarketable(good.id, val)
    .then((result) => {
      console.log("修改[" + good.name + "]是否上架显示，结果：", result);
      getGoodCounts();
    })
    .catch((err: any) => {
      good.isMarketable = !val;
      console.error("修改是否上架出错：", err);
    });
};

// 修改是否推荐
const onIsRecommend = (val: boolean, good: RowGoodsType) => {
  goodsApi
    .updateGoodIsRecommend(good.id, val)
    .then((result) => {
      console.log("修改[" + good.name + "]是否推荐显示，结果：", result);
    })
    .catch((err: any) => {
      good.isRecommend = !val;
      console.error("修改是否推荐出错：", err);
    });
};

// 修改是否热门
const onIsHot = (val: boolean, good: RowGoodsType) => {
  goodsApi
    .updateGoodIsHot(good.id, val)
    .then((result) => {
      console.log("修改[" + good.name + "]是否热门显示，结果：", result);
    })
    .catch((err: any) => {
      good.isHot = !val;
      console.error("修改是否热门出错：", err);
    });
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

// 获取平台设置
const getPlatformSetting = (node: string, callBack: Function) => {
  usePlatformSettingApi()
    .getPlatformSetting()
    .then((result: any) => {
      let settingValue = result?.goodsSettings.find((p: any) => p.name === node);
      if (!settingValue) return;
      let stocksWarnNum = 0;
      if (settingValue) stocksWarnNum = Number(settingValue.value);
      state.tableData.param.goodStockWarn = stocksWarnNum;
      callBack(stocksWarnNum);
    })
    .catch((err: any) => {
      console.error("获取平台设置信息出错：", err);
    });
};

// 获取商品数量
const getGoodCounts = () => {
  getPlatformSetting("BaseService.GoodsSetting.GoodsStocksWarn", (num: number) => {
    goodsApi
      .getGoodCount(num)
      .then((result) => {
        if (result) {
          goodCount.value.allCount = result.allCount;
          goodCount.value.upMarketableCount = result.upMarketableCount;
          goodCount.value.downMarketableCount = result.downMarketableCount;
          goodCount.value.stockWarningCount = result.stockWarningCount;
        }
      })
      .catch((err) => {
        console.error("查询获取商品数量出错：", err);
      });
  });
};

// 页面加载完时
onMounted(() => {
  getGoodBrands();
  getGoodCategorys();
  getGoodCounts();
  if (route.query.activeGood) {
    setTimeout(() => {
      let aGood = route.query.activeGood as string;
      if (aGood != "全部商品") {
        activeGood.value = aGood;
        goodCount.value.isShow = true;
      }
    }, 500);
  } else {
    getTableData();
  }
});
</script>

<style scoped lang="scss">
.good-goods-container {
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

.good-search-toggle {
  height: 32px;
  line-height: 32px;
  cursor: pointer;
  white-space: nowrap;
  user-select: none;
  display: flex;
  align-items: center;
  color: var(--el-color-primary);
}

.group-btn {
  border: 1px #ebeef5 solid;
  border-right: 1px #dcdfe6 solid !important;
}

.good-price {
  color: #f56c6c;
}

.mkt-price {
  color: #999999;
  text-decoration: line-through;
}
</style>
