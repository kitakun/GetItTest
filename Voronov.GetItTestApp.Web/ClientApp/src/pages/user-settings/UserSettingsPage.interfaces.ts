import { IUser } from "../../models";
import { AuthActions } from "../../store/login/types";
import { DispatchProp } from "react-redux";

export interface IUserSettingsPageState {
    loadedUser?: IUser;
    isBusy: boolean;
    inErrorState: boolean;

    [key: string]: any;
}

export interface IUserSettingsPageProps extends DispatchProp<AuthActions | any>  {
}