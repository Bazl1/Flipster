import { useState } from "react";
import Input from "../Input/Input";
import s from "./LoginForm.module.scss";
import { useForm } from "react-hook-form";
import { useAppDispatch } from "../../shared/hooks/storeHooks";
import { useNavigate } from "react-router-dom";
import { Toaster, toast } from "react-hot-toast";
import { login } from "../../store/slices/AuthSlice";

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

    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const Submit = async () => {
        try {
            const action = await dispatch(login({ email, password }));
            if (action.payload === 200) {
                navigate("/");
            } else {
                toast.error("Incorrect email or password");
            }
        } catch (error) {
            console.log(error);
        }
    };

    return (
        <>
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
                    dataСy="input-email"
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
                    dataСy="input-password"
                />
                <button className={s.form__btn} type="submit" data-cy="submit">
                    sign in
                </button>
            </form>
            <Toaster position="bottom-left" reverseOrder={false} />
        </>
    );
};

export default LoginForm;
