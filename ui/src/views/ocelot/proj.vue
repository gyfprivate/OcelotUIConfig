<template>
  <div class="flex h-screen">
    <!-- 左侧列表区域 -->
    <ProjList />
    <!-- 右侧内容区域 -->
    <div v-if="projStore.currentProj.id" class="left-container">
      <Info />
      <br style="margin: 0;w" />
      <RouteList />
    </div>
    <div v-else class="flex items-center">
      <h2>请选择左侧列表项查看详情</h2>
    </div>
  </div>
</template>
<script lang="ts" setup>
import { ref, reactive, onMounted, computed } from "vue";
import ProjList from "./components/projList.vue";
import Info from "./components/projInfo.vue";
import RouteList from "./components/projRoute.vue";
import { useProjStore } from "../../store/proj";
const projStore = useProjStore();
</script>
<style scoped>
.items-center {
  padding: 5px;
}
.left-container {
  padding: 5px;
  display: flex;
  flex-direction: column;
  width: 100%;
}
/* 移除Tailwind CSS依赖的样式，使用纯CSS实现 */
.flex {
  display: flex;
}

.h-screen {
  min-height: calc(100% - 46px);
  margin: 10px;
}

.w-24 {
  width: 200px;
  margin: 5px;
}

.bg-gray-800 {
  background-color: #1f2937;
}

.text-white {
  color: white;
}

.flex-col {
  flex-direction: column;
}

.p-2 {
  padding: 0.5rem;
}

.space-x-1 > * + * {
  margin-left: 0.25rem;
}

.overflow-y-auto {
  overflow-y: auto;
}

.flex-1 {
  flex: 1;
}

.shadow-hover {
  box-shadow: 0 0 11px rgba(33, 33, 33, 0.2);
}

.hover:shadow-lg:hover {
  box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.1),
    0 4px 6px -2px rgba(0, 0, 0, 0.05);
}

.p-4 {
  padding: 5px;
}

.space-y-4 > * + * {
  margin-top: 1rem;
}

.text-gray-500 {
  color: #6b7280;
}
</style>
