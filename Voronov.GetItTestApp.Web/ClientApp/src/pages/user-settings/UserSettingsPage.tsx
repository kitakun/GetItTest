import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

import { IUserSettingsPageProps, IUserSettingsPageState } from './UserSettingsPage.interfaces';
import LoadingIndicator from '../../components/loading-indicator/LoadingIndicator';

import { globalStore } from '../..';
import UserService from './UserSettingsPage.service';

import './UserSettingsPage.scss';
import { ApplicationState } from '../../store';
import { updateUserInfoAction } from '../../store/login/actions';

class UserSettingsPage extends React.Component<IUserSettingsPageProps, IUserSettingsPageState> {

    constructor(prop: IUserSettingsPageProps) {
        super(prop);
        this.state = {
            isBusy: false,
            loadedUser: void 0,
            inErrorState: false
        }
    }

    public componentWillMount() {
        this.loadUserInfo();
    }

    private loadUserInfo(): void {
        try {
            const idForSearch = globalStore.getState().auth.userId;
            UserService.loadUserById(parseInt(idForSearch))
                .then((loadedUser) => {
                    this.setState({
                        isBusy: false,
                        loadedUser: loadedUser
                    });
                });
        } catch (err) {
            console.error(err);
            this.setState({
                inErrorState: true,
                isBusy: false
            })
        }
    }

    private handleOnChange(event: any) {
        let changedUser = { ...this.state.loadedUser, [event.target.name]: event.target.value };
        this.setState({ loadedUser: changedUser });
    }

    private startUpdateUser(): void {
        try {
            this.setState({ isBusy: true });
            const idForSearch = globalStore.getState().auth.userId;
            UserService.updateUser({
                id: parseInt(idForSearch),
                name: this.state.loadedUser.firstName,
                lastName: this.state.loadedUser.lastName
            }).then(() => {
                this.setState({ isBusy: false });
                this.props.dispatch(updateUserInfoAction(
                    this.state.loadedUser.firstName,
                    this.state.loadedUser.lastName));
            });
        } catch (err) {
            console.error(err);
            this.setState({ isBusy: false });
        }
    }

    public render() {

        let content = this.state.inErrorState
            ? this.errorPage()
            : (this.state.loadedUser
                ? this.settingPage()
                : this.loadingContent());

        return (
            <div className='user-settings'>
                <h3>User settings</h3>
                {content}
            </div>
        );
    }

    private loadingContent(): JSX.Element {
        return (<LoadingIndicator></LoadingIndicator>)
    }

    private settingPage(): JSX.Element {

        if (!this.state.loadedUser)
            throw Error('impossible');

        return (
            <div>

                <table className='table'>
                    <tbody>
                        <tr>
                            <td className='text-right'>Id</td>
                            <td className='text-left'>
                                <span className='muted'>{this.state.loadedUser.id}</span>
                            </td>
                        </tr>
                        <tr>
                            <td className='text-right'>Account Name</td>
                            <td className='text-left'>
                                <span className='muted'>{this.state.loadedUser.login}</span>
                            </td>
                        </tr>
                        <tr>
                            <td className='text-right'>First Name</td>
                            <td>
                                <input type="text"
                                    className="form-control"
                                    placeholder="First Name"
                                    disabled={this.state.isBusy}
                                    value={this.state.loadedUser.firstName}
                                    name="firstName"
                                    onChange={e => this.handleOnChange(e)} />
                            </td>
                        </tr>
                        <tr>
                            <td className='text-right'>Last Name</td>
                            <td>
                                <input type="text"
                                    className="form-control"
                                    placeholder="Last Name"
                                    disabled={this.state.isBusy}
                                    value={this.state.loadedUser.lastName}
                                    name="lastName"
                                    onChange={e => this.handleOnChange(e)} />
                            </td>
                        </tr>
                        <tr>
                            <td className='text-left'>
                                <Link to='/'>
                                    <button type="button" className="btn btn-warning text-right">Cancel</button>
                                </Link>
                            </td>
                            <td className='text-right'>
                                <button
                                    type="button"
                                    disabled={this.state.isBusy}
                                    onClick={e => this.startUpdateUser()}
                                    className="btn btn-success text-right">Update</button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        )
    }

    private errorPage(): JSX.Element {
        return (
            <div>
                <div className="alert alert-danger" role="alert">
                    <span className="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                    <span className="sr-only">Error:</span>
                    Try again later..
                </div>
            </div>
        )
    }
};

const mapStateToProps = (state: ApplicationState) => state.auth;
export default connect(mapStateToProps)(UserSettingsPage);