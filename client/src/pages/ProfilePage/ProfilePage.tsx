import { Suspense, lazy, useEffect, useState } from "react";
import s from "./ProfilePage.module.scss";
import { AnimatePresence, motion } from "framer-motion";
import Loader from "../../component/Loader/Loader";
import VerticalList from "../../component/VerticalList/VerticalList";
import { useGetUserAdvertsQuery } from "../../services/AdvertService";
import { useAppSelector } from "../../shared/hooks/storeHooks";
import { selectUserInfo } from "../../store/selectors";
import { AdvertResponse } from "../../types/response/AdvertResponse";

const Settings = lazy(() => import("../../component/Settings/Settings"));

const ProfilePage = () => {
    const [activeTab, setActiveTab] = useState<number>(1);
    const [advertList, setAdvertList] = useState<AdvertResponse | null>(null);
    const [activePage, setActivePage] = useState<number>(1);

    const user = useAppSelector(selectUserInfo);

    const { data } = useGetUserAdvertsQuery({ limit: 6, page: activePage, userId: user.id });

    useEffect(() => {
        setAdvertList(data || null);
    }, [data]);

    return (
        <section className={s.profile}>
            <div className="container">
                <div className={s.profile__inner}>
                    <div className={s.profile__btns}>
                        <button className={s.profile__tabs_btn} onClick={() => setActiveTab(1)}>
                            My adverts
                        </button>
                        <button className={s.profile__tabs_btn} onClick={() => setActiveTab(2)} data-cy="settings-btn">
                            Settings
                        </button>
                    </div>

                    <div className={s.profile__tabs}>
                        <AnimatePresence mode="wait">
                            {activeTab === 1 && (
                                <motion.div
                                    key="my-adverts"
                                    className={s.profile__tab_content}
                                    initial={{ opacity: 0 }}
                                    animate={{ opacity: 1 }}
                                    exit={{ opacity: 0 }}
                                >
                                    <Suspense fallback={<Loader />}>
                                        <VerticalList
                                            title="Your Adverts"
                                            list={advertList || null}
                                            changes={true}
                                            setActivePage={setActivePage}
                                            activePage={activePage}
                                        />
                                    </Suspense>
                                </motion.div>
                            )}
                            {activeTab === 2 && (
                                <motion.div
                                    key="settings"
                                    className={s.profile__tab_content}
                                    initial={{ opacity: 0 }}
                                    animate={{ opacity: 1 }}
                                    exit={{ opacity: 0 }}
                                    data-cy="settings"
                                >
                                    <Suspense fallback={<Loader />}>
                                        <Settings />
                                    </Suspense>
                                </motion.div>
                            )}
                        </AnimatePresence>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default ProfilePage;
