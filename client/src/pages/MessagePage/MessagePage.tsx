import { useForm } from "react-hook-form";
import Textarea from "../../component/Textarea/Textarea";
import s from "./MessagePage.module.scss";
import { useState } from "react";

const MessagePage = () => {
    const [message, setMessage] = useState<string>("");
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm({
        mode: "onBlur",
    });
    return (
        <section className={s.message}>
            <div className="container">
                <div className={s.message__inner}>
                    <div className={s.message__box}>
                        <div className={s.message__item}>
                            <p className={s.message__item_text}>Text fdsfs dsf fds fsdfdsf</p>
                        </div>
                        <div className={`${s.message__item} ${s.message__item_me}`}>
                            <p className={s.message__item_text}>Text fdsfs dsf fds fsdfdsf</p>
                        </div>
                        <div className={s.message__item}>
                            <p className={s.message__item_text}>Text fdsfs dsf fds fsdfdsf</p>
                        </div>
                        <div className={s.message__item}>
                            <p className={s.message__item_text}>Text fdsfs dsf fds fsdfdsf</p>
                        </div>
                    </div>
                    <form className={s.message__form}>
                        <Textarea
                            text="Type your message"
                            registerName="message"
                            register={register}
                            errors={errors}
                            value={message}
                            setValue={setMessage}
                            rows={6}
                        />
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
