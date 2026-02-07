<template>
  <div class="system-organization-unit-container layout-padding">
    <el-row :gutter="15">
      <el-col :span="6">
        <div class="card-tree-container">
          <el-card shadow="hover" class="layout-padding-auto">
            <template #header>
              <div class="card-header">
                <span class="iconfont icon-shuxing">&nbsp;组织结构树</span>
                <el-button
                  v-auth="'BaseService.OrganizationUnits.Create'"
                  class="button ou-btn"
                  text
                  :icon="Plus"
                  @click="onAddOu"
                  >添加组织</el-button
                >
              </div>
            </template>
            <div class="tree-content" :style="'height:' + itemHeight + 'px'">
              <!-- v-loading="state.treeData.total < 1" -->
              <el-tree
                :data="state.treeData.data"
                :props="state.treeData.props"
                :expand-on-click-node="false"
                default-expand-all
                @current-change="treeCurrentChange"
              >
                <template #default="{ node }">
                  <span
                    title="[单击右键]进行操作"
                    @contextmenu.prevent="ouMenuRef?.openMenu($event, node)"
                  >
                    {{ node.label }}
                  </span>
                </template>
              </el-tree>
            </div>
          </el-card>
        </div>
      </el-col>
      <el-col :span="18">
        <div class="card-user-role-container">
          <el-card shadow="hover" class="layout-padding-auto">
            <template #header>
              <div class="card-header">
                <span>
                  <el-icon><UserFilled /></el-icon>
                  {{ state.treeData.currentData.displayName }}
                </span>
              </div>
            </template>
            <div class="user-role-content" :style="'height:' + itemHeight + 'px'">
              <el-tabs
                type="border-card"
                v-if="state.treeData.currentData.id"
                v-model="activeName"
                class="ur-tabs"
                @tab-click="handleClick"
              >
                <el-tab-pane
                  label="用户"
                  name="User"
                  v-auth="'BaseService.OrganizationUnits.ManagingUser'"
                >
                  <user-organization-unit ref="userOrganizationUnitRef" />
                </el-tab-pane>
                <el-tab-pane
                  label="角色"
                  name="Role"
                  v-auth="'BaseService.OrganizationUnits.ManagingRole'"
                >
                  <organization-unit-role ref="organizationUnitRoleRef" />
                </el-tab-pane>
              </el-tabs>
              <el-empty v-else description="选择一个组织单元以浏览其成员." />
            </div>
          </el-card>
        </div>
      </el-col>
    </el-row>
    <ou-menu :menu-items="ouMenuItems" ref="ouMenuRef" />
    <ou-menu-dialog ref="ouMenuDialogRef" @refresh="getTreeData" />
  </div>
</template>

<script setup lang="ts" name="systemOrganizationUnit">
import { defineAsyncComponent, reactive, onMounted, ref, nextTick } from "vue";
import { ElMessageBox, ElMessage, ElLoading } from "element-plus";
import type { TabsPaneContext } from "element-plus";
import { Plus, UserFilled } from "@element-plus/icons-vue";
import { useOrganizationUnitApi } from "/@/api/system/organizationUnit/index";
import { OuMenuItem } from "./components/ouMenu.vue";

// 引入[组织单元菜单选择]组件
const OuMenu = defineAsyncComponent(
  () => import("/@/views/system/organizationUnit/components/ouMenu.vue")
);
const OuMenuDialog = defineAsyncComponent(
  () => import("/@/views/system/organizationUnit/components/ouMenuDialog.vue")
);

// 引入[用户组织单元]组件
const UserOrganizationUnit = defineAsyncComponent(
  () => import("/@/views/system/organizationUnit/components/userOrganizationUnit.vue")
);

// 引入[角色组织单元]组件
const OrganizationUnitRole = defineAsyncComponent(
  () => import("/@/views/system/organizationUnit/components/organizationUnitRole.vue")
);

// 引入组织单位 Api 请求接口
const organizationUnitApi = useOrganizationUnitApi();

import { auth } from "/@/utils/authFunction";

const userOrganizationUnitRef = ref();
const organizationUnitRoleRef = ref();
const itemHeight = ref(0);
const state = reactive({
  treeData: {
    showRightMenu: false,
    props: {
      children: "children",
      label: "displayName",
    },
    data: [] as any,
    total: 0,
    currentData: {
      id: "",
      displayName: "",
    },
    loading: false,
    param: {
      includeDetails: true,
    },
  },
});

// 初始化组织单位结构树数据
const getTreeData = () => {
  state.treeData.loading = true;
  organizationUnitApi
    .getOrganizationUnitTree(state.treeData.param)
    .then((result) => {
      state.treeData.total = result.length;
      state.treeData.data = result;
      state.treeData.loading = false;
    })
    .catch((err: any) => {
      state.treeData.loading = false;
      console.error("查询组织单位结构树出错：", err);
    });
};

// 组织单位结构树选中节点变化时触发
const treeCurrentChange = (data: any, node: any) => {
  ouMenuRef?.value?.closeMenu();
  const loading = ElLoading.service({
    lock: true,
    text: "Loading",
    background: "rgba(0, 0, 0, 0.1)",
  });
  state.treeData.currentData.id = data.id;
  state.treeData.currentData.displayName = data.displayName;
  nextTick(() => {
    searchChildOuUser();
    searchChildOuRole();
  });
  // 查询[用户]组织机构
  function searchChildOuUser(): void {
    if (!userOrganizationUnitRef.value) {
      // 暂停1秒在执行
      setTimeout(() => {
        searchChildOuUser();
      }, 1000);
      return;
    }
    loading.close();
    userOrganizationUnitRef.value.searchOuUser(state.treeData.currentData.id);
  }
  // 查询[角色]组织机构
  function searchChildOuRole(): void {
    if (!organizationUnitRoleRef.value) {
      // 暂停1秒在执行
      setTimeout(() => {
        searchChildOuRole();
      }, 1000);
      return;
    }
    loading.close();
    organizationUnitRoleRef.value.searchOuRole(state.treeData.currentData.id);
  }
};

// 定义组织单元选择
const ouMenuRef = ref();
const ouMenuDialogRef = ref();
const ouMenuItems = ref<OuMenuItem[]>([]);
if (auth("BaseService.OrganizationUnits.Create")) {
  ouMenuItems.value.push({
    name: "添加下级组织",
    icon: "add",
    action: (item: any) => {
      ouMenuDialogRef.value.openDialog("addChildren", item.data);
    },
  });
}
if (auth("BaseService.OrganizationUnits.Update")) {
  ouMenuItems.value.push({
    name: "编辑",
    icon: "edit",
    action: (item: any) => {
      ouMenuDialogRef.value.openDialog("edit", item.data);
    },
  });
}
if (auth("BaseService.OrganizationUnits.Delete")) {
  ouMenuItems.value.push({
    name: "删除",
    icon: "delete",
    action: (item: any) => {
      onDeleteOu(item.data);
    },
  });
}

// 添加组织
const onAddOu = () => {
  ouMenuDialogRef.value.openDialog("add");
};

// 删除组织
const onDeleteOu = (data: any) => {
  ElMessageBox.confirm(`此操作将永久删除组织：“${data.displayName}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    organizationUnitApi
      .deleteOrganizationUnit(data.id)
      .then((result) => {
        if (result) {
          getTreeData();
          ElMessage.success("删除成功");
        }
      })
      .catch((err) => {
        console.error("提交出错：", err);
      });
  });
};

// 选中(用户|角色)名称
const activeName = ref("User");
const handleClick = (tab: TabsPaneContext, event: Event) => {
  console.log(tab, event);
};

// 页面加载完时
onMounted(() => {
  itemHeight.value = window.innerHeight - 212;
  getTreeData();
});
</script>

<style scoped lang="scss">
.system-organization-unit-container {
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

.card-tree-container {
  .tree-content {
    overflow-y: auto;
  }
}

.card-user-role-container {
  .user-role-content {
    // min-height: 500px;
  }
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 25px;
}

.ou-btn {
  color: #40a9ff;
}

.ur-tabs > .el-tabs__content {
  padding: 32px;
  color: #6b778c;
  font-size: 32px;
  font-weight: 600;
}
</style>
