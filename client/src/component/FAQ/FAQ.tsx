import { useState } from "react";
import s from "./FAQ.module.scss";
import { AnimatePresence, motion } from "framer-motion";

interface FAQProps {
    title: string;
    children: any;
}

const FAQ: React.FC<FAQProps> = ({ title, children }) => {
    const [active, setActive] = useState<boolean>(false);
    return (
        <div className={s.faq}>
            <button
                className={
                    active
                        ? `${s.faq__title} ${s.faq__title_active}`
                        : `${s.faq__title}`
                }
                onClick={() => setActive((current) => !current)}
            >
                {title}
            </button>
            <AnimatePresence>
                {active && (
                    <motion.div
                        initial={{
                            opacity: 0,
                        }}
                        animate={{
                            opacity: 1,
                        }}
                        exit={{
                            opacity: 0,
                        }}
                        className={s.faq__content}
                    >
                        {children}
                    </motion.div>
                )}
            </AnimatePresence>
        </div>
    );
};

export default FAQ;
