import s from "./ChatsPage.module.scss";
import user from "../../shared/assets/img/user.png";
import { MdOutlineOpenInNew } from "react-icons/md";
import { MdDelete } from "react-icons/md";
import { FaEye } from "react-icons/fa";
import { Link } from "react-router-dom";

const ChatsPage = () => {
    return (
        <section className={s.message}>
            <div className="container">
                <div className={s.message__inner}>
                    <h2 className={s.message__title}>Your chats</h2>
                    <div className={s.message__items}>
                        <div className={s.message__item}>
                            <div className={s.message__item_box}>
                                <h3 className={s.message__item_title}>Title demo</h3>
                                <Link to={"/"} className={s.message__user}>
                                    <img className={s.message__avatar} src={user} alt="avatar" />
                                    <p className={s.message__user_name}>Maxim Ostapenko</p>
                                </Link>
                            </div>
                            <Link to={"/"} className={s.message__open}>
                                <span>Open chat</span>
                                <MdOutlineOpenInNew />
                            </Link>
                            <button className={s.message__delete}>
                                <MdDelete />
                            </button>
                            <div className={s.message__notification}>
                                3
                                <span>
                                    <FaEye />
                                </span>
                            </div>
                        </div>
                        <div className={s.message__item}>
                            <div className={s.message__item_box}>
                                <h3 className={s.message__item_title}>Title demo</h3>
                                <Link to={"/"} className={s.message__user}>
                                    <img className={s.message__avatar} src={user} alt="avatar" />
                                    <p className={s.message__user_name}>Maxim Ostapenko</p>
                                </Link>
                            </div>
                            <Link to={"/"} className={s.message__open}>
                                <span>Open chat</span>
                                <MdOutlineOpenInNew />
                            </Link>
                            <button className={s.message__delete}>
                                <MdDelete />
                            </button>
                            <div className={s.message__notification}>
                                5
                                <span>
                                    <FaEye />
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default ChatsPage;
