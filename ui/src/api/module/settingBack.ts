

import Http from '../http';

export const get_setting_back = function (arg: any) {
    return Http.get('/api/SettingBackup', arg)
}
