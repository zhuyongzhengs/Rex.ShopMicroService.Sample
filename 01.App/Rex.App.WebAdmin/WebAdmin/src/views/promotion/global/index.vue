<template>
  <div class="promotion-global-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="promotion-global-search">
        <el-row :gutter="15">
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.name"
              size="default"
              placeholder="请输入促销名称"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.isExclusive"
              size="default"
              placeholder="是否排他"
            >
              <el-option label="是" :value="true" />
              <el-option label="否" :value="false" />
            </el-select>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.isEnable"
              size="default"
              placeholder="是否开启"
            >
              <el-option label="是" :value="true" />
              <el-option label="否" :value="false" />
            </el-select>
          </el-col>
          <el-col :span="7">
            <el-date-picker
              v-model="state.tableData.param.startAndEndTime"
              type="datetimerange"
              range-separator="~"
              start-placeholder="开始时间"
              end-placeholder="结束时间"
              size="default"
              format="YYYY-MM-DD HH:mm:ss"
              value-format="YYYY-MM-DD HH:mm:ss"
            />
          </el-col>
          <el-col :span="7">
            <el-button
              size="default"
              type="primary"
              class="ml10"
              @click="searchPromotion"
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
    <el-card shadow="hover" class="layout-padding-auto">
      <template #header>
        <el-button
          size="default"
          v-auth="'PromotionService.Promotions.Create'"
          type="success"
          @click="onOpenAddPromotion('add')"
        >
          <el-icon>
            <ele-Plus />
          </el-icon>
          新增
        </el-button>
      </template>
      <el-table
        :data="state.tableData.data"
        v-loading="state.tableData.loading"
        style="width: 100%"
      >
        <el-table-column type="index" label="序号" width="60" />
        <el-table-column prop="name" label="促销名称" show-overflow-tooltip />
        <el-table-column prop="weight" label="权重" show-overflow-tooltip width="70" />
        <el-table-column
          prop="isExclusive"
          label="是否排他"
          show-overflow-tooltip
          width="90"
        >
          <template #default="scope">
            <el-switch
              v-model="scope.row.isExclusive"
              inline-prompt
              active-text="是"
              inactive-text="否"
              size="default"
              @change="onIsExclusive($event, scope.row)"
            />
          </template>
        </el-table-column>
        <el-table-column
          prop="isEnable"
          label="是否开启"
          show-overflow-tooltip
          width="90"
        >
          <template #default="scope">
            <el-switch
              v-model="scope.row.isEnable"
              inline-prompt
              active-text="是"
              inactive-text="否"
              size="default"
              @change="onIsEnable($event, scope.row)"
            />
          </template>
        </el-table-column>
        <el-table-column
          prop="startTime"
          label="开始时间"
          show-overflow-tooltip
          width="165"
        />
        <el-table-column
          prop="endTime"
          label="结束时间"
          show-overflow-tooltip
          width="165"
        />
        <el-table-column fixed="right" label="操作" width="150">
          <template #default="scope">
            <el-button
              v-auth="'PromotionService.Promotions.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditSetParameter(scope.row)"
              >设置参数</el-button
            >
            <el-button
              v-auth="'PromotionService.Promotions.Delete'"
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
    <promotion-dialog ref="promotionDialogRef" @refresh="getTableData()" />
    <set-parameters-dialog ref="setParametersDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="promotion">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import { usePromotionApi } from "/@/api/promotion/index";

// 引入组件
const PromotionDialog = defineAsyncComponent(
  () => import("/@/views/promotion/global/components/promotionDialog.vue")
);
const SetParametersDialog = defineAsyncComponent(
  () => import("/@/views/promotion/global/components/setParametersDialog.vue")
);

// 引入全局促销 Api 请求接口
const promotionApi = usePromotionApi();

// 定义变量内容
const promotionDialogRef = ref();
const setParametersDialogRef = ref();
const state = reactive<PromotionState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      name: null,
      isExclusive: null,
      isEnable: null,
      types: [1],
      startAndEndTime: [],
      sorting: "weight asc, creationTime desc",
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
  promotionApi
    .getPromotionList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询全局促销列表出错：", err);
    });
};

// 搜索
const searchPromotion = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.name = null;
  state.tableData.param.isExclusive = null;
  state.tableData.param.isEnable = null;
  state.tableData.param.startAndEndTime = [];
};

// 修改是否排他
const onIsExclusive = (val: boolean, promotion: RowPromotionType) => {
  promotionApi
    .updatePromotionIsExclusive(promotion.id, val)
    .then((result) => {
      console.log("修改[" + promotion.name + "]是否排他显示，结果：", result);
    })
    .catch((err: any) => {
      promotion.isExclusive = !val;
      console.error("修改是否排他出错：", err);
    });
};

// 修改是否开启
const onIsEnable = (val: boolean, promotion: RowPromotionType) => {
  promotionApi
    .updatePromotionIsEnable(promotion.id, val)
    .then((result) => {
      console.log("修改[" + promotion.name + "]是否启用显示，结果：", result);
    })
    .catch((err: any) => {
      promotion.isEnable = !val;
      console.error("修改是否启用出错：", err);
    });
};

// 打开新增全局促销弹窗
const onOpenAddPromotion = (type: string) => {
  promotionDialogRef.value.openDialog(type);
};

// 打开修改设置参数
const onOpenEditSetParameter = (row: RowPromotionType) => {
  setParametersDialogRef.value.openDialog(row);
};

// 删除全局促销
const onRowDel = (row: RowPromotionType) => {
  ElMessageBox.confirm(`此操作将永久删除促销名称：“${row.name}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    promotionApi
      .deletePromotion(row.id)
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

// 页面加载完时
onMounted(() => {
  getTableData();
});
</script>

<style scoped lang="scss">
.promotion-global-container {
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
</style>
