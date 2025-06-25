import Http from '../http';
export const get_global_configuration = function () {
    return Http.get('/api/GlobalConfig')
}


/**
 * 
 * @param projId 0 表示全局
 * @param data 
 * @returns 
 */
export const modify_global_configuration = function (projId: string, data: any) {
    return Http.post(`/api/GlobalConfig/proj/${projId}`, data)
}


export const edit_asyn = function (id: string, data: any) {
    return Http.put(`/api/SynchronizationProject/${id}`, data)
}



