import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import SettingsService from "../../services/SettingsService";
import { setUser } from "./AuthSlice";

interface ChangePasswordProps {
    oldPassword: string;
    newPassword: string;
}

interface ChangeDetailsProps {
    locationLabel: string;
    name: string;
}

export const changePassword = createAsyncThunk(
    "settings/changePassword",
    async function ({ oldPassword, newPassword }: ChangePasswordProps) {
        try {
            const response = await SettingsService.changePassword(
                oldPassword,
                newPassword,
            );
            return response.status;
        } catch (error) {
            console.log(error);
        }
    },
);

export const changeNumber = createAsyncThunk(
    "settings/changeNumber",
    async function ({ number }: { number: string }, { dispatch }) {
        try {
            const response = await SettingsService.changeNumber(number);
            dispatch(setUser(response.data));
            return response.status;
        } catch (error) {
            console.log(error);
        }
    },
);

export const changeDetails = createAsyncThunk(
    "settings/changeDetails",
    async function ({ locationLabel, name }: ChangeDetailsProps, { dispatch }) {
        try {
            const response = await SettingsService.changeDetails(
                locationLabel,
                name,
            );
            dispatch(setUser(response.data));
            return response.status;
        } catch (error) {
            console.log(error);
        }
    },
);

export const changeAvatar = createAsyncThunk(
    "settings/changeAvatar",
    async function ({ imgUrl }: { imgUrl: any }, { dispatch }) {
        try {
            const DataFile = new FormData();
            DataFile.append("Avatar", imgUrl);
            const response = await SettingsService.changeAvatar(DataFile);
            dispatch(setUser(response.data));
            return response.status;
        } catch (error) {
            console.log(error);
        }
    },
);

export const SettingSlice = createSlice({
    name: "settings",
    initialState: {},
    reducers: {},
});
