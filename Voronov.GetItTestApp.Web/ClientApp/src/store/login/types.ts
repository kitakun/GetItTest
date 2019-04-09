import { Action } from 'redux';

import { IAuthModel } from '../../models/auth/IAuthModel';

export const AuthLoginActionName = '@@auth/AUTH_START_LOGIN';
export const AuthLoginResponseActionName = '@@auth/AUTH_RESPONSE_LOGIN';
export const AuthLogoutActionName = '@@auth/AUTH_LOGOUT';

export const UserUpdateNames = '@@user/UPDATE_INFO';

export interface AuthState {
    accountname: string;
    username: string;
    token?: string;
    inLoading: boolean;
    userId: string;
}

export interface AuthLoginAction extends Action {
    type: '@@auth/AUTH_START_LOGIN', //AuthLoginActionName;
    data: IAuthModel;
}

export interface AuthLoginResponseAction extends Action {
    type: '@@auth/AUTH_RESPONSE_LOGIN', //AuthLoginResponseActionName;
    token: string;
    username: string;
    userId: string;
}

export interface UpdateUserInfoAction extends Action {
    type: '@@user/UPDATE_INFO', //UserUpdateNames;
    firstName: string;
    lastName: string;
}

export interface AuthLogoutAction extends Action {
    type: '@@auth/AUTH_LOGOUT', //AuthLogoutActionName;
}

export type AuthActions = AuthLoginAction | AuthLoginResponseAction | UpdateUserInfoAction | AuthLogoutAction;