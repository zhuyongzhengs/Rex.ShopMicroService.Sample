<template>
  <div class="system-menu-dialog-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      :lock-scroll="false"
      width="769px"
    >
      <el-form
        ref="menuDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="80px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="上级菜单" prop="pId">
              <el-cascader
                :options="state.menuData"
                :props="{ checkStrictly: true, value: 'id', label: 'title' }"
                placeholder="请选择上级菜单"
                clearable
                class="w100"
                v-model="state.ruleForm.pMenu"
                @change="pMenuChange"
              >
                <template #default="{ node, data }">
                  <span>{{ data.title }}</span>
                  <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
                </template>
              </el-cascader>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="菜单类型" prop="menuType">
              <el-radio-group v-model="state.ruleForm.menuType">
                <el-radio :label="1">菜单</el-radio>
                <el-radio :label="2">按钮</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="菜单名称" prop="title">
              <el-input
                v-model="state.ruleForm.meta.title"
                placeholder="格式：message.router.xxx"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <template v-if="state.ruleForm.menuType === 1">
            <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
              <el-form-item label="菜单图标" prop="icon">
                <icon-selector
                  placeholder="请输入菜单图标"
                  v-model="state.ruleForm.meta.icon"
                />
              </el-form-item>
            </el-col>
            <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
              <el-form-item label="路由名称" prop="name">
                <el-input
                  v-model="state.ruleForm.name"
                  placeholder="路由中的 name 值"
                  clearable
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
              <el-form-item label="路由路径" prop="path">
                <el-input
                  v-model="state.ruleForm.path"
                  placeholder="路由中的 path 值"
                  clearable
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
              <el-form-item label="重定向" prop="redirect">
                <el-input
                  v-model="state.ruleForm.redirect"
                  placeholder="请输入路由重定向"
                  clearable
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
              <el-form-item label="组件路径" prop="component">
                <el-input
                  v-model="state.ruleForm.component"
                  placeholder="组件路径"
                  clearable
                ></el-input>
              </el-form-item>
            </el-col>
          </template>
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="权限标识" prop="permissionIdentifying">
              <el-input
                v-model="state.ruleForm.permissionIdentifying"
                placeholder="请输入权限标识"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
            <el-form-item label="菜单排序" prop="menuSort">
              <el-input-number
                min="0"
                precision="0"
                v-model="state.ruleForm.menuSort"
                controls-position="right"
                placeholder="请输入排序"
                class="w100"
              />
            </el-form-item>
          </el-col>
          <template v-if="state.ruleForm.menuType === 1">
            <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
              <el-form-item label="是否外链" prop="isLink">
                <el-radio-group
                  v-model="state.ruleForm.isLink"
                  :disabled="state.ruleForm.meta.isIframe"
                  @change="onSelectIsLinkChange"
                >
                  <el-radio :label="true">是</el-radio>
                  <el-radio :label="false">否</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
            <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
              <el-form-item label="是否内嵌" prop="isIframe">
                <el-radio-group
                  v-model="state.ruleForm.meta.isIframe"
                  @change="onSelectIframeChange"
                >
                  <el-radio :label="true">是</el-radio>
                  <el-radio :label="false">否</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
            <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
              <el-form-item label="链接地址" prop="link">
                <el-input
                  v-model="state.ruleForm.meta.link"
                  placeholder="外链/内嵌时链接地址（http:xxx.com）"
                  clearable
                  :disabled="!state.ruleForm.isLink"
                >
                </el-input>
              </el-form-item>
            </el-col>
            <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
              <el-form-item label="是否隐藏" prop="isHide">
                <el-radio-group v-model="state.ruleForm.meta.isHide">
                  <el-radio :label="true">隐藏</el-radio>
                  <el-radio :label="false">不隐藏</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
            <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
              <el-form-item label="页面缓存" prop="isKeepAlive">
                <el-radio-group v-model="state.ruleForm.meta.isKeepAlive">
                  <el-radio :label="true">缓存</el-radio>
                  <el-radio :label="false">不缓存</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
            <el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12">
              <el-form-item label="是否固定" prop="isAffix">
                <el-radio-group v-model="state.ruleForm.meta.isAffix">
                  <el-radio :label="true">固定</el-radio>
                  <el-radio :label="false">不固定</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
          </template>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(menuDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="systemMenuDialog">
import { defineAsyncComponent, reactive, ref, nextTick } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import { i18n } from "/@/i18n/index";
import _ from "lodash";
import { useMenuApi } from "/@/api/system/menu/index";
import { setBackEndControlRefreshRoutes } from "/@/router/backEnd";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入组件
const IconSelector = defineAsyncComponent(
  () => import("/@/components/iconSelector/index.vue")
);

// 引入菜单 Api 请求接口
const menuApi = useMenuApi();

// 定义变量内容
const menuDialogFormRef = ref();
const state = reactive({
  ruleForm: {
    pMenu: [""], // 上级菜单
    pId: null, // 上级菜单ID
    menuType: 1, // 菜单类型
    name: "", // 路由名称
    component: "", // 组件路径
    componentAlias: "", // 组件路径别名
    isLink: false, // 是否外链
    menuSort: 0, // 菜单排序
    path: "", // 路由路径
    redirect: "", // 路由重定向，有子集 children 时
    permissionIdentifying: "", // 权限标识
    meta: {
      title: "", // 菜单名称
      icon: "", // 菜单图标
      isHide: false, // 是否隐藏
      isKeepAlive: true, // 是否缓存
      isAffix: false, // 是否固定
      link: "", // 外链/内嵌时链接地址（http:xxx.com），开启外链条件，`1、isLink: 链接地址不为空`
      isIframe: false, // 是否内嵌，开启条件
    },
  },
  menuData: [] as RouteItems, // 上级菜单数据
  dialog: {
    isShowDialog: false,
    type: "",
    editId: "",
    title: "",
    submitTxt: "",
  },
});

// 表单验证规则
const formRules = reactive<FormRules>({
  title: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (!state.ruleForm.meta.title) {
          callback(new Error("请输入菜单名称！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  permissionIdentifying: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (state.ruleForm.menuType == 2 && !value) {
          callback(new Error("请输入权限标识！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  name: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (state.ruleForm.menuType == 1 && !value) {
          callback(new Error("请输入路由名称！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  path: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (state.ruleForm.menuType == 1 && !value) {
          callback(new Error("请输入路由路径！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  component: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (state.ruleForm.menuType == 1 && !value) {
          callback(new Error("请输入组件路径！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
});

// 上级菜单切换
function pMenuChange(val: []): void {
  if (!val || val.length < 1) {
    state.ruleForm.pId = null;
    return;
  }
  state.ruleForm.pId = val[val.length - 1];
}

// 获取 pinia 中的路由
const getMenuData = (routes: RouteItems) => {
  const arr: RouteItems = [];
  routes.map((val: RouteItem) => {
    val["title"] = i18n?.global.t(val.meta?.title as string);
    arr.push({ ...val });
    if (val.children) getMenuData(val.children);
  });
  return arr;
};

// 表单重置
const ruleFormReset = () => {
  state.dialog.editId = "";
  state.ruleForm = {
    pMenu: [""], // 上级菜单
    pId: null, // 上级菜单ID
    menuType: 1, // 菜单类型
    name: "", // 路由名称
    component: "", // 组件路径
    componentAlias: "", // 组件路径别名
    isLink: false, // 是否外链
    menuSort: 0, // 菜单排序
    path: "", // 路由路径
    redirect: "", // 路由重定向，有子集 children 时
    permissionIdentifying: "", // 权限标识
    meta: {
      title: "message.router.xxx", // 菜单名称
      icon: "", // 菜单图标
      isHide: false, // 是否隐藏
      isKeepAlive: true, // 是否缓存
      isAffix: false, // 是否固定
      link: "", // 外链/内嵌时链接地址（http:xxx.com），开启外链条件，`1、isLink: 链接地址不为空`
      isIframe: false, // 是否内嵌，开启条件
    },
  };
};

// 打开弹窗
const openDialog = (type: string, row?: any, pMenu: string[] = []) => {
  getMenuTreeData();
  nextTick(() => {
    ruleFormReset(); // 重置表单
    if (type === "edit") {
      Object.assign(state.ruleForm, row);
      if (state.ruleForm.pId) {
        state.ruleForm.pMenu = pMenu;
      }
      state.dialog.editId = row.id;
      state.dialog.title = "修改菜单";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增菜单";
      state.dialog.submitTxt = "新 增";
    }
    state.dialog.type = type;
    state.dialog.isShowDialog = true;
  });
};

// 关闭弹窗
const closeDialog = () => {
  state.dialog.isShowDialog = false;
};

// 是否外链下拉改变
const onSelectIsLinkChange = () => {
  if (!state.ruleForm.isLink) {
    state.ruleForm.meta.link = "";
  }
};

// 是否内嵌下拉改变
const onSelectIframeChange = () => {
  if (state.ruleForm.meta.isIframe) state.ruleForm.isLink = true;
  else state.ruleForm.isLink = false;
  onSelectIsLinkChange();
};

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
    menuApi
      .updateMenu(state.dialog.editId, state.ruleForm)
      .then((result) => {
        if (result) {
          ElMessage.success("修改成功！");
          closeDialog(); // 关闭弹窗
          emit("refresh");
          setBackEndControlRefreshRoutes(); // 刷新菜单
        }
      })
      .catch((err) => {
        console.error("提交出错：", err);
      });
    return;
  }

  // 添加
  menuApi
    .addMenu(state.ruleForm)
    .then((result) => {
      if (result) {
        ElMessage.success("添加成功！");
        closeDialog(); // 关闭弹窗
        emit("refresh");
        setBackEndControlRefreshRoutes(); // 刷新菜单
      }
    })
    .catch((err) => {
      console.error("提交出错：", err);
    });
};

// 取消
const onCancel = () => {
  closeDialog();
};

// 初始化上级菜单
const getMenuTreeData = () => {
  menuApi
    .getMenuTree()
    .then((result) => {
      state.menuData = getMenuData(result);
    })
    .catch((err) => {
      console.error("查询(选择)上级菜单出错：", err);
    });
};

// 暴露变量
defineExpose({
  openDialog,
});
</script>
