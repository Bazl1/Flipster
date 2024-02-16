import { useState } from "react";
import s from "./CreateAdvert.module.scss";
import Input from "../../component/Input/Input";
import { MdFileUpload } from "react-icons/md";
import { useForm } from "react-hook-form";
import { Pagination } from "swiper/modules";
import { Swiper, SwiperSlide } from "swiper/react";
import "swiper/css";
import "swiper/css/pagination";
import Textarea from "../../component/Textarea/Textarea";
import toast, { Toaster } from "react-hot-toast";
import LocationSelect, { ILocationList } from "../../component/LocationSelect/LocationSelect";
import { useAddAvdertMutation } from "../../services/AdvertService";
import { useAppSelector } from "../../shared/hooks/storeHooks";
import { selectUserInfo } from "../../store/selectors";

const CreateAdvert = () => {
    const user = useAppSelector(selectUserInfo);

    const [title, setTitle] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [images, setImages] = useState<string[]>([]);
    const [free, setFree] = useState<boolean>(false);
    const [price, setPrice] = useState<string>("");
    const [type, setType] = useState<string>("Business");
    const [status, setStatus] = useState<string>("New");
    const [location, setLocation] = useState<ILocationList | null>(null);
    const [email, setEmail] = useState<string>(user.email);
    const [phone, setPhone] = useState<string>(user.number || "");

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm({
        mode: "onBlur",
    });

    const [addAvdert, { isSuccess }] = useAddAvdertMutation();

    const handleAddImages = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const input = e.target;
        if (input.files) {
            const maxSize = 2 * 1024 * 1024;
            if (input.files.length >= 10) {
                toast.error("The maximum number of images is 10");
                return;
            }
            for (let i = 0; i < input.files.length; i++) {
                if (input.files[i].size > maxSize) {
                    toast.error("Maximum image size 2mb");
                } else {
                    await new Promise((resolve, reject) => {
                        const reader = new FileReader();
                        reader.onload = function (e) {
                            if (images.length >= 10) {
                                toast.error("The maximum number of images is 10");
                                resolve(null);
                            } else {
                                setImages((prevImages) => [...prevImages, e.target?.result as string]);
                                resolve(null);
                            }
                        };
                        reader.onerror = reject;
                        if (input.files) {
                            reader.readAsDataURL(input.files[i]);
                        }
                    });
                }
            }
        }
    };

    const Submit = async () => {
        if (free) {
            setPrice("");
        }
        const locationLabel = location?.label || "";

        const data = new FormData();
        // data.append("Title", title);
        // data.append("Description", description);
        // data.append("IsFree", free.toString());
        // data.append("Price", price);
        // data.append("ProductType", status);
        // data.append("BusinessType", type);
        // data.append("Location", locationLabel);
        // data.append("CategoryId", "0");
        // data.append("Email", email);
        // data.append("PhoneNumber", phone);
        images.forEach((image) => {
            data.append("images", image);
        });

        await addAvdert(data);

        if (isSuccess) {
            toast.success("Product has been successfully added ");
        } else {
            toast.error("Error when adding a product");
        }
    };

    return (
        <>
            <section className={s.create}>
                <div className="container">
                    <div className={s.create__inner}>
                        <h2 className={s.create__title}>Create your advert</h2>
                        <form className={s.create__form} onSubmit={handleSubmit(Submit)}>
                            <div className={s.create__form_columns}>
                                <Input
                                    text={"Title"}
                                    registerName="title"
                                    type="text"
                                    register={register}
                                    errors={errors}
                                    value={title}
                                    setValue={setTitle}
                                    validationOptions={{
                                        required: "Required field",
                                    }}
                                />
                            </div>
                            <div className={s.create__line}></div>
                            <div className={s.create__onload_imgs}>
                                <label className={s.create__onload_box}>
                                    <p className={s.create__onload_text}>
                                        <span>
                                            <MdFileUpload />
                                        </span>
                                        Load Images
                                    </p>
                                    <input
                                        className={s.create__file}
                                        type="file"
                                        onChange={handleAddImages}
                                        accept="image/png, image/jpeg"
                                        required
                                        multiple
                                    />
                                </label>
                                {images.length > 0 ? (
                                    <Swiper
                                        className={s.create__slider}
                                        modules={[Pagination]}
                                        pagination={{ clickable: true }}
                                        spaceBetween={40}
                                        slidesPerView={3}
                                    >
                                        {images.map((item: any, index: number) => {
                                            return (
                                                <SwiperSlide key={index} className={s.create__from_slide}>
                                                    <img className={s.create__form_slide_img} src={item} alt="img" />
                                                </SwiperSlide>
                                            );
                                        })}
                                    </Swiper>
                                ) : (
                                    <div className={s.create__skeletons}>
                                        <div className={s.create__skelet}></div>
                                        <div className={s.create__skelet}></div>
                                        <div className={s.create__skelet}></div>
                                    </div>
                                )}
                            </div>
                            <div className={s.create__line}></div>
                            <div className={s.create__form_columns}>
                                <Textarea
                                    text={"Description"}
                                    registerName="description"
                                    register={register}
                                    value={description}
                                    setValue={setDescription}
                                    errors={errors}
                                    validationOptions={{
                                        required: "Required field",
                                    }}
                                />
                            </div>
                            <div className={s.create__line}></div>
                            <div className={s.create__form_columns}>
                                <div className={s.create__btns}>
                                    <button
                                        onClick={() => setFree(false)}
                                        className={
                                            !free ? `${s.create__btn} ${s.create__btn_active}` : `${s.create__btn}`
                                        }
                                        type="button"
                                    >
                                        Price
                                    </button>
                                    <button
                                        onClick={() => setFree(true)}
                                        className={
                                            free ? `${s.create__btn} ${s.create__btn_active}` : `${s.create__btn}`
                                        }
                                        type="button"
                                    >
                                        Free
                                    </button>
                                </div>
                                {!free && (
                                    <Input
                                        text={"Price $$$"}
                                        type={"number"}
                                        register={register}
                                        registerName="price"
                                        value={price}
                                        setValue={setPrice}
                                        errors={errors}
                                        validationOptions={{
                                            required: "Required field",
                                            minLeght: {
                                                value: 1,
                                                message: "Enter at least one character",
                                            },
                                        }}
                                    />
                                )}
                            </div>
                            <div className={s.create__line}></div>
                            <div className={s.create__form_columns}>
                                <div className={s.create__btns}>
                                    <button
                                        onClick={() => setType("Private")}
                                        className={
                                            type === "Private"
                                                ? `${s.create__btn} ${s.create__btn_active}`
                                                : `${s.create__btn}`
                                        }
                                        type="button"
                                    >
                                        Private
                                    </button>
                                    <button
                                        onClick={() => setType("Business")}
                                        className={
                                            type === "Business"
                                                ? `${s.create__btn} ${s.create__btn_active}`
                                                : `${s.create__btn}`
                                        }
                                        type="button"
                                    >
                                        Business
                                    </button>
                                    <button
                                        onClick={() => setStatus("New")}
                                        className={
                                            status === "New"
                                                ? `${s.create__btn} ${s.create__btn_active}`
                                                : `${s.create__btn}`
                                        }
                                        type="button"
                                    >
                                        New
                                    </button>
                                    <button
                                        onClick={() => setStatus("Used")}
                                        className={
                                            status === "Used"
                                                ? `${s.create__btn} ${s.create__btn_active}`
                                                : `${s.create__btn}`
                                        }
                                        type="button"
                                    >
                                        Used
                                    </button>
                                </div>
                            </div>
                            <div className={s.create__line}></div>
                            <div className={s.create__form_columns}>
                                <label className={s.create__select_columns}>
                                    <span>Choose your location</span>
                                    <LocationSelect value={location} setValue={setLocation} />
                                </label>
                            </div>
                            <div className={s.create__line}></div>
                            <div className={`${s.create__form_columns} ${s.create__form_columns_flex}`}>
                                <Input
                                    text="Email"
                                    type="Email"
                                    register={register}
                                    registerName="email"
                                    errors={errors}
                                    value={email}
                                    setValue={setEmail}
                                    validationOptions={{
                                        required: "Required field",
                                        pattern: {
                                            value: /^((([0-9A-Za-z]{1}[-0-9A-z\.]{1,}[0-9A-Za-z]{1})|([0-9А-Яа-я]{1}[-0-9А-я\.]{1,}[0-9А-Яа-я]{1}))@([-A-Za-z]{1,}\.){1,2}[-A-Za-z]{2,})$/u,
                                            message: "Enter a valid email",
                                        },
                                    }}
                                />
                                <Input
                                    text="Phone number"
                                    type="text"
                                    register={register}
                                    registerName="phone"
                                    errors={errors}
                                    value={phone}
                                    setValue={setPhone}
                                    validationOptions={{
                                        required: "Required field",
                                        pattern: {
                                            value: /\+380\s*\d{2}\s*\d{3}\s*\d{4}/,
                                            message:
                                                "Please enter a valid phone number in the format +380 00 000 0000.",
                                        },
                                        maxLength: {
                                            value: 13,
                                            message: "The maximum length of the phone number is 13 characters",
                                        },
                                    }}
                                />
                            </div>
                            <button className={s.create__create_btn} type="submit">
                                Create
                            </button>
                        </form>
                    </div>
                </div>
            </section>
            <Toaster position="bottom-left" reverseOrder={false} />
        </>
    );
};

export default CreateAdvert;
