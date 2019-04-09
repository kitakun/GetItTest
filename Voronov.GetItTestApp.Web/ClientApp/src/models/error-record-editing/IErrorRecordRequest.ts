import { ErrorStatus, ErrorUrgency, ErrorImportanceType } from "../IErrorRecordEnums";

export interface IEditableErrorRecord {
    id?: number;

    shortDescription: string;
    fullDescription: string;

    status: ErrorStatus;
    urgency: ErrorUrgency;
    criticalType: ErrorImportanceType;

    availableNextStates?: Array<ErrorStatus>;
}