import { Reducer } from 'redux';

import { ErrorsPreviewState, StartPreviewLoading, StartPreviewLoadingAction, FinishPreviewLoading, FinishPreviewLoadingAction } from "./types";

export const initialState: ErrorsPreviewState = {
    lastLoadedData: void 0,
    lastUsedFilter: null,
    inLoading: false
};

const errorsPreviewListReducer: Reducer<ErrorsPreviewState> = (state: ErrorsPreviewState = initialState, action) => {
    switch (action.type) {

        case StartPreviewLoading:
            let spla = action as StartPreviewLoadingAction;
            return { ...state, lastUsedFilter: spla.filter, inLoading: true };

        case FinishPreviewLoading:
            let fpla = action as FinishPreviewLoadingAction;
            return { ...state, lastLoadedData: fpla.data, inLoading: false };

        default:
            return state;
    }
};

export default errorsPreviewListReducer;