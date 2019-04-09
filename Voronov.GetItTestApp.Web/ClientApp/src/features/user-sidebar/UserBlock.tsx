import React from "react";
import { Link } from 'react-router-dom'
import { connect } from "react-redux";

import { ApplicationState } from "../../store";

import { IUserBlockProps } from './UserBlock.interfaces';

import './UserBlock.scss';

class UserBlock extends React.Component<IUserBlockProps, any>{
    constructor(props: IUserBlockProps) {
        super(props);
        this.state = {};
    }

    render() {
        return (
            <div className='user-block'>
                <h3>USER</h3>
                <p>{this.props.username}</p>
                <div className='mb'>
                    <Link to="/user">
                        <button type="button" className="btn btn-outline-info btn-block">
                            Settings
                        </button>
                    </Link>
                </div>
                <div>
                    <Link to="/logout">
                        <button type="button" className="btn btn-outline-secondary btn-block">
                            Logout
                        </button>
                    </Link>
                </div>
            </div>
        );
    }
}

const mapStateToProps = (state: ApplicationState) => state.auth;
export default connect(mapStateToProps)(UserBlock);