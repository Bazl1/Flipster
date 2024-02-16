import s from "./HorizontalList.module.scss";
import demo from "../../shared/assets/img/login-banner.jpeg";
import { IoMdHeart } from "react-icons/io";

const HorizontalList = () => {
    return (
        <section className={s.list}>
            <div className={s.list__inner}>
                <h2 className={s.list__title}>JUST FOR YOU</h2>
                <div className={s.list__items}>
                    <div className={s.list__item}>
                        <img className={s.list__item_img} src={demo} alt="img" />
                        <div className={s.list__item_box}>
                            <label className={s.list__item_label}>
                                <h3 className={s.list__item_title}>Demo title</h3>
                                <span className={s.list__item_location}>Location: UA, ZP reg, ZP</span>
                            </label>
                            <h4 className={s.list__item_price}>$30</h4>
                        </div>
                        <div className={s.list__item_likebox}>
                            <button className={s.list__item_like}>
                                <IoMdHeart />
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default HorizontalList;
