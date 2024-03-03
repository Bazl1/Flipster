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
import Loader from "../../component/Loader/Loader";

let connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5145/hubs/chats", {
        accessTokenFactory: () => `${localStorage.getItem("token")}`,
    })
    .build();

const updateMessage = (id: string, array: IMessage[], text: string) => {
    const updatedMessages = array?.map((item: IMessage) => {
        if (item.id === id) {
            return { ...item, text: text };
        }
        return item;
    });
    return updatedMessages;
};

const MessagePage = () => {
    const { id } = useParams();
    const chatId = id || "";

    const [loading, setLoading] = useState<boolean>(true);
    const [allMessages, setAllMessages] = useState<IMessage[]>([]);
    const [message, setMessage] = useState<string>("");

    const { register, handleSubmit } = useForm({
        mode: "onBlur",
    });

    const user = useAppSelector(selectAuthInfo);

    const fetchMessages = async () => {
        const response = await ChatsService.getMessages(chatId);
        return response.data.messages;
    };

    // const handleChangeMessage = useCallback((messageId: string, text: string) => {
    //     const updatedMessages = updateMessage(messageId, allMessages, text);
    //     setAllMessages(updatedMessages);
    // }, []);

    const handleDeleteMessage = useCallback((messageId: string) => {
        const updatedMessages = updateMessage(messageId, allMessages, "This message has been deleted.");
        setAllMessages(updatedMessages || []);
        connection.invoke("RemoveMessage", messageId);
    }, []);

    const handleSendMessage = () => {
        connection.invoke("SendMessage", chatId, message);
    };

    const handleAddMessage = (message: IMessage) => {
        console.log("render handleAddMessage");
        console.log(message);
        setAllMessages((current) => [...current, message]);
    };

    const handleRemovedMessage = (id: string) => {
        const updatedMessages = updateMessage(id, allMessages, "This message has been deleted.");
        setAllMessages(updatedMessages || []);
    };

    useEffect(() => {
        fetchMessages()
            .then((res) => setAllMessages(res))
            .then(() => setLoading(false));

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
                        {loading ? (
                            <Loader />
                        ) : allMessages && allMessages.length > 0 ? (
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
                                        data={message.createdAt}
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
