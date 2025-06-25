import { layer } from "@layui/layui-vue";
import { computed, ComputedRef, ref, watch } from "vue";
import { useRoute, useRouter } from "vue-router";
import { diff } from "../../library/arrayUtil";
import { getParents, getNode } from "../../library/treeUtil";
import { useAppStore } from "../../store/app";
import { useUserStore } from "../../store/user";

export function useMenu() {

  const route = useRoute();
  const router = useRouter();
  const userStore = useUserStore();
  const appStore = useAppStore();
  const selectedKey = ref(route.path);
  const openKeys = ref<string[]>([]);
  const isAccordion = computed(() => appStore.accordion);
  const isSubfield = computed(() => appStore.subfield);
  const mainSelectedKey = ref("/workspace");

  const menus = computed(() => {
    if (isSubfield.value) {
      const node = getNode(userStore.menus, mainSelectedKey.value);
      if (node) {
        return node.children;
      } else {
        return [];
      }
    } else {
      return userStore.menus;
    }
  });

  const mainMenus: ComputedRef<any[]> = computed(() => {
    if (isSubfield.value) {
      return userStore.menus;
    } else {
      return [];
    }
  });

  watch(
    route,
    () => {
      selectedKey.value = route.path;
      const andParents = getParents(menus.value, route.path);
      if (andParents && andParents.length > 0) {
        let andParentKeys = andParents.map((item: any) => item.id);
        if (isAccordion.value) {
          openKeys.value = [...andParentKeys];
        } else {
          openKeys.value = [...andParentKeys, ...openKeys.value];
        }
      }
    },
    { immediate: true }
  );
  watch(
    () => userStore.menus, // 监听菜单数据的变化
    (newMenus) => {
      if (newMenus && newMenus.length > 0) {
        // 菜单数据加载完成后，设置openKeys为所有菜单项的键
        openKeys.value = getAllMenuKeys(newMenus);
        // console.log(openKeys.value)
      }
    },
    { immediate: true } // 确保在组件挂载时立即执行
  );
  function getAllMenuKeys(menus: any[]): string[] {
    let keys: string[] = [];

    function traverse(menu: any) {
      keys.push(menu.id);
      if (menu.children && menu.children.length > 0) {
        menu.children.forEach(child => traverse(child));
      }
    }

    menus.forEach(menu => traverse(menu));
    return keys;
  }
  const to = (id: string) => {
    router.push(id);
  };

  function changeSelectedKey(key: string) {
    var node = getNode(userStore.menus, key);

    if (node.type && node.type == "modal") {
      layer.open({
        type: "iframe",
        content: node.id,
        area: ["80%", "80%"],
        maxmin: true,
      });
      return;
    }

    if (node.type && node.type == "blank") {
      window.open(node.id, "_blank");
      return;
    }

    to(key);
  }

  function changeOpenKeys(keys: string[]) {
    console.log("changeOpenKeys", keys)
    const addArr = diff(openKeys.value, keys);
    if (keys.length > openKeys.value.length && isAccordion.value) {
      var arr = getParents(menus.value, addArr[0]);
      if (arr && arr.length > 0) {
        openKeys.value = arr.map((item: any) => {
          return item.id;
        });
      }
    } else {
      openKeys.value = keys;
    }
  }

  function changeMainSelectedKey(key: string) {

    var node = getNode(userStore.menus, key);

    if (node.type && node.type == "modal") {
      layer.open({
        type: "iframe",
        content: node.id,
        area: ["80%", "80%"],
        maxmin: true,
      });
      return;
    }

    if (node.type && node.type == "blank") {
      window.open(node.id, "_blank");
      return;
    }

    mainSelectedKey.value = key;
  }

  return {
    selectedKey,
    openKeys,
    changeOpenKeys,
    changeSelectedKey,
    isAccordion,
    menus,
    mainMenus,
    mainSelectedKey,
    changeMainSelectedKey
  };
}
