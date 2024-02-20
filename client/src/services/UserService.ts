import { AxiosResponse } from "axios";
import axiosApi from "../shared/axios";
import { IUser } from "../types/IUser";

export default class UserService {
    static async getUserInfo(id: string): Promise<AxiosResponse<IUser>> {
        return axiosApi.get(`/users/${id}`);
    }
}
