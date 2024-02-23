import "../../shared/assets/styles/formSelect.scss";
import Select from "react-select";
import { useEffect, useState } from "react";
import LocationService from "../../services/LocationService";

export interface IGetLocationList {
    label: string;
}

export interface ILocationList extends IGetLocationList {
    value: string;
}

interface LocationSelectProps {
    setValue: (value: ILocationList | null) => void;
    value: ILocationList | null;
    setRequired?: boolean;
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

const LocationSelect: React.FC<LocationSelectProps> = ({ setValue, value, setRequired }) => {
    const [locationList, setLocationList] = useState<ILocationList[]>([]);
    const [isClearable, setIsClearable] = useState(true);

    useEffect(() => {
        fetchLocationList().then((res) => setLocationList(res));
    }, []);

    return (
        <Select
            classNamePrefix="form-select"
            options={locationList}
            onChange={(selectedOptions) => setValue(selectedOptions)}
            value={value}
            placeholder={"Choose your location"}
            isClearable={isClearable}
            required={setRequired ? true : false}
        />
    );
};

export default LocationSelect;
