import s from "./ChatsPage.module.scss";
import user from "../../shared/assets/img/user.png";
import { MdOutlineOpenInNew } from "react-icons/md";
import { MdDelete } from "react-icons/md";
import { FaEye } from "react-icons/fa";
import { Link } from "react-router-dom";
import { useEffect, useState } from "react";
import { IChat } from "../../types/IChat";
import ChatsService from "../../services/ChatsService";

const fetchChats = async () => {
    const response = await ChatsService.getChats();
    return response.data.chats;
};

const ChatsPage = () => {
    const [chats, setChats] = useState<IChat[] | null>(null);

    const handleDeleteChat = async (id: string) => {
        await ChatsService.deleteChat(id);
        const newChats = chats?.filter((chat: IChat) => chat.id !== id);
        setChats(newChats || null);
    };

    useEffect(() => {
        fetchChats().then((res) => setChats(res));
    }, [fetchChats]);

    return (
        <section className={s.message}>
            <div className="container">
                <div className={s.message__inner}>
                    <h2 className={s.message__title}>Your chats</h2>
                    <div className={s.message__items}>
                        {chats && chats.length > 0 ? (
                            chats.map((chat: IChat) => {
                                return (
                                    <div key={chat.id} className={s.message__item}>
                                        <div className={s.message__item_box}>
                                            <h3 className={s.message__item_title}>{chat.title}</h3>
                                            <Link to={`/profile/${chat.interlocutor.id}`} className={s.message__user}>
                                                <img
                                                    className={s.message__avatar}
                                                    src={
                                                        chat.interlocutor.avatar !== ""
                                                            ? chat.interlocutor.avatar
                                                            : user
                                                    }
                                                    alt="avatar"
                                                />
                                                <p className={s.message__user_name}>{chat.interlocutor.name}</p>
                                            </Link>
                                        </div>
                                        <Link to={`/message/${chat.id}`} className={s.message__open}>
                                            <span>Open chat</span>
                                            <MdOutlineOpenInNew />
                                        </Link>
                                        <button onClick={() => handleDeleteChat(chat.id)} className={s.message__delete}>
                                            <MdDelete />
                                        </button>
                                        <div className={s.message__notification}>
                                            {chat.unreadMessageCount}
                                            <span>
                                                <FaEye />
                                            </span>
                                        </div>
                                    </div>
                                );
                            })
                        ) : (
                            <h3 className={s.message__subtitle}>There are currently no chat rooms available</h3>
                        )}
                    </div>
                </div>
            </div>
        </section>
    );
};

export default ChatsPage;
