import { apiRTK } from "../store/api";
import { AdvertResponse } from "../types/response/AdvertResponse";

export const favoriteAdvertApi = apiRTK.injectEndpoints({
    endpoints: (build) => ({
        addNewFavorite: build.mutation<void, { id: string }>({
            query: (params) => ({
                url: `/favorites/${params.id}`,
                method: "POST",
            }),
            invalidatesTags: (id) => (id ? [{ type: "Favorite", id: id }] : []),
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

export const { useAddNewFavoriteMutation, useGetFavoritesQuery } = favoriteAdvertApi;
