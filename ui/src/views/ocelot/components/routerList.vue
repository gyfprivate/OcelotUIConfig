<template>
  <!-- 左侧列表区域 -->
  <lay-card class="w-240 flex flex-col">
    <h4>{{ `[${routerStore.currentRouterProj.projectName}] 路由列表` }}</h4>
    <div class="p-2 flex space-x-1">
      <lay-button
        prefix-icon="layui-icon-addition"
        type="primary"
        size="xs"
        @click="addItem"
      >
        增加
      </lay-button>
      <lay-button
        prefix-icon="layui-icon-refresh"
        size="xs"
        @click="routerStore.refreshRouterList()"
      >
        刷新
      </lay-button>
    </div>
    <div class="overflow-y-auto flex-1">
      <ul>
        <li
          v-for="item in routerStore.routerList"
          :key="item.id"
          class="p-2 cursor-pointer border-b border-gray-200"
          :class="
            routerStore.currentRouter.id === item.id
              ? 'bg-blue-100'
              : 'bg-gray-100'
          "
          @click="routerStore.selectRouter(item)"
        >
          <div>
            <div class="flex items-center justify-between">
              {{ item.upstreamPathTemplate }}
              <lay-switch
                v-model="item.enabled"
                onswitch-text="启用"
                unswitch-text="禁用"
                @change="changeEnable(item.id, $event)"
              ></lay-switch>
            </div>
            <div class="flex items-center justify-between">
              <span> -> </span>
              {{ item.downstreamPathTemplate }}
            </div>
          </div>
        </li>
      </ul>
    </div>
    <projedit ref="editRef" @refresh="refreshProj" />
  </lay-card>
</template>
<script lang="ts" setup>
import { ref, reactive, onMounted, computed, watch } from "vue";
import { formatCurrentTime, formatTimeNative } from "../../../api/timeutil";
import { useRouterStore } from "../../../store/ocelotRouter";
import { update_router_enabled } from "../../../api/module/router";
import router from "@/router";

const routerStore = useRouterStore();

const changeEnable = (id: string, enabled: boolean) => {
  // console.log(enabled);
  update_router_enabled(id, enabled)
    .then((res) => {})
    .catch((err) => {});
};

onMounted(() => {
  routerStore.refreshRouterList();
});

// 增加项目
const addItem = () => {
  routerStore.addRouter();
};
</script>
<style scoped>
.text2 {
  text-align: right;
}
/* 移除Tailwind CSS依赖的样式，使用纯CSS实现 */
.flex {
  display: flex;
}

.w-240 {
  width: 260px;
  margin: 5px;
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

.cursor-pointer {
  cursor: pointer;
}

.border-gray-200 {
  border-color: #e5e7eb;
}
.border-b {
  border-bottom-width: 1px;
  transition: background-color 0.3s ease;
  border-bottom: 1px solid #eee;
}
.bg-gray-100:hover {
  background-color: #f8fafc;
}

.bg-blue-100 {
  background-color: #e6f7f3;
}

.flex-1 {
  flex: 1;
}
.items-center {
  align-items: center;
}
.justify-between {
  justify-content: space-between;
}
</style>
