import { useEffect, useState } from "react";
import LocationService from "../../services/LocationService";
import Select from "react-select";
import { IGetLocationList, ILocationList } from "../LocationSelect/LocationSelect";
import CategoriesService from "../../services/CategoriesService";
import { ICategory } from "../../types/response/CategoryResponse";
import { CategoriesList } from "../CategoriesSelect/CategoriesSelect";
import "./Select.scss";
import s from "./SearchBox.module.scss";
import { ISearchParams } from "../../pages/SearchPage/SearchPage";

interface SearchBoxProps {
    onSearch: (searchParams: ISearchParams) => void;
}

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

const SearchBox = ({ onSearch }: SearchBoxProps) => {
    const [locationList, setLocationList] = useState<ILocationList[]>([]);
    const [categoriesList, setCategoriesList] = useState<CategoriesList[]>([]);

    const [query, setQuery] = useState<string>("");
    const [category, setCategory] = useState<CategoriesList | null>(null);
    const [locationSelect, setLocationSelect] = useState<ILocationList | null>(null);
    const [sorting, setSorting] = useState<{ value: string; label: string } | null>(null);
    const [min, setMin] = useState<string | null>(null);
    const [max, setMax] = useState<string | null>(null);
    const [isClearable, setIsClearable] = useState(true);

    const Submit = async (e: any) => {
        e.preventDefault();
        const categoryId = category?.value || "null";
        const locationLabel = locationSelect?.label || "null";
        const minStr = min || "-1";
        const maxStr = max || "-1";
        onSearch({ query, categoryId, location: locationLabel, min: minStr, max: maxStr });
    };

    useEffect(() => {
        fetchLocationList().then((res) => setLocationList(res));
        getCategoriesList().then((res) => setCategoriesList(res));
    }, []);

    return (
        <form className={s.search} onSubmit={(e) => Submit(e)}>
            <div className={s.search__row}>
                <label className={s.search__columns}>
                    <input
                        className={s.search__input_big}
                        value={query}
                        onChange={(e) => setQuery(e.target.value)}
                        type="text"
                        placeholder="Enter your search query"
                    />
                </label>
                <label className={s.search__columns}>
                    <Select
                        classNamePrefix="form-select-big"
                        options={locationList}
                        onChange={(selectedOptions) => setLocationSelect(selectedOptions)}
                        value={locationSelect}
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
                    <input
                        className={s.search__input}
                        value={min || ""}
                        onChange={(e) => setMin(e.target.value)}
                        type="number"
                        placeholder="Min $"
                    />
                    <input
                        className={s.search__input}
                        value={max || ""}
                        onChange={(e) => setMax(e.target.value)}
                        type="number"
                        placeholder="Max $"
                    />
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
                                value: "PopularFirst",
                                label: "Popular first",
                            },
                            {
                                value: "CheapOnesFirst",
                                label: "Cheap ones first",
                            },
                            {
                                value: "DearOnesFirst",
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
