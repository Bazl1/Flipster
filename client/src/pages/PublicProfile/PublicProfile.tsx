import s from "./PublicProfile.module.scss";
import user from "../../shared/assets/img/user.png";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import VerticalList from "../../component/VerticalList/VerticalList";
import UserService from "../../services/UserService";
import { IUser } from "../../types/IUser";
import { useGetUserAdvertsQuery } from "../../services/AdvertService";
import { AdvertResponse } from "../../types/response/AdvertResponse";

const PublicProfile = () => {
    const { id } = useParams();
    const userId = id || "";

    const [userInfo, setUserInfo] = useState<IUser>({} as IUser);
    const [activePage, setActivePage] = useState<number>(1);
    const [advertsList, setAdvertsList] = useState<AdvertResponse | null>(null);
    const [show, setShow] = useState<boolean>(false);

    const { data } = useGetUserAdvertsQuery({ limit: 6, page: activePage, userId: userId });

    const FetchUserData = async () => {
        const response = await UserService.getUserInfo(userId);
        return response.data;
    };

    useEffect(() => {
        FetchUserData().then((res) => setUserInfo(res));
    }, [id]);

    useEffect(() => {
        setAdvertsList(data || null);
    }, [data]);

    return (
        <section className={s.profile}>
            <div className="container">
                <div className={s.profile__inner}>
                    <div className={s.profile__top}>
                        <div className={s.profile__user}>
                            {userInfo.avatar ? (
                                <img className={s.profile__avatar} src={userInfo.avatar} alt="avatar" />
                            ) : (
                                <img className={s.profile__avatar} src={user} alt="avatar" />
                            )}
                            <h3 className={s.profile__name}>{userInfo.name}</h3>
                        </div>
                        {userInfo.phoneNumber ? (
                            !show ? (
                                <button
                                    onClick={() => setShow((current) => !current)}
                                    className={s.profile__show_number}
                                >
                                    Show number
                                </button>
                            ) : (
                                <Link to={"tel:userInfo.phoneNumber"} className={s.profile__number}>
                                    {userInfo.phoneNumber}
                                </Link>
                            )
                        ) : null}
                    </div>
                    <div className={s.profile__adverts}>
                        <VerticalList
                            title="Adverts"
                            list={advertsList || null}
                            activePage={activePage}
                            setActivePage={setActivePage}
                        />
                    </div>
                </div>
            </div>
        </section>
    );
};

export default PublicProfile;
