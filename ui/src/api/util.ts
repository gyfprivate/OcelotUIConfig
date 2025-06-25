export const stringToArray = (str: string) => {
    if (!str) return [];
    return JSON.parse(str.replace(/'/g, '"'));
};

export const arrayToString = (arr: any) => {
    if (!Array.isArray(arr)) return '[]';
    return JSON.stringify(arr).replace(/"/g, "'");
};


export const convertSwitchValue = (value: string): null | boolean => {
    switch (value) {
        case '1':
            return true;
        case '2':
            return false;
        case '0':
            return null;
        default:
            return null;
    }
};

export const reConvertSwitchValue = (value: null | boolean): string => {

    // console.log(value)
    if (value === undefined || value === null) return '0';

    switch (value) {
        case true:
            return '1';
        case false:
            return '2';
        default:
            return '0';
    }
};