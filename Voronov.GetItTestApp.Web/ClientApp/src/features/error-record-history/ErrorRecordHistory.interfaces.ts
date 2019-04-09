import IErrorRecordHistory from '../../models/error-record-editing/IErrorRecordHistory';

export interface RecordErrorHistoryState {
    isBusy: boolean;
    history?: Array<IErrorRecordHistory>;
    errorState: boolean;

    [key: string]: any;
}


export interface RecordErrorHistoryProps {
    id?: number;
}