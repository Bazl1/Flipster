import { IUser } from "../IUser";

export interface AuthResponse {
    accessToken: string;
    antiforgeryToken?: string;
    user: IUser;
}
