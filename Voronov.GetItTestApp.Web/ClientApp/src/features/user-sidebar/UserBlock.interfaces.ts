import { DispatchProp } from "react-redux";
import { AuthState, AuthActions } from "../../store/login/types";

export interface IUserBlockProps extends DispatchProp<AuthActions | any>, AuthState {
}