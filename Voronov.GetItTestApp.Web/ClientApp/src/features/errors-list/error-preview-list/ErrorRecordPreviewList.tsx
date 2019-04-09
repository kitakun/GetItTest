import React, { } from "react";
import { Link } from "react-router-dom";
import { Button } from "react-bootstrap";
import { connect } from "react-redux";

import { BootstrapTable, TableHeaderColumn } from "react-bootstrap-table";

import LoadingIndicator from '../../../components/loading-indicator/LoadingIndicator'
import { IErrorsPreviewListProps, IErrorsPreviewListState } from './ErrorRecordPreviewList.interface'
import { ApplicationState } from "../../../store";
import { lunchAsyncPreviewLoadingAction } from "../../../store/errors-preview/actions";
import { ErrorStatus } from "../../../models/IErrorRecordEnums";

import './ErrorRecordPreviewList.scss';
import '../../../../node_modules/react-bootstrap-table/dist/react-bootstrap-table-all.min.css';


function enumToString(cell: any, row: any, enumObject: any):string {
    return enumObject[cell];
}

function sortByName(a, b, order, field, enumObject) {
    if (order === 'desc') {
        if (enumObject[a[field]] > enumObject[b[field]]) {
            return -1;
        } else if (enumObject[a[field]] < enumObject[b[field]]) {
            return 1;
        }
        return 0;
    }
    if (enumObject[a[field]] < enumObject[b[field]]) {
        return -1;
    } else if (enumObject[a[field]] > enumObject[b[field]]) {
        return 1;
    }
    return 0;
}

function formatRedirectLink(id: number) {
    return (
        <Link style={{ color: 'white' }} to={`/record/${id}`}>
            <Button variant={'info'}>
                Open
            </Button>
        </Link>
    );
}

class ErrorRecordPreviewList extends React.Component<IErrorsPreviewListProps, IErrorsPreviewListState>{
    constructor(props: any) {
        super(props);
        this.state = {};
    }

    public componentDidMount() {
        if (!this.props.inLoading && !this.props.lastLoadedData) {
            this.props.dispatch(lunchAsyncPreviewLoadingAction({}));
        }
    }

    public render() {
        let content = !this.props.inLoading
            ? this.tableContent()
            : this.loadingContent();

        return (
            <div className='error-preview'>
                <h3>Recorded errors:</h3>
                {content}
            </div>
        );
    }

    private loadingContent(): JSX.Element {
        return (<LoadingIndicator> </LoadingIndicator>);
    }

    private tableContent(): JSX.Element {
        let result: JSX.Element = (<div>Something gone wrong</div>);
        if (this.props.lastLoadedData) {
            this.props.lastLoadedData.forEach(ld => ld['open'] = () => console.log('hack'));
        }
        if (this.props.lastLoadedData) {
            if (this.props.lastLoadedData.length == 0) {
                result = (
                    <div>
                        <h4>Can't see anything</h4>
                    </div>
                )
            } else {
                result = (
                    <div>
                        <BootstrapTable ref='table' data={this.props.lastLoadedData} version='4'>
                            <TableHeaderColumn dataField='id' isKey={true} dataSort={true}>Record Id</TableHeaderColumn>
                            <TableHeaderColumn dataField='description' dataSort={false}>Description</TableHeaderColumn>
                            <TableHeaderColumn dataField='inputDate' dataSort={true}>Input Date</TableHeaderColumn>
                            <TableHeaderColumn
                                dataField='status'
                                dataSort={true}
                                formatExtraData={ErrorStatus}
                                dataFormat={enumToString}
                                sortFunc={sortByName}
                                sortFuncExtraData={ErrorStatus}>Status</TableHeaderColumn>
                            <TableHeaderColumn dataField='id' dataSort={false} dataFormat={formatRedirectLink}></TableHeaderColumn>
                        </BootstrapTable>
                    </div>
                );
            }
        }
        return result;
    }
}

const mapStateToProps = (state: ApplicationState) => state.errorsPreviewList;
export default connect(mapStateToProps)(ErrorRecordPreviewList);