import { Reducer } from 'redux';
import {
    AuthLoginActionName,
    AuthState,
    AuthLoginResponseActionName,
    AuthLoginAction,
    AuthLoginResponseAction,
    UpdateUserInfoAction,
    UserUpdateNames,
    AuthLogoutActionName
} from './types';

export const initialState: AuthState = {
    accountname: '',
    username: '',
    token: void 0,
    inLoading: false,
    userId: ''
};

const authReducer: Reducer<AuthState> = (state: AuthState = initialState, action) => {
    switch (action.type) {

        case AuthLoginActionName:
            let alan = action as AuthLoginAction;
            return {
                ...state,
                accountname: alan.data.login,
                inLoading: true
            };

        case AuthLoginResponseActionName:
            let alra = action as AuthLoginResponseAction;
            return {
                ...state,
                token: alra.token,
                username: alra.username,
                userId: alra.userId,
                inLoading: false
            };

        case AuthLogoutActionName:
            return {
                ...state,
                username: '',
                accountname: '',
                token: null,
                userId: ''
            };

        case UserUpdateNames:
            let uun = action as UpdateUserInfoAction;
            return {
                ...state,
                username: `${uun.firstName} ${uun.lastName}`
            };

        default:
            return state;
    }
};

export default authReducer;