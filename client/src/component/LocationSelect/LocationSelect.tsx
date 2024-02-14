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
}

const fetchLocationList = async () => {
    const LocationList = await LocationService.getLocationList();
    const options = LocationList.data.map(
        (item: IGetLocationList, index: number) => {
            return {
                value: index.toString(),
                label: item.label,
            };
        },
    );
    return options;
};

const LocationSelect: React.FC<LocationSelectProps> = ({ setValue, value }) => {
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
        />
    );
};

export default LocationSelect;
