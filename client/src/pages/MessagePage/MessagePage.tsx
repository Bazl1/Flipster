import { useForm } from "react-hook-form";
import s from "./MessagePage.module.scss";
import { useEffect, useState } from "react";
import MessageItem from "../../component/MessageItem/MessageItem";
import ChatsService from "../../services/ChatsService";
import { useParams } from "react-router-dom";
import { IMessage } from "../../types/IMessage";
import { useAppSelector } from "../../shared/hooks/storeHooks";
import { selectAuthInfo } from "../../store/selectors";

const MessagePage = () => {
    const { id } = useParams();
    const chatId = id || "";

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

    const Submit = () => {};

    useEffect(() => {
        fetchMessages().then((res) => setAllMessages(res));
    }, [fetchMessages]);

    return (
        <section className={s.message} onSubmit={handleSubmit(Submit)}>
            <div className="container">
                <div className={s.message__inner}>
                    <div className={s.message__box}>
                        {allMessages?.map((message: IMessage) => {
                            let myMessage: boolean = false;
                            if (message.id === user.user.id) {
                                myMessage = true;
                            }
                            return (
                                <MessageItem
                                    title={message.text}
                                    avatar={message.from.avatar}
                                    data={message.createdAd}
                                    myMessage={myMessage}
                                />
                            );
                        })}
                    </div>
                    <form className={s.message__form}>
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
