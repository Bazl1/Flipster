import { configureStore } from "@reduxjs/toolkit";
import AuthSlice from "./slices/AuthSlice";
import { apiRTK } from "./api";

export const store = configureStore({
    reducer: {
        [apiRTK.reducerPath]: apiRTK.reducer,
        auth: AuthSlice,
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(apiRTK.middleware),
    devTools: true,
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
