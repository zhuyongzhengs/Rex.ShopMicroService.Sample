<template>
  <div class="system-security-log-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="system-security-log-search">
        <el-row :gutter="15">
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.actionName"
              size="default"
              placeholder="请输入执行操作"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.userName"
              size="default"
              placeholder="请输入操作人"
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
          <el-col :span="9">
            <el-button
              size="default"
              type="primary"
              class="ml10"
              @click="searchSecurityLog"
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
          <!-- <el-col :span="4"> </el-col> -->
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
          prop="userName"
          label="操作人"
          width="230"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          prop="applicationName"
          label="应用程序"
          width="240"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          prop="identity"
          label="用户身份"
          width="150"
          show-overflow-tooltip
        >
        </el-table-column>
        <el-table-column
          prop="action"
          label="执行操作"
          width="240"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          prop="clientIpAddress"
          label="客户端IP"
          width="150"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          prop="clientId"
          label="ClientId"
          width="300"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          prop="browserInfo"
          label="浏览器"
          width="300"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          fixed="right"
          prop="creationTime"
          label="创建日期"
          width="170"
          show-overflow-tooltip
        >
          <template #default="scope">
            <span>{{ scope.row.creationTime }}</span>
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
  </div>
</template>

<script setup lang="ts" name="systemSecurityLog">
import { reactive, onMounted } from "vue";
import _ from "lodash";
import { useSecurityLogApi } from "/@/api/logManage/securityLog/index";

// 引入用户 Api 请求接口
const securityLogApi = useSecurityLogApi();

const state = reactive<SysSecurityLogState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      actionName: "",
      userName: "",
      creationTime: [],
      sorting: "creationTime desc",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 搜索
const searchSecurityLog = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.actionName = "";
  state.tableData.param.userName = "";
  state.tableData.param.creationTime = [];
};

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  let tabParam = _.cloneDeep(state.tableData.param);
  delete tabParam.creationTime;
  if (
    state.tableData.param.creationTime &&
    state.tableData.param.creationTime.length == 2
  ) {
    tabParam.beginTime = state.tableData.param.creationTime[0];
    tabParam.endTime = state.tableData.param.creationTime[1];
  }
  securityLogApi
    .getSecurityLogList(tabParam)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询安全日志列表出错：", err);
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
.system-security-log-container {
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

.detail-container {
  padding: 15px;
}

.detail-item {
  word-wrap: break-word;
  word-break: break-all;
  line-height: 20px;
}
</style>
