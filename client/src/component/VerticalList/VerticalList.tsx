import s from "./VerticalList.module.scss";
import { IoMdHeart } from "react-icons/io";
import { FaPenToSquare } from "react-icons/fa6";
import { IoTrashBinSharp } from "react-icons/io5";
import { Advert, AdvertResponse } from "../../types/response/AdvertResponse";
import { useDeleteAdvertMutation } from "../../services/AdvertService";
import toast from "react-hot-toast";
import { Link } from "react-router-dom";

interface VerticalListProps {
    title: string;
    list: AdvertResponse | null;
    setActivePage: (value: number) => void;
    activePage: number;
    changes?: boolean;
}

const VerticalList: React.FC<VerticalListProps> = ({ title, list, setActivePage, activePage, changes = false }) => {
    const [deleteAdvert, { isError }] = useDeleteAdvertMutation();

    const handleRemoveAdvert = async (id: string) => {
        if (list?.adverts.length === 1 && activePage > 1) {
            setActivePage(activePage - 1);
        }

        await deleteAdvert({ id });
        if (!isError) {
            toast.success("Successful removal");
        } else {
            toast.error("Deletion error");
        }
    };

    return (
        <div className={s.list}>
            <h2 className={s.list__title}>{title}</h2>
            <div className={s.list__items}>
                {list &&
                    list.adverts &&
                    list.adverts.map((item: Advert) => {
                        return (
                            <div key={item.id} className={s.list__item}>
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
                                                    <Link to={`/change-advert/${item.id}`} className={s.list__item_btn}>
                                                        <FaPenToSquare />
                                                    </Link>
                                                    <button
                                                        onClick={() => handleRemoveAdvert(item.id)}
                                                        className={s.list__item_btn}
                                                    >
                                                        <IoTrashBinSharp />
                                                    </button>
                                                </div>
                                            ) : (
                                                <button className={s.list__item_btn}>
                                                    <IoMdHeart />
                                                </button>
                                            )}
                                            <p className={s.list__item_price}>
                                                Price: <span>${item.price}</span>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                {item.isFree && <div className={s.list__item_free}>Free</div>}
                            </div>
                        );
                    })}
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
        </div>
    );
};

export default VerticalList;
