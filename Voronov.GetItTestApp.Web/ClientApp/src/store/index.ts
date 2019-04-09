import { combineReducers, Reducer } from 'redux';
import { routerReducer, RouterState } from 'react-router-redux';

// Import your state types and reducers here.
import { AuthState } from './login/types';
import authReducer from './login/reducer';
import errorsPreviewListReducer from './errors-preview/reducer';
import { ErrorsPreviewState } from './errors-preview/types';

// The top-level state object
export interface ApplicationState {
    router: RouterState;
    auth: AuthState;
    errorsPreviewList: ErrorsPreviewState;
}

export const reducers: Reducer<ApplicationState> = combineReducers<ApplicationState>({
    router: routerReducer,
    auth: authReducer,
    errorsPreviewList: errorsPreviewListReducer
});