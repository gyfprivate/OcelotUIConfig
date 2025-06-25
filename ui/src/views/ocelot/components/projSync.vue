<template>
  <lay-card class="container">
    <lay-step :active="active" center>
      <lay-step-item title="配置中心"> </lay-step-item>
      <lay-step-item title="配置预览"></lay-step-item>
      <lay-step-item title="同步"></lay-step-item>
    </lay-step>
    <lay-button @click="previous">上一步</lay-button>
    <lay-button @click="next">下一步</lay-button>
    <lay-line theme="red"></lay-line>

    <!-- 配置列表 -->
    <consul :data="currentConsul" :border="true" title="配置中心" v-if="active == 0">      </consul>


    <!-- 路由预览 -->
    <div v-if="active == 1">
      <lay-button @click="refreshJson">刷新</lay-button>
      <!-- {{ jsonData }} -->
      <VueJsonPretty
        :data="jsonData"
        :collapsedOnClickBrackets="true"
        :showSelectController="true"
      ></VueJsonPretty>
    </div>
    <!-- 运行同步 -->
    <div v-if="active == 2">
      <lay-button
        border="blue"
        prefix-icon="layui-icon-refresh-one"
        class="sync-btn"
        @click="onSync"
        >同步</lay-button
      >

      <!-- 配置列表 -->
      <consul :data="currentConsul" :border="true">      </consul>
 
    </div>
  </lay-card>
</template>
<script lang="ts" setup>
import { reactive, ref, watch, computed, onBeforeMount } from "vue";
import { layer } from "@layui/layui-vue";
import { useProjStore } from "../../../store/proj";
import { get_json, sync_routes } from "../../../api/module/routeJson";
import consul from "../../component/consulDescription.vue"
// https://github.com/leezng/vue-json-pretty/blob/e74d358bb6953dd0e4c581ca6b3550705008feb4/README.zh_CN.md
import VueJsonPretty from "vue-json-pretty";
import "vue-json-pretty/lib/styles.css";

const projStore = useProjStore();

const currentConsul = computed(() =>
  projStore.consulList.find(
    (o: any) => o.id == projStore.currentProj.configCenter?.id
  )
);

const active = ref(0);
const jsonData = ref();
const next = () => {
  if (active.value++ >= 2) active.value = 2;
};
const previous = () => {
  if (active.value-- === 0) active.value = 0;
};

onBeforeMount(() => {
  projStore.refreshConsulList();
  refreshJson();
});

const refreshJson = () => {
  get_json(projStore.currentProj.id)
    .then(({ data }) => {
      //   jsonData.value = data;
      jsonData.value = JSON.parse(data);
      //   console.log(jsonData.value);
    })
    .catch((err) => {});
};
const onSave = () => {
  //   console.log(currentConsul.value);
  projStore.saveProjConfig(currentConsul.value.id);
};

// 同步
const onSync = () => {
  sync_routes(projStore.currentProj.id)
    .then((res) => {
      layer.msg(res.data);
    })
    .catch((err) => {
      layer.msg(err);
    });
};
</script>
<style scoped>
.container {
  margin: 0px;
  width: 100%;
}
.tag-consul span {
  margin-right: 10px;
}
.config-contain {
  display: flex;
  gap: 10px;
  margin-top: 10px;
}
.sync-btn {
  margin-bottom: 10px;
}
</style>
