import React from 'react';
import { Link } from 'react-router-dom'
import { connect } from 'react-redux';
import { Button } from 'react-bootstrap';

import { ApplicationState } from '../../store';
import { AuthState } from '../../store/login/types';

import './NavBar.scss';

interface INavBarProps extends AuthState {
}

class NavBar extends React.Component<INavBarProps, any>{

    constructor(props: INavBarProps) {
        super(props);
    }

    public render() {

        const isLogged = this.props.token != null && this.props.token.length > 0;
        const btnsContent = isLogged
            ? (<div>
                <Link to='/new-record'>
                    <Button className='split' variant={'info'}>New error</Button>
                </Link>
                <Link to='/'>
                    <Button className='split' variant={'info'}>Error list</Button>
                </Link>
                <Link to='/users'>
                    <Button className='split' variant={'info'}>All users</Button>
                </Link>
                <Link to='/logout'>
                    <Button className='split' variant={'info'}>Logout</Button>
                </Link>
            </div>)
            : (<div></div>);

        return (
            <div className='navBar'>
                <nav className="navbar navbar-light bg-light">
                    <Link to='/' className='navbar-brand'>
                        Error Tracking System
                    </Link>
                    {btnsContent}
                </nav>
            </div>
        );
    }
}

const mapStateToProps = (state: ApplicationState) => state.auth;
export default connect(mapStateToProps)(NavBar);