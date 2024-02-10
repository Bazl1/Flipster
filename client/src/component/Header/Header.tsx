import { Link } from "react-router-dom";
import s from "./Header.module.scss";
import { BiSolidMessage } from "react-icons/bi";
import { MdFavorite } from "react-icons/md";
import { FaUserCircle } from "react-icons/fa";
import { useAppDispatch, useAppSelector } from "../../shared/hooks/storeHooks";
import { selectIsAuth } from "../../store/selectors";
import { logout } from "../../store/slices/AuthSlice";

const Header = () => {
    const isAuth = useAppSelector(selectIsAuth);
    const dispatch = useAppDispatch();
    return (
        <header className="header">
            <div className="container">
                <div className={s.header__inner}>
                    <Link to={"/"} className={s.header__logo}>
                        FLIPSTER
                    </Link>
                    {!isAuth ? (
                        <div className={s.header__btns}>
                            <Link to={"/login"} className={s.header__btn}>
                                Sign in
                            </Link>
                            <Link
                                to={"/registration"}
                                className={s.header__btn}
                            >
                                Sign up
                            </Link>
                        </div>
                    ) : (
                        <nav className={s.header__menu}>
                            <ul className={s.header__list}>
                                <li className={s.header__list_item}>
                                    <Link to={"/messages"}>
                                        Messages{" "}
                                        <span>
                                            <BiSolidMessage />
                                        </span>
                                    </Link>
                                </li>
                                <li className={s.header__list_item}>
                                    <Link to={"/favorite"}>
                                        Favorite{" "}
                                        <span>
                                            <MdFavorite />
                                        </span>
                                    </Link>
                                </li>
                                <li className={s.header__list_item}>
                                    <Link to={"/profile"}>
                                        Profile{" "}
                                        <span>
                                            <FaUserCircle />
                                        </span>
                                    </Link>
                                </li>
                            </ul>
                            <Link to={"/"} className={s.header__fullbtn}>
                                CREATE ADVERT
                            </Link>
                            <button
                                onClick={() => {
                                    dispatch(logout());
                                }}
                                className={s.header__fullbtn}
                            >
                                Logout
                            </button>
                        </nav>
                    )}
                </div>
            </div>
        </header>
    );
};

export default Header;
