import React from 'react';
import { connect } from 'react-redux';

import { Redirect } from "react-router";

import { ILoginPageProps } from './LoginPage.interfaces';
import { ApplicationState } from '../../store';
import { authLogoutAction } from '../../store/login/actions';

class LogoutPage extends React.Component<ILoginPageProps, any> {

    constructor(props: any) {
        super(props);
    }

    componentWillMount() {
        this.props.dispatch(authLogoutAction())
    }

    render() {
        return (<Redirect to={{ pathname: '/auth' }} />)
    }
}

const mapStateToProps = (state: ApplicationState) => state.auth;
export default connect(mapStateToProps)(LogoutPage);