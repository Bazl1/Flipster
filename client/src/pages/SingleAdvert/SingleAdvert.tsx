import s from "./SingleAdvert.module.scss";
import { Pagination } from "swiper/modules";
import { Swiper, SwiperSlide } from "swiper/react";
import "swiper/css";
import "swiper/css/pagination";
import user from "../../shared/assets/img/user.png";
import { useState } from "react";
import { Link } from "react-router-dom";
import HorizontalList from "../../component/HorizontalList/HorizontalList";

const SingleAdvert = () => {
    const [active, setActive] = useState<boolean>(false);
    return (
        <section className={s.advert}>
            <div className="container">
                <div className={s.advert__inner}>
                    <div className={s.advert__columns}>
                        {/* <Swiper
                            className={s.advert__slider}
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
                        </Swiper> */}
                        <div className={s.advert__skeletons}>
                            <div className={s.advert__skelet}></div>
                            <div className={s.advert__skelet}></div>
                            <div className={s.advert__skelet}></div>
                        </div>

                        <div className={s.advert__content}>
                            <div className={s.advert__content_top}>
                                <h2 className={s.advert__title}>Demo title</h2>
                                <p className={s.advert__price}>$30</p>
                            </div>
                            <div className={s.advert__content_columns}>
                                <label className={s.advert__label}>
                                    <span>Location:</span>
                                    <h4>UA, ZP reg, ZP</h4>
                                </label>
                                <label className={s.advert__label}>
                                    <span>Status:</span>
                                    <h4>Used</h4>
                                </label>
                                <label className={s.advert__label}>
                                    <span>Type:</span>
                                    <h4>Business</h4>
                                </label>
                            </div>
                            <p className={s.advert__text}>
                                Lorem ipsum dolor sit amet consectetur adipisicing elit. Suscipit repudiandae dolorem
                                cum, sit accusamus ipsam cupiditate quisquam tempore, culpa architecto mollitia eius?
                                Recusandae obcaecati, dicta tempora ipsam nulla quia voluptates. Quisquam obcaecati
                                reiciendis sapiente totam libero fugiat placeat modi, nostrum, voluptas dolores ratione
                                nesciunt, similique voluptatibus ullam voluptate laborum! Quo vero tenetur quas
                                similique ipsa, quos dolor vel aliquid temporibus!
                            </p>
                        </div>
                    </div>
                    <div className={s.advert__columns}>
                        <div className={s.advert__user}>
                            <img className={s.advert__user_img} src={user} alt="user" />
                            <h3 className={s.advert__name}>Maxim Ostapenko</h3>
                        </div>
                        <div className={s.advert__btns}>
                            <button className={s.advert__user_btn}>Send Message</button>
                            {!active ? (
                                <button onClick={() => setActive(true)} className={s.advert__user_btn}>
                                    Show Phone Number
                                </button>
                            ) : (
                                <Link className={s.advert__number} to={"tel:+380632805331"}>
                                    +380 63 280 5331
                                </Link>
                            )}
                        </div>
                    </div>
                </div>
                <HorizontalList />
            </div>
        </section>
    );
};

export default SingleAdvert;
