import s from "./HorizontalList.module.scss";
import { Advert, AdvertResponse } from "../../types/response/AdvertResponse";
import HorizontalListItem from "../HorizontalListItem/HorizontalListItem";
import { useEffect, useState } from "react";

interface HorizontalListProprs {
    title: string;
    list: AdvertResponse | null;
    setActivePage: (value: number) => void;
}

interface ILike {
    id: string;
}

const HorizontalList: React.FC<HorizontalListProprs> = ({ title, list, setActivePage }) => {
    const [likes, setLikes] = useState<ILike[]>([]);

    useEffect(() => {
        const localLikes = JSON.parse(localStorage.getItem("favorite") || "[]");
        setLikes(localLikes);
    }, [list]);

    return (
        <section className={s.list}>
            <div className={s.list__inner}>
                <h2 className={s.list__title}>{title}</h2>
                <div className={s.list__items}>
                    {list &&
                        list.adverts &&
                        list.adverts.map((advert: Advert) => {
                            let InitialLike = likes.some((item: ILike) => item.id === advert.id);
                            return <HorizontalListItem key={advert.id} item={advert} InitialLike={InitialLike} />;
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
