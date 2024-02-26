import { useEffect, useState } from "react";
import SearchBox from "../../component/SearchBox/SearchBox";
import VerticalList from "../../component/VerticalList/VerticalList";
import s from "./SearchPage.module.scss";
import { AdvertResponse } from "../../types/response/AdvertResponse";
import { useLazyGetAdvertsSearchQuery } from "../../services/AdvertService";
import { useParams } from "react-router-dom";

export interface ISearchParams {
    query: string;
    categoryId: string;
    location: string;
    min: string;
    max: string;
    sort: string;
}

const SearchPage = () => {
    const { search, location } = useParams();
    const searchStr = search || "";
    const locationStr = location || "";

    const [data, setData] = useState<AdvertResponse | null>(null);
    const [activePage, setActivePage] = useState<number>(1);

    const [searchQuery, { data: searchData }] = useLazyGetAdvertsSearchQuery();

    const handleSearch = async (searchParams: ISearchParams) => {
        searchQuery({
            limit: 12,
            page: activePage,
            ...searchParams,
        });
    };

    useEffect(() => {
        searchQuery({
            limit: 12,
            page: activePage,
            query: searchStr,
            location: locationStr,
        });
    }, []);

    useEffect(() => {
        setData(searchData || null);
    }, [searchData]);
    return (
        <>
            <section className={s.search}>
                <div className="container">
                    <div className={s.search__inner}>
                        <SearchBox onSearch={handleSearch} />
                    </div>
                </div>
            </section>
            <section className={s.adverts}>
                <div className="container">
                    <div className={s.adverts__inner}>
                        {data && data.adverts.length > 0 ? (
                            <VerticalList
                                title="Your search query"
                                list={data}
                                activePage={activePage}
                                setActivePage={setActivePage}
                            />
                        ) : (
                            <h3 className={s.adverts__search_title}>Nothing was found for your request</h3>
                        )}
                    </div>
                </div>
            </section>
        </>
    );
};

export default SearchPage;
