import { ErrorStatus } from "../IErrorRecordEnums";

export default interface IErrorRecordHistory {
    id: number;

    date: Date;

    comment: string;

    action: ErrorStatus;

    changedBy: string;
}