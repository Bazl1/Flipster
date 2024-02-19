import s from "./HorizontalList.module.scss";
import { IoMdHeart } from "react-icons/io";
import { Advert, AdvertResponse } from "../../types/response/AdvertResponse";
import { Link } from "react-router-dom";

interface HorizontalListProprs {
    title: string;
    list: AdvertResponse | null;
    setActivePage: (value: number) => void;
}

const HorizontalList: React.FC<HorizontalListProprs> = ({ title, list, setActivePage }) => {
    return (
        <section className={s.list}>
            <div className={s.list__inner}>
                <h2 className={s.list__title}>{title}</h2>
                <div className={s.list__items}>
                    {list &&
                        list.adverts &&
                        list.adverts.map((item: Advert) => {
                            return (
                                <Link to={`/adverts/${item.id}`} key={item.id} className={s.list__item}>
                                    <img className={s.list__item_img} src={item.images[0]} alt="img" />
                                    <div className={s.list__item_box}>
                                        <label className={s.list__item_label}>
                                            <h3 className={s.list__item_title}>{item.title}</h3>
                                            <span className={s.list__item_location}>
                                                Location: {item.contact.location}
                                            </span>
                                        </label>
                                        <h4 className={s.list__item_price}>${item.price}</h4>
                                    </div>
                                    <div className={s.list__item_likebox}>
                                        <button className={s.list__item_like}>
                                            <IoMdHeart />
                                        </button>
                                    </div>
                                    {item.isFree && <div className={s.list__free}>Free</div>}
                                </Link>
                            );
                        })}
                </div>
            </div>
            {list?.pageCount && list?.pageCount !== 1 && list?.pageCount > 0 ? (
                <div className={s.list__paginations}>
                    {Array.from({ length: list?.pageCount }, (_, index) => (
                        <div key={index} className={s.list__pagination_item} onClick={() => setActivePage(index + 1)}>
                            {index + 1}
                        </div>
                    ))}
                </div>
            ) : null}
        </section>
    );
};

export default HorizontalList;
