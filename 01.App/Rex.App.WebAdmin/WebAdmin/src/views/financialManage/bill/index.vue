<template>
  <div class="bill-Payment-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="bill-Payment-search">
        <el-row :gutter="15">
          <el-col :span="3">
            <el-input
              v-model="state.tableData.param.no"
              size="default"
              placeholder="请输入支付单号"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.type"
              size="default"
              placeholder="选择单据类型"
            >
              <el-option
                v-for="(bpType, index) in billPaymentTypes"
                :key="index"
                :label="bpType.description"
                :value="bpType.value"
              />
            </el-select>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.status"
              size="default"
              placeholder="选择支付状态"
            >
              <el-option
                v-for="(bpStatus, index) in billPaymentStatus"
                :key="index"
                :label="bpStatus.description"
                :value="bpStatus.value"
              />
            </el-select>
          </el-col>
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.tradeNo"
              size="default"
              placeholder="请输入第三方平台交易流水号"
              clearable
            >
            </el-input>
          </el-col>
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
          <el-col :span="4">
            <el-button
              size="default"
              type="primary"
              class="ml10"
              @click="searchBillPayment"
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
      <el-table
        :data="state.tableData.data"
        v-loading="state.tableData.loading"
        style="width: 100%"
      >
        <el-table-column fixed type="index" label="序号" width="60" />
        <el-table-column
          fixed
          prop="no"
          label="支付单号"
          show-overflow-tooltip
          width="160"
        />
        <el-table-column prop="money" label="支付金额" show-overflow-tooltip width="100">
          <template #default="scope">
            <el-text class="mx-1" type="danger">￥{{ scope.row.money }}</el-text>
          </template>
        </el-table-column>
        <el-table-column prop="userName" label="用户" show-overflow-tooltip width="210" />
        <el-table-column prop="type" label="单据类型" show-overflow-tooltip width="100">
          <template #default="scope">
            <el-text type="info">{{ formatBillType(scope.row.type) }}</el-text>
          </template>
        </el-table-column>
        <el-table-column prop="status" label="支付状态" show-overflow-tooltip width="100">
          <template #default="scope">
            <el-tag :type="scope.row.status == 2 ? 'success' : 'info'">{{
              formatBillStatus(scope.row.status)
            }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column
          prop="paymentCode"
          label="支付方式"
          show-overflow-tooltip
          width="120"
        >
          <template #default="scope">
            <el-text type="info">{{ scope.row.paymentCode }}</el-text>
          </template>
        </el-table-column>
        <el-table-column
          prop="tradeNo"
          label="第三方平台交易流水号"
          show-overflow-tooltip
          width="260"
        />
        <el-table-column
          prop="payedMsg"
          label="支付备注"
          show-overflow-tooltip
          min-width="180"
        />
        <el-table-column fixed="right" prop="creationTime" label="创建时间" width="165" />
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
  </div>
</template>

<script setup lang="ts" name="billPayment">
import { ref, reactive, onBeforeMount, onMounted } from "vue";
import _ from "lodash";
import { useBillPaymentApi } from "/@/api/financialManage/billPayment/index";

// 引入支付方式 Api 请求接口
const billPaymentApi = useBillPaymentApi();

// 定义变量内容
let billPaymentStatus = ref<EnumValueType[]>([]);
let billPaymentTypes = ref<EnumValueType[]>([]);
const state = reactive<BillPaymentState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      no: null,
      type: null,
      status: null,
      tradeNo: null,
      creationTime: [],
      sorting: "CreationTime DESC",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 查询支付状态
const getGoodParamTypes = () => {
  billPaymentApi
    .getBillPaymentStatus()
    .then((result) => {
      billPaymentStatus.value = result;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询支付状态出错：", err);
    });
};

// 查询查询单据类型
const getBillPaymentTypes = () => {
  billPaymentApi
    .getBillPaymentType()
    .then((result) => {
      billPaymentTypes.value = result;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询单据类型出错：", err);
    });
};

// 支付状态
const formatBillStatus = (status: number) => {
  for (let i = 0; i < billPaymentStatus.value.length; i++) {
    const bpStatus = billPaymentStatus.value[i];
    if (bpStatus.value == String(status)) {
      return bpStatus.description;
    }
  }
  return "";
};

// 单据类型
const formatBillType = (type: number) => {
  for (let i = 0; i < billPaymentTypes.value.length; i++) {
    const bpType = billPaymentTypes.value[i];
    if (bpType.value == String(type)) {
      return bpType.description;
    }
  }
  return "";
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

  billPaymentApi
    .getBillPaymentList(searchParam)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询支付方式列表出错：", err);
    });
};

// 搜索
const searchBillPayment = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.no = null;
  state.tableData.param.type = null;
  state.tableData.param.status = null;
  state.tableData.param.tradeNo = null;
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

// 组件挂载到节点上之前
onBeforeMount(() => {
  getGoodParamTypes();
  getBillPaymentTypes();
});

// 页面加载完时
onMounted(() => {
  getTableData();
});
</script>

<style scoped lang="scss">
.bill-Payment-container {
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
