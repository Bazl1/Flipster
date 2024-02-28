import s from "./ChatsPage.module.scss";
import user from "../../shared/assets/img/user.png";
import { MdOutlineOpenInNew } from "react-icons/md";
import { Link } from "react-router-dom";

const ChatsPage = () => {
    return (
        <section className={s.message}>
            <div className="container">
                <div className={s.message__inner}>
                    <h2 className={s.message__title}>Your chats</h2>
                    <div className={s.message__items}>
                        <div className={s.message__item}>
                            <Link to={"/"} className={s.message__user}>
                                <img className={s.message__avatar} src={user} alt="avatar" />
                                <h3 className={s.message__user_name}>Maxim Ostapenko</h3>
                            </Link>
                            <Link to={"/"} className={s.message__open}>
                                <span>Open chat</span>
                                <MdOutlineOpenInNew />
                            </Link>
                            <div className={s.message__notification}>3</div>
                        </div>
                        <div className={s.message__item}>
                            <Link to={"/"} className={s.message__user}>
                                <img className={s.message__avatar} src={user} alt="avatar" />
                                <h3 className={s.message__user_name}>Maxim Ostapenko</h3>
                            </Link>
                            <Link to={"/"} className={s.message__open}>
                                <span>Open chat</span>
                                <MdOutlineOpenInNew />
                            </Link>
                            <div className={s.message__notification}>3</div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default ChatsPage;
