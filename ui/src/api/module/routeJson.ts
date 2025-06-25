import Http from '../http';

/**
 * 获取
 */
export const get_json = function (projId: string) {
    return Http.get(`/api/RouteJson/${projId}`)
}


export const sync_routes = function (projId: string) {
    return Http.post(`/api/RouteJson/${projId}`)
}

export const rollback_config = function (bakId: string) {
    return Http.put(`/api/RouteJson/${bakId}`)
}
