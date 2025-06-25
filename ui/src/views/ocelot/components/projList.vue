<template>
  <!-- 左侧列表区域 -->
  <lay-card class="w-240 flex flex-col">
    <h4>同步项目列表</h4>
    <div class="p-2 flex space-x-1">
      <lay-button
        v-if="!justView"
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
        @click="projStore.refreshProjList()"
      >
        刷新
      </lay-button>
    </div>
    <div class="overflow-y-auto flex-1">
      <ul>
        <li
          v-for="item in projStore.projList"
          :key="item.id"
          class="p-2 cursor-pointer border-b border-gray-200"
          :class="
            projStore.currentProj.id === item.id ? 'bg-blue-100' : 'bg-gray-100'
          "
          @click="projStore.selectProj(item)"
        >
          <div class="flex items-center justify-between">
            {{ item.projectName }}
            <lay-button
              v-if="!justView"
              type="normal"
              size="xs"
              @click="edit(item)"
              >编辑</lay-button
            >
          </div>
        </li>
        <!-- <li
          v-for="item in items"
          :key="item.id"
          :class="
            selectedItemId === item.id ? 'bg-blue-100' : 'hover:bg-gray-100'
          "
          class="p-2 cursor-pointer border-b border-gray-200"
          @click="selectItem(item.id)"
        >
          {{ item.name }}
        </li> -->
      </ul>
    </div>
    <projedit ref="editRef" />
  </lay-card>
</template>
<script lang="ts" setup>
import { ref, reactive, onMounted, computed } from "vue";
import { get_list_asyn } from "../../../api/module/proj";
import projedit from "./components/edit.vue";
import { formatCurrentTime, formatTimeNative } from "../../../api/timeutil";
import { useProjStore } from "../../../store/proj";

// 定义 props 类型
const props = withDefaults(
  defineProps<{
    justView: boolean;
  }>(),
  { justView: false }
);

const projStore = useProjStore();

const editRef: any = ref(null);

onMounted(() => {
  projStore.refreshProjList();
});

const edit = (data: any) => {
  data.createTime = formatTimeNative(data.createTime);
  data.updateTime = formatTimeNative(data.updateTime);
  editRef.value?.show({
    title: "编辑同步项目",
    data: data,
    type: 1,
  });
};
// 增加项目
const addItem = () => {
  const timestr = formatCurrentTime();
  editRef.value?.show({
    title: "新增同步项目",
    data: {
      id: "",
      projectName: "",
      orderIndex: 100,
      enabled: true,
      createTime: timestr,
      updateTime: timestr,
      type: 1,
    },
    type: 0,
  });
};
</script>
<style scoped>
/* 移除Tailwind CSS依赖的样式，使用纯CSS实现 */
.flex {
  display: flex;
}

.w-240 {
  width: 200px;
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
