<template>
  <!-- 配置列表 -->
  <lay-descriptions style="" :column="4" :border="border" :title="title">
    <lay-descriptions-item label="Path"
      ><lay-tag type="normal">{{ data?.path }}</lay-tag></lay-descriptions-item
    >
    <lay-descriptions-item label="Host">
      <lay-tag type="normal">{{ data?.host }}</lay-tag>
    </lay-descriptions-item>
    <lay-descriptions-item label="Port">
      <lay-tag type="normal"> {{ data?.port }}</lay-tag>
    </lay-descriptions-item>
    <lay-descriptions-item label="DC">
      <lay-tag type="normal">{{ data?.key }}</lay-tag></lay-descriptions-item
    >
  </lay-descriptions>
</template>
<script lang="ts" setup>
import { ref } from "vue";

interface Props {
  title: string;
  data?: any;
  border?: boolean;
}

const props = withDefaults(defineProps<Props>(), {
  title: "",
  data: {},
  border: false,
});
</script>
