import React, { ReactNode } from 'react';
import { Col } from 'react-bootstrap';

import "./AppPanel.scss";

interface IPanelProps {
    children: ReactNode;
}

const AppPanel = (props: IPanelProps) => {
    return (
        <Col className='app-panel text-left' xs={12}>
            {props.children}
        </Col>
    );
};
export default AppPanel;