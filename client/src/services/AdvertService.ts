import { apiRTK } from "../store/api";
import { IAdvert } from "../types/IAdvert";
import { Advert, AdvertResponse } from "../types/response/AdvertResponse";

export const advertApi = apiRTK.injectEndpoints({
    endpoints: (build) => ({
        addAvdert: build.mutation<void, FormData>({
            query: (body: FormData) => ({
                url: "/adverts",
                method: "POST",
                headers: {
                    RequestVerificationToken: localStorage.getItem("antiforgeryToken") || "",
                },
                body: body,
            }),
            invalidatesTags: [{ type: "MyAdverts", id: "LIST" }],
        }),

        deleteAdvert: build.mutation<void, { id: string }>({
            query: (params) => ({
                url: `/adverts/${params.id}`,
                method: "DELETE",
            }),
            invalidatesTags: () => [{ type: "MyAdverts", id: "LIST" }],
        }),

        updateAdvert: build.mutation<IAdvert, { id: string; body: FormData }>({
            query: (params) => ({
                url: `adverts/${params.id}`,
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

        getMyAdverts: build.query<AdvertResponse, { limit: number; page: number; userId: string }>({
            query: (params) => ({
                url: `/adverts/?page=${params.page}&limit=${params.limit}&user=${params.userId}`,
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

        getAdvertForId: build.query<Advert, { id: string }>({
            query: (params) => ({
                url: `/adverts/${params.id}`,
                method: "GET",
            }),
            providesTags: (advert) => (advert ? [{ type: "Adverts", id: advert.id }] : []),
        }),
    }),
});

export const {
    useAddAvdertMutation,
    useUpdateAdvertMutation,
    useGetMyAdvertsQuery,
    useDeleteAdvertMutation,
    useGetAdvertForIdQuery,
} = advertApi;
