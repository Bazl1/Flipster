import { useEffect, useState } from "react";
import LocationService from "../../services/LocationService";
import Select from "react-select";
import { IGetLocationList, ILocationList } from "../LocationSelect/LocationSelect";
import CategoriesService from "../../services/CategoriesService";
import { ICategory } from "../../types/response/CategoryResponse";
import { CategoriesList } from "../CategoriesSelect/CategoriesSelect";
import "./Select.scss";
import s from "./SearchBox.module.scss";

const fetchLocationList = async () => {
    const LocationList = await LocationService.getLocationList();
    const options = LocationList.data.map((item: IGetLocationList, index: number) => {
        return {
            value: index.toString(),
            label: item.label,
        };
    });
    return options;
};

const getCategoriesList = async () => {
    const response = await CategoriesService.getCategorys();
    const options = response.data.map((item: ICategory) => {
        return {
            value: item.id,
            label: item.title,
        };
    });
    return options;
};

const SearchBox = () => {
    const [locationList, setLocationList] = useState<ILocationList[]>([]);
    const [categoriesList, setCategoriesList] = useState<CategoriesList[]>([]);

    const [category, setCategory] = useState<CategoriesList | null>(null);
    const [location, setLocation] = useState<ILocationList | null>(null);
    const [sorting, setSorting] = useState<{ value: string; label: string } | null>(null);
    const [isClearable, setIsClearable] = useState(true);

    const Submit = async () => {};

    useEffect(() => {
        fetchLocationList().then((res) => setLocationList(res));
        getCategoriesList().then((res) => setCategoriesList(res));
    }, []);

    return (
        <form className={s.search}>
            <div className={s.search__row}>
                <label className={s.search__columns}>
                    <input className={s.search__input_big} type="text" placeholder="Enter your search query" />
                </label>
                <label className={s.search__columns}>
                    <Select
                        classNamePrefix="form-select-big"
                        options={locationList}
                        onChange={(selectedOptions) => setLocation(selectedOptions)}
                        value={location}
                        placeholder={"Select location"}
                        isClearable={isClearable}
                    />
                </label>
                <button className={s.search__btn} type="submit">
                    Search
                </button>
            </div>
            <div className={s.search__row}>
                <label className={`${s.search__columns} ${s.search__columns_flex}`}>
                    <input className={s.search__input} type="number" placeholder="Min $" />
                    <input className={s.search__input} type="number" placeholder="Max $" />
                </label>
                <label className={s.search__columns}>
                    <Select
                        classNamePrefix="form-select"
                        options={categoriesList}
                        onChange={(selectedOptions) => setCategory(selectedOptions)}
                        value={category}
                        placeholder={"Select a category"}
                        isClearable={isClearable}
                    />
                </label>
                <label className={`${s.search__columns} ${s.search__columns_small}`}>
                    <Select
                        classNamePrefix="form-select"
                        options={[
                            {
                                value: "Popular-first",
                                label: "Popular first",
                            },
                            {
                                value: "Cheap-ones-first",
                                label: "Cheap ones first",
                            },
                            {
                                value: "Dear-ones-first",
                                label: "Dear ones first",
                            },
                        ]}
                        onChange={(selectedOptions) => setSorting(selectedOptions)}
                        value={sorting}
                        placeholder={"Price Range"}
                        isClearable={isClearable}
                    />
                </label>
            </div>
        </form>
    );
};

export default SearchBox;
