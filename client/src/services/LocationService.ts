import axiosApi from "../shared/axios";
import { IGetLocationList } from "../component/LocationSelect/LocationSelect";
import { AxiosResponse } from "axios";

export default class LocationService {
    static async getLocationList(): Promise<AxiosResponse<IGetLocationList[]>> {
        return axiosApi.get("/locations");
    }
}
