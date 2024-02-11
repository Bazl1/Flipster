import { useState } from "react";
import Select from "react-select";
import Input from "../Input/Input";
import { useForm } from "react-hook-form";
import s from "./ChangeContactDetailsFrom.module.scss";
import "../../shared/assets/styles/formSelect.scss";

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

const ChangeContactDetailsFrom = () => {
    const [name, setName] = useState<string>("");
    const [isClearable, setIsClearable] = useState(true);

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm({
        mode: "onBlur",
    });

    const Submit = () => {};

    return (
        <form className={s.form} onSubmit={handleSubmit(Submit)}>
            <label className={s.form__columns}>
                <span>Choose your location</span>
                <Select
                    classNamePrefix="form-select"
                    options={options}
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
    );
};

export default ChangeContactDetailsFrom;
