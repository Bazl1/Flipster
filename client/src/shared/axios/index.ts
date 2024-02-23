import axios from "axios";
import AuthService from "../../services/AuthService";

export const API_URL = "http://localhost:5145/api";

const axiosApi = axios.create({
    withCredentials: true,
    baseURL: API_URL,
});

axiosApi.interceptors.request.use((config) => {
    const token = localStorage.getItem("token");
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

axiosApi.interceptors.response.use(
    (config) => {
        return config;
    },
    async (error) => {
        const originalRequest = error.config;
        if (error.response.status == 401 && originalRequest._isRetry === false) {
            originalRequest._isRetry = true;
            const response = await AuthService.refresh();
            localStorage.setItem("token", response.data.accessToken);
            return axiosApi.request(originalRequest);
        }
        return Promise.reject(error);
    },
);

export default axiosApi;
