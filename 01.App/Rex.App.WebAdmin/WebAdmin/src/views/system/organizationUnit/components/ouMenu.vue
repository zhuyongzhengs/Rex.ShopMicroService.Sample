<template>
  <div
    v-show="visible"
    :style="{
      left: position.left + 'px',
      top: position.top + 'px',
      display: visible ? 'block' : 'none',
    }"
    class="context-menu"
  >
    <div
      v-for="(item, i) in menuItems"
      :key="i"
      class="menu-item"
      @click="item.action(rightClickItem)"
    >
      <el-icon size="14" class="item-icon" v-if="item.icon === 'add'">
        <ele-Plus />
      </el-icon>

      <el-icon size="14" class="item-icon" v-else-if="item.icon === 'edit'">
        <ele-Edit />
      </el-icon>

      <el-icon size="14" class="item-icon" v-else-if="item.icon === 'delete'">
        <ele-Delete />
      </el-icon>

      <el-icon size="14" class="item-icon" v-else>
        <ele-Link />
      </el-icon>
      <span class="item-name"> {{ item.name }} </span>
    </div>
  </div>
</template>

<script setup lang="ts" name="ouMenu">
import { ref, watch } from 'vue';
    
interface Props {
  menuItems: OuMenuItem[];
}

export interface OuMenuItem {
  name: string;
  icon?: string;
  action: (rightClickItem: any) => void;
}

const props = defineProps<Props>();
const visible = ref(false);
const rightClickItem = ref(null);
const position = ref({
  top: 0,
  left: 0,
});

const openMenu = (e: MouseEvent, item: any) => {
  visible.value = true;
  let xVal = 220;
  let layoutAsidePc220 = document.getElementsByClassName('layout-aside-pc-220');
  if(layoutAsidePc220.length < 1) {
    xVal = 64;
  }
  let calcTop = (e.pageY - 65) < 1 ? 0 : (e.pageY - 65);
  let calcLeft = (e.pageX - xVal) < 1 ? 0 : (e.pageX - xVal);
  position.value.top = calcTop;
  position.value.left = calcLeft;
  rightClickItem.value = item;
};

const closeMenu = () => {
  visible.value = false;
};

watch(visible, () => {
  if (visible.value) {
    document.body.addEventListener("click", closeMenu);
  } else {
    document.body.removeEventListener("click", closeMenu);
  }
});

defineExpose({ 
  openMenu,
  closeMenu
});
</script>
<style scoped lang="scss">
  .context-menu {
    margin: 0px;
    background: #fff;
    z-index: 2;
    position: absolute;
    list-style-type: none;
    border-radius: 4px;
    font-size: 12px;
    font-weight: 400;
    color: #333;
    box-shadow: 2px 2px 2px 2px rgba(0, 0, 0, 0.3);
   
    .menu-item {
      padding: 0px 15px;
      height: 40px;
      line-height: 40px;
      color: rgb(29, 33, 41);
      cursor: pointer;
      font-size: 14px;
      border-bottom: 1px #e4e7ed dotted;
      .item-icon {
        position: relative;
        top: 2px;
      }
      .item-name {
        display: inline-block;
        margin-left: 8px;
      }
    }
    .menu-item:hover {
      background: var(--el-color-primary-light-9);
      border-radius: 4px;
    }
  }
  </style>