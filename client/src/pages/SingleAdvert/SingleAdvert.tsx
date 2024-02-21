import s from "./SingleAdvert.module.scss";
import { Pagination } from "swiper/modules";
import { Swiper, SwiperSlide } from "swiper/react";
import "swiper/css";
import "swiper/css/pagination";
import user from "../../shared/assets/img/user.png";
import { useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import HorizontalList from "../../component/HorizontalList/HorizontalList";
import { useGetAdvertForIdQuery } from "../../services/AdvertService";
import Loader from "../../component/Loader/Loader";

type IParams = {
    [key: string]: string | undefined;
};

const SingleAdvert = () => {
    const { id } = useParams<IParams>();
    const navigate = useNavigate();

    const AdvertId = id || "";
    const { data, isLoading, isError } = useGetAdvertForIdQuery({ id: AdvertId });

    const [active, setActive] = useState<boolean>(false);

    if (isLoading) {
        return <Loader />;
    }

    if (isError) {
        navigate("/");
        return <></>;
    }

    return (
        <section className={s.advert}>
            <div className="container">
                <div className={s.advert__inner}>
                    <div className={s.advert__columns}>
                        {data && data.images ? (
                            <Swiper
                                className={s.advert__slider}
                                modules={[Pagination]}
                                pagination={{ clickable: true }}
                                spaceBetween={40}
                                slidesPerView={3}
                            >
                                {data?.images.map((item: any, index: number) => {
                                    return (
                                        <SwiperSlide key={index} className={s.advert__from_slide}>
                                            <img className={s.advert__form_slide_img} src={item} alt="img" />
                                        </SwiperSlide>
                                    );
                                })}
                            </Swiper>
                        ) : (
                            <div className={s.advert__skeletons}>
                                <div className={s.advert__skelet}></div>
                                <div className={s.advert__skelet}></div>
                                <div className={s.advert__skelet}></div>
                            </div>
                        )}

                        <div className={s.advert__content}>
                            <div className={s.advert__content_top}>
                                <h2 className={s.advert__title}>
                                    {data?.title} {data?.isFree && <div className={s.advert__free}>Free</div>}
                                </h2>
                                <p className={s.advert__price}>${data?.price}</p>
                            </div>
                            <div className={s.advert__content_columns}>
                                <label className={s.advert__label}>
                                    <span>Location:</span>
                                    <h4>{data?.contact.location}</h4>
                                </label>
                                <label className={s.advert__label}>
                                    <span>Status:</span>
                                    <h4>{data?.productType}</h4>
                                </label>
                                <label className={s.advert__label}>
                                    <span>Type:</span>
                                    <h4>{data?.businessType}</h4>
                                </label>
                            </div>
                            <p className={s.advert__text}>{data?.description}</p>
                        </div>
                    </div>
                    <div className={s.advert__columns}>
                        <Link className={s.advert__user_link} to={`/profile/${data?.contact.id}`}>
                            <div className={s.advert__user}>
                                {data?.contact.avatar ? (
                                    <img className={s.advert__user_img} src={data?.contact.avatar} alt="user" />
                                ) : (
                                    <img className={s.advert__user_img} src={user} alt="user" />
                                )}
                                <h3 className={s.advert__name}>{data?.contact.name}</h3>
                            </div>
                        </Link>
                        <div className={s.advert__btns}>
                            <button className={s.advert__user_btn}>Send Message</button>
                            {data?.contact.phoneNumber ? (
                                !active ? (
                                    <button onClick={() => setActive(true)} className={s.advert__user_btn}>
                                        Show Phone Number
                                    </button>
                                ) : (
                                    <Link
                                        className={s.advert__number}
                                        to={`tel:${data?.contact.phoneNumber && data?.contact.phoneNumber.trim()}`}
                                    >
                                        {data?.contact.phoneNumber}
                                    </Link>
                                )
                            ) : null}
                        </div>
                    </div>
                </div>
                {/* <HorizontalList title="Similar advertisements" /> */}
            </div>
        </section>
    );
};

export default SingleAdvert;
