import { AxiosResponse } from "axios";
import axiosApi from "../shared/axios/index";
import { AuthResponse } from "../types/response/AuthResponse";

export default class AuthService {
    static async registration(
        name: string,
        email: string,
        password: string,
    ): Promise<AxiosResponse<AuthResponse>> {
        return axiosApi.post("/auth/register", { name, email, password });
    }

    static async login(
        email: string,
        password: string,
    ): Promise<AxiosResponse<AuthResponse>> {
        return axiosApi.post("/auth/login", { email, password });
    }

    static async logout(): Promise<void> {
        return axiosApi.post("/auth/logout");
    }

    static async refresh(): Promise<AxiosResponse<AuthResponse>> {
        return axiosApi.post("/auth/refresh");
    }
}
