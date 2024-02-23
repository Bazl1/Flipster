import { useRef, useState } from "react";
import s from "./ChangeAvatarForm.module.scss";
import { FaUserTie } from "react-icons/fa";
import { MdFileUpload } from "react-icons/md";
import { useAppDispatch } from "../../shared/hooks/storeHooks";
import { changeAvatar } from "../../store/slices/SettingsSlice";
import toast, { Toaster } from "react-hot-toast";

const ChangeAvatarForm = () => {
    const [imgUrl, setImgUrl] = useState<any>(null);
    const refImg = useRef<HTMLImageElement | null>(null);

    const dispatch = useAppDispatch();

    const handleUploadImg = (e: React.ChangeEvent<HTMLInputElement>) => {
        const input = e.target;
        if (input.files && input.files[0]) {
            const maxSize = 2 * 1024 * 1024;
            if (input.files[0].size > maxSize) {
                toast.error("Maximum image size 2mb");
            } else {
                const reader = new FileReader();
                reader.onload = function (e) {
                    if (refImg.current) {
                        refImg.current.src = e.target?.result as string;
                    }
                };
                reader.readAsDataURL(input.files[0]);
                setImgUrl(input.files[0]);
            }
        }
    };

    const Submit = async (e: any) => {
        e.preventDefault();
        try {
            const response = await dispatch(changeAvatar({ imgUrl }));
            if (response.payload === 200) {
                toast.success("Your avatar has been successfully changed");
            } else {
                toast.error("An error occurred while loading the image");
            }
        } catch (error) {
            console.log(error);
        }
    };

    return (
        <>
            <form className={s.form} onSubmit={Submit}>
                <div className={s.form__row}>
                    <div className={s.form__columns}>
                        <label className={s.form__input_box}>
                            <p className={s.form__text}>
                                Attach an image in JPG, PNG format.
                                <br />
                                Maximum size 800 KB.
                            </p>
                            <span>
                                <MdFileUpload />
                                <p className={s.form__text}>Upload a photo</p>
                            </span>
                            <input
                                className={s.form__file_input}
                                type="file"
                                onChange={handleUploadImg}
                                accept="image/png, image/jpeg"
                                data-cy="select-avatar"
                            />
                        </label>
                    </div>
                    <div className={s.form__columns}>
                        {imgUrl === null ? (
                            <div className={s.form__skeleton}>
                                <FaUserTie />
                            </div>
                        ) : (
                            <img ref={refImg} className={s.form__avatar} src={imgUrl} alt="avatar" />
                        )}
                    </div>
                </div>
                <button className={s.form__btn} type="submit" data-cy="submit">
                    Save Changes
                </button>
            </form>
            <Toaster position="bottom-left" reverseOrder={false} />
        </>
    );
};

export default ChangeAvatarForm;
