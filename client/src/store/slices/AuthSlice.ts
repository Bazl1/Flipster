import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { IUser } from "../../types/IUser";
import AuthService from "../../services/AuthService";
import { AxiosError } from "axios";

interface ILogin {
    email: string;
    password: string;
}

interface IRegistration extends ILogin {
    name: string;
}

interface initialState {
    user: IUser;
    isAuth: boolean;
}

const initialState = {
    user: {} as IUser,
    isAuth: false,
};

export const registration = createAsyncThunk(
    "auth/registration",
    async function ({ name, email, password }: IRegistration, { dispatch }) {
        try {
            const response = await AuthService.registration(
                name,
                email,
                password,
            );
            localStorage.setItem("token", response.data.accessToken);
            dispatch(setAuth(true));
            dispatch(setUser(response.data.user));
            return response.status;
        } catch (error) {
            console.log(error);
        }
    },
);

export const login = createAsyncThunk(
    "auth/login",
    async function ({ email, password }: ILogin, { dispatch }) {
        try {
            const response = await AuthService.login(email, password);
            localStorage.setItem("token", response.data.accessToken);
            dispatch(setAuth(true));
            dispatch(setUser(response.data.user));
            return response.status;
        } catch (error) {
            console.log(error);
        }
    },
);

export const logout = createAsyncThunk(
    "auth/logout",
    async function (_, { dispatch }) {
        try {
            await AuthService.logout;
            localStorage.removeItem("token");
            dispatch(setAuth(false));
            dispatch(setUser({} as IUser));
        } catch (error) {
            console.log(error);
        }
    },
);

export const checkAuth = createAsyncThunk(
    "auth/refresh",
    async function (_, { dispatch }) {
        try {
            const response = await AuthService.refresh();
            localStorage.setItem("token", response.data.accessToken);
            dispatch(setAuth(true));
            dispatch(setUser(response.data.user));
        } catch (error) {
            console.log(error);
        }
    },
);

export const AuthSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        setAuth(state, action) {
            state.isAuth = action.payload;
        },
        setUser(state, action) {
            state.user = action.payload;
        },
    },
});

export const { setAuth, setUser } = AuthSlice.actions;
export default AuthSlice.reducer;
