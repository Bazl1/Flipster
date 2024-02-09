import { Link } from "react-router-dom";
import s from "./RegisterPage.module.scss";
import RegistrationForm from "../../component/RegistrationForm/RegistrationForm";

const RegisterPage = () => {
    return (
        <section className={s.register}>
            <div className="container">
                <div className={s.register__inner}>
                    <h2 className={s.register__title}>Create an account</h2>
                    <h3 className={s.register__subtitle}>
                        <span>
                            <Link className={s.register__link} to={"/login"}>
                                Log in to your account
                            </Link>
                        </span>
                        or enter credentials.
                    </h3>
                    <RegistrationForm />
                </div>
            </div>
        </section>
    );
};

export default RegisterPage;
