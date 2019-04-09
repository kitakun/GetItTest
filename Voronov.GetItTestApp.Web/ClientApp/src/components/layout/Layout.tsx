import React from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import NavBar from '../navbar/NavBar';

const Layout = (props: any) => {
    return (
        <Container fluid>
            <Row className='justify-content-md-center'>
                <Col sm={12} style={{padding:0}}>
                    <NavBar />
                </Col>
                <Col sm={12}>
                    {props.children}
                </Col>
            </Row>
        </Container>
    );
};
export default Layout;