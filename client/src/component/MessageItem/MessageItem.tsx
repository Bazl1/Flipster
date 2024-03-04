import { memo, useRef, useState } from "react";
import user from "../../shared/assets/img/user.png";
import { Link } from "react-router-dom";
import { FaPencilAlt } from "react-icons/fa";
import { FaDeleteLeft } from "react-icons/fa6";
import s from "./MessageItem.module.scss";
import useOutside from "../../shared/hooks/useOutside";
import { IoCheckmarkSharp, IoCheckmarkDoneSharp } from "react-icons/io5";

interface MessageItemProps {
    id: string;
    myMessage?: boolean;
    title: string;
    avatar: string;
    data: string;
    deleteMessage: (messageId: string) => void;
    changeMessage: (messageId: string, text: string) => void;
    isDeleted: boolean;
    isRead: boolean;
}

const MessageItem = memo(
    ({
        id,
        title,
        avatar,
        data,
        deleteMessage,
        changeMessage,
        myMessage = false,
        isDeleted,
        isRead,
    }: MessageItemProps) => {
        const [activeToolbar, setActiveToolbar] = useState<boolean>(false);
        const ref = useRef<HTMLDivElement>(null);

        useOutside({ ref, setValue: setActiveToolbar, value: activeToolbar });

        const handleRightClick = (e: any) => {
            e.preventDefault();
            setActiveToolbar(true);
        };

        return (
            <>
                {!myMessage ? (
                    <div
                        ref={ref}
                        className={isDeleted ? `${s.message__item} ${s.message__item_delete}` : `${s.message__item}`}
                    >
                        <Link to={"/"} className={s.message__user}>
                            <img className={s.message__avatar} src={avatar !== "" ? avatar : user} alt="avatar" />
                        </Link>
                        <div className={s.message__item_box}>
                            <p className={s.message__item_text}>{title}</p>
                            <div className={s.message__toolbar}>
                                <p className={s.message__data}>{data}</p>
                                <div className={s.message__read}>
                                    {!isRead ? <IoCheckmarkSharp /> : <IoCheckmarkDoneSharp />}
                                </div>
                            </div>
                        </div>
                    </div>
                ) : (
                    <div
                        ref={ref}
                        className={
                            isDeleted
                                ? `${s.message__item} ${s.message__item_me} ${s.message__item_delete}`
                                : activeToolbar
                                  ? `${s.message__item} ${s.message__item_me} ${s.message__active}`
                                  : `${s.message__item} ${s.message__item_me}`
                        }
                    >
                        <div className={s.message__item_box} onContextMenu={(e) => handleRightClick(e)}>
                            <p className={s.message__item_text}>{title}</p>
                            <div className={s.message__toolbar}>
                                <div className={s.message__read}>
                                    {!isRead ? <IoCheckmarkSharp /> : <IoCheckmarkDoneSharp />}
                                </div>
                                <p className={s.message__data}>{data}</p>
                                <button onClick={() => changeMessage(id, title)} className={s.message__toolbar_btn}>
                                    <FaPencilAlt />
                                </button>
                                <button onClick={() => deleteMessage(id)} className={s.message__toolbar_btn}>
                                    <FaDeleteLeft />
                                </button>
                            </div>
                        </div>
                        <Link to={"/"} className={s.message__user}>
                            <img className={s.message__avatar} src={avatar !== "" ? avatar : user} alt="avatar" />
                        </Link>
                    </div>
                )}
            </>
        );
    },
);

export default MessageItem;
