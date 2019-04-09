import { applyMiddleware, Store, createStore } from 'redux';
import { composeWithDevTools } from 'redux-devtools-extension';
import { History } from 'history';

import thunk from 'redux-thunk';
import { routerMiddleware } from 'react-router-redux';

import { ApplicationState, reducers } from './index';

export default function configureStore(history: History, initialState: ApplicationState): Store<ApplicationState> {

  const composeEnhancers = composeWithDevTools({});

  const middleware = [
    thunk,
    routerMiddleware(history)
  ];

  // We'll create our store with the combined reducers and the initial Redux state that
  // we'll be passing from our entry point.
  const store = createStore<ApplicationState, any, any, any>(
    reducers,
    initialState,
    composeEnhancers(applyMiddleware(...middleware)),
  );

  return store;
}