import axios, { AxiosResponse } from 'axios';

import { getHeaders } from '../../components/Utils';
import { IUser } from '../../models';

class UserListsPageService implements IUserListsPageService {
    public async loadList(): Promise<Array<IUser>> {
        let loadResponse: AxiosResponse<Array<IUser>>;
        try {
            loadResponse = await axios.post<Array<IUser>>('/api/user/list', null, { headers: getHeaders() });

            return loadResponse.data;
        } catch (err) {
            console.log('something was terribly wrong');
            console.error(err);
            throw err;
        }
    }
}

interface IUserListsPageService {
    loadList(): Promise<Array<IUser>>;
}

const serviceInstance = new UserListsPageService();
export default serviceInstance;