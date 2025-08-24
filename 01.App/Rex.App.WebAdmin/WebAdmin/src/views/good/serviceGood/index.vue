<template>
  <div class="good-service-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="good-service-search">
        <el-row :gutter="15">
          <el-col :span="6">
            <el-input
              v-model="state.tableData.param.title"
              size="default"
              placeholder="请输入项目名称"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="4">
            <el-select
              size="default"
              v-model="state.tableData.param.status"
              placeholder="请选择项目状态"
            >
              <el-option
                v-for="(status, index) in serviceGoodStatusItems"
                :key="index"
                :label="status.description"
                :value="status.value"
              />
            </el-select>
          </el-col>
          <el-col :span="4">
            <el-select
              size="default"
              v-model="state.tableData.param.validityType"
              placeholder="请选择核销类型"
            >
              <el-option
                v-for="(status, index) in serviceGoodValidityTypeItems"
                :key="index"
                :label="status.description"
                :value="status.value"
              />
            </el-select>
          </el-col>
          <el-col :span="8">
            <el-button
              id="serviceGood_search_btn"
              size="default"
              type="primary"
              @click="searchServiceGood"
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
          v-auth="'GoodService.ServiceGoods.Create'"
          type="success"
          @click="onOpenAddServiceGood"
          class="mr10"
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
        <el-table-column fixed type="index" label="序号" width="60" />
        <el-table-column prop="title" label="项目名称" show-overflow-tooltip width="300">
          <template #default="scope">
            <span>{{ scope.row.title }}</span>
          </template>
        </el-table-column>
        <el-table-column
          prop="thumbnail"
          label="缩略图"
          show-overflow-tooltip
          width="150"
        >
          <template #default="scope">
            <el-image
              class="img-logo"
              :src="scope.row.thumbnail"
              :zoom-rate="1.2"
              :max-scale="7"
              :min-scale="0.2"
              :preview-src-list="[scope.row.thumbnail]"
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
        <el-table-column prop="costPrice" label="售价" show-overflow-tooltip width="120">
          <template #default="scope">
            <span>￥{{ scope.row.money }}</span>
          </template>
        </el-table-column>
        <el-table-column
          prop="allowedMembership"
          label="允许购买会员级别"
          show-overflow-tooltip
          width="240"
        >
          <template #default="scope">
            <el-tag
              size="default"
              class="mr10"
              type="info"
              v-for="(allowedMembership, index) in displayUserGradeTitle(
                scope.row.allowedMemberships
              )"
              :key="index"
              >{{ allowedMembership }}</el-tag
            >
          </template>
        </el-table-column>
        <el-table-column prop="status" label="项目状态" show-overflow-tooltip width="150">
          <template #default="scope">
            <span>{{ displayStatusName(scope.row.status) }}</span>
          </template>
        </el-table-column>
        <el-table-column
          prop="maxBuyNumber"
          label="项目重复购买次数"
          show-overflow-tooltip
          width="150"
        >
          <template #default="scope">
            <span>{{ scope.row.maxBuyNumber }}</span>
          </template>
        </el-table-column>
        <el-table-column
          prop="amount"
          label="项目可销售数量"
          show-overflow-tooltip
          width="150"
        >
          <template #default="scope">
            <span>{{ scope.row.amount }}</span>
          </template>
        </el-table-column>
        <el-table-column
          prop="startTime"
          label="项目开始时间"
          show-overflow-tooltip
          width="165"
        />
        <el-table-column
          prop="endTime"
          label="项目截止时间"
          show-overflow-tooltip
          width="165"
        />
        <el-table-column
          prop="validityType"
          label="核销有效期类型"
          show-overflow-tooltip
          width="150"
        >
          <template #default="scope">
            <span>{{ displayValidityTypeName(scope.row.validityType) }}</span>
          </template>
        </el-table-column>
        <el-table-column
          prop="validityStartTime"
          label="核销开始时间"
          show-overflow-tooltip
          width="165"
        />
        <el-table-column
          prop="validityEndTime"
          label="核销结束时间"
          show-overflow-tooltip
          width="165"
        />
        <el-table-column
          prop="ticketNumber"
          label="核销服务券数量"
          show-overflow-tooltip
          width="150"
        >
          <template #default="scope">
            <span>{{ scope.row.ticketNumber }}</span>
          </template>
        </el-table-column>
        <el-table-column
          prop="creationTime"
          label="创建时间"
          show-overflow-tooltip
          width="165"
        />
        <el-table-column fixed="right" label="操作" width="130">
          <template #default="scope">
            <el-button
              v-auth="'GoodService.ServiceGoods.Look'"
              size="small"
              text
              type="info"
              @click="onOpenLookServiceGood(scope.row)"
              >查看</el-button
            >
            <el-button
              v-auth="'GoodService.ServiceGoods.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditServiceGood(scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'GoodService.ServiceGoods.Delete'"
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
  </div>
</template>

<script setup lang="ts" name="service">
import { reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import { useRouter } from "vue-router";
import { useServiceGoodApi } from "/@/api/good/serviceGood/index";
import { useUserGradeApi } from "/@/api/system/userGrade/index";

// 引入服务项目 Api 请求接口
const serviceGoodApi = useServiceGoodApi();
const userGradeApi = useUserGradeApi();

// 定义变量内容
const router = useRouter();
const serviceGoodStatusItems = ref([] as EnumValueType[]);
const serviceGoodValidityTypeItems = ref([] as EnumValueType[]);
const allowedMembershipItems = ref([] as RowUserGradeType[]);
const state = reactive<ServiceGoodState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      title: null,
      status: null,
      validityType: null,
      sorting: "creationTime desc",
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
  serviceGoodApi
    .getServiceGoodList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询服务项目列表出错：", err);
    });
};

// 搜索
const searchServiceGood = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.title = null;
  state.tableData.param.status = null;
  state.tableData.param.validityType = null;
};

// 打开新增服务项目弹窗
const onOpenAddServiceGood = () => {
  router.push({ path: "/good/serviceGood/create" });
};

// 打开修改服务项目弹窗
const onOpenEditServiceGood = (row: RowServiceGoodType) => {
  const params: EmptyObjectType = { id: row.id };
  router.push({
    path: "/good/serviceGood/update",
    query: params,
  });
};

// 打开查看服务项目弹窗
const onOpenLookServiceGood = (row: RowServiceGoodType) => {
  const params: EmptyObjectType = { id: row.id };
  router.push({
    path: "/good/serviceGood/look",
    query: params,
  });
};

// 删除服务项目
const onRowDel = (row: RowServiceGoodType) => {
  ElMessageBox.confirm(
    `此操作将永久删除服务项目名称：“${row.title}”，是否继续?`,
    "提示",
    {
      confirmButtonText: "确认",
      cancelButtonText: "取消",
      type: "warning",
    }
  ).then(() => {
    serviceGoodApi
      .deleteServiceGood(row.id)
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

// 获取项目状态
const getServiceGoodStatuItems = () => {
  serviceGoodApi
    .getServiceGoodStatus()
    .then((result) => {
      serviceGoodStatusItems.value = result;
    })
    .catch((err: any) => {
      console.error("查询状态出错：", err);
    });
};

// 显示项目状态
const displayStatusName = (val: number) => {
  if ((val || Number(val) >= 0) && serviceGoodStatusItems.value.length > 0) {
    let displayName = serviceGoodStatusItems.value
      .filter((p) => p.value == String(val))
      .map((p) => p.description)[0];
    return displayName;
  }
  return null;
};

// 获取服务核销类型
const getServiceGoodValidityTypeItems = () => {
  serviceGoodApi
    .getServiceValidityTypes()
    .then((result) => {
      serviceGoodValidityTypeItems.value = result;
    })
    .catch((err) => {
      console.error("查询服务核销类型出错：", err);
    });
};

// 显示项目状态
const displayValidityTypeName = (val: number) => {
  if ((val || Number(val) >= 0) && serviceGoodValidityTypeItems.value.length > 0) {
    let displayName = serviceGoodValidityTypeItems.value
      .filter((p) => p.value == String(val))
      .map((p) => p.description)[0];
    return displayName;
  }
  return null;
};

// 查询用户等级
const getUserGrade = () => {
  userGradeApi
    .getUserGradeList({
      title: "",
      sorting: "creationTime desc",
      skipCount: 0,
      maxResultCount: 100,
    })
    .then((result) => {
      if (result) {
        allowedMembershipItems.value = result.items;
      }
    })
    .catch((err: any) => {
      console.error("查询用户等级出错：", err);
    });
};

// 显示会员等级
const displayUserGradeTitle = (val: string[]) => {
  if (allowedMembershipItems.value.length > 0) {
    let displayTitles = allowedMembershipItems.value
      .filter((p) => val.includes(p.id))
      .map((p) => p.title);
    return displayTitles;
  }
  return [];
};

// 页面加载完时
onMounted(() => {
  getServiceGoodStatuItems();
  getServiceGoodValidityTypeItems();
  getUserGrade();
  getTableData();
});
</script>

<style scoped lang="scss">
.good-service-container {
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
</style>
