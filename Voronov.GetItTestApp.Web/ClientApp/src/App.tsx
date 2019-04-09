import React, { Component } from 'react';
import { Route } from 'react-router';

import Layout from './components/layout/Layout';
import ErrorsPreviewPage from './pages/errors-preview/ErrorsPreviewPage';
import LoginPage from './pages/login/LoginPage';
import LogoutPage from './pages/login/LogoutPage';

import PrivateRoute from './components/private-route/PrivateRoute';
import UserSettingsPage from './pages/user-settings/UserSettingsPage';
import { EditErrorRecordPage } from './pages/edit-error-record/EditErrorRecordPage';
import { CreateNewErrorRecordPage } from './pages/create-new-error/CreateNewErrorRecordPage';
import UserListsPage from './pages/users-list/UserListsPage';

import './App.scss';

class App extends Component {
  render() {
    return (
      <div className="App">

        <Layout>
          <PrivateRoute exact path='/' component={ErrorsPreviewPage} />
          <Route path='/auth' component={LoginPage} />
          <Route path='/logout' component={LogoutPage} />
          <PrivateRoute exact path='/user' component={UserSettingsPage} />
          <PrivateRoute exact path='/record/:id' component={EditErrorRecordPage} />
          <PrivateRoute exact path='/new-record' component={CreateNewErrorRecordPage} />
          <PrivateRoute exact path='/users' component={UserListsPage} />
        </Layout>

      </div>
    );
  }
}

export default App;