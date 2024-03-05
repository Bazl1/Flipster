import { Link } from "react-router-dom";
import s from "./Header.module.scss";
import { BiSolidMessage } from "react-icons/bi";
import { MdFavorite } from "react-icons/md";
import { FaUserCircle } from "react-icons/fa";
import { useAppDispatch, useAppSelector } from "../../shared/hooks/storeHooks";
import { selectIsAuth } from "../../store/selectors";
import { logout } from "../../store/slices/AuthSlice";
import { useState } from "react";
import { HiMenuAlt3 } from "react-icons/hi";
import { IoClose } from "react-icons/io5";

const Header = () => {
    const [isOpen, setIsOpen] = useState<boolean>(false);

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
                            <Link to={"/registration"} className={s.header__btn}>
                                Sign up
                            </Link>
                        </div>
                    ) : (
                        <>
                            <button onClick={() => setIsOpen(true)} className={`${s.header__open_menu} pc-hidden`}>
                                <HiMenuAlt3 />
                            </button>

                            <nav
                                className={isOpen ? `${s.header__menu} ${s.header__menu_active}` : `${s.header__menu}`}
                            >
                                <button
                                    onClick={() => setIsOpen(false)}
                                    className={`${s.header__close_menu} pc-hidden `}
                                >
                                    <IoClose />
                                </button>
                                <ul className={s.header__list}>
                                    <li className={s.header__list_item}>
                                        <Link to={"/chats"}>
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
                                <Link to={"/create-advert"} className={s.header__fullbtn}>
                                    CREATE ADVERT
                                </Link>
                                <button
                                    onClick={() => {
                                        dispatch(logout());
                                    }}
                                    className={s.header__fullbtn}
                                    data-cy="button-logout"
                                >
                                    Logout
                                </button>
                            </nav>
                        </>
                    )}
                </div>
            </div>
        </header>
    );
};

export default Header;
