import { memo, useRef, useState } from "react";
import user from "../../shared/assets/img/user.png";
import { Link } from "react-router-dom";
import { FaPencilAlt } from "react-icons/fa";
import { FaDeleteLeft } from "react-icons/fa6";
import s from "./MessageItem.module.scss";
import useOutside from "../../shared/hooks/useOutside";

interface MessageItemProps {
    id: string;
    myMessage?: boolean;
    title: string;
    avatar: string;
    data: string;
    deleteMessage: (messageId: string) => void;
}

const MessageItem = memo(({ id, title, avatar, data, deleteMessage, myMessage = false }: MessageItemProps) => {
    console.log("message item");
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
                <div ref={ref} className={s.message__item}>
                    <Link to={"/"} className={s.message__user}>
                        <img className={s.message__avatar} src={avatar !== "" ? avatar : user} alt="avatar" />
                    </Link>
                    <div className={s.message__item_box} onContextMenu={(e) => handleRightClick(e)}>
                        <p className={s.message__item_text}>{title}</p>
                        <div className={s.message__toolbar}>
                            <p className={s.message__data}>{data}</p>
                        </div>
                    </div>
                </div>
            ) : (
                <div
                    ref={ref}
                    className={
                        activeToolbar
                            ? `${s.message__item} ${s.message__item_me} ${s.message__active}`
                            : `${s.message__item} ${s.message__item_me}`
                    }
                >
                    <div className={s.message__item_box} onContextMenu={(e) => handleRightClick(e)}>
                        <p className={s.message__item_text}>{title}</p>
                        <div className={s.message__toolbar}>
                            <p className={s.message__data}>{data}</p>
                            <button className={s.message__toolbar_btn}>
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
});

export default MessageItem;
