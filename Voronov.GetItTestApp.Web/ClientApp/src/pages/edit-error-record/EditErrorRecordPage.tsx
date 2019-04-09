import React from "react";
import { EditRecordErrorPageProps, EditRecordErrorPageState } from "./EditErrorRecordPage.interface";
import { EditRecordError } from "../../features/edit-error-record/EditRecordError";

export class EditErrorRecordPage extends React.Component<EditRecordErrorPageProps, EditRecordErrorPageState> {

    constructor(props: EditRecordErrorPageProps) {
        super(props);
    }

    public render() {
        return (
            <div>
                <EditRecordError isNew={false} id={parseInt(this.props.match.params.id)}></EditRecordError>
            </div>
        );
    }
}