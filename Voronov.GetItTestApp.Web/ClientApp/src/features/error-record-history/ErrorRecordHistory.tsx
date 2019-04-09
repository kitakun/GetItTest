import React from "react";
import { Alert, Table } from "react-bootstrap";

import { RecordErrorHistoryProps, RecordErrorHistoryState } from './ErrorRecordHistory.interfaces'
import LoadingIndicator from "../../components/loading-indicator/LoadingIndicator";
import ErrorRecordHistoryservice from './ErrorRecordHistory.service';
import { ErrorStatus } from "../../models/IErrorRecordEnums";
import { enumFormatter } from "../../components/Utils";

export default class ErrorRecordHistory extends React.Component<RecordErrorHistoryProps, RecordErrorHistoryState>{
    constructor(props: RecordErrorHistoryProps) {
        super(props);
        this.state = {
            errorState: false,
            history: void 0,
            isBusy: false
        };
    }

    public componentWillMount() {
        this.setState({ isBusy: true });
        ErrorRecordHistoryservice
            .loadById(this.props.id)
            .then((historyArray) => this.setState({ history: historyArray }))
            .catch(() => this.setState({ errorState: true }))
            .finally(() => this.setState({ isBusy: false }));
    }

    public render(): JSX.Element {

        const content = !this.state.isBusy
            ? this.historyContent()
            : this.loadingContent();

        return (
            <div className='user-block'>
                <Alert variant={'info'}>
                    History info
                </Alert>
                {content}
            </div>
        );
    }

    private loadingContent(): JSX.Element {
        return (<LoadingIndicator></LoadingIndicator>)
    }

    private historyContent(): JSX.Element {
        return (
            <Table>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>To State</th>
                        <th>Date</th>
                        <th>Comment</th>
                        <th>Changed By</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.history.map((history, ind) => {
                        return [
                            <tr key={history.id}>
                                <td>{history.id}</td>
                                <td>{enumFormatter(history.action, ErrorStatus)}</td>
                                <td>{new Date(history.date).toLocaleDateString()}</td>
                                <td>{history.comment}</td>
                                <td>{history.changedBy}</td>
                            </tr>
                        ]
                    })}
                </tbody>
            </Table>
        );
    }
}
