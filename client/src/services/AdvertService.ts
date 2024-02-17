import { apiRTK } from "../store/api";
import { IAdvert } from "../types/IAdvert";
import { AdvertResponse } from "../types/response/AdvertResponse";

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

        updateAdvert: build.mutation<IAdvert, Pick<IAdvert, "id"> & Partial<IAdvert>>({
            query(data) {
                const { id, ...body } = data;
                return {
                    url: `adverts/${id}`,
                    method: "PUT",
                    headers: {
                        RequestVerificationToken: localStorage.getItem("antiforgeryToken") || "",
                    },
                    body,
                };
            },
            invalidatesTags: (advert) => [{ type: "MyAdverts", id: advert?.id }],
        }),

        getMyAdverts: build.query<AdvertResponse, { limit: number; page: string; userId: string }>({
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
    }),
});

export const { useAddAvdertMutation, useUpdateAdvertMutation, useGetMyAdvertsQuery } = advertApi;
