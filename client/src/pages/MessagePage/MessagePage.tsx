import { useForm } from "react-hook-form";
import s from "./MessagePage.module.scss";
import { useState } from "react";
import MessageItem from "../../component/MessageItem/MessageItem";

const MessagePage = () => {
    const [message, setMessage] = useState<string>("");

    const { register, handleSubmit } = useForm({
        mode: "onBlur",
    });

    const Submit = () => {};

    return (
        <section className={s.message} onSubmit={handleSubmit(Submit)}>
            <div className="container">
                <div className={s.message__inner}>
                    <div className={s.message__box}>
                        <MessageItem />
                        <MessageItem myMessage={true} />
                        <MessageItem />
                        <MessageItem />
                        <MessageItem />
                        <MessageItem />
                        <MessageItem />
                        <MessageItem />
                        <MessageItem />
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
