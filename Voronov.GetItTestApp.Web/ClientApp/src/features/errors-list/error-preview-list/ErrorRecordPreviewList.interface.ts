import { DispatchProp } from "react-redux";

import { ErrorsPreviewListActions, ErrorsPreviewState } from "../../../store/errors-preview/types";

export interface IErrorsPreviewListState {

    [key: string]: any;
}

export interface IErrorsPreviewListProps extends DispatchProp<ErrorsPreviewListActions | any>, ErrorsPreviewState {
}