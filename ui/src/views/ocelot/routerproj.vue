<template>
  <div class="flex h-screen">
    <!-- 左侧列表区域 -->
    <LeftMenu />
    <!-- 右侧内容区域 -->
    <div class="flex-1 overflow-auto">
      <div v-if="routerStore.selectedProj" class="flex h-screen2">
        <!-- 显示路由列表 -->
        <routerList :title="routerTitle" :ref="routerRef" />

        <!-- 显示 选中项的详细信息 -->
        <routerInfo v-if="routerStore.currentRouterProj" />
        <div v-else class="empty-content2">
          <lay-card class="flex empty-content">
            <h2>请选择左侧路由查看详情</h2>
          </lay-card>
        </div>
      </div>
      <div v-else class="empty-content">
          <h2>请选择左侧路由项目查看详情</h2>
      </div>
    </div>
  </div>
</template>
<script lang="ts" setup>
import { ref, reactive, onMounted, computed } from "vue";
import LeftMenu from "./components/routerProj.vue";
import routerList from "./components/routerList.vue";
import routerInfo from "./components/routerInfo.vue";
import { get_info_router } from "../../api/module/proj";
import { useRouterStore } from "../../store/ocelotRouter.ts";

const selectedProj: any = ref(null);
const selectedRouter: any = ref(null);
const routerTitle = ref("");
const routerRef: any = ref(null);

//路由信息
const routerStore = useRouterStore();


</script>
<style scoped>
/* 移除Tailwind CSS依赖的样式，使用纯CSS实现 */
.flex {
  display: flex;
}

.h-screen {
  min-height: calc(100% - 46px);
  margin: 10px;
}
.h-screen2 {
  min-height: 100%;
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

.space-y-4 > * + * {
  margin-top: 1rem;
}

.text-gray-500 {
  color: #6b7280;
}

.empty-content {
  margin: 5px;
  height: 100%;
  padding-bottom: 10px;
}
.empty-content2 {
  margin-bottom: 10px;
  width: 100%;
}
</style>
