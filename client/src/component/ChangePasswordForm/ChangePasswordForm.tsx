import { useState } from "react";
import Input from "../Input/Input";
import s from "./ChangePasswordForm.module.scss";
import { useForm } from "react-hook-form";
import { useAppDispatch } from "../../shared/hooks/storeHooks";
import { changePassword } from "../../store/slices/SettingsSlice";
import toast, { Toaster } from "react-hot-toast";

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

    const dispatch = useAppDispatch();

    const Submit = async () => {
        if (newPassword === confirmPassword) {
            try {
                const response = await dispatch(
                    changePassword({ oldPassword, newPassword }),
                );
                if (response.payload === 200) {
                    toast.success("Changes saved");
                } else {
                    toast.error("Incorrect current password ");
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
                <label className={s.form__columns}>
                    <Input
                        text="Enter the current password"
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
                                message:
                                    "Maximum password length 21 characters",
                            },
                            minLength: {
                                value: 8,
                                message:
                                    "Minimum password length is 8 characters",
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
                                message:
                                    "Maximum password length 21 characters",
                            },
                            minLength: {
                                value: 8,
                                message:
                                    "Minimum password length is 8 characters",
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
                                message:
                                    "Maximum password length 21 characters",
                            },
                            minLength: {
                                value: 8,
                                message:
                                    "Minimum password length is 8 characters",
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

export default ChangePasswordForm;
