import { apiRTK } from "../store/api";
import { IAdvert } from "../types/IAdvert";
import { Advert, AdvertResponse } from "../types/response/AdvertResponse";

export const advertApi = apiRTK.injectEndpoints({
    endpoints: (build) => ({
        addAvdert: build.mutation<void, FormData>({
            query: (body: FormData) => ({
                url: "/catalog",
                method: "POST",
                headers: {
                    RequestVerificationToken: localStorage.getItem("antiforgeryToken") || "",
                },
                body: body,
            }),
            invalidatesTags: [
                { type: "MyAdverts", id: "LIST" },
                { type: "Adverts", id: "LIST" },
            ],
        }),

        deleteAdvert: build.mutation<void, { id: string }>({
            query: (params) => ({
                url: `/catalog/${params.id}`,
                method: "DELETE",
            }),
            invalidatesTags: () => [{ type: "MyAdverts", id: "LIST" }],
        }),

        updateAdvert: build.mutation<IAdvert, { id: string; body: FormData }>({
            query: (params) => ({
                url: `/catalog/${params.id}`,
                method: "PUT",
                headers: {
                    RequestVerificationToken: localStorage.getItem("antiforgeryToken") || "",
                },
                body: params.body,
            }),
            invalidatesTags: (advert) => [
                { type: "MyAdverts", id: advert?.id },
                { type: "Adverts", id: advert?.id },
            ],
        }),

        getUserAdverts: build.query<AdvertResponse, { limit: number; page: number; userId: string }>({
            query: (params) => ({
                url: `/catalog/?page=${params.page}&limit=${params.limit}&userId=${params.userId}`,
                method: "GET",
            }),
            providesTags: (result) =>
                result?.adverts
                    ? [
                          ...result.adverts.map(({ id }) => ({ type: "MyAdverts" as const, id })),
                          { type: "MyAdverts", id: "LIST" },
                      ]
                    : [{ type: "MyAdverts", id: "LIST" }],
        }),

        getAdverts: build.query<AdvertResponse, { limit: number; page: number }>({
            query: (params) => ({
                url: `/catalog/?page=${params.page}&limit=${params.limit}`,
                method: "GET",
            }),
            providesTags: (result) => {
                return result?.adverts
                    ? [
                          ...result.adverts.map(({ id }) => ({ type: "Adverts" as const, id })),
                          { type: "Adverts", id: "LIST" },
                      ]
                    : [{ type: "Adverts", id: "LIST" }];
            },
        }),

        getRecommendedAdverts: build.query<AdvertResponse, { limit: number; page: number }>({
            query: (params) => ({
                url: `catalog/recommendations/?page=${params.page}&limit=${params.limit}`,
                method: "GET",
            }),
            providesTags: (result) => {
                return result?.adverts
                    ? [
                          ...result.adverts.map(({ id }) => ({ type: "Adverts" as const, id })),
                          { type: "Adverts", id: "LIST" },
                      ]
                    : [{ type: "Adverts", id: "LIST" }];
            },
        }),

        getRecommendedForAdvert: build.query<AdvertResponse, { id: string }>({
            query: (params) => ({
                url: `catalog/recommendations/${params.id}/?page=${1}`,
                method: "GET",
            }),
            providesTags: (result) => {
                return result?.adverts
                    ? [
                          ...result.adverts.map(({ id }) => ({ type: "Adverts" as const, id })),
                          { type: "Adverts", id: "LIST" },
                      ]
                    : [{ type: "Adverts", id: "LIST" }];
            },
        }),

        getAdvertForId: build.query<Advert, { id: string }>({
            query: (params) => ({
                url: `/catalog/${params.id}`,
                method: "GET",
            }),
            providesTags: (advert) => (advert ? [{ type: "Adverts", id: advert.id }] : []),
        }),

        getAdvertsSearch: build.query<
            AdvertResponse,
            {
                limit: number;
                page: number;
                query?: string;
                categoryId?: string;
                location?: string;
                min?: string;
                max?: string;
                sort?: string;
            }
        >({
            query: (params) => ({
                url: `/catalog/?page=${params.page}&limit=${params.limit}&query=${params.query || ""}&categoryId=${params.categoryId || ""}&location=${params.location || ""}&min=${params.min || -1}&max=${params.max || -1}&sort=${params.sort || ""}`,
                method: "GET",
            }),
            providesTags: (result) => {
                return result?.adverts
                    ? [
                          ...result.adverts.map(({ id }) => ({ type: "Adverts" as const, id })),
                          { type: "Adverts", id: "LIST" },
                      ]
                    : [{ type: "Adverts", id: "LIST" }];
            },
        }),
    }),
});

export const {
    useAddAvdertMutation,
    useUpdateAdvertMutation,
    useGetUserAdvertsQuery,
    useDeleteAdvertMutation,
    useGetAdvertForIdQuery,
    useGetAdvertsQuery,
    useGetAdvertsSearchQuery,
    useLazyGetAdvertsSearchQuery,
    useGetRecommendedAdvertsQuery,
    useGetRecommendedForAdvertQuery,
} = advertApi;
