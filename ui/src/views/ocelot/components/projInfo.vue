<template>
  <lay-card class="container">
    <h3>选择配置中心</h3>
    <div class="config-contain">
      <lay-select v-model="currentConsul" placeholder="请选择">
        <lay-select-option
          v-for="item in projStore.consulList"
          :key="item"
          :value="item"
          :label="item?.name"
        ></lay-select-option>
      </lay-select>
      <lay-button type="primary" @click="onSave">保存</lay-button>
    </div>
    <br />
    <consul :data="currentConsul" :border="true"></consul>
    <!-- <div class="tag-consul">
      <lay-tag type="normal">Host:{{ currentConsul?.host }}</lay-tag>
      <lay-tag type="normal">Port {{ currentConsul?.port }}</lay-tag>
      <lay-tag type="normal">DC:{{ currentConsul?.key }}</lay-tag>
      <lay-tag type="normal">Path:{{ currentConsul?.path }}</lay-tag>
    </div> -->
  </lay-card>
</template>
<script lang="ts" setup>
import { reactive, ref, watch, computed, onBeforeMount } from "vue";
import { layer } from "@layui/layui-vue";
import { useProjStore } from "../../../store/proj";
import consul from "../../component/consulDescription.vue";
const projStore = useProjStore();

const currentConsul: any = ref();
onBeforeMount(() => {
  projStore.refreshConsulList((data) => {
    currentConsul.value = data.find(
      (o: any) => o.id == projStore.currentProj.configCenter?.id
    );
  });
});
const onSave = () => {
  //   console.log(currentConsul.value);
  projStore.saveProjConfig(currentConsul.value?.id);
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
</style>
