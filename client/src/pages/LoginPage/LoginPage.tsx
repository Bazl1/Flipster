import s from "./LoginPage.module.scss";
import { Link } from "react-router-dom";
import LoginForm from "../../component/LoginForm/LoginForm";

const LoginPage = () => {
    return (
        <section className={s.login}>
            <div className="container">
                <div className={s.login__inner}>
                    <h2 className={s.login__title}>
                        Welcome back to FLIPSTER.
                    </h2>
                    <h3 className={s.login__subtitle}>
                        <span>
                            <Link
                                className={s.login__link}
                                to={"/registration"}
                            >
                                Create an account
                            </Link>
                        </span>
                        or enter credentials.
                    </h3>
                    <LoginForm />
                </div>
            </div>
        </section>
    );
};

export default LoginPage;
