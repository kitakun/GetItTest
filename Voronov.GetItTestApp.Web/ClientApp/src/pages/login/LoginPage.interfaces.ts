import { AuthState, AuthActions } from "../../store/login/types";
import { DispatchProp } from "react-redux";
import { RouteComponentProps } from "react-router";

export interface ILoginPageState {
    redirectTimeoutStarted?: boolean;

    account: string;
    password: string;

    [key: string]: any;
}

export interface ILoginPageProps extends DispatchProp<AuthActions | any>, AuthState, RouteComponentProps {
}