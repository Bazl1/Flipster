import { Link } from "react-router-dom";
import s from "./HorizontalListItem.module.scss";
import { Advert } from "../../types/response/AdvertResponse";
import { RiDislikeFill } from "react-icons/ri";
import { IoMdHeart } from "react-icons/io";
import { useState } from "react";
import useFavorite from "../../shared/hooks/useFavorite";
import { FaEye } from "react-icons/fa";

interface HorizontalListItemProps {
    item: Advert;
    InitialLike: boolean;
}

const HorizontalListItem: React.FC<HorizontalListItemProps> = ({ item, InitialLike }) => {
    const [like, setLike] = useState<boolean>(InitialLike);

    const toggleFavorite = useFavorite(item.id);

    return (
        <Link to={`/advert/${item.id}`} key={item.id} className={s.list__item}>
            <img className={s.list__item_img} src={item.images[0]} alt="img" />
            <div className={s.list__item_box}>
                <label className={s.list__item_label}>
                    <h3 className={s.list__item_title}>{item.title}</h3>

                    <span className={s.list__item_location}>Location: {item.contact.location}</span>
                    <div className={s.list__item_view}>
                        {item.views}
                        <span>
                            <FaEye />
                        </span>
                    </div>
                </label>
                <h4 className={s.list__item_price}>${item.price}</h4>
            </div>
            <div className={s.list__item_likebox}>
                <button
                    onClick={(e) => {
                        e.preventDefault();
                        setLike(!like);
                        toggleFavorite();
                    }}
                    className={s.list__item_like}
                >
                    {!like ? <IoMdHeart /> : <RiDislikeFill />}
                </button>
            </div>
            {item.isFree && <div className={s.list__free}>Free</div>}
        </Link>
    );
};

export default HorizontalListItem;
