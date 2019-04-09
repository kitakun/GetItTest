import { IErrorRecordPreviewModel } from "../../models/error-preview/IErrorRecordPreview";

import { Action } from 'redux';

export const StartPreviewLoading = '@@errorPreview/START_LOADING';
export const FinishPreviewLoading = '@@errorPreview/RESPONSE_LOADING';

export interface ErrorsPreviewState {
    lastLoadedData?: Array<IErrorRecordPreviewModel>;
    lastUsedFilter?: any;
    inLoading: boolean;
}

export interface StartPreviewLoadingAction extends Action {
    type: '@@errorPreview/START_LOADING', //StartPreviewLoading;
    filter?: any;
}

export interface FinishPreviewLoadingAction extends Action {
    type: '@@errorPreview/RESPONSE_LOADING', //FinishPreviewLoading;
    data?: Array<IErrorRecordPreviewModel>;
}

export type ErrorsPreviewListActions = StartPreviewLoadingAction | FinishPreviewLoadingAction;