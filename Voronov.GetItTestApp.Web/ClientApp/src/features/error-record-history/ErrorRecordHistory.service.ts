import axios, { AxiosResponse } from 'axios';

import IErrorRecordHistory from '../../models/error-record-editing/IErrorRecordHistory';
import { getHeaders } from '../../components/Utils';

class ErrorRecordHistoryservice implements IErrorRecordHistoryservice {
    public async loadById(id: number): Promise<Array<IErrorRecordHistory>> {
        let loadResponse: AxiosResponse<Array<IErrorRecordHistory>>;
        try {
            loadResponse = await axios.post<Array<IErrorRecordHistory>>(`/api/errorrecords/history?id=${id}`, null, { headers: getHeaders() });

            return loadResponse.data;
        } catch (err) {
            console.log('something was terribly wrong');
            console.error(err);
            throw err;
        }
    }
}

interface IErrorRecordHistoryservice {
    loadById(id: number): Promise<Array<IErrorRecordHistory>>;
}

const serviceInstance = new ErrorRecordHistoryservice();
export default serviceInstance;