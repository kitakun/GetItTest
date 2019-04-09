import { IUser } from "../../models";

export interface UserListPageState {
    isBusy: boolean;
    users?: Array<IUser>;
    errorState: boolean;
}