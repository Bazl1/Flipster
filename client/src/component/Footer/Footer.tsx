import s from "./Footer.module.scss";

const Footer = () => {
    return (
        <footer className="footer">
            <div className="container">
                <div className={s.footer__inner}>
                    <p className={s.footer__copywriting}>
                        Copyright © 2024 Flipster, Inc. All rights reserved.
                    </p>
                </div>
            </div>
        </footer>
    );
};

export default Footer;
