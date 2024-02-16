import { createApi, fetchBaseQuery, retry, BaseQueryFn, FetchArgs } from "@reduxjs/toolkit/query/react";
import AuthService from "../services/AuthService";

const baseQuery = fetchBaseQuery({
    baseUrl: "http://localhost:5247/api",
    prepareHeaders: (headers) => {
        const token = localStorage.getItem("token");
        if (token) {
            headers.set("Authorization", `Bearer ${token}`);
        }
        return headers;
    },
    credentials: "include",
});

const baseQueryWithRefresh: BaseQueryFn<string | FetchArgs, unknown, unknown> = async (args, api, extraOptions) => {
    let result = await baseQuery(args, api, extraOptions);

    if (result.error && result.error.status === 401) {
        try {
            const response = await AuthService.refresh();
            localStorage.setItem("token", response.data.accessToken);
            result = await baseQuery(args, api, extraOptions);
        } catch (error) {
            localStorage.removeItem("token");
        }
    }

    return result;
};

const baseQueryWithRetry = retry(baseQueryWithRefresh, { maxRetries: 2 });

export const apiRTK = createApi({
    baseQuery: baseQueryWithRetry,
    tagTypes: ["Adverts", "MyAdverts"],
    endpoints: () => ({}),
    keepUnusedDataFor: 60,
});
