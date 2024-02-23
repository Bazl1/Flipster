import s from "./ChangeAdvert.module.scss";
import { useEffect, useState } from "react";
import Input from "../../component/Input/Input";
import { MdFileUpload } from "react-icons/md";
import { IoClose } from "react-icons/io5";
import { useForm } from "react-hook-form";
import { Pagination } from "swiper/modules";
import { Swiper, SwiperSlide } from "swiper/react";
import "swiper/css";
import "swiper/css/pagination";
import Textarea from "../../component/Textarea/Textarea";
import toast from "react-hot-toast";
import LocationSelect, { ILocationList } from "../../component/LocationSelect/LocationSelect";
import { useGetAdvertForIdQuery, useUpdateAdvertMutation } from "../../services/AdvertService";
import CategoriesSelect, { CategoriesList } from "../../component/CategoriesSelect/CategoriesSelect";
import { useParams } from "react-router-dom";

const ChangeAdvert = () => {
    const { id } = useParams();
    const AdvertId = id || "";

    const { data, refetch } = useGetAdvertForIdQuery({ id: AdvertId });

    const [title, setTitle] = useState<string>("");
    const [category, setCategory] = useState<CategoriesList | null>(null);
    const [description, setDescription] = useState<string>("");
    const [images, setImages] = useState<string[]>([]);
    const [fetchImages, setFetchImages] = useState<File[]>([]);
    const [free, setFree] = useState<boolean>(false);
    const [price, setPrice] = useState<string>("");
    const [type, setType] = useState<string>("Business");
    const [status, setStatus] = useState<string>("New");
    const [location, setLocation] = useState<ILocationList | null>(null);
    const [email, setEmail] = useState<string>("");
    const [phone, setPhone] = useState<string | undefined>("");

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm({
        mode: "onBlur",
    });

    const [updateAdvert, { isError }] = useUpdateAdvertMutation();

    const handleAddImages = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const input = e.target;

        if (input.files) {
            const maxSize = 2 * 1024 * 1024;
            if (input.files.length + images.length > 10) {
                toast.error("The maximum number of images is 10");
                return;
            }
            for (let i = 0; i < input.files.length; i++) {
                const file = input.files[i];

                if (input.files[i].size > maxSize) {
                    toast.error("Maximum image size 2mb");
                } else {
                    await new Promise((resolve, reject) => {
                        const reader = new FileReader();
                        reader.onload = function () {
                            if (images.length >= 10) {
                                toast.error("The maximum number of images is 10");
                                resolve(null);
                            } else {
                                const imageUrl = URL.createObjectURL(file);
                                setImages((prevImages) => [...prevImages, imageUrl]);
                                setFetchImages((prevImages) => [...prevImages, file]);
                                resolve(null);
                            }
                        };
                        reader.onerror = reject;
                        reader.readAsDataURL(file);
                    });
                }
            }
        }
    };

    const handleRemoveImage = (index: number) => {
        setImages((prevImages) => prevImages.filter((_, i) => i !== index));
        setFetchImages((prevImages) => prevImages.filter((_, i) => i !== index));
    };

    const Submit = async () => {
        if (free) {
            setPrice("");
        }
        const locationLabel = location?.label || "";
        const categoryId = category?.value || "";
        const data = new FormData();
        data.append("Title", title);
        data.append("Description", description);
        data.append("IsFree", free.toString());
        if (!free) {
            data.append("Price", price);
        }
        data.append("ProductType", status);
        data.append("BusinessType", type);
        data.append("Location", locationLabel);
        data.append("CategoryId", categoryId.toString());
        data.append("Email", email);
        data.append("PhoneNumber", phone || "");
        console.log(images);
        images.forEach((image) => {
            console.log(image);
            data.append("ImageUrls", image);
        });
        fetchImages.forEach((image) => {
            console.log(image);
            data.append("Images", image);
        });

        if (id) {
            await updateAdvert({ id, body: data });
            if (!isError) {
                toast.success("Changes saved");
            } else {
                toast.error("Save error");
            }
        }
    };

    useEffect(() => {
        if (id) {
            refetch();
        }
    }, [id, refetch]);

    useEffect(() => {
        if (data) {
            setTitle(data.title);
            setCategory({
                value: "",
                label: data.category.title,
            });
            setDescription(data.description);
            setImages(data.images);
            setFree(data.isFree);
            setPrice(data.price);
            setType(data.businessType);
            setStatus(data.productType);
            setLocation({
                value: "",
                label: data.contact.location || "",
            });
            setEmail(data.contact.email);
            setPhone(data.contact.phoneNumber);
        }
    }, [data]);

    return (
        <section className={s.change}>
            <div className="container">
                <div className={s.change__inner}>
                    <h2 className={s.change__title}>change your advert</h2>
                    <form className={s.change__form} onSubmit={handleSubmit(Submit)}>
                        <div className={s.change__form_columns}>
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
                        <div className={s.change__line}></div>
                        <div className={s.change__form_columns}>
                            <label className={s.change__select_columns}>
                                <span>Select a category</span>
                                <CategoriesSelect value={category} setValue={setCategory} setRequired={true} />
                            </label>
                        </div>
                        <div className={s.change__line}></div>
                        <div className={s.change__onload_imgs}>
                            <label className={s.change__onload_box}>
                                <p className={s.change__onload_text}>
                                    <span>
                                        <MdFileUpload />
                                    </span>
                                    Load Images
                                </p>
                                <input
                                    className={s.change__file}
                                    type="file"
                                    onChange={handleAddImages}
                                    accept="image/png, image/jpeg"
                                    // required
                                    multiple
                                />
                            </label>
                            {images.length > 0 ? (
                                <Swiper
                                    className={s.change__slider}
                                    modules={[Pagination]}
                                    pagination={{ clickable: true }}
                                    spaceBetween={40}
                                    slidesPerView={3}
                                >
                                    {images.map((item: any, index: number) => {
                                        return (
                                            <SwiperSlide key={index} className={s.change__from_slide}>
                                                <img className={s.change__form_slide_img} src={item} alt="img" />
                                                <div className={s.change__overlay}></div>
                                                <button
                                                    className={s.change__delete}
                                                    onClick={() => handleRemoveImage(index)}
                                                    type="button"
                                                >
                                                    <IoClose />
                                                </button>
                                            </SwiperSlide>
                                        );
                                    })}
                                </Swiper>
                            ) : (
                                <div className={s.change__skeletons}>
                                    <div className={s.change__skelet}></div>
                                    <div className={s.change__skelet}></div>
                                    <div className={s.change__skelet}></div>
                                </div>
                            )}
                        </div>
                        <div className={s.change__line}></div>
                        <div className={s.change__form_columns}>
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
                        <div className={s.change__line}></div>
                        <div className={s.change__form_columns}>
                            <div className={s.change__btns}>
                                <button
                                    onClick={() => setFree(false)}
                                    className={!free ? `${s.change__btn} ${s.change__btn_active}` : `${s.change__btn}`}
                                    type="button"
                                >
                                    Price
                                </button>
                                <button
                                    onClick={() => setFree(true)}
                                    className={free ? `${s.change__btn} ${s.change__btn_active}` : `${s.change__btn}`}
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
                        <div className={s.change__line}></div>
                        <div className={s.change__form_columns}>
                            <div className={s.change__btns}>
                                <button
                                    onClick={() => setType("Private")}
                                    className={
                                        type === "Private"
                                            ? `${s.change__btn} ${s.change__btn_active}`
                                            : `${s.change__btn}`
                                    }
                                    type="button"
                                >
                                    Private
                                </button>
                                <button
                                    onClick={() => setType("Business")}
                                    className={
                                        type === "Business"
                                            ? `${s.change__btn} ${s.change__btn_active}`
                                            : `${s.change__btn}`
                                    }
                                    type="button"
                                >
                                    Business
                                </button>
                                <button
                                    onClick={() => setStatus("New")}
                                    className={
                                        status === "New"
                                            ? `${s.change__btn} ${s.change__btn_active}`
                                            : `${s.change__btn}`
                                    }
                                    type="button"
                                >
                                    New
                                </button>
                                <button
                                    onClick={() => setStatus("Used")}
                                    className={
                                        status === "Used"
                                            ? `${s.change__btn} ${s.change__btn_active}`
                                            : `${s.change__btn}`
                                    }
                                    type="button"
                                >
                                    Used
                                </button>
                            </div>
                        </div>
                        <div className={s.change__line}></div>
                        <div className={s.change__form_columns}>
                            <label className={s.change__select_columns}>
                                <span>Choose your location</span>
                                <LocationSelect value={location} setValue={setLocation} setRequired={true} />
                            </label>
                        </div>
                        <div className={s.change__line}></div>
                        <div className={`${s.change__form_columns} ${s.change__form_columns_flex}`}>
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
                                        value: /\+380\s\d{2}\s\d{3}\s\d{4}/,
                                        message: "Please enter a valid phone number in the format +380 00 000 0000.",
                                    },
                                    maxLength: {
                                        value: 16,
                                        message: "The maximum length of the phone number is 12 characters",
                                    },
                                }}
                            />
                        </div>
                        <button className={s.change__change_btn} type="submit">
                            Change
                        </button>
                    </form>
                </div>
            </div>
        </section>
    );
};

export default ChangeAdvert;
