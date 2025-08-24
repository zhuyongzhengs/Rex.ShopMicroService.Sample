<template>
  <div class="service-description-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="service-description-search">
        <el-row :gutter="15">
          <el-col :span="6">
            <el-input
              v-model="state.tableData.param.title"
              size="default"
              placeholder="请输入名称"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.type"
              size="default"
              placeholder="选择类型"
            >
              <el-option
                v-for="type in serviceNoteTypes"
                :key="type.value"
                :label="type.description"
                :value="type.value"
              />
            </el-select>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.isShow"
              size="default"
              placeholder="是否展示"
            >
              <el-option label="开启" :value="true" />
              <el-option label="关闭" :value="false" />
            </el-select>
          </el-col>
          <el-col :span="12">
            <el-button
              size="default"
              type="primary"
              class="ml10"
              @click="searchServiceDescription"
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
          <el-col :span="9"></el-col>
        </el-row>
      </div>
    </el-card>
    <el-card shadow="hover" class="layout-padding-auto">
      <template #header>
        <el-button
          size="default"
          v-auth="'GoodService.ServiceDescriptions.Create'"
          type="success"
          @click="onOpenAddServiceDescription('add')"
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
        <el-table-column prop="sort" label="排序" width="60" />
        <el-table-column prop="title" label="名称" show-overflow-tooltip width="200" />
        <el-table-column prop="description" label="描述" show-overflow-tooltip />
        <el-table-column prop="type" label="类型" width="150">
          <template #default="scope">
            <span>{{ showType(scope.row.type) }}</span>
          </template>
        </el-table-column>
        <el-table-column prop="isShow" label="是否展示" show-overflow-tooltip width="100">
          <template #default="scope">
            <el-switch
              v-model="scope.row.isShow"
              inline-prompt
              active-text="开启"
              inactive-text="关闭"
              size="default"
              @change="onIsShow($event, scope.row)"
            />
          </template>
        </el-table-column>
        <el-table-column label="操作" width="100">
          <template #default="scope">
            <el-button
              v-auth="'GoodService.ServiceDescriptions.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditServiceDescription('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'GoodService.ServiceDescriptions.Delete'"
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
    <service-description-dialog
      :serviceNoteTypes="serviceNoteTypes"
      ref="serviceDescriptionDialogRef"
      @refresh="getTableData()"
    />
  </div>
</template>

<script setup lang="ts" name="serviceDescription">
import { defineAsyncComponent, reactive, onMounted, ref, computed } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import { useServiceDescriptionApi } from "/@/api/shopSetting/serviceDescription/index";

// 引入组件
const ServiceDescriptionDialog = defineAsyncComponent(
  () =>
    import(
      "/@/views/shopSetting/serviceDescription/components/serviceDescriptionDialog.vue"
    )
);

// 引入商城服务描述 Api 请求接口
const serviceDescriptionApi = useServiceDescriptionApi();

// 定义变量内容
const serviceDescriptionDialogRef = ref();
const serviceNoteTypes = ref([] as EnumValueType[]);
const state = reactive<ServiceDescriptionState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      title: null,
      type: null,
      isShow: null,
      sorting: "Sort",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 展示类型
const showType = (type: number) => {
  let typeVal = serviceNoteTypes.value.find((p) => p.value == String(type));
  return typeVal?.description;
};

// 查询商城服务描述类型
const getServiceNoteTypes = () => {
  serviceDescriptionApi
    .getShopServiceNoteTypes()
    .then((result) => {
      serviceNoteTypes.value = result;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("商城服务描述出错：", err);
    });
};

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  serviceDescriptionApi
    .getServiceDescriptionList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询商城服务描述列表出错：", err);
    });
};

// 搜索
const searchServiceDescription = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.title = null;
  state.tableData.param.type = null;
  state.tableData.param.isShow = null;
};

// 打开新增商城服务描述弹窗
const onOpenAddServiceDescription = (type: string) => {
  serviceDescriptionDialogRef.value.openDialog(type);
};

// 打开修改商城服务描述弹窗
const onOpenEditServiceDescription = (type: string, row: RowServiceDescriptionType) => {
  serviceDescriptionDialogRef.value.openDialog(type, row);
};

// 是否显示
const onIsShow = (val: boolean, serviceDescription: RowServiceDescriptionType) => {
  serviceDescriptionApi
    .updateServiceDescriptionIsShow(serviceDescription.id, val)
    .then((result) => {
      console.log("修改[" + serviceDescription.title + "]是否显示，结果：", result);
    })
    .catch((err: any) => {
      serviceDescription.isShow = !val;
      console.error("修改出错：", err);
    });
};

// 删除商城服务描述
const onRowDel = (row: RowServiceDescriptionType) => {
  ElMessageBox.confirm(
    `此操作将永久删除商城服务描述名称：“${row.title}”，是否继续?`,
    "提示",
    {
      confirmButtonText: "确认",
      cancelButtonText: "取消",
      type: "warning",
    }
  ).then(() => {
    serviceDescriptionApi
      .deleteServiceDescription(row.id)
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
  getServiceNoteTypes();
  getTableData();
});
</script>

<style scoped lang="scss">
.service-description-container {
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
