import { IEditableErrorRecord } from "../../models/error-record-editing/IErrorRecordRequest";

export interface EditRecordErrorState {
    record?: IEditableErrorRecord;
    isBusy: boolean;
    inErrorState: boolean;
    redirectToId?: number;
    comment: string;
    commentValid: boolean;

    [key: string]: any;
}


export interface EditRecordErrorProps {
    isNew?: boolean;
    id?: number;
}