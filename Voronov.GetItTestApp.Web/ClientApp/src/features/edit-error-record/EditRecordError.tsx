import React from "react";
import { EditRecordErrorProps, EditRecordErrorState } from "./EditRecordError.interfaces";
import { Alert, Button } from "react-bootstrap";
import { Link, Redirect } from "react-router-dom";

import ErrorRecordsService from './EditRecordError.service';

import LoadingIndicator from "../../components/loading-indicator/LoadingIndicator";
import ErrorRecordHistory from "../error-record-history/ErrorRecordHistory";
import { ErrorStatus } from "../../models/IErrorRecordEnums";
import { enumFormatter } from '../../components/Utils';

import './EditRecordError.scss';

export class EditRecordError extends React.Component<EditRecordErrorProps, EditRecordErrorState> {

    constructor(props: EditRecordErrorProps) {
        super(props);
        this.state = {
            inErrorState: false,
            isBusy: false,
            comment: '',
            commentValid: false,
            record: {
                fullDescription: '',
                shortDescription: '',
                criticalType: 0,
                status: 0,
                urgency: 0
            }
        }
    }

    public componentWillMount() {
        if (!this.props.isNew) {
            if (!this.props.id) {
                this.setState({ inErrorState: true });
            } else {
                ErrorRecordsService
                    .loadById(this.props.id)
                    .then(loadedData => this.setState({
                        isBusy: false,
                        record: loadedData
                    }));
            }
        }
    }

    private handleOnChange(event: any) {
        const changedRecord = { ...this.state.record, [event.target.name]: event.target.value };
        this.setState({ record: changedRecord })
    }

    private commentChange(event: any): void {
        this.setState({ commentValid: this.state.comment.length > 0, comment: event.target.value });
    }

    private submitData() {
        if (this.props.isNew) {
            this.setState({ isBusy: true });
            ErrorRecordsService
                .createNewRecord(this.state.record)
                .then((entityId: number) => this.setState({ isBusy: false, redirectToId: entityId }))
                .catch(() => this.setState({ isBusy: false, inErrorState: true }));
        }
    }

    private changeState(newState: ErrorStatus) {

        if (this.state.comment.length == 0) {
            this.setState({ commentValid: false });
            return;
        }

        this.setState({ isBusy: true });
        ErrorRecordsService.changeState({
            action: newState,
            comment: this.state.comment,
            id: this.state.record.id
        })
            .then(() => {
                this.setState({ record: null, comment: '', commentValid: false });

                ErrorRecordsService
                    .loadById(this.props.id)
                    .then(loadedData => this.setState({
                        isBusy: false,
                        record: loadedData
                    }));
            })
            .catch(() => this.setState({ inErrorState: true, isBusy: false }));
    }

    public render() {
        let content = this.state.inErrorState
            ? this.errorPage()
            : (this.state.record
                ? this.settingPage()
                : this.loadingContent());

        if (this.state.redirectToId) {
            debugger
            content = (
                <Redirect to={`/record/${this.state.redirectToId}`}></Redirect>
            );
        }

        return (
            <div className='record-editing'>
                {content}
            </div>
        );
    }

    private loadingContent(): JSX.Element {
        return (<LoadingIndicator></LoadingIndicator>)
    }

    private settingPage(): JSX.Element {

        if (!this.state.record)
            throw Error('impossible');

        const actionsContent = this.workflowActions();

        return (
            <div>
                {!this.props.isNew &&
                    <Alert variant={'warning'}>
                        All changes will be displayed in history block!
                    </Alert>}

                <table className='table'>
                    <tbody>
                        <tr>
                            <td className='text-right'>Short Description</td>
                            <td className='text-left'>
                                <textarea
                                    className='form-control'
                                    disabled={this.state.isBusy || !this.props.isNew}
                                    name="shortDescription"
                                    onChange={e => this.handleOnChange(e)}
                                    value={this.state.record.shortDescription}>
                                </textarea>
                            </td>
                        </tr>
                        <tr>
                            <td className='text-right'>Full Description</td>
                            <td>
                                <textarea
                                    className='form-control'
                                    disabled={this.state.isBusy || !this.props.isNew}
                                    name="fullDescription"
                                    onChange={e => this.handleOnChange(e)}
                                    value={this.state.record.fullDescription}>
                                </textarea>
                            </td>
                        </tr>
                        <tr>
                            <td className='text-right'>Status</td>
                            <td>
                                <select value={this.state.record.status}
                                    name="status"
                                    className="form-control"
                                    disabled={true}>
                                    <option value={0}>New</option>
                                    <option value={1}>Opened</option>
                                    <option value={2}>Resolved</option>
                                    <option value={3}>Completed</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td className='text-right'>Urgency</td>
                            <td>
                                <select value={this.state.record.urgency}
                                    name="urgency"
                                    className="form-control"
                                    disabled={this.state.isBusy || !this.props.isNew}
                                    onChange={e => this.handleOnChange(e)}>
                                    <option value={0}>Non Emergent</option>
                                    <option value={10}>Not Urgent</option>
                                    <option value={20}>Urgent</option>
                                    <option value={30}>Very Urgent</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td className='text-right'>Critical Type</td>
                            <td>
                                <select value={this.state.record.criticalType}
                                    name="criticalType"
                                    className="form-control"
                                    disabled={this.state.isBusy || !this.props.isNew}
                                    onChange={e => this.handleOnChange(e)}>
                                    <option value={0}>Accident</option>
                                    <option value={1}>Critical</option>
                                    <option value={2}>NonCritical</option>
                                    <option value={3}>ChangeRequest</option>
                                </select>
                            </td>
                        </tr>
                        {!this.props.isNew && this.commentInput()}
                        <tr>
                            <td className='text-left'>
                                <Link to='/'>
                                    <Button variant={'warning'}>Back</Button>
                                </Link>
                            </td>
                            <td className='text-right'>
                                {actionsContent}
                            </td>
                        </tr>
                    </tbody>
                </table>

                {!this.props.isNew &&
                    <div>
                        <hr></hr>
                        <ErrorRecordHistory id={this.props.id}></ErrorRecordHistory>
                    </div>
                }
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

    private workflowActions(): JSX.Element {

        if (this.props.isNew) {
            return (<Button
                disabled={this.state.isBusy}
                onClick={_ => this.submitData()}
                variant={'success'}>Create</Button>);
        }

        return (
            <div>
                {this.state.record && this.state.record.availableNextStates &&
                    this.state.record.availableNextStates.map((state, ind) => {
                        return [
                            <Button
                                key={ind}
                                className='split'
                                disabled={this.state.isBusy}
                                onClick={_ => this.changeState(state)}
                                variant={'success'}>{enumFormatter(state, ErrorStatus)}</Button>
                        ]
                    })}
            </div>
        );
    }

    private commentInput(): JSX.Element {
        return (
            <tr>
                <td className='text-right'>Comment</td>
                <td>
                    <textarea
                        className={`form-control ${this.state.commentValid ? 'is-valid' : 'is-invalid'}`}
                        disabled={this.state.isBusy || this.props.isNew}
                        onChange={e => this.commentChange(e)}
                        value={this.state.comment}>
                    </textarea>
                </td>
            </tr>
        );
    }
}