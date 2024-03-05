import { useForm } from "react-hook-form";
import s from "./MessagePage.module.scss";
import { useCallback, useEffect, useRef, useState } from "react";
import MessageItem from "../../component/MessageItem/MessageItem";
import ChatsService from "../../services/ChatsService";
import { useNavigate, useParams } from "react-router-dom";
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

const updateMessage = (id: string, array: IMessage[], text: string, isDelete = false) => {
    const updatedMessages = array.map((item: IMessage) => {
        if (item.id === id) {
            return { ...item, text: text, isDeleted: isDelete };
        }
        return item;
    });
    return updatedMessages;
};

const MessagePage = () => {
    const { id } = useParams();
    const chatId = id || "";

    const messagesEndRef = useRef<HTMLDivElement | null>(null);

    const [loading, setLoading] = useState<boolean>(true);
    const [changes, setChanges] = useState<boolean>(false);
    const [changeMessageId, setChangeMessageId] = useState<string>("");
    const [allMessages, setAllMessages] = useState<IMessage[]>([]);
    const [message, setMessage] = useState<string>("");

    const { register, handleSubmit } = useForm({
        mode: "onBlur",
    });

    const user = useAppSelector(selectAuthInfo);
    const navigate = useNavigate();

    const fetchMessages = async () => {
        const response = await ChatsService.getMessages(chatId);
        setAllMessages(response.data.messages);
    };

    const handleChangeMessage = useCallback((messageId: string, text: string) => {
        setMessage(text);
        setChanges(true);
        setChangeMessageId(messageId);
    }, []);

    const handleDeleteMessage = useCallback((messageId: string) => {
        setAllMessages((currentMessages) => {
            const updatedMessages = updateMessage(messageId, currentMessages, "This message has been deleted.", true);
            return updatedMessages;
        });
        connection.invoke("RemoveMessage", messageId);
    }, []);

    const handleChangedMessage = (messageId: string, text: string) => {
        setAllMessages((currentMessages) => {
            const updatedMessages = updateMessage(messageId, currentMessages, text);
            return updatedMessages;
        });
    };

    const handleSendMessage = () => {
        if (changes) {
            setAllMessages((currentMessages) => {
                const updatedMessages = updateMessage(changeMessageId, currentMessages, message);
                return updatedMessages;
            });
            connection.invoke("ChangeMessage", changeMessageId, message);
            setChanges(false);
            setMessage("");
        } else {
            connection.invoke("SendMessage", chatId, message);
            setMessage("");
        }
    };

    const handleAddMessage = (message: IMessage) => {
        setAllMessages((current) => [...current, message]);
    };

    const handleRemovedMessage = (id: string) => {
        setAllMessages((currentMessages) => {
            const updatedMessages = updateMessage(id, currentMessages, "This message has been deleted.", true);
            return updatedMessages;
        });
    };

    const scrollToBottom = () => {
        messagesEndRef.current?.scrollIntoView({ behavior: "smooth", block: "end" });
    };

    const handleReviewed = () => {
        setAllMessages((currentMessages) => {
            const updatedMessages = currentMessages.map((item: IMessage) => {
                return { ...item, isRead: true };
            });
            return updatedMessages;
        });
    };

    useEffect(() => {
        fetchMessages();
        connection.start().then(() => connection.invoke("StartReceivingMessages", chatId));

        connection.on("e:success", () => {
            setLoading(false);
        });
        connection.on("e:messages:new", handleAddMessage);
        connection.on("e:messages:removed", handleRemovedMessage);
        connection.on("e:messages:changed", handleChangedMessage);
        connection.on("e:messages:reviewed", handleReviewed);
        connection.on("e:error", () => {
            navigate("/");
        });
        return () => {
            connection.off("e:success");
            connection.off("e:messages:new");
            connection.off("e:messages:removed");
            connection.off("e:messages:reviewed");
            connection.off("e:error");
            connection.invoke("EndReceivingMessages", chatId);
            connection.stop();
        };
    }, []);

    useEffect(() => {
        scrollToBottom();
    }, [allMessages]);

    if (loading) {
        return <Loader />;
    }

    return (
        <section className={s.message}>
            <div className="container">
                <div className={s.message__inner}>
                    <div className={s.message__box}>
                        <div className={s.message__items}>
                            {allMessages && allMessages.length > 0 ? (
                                <>
                                    {allMessages?.map((message: IMessage) => {
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
                                                isDeleted={message.isDeleted}
                                                isRead={message.isRead}
                                                deleteMessage={handleDeleteMessage}
                                                changeMessage={handleChangeMessage}
                                            />
                                        );
                                    })}
                                    <div ref={messagesEndRef}></div>
                                </>
                            ) : (
                                <h3 className={s.message__chat_title}>Write the first message &#128521;</h3>
                            )}
                        </div>
                        <form className={s.message__form} onSubmit={handleSubmit(handleSendMessage)}>
                            <input
                                {...register("text", {
                                    required: "Required field",
                                })}
                                value={message}
                                onChange={(e) => {
                                    setMessage(e.target.value);
                                }}
                                className={s.message__textarea}
                            />
                            <button className={s.message__btn} type="submit">
                                Submit
                            </button>
                        </form>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default MessagePage;
