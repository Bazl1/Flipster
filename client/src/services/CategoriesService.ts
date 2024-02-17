import { AxiosResponse } from "axios";
import axiosApi from "../shared/axios";
import { ICategory } from "../types/response/CategoryResponse";

export default class CategoriesService {
    static async getCategorys(): Promise<AxiosResponse<ICategory[]>> {
        return axiosApi.get("/categroies");
    }
}
