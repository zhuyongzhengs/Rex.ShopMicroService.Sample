<template>
  <div class="system-menu-container layout-pd">
    <el-card shadow="hober" class="mb15">
      <div class="system-menu-search">
        <el-row :gutter="15">
          <el-col :span="6">
            <el-input
              v-model="state.tableData.param.filter"
              size="default"
              placeholder="请输入路由名称"
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
            v-auth="'BaseService.Menus.Create'"
            size="default"
            type="success"
            @click="onOpenAddMenu"
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
        style="width: 100%; height: 67vh"
        row-key="id"
        :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
      >
        <el-table-column fixed label="菜单名称" width="220" show-overflow-tooltip>
          <template #default="scope">
            <SvgIcon :name="scope.row.meta.icon" />
            <span class="ml10">{{ $t(scope.row.meta.title) }}</span>
          </template>
        </el-table-column>

        <el-table-column
          prop="name"
          label="路由名称"
          width="150"
          show-overflow-tooltip
        ></el-table-column>

        <el-table-column
          prop="path"
          label="路由路径"
          width="200"
          show-overflow-tooltip
        ></el-table-column>

        <el-table-column label="组件路径" width="240" show-overflow-tooltip>
          <template #default="scope">
            <span>{{ scope.row.component }}</span>
          </template>
        </el-table-column>

        <el-table-column label="权限标识" width="260" show-overflow-tooltip>
          <template #default="scope">
            <span>{{ scope.row.permissionIdentifying }}</span>
          </template>
        </el-table-column>

        <el-table-column label="排序" show-overflow-tooltip width="80">
          <template #default="scope">
            {{ scope.row.menuSort }}
          </template>
        </el-table-column>

        <el-table-column label="类型" show-overflow-tooltip width="80">
          <template #default="scope">
            <el-tag v-if="scope.row.menuType === 1" type="success" size="small"
              >菜单</el-tag
            >
            <el-tag v-if="scope.row.menuType === 2" type="plain" size="small"
              >按钮</el-tag
            >
          </template>
        </el-table-column>

        <el-table-column label="是否外链" show-overflow-tooltip width="90">
          <template #default="scope">
            <el-checkbox v-model="scope.row.isLink" size="large" disabled />
          </template>
        </el-table-column>

        <el-table-column label="外链地址" show-overflow-tooltip width="150">
          <template #default="scope">
            {{ scope.row.meta.link }}
          </template>
        </el-table-column>

        <el-table-column label="路由重定向" show-overflow-tooltip width="200">
          <template #default="scope">
            {{ scope.row.redirect }}
          </template>
        </el-table-column>

        <el-table-column label="是否隐藏" show-overflow-tooltip width="90">
          <template #default="scope">
            <el-checkbox v-model="scope.row.meta.isHide" size="large" disabled />
          </template>
        </el-table-column>

        <el-table-column label="是否缓存" show-overflow-tooltip width="90">
          <template #default="scope">
            <el-checkbox v-model="scope.row.meta.isKeepAlive" size="large" disabled />
          </template>
        </el-table-column>

        <el-table-column label="是否固定" show-overflow-tooltip width="90">
          <template #default="scope">
            <el-checkbox v-model="scope.row.meta.isAffix" size="large" disabled />
          </template>
        </el-table-column>

        <el-table-column label="是否内嵌" show-overflow-tooltip width="90">
          <template #default="scope">
            <el-checkbox v-model="scope.row.meta.isIframe" size="large" disabled />
          </template>
        </el-table-column>

        <el-table-column fixed="right" label="操作" show-overflow-tooltip width="140">
          <template #default="scope">
            <el-button
              v-auth="'BaseService.Menus.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditMenu('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'BaseService.Menus.Delete'"
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
    <menu-dialog ref="menuDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="systemMenu">
import { defineAsyncComponent, ref, onMounted, reactive } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import { useMenuApi } from "/@/api/system/menu/index";
import { setBackEndControlRefreshRoutes } from "/@/router/backEnd";

// 引入组件
const MenuDialog = defineAsyncComponent(
  () => import("/@/views/system/menu/components/menuDialog.vue")
);

// 引入菜单 Api 请求接口
const menuApi = useMenuApi();

const menuDialogRef = ref();
const state = reactive<SysMenuState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      filter: "",
      sorting: "MenuSort",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 获取菜单数据
const getTableData = () => {
  state.tableData.loading = true;
  menuApi
    .getMenuFilter(state.tableData.param)
    .then((result) => {
      let pMenuList = result.filter((m) => m.pId == null || m.pId == "");
      setTreeMenu(pMenuList, result);
      state.tableData.data = pMenuList;
      state.tableData.loading = false;
    })
    .catch((err) => {
      state.tableData.loading = false;
      console.error("查询菜单列表出错：", err);
    });
};

// 转换为Tree
function setTreeMenu(pMenus: RowMenuType[], menus: RowMenuType[]) {
  pMenus.forEach((pMenu) => {
    menus.forEach((menu) => {
      if (menu.pId == pMenu.id) {
        if (!pMenu.children) pMenu.children = [];
        pMenu.children.push(menu);
      }
    });
    if (pMenu.children) {
      setTreeMenu(pMenu.children, menus);
    }
  });
}

// 打开新增菜单弹窗
const onOpenAddMenu = (type: string) => {
  menuDialogRef.value.openDialog(type);
};

// 打开编辑菜单弹窗
const onOpenEditMenu = (type: string, row: RowMenuType) => {
  let pMenu = getPrentMenu(state.tableData.data, row.id);
  menuDialogRef.value.openDialog(type, row, pMenu);
};

// 获取上级菜单
const getPrentMenu = (tree: RowMenuType[], childId: string, pIds: string[] = []) => {
  for (const node of tree) {
    if (node.id === childId) {
      return pIds;
    } else if (node.children && node.children.length > 0) {
      const result = getPrentMenu(node.children, childId, pIds.concat(node.id)) as any;
      if (result !== null) {
        return result;
      }
    }
  }
  return null;
};

// 删除当前行
const onTabelRowDel = (row: RowMenuType) => {
  ElMessageBox.confirm(`此操作将永久删除路由：${row.name}, 是否继续?`, "提示", {
    confirmButtonText: "删除",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    menuApi
      .deleteMenu(row.id)
      .then((result) => {
        if (result) {
          getTableData();
          ElMessage.success("删除成功");
          setBackEndControlRefreshRoutes(); // 刷新菜单
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
