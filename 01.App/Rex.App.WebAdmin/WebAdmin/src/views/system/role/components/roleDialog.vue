<template>
  <div class="system-role-dialog-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="769px"
    >
      <el-form
        ref="roleDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="角色名称" prop="name">
              <el-input
                v-model="state.ruleForm.name"
                placeholder="请输入角色名称"
                :disabled="!!state.dialog.editId && state.ruleForm.name == 'admin'"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="是否默认">
              <el-switch
                v-model="state.ruleForm.isDefault"
                inline-prompt
                active-text="是"
                inactive-text="否"
              ></el-switch>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="菜单权限">
              <el-tree
                ref="menuTreeRef"
                :data="state.menuData"
                :props="state.menuProps"
                @check="checkMenuRoleNode"
                node-key="permissionIdentifying"
                :default-checked-keys="state.permissionDefaultChecked"
                show-checkbox
                class="menu-data-tree"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            v-auth-all="[
              'AbpIdentity.Roles.Create',
              'AbpIdentity.Roles.Update',
              'AbpIdentity.Roles.ManagePermissions',
            ]"
            type="primary"
            @click="submitValidate(roleDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="systemRoleDialog">
import { nextTick, reactive, ref } from "vue";
import { i18n } from "/@/i18n/index";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useMenuApi } from "/@/api/system/menu/index";
import { useRoleApi } from "/@/api/system/role/index";
import { useRoleMenuApi } from "/@/api/system/roleMenu/index";
import { usePermissionApi } from "/@/api/system/permission/index";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入菜单 Api 请求接口
const menuApi = useMenuApi();

// 引入角色 Api 请求接口
const roleApi = useRoleApi();

// 引入角色菜单 Api 请求接口
const roleMenuApi = useRoleMenuApi();

// 引入权限 Api 请求接口
const permissionApi = usePermissionApi();

// 定义变量内容
const roleDialogFormRef = ref();
const menuTreeRef = ref();
const state = reactive({
  ruleForm: {
    name: "", // 角色名称
    isDefault: false, // 是否默认
    concurrencyStamp: "",
  },
  permissionDefaultChecked: [] as any,
  permissionForm: {
    permissions: [] as any,
    menuIds: [] as any,
  },
  menuData: [] as RowMenuType[],
  menuList: [] as any,
  menuProps: {
    children: "children",
    label: "label",
  },
  dialog: {
    isShowDialog: false,
    type: "",
    editId: "",
    title: "",
    submitTxt: "",
  },
});

// 打开弹窗
const openDialog = (type: string, row: RowRoleType) => {
  state.permissionDefaultChecked = [];
  state.dialog.type = type;
  if (row?.id) {
    getPermissionBind(row.name, () => {
      getMdata();
    });
    return;
  }
  getMdata();

  // 获取菜单数据
  function getMdata(): void {
    getMenuData(() => {
      nextTick(() => {
        state.dialog.editId = "";
        resetForm();
        if (type === "edit") {
          state.dialog.editId = row.id;
          state.ruleForm.name = row.name;
          state.ruleForm.isDefault = row.isDefault;
          getRoleMenuList(row.id);
          state.dialog.title = "修改角色";
          state.dialog.submitTxt = "修 改";
        } else {
          state.dialog.title = "新增角色";
          state.dialog.submitTxt = "新 增";
        }
        state.dialog.isShowDialog = true;
      });
    });
  }
};

// 获取角色菜单信息
const getRoleMenuList = (roleId: string) => {
  roleMenuApi
    .getRoleMenuByRoleId(roleId)
    .then(async (result) => {
      if (result) {
        state.permissionForm.menuIds = [];
        result.forEach((rMenu: any) => {
          state.permissionForm.menuIds.push(rMenu.menuId);
        });
      }
    })
    .catch((err) => {
      console.error("查询出错：", err);
    });
};

// 关闭弹窗
const closeDialog = () => {
  state.dialog.isShowDialog = false;
};

// 取消
const onCancel = () => {
  closeDialog();
};

// 表单验证规则
const formRules = reactive<FormRules>({
  name: [{ required: true, message: "请输入角色名称！", trigger: "blur" }],
});

// 提交验证
const submitValidate = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;
  await formEl.validate((valid, fields) => {
    if (valid) {
      onSubmit();
    } else {
      console.warn("未验证通过!", fields);
    }
  });
};

// 提交
const onSubmit = () => {
  if (state.dialog.type == "edit") {
    // 修改
    roleApi
      .updateRole(state.dialog.editId, state.ruleForm)
      .then(async (result) => {
        if (result) {
          await savePermission();
          let rMenuData = Object.assign(
            { roleId: result.id },
            { menuIds: state.permissionForm.menuIds }
          );
          await saveRoleMenu(rMenuData); // 保存角色菜单
          ElMessage.success("修改成功！");
          closeDialog(); // 关闭弹窗
          emit("refresh");
        }
      })
      .catch((err) => {
        console.error("提交出错：", err);
      });
    return;
  }

  // 添加
  roleApi
    .addRole(state.ruleForm)
    .then(async (result) => {
      if (result) {
        await savePermission(); // 保存角色权限
        let rMenuData = Object.assign(
          { roleId: result.id },
          { menuIds: state.permissionForm.menuIds }
        );
        await saveRoleMenu(rMenuData); // 保存角色菜单
        ElMessage.success("添加成功！");
        closeDialog(); // 关闭弹窗
        emit("refresh");
      }
    })
    .catch((err) => {
      console.error("提交出错：", err);
    });
};

// 保存权限
const savePermission = async () => {
  return new Promise((resolve, reject) => {
    debugger;
    permissionApi
      .updatePermissions(
        { providerName: "R", providerKey: state.ruleForm.name },
        state.permissionForm
      )
      .then((result) => {
        resolve(result);
      })
      .catch((err) => {
        reject(err);
        console.error("保存权限信息出错：", err);
      });
  });
};

// 保存角色菜单
const saveRoleMenu = async (roleMenu: object) => {
  return new Promise((resolve, reject) => {
    roleMenuApi
      .updateManyRoleMenu(roleMenu)
      .then((result) => {
        resolve(result);
      })
      .catch((err) => {
        reject(err);
        console.error("保存角色菜单信息出错：", err);
      });
  });
};

// 获取菜单权限数据
const getMenuData = (callBack: Function) => {
  menuApi
    .getMenuFilter({ sorting: "MenuSort", filter: "" })
    .then((result) => {
      if (!result) return;
      result.forEach((m: any) => {
        m["label"] = i18n.global.t(m.meta?.title as string);
      });
      state.menuList = _.cloneDeep(result);
      let pMenuList = result.filter((m) => m.pId == null || m.pId == "");
      setTreeMenu(pMenuList, result);
      state.menuData = pMenuList;
      callBack();
    })
    .catch((err) => {
      console.error("查询菜单权限出错：", err);
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

// 获取权限绑定
const getPermissionBind = (pKey: string, callBack: Function) => {
  permissionApi
    .getPermissions({ providerName: "R", providerKey: pKey })
    .then((result) => {
      if (!result || !result.groups) return;
      for (let i = 0; i < result.groups.length; i++) {
        let gp = result.groups[i];
        for (let j = 0; j < gp.permissions.length; j++) {
          let per = gp.permissions[j];
          if (per.isGranted) {
            state.permissionDefaultChecked.push(per.name);
          }
        }
      }
      for (let i = 0; i < state.permissionDefaultChecked.length; i++) {
        let val = state.permissionDefaultChecked[i];
        state.permissionForm.permissions.push({
          name: val,
          isGranted: true,
        });
      }
      callBack();
    })
    .catch((err) => {
      console.error("查询权限信息出错：", err);
    });
};

// 点击权限菜单之后触发
const checkMenuRoleNode = (data: any, treeNode: any) => {
  state.permissionForm.permissions = [];
  state.menuList.forEach((m: any) => {
    if (m.permissionIdentifying) {
      state.permissionForm.permissions.push({
        name: m.permissionIdentifying,
        isGranted: treeNode.checkedKeys.includes(m.permissionIdentifying),
      });
    }
  });
  state.permissionForm.menuIds = [];
  let mTree = menuTreeRef.value;
  if (mTree) {
    let halfTreeNodes = mTree.getHalfCheckedNodes();
    halfTreeNodes.forEach((hTree: any) => {
      state.permissionForm.menuIds.push(hTree.id);
    });
    let treeNodes = mTree.getCheckedNodes();
    treeNodes.forEach((tree: any) => {
      state.permissionForm.menuIds.push(tree.id);
    });
  }
};

// 表单重置
const resetForm = () => {
  state.ruleForm.name = "";
  state.ruleForm.isDefault = false;
  state.ruleForm.concurrencyStamp = "";
};

// 暴露变量
defineExpose({
  openDialog,
});
</script>

<style scoped lang="scss">
.system-role-dialog-container {
  .menu-data-tree {
    width: 100%;
    border: 1px solid var(--el-border-color);
    border-radius: var(--el-input-border-radius, var(--el-border-radius-base));
    padding: 5px;
    max-height: 440px;
    overflow-y: auto;
  }
}
</style>
