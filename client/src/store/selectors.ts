import { RootState } from ".";

export const selectAuthInfo = (state: RootState) => state.auth;
export const selectUserInfo = (state: RootState) => state.auth.user;
export const selectIsAuth = (state: RootState) => state.auth.isAuth;
