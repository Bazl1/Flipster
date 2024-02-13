import s from "./VerticalList.module.scss";
import { IoMdHeart } from "react-icons/io";
import demo from "../../shared/assets/img/login-banner.jpeg";

const VerticalList = () => {
    return (
        <div className={s.list}>
            <h2 className={s.list__title}>Adverts</h2>
            <div className={s.list__items}>
                <div className={s.list__item}>
                    <div className={s.list__item_row}>
                        <div className={s.list__item_columns}>
                            <img
                                className={s.list__item_img}
                                src={demo}
                                alt="img"
                            />
                        </div>
                        <div className={s.list__item_columns}>
                            <div className={s.list__item_box}>
                                <h3 className={s.list__item_title}>
                                    Demo title
                                </h3>
                                <p className={s.list__item_location}>
                                    Location: Uktaine, Kyev obls, Kyev
                                </p>
                            </div>
                            <div className={s.list__item_box}>
                                <button className={s.list__item_like}>
                                    <IoMdHeart />
                                </button>
                                <p className={s.list__item_price}>
                                    Price: <span>$100</span>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className={s.list__paginations}>
                <div className={s.list__pagination_item}>1</div>
                <div className={s.list__pagination_item}>2</div>
                <div className={s.list__pagination_item}>10</div>
            </div>
        </div>
    );
};

export default VerticalList;
