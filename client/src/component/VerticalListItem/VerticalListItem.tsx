import { useState } from "react";
import { IoMdHeart } from "react-icons/io";
import { FaPenToSquare } from "react-icons/fa6";
import { IoTrashBinSharp } from "react-icons/io5";
import { RiDislikeFill } from "react-icons/ri";
import { Advert } from "../../types/response/AdvertResponse";
import s from "./VerticalListItem.module.scss";
import { Link, useNavigate } from "react-router-dom";
import { FaEye } from "react-icons/fa";
import useFavorite from "../../shared/hooks/useFavorite";

interface VarticalListItemProps {
    item: Advert;
    changes: boolean;
    handleRemoveAdvert: (e: any, value: string) => void;
    InitialLike: boolean;
}

const VerticalListItem: React.FC<VarticalListItemProps> = ({ item, changes, handleRemoveAdvert, InitialLike }) => {
    const [like, setLike] = useState<boolean>(InitialLike);

    const navigate = useNavigate();
    const toggleFavorite = useFavorite(item.id);

    return (
        <Link to={`/advert/${item.id}`} key={item.id} className={s.list__item}>
            <div className={s.list__item_row}>
                <div className={s.list__item_columns}>
                    <img className={s.list__item_img} src={item.images[0]} alt="img" />
                </div>
                <div className={s.list__item_columns}>
                    <div className={s.list__item_box}>
                        <h3 className={s.list__item_title}>{item.title}</h3>
                        <p className={s.list__item_location}>Location: {item.contact.location}</p>
                    </div>
                    <div className={s.list__item_box}>
                        {changes ? (
                            <div className={s.list__item_btns}>
                                <div className={s.list__item_view}>
                                    {item.views}
                                    <span>
                                        <FaEye />
                                    </span>
                                </div>
                                <button
                                    onClick={(e) => {
                                        e.preventDefault();
                                        navigate(`/change-advert/${item.id}`);
                                    }}
                                    className={s.list__item_btn}
                                >
                                    <FaPenToSquare />
                                </button>
                                <button onClick={(e) => handleRemoveAdvert(e, item.id)} className={s.list__item_btn}>
                                    <IoTrashBinSharp />
                                </button>
                            </div>
                        ) : (
                            <div className={s.list__item_btns}>
                                <div className={s.list__item_view}>
                                    {item.views}
                                    <span>
                                        <FaEye />
                                    </span>
                                </div>

                                <button
                                    onClick={(e) => {
                                        e.preventDefault();
                                        setLike(!like);
                                        toggleFavorite();
                                    }}
                                    className={s.list__item_btn}
                                >
                                    {!like ? <IoMdHeart /> : <RiDislikeFill />}
                                </button>
                            </div>
                        )}
                        <p className={s.list__item_price}>
                            Price: <span>${item.price}</span>
                        </p>
                    </div>
                </div>
            </div>
            {item.isFree && <div className={s.list__item_free}>Free</div>}
        </Link>
    );
};

export default VerticalListItem;
