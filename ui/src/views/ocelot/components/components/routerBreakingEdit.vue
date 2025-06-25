<template>
  <lay-form :size="'md'" :labelWidth="125" isLabelTooltip>
    <lay-row>
      <lay-col :md="12" :sm="24">
        <lay-form-item label="允许的例外数量">
          <lay-input-number
            v-model="
              routeStore.currentRouter.qosExceptionsallowedbeforebreaking
            "
            type="text"
            placeholder="打开断路器之前允许的例外数量"
          ></lay-input-number>
        </lay-form-item>
      </lay-col>
      <lay-col :md="12" :sm="24">
        <lay-form-item label="恢复时间间隔">
          <lay-input
            v-model="
              routeStore.currentRouter.qosExceptionsallowedbeforebreaking
            "
            type="text"
            placeholder="打开断路器之前允许的例外数量"
          >
            <template #suffix>毫秒</template></lay-input
          >
        </lay-form-item>
      </lay-col>

      <lay-col :md="12" :sm="24">
        <lay-form-item label="请求超时时间">
          <lay-input
            v-model="routeStore.currentRouter.qosTimeoutvalue"
            type="text"
            placeholder="请求超时时间"
          >
            <template #suffix>毫秒</template></lay-input
          >
        </lay-form-item>
      </lay-col>
    </lay-row>
  </lay-form>
</template>
<script lang="ts" setup>
import { ref, reactive } from "vue";
import { useRouterStore } from "../../../../store/ocelotRouter";
const routeStore = useRouterStore();
</script>
<style scoped></style>
