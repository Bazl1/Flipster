import { useState } from "react";
import Input from "../Input/Input";
import s from "./ChangePasswordForm.module.scss";
import { useForm } from "react-hook-form";

const ChangePasswordForm = () => {
    const [oldPassword, setOldPassword] = useState<string>("");
    const [newPassword, setNewPassword] = useState<string>("");
    const [confirmPassword, setConfirmPassword] = useState<string>("");

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
                    text="Enter old password"
                    type="password"
                    registerName="password"
                    register={register}
                    errors={errors}
                    value={oldPassword}
                    setValue={setOldPassword}
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
            </label>
            <div className={s.form__line}></div>
            <label className={s.form__columns}>
                <Input
                    text="Enter a new password"
                    type="password"
                    registerName="newPassword"
                    register={register}
                    errors={errors}
                    value={newPassword}
                    setValue={setNewPassword}
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
            </label>
            <div className={s.form__line}></div>
            <label className={s.form__columns}>
                <Input
                    text="Confirm the password"
                    type="password"
                    registerName="newPassword"
                    register={register}
                    errors={errors}
                    value={confirmPassword}
                    setValue={setConfirmPassword}
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
            </label>
            <button className={s.form__btn} type="submit" data-cy="submit">
                Save Changes
            </button>
        </form>
    );
};

export default ChangePasswordForm;
