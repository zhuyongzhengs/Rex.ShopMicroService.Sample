<template>
  <div class="shop-setting-store-container layout-padding">
    <el-row :gutter="15">
      <el-col :span="6">
        <div class="card-store-container">
          <el-card shadow="hover" class="layout-padding-auto">
            <template #header>
              <div class="card-header">
                <el-row :gutter="15" class="mb10">
                  <el-col :span="16">
                    <el-input
                      v-model="storeState.tableData.param.storeName"
                      size="default"
                      maxlength="10"
                      placeholder="请输入门店名称"
                      clearable
                    />
                  </el-col>
                  <el-col :span="8">
                    <el-button size="default" type="primary" @click="searchGoodStore">
                      <el-icon>
                        <ele-Search />
                      </el-icon>
                      查询
                    </el-button>
                  </el-col>
                </el-row>
                <el-row :gutter="15">
                  <el-col :span="8">
                    <el-button
                      size="default"
                      v-auth="'GoodService.Stores.Create'"
                      type="success"
                      @click="onOpenAddStore('add')"
                    >
                      <el-icon>
                        <ele-Plus />
                      </el-icon>
                      添加
                    </el-button>
                  </el-col>
                  <el-col :span="8">
                    <el-button
                      size="default"
                      v-auth="'GoodService.Stores.Update'"
                      type="warning"
                      @click="onOpenEditStore('edit')"
                    >
                      <el-icon>
                        <ele-Edit />
                      </el-icon>
                      修改
                    </el-button>
                  </el-col>
                  <el-col :span="8">
                    <el-button
                      size="default"
                      v-auth="'GoodService.Stores.Delete'"
                      type="danger"
                      @click="onRowDelStore"
                    >
                      <el-icon>
                        <ele-Delete />
                      </el-icon>
                      删除
                    </el-button>
                  </el-col>
                </el-row>
              </div>
            </template>
            <div class="store-content" :style="'height: ' + itemHeight + 'px'">
              <el-table
                :data="storeState.tableData.data"
                v-loading="storeState.tableData.loading"
                highlight-current-row
                @current-change="currentStoreChange"
                style="width: 100%"
                height="540"
              >
                <el-table-column type="index" label="序号" width="60" />
                <el-table-column
                  prop="storeName"
                  label="门店名称"
                  show-overflow-tooltip
                />
              </el-table>
            </div>
          </el-card>
        </div>
      </el-col>
      <el-col :span="18">
        <div class="card-store-container">
          <el-card shadow="hover" class="layout-padding-auto">
            <template #header>
              <div class="card-header">
                <el-row :gutter="15">
                  <el-col :span="6">
                    <el-input
                      v-model="storeClerkState.tableData.param.userName"
                      size="default"
                      maxlength="10"
                      placeholder="请输入店员昵称"
                      clearable
                    />
                  </el-col>
                  <el-col :span="6">
                    <el-input
                      v-model="storeClerkState.tableData.param.phoneNumber"
                      size="default"
                      maxlength="12"
                      placeholder="请输入手机号码"
                      clearable
                    />
                  </el-col>
                  <el-col :span="12">
                    <el-button
                      size="default"
                      type="primary"
                      @click="searchGoodStoreClerk"
                    >
                      <el-icon>
                        <ele-Search />
                      </el-icon>
                      查询
                    </el-button>
                    <el-divider direction="vertical" />
                    <el-button
                      size="default"
                      v-auth="'GoodService.StoreClerks.Create'"
                      type="success"
                      @click="onOpenAddStoreClerk('add')"
                    >
                      <el-icon>
                        <ele-Plus />
                      </el-icon>
                      添加
                    </el-button>
                    <el-button
                      size="default"
                      v-auth="'GoodService.StoreClerks.Delete'"
                      type="danger"
                      @click="onRowDelStoreClerkMany"
                    >
                      <el-icon>
                        <ele-Delete />
                      </el-icon>
                      批量删除
                    </el-button>
                  </el-col>
                </el-row>
              </div>
            </template>
            <div class="store-content" :style="'height: ' + (itemHeight + 42) + 'px'">
              <el-table
                ref="storeClerkTableRef"
                :data="storeClerkState.tableData.data"
                v-loading="storeClerkState.tableData.loading"
                :empty-text="
                  storeClerkState.tableData.param.storeId
                    ? '暂无数据'
                    : '选择左侧门店信息进行浏览.'
                "
                style="width: 100%"
                height="600"
              >
                <el-table-column type="selection" width="55" />
                <el-table-column prop="storeName" label="门店名称" show-overflow-tooltip>
                  <template #default="scope">
                    <label>{{ scope.row.store?.storeName }}</label>
                  </template>
                </el-table-column>
                <el-table-column
                  prop="avatar"
                  label="头像"
                  show-overflow-tooltip
                  width="100"
                >
                  <template #default="scope">
                    <el-avatar :size="50" :src="scope.row.avatar" />
                  </template>
                </el-table-column>
                <el-table-column prop="userName" label="店员昵称" show-overflow-tooltip />
                <el-table-column
                  prop="phoneNumber"
                  label="手机号码"
                  show-overflow-tooltip
                />
                <el-table-column
                  prop="creationTime"
                  label="关联时间"
                  show-overflow-tooltip
                />
                <el-table-column fixed="right" label="操作" width="100">
                  <template #default="scope">
                    <el-button
                      v-auth="'GoodService.StoreClerks.Update'"
                      size="small"
                      text
                      type="warning"
                      @click="onOpenEditStoreClerk('edit', scope.row)"
                      >修改</el-button
                    >
                    <el-button
                      v-auth="'GoodService.StoreClerks.Delete'"
                      size="small"
                      text
                      type="danger"
                      @click="onRowDelStoreClerk(scope.row)"
                      >删除</el-button
                    >
                  </template>
                </el-table-column>
              </el-table>
              <el-pagination
                @size-change="onHandleSizeChange"
                @current-change="onHandleCurrentChange"
                class="mt15"
                v-model:current-page="storeClerkState.tableData.currentPage"
                background
                v-model:page-size="storeClerkState.tableData.param.maxResultCount"
                layout="total, sizes, prev, pager, next, jumper"
                :total="storeClerkState.tableData.total"
              >
              </el-pagination>
            </div>
          </el-card>
        </div>
      </el-col>
    </el-row>
    <store-dialog ref="storeDialogRef" @refresh="getStoreTableData()" />
    <store-clerk-dialog ref="storeClerkDialogRef" @refresh="getStoreClerkTableData()" />
  </div>
</template>

<script setup lang="ts" name="shop-settingStore">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import { useGoodStoreApi } from "/@/api/shopSetting/store/index";
import { useGoodStoreClerkApi } from "/@/api/shopSetting/storeClerk/index";

// 引入组件
const StoreDialog = defineAsyncComponent(
  () => import("/@/views/shopSetting/store/components/storeDialog.vue")
);
const StoreClerkDialog = defineAsyncComponent(
  () => import("/@/views/shopSetting/store/components/storeClerkDialog.vue")
);

// 引入 Api 请求接口
const storeApi = useGoodStoreApi();
const storeClerkApi = useGoodStoreClerkApi();

const itemHeight = ref(0);
const storeDialogRef = ref();
const storeClerkDialogRef = ref();
const currentStoreRow = ref();
const storeClerkTableRef = ref();
const storeState = reactive<StoreState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      storeName: null,
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});
const storeClerkState = reactive<StoreClerkState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      storeId: null,
      userName: null,
      phoneNumber: null,
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 门店选择切换
const currentStoreChange = (val: RowStoreType | undefined) => {
  currentStoreRow.value = val;
  storeClerkState.tableData.param.storeId = val?.id;
  searchGoodStoreClerk();
};

// 打开新增门店弹窗
const onOpenAddStore = (type: string) => {
  storeDialogRef.value.openDialog(type, null);
};

// 打开修改门店弹窗
const onOpenEditStore = (type: string) => {
  if (!currentStoreRow.value) {
    ElMessage.warning("请选择一行数据！");
    return;
  }
  storeDialogRef.value.openDialog(type, currentStoreRow.value);
};

// 删除门店
const onRowDelStore = () => {
  if (!currentStoreRow.value) {
    ElMessage.warning("请选择一行数据！");
    return;
  }
  ElMessageBox.confirm(
    `此操作将永久删除门店名称：“${currentStoreRow.value.storeName}”，是否继续?`,
    "提示",
    {
      confirmButtonText: "确认",
      cancelButtonText: "取消",
      type: "warning",
    }
  ).then(() => {
    storeApi
      .deleteGoodStore(currentStoreRow.value.id)
      .then((result) => {
        if (result) {
          getStoreTableData();
          ElMessage.success("删除成功");
        }
      })
      .catch((err: any) => {
        console.error("提交出错：", err);
      });
  });
};

// 搜索门店
const searchGoodStore = () => {
  storeState.tableData.currentPage = 1;
  getStoreTableData();
};

// 初始化门店表格数据
const getStoreTableData = () => {
  storeState.tableData.loading = true;
  storeApi
    .getGoodStoreAll({
      storeName: storeState.tableData.param.storeName,
    })
    .then((result) => {
      storeState.tableData.total = result.length;
      storeState.tableData.data = result;
      storeState.tableData.loading = false;
    })
    .catch((err: any) => {
      storeState.tableData.loading = false;
      console.error("查询门店列表出错：", err);
    });
};

// 搜索店铺店员关联
const searchGoodStoreClerk = () => {
  storeClerkState.tableData.currentPage = 1;
  getStoreClerkTableData();
};

// 打开新增店铺店员关联弹窗
const onOpenAddStoreClerk = (type: string) => {
  if (!storeClerkState.tableData.param.storeId) {
    ElMessage.warning("请先选择左侧门店信息！");
    return;
  }
  storeClerkDialogRef.value.openDialog(
    type,
    storeClerkState.tableData.param.storeId,
    null
  );
};

// 打开修改店铺店员关联弹窗
const onOpenEditStoreClerk = (type: string, row: RowStoreClerkType) => {
  storeClerkDialogRef.value.openDialog(
    type,
    storeClerkState.tableData.param.storeId,
    row
  );
};

// 删除店铺店员
const onRowDelStoreClerk = (row: RowStoreClerkType) => {
  ElMessageBox.confirm(`此操作将永久删除店员呢称：“${row.userName}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    storeClerkApi
      .deleteGoodStoreClerk(row.id)
      .then((result) => {
        if (result) {
          getStoreClerkTableData();
          ElMessage.success("删除成功");
        }
      })
      .catch((err: any) => {
        console.error("提交出错：", err);
      });
  });
};

// 批量删除店铺店员
const onRowDelStoreClerkMany = () => {
  const selRows = storeClerkTableRef.value.getSelectionRows() as RowStoreClerkType[];
  if (selRows.length < 1) {
    ElMessage.warning("您还未选择要删除的店员信息！");
    return;
  }
  const ids = selRows.map((p) => p.id);
  ElMessageBox.confirm(`您选择了${selRows.length}条数据，确定要删除吗?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    storeClerkApi
      .deleteGoodStoreClerkMany(ids)
      .then((result) => {
        if (result) {
          getStoreClerkTableData();
          ElMessage.success("删除成功");
        }
      })
      .catch((err: any) => {
        console.error("提交出错：", err);
      });
  });
};

// 查询店铺店员关联信息
const getStoreClerkTableData = () => {
  storeClerkState.tableData.loading = true;
  storeClerkApi
    .getGoodStoreClerkList(storeClerkState.tableData.param)
    .then((result) => {
      storeClerkState.tableData.total = result.totalCount;
      storeClerkState.tableData.data = result.items;
      storeClerkState.tableData.loading = false;
    })
    .catch((err: any) => {
      storeClerkState.tableData.loading = false;
      console.error("查询店铺店员关联信息列表出错：", err);
    });
};

// 分页改变
const onHandleSizeChange = (val: number) => {
  storeClerkState.tableData.param.maxResultCount = val;
  getStoreClerkTableData();
};

// 分页改变
const onHandleCurrentChange = (val: number) => {
  storeClerkState.tableData.currentPage = val;
  storeClerkState.tableData.param.skipCount =
    (storeClerkState.tableData.currentPage - 1) *
    storeClerkState.tableData.param.maxResultCount;
  getStoreClerkTableData();
};

// 页面加载完时
onMounted(() => {
  itemHeight.value = window.innerHeight - 260;
  getStoreTableData();
});
</script>

<style scoped lang="scss">
.shop-setting-store-container {
  :deep(.el-card__body) {
    display: flex;
    flex-direction: column;
    flex: 1;
    overflow: auto;
  }
}

.card-store-container {
  .store-content {
    overflow-y: auto;
  }
}
</style>
