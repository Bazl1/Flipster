import s from "./PublicProfile.module.scss";
import user from "../../shared/assets/img/user.png";
import { useState } from "react";
import { Link } from "react-router-dom";
import VerticalList from "../../component/VerticalList/VerticalList";

const PublicProfile = () => {
    const [show, setShow] = useState<boolean>(false);
    return (
        <section className={s.profile}>
            <div className="container">
                <div className={s.profile__inner}>
                    <div className={s.profile__top}>
                        <div className={s.profile__user}>
                            <img className={s.profile__avatar} src={user} alt="avatar" />
                            <h3 className={s.profile__name}>Maxim Ostapenko</h3>
                        </div>
                        {!show ? (
                            <button onClick={() => setShow((current) => !current)} className={s.profile__show_number}>
                                Show number
                            </button>
                        ) : (
                            <Link to={"tel:+380632805354"} className={s.profile__number}>
                                +380 63 280 5354
                            </Link>
                        )}
                    </div>
                    <div className={s.profile__adverts}>
                        <VerticalList title="Adverts" />
                    </div>
                </div>
            </div>
        </section>
    );
};

export default PublicProfile;
