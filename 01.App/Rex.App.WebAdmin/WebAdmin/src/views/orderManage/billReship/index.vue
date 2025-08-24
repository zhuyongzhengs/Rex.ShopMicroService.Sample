<template>
  <div class="billReship-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="billReship-search">
        <el-row :gutter="15" class="mt10">
          <el-col :span="3">
            <el-input
              v-model="state.tableData.param.no"
              size="default"
              placeholder="请输入退货单号"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-input
              v-model="state.tableData.param.aftersalesNo"
              size="default"
              placeholder="请输入售后单号"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-input
              v-model="state.tableData.param.orderNo"
              size="default"
              placeholder="请输入订单号"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-input
              v-model="state.tableData.param.logiNo"
              size="default"
              placeholder="请输入物流单号"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.status"
              size="default"
              placeholder="选择状态"
            >
              <el-option
                v-for="(rStatus, index) in billReshipStatus"
                :key="index"
                :label="rStatus.description"
                :value="rStatus.value"
              />
            </el-select>
          </el-col>
          <!--
          <el-col :span="7">
            <el-date-picker
              v-model="state.tableData.param.creationTime"
              type="datetimerange"
              range-separator="~"
              start-placeholder="开始时间"
              end-placeholder="结束时间"
              size="default"
              format="YYYY-MM-DD HH:mm:ss"
              value-format="YYYY-MM-DD HH:mm:ss"
            />
          </el-col>
          -->
          <el-col :span="5">
            <el-button size="default" type="primary" @click="searchBillReship">
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
      <el-table
        :data="state.tableData.data"
        ref="billReshipTableRef"
        v-loading="state.tableData.loading"
        style="width: 100%"
      >
        <el-table-column fixed type="index" width="65" label="序号" />
        <el-table-column
          fixed
          prop="no"
          label="退货单号"
          show-overflow-tooltip
          width="160"
        />
        <el-table-column label="订单号" show-overflow-tooltip width="160">
          <template #default="scope">
            {{ scope.row.orderNo }}
          </template>
        </el-table-column>
        <el-table-column label="售后单号" show-overflow-tooltip width="160">
          <template #default="scope">
            {{ scope.row.aftersalesNo }}
          </template>
        </el-table-column>
        <el-table-column prop="userName" label="用户" show-overflow-tooltip width="180" />
        <el-table-column
          prop="logiCode"
          label="物流编码"
          show-overflow-tooltip
          width="150"
        />
        <el-table-column
          prop="logiNo"
          label="物流单号"
          show-overflow-tooltip
          width="160"
        />
        <el-table-column prop="statusDisplay" label="审核状态" width="100">
          <template #default="scope">
            <el-text size="default" :type="colorStatusType(scope.row.status)">{{
              scope.row.statusDisplay
            }}</el-text>
          </template>
        </el-table-column>
        <el-table-column
          prop="creationTime"
          label="创建时间"
          width="170"
        ></el-table-column>
        <el-table-column fixed="right" label="操作" width="120">
          <template #default="scope">
            <el-button
              size="small"
              text
              type="info"
              @click="onBillReshipDetail(scope.row)"
              >查看</el-button
            >
            <el-button
              v-if="scope.row.status === 2"
              v-auth="'OrderService.BillReships.ConfirmDelivery'"
              size="small"
              text
              type="warning"
              @click="onConfirmDelivery(scope.row)"
              >确认收货</el-button
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
    <bill-Reship-detail-dialog ref="billReshipDetailDialogRef" />
  </div>
</template>

<script setup lang="ts" name="billReship">
import { reactive, onBeforeMount, onMounted, ref, defineAsyncComponent } from "vue";
import _ from "lodash";
import { useBillReshipApi } from "/@/api/orderManage/billReship/index";
import { ElMessage, ElMessageBox } from "element-plus";

// 引入组件
const BillReshipDetailDialog = defineAsyncComponent(
  () => import("/@/views/orderManage/billReship/components/detailDialog.vue")
);

// 引入 Api 请求接口
const billReshipApi = useBillReshipApi();

// 定义变量内容
const billReshipDetailDialogRef = ref();
const billReshipStatus = ref<EnumValueType[]>([]);
const state = reactive<BillReshipState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      no: null,
      orderNo: null,
      aftersalesNo: null,
      logiNo: null,
      status: null,
      creationTime: [],
      sorting: "CreationTime DESC",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 查看退货单详情
const onBillReshipDetail = (row: RowBillReshipManyType) => {
  billReshipDetailDialogRef.value.openDialog(row);
};

// 确认收货
const onConfirmDelivery = (row: RowBillReshipManyType) => {
  ElMessageBox.confirm(`退货单号：${row.no}，您确认要收货吗?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    billReshipApi
      .updateConfirmDelivery(row.id)
      .then((result) => {
        if (result) {
          getTableData();
          ElMessage.success("收货成功");
        }
      })
      .catch((err: any) => {
        console.error("收货出错：", err);
      });
  });
};

// 状态颜色
const colorStatusType = (status: number) => {
  switch (status) {
    case 1:
      return "warning";
    case 2:
      return "primary";
    case 5:
      return "success";
    default:
      return "info";
  }
};

// 查询退货单状态
const getBillBillReshipStatus = () => {
  billReshipApi
    .getBillReshipStatus()
    .then((result) => {
      billReshipStatus.value = result;
    })
    .catch((err: any) => {
      console.error("查询退货单状态出错：", err);
    });
};

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;

  let searchParam = _.cloneDeep(state.tableData.param);
  delete searchParam.creationTime;
  if (
    state.tableData.param.creationTime &&
    state.tableData.param.creationTime.length == 2
  ) {
    searchParam.beginTime = state.tableData.param.creationTime[0];
    searchParam.endTime = state.tableData.param.creationTime[1];
  }

  billReshipApi
    .getBillReshipList(searchParam)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询退货单方式列表出错：", err);
    });
};

// 搜索
const searchBillReship = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.no = null;
  state.tableData.param.orderNo = null;
  state.tableData.param.aftersalesNo = null;
  state.tableData.param.logiNo = null;
  state.tableData.param.status = null;
  state.tableData.param.creationTime = [];
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

// 页面加载前
onBeforeMount(() => {
  getBillBillReshipStatus();
  getTableData();
});

// 页面加载完时
onMounted(() => {
  // ...
});
</script>

<style scoped lang="scss">
.billReship-container {
  :deep(.el-card__body) {
    display: flex;
    flex-direction: column;
    flex: 1;
    overflow: auto;
    .el-table {
      flex: 1;
    }
  }
  .group-btn {
    border: 1px #ebeef5 solid;
    border-right: 1px #dcdfe6 solid !important;
  }
  .el-dropdown-print {
    height: 24px;
    line-height: 24px;
    margin: 0px 8px;
    font-size: 12px;
    color: #e6a23c;
    cursor: default;
  }
}
</style>
