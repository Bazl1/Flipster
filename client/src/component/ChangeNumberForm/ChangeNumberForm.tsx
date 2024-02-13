import { useState } from "react";
import s from "./ChangeNumberForm.module.scss";
import Input from "../Input/Input";
import { useForm } from "react-hook-form";
import { useAppDispatch } from "../../shared/hooks/storeHooks";
import { changeNumber } from "../../store/slices/SettingsSlice";
import toast, { Toaster } from "react-hot-toast";

const ChangeNumberForm = () => {
    const [number, setNumber] = useState<string>("");

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
            const response = await dispatch(changeNumber({ number }));
            if (response.payload === 200) {
                toast.success("Changes saved");
            } else {
                toast.error("A user with this number already exists");
            }
        } catch (error) {
            console.log(error);
        }
    };

    return (
        <>
            <form className={s.form} onSubmit={handleSubmit(Submit)}>
                <label className={s.form__columns}>
                    <Input
                        text="Enter the number"
                        type="text"
                        registerName="phone"
                        register={register}
                        errors={errors}
                        value={number}
                        setValue={setNumber}
                        validationOptions={{
                            required: "Required field",
                            pattern: {
                                value: /\+380\s*\d{2}\s*\d{3}\s*\d{4}/,
                                message:
                                    "Please enter a valid phone number in the format +380 00 000 0000.",
                            },
                            maxLength: {
                                value: 13,
                                message:
                                    "The maximum length of the phone number is 13 characters",
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

export default ChangeNumberForm;
