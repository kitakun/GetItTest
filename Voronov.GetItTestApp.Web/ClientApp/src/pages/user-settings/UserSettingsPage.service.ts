import axios, { AxiosResponse } from 'axios';

import { IUser } from '../../models/user/IUser';
import { IUserUpdateRequest } from '../../models/user/IUserUpdateReqest';
import { getHeaders } from '../../components/Utils';

class UserSettingsPageService implements IUserSettingsPageService {
    public async loadUserById(id: number): Promise<IUser> {

        let loadResponse: AxiosResponse<IUser>;

        try {
            loadResponse = await axios.post(`/api/user/loadbyid?id=${id}`, null, { headers: getHeaders() });

            return loadResponse.data;
        } catch (err) {
            console.log('something was terribly wrong');
            console.error(err);
            return null;
        }
    }

    public async updateUser(model: IUserUpdateRequest): Promise<boolean> {
        try {
            let loadResponse = await axios.post(`/api/user/updateuserinfo`, model, { headers: getHeaders() });

            return true;
        } catch (err) {
            console.log('something was terribly wrong');
            console.error(err);
            return false;
        }
    }
}

interface IUserSettingsPageService {
    loadUserById(id: number): Promise<IUser>;

    updateUser(model: IUserUpdateRequest): Promise<boolean>;
}

const serviceInstance = new UserSettingsPageService();
export default serviceInstance;