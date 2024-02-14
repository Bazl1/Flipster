import { Suspense, lazy, useState } from "react";
import s from "./ProfilePage.module.scss";
import { AnimatePresence, motion } from "framer-motion";
import Loader from "../../component/Loader/Loader";

const Settings = lazy(() => import("../../component/Settings/Settings"));

const ProfilePage = () => {
    const [activeTab, setActiveTab] = useState<number>(1);

    return (
        <section className={s.profile}>
            <div className="container">
                <div className={s.profile__inner}>
                    <div className={s.profile__btns}>
                        <button
                            className={s.profile__tabs_btn}
                            onClick={() => setActiveTab(1)}
                        >
                            My adverts
                        </button>
                        <button
                            className={s.profile__tabs_btn}
                            onClick={() => setActiveTab(2)}
                        >
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
                                    dsfsd1
                                </motion.div>
                            )}
                            {activeTab === 2 && (
                                <motion.div
                                    key="settings"
                                    className={s.profile__tab_content}
                                    initial={{ opacity: 0 }}
                                    animate={{ opacity: 1 }}
                                    exit={{ opacity: 0 }}
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
