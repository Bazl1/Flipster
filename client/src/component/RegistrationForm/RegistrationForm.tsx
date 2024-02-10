import s from "./RegistrationForm.module.scss";
import { useState } from "react";
import { useForm } from "react-hook-form";
import Input from "../Input/Input";

const RegistrationForm = () => {
    const [name, setName] = useState<string>("");
    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [confirmPassword, setConfirgPassword] = useState<string>("");

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
            <Input
                text="Name*"
                type="text"
                registerName="name"
                register={register}
                value={name}
                setValue={setName}
                errors={errors}
                validationOptions={{
                    required: "Required field",
                }}
            />
            <Input
                text="Email*"
                type="email"
                registerName="email"
                register={register}
                value={email}
                setValue={setEmail}
                errors={errors}
                validationOptions={{
                    required: "Required field",
                    pattern: {
                        value: /^((([0-9A-Za-z]{1}[-0-9A-z\.]{1,}[0-9A-Za-z]{1})|([0-9А-Яа-я]{1}[-0-9А-я\.]{1,}[0-9А-Яа-я]{1}))@([-A-Za-z]{1,}\.){1,2}[-A-Za-z]{2,})$/u,
                        message: "Enter a valid email",
                    },
                }}
            />
            <Input
                text="Password*"
                type="password"
                registerName="password"
                register={register}
                value={password}
                setValue={setPassword}
                errors={errors}
                validationOptions={{
                    required: "Required field",
                    maxLength: {
                        value: 21,
                        message: "Maximum password length 21 characters",
                    },
                    minLength: {
                        value: 8,
                        message: "Minimum password length is 8 characters",
                    },
                }}
            />
            <Input
                text="Confirm Password*"
                type="password"
                registerName="confirmPassword"
                register={register}
                value={confirmPassword}
                setValue={setConfirgPassword}
                errors={errors}
                validationOptions={{
                    required: "Required field",
                    maxLength: {
                        value: 21,
                        message: "Maximum password length 21 characters",
                    },
                    minLength: {
                        value: 8,
                        message: "Minimum password length is 8 characters",
                    },
                }}
            />
            <button className={s.form__btn} type="submit">
                SIGN UP
            </button>
        </form>
    );
};

export default RegistrationForm;
