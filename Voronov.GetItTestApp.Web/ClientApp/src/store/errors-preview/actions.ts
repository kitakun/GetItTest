import { ActionCreator } from 'redux';
import { ThunkDispatch, ThunkAction } from 'redux-thunk';
import axios from 'axios';

import { StartPreviewLoadingAction, StartPreviewLoading, ErrorsPreviewState, FinishPreviewLoading, FinishPreviewLoadingAction } from './types';
import { IResponse, IErrorRecordPreviewModel } from '../../models/index';
import { getHeaders } from '../../components/Utils';


export const startPreviewLoadingAction: ActionCreator<StartPreviewLoadingAction> = (filter?: any) =>
    ({
        type: StartPreviewLoading,
        filter: filter
    });

export const lunchAsyncPreviewLoadingAction = (filter: any): ThunkAction<Promise<void>, any, any, any> =>
    (dispatch: ThunkDispatch<ErrorsPreviewState, any, any>, getState: () => ErrorsPreviewState, extraArgs: any): Promise<void> => {
        dispatch(startPreviewLoadingAction(filter));

        return axios
            .post('/api/errorrecords/previewlist', null, { headers: getHeaders() })
            .then((loadedDataResponse: IResponse<Array<IErrorRecordPreviewModel>>) => {
                let collection = loadedDataResponse.data;
                dispatch(finishPreviewLoadingAction(collection));
            }).catch(() => {
                dispatch(finishPreviewLoadingAction(void 0));
            });
    };

export const finishPreviewLoadingAction: ActionCreator<FinishPreviewLoadingAction> = (recievedArray: Array<IErrorRecordPreviewModel>) =>
    ({
        type: FinishPreviewLoading,
        data: recievedArray
    });