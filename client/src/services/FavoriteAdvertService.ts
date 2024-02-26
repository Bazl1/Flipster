import { apiRTK } from "../store/api";

export const favoriteAdvertApi = apiRTK.injectEndpoints({
    endpoints: (build) => ({
        synchronization: build.query<[{ id: string }], void>({
            query: () => {
                return {
                    url: "/",
                    method: "GET",
                };
            },
        }),
    }),
});
