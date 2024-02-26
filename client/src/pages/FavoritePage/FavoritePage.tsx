import { useState } from "react";
import VerticalList from "../../component/VerticalList/VerticalList";
import s from "./FavoritePage.module.scss";
import { useGetFavoritesQuery } from "../../services/FavoriteAdvertService";

const FavoritePage = () => {
    const [activePage, setActivePage] = useState<number>(1);

    const { data } = useGetFavoritesQuery({ page: activePage, limit: 12 });

    return (
        <section className={s.favorite}>
            <div className="container">
                <div className={s.favorite__inner}>
                    <VerticalList
                        title={"My favorite ads"}
                        list={data || null}
                        setActivePage={setActivePage}
                        activePage={activePage}
                    />
                </div>
            </div>
        </section>
    );
};

export default FavoritePage;
