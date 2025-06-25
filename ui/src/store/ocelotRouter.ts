import { defineStore } from 'pinia'
import { layer } from "@layui/layui-vue";

import {
    get_router_info,
    get_router_list,
    update_router,
    add_router,
} from "../api/module/router";
export const useRouterStore = defineStore({
    id: 'router',
    state: () => {
        return {
            currentRouter: {} as any,
            currentRouterProj: {} as any,
            routerList: [],
            selectedProj: false,
        }
    },
    actions: {
        selectProj(item: any) {
            this.currentRouterProj = item
            this.selectedProj = true
            this.refreshRouterList();
        },
        selectRouter(item: any) {
            if (this.currentRouter.id && this.currentRouter.id == item.id) return;
            get_router_info(item.id)
                .then(({ data }) => {
                    if (data.projectId != this.currentRouterProj.id) {
                        layer.msg("路由不属于当前项目，请重新选择项目！", { icon: 5 });
                        return;
                    }
                    this.currentRouter = data
                })
                .catch((err) => {
                    this.currentRouter = { projectId: this.currentRouterProj.id }
                })
        }
        ,
        addRouter() {
            this.currentRouter = { projectId: this.currentRouterProj.id }
        },
        // 刷新路由列表
        refreshRouterList() {

            this.routerList = []
            if (this.currentRouterProj.id)
                get_router_list(this.currentRouterProj.id).then(({ data }) => {
                    console.log(data);
                    this.routerList = data;
                });
        }
        ,
        onSaveRouter() {
            if (this.currentRouter.id) {
                console.log("编辑路由信息", this.currentRouter);
                // 编辑
                update_router(this.currentRouter)
                    .then((res) => {
                        layer.msg("更新成功");
                        this.refreshRouterList(); // 刷新路由列表
                    })
                    .catch((err) => {
                        layer.msg("网络异常，请稍后再试");
                    });
            } else {
                console.log("新增路由信息", this.currentRouter);
                // 新增
                add_router(this.currentRouter)
                    .then((res) => {
                        console.log(res);
                        layer.msg("新增成功");
                        this.refreshRouterList(); // 刷新路由列表
                    })
                    .catch((err) => {
                        layer.msg("网络异常，请稍后再试");
                    });
            }
        }
    }

})