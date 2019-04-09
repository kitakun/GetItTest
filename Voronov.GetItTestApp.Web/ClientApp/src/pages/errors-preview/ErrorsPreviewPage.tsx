import React from 'react';
import { Link } from 'react-router-dom';

import { Container, Row, Col, Button } from 'react-bootstrap';

import AppPanel from '../../components/layout/AppPanel';
import ErrorRecordPreviewList from '../../features/errors-list/error-preview-list/ErrorRecordPreviewList'
import UserBlock from '../../features/user-sidebar/UserBlock';

const ErrorsPreviewPage = () => {
    return (
        <Container fluid>
            <Row>
                <Col sm={9}>
                    <AppPanel>
                        <Link to='/new-record'>
                            <Button variant={'info'}>Create new</Button>
                        </Link>
                    </AppPanel>

                    <ErrorRecordPreviewList></ErrorRecordPreviewList>
                </Col>
                <Col sm={3}>
                    <UserBlock />
                </Col>
            </Row>
        </Container>
    );
};
export default ErrorsPreviewPage;