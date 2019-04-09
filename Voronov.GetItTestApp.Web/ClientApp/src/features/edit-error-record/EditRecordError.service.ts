import axios, { AxiosResponse } from 'axios';

import { IEditableErrorRecord } from '../../models/error-record-editing/IErrorRecordRequest';
import IErrorRecordChangeStateRequest from '../../models/error-record-editing/IErrorRecordChangeStateRequest';
import { getHeaders } from '../../components/Utils';

class ErrorRecordsService implements IErrorRecordsService {
    public async createNewRecord(entity: IEditableErrorRecord): Promise<number> {
        try {
            let loadResponse = await axios.post<number>(`/api/errorrecords/create`, entity, { headers: getHeaders() });

            return loadResponse.data;
        } catch (err) {
            console.log('something was terribly wrong');
            console.error(err);
            throw err;
        }
    }

    public async loadById(id: number): Promise<IEditableErrorRecord> {
        let loadResponse: AxiosResponse<IEditableErrorRecord>;
        try {
            loadResponse = await axios.post(`/api/errorrecords/byId?id=${id}`, null, { headers: getHeaders() });

            return loadResponse.data;
        } catch (err) {
            console.log('something was terribly wrong');
            console.error(err);
            throw err;
        }
    }

    public async changeState(entity: IErrorRecordChangeStateRequest): Promise<boolean> {
        try {
            let loadResponse = await axios.post<boolean>(`/api/errorrecords/change`, entity, { headers: getHeaders() });

            return loadResponse.data;
        } catch (err) {
            console.log('something was terribly wrong');
            console.error(err);
            throw err;
        }
    }
}

interface IErrorRecordsService {
    createNewRecord(entity: IEditableErrorRecord): Promise<number>;

    loadById(id: number): Promise<IEditableErrorRecord>;

    changeState(entity: IErrorRecordChangeStateRequest): Promise<boolean>;
}

const serviceInstance = new ErrorRecordsService();
export default serviceInstance;