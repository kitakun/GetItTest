import { ActionCreator } from 'redux';
import { ThunkDispatch, ThunkAction } from 'redux-thunk';
import axios from 'axios';

import { AuthLoginAction, AuthLoginActionName, AuthState, AuthLoginResponseActionName, AuthLoginResponseAction, UpdateUserInfoAction, UserUpdateNames, AuthLogoutActionName, AuthLogoutAction } from './types';
import { IAuthModel } from '../../models/auth/IAuthModel';
import * as models from '../../models/index';
import { getHeaders } from '../../components/Utils';

export const startAuthLoginAction: ActionCreator<AuthLoginAction> = (data: IAuthModel) =>
    ({
        type: AuthLoginActionName,
        data: data
    });

export const createAsyncAuthLoginAction = (request: IAuthModel): ThunkAction<Promise<void>, any, any, any> =>
    (dispatch: ThunkDispatch<AuthState, IAuthModel, any>, getState: () => AuthState, extraArgs: any): Promise<void> => {
        dispatch(startAuthLoginAction(request));

        return axios
            .post('/api/auth/token', request, { headers: getHeaders() })
            .then((tokenResponse: models.IResponse<models.IAuthResponse>) => {
                dispatch(finishAuthLoginAction(
                    tokenResponse.data.access_token,
                    tokenResponse.data.username,
                    tokenResponse.data.id));
            }).catch(() => {
                dispatch(finishAuthLoginAction(void 0));
            });
    };

export const finishAuthLoginAction: ActionCreator<AuthLoginResponseAction> = (token: string, username: string, id: string) =>
    ({
        type: AuthLoginResponseActionName,
        token: token,
        username: username,
        userId: id
    });

export const authLogoutAction: ActionCreator<AuthLogoutAction> = () =>
    ({
        type: AuthLogoutActionName,
    });

export const updateUserInfoAction: ActionCreator<UpdateUserInfoAction> = (fName: string, lName: string) =>
    ({
        type: UserUpdateNames,
        firstName: fName,
        lastName: fName
    });