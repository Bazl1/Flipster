import { AxiosResponse } from "axios";
import axiosApi from "../shared/axios";
import { ChatsResponse } from "../types/response/ChatsResponse";
import { MessagesResponse } from "../types/response/MessagesResponse";

export default class ChatsService {
    static createChat(advertId: string): Promise<AxiosResponse<{ id: string }>> {
        return axiosApi.post<{ id: string }>("/chats", { advertId });
    }

    static deleteChat(id: string): Promise<void> {
        return axiosApi.delete(`/chats/${id}`);
    }

    static getChats(): Promise<AxiosResponse<ChatsResponse>> {
        return axiosApi.get<ChatsResponse>("/chats");
    }

    static getMessages(id: string): Promise<AxiosResponse<MessagesResponse>> {
        return axiosApi.get<MessagesResponse>(`/chats/${id}/messages`);
    }
}
