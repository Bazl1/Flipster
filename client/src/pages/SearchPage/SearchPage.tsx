import SearchBox from "../../component/SearchBox/SearchBox";
import VerticalList from "../../component/VerticalList/VerticalList";
import s from "./SearchPage.module.scss";

const SearchPage = () => {
    return (
        <>
            <section className={s.search}>
                <div className="container">
                    <div className={s.search__inner}>
                        <SearchBox />
                    </div>
                </div>
            </section>
            <section className={s.adverts}>
                <div className="container">
                    <div className={s.adverts__inner}>{/* <VerticalList title="Your search query" /> */}</div>
                </div>
            </section>
        </>
    );
};

export default SearchPage;
