import React from 'react';
import { Route, Redirect, RouteProps, RouteComponentProps } from 'react-router';

import { globalStore } from '../..';

const PrivateRoute = ({ component, ...rest }: RouteProps) => {
    const authState = globalStore.getState().auth;

    if (!component) {
        throw Error("component is undefined");
    }

    const Component = component; // JSX Elements have to be uppercase.
    const render = (props: RouteComponentProps<any>): React.ReactNode => {
        if (!!authState.token) {
            return <Component {...props} />;
        }
        return <Redirect to={{ pathname: '/auth' }} />
    };

    return (<Route {...rest} render={render} />);
}

export default PrivateRoute;