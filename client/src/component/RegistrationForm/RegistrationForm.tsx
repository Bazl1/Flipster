import s from "./RegistrationForm.module.scss";
import { useState } from "react";
import { useForm } from "react-hook-form";
import Input from "../Input/Input";
import { useAppDispatch } from "../../shared/hooks/storeHooks";
import { registration } from "../../store/slices/AuthSlice";
import { Toaster, toast } from "react-hot-toast";
import { useNavigate } from "react-router-dom";

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

    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const Submit = async () => {
        if (password === confirmPassword) {
            try {
                const action = await dispatch(
                    registration({ name, email, password }),
                );
                if (action.payload === 200) {
                    navigate("/");
                } else {
                    toast.error(
                        "A user with this e-mail address already exists",
                    );
                }
            } catch (error) {
                console.log(error);
            }
        } else {
            toast.error("Password mismatch");
        }
    };
    return (
        <>
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
                    dataСy="input-name"
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
                    dataСy="input-email"
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
                    dataСy="input-password"
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
                    dataСy="input-confirm-password"
                />
                <button className={s.form__btn} type="submit" data-cy="submit">
                    SIGN UP
                </button>
            </form>
            <Toaster position="bottom-left" reverseOrder={false} />
        </>
    );
};

export default RegistrationForm;
