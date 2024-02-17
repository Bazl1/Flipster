import { useEffect, useState } from "react";
import CategoriesService from "../../services/CategoriesService";
import { ICategory } from "../../types/response/CategoryResponse";
import "../../shared/assets/styles/formSelect.scss";
import Select from "react-select";

export interface CategoriesList {
    value: number;
    label: string;
}

interface CategoriesSelectProps {
    setValue: (value: CategoriesList | null) => void;
    value: CategoriesList | null;
}

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

const CategoriesSelect: React.FC<CategoriesSelectProps> = ({ value, setValue }) => {
    const [categoriesList, setCategoriesList] = useState<CategoriesList[]>([]);
    const [isClearable, setIsClearable] = useState(true);

    useEffect(() => {
        getCategoriesList().then((res) => setCategoriesList(res));
    }, []);

    return (
        <Select
            classNamePrefix="form-select"
            options={categoriesList}
            onChange={(selectedOptions) => setValue(selectedOptions)}
            value={value}
            placeholder={"Select a category"}
            isClearable={isClearable}
        />
    );
};

export default CategoriesSelect;
