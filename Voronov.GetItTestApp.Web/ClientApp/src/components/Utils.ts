import { globalStore } from "..";

export const enumFormatter = function <T>(val: T, enumObject: any): string {
    return enumObject[val];
}

export const getHeaders = function () {
    const authState = globalStore.getState().auth;
    if (authState.token && authState.token.length > 0) {
        return {
            'Authorization': `Bearer ${authState.token}`
        };
    }
    return {};
}