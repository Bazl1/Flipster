import { AxiosResponse } from "axios";
import axiosApi from "../shared/axios";
import { IUser } from "../types/IUser";

export default class SettingsService {
    static async changePassword(
        currentPassword: string,
        newPassword: string,
    ): Promise<AxiosResponse> {
        return axiosApi.put("/users/change-password", {
            currentPassword,
            newPassword,
        });
    }

    static async changeNumber(
        phoneNumber: string,
    ): Promise<AxiosResponse<IUser>> {
        return axiosApi.put("/users/change-phone", { phoneNumber });
    }

    static async changeAvatar(images: any): Promise<AxiosResponse<IUser>> {
        return axiosApi.put("/users/change-avatar", images, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        });
    }

    static async changeDetails(
        location: string,
        name: string,
    ): Promise<AxiosResponse<IUser>> {
        return axiosApi.put("/users/change-details", { location, name });
    }
}
