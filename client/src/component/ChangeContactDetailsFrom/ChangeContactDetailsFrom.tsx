import { useState } from "react";
import Select from "react-select";
import Input from "../Input/Input";
import { useForm } from "react-hook-form";
import s from "./ChangeContactDetailsFrom.module.scss";
import "../../shared/assets/styles/formSelect.scss";
import { useAppDispatch } from "../../shared/hooks/storeHooks";
import { changeDetails } from "../../store/slices/SettingsSlice";
import toast, { Toaster } from "react-hot-toast";

const options = [
    { value: "chocolate", label: "Chocolate" },
    { value: "strawberry", label: "Strawberry" },
    { value: "vanilla", label: "Vanilla" },
    { value: "chocolate", label: "Chocolate" },
    { value: "strawberry", label: "Strawberry" },
    { value: "vanilla", label: "Vanilla" },
    { value: "chocolate", label: "Chocolate" },
    { value: "strawberry", label: "Strawberry" },
    { value: "vanilla", label: "Vanilla" },
    { value: "chocolate", label: "Chocolate" },
    { value: "strawberry", label: "Strawberry" },
    { value: "vanilla", label: "Vanilla" },
    { value: "chocolate", label: "Chocolate" },
    { value: "strawberry", label: "Strawberry" },
    { value: "vanilla", label: "Vanilla" },
];

interface ILocation {
    value: string;
    label: string;
}

const ChangeContactDetailsFrom = () => {
    const [location, setLocation] = useState<ILocation | null>(null);
    const [name, setName] = useState<string>("");
    const [isClearable, setIsClearable] = useState(true);

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm({
        mode: "onBlur",
    });

    const dispatch = useAppDispatch();

    const Submit = async () => {
        try {
            const locationLabel = location?.label || "";
            const response = await dispatch(
                changeDetails({ locationLabel, name }),
            );
            if (response.payload === 200) {
                toast.success("Changes saved");
            } else {
                toast.error("The modified data could not be saved");
            }
        } catch (error) {
            console.log(error);
        }
    };

    return (
        <>
            <form className={s.form} onSubmit={handleSubmit(Submit)}>
                <label className={s.form__columns}>
                    <span>Choose your location</span>
                    <Select
                        classNamePrefix="form-select"
                        options={options}
                        onChange={(selectedOptions) =>
                            setLocation(selectedOptions)
                        }
                        value={location}
                        placeholder={"Choose your location"}
                        isClearable={isClearable}
                    />
                </label>
                <div className={s.form__line}></div>
                <label className={s.form__columns}>
                    <Input
                        registerName="name"
                        text="Contact person"
                        type="text"
                        register={register}
                        errors={errors}
                        value={name}
                        setValue={setName}
                        validationOptions={{
                            maxLength: {
                                value: 100,
                                message:
                                    "The maximum length of the string should not exceed 100 characters",
                            },
                        }}
                    />
                </label>
                <button className={s.form__btn} type="submit" data-cy="submit">
                    Save Changes
                </button>
            </form>
            <Toaster position="bottom-left" reverseOrder={false} />
        </>
    );
};

export default ChangeContactDetailsFrom;
