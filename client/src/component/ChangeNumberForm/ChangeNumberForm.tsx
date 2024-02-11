import { useState } from "react";
import s from "./ChangeNumberForm.module.scss";
import Input from "../Input/Input";
import { useForm } from "react-hook-form";

const ChangeNumberForm = () => {
    const [number, setNumber] = useState<string>("");

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm({
        mode: "onBlur",
    });

    const Submit = async () => {};

    return (
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
                    }}
                />
            </label>
            <button className={s.form__btn} type="submit" data-cy="submit">
                Save Changes
            </button>
        </form>
    );
};

export default ChangeNumberForm;
