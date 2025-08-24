<template>
  <div class="good-category-container layout-pd">
    <el-card shadow="hober" class="mb15" style="display: none">
      <div class="good-category-search">
        <el-row :gutter="15">
          <el-col :span="6">
            <el-input
              v-model="state.tableData.param.name"
              size="default"
              placeholder="请输入分类名称"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="6">
            <el-button size="default" type="primary" class="ml10" @click="getTableData">
              <el-icon>
                <ele-Search />
              </el-icon>
              查询
            </el-button>
          </el-col>
          <el-col :span="12"></el-col>
        </el-row>
      </div>
    </el-card>
    <el-card shadow="hover">
      <template #header>
        <div class="card-header">
          <el-button
            v-auth="'GoodService.GoodCategorys.Create'"
            size="default"
            type="success"
            @click="onOpenAddGoodCategory"
          >
            <el-icon>
              <ele-Plus />
            </el-icon>
            新增
          </el-button>
        </div>
      </template>
      <el-table
        :data="state.tableData.data"
        v-loading="state.tableData.loading"
        style="width: 100%"
        max-height="520"
        row-key="id"
        :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
        default-expand-all
      >
        <el-table-column
          prop="name"
          label="分类名称"
          show-overflow-tooltip
        ></el-table-column>

        <el-table-column prop="imageUrl" label="图片" show-overflow-tooltip width="150">
          <template #default="scope">
            <el-image
              class="img-logo"
              fit="scale-down"
              :src="scope.row.imageUrl"
              :zoom-rate="1.2"
              :max-scale="7"
              :min-scale="0.2"
              :preview-src-list="[scope.row.imageUrl]"
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

        <el-table-column prop="isShow" label="是否显示" show-overflow-tooltip width="100">
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
        <el-table-column prop="sort" label="排序" show-overflow-tooltip width="65" />
        <el-table-column fixed="right" label="操作" show-overflow-tooltip width="140">
          <template #default="scope">
            <el-button
              v-auth="'GoodService.GoodCategorys.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditGoodCategory('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'GoodService.GoodCategorys.Delete'"
              size="small"
              text
              type="danger"
              @click="onTabelRowDel(scope.row)"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
    </el-card>
    <good-category-dialog ref="goodCategoryDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="goodCategory">
import { defineAsyncComponent, ref, onMounted, reactive, computed } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import { Picture as IconPicture } from "@element-plus/icons-vue";
import { useGoodCategoryApi } from "/@/api/good/category/index";

// 引入组件
const GoodCategoryDialog = defineAsyncComponent(
  () => import("/@/views/good/category/components/categoryDialog.vue")
);

// 引入商品分类 Api 请求接口
const goodCategoryApi = useGoodCategoryApi();

// 定义变量
const goodCategoryDialogRef = ref();
const state = reactive<GoodCategoryState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      name: null,
      sorting: "Sort",
      skipCount: 0,
      maxResultCount: 1000,
    },
  },
});

// 获取商品分类数据
const getTableData = () => {
  state.tableData.loading = true;
  goodCategoryApi
    .getGoodCategoryShow()
    .then((result) => {
      let pGoodCategoryList = result.filter(
        (m) => m.parentId == null || m.parentId == ""
      );
      setTreeGoodCategory(pGoodCategoryList, result);
      state.tableData.data = pGoodCategoryList;
      state.tableData.loading = false;
    })
    .catch((err) => {
      state.tableData.loading = false;
      console.error("查询商品分类列表出错：", err);
    });
};

// 转换为Tree
function setTreeGoodCategory(
  pGoodCategorys: RowGoodCategoryType[],
  categorys: RowGoodCategoryType[]
) {
  pGoodCategorys.forEach((pGoodCategory) => {
    categorys.forEach((category) => {
      if (category.parentId == pGoodCategory.id) {
        if (!pGoodCategory.children) pGoodCategory.children = [];
        pGoodCategory.children.push(category);
      }
    });
    if (pGoodCategory.children) {
      setTreeGoodCategory(pGoodCategory.children, categorys);
    }
  });
}

// 是否显示
const onIsShow = (val: boolean, category: RowGoodCategoryType) => {
  goodCategoryApi
    .updateGoodCategoryIsShow(category.id, val)
    .then((result) => {
      console.log("修改[" + category.name + "]是否显示，结果：", result);
    })
    .catch((err: any) => {
      category.isShow = !val;
      console.error("修改出错：", err);
    });
};

// 打开新增商品分类弹窗
const onOpenAddGoodCategory = (type: string) => {
  goodCategoryDialogRef.value.openDialog(type);
};

// 打开编辑商品分类弹窗
const onOpenEditGoodCategory = (type: string, row: RowGoodCategoryType) => {
  let pGoodCategory = getPrentGoodCategory(state.tableData.data, row.id);
  goodCategoryDialogRef.value.openDialog(type, row, pGoodCategory);
};

// 获取上级商品分类
const getPrentGoodCategory = (
  tree: RowGoodCategoryType[],
  childId: string,
  parentIds: string[] = []
) => {
  for (const node of tree) {
    if (node.id === childId) {
      return parentIds;
    } else if (node.children && node.children.length > 0) {
      const result = getPrentGoodCategory(
        node.children,
        childId,
        parentIds.concat(node.id)
      ) as any;
      if (result !== null) {
        return result;
      }
    }
  }
  return null;
};

// 删除当前行
const onTabelRowDel = (row: RowGoodCategoryType) => {
  ElMessageBox.confirm(`此操作将永久删除路由：${row.name}, 是否继续?`, "提示", {
    confirmButtonText: "删除",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    goodCategoryApi
      .deleteGoodCategory(row.id)
      .then((result) => {
        if (result) {
          getTableData();
          ElMessage.success("删除成功");
        }
      })
      .catch((err) => {
        console.error("提交出错：", err);
      });
  });
};

// 页面加载完时
onMounted(() => {
  getTableData();
});
</script>

<style scoped lang="scss">
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
