import { AxiosResponse } from "axios";
import axiosApi from "../shared/axios";
import { IUser } from "../types/IUser";

export default class SettingsService {
    static async changePassword(
        oldPassword: string,
        newPassword: string,
    ): Promise<AxiosResponse> {
        return axiosApi.put("/users/change-password", {
            oldPassword,
            newPassword,
        });
    }

    static async changeNumber(number: string): Promise<AxiosResponse<IUser>> {
        return axiosApi.put("/users/change-phone-number", { number });
    }

    static async changeAvatar(images: any): Promise<AxiosResponse<IUser>> {
        return axiosApi.put("/users/change-avatar", { images });
    }

    static async changeDetails(
        location: string,
        name: string,
    ): Promise<AxiosResponse<IUser>> {
        return axiosApi.put("/users/change-details", { location, name });
    }
}
