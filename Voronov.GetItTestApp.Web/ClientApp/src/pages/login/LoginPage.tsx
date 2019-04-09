import React from 'react';
import { connect } from 'react-redux';

import { withRouter } from 'react-router';
import { Col, Row } from 'react-bootstrap';

import { ILoginPageState, ILoginPageProps } from './LoginPage.interfaces';
import { ApplicationState } from '../../store';
import { createAsyncAuthLoginAction } from '../../store/login/actions';

import './LoginPage.scss';

class LoginPage extends React.Component<ILoginPageProps, ILoginPageState> {

    constructor(props: ILoginPageProps) {
        super(props);
        this.state = {
            redirectToReferrer: props.token != null,
            account: '',
            password: '',
            redirectTimeoutStarted: false
        };
    }

    public login(): void {
        this.props.dispatch(createAsyncAuthLoginAction({
            login: this.state.account,
            password: this.state.password
        }));
    }

    private handleOnChange(event: any) {
        this.setState({ [event.target.name]: event.target.value });
    }

    private globalKeyDownEvent(event: { keyCode: number; }) {
        //enter
        if (event.keyCode == 13) {
            this.login();
        }
    }

    public componentDidUpdate() {
        if (this.props.token
            && !this.state.redirectTimeoutStarted
            && !this.state.redirectToReferrer) {

            this.setState({ redirectTimeoutStarted: true });

            setTimeout(() => this.props.history.push('/'), 2000);
        }
    }

    public render(): JSX.Element {

        let formContent = this.props.token
            ? this.successMessageContent()
            : this.mainFormContent();

        return (
            <Row className='justify-content-md-center'>
                <Col xs={12} sm={9} md={6} lg={3} className="login-page">
                    {formContent}
                </Col>
            </Row>
        );
    };

    private successMessageContent(): JSX.Element {
        return (
            <div>
                <div className="alert alert-success" role="alert">
                    successfully logged in
                </div>
                <p>you will be redirected soon</p>
            </div>
        );
    }

    private mainFormContent(): JSX.Element {
        return (
            <div>
                <div className="form-group">
                    <span>Email address</span>
                    <input type="email"
                        className="form-control"
                        disabled={this.props.inLoading}
                        value={this.state.account}
                        name="account"
                        onChange={e => this.handleOnChange(e)}
                        onKeyDown={e => this.globalKeyDownEvent(e)}
                        aria-describedby="emailHelp"
                        placeholder="Enter email" />
                    <small className="form-text text-muted">We'll never share your email with anyone else.</small>
                </div>

                <div className="form-group">
                    <span>Password</span>
                    <input type="password"
                        disabled={this.props.inLoading}
                        value={this.state.password}
                        name="password"
                        onChange={e => this.handleOnChange(e)}
                        onKeyDown={e => this.globalKeyDownEvent(e)}
                        className="form-control"
                        placeholder="Password" />
                </div>

                <div className="footer">
                    <button onClick={e => this.login()} disabled={this.props.inLoading} className="btn btn-primary">
                        <span>Sign in</span>
                        <span hidden={!this.props.inLoading} className="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                    </button>
                </div>
            </div>
        );
    }
};

const mapStateToProps = (state: ApplicationState) => state.auth;
export default withRouter(connect(mapStateToProps)(LoginPage));