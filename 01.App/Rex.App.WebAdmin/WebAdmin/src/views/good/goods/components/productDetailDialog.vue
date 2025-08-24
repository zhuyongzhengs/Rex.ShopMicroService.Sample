<template>
  <div class="product-detail-container layout-padding">
    <el-dialog
      :title="dialog.title"
      v-model="dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      center
      width="1000px"
    >
      <el-table
        :data="productStockTable.data"
        v-loading="productStockTable.loading"
        style="width: 100%"
        height="300"
      >
        <el-table-column type="index" fixed label="序号" width="60" />
        <el-table-column prop="sn" fixed label="货品条码" width="170" />
        <el-table-column prop="images" fixed label="图片" width="100">
          <template #default="scope">
            <el-image
              class="img-logo"
              fit="cover"
              :src="scope.row.images"
              :zoom-rate="1.2"
              @click="showImage(scope.$index)"
            >
              <template #error>
                <div class="image-slot">
                  <el-icon><icon-picture /></el-icon>
                </div>
              </template>
            </el-image>
          </template>
        </el-table-column>

        <el-table-column prop="stock" label="库存" width="100">
          <template #default="scope">
            <el-input
              v-model="scope.row.stock"
              size="default"
              placeholder="库存"
              @input="scope.row.stock = verifiyNumberInteger(scope.row.stock)"
            />
          </template>
        </el-table-column>
        <el-table-column prop="freezeStock" label="冻结库存" width="100">
          <template #default="scope">
            <el-text type="info" tag="ins" size="default">{{
              scope.row.freezeStock
            }}</el-text>
          </template>
        </el-table-column>
        <el-table-column prop="remainingStock" label="可用库存" width="100" />
        <el-table-column prop="costPrice" label="成本价" width="100">
          <template #default="scope">
            <el-input
              v-model="scope.row.costPrice"
              size="default"
              placeholder="成本价"
              @input="
                scope.row.costPrice = verifyNumberIntegerAndFloat(scope.row.costPrice)
              "
            />
          </template>
        </el-table-column>
        <el-table-column prop="price" label="销售价" width="100">
          <template #default="scope">
            <el-input
              v-model="scope.row.price"
              size="default"
              placeholder="销售价"
              @input="scope.row.price = verifyNumberIntegerAndFloat(scope.row.price)"
            />
          </template>
        </el-table-column>
        <el-table-column prop="mktPrice" label="市场价" width="100">
          <template #default="scope">
            <el-input
              v-model="scope.row.mktPrice"
              size="default"
              placeholder="市场价"
              @input="
                scope.row.mktPrice = verifyNumberIntegerAndFloat(scope.row.mktPrice)
              "
            />
          </template>
        </el-table-column>
        <el-table-column prop="weight" label="重量(克)" width="100">
          <template #default="scope">
            <el-input
              v-model="scope.row.weight"
              size="default"
              placeholder="重量(克)"
              @input="scope.row.weight = verifyNumberIntegerAndFloat(scope.row.weight)"
            />
          </template>
        </el-table-column>
        <el-table-column
          prop="spesDesc"
          show-overflow-tooltip
          label="产品规格"
          width="280"
        />
        <el-table-column prop="isDefault" fixed="right" label="是否默认" width="90">
          <template #default="scope">
            <el-switch
              v-model="scope.row.isDefault"
              inline-prompt
              active-text="是"
              inactive-text="否"
              size="default"
              :disabled="false"
              @change="onDefaultChange($event, scope.row)"
            />
          </template>
        </el-table-column>
      </el-table>
      <el-text type="info" size="small">注：提交后，修改的信息才会生效。</el-text>
      <template #footer>
        <span class="dialog-footer">
          <el-button
            type="primary"
            @click="onSubmit"
            size="default"
            :disable="!productStockTable.data || productStockTable.data.length == 0"
            >提 交</el-button
          >
          &nbsp;&nbsp;&nbsp;&nbsp;
          <el-button @click="onCancel" size="default">取 消</el-button>
        </span>
      </template>
    </el-dialog>
    <el-image-viewer
      v-if="imageViewer.show"
      :url-list="productStockTable.data.map((item) => item.images)"
      show-progress
      :initial-index="imageViewer.index"
      @close="imageViewer.show = false"
    />
  </div>
</template>

<script setup lang="ts" name="product">
import { reactive, nextTick, ref } from "vue";
import _ from "lodash";
import {
  verifiyNumberInteger,
  verifyNumberIntegerAndFloat,
} from "/@/utils/toolsValidate";
import { useGoodsApi } from "/@/api/good/goods/index";
import { ElMessage } from "element-plus";
import { Picture as IconPicture } from "@element-plus/icons-vue";

// 引入 Api 请求接口
const productApi = useGoodsApi();

// 定义变量内容
const imageViewer = ref({
  show: false,
  index: 0,
});
const productStockTable = reactive({
  loading: false,
  goodId: "",
  data: [] as RowProductStockDetailType[],
});
const dialog = reactive({
  isShowDialog: false,
  title: "",
});

// 显示图片
const showImage = (index: number) => {
  imageViewer.value.index = index;
  imageViewer.value.show = true;
};

// 初始化表格数据
const getTableData = () => {
  productStockTable.loading = true;
  productApi
    .getProductStockDetails(productStockTable.goodId)
    .then((result) => {
      productStockTable.data = result;
      productStockTable.loading = false;
    })
    .catch((err: any) => {
      productStockTable.loading = false;
      console.error("查询货品信息出错：", err);
    });
};

// 搜索
const searchProductDetail = () => {
  getTableData();
};

// 重置
const onReset = () => {
  // ...
};

// 打开弹窗
const openDialog = (goodId: string) => {
  productStockTable.goodId = goodId;
  searchProductDetail();
  nextTick(() => {
    dialog.title = "货品信息";
    dialog.isShowDialog = true;
  });
};

// 关闭弹窗
const closeDialog = () => {
  dialog.isShowDialog = false;
};

// 是否默认切换
const onDefaultChange = (value: boolean, row: RowProductStockDetailType) => {
  if (value) {
    productStockTable.data
      .filter((x) => x.id !== row.id)
      .forEach((item) => {
        item.isDefault = false;
      });
  }
};

// 提交
const onSubmit = () => {
  if (productStockTable.data.filter((x) => x.isDefault).length > 1) {
    ElMessage.warning("默认货品只能有一个！");
    return;
  } else if (productStockTable.data.filter((x) => x.isDefault).length < 1) {
    ElMessage.warning("您还未选择默认商品！");
    return;
  }
  let submitData = productStockTable.data.map((item) => {
    return {
      id: item.id,
      stock: item.stock,
      price: item.price,
      costPrice: item.costPrice,
      mktPrice: item.mktPrice,
      weight: item.weight,
      isDefault: item.isDefault,
    };
  });
  productApi
    .updateProductStocksAsync(submitData)
    .then((result) => {
      if (result) {
        ElMessage.success("货品信息更新成功！");
        onCancel();
      }
    })
    .catch((err: any) => {
      console.error("更新货品信息出错：", err);
    });
};

// 取消
const onCancel = () => {
  closeDialog();
};

// 暴露变量
defineExpose({
  openDialog,
});
</script>

<style scoped lang="scss">
.product-detail-container {
  :deep(.el-card__body) {
    display: flex;
    flex-direction: column;
    flex: 1;
    overflow: auto;
    .el-table {
      flex: 1;
    }
  }
  .good-price {
    color: #f56c6c;
  }
  .img-logo {
    width: 50px;
    height: 50px;
    cursor: pointer;
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
}
</style>
