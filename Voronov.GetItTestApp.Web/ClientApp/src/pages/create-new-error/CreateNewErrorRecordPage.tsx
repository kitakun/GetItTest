import React from "react";
import { CreateNewErrorProps, CreateNewErrorState } from "./CreateNewErrorRecordPage.interfaces";
import { EditRecordError } from "../../features/edit-error-record/EditRecordError";

export class CreateNewErrorRecordPage extends React.Component<CreateNewErrorProps, CreateNewErrorState> {

    constructor(props: CreateNewErrorProps) {
        super(props);
    }

    public render() {
        return (
            <div>
                <EditRecordError isNew={true}></EditRecordError>
            </div>
        );
    }
}