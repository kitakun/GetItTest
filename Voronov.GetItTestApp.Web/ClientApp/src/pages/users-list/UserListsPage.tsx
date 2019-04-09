import React from "react";

import { UserListPageState } from "./UserListsPage.interface";
import UserListsPageService from './UserListsPage.service';
import { Alert, Table } from "react-bootstrap";
import LoadingIndicator from "../../components/loading-indicator/LoadingIndicator";

export default class UserListsPage extends React.Component<any, UserListPageState>{

    constructor(props: any) {
        super(props);
        this.state = {
            errorState: false,
            users: void 0,
            isBusy: false
        };
    }

    public componentWillMount() {
        this.setState({ isBusy: true });
        UserListsPageService
            .loadList()
            .then((allUsers) => this.setState({ users: allUsers, isBusy: false }))
            .catch(() => this.setState({ isBusy: false, errorState: true }));
    }

    public render() {
        const content = !this.state.isBusy
            ? this.usersTable()
            : <LoadingIndicator></LoadingIndicator>;

        return (
            <div className='user-block'>
                <Alert variant={'info'}>
                    All users
                </Alert>
                {content}
            </div>
        );
    }

    private usersTable(): JSX.Element {
        return (
            <Table>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Login</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.users.map((user, ind) => {
                        return [
                            <tr key={user.id}>
                                <td>{user.id}</td>
                                <td>{user.login}</td>
                                <td>{user.firstName}</td>
                                <td>{user.lastName}</td>
                            </tr>
                        ]
                    })}
                </tbody>
            </Table>
        );
    }
}