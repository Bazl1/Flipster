export interface IMessage {
    id: string;
    from: {
        id: string;
        name: string;
        avatar: string;
    };
    text: string;
    isRead: boolean;
    isDeleted: boolean;
    createdAt: string;
}
