import { apiRTK } from "../store/api";
import { AdvertResponse } from "../types/response/AdvertResponse";

export const favoriteAdvertApi = apiRTK.injectEndpoints({
    endpoints: (build) => ({
        toggleAddFavorite: build.mutation<{ advertId: string }, { advertId: string }>({
            query: (params) => ({
                url: `/favorites`,
                method: "POST",
                body: params,
            }),
            invalidatesTags: (advert) => (advert?.advertId ? [{ type: "Favorite", id: advert?.advertId }] : []),
        }),

        getFavorites: build.query<AdvertResponse, { page: number; limit: number }>({
            query: (params) => ({
                url: `/favorites/?page=${params.page}&limit=${params.limit}`,
                method: "GET",
            }),
            providesTags: (result) => {
                return result?.adverts
                    ? [
                          ...result.adverts.map(({ id }) => ({ type: "Favorite" as const, id })),
                          { type: "Favorite", id: "LIST" },
                      ]
                    : [{ type: "Favorite", id: "LIST" }];
            },
        }),
    }),
});

export const { useToggleAddFavoriteMutation, useGetFavoritesQuery } = favoriteAdvertApi;
