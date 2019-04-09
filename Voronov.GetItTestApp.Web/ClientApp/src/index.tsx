import 'babel-polyfill';
import React from 'react';
import ReactDOM from 'react-dom';

import * as serviceWorker from './serviceWorker';
import { createBrowserHistory } from 'history';
import { Router } from 'react-router';
import { Provider } from 'react-redux';

import App from './App';
import configureStore from './store/ConfigureStore';

import './index.css';

// Create browser history to use in the Redux store
const baseUrl: string = document.getElementsByTagName('base')[0].getAttribute('href') as string;
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
declare const window: any;
const initialState = window['initialReduxState'];
export const globalStore = configureStore(history, initialState);

const rootElement = document.getElementById('root');
const appInitial = (
    <Provider store={globalStore}>
        <Router history={history}>
            <App />
        </Router>
    </Provider>
);

ReactDOM.render(appInitial, rootElement);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: http://bit.ly/CRA-PWA
serviceWorker.unregister();
