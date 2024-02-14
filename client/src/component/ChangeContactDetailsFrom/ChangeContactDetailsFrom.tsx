import { useState } from "react";
import Input from "../Input/Input";
import { useForm } from "react-hook-form";
import s from "./ChangeContactDetailsFrom.module.scss";
import "../../shared/assets/styles/formSelect.scss";
import { useAppDispatch } from "../../shared/hooks/storeHooks";
import { changeDetails } from "../../store/slices/SettingsSlice";
import toast, { Toaster } from "react-hot-toast";
import LocationSelect, {
    ILocationList,
} from "../LocationSelect/LocationSelect";

const ChangeContactDetailsFrom = () => {
    const [location, setLocation] = useState<ILocationList | null>(null);
    const [newName, setNewName] = useState<string | null>(null);

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
            const name = newName || "";
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
                    <LocationSelect value={location} setValue={setLocation} />
                </label>
                <div className={s.form__line}></div>
                <label className={s.form__columns}>
                    <Input
                        registerName="name"
                        text="Contact person"
                        type="text"
                        register={register}
                        errors={errors}
                        value={newName || ""}
                        setValue={setNewName}
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
