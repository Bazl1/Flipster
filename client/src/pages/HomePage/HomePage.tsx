import { useEffect, useState } from "react";
import Input from "../../component/Input/Input";
import s from "./HomePage.module.scss";
import { useForm } from "react-hook-form";
import { IoMdSearch } from "react-icons/io";
import LocationSelect, { ILocationList } from "../../component/LocationSelect/LocationSelect";
import HorizontalList from "../../component/HorizontalList/HorizontalList";
import CategoriesService from "../../services/CategoriesService";
import { ICategory } from "../../types/response/CategoryResponse";
import { Link, useNavigate } from "react-router-dom";
import { useGetRecommendedAdvertsQuery } from "../../services/AdvertService";
import { AdvertResponse } from "../../types/response/AdvertResponse";

const FetchCategoriesData = async () => {
    const response = await CategoriesService.getCategorys();
    return response.data;
};

const HomePage = () => {
    const [searchInput, setSearchInput] = useState<string>("");
    const [location, setLocation] = useState<ILocationList | null>(null);
    const [categories, setCategories] = useState<ICategory[]>([]);
    const [advertsList, setAdvertsList] = useState<AdvertResponse | null>(null);
    const [activePage, setActivePage] = useState<number>(1);

    const { data } = useGetRecommendedAdvertsQuery({ limit: 12, page: activePage });
    const navigate = useNavigate();

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm({
        mode: "onBlur",
    });

    const Submit = () => {
        if (searchInput && location) {
            navigate(`/search/query/${searchInput}/location/${location.label}`);
        }
        if (searchInput && !location) {
            navigate(`/search/query/${searchInput}`);
        }
        if (location && !searchInput) {
            navigate(`/search/location/${location.label}`);
        }
        if (!searchInput && !location) {
            navigate("/search/");
        }
    };

    useEffect(() => {
        FetchCategoriesData().then((res) => {
            setCategories(res);
        });
    }, []);

    useEffect(() => {
        setAdvertsList(data || null);
    }, [data]);

    return (
        <>
            <section className={s.search}>
                <div className="container">
                    <div className={s.search__inner}>
                        <h2 className={s.search__title}>Find what you need!</h2>
                        <form className={s.search__form} onSubmit={handleSubmit(Submit)}>
                            <Input
                                text=""
                                placeholder="Your search query"
                                type="text"
                                registerName="search"
                                value={searchInput}
                                setValue={setSearchInput}
                                register={register}
                                errors={errors}
                            />
                            <div className={s.search__select}>
                                <LocationSelect value={location} setValue={setLocation} />
                            </div>
                            <button className={s.search__btn} type="submit">
                                Search
                                <span>
                                    <IoMdSearch />
                                </span>
                            </button>
                        </form>
                    </div>
                </div>
            </section>
            <section className={s.categories}>
                <div className="container">
                    <div className={s.categories__inner}>
                        <h2 className={s.categories__title}>Sections on the service</h2>
                        <div className={s.categories__items}>
                            {categories &&
                                categories.map((item: ICategory) => {
                                    return (
                                        <Link
                                            to={`/search/category/${item.id}`}
                                            key={item.id}
                                            className={s.categories__item}
                                        >
                                            <img className={s.categories__img} src={item.icon} alt="img" />
                                            <h3 className={s.categories__item_title}>{item.title}</h3>
                                        </Link>
                                    );
                                })}
                        </div>
                    </div>
                </div>
            </section>
            <section className={s.products}>
                <div className="container">
                    <div className="products__inner">
                        <HorizontalList title="Best Adverts" list={advertsList} setActivePage={setActivePage} />
                    </div>
                </div>
            </section>
        </>
    );
};

export default HomePage;
