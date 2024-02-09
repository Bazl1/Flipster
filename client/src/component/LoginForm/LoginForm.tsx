import { useState } from "react";
import Input from "../Input/Input";
import s from "./LoginForm.module.scss";
import { useForm } from "react-hook-form";

const LoginForm = () => {
    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");

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
                text={"Email*"}
                registerName={"email"}
                type={"email"}
                value={email}
                setValue={setEmail}
                register={register}
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
                text={"Password*"}
                registerName="password"
                type="password"
                value={password}
                setValue={setPassword}
                register={register}
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
                sign in
            </button>
        </form>
    );
};

export default LoginForm;
