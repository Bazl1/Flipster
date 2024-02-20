import s from "./VerticalList.module.scss";
import { Advert, AdvertResponse } from "../../types/response/AdvertResponse";
import { useDeleteAdvertMutation } from "../../services/AdvertService";
import toast from "react-hot-toast";
import VerticalListItem from "../VerticalListItem/VerticalListItem";
import { useEffect, useState } from "react";

interface VerticalListProps {
    title: string;
    list: AdvertResponse | null;
    setActivePage: (value: number) => void;
    activePage: number;
    changes?: boolean;
}

interface ILike {
    id: string;
}

const VerticalList: React.FC<VerticalListProps> = ({ title, list, setActivePage, activePage, changes = false }) => {
    const [deleteAdvert, { isError }] = useDeleteAdvertMutation();
    const [likes, setLikes] = useState<ILike[]>([]);

    const handleRemoveAdvert = async (e: any, id: string) => {
        e.preventDefault();
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

    useEffect(() => {
        const localLikes = JSON.parse(localStorage.getItem("favorite") || "[]");
        setLikes(localLikes);
    }, []);

    return (
        <div className={s.list}>
            <h2 className={s.list__title}>{title}</h2>
            <div className={s.list__items}>
                {list &&
                    list.adverts &&
                    list.adverts.map((advert: Advert) => {
                        let InitialLike = likes.some((item: ILike) => item.id === advert.id);

                        return (
                            <VerticalListItem
                                key={advert.id}
                                item={advert}
                                changes={changes}
                                InitialLike={InitialLike}
                                handleRemoveAdvert={handleRemoveAdvert}
                            />
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
