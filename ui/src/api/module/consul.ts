import Http from '../http';
export const get_consul_list = function () {
    return Http.get('/api/configcenter')
}

export const add_consul = function (data: any) {
    return Http.post('/api/configcenter', data)
}


export const edit_consul = function (data: any) {
    return Http.put(`/api/configcenter/${data.id}`, data)
}
