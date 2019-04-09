import React from "react";

import { IErrorPreviewElementProps } from './ErrorPreviewElement.interface';
import { Row, Col } from "react-bootstrap";

import './ErrorPreviewElement.scss';

export default class ErrorPreviewElement extends React.Component<IErrorPreviewElementProps, any>{
    constructor(props: IErrorPreviewElementProps) {
        super(props);
        this.state = {};
    }

    render() {
        return (
            <div className='error-record'>

                <Row className='no-margin header'>
                    <Col xs={6}>
                        <p className='muted text-left'>{this.props.entity.id}</p>
                    </Col>
                    <Col xs={6}>
                        <p className='muted text-right'>{this.toLocaleDateString(this.props.entity.inputDate)}</p>
                    </Col>
                </Row>

                <Row className='text-left'>
                    <Col xs={12}>
                        <p className='no-margin'>Preview description:</p>
                        <p className='no-margin'>{this.props.entity.description}</p>
                    </Col>
                </Row>

                <table className='table table-sm'>
                    <tbody>
                        <tr>
                            <td>Status</td>
                            <td>Urgency</td>
                            <td>Importance Type</td>
                        </tr>
                        <tr>
                            <td>{this.props.entity.status}</td>
                            <td>{this.props.entity.urgency}</td>
                            <td>{this.props.entity.importanceType}</td>
                        </tr>
                    </tbody>
                </table>

            </div>
        );
    }

    private toLocaleDateString(date: any): string {
        const parsedDate = new Date(date);
        const result = parsedDate.toLocaleDateString();
        return result;
    }
}