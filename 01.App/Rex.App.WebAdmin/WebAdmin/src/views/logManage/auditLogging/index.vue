<template>
  <div class="system-audit-logging-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="system-audit-logging-search">
        <el-row :gutter="15">
          <el-col :span="4">
            <el-select
              v-model="state.tableData.param.httpMethod"
              size="default"
              placeholder="请选择Http方法"
            >
              <el-option label="GET" value="GET" />
              <el-option label="POST" value="POST" />
              <el-option label="PUT" value="PUT" />
              <el-option label="DELETE" value="DELETE" />
            </el-select>
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
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.httpStatusCode"
              size="default"
              @input="onVerifiyNumberInteger($event)"
              maxlength="5"
              placeholder="请输入Http状态码"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="7">
            <el-date-picker
              v-model="state.tableData.param.executionTime"
              type="datetimerange"
              range-separator="~"
              start-placeholder="开始时间"
              end-placeholder="结束时间"
              size="default"
              format="YYYY-MM-DD HH:mm:ss"
              value-format="YYYY-MM-DD HH:mm:ss"
            />
          </el-col>
          <el-col :span="5">
            <el-button
              size="default"
              type="primary"
              class="ml10"
              @click="searchAuditLogging"
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
          prop="userName"
          label="操作人"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          prop="applicationName"
          label="应用程序"
          width="240"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          prop="httpMethod"
          label="Http方法"
          width="110"
          show-overflow-tooltip
        >
          <template #default="scope">
            <el-tag v-if="scope.row.httpMethod == 'POST'" size="default" type="success">{{
              scope.row.httpMethod
            }}</el-tag>
            <el-tag v-else-if="scope.row.httpMethod == 'GET'" size="default">{{
              scope.row.httpMethod
            }}</el-tag>
            <el-tag
              v-else-if="scope.row.httpMethod == 'PUT'"
              size="default"
              type="warning"
              >{{ scope.row.httpMethod }}</el-tag
            >
            <el-tag
              v-else-if="scope.row.httpMethod == 'DELETE'"
              size="default"
              type="danger"
              >{{ scope.row.httpMethod }}</el-tag
            >
          </template>
        </el-table-column>
        <el-table-column
          prop="httpStatusCode"
          label="Http状态码"
          width="110"
          show-overflow-tooltip
        >
          <template #default="scope">
            <el-tag
              v-if="scope.row.httpStatusCode < '300'"
              size="default"
              type="success"
              >{{ scope.row.httpStatusCode }}</el-tag
            >
            <el-tag
              v-else-if="scope.row.httpStatusCode < '500'"
              size="default"
              type="warning"
              >{{ scope.row.httpStatusCode }}</el-tag
            >
            <el-tag
              v-else-if="scope.row.httpStatusCode <= '600'"
              size="default"
              type="danger"
              >{{ scope.row.httpStatusCode }}</el-tag
            >
          </template>
        </el-table-column>
        <el-table-column
          prop="url"
          label="Url"
          width="300"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          prop="executionDuration"
          label="执行耗时"
          width="100"
          show-overflow-tooltip
        >
          <template #default="scope">
            <span>{{ scope.row.executionDuration }}&nbsp;ms</span>
          </template>
        </el-table-column>
        <el-table-column
          prop="exceptions"
          label="异常信息"
          width="300"
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
          width="210"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          prop="browserInfo"
          label="浏览器"
          width="300"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          prop="executionTime"
          label="执行日期"
          width="170"
          show-overflow-tooltip
        >
          <template #default="scope">
            <span>{{ scope.row.executionTime }}</span>
          </template>
        </el-table-column>
        <el-table-column fixed="right" label="操作" width="100">
          <template #default="scope">
            <el-button size="small" text type="primary" @click="onLookDetail(scope.row)"
              >查看详情</el-button
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
    <el-drawer
      v-model="suditLoggingDetailDrawer.showDetail"
      title="审计日志详情"
      :with-header="true"
    >
      <div class="detail-container">
        <el-card shadow="hover" class="layout-padding-auto mb15">
          <template #header>
            <div class="card-header">用户信息</div>
          </template>
          <div class="user-info-content" v-if="suditLoggingDetailDrawer.auditLoggingData">
            <el-form size="default" label-width="80px">
              <el-row :gutter="35">
                <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24">
                  <el-form-item label="用户名称">
                    <span class="detail-item">{{
                      suditLoggingDetailDrawer.auditLoggingData.userName
                    }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24">
                  <el-form-item label="IP地址">
                    <span class="detail-item">{{
                      suditLoggingDetailDrawer.auditLoggingData.clientIpAddress
                    }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                  <el-form-item label="客户端ID">
                    <span class="detail-item">{{
                      suditLoggingDetailDrawer.auditLoggingData.clientId
                    }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                  <el-form-item label="浏览器">
                    <span class="detail-item">{{
                      suditLoggingDetailDrawer.auditLoggingData.browserInfo
                    }}</span>
                  </el-form-item>
                </el-col>
              </el-row>
            </el-form>
          </div>
        </el-card>
        <el-card shadow="hover" class="layout-padding-auto">
          <template #header>
            <div class="card-header">操作信息</div>
          </template>
          <div
            class="action-info-content"
            v-if="suditLoggingDetailDrawer.auditLoggingData?.actions"
          >
            <el-form
              v-for="(action, index) in suditLoggingDetailDrawer.auditLoggingData
                ?.actions"
              :key="index"
              size="default"
              label-width="90px"
              class="mb15"
            >
              <el-row :gutter="35">
                <el-col :xs="24" :sm="24" :md="12" :lg="24" :xl="24">
                  <el-form-item label="服务名称">
                    <span class="detail-item">{{ action.serviceName }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="12" :sm="24" :md="24" :lg="24" :xl="24">
                  <el-form-item label="方法名称">
                    <span class="detail-item">{{ action.methodName }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                  <el-form-item label="Http方法">
                    <span class="detail-item">
                      <el-tag
                        v-if="
                          suditLoggingDetailDrawer.auditLoggingData.httpMethod == 'POST'
                        "
                        size="default"
                        type="success"
                        >{{
                          suditLoggingDetailDrawer.auditLoggingData.httpMethod
                        }}</el-tag
                      >
                      <el-tag
                        v-else-if="
                          suditLoggingDetailDrawer.auditLoggingData.httpMethod == 'GET'
                        "
                        size="default"
                        >{{
                          suditLoggingDetailDrawer.auditLoggingData.httpMethod
                        }}</el-tag
                      >
                      <el-tag
                        v-else-if="
                          suditLoggingDetailDrawer.auditLoggingData.httpMethod == 'PUT'
                        "
                        size="default"
                        type="warning"
                        >{{
                          suditLoggingDetailDrawer.auditLoggingData.httpMethod
                        }}</el-tag
                      >
                      <el-tag
                        v-else-if="
                          suditLoggingDetailDrawer.auditLoggingData.httpMethod == 'DELETE'
                        "
                        size="default"
                        type="danger"
                        >{{
                          suditLoggingDetailDrawer.auditLoggingData.httpMethod
                        }}</el-tag
                      >
                    </span>
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                  <el-form-item label="状态码">
                    <el-tag
                      v-if="
                        suditLoggingDetailDrawer.auditLoggingData.httpStatusCode < 300
                      "
                      type="success"
                      >{{
                        suditLoggingDetailDrawer.auditLoggingData.httpStatusCode
                      }}</el-tag
                    >
                    <el-tag
                      v-else-if="
                        suditLoggingDetailDrawer.auditLoggingData.httpStatusCode < 500
                      "
                      type="warning"
                      >{{
                        suditLoggingDetailDrawer.auditLoggingData.httpStatusCode
                      }}</el-tag
                    >
                    <el-tag
                      v-else-if="
                        suditLoggingDetailDrawer.auditLoggingData.httpStatusCode <= 600
                      "
                      type="danger"
                      >{{
                        suditLoggingDetailDrawer.auditLoggingData.httpStatusCode
                      }}</el-tag
                    >
                  </el-form-item>
                </el-col>
                <el-col :xs="12" :sm="24" :md="24" :lg="24" :xl="24">
                  <el-form-item label="执行时间">
                    <span class="detail-item">{{ action.executionTime }}</span>
                  </el-form-item>
                </el-col>
                <el-col :xs="12" :sm="24" :md="24" :lg="24" :xl="24">
                  <el-form-item label="执行时长">
                    <span class="detail-item"
                      >{{ action.executionDuration }}&nbsp;ms</span
                    >
                  </el-form-item>
                </el-col>
                <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                  <el-form-item label="参数">
                    <span class="detail-item">
                      {{ action.parameters }}
                    </span>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-divider
                border-style="dashed"
                v-if="
                  index != suditLoggingDetailDrawer.auditLoggingData.actions.length - 1
                "
              />
            </el-form>
          </div>
        </el-card>
      </div>
    </el-drawer>
  </div>
</template>

<script setup lang="ts" name="systemAuditLogging">
import { reactive, onMounted } from "vue";
import _ from "lodash";
import { useAuditLoggingApi } from "/@/api/logManage/auditLogging/index";
import { verifiyNumberInteger } from "/@/utils/toolsValidate";

// 引入用户 Api 请求接口
const auditLoggingApi = useAuditLoggingApi();

// 定义变量内容
const suditLoggingDetailDrawer = reactive({
  showDetail: false,
  auditLoggingData: {} as any,
});

const state = reactive<SysAuditLoggingState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      executionTime: [],
      userName: "",
      httpMethod: "",
      correlationId: "",
      applicationName: "",
      httpStatusCode: "",
      sorting: "executionTime desc",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 搜索
const searchAuditLogging = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.url = "";
  state.tableData.param.userName = "";
  state.tableData.param.httpMethod = "";
  state.tableData.param.correlationId = "";
  state.tableData.param.applicationName = "";
  state.tableData.param.httpStatusCode = "";
  state.tableData.param.executionTime = [];
};

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  let tabParam = _.cloneDeep(state.tableData.param);
  delete tabParam.executionTime;
  if (
    state.tableData.param.executionTime &&
    state.tableData.param.executionTime.length == 2
  ) {
    tabParam.beginTime = state.tableData.param.executionTime[0];
    tabParam.endTime = state.tableData.param.executionTime[1];
  }
  auditLoggingApi
    .getAuditLoggingList(tabParam)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询审计日志列表出错：", err);
    });
};

// 查看详情
const onLookDetail = (row: RowAuditLoggingType) => {
  suditLoggingDetailDrawer.showDetail = true;
  suditLoggingDetailDrawer.auditLoggingData = row;
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

// 正整数
const onVerifiyNumberInteger = (val: string) => {
  state.tableData.param.httpStatusCode = verifiyNumberInteger(val);
};

// 页面加载完时
onMounted(() => {
  getTableData();
});
</script>

<style scoped lang="scss">
.system-audit-logging-container {
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
