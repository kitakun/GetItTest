import { ErrorStatus } from "../IErrorRecordEnums";

export default interface IErrorRecordChangeStateRequest {
    id: number;

    comment: string;

    action: ErrorStatus;
}