export interface IChat {
    id: string;
    title: string;
    unreadMessageCount: number;
    interlocutor: {
        id: string;
        name: string;
        avatar: string;
    };
}
