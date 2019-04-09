import { ErrorImportanceType, ErrorStatus, ErrorUrgency } from '../IErrorRecordEnums';

export interface IErrorRecordPreviewModel {
    id: number;

    inputDate: Date;

    description: string;

    status: ErrorStatus;

    urgency: ErrorUrgency;

    importanceType: ErrorImportanceType;
}