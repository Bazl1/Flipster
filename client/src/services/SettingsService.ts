import { AxiosResponse } from "axios";
import axiosApi from "../shared/axios";
import { IUser } from "../types/IUser";

export default class SettingsService {
    static async changePassword(currentPassword: string, newPassword: string): Promise<AxiosResponse> {
        return axiosApi.put("/users/my/change-password", {
            currentPassword,
            newPassword,
        });
    }

    static async changeNumber(phoneNumber: string): Promise<AxiosResponse<IUser>> {
        return axiosApi.put("/users/my/change-settings", { phoneNumber });
    }

    static async changeAvatar(images: any): Promise<AxiosResponse<IUser>> {
        return axiosApi.put("/users/my/change-avatar", images, {
            headers: {
                RequestVerificationToken: localStorage.getItem("antiforgeryToken") || "",
            },
        });
    }

    static async changeDetails(location: string, name: string): Promise<AxiosResponse<IUser>> {
        return axiosApi.put(`/users/my/change-settings`, { location, name });
    }
}
