<template>
  <lay-menu
    :tree="true"
    :collapse="collapse"
    :level="appStore.level"
    :inverted="appStore.inverted"
    :theme="appStore.sideTheme"
    :openKeys="openKeys"
    :selectedKey="selectedKey"
    @changeOpenKeys="changeOpenKeys"
    @changeSelectedKey="changeSelectedKey"
  >
    <GlobalMenuItem :menus="menus" :teleportProps="{ disabled: false }"></GlobalMenuItem>
  </lay-menu>
</template>

<script lang="ts">
export default {
  name: "GlobalMenu",
};
</script>

<script lang="ts" setup>
import { useAppStore } from "../../store/app";
import GlobalMenuItem from "./GlobalMenuItem.vue";

const appStore = useAppStore();

interface MenuProps {
  collapse: boolean;
  selectedKey: string;
  openKeys: string[];
  menus: any[];
}

const props = withDefaults(defineProps<MenuProps>(), {
  collapse: false,
});

const emits = defineEmits(["changeOpenKeys", "changeSelectedKey"]);

const changeOpenKeys = (keys: string[]) => {
  emits("changeOpenKeys", keys);
};

const changeSelectedKey = (key: string) => {
  emits("changeSelectedKey", key);
};
</script>

<style>
.layui-nav-tree * {
  font-size: 14px;
}

.layui-nav-tree .layui-nav-item > a,
.layui-nav-tree.inverted .layui-nav-item > a {
  padding: 3px 22px;
}

.layui-nav-tree.inverted .layui-this > a {
  padding: 3px 16px;
}

.layui-nav-tree .layui-nav-item > a > span {
  padding-left: 10px;
}

.layui-nav-tree .layui-nav-item > a .layui-nav-more {
  font-size: 12px !important;
  padding: 3px 0px;
}

/* 增加颜色区别 */
/* .layui-nav-child .layui-nav-item:not(.layui-this) {
  background-color: rgb(0, 0, 0);
} */
.layui-nav-tree > div > .layui-nav-item {
  background-color: #cccccc3b;
}
.layui-nav-tree .layui-nav-child {
  background-color: rgb(0, 0, 0);
}
.layui-nav-tree .layui-nav-child {
  padding-bottom: 0;
}
</style>
