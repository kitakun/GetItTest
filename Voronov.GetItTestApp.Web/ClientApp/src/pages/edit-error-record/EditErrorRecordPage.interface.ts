import { RouteComponentProps } from "react-router";

export interface EditRecordErrorPageState { }

interface MatchParams {
    id: string;
}

export interface EditRecordErrorPageProps extends RouteComponentProps<MatchParams> { }