import { useForm } from "react-hook-form";
import s from "./MessagePage.module.scss";
import { useCallback, useEffect, useState } from "react";
import MessageItem from "../../component/MessageItem/MessageItem";
import ChatsService from "../../services/ChatsService";
import { useParams } from "react-router-dom";
import { IMessage } from "../../types/IMessage";
import { useAppSelector } from "../../shared/hooks/storeHooks";
import { selectAuthInfo } from "../../store/selectors";
import * as signalR from "@microsoft/signalr";

let connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5145/hubs/chats", {
        accessTokenFactory: () => `${localStorage.getItem("token")}`,
    })
    .build();

const removeMessage = (id: string, array: IMessage[]) => {
    const updatedMessages = array?.map((item: IMessage) => {
        if (item.id === id) {
            return { ...item, text: "This message has been deleted." };
        }
        return item;
    });
    return updatedMessages;
};

const MessagePage = () => {
    console.log("Page render");
    const { id } = useParams();
    const chatId = id || "";

    const [loading, setLoading] = useState<boolean>(true);
    const [allMessages, setAllMessages] = useState<IMessage[] | null>(null);
    const [message, setMessage] = useState<string>("");

    const { register, handleSubmit } = useForm({
        mode: "onBlur",
    });

    const user = useAppSelector(selectAuthInfo);

    const fetchMessages = async () => {
        const response = await ChatsService.getMessages(chatId);
        return response.data.messages;
    };

    const handleDeleteMessage = useCallback((messageId: string) => {
        const updatedMessages = removeMessage(messageId, allMessages || []);
        setAllMessages(updatedMessages || null);
        connection.invoke("RemoveMessage", messageId);
    }, []);

    const handleSendMessage = () => {
        console.log("submit");
        connection.invoke("SendMessage", chatId, message);
    };

    const handleAddMessage = (data: string) => {
        const response: IMessage = JSON.parse(data);
        console.log([...(allMessages || []), response]);
        setAllMessages([...(allMessages || []), response]);
    };

    const handleRemovedMessage = (id: string) => {
        const updatedMessages = removeMessage(id, allMessages || []);
        setAllMessages(updatedMessages || null);
    };

    useEffect(() => {
        fetchMessages().then((res) => setAllMessages(res));

        connection.start().then(() => connection.invoke("StartReceivingMessages", chatId));
        connection.on("e:messages:new", handleAddMessage);
        connection.on("e:messages:removed", handleRemovedMessage);
        connection.on("e:error", (message) => console.log(message));
        return () => {
            connection.off("e:messages:new");
            connection.off("e:messages:removed");
            connection.off("e:error");
            connection.invoke("EndReceivingMessages", chatId);
        };
    }, []);

    return (
        <section className={s.message}>
            <div className="container">
                <div className={s.message__inner}>
                    <div className={s.message__box}>
                        {allMessages && allMessages.length > 0 ? (
                            allMessages?.map((message: IMessage) => {
                                let myMessage: boolean = false;
                                if (message.from.id === user.user.id) {
                                    myMessage = true;
                                }
                                return (
                                    <MessageItem
                                        key={message.id}
                                        id={message.id}
                                        title={message.text}
                                        avatar={message.from.avatar}
                                        data={message.createdAd}
                                        myMessage={myMessage}
                                        deleteMessage={handleDeleteMessage}
                                    />
                                );
                            })
                        ) : (
                            <h3 className={s.message__chat_title}>Write the first message &#128521;</h3>
                        )}
                    </div>
                    <form className={s.message__form} onSubmit={handleSubmit(handleSendMessage)}>
                        <textarea
                            {...register("textarea", {
                                required: "Required field",
                            })}
                            value={message}
                            onChange={(e) => {
                                setMessage(e.target.value);
                            }}
                            className={s.message__textarea}
                            rows={5}
                        ></textarea>
                        <button className={s.message__btn} type="submit">
                            Submit
                        </button>
                    </form>
                </div>
            </div>
        </section>
    );
};

export default MessagePage;
