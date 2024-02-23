import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { Suspense, lazy, useEffect } from "react";
import { useAppDispatch } from "./shared/hooks/storeHooks";
import { checkAuth } from "./store/slices/AuthSlice";
import LayoutMain from "./component/LayoutMain/LayoutMain";
import LayoutFullPage from "./component/LayoutFullPage/LayoutFullPage";
import Loader from "./component/Loader/Loader";

const LoginPage = lazy(() => import("./pages/LoginPage/LoginPage"));
const RegisterPage = lazy(() => import("./pages/RegisterPage/RegisterPage"));
const HomePage = lazy(() => import("./pages/HomePage/HomePage"));
const ProfilePage = lazy(() => import("./pages/ProfilePage/ProfilePage"));
const PublicProfile = lazy(() => import("./pages/PublicProfile/PublicProfile"));
const CreateAdvert = lazy(() => import("./pages/CreateAdvert/CreateAdvert"));
const ChangeAdvert = lazy(() => import("./pages/ChangeAdvert/ChangeAdvert"));
const SingleAdvert = lazy(() => import("./pages/SingleAdvert/SingleAdvert"));
const SearchPage = lazy(() => import("./pages/SearchPage/SearchPage"));
const FavoritePage = lazy(() => import("./pages/FavoritePage/FavoritePage"));

function App() {
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (localStorage.getItem("token")) {
            dispatch(checkAuth());
        }
    }, []);

    return (
        <Router>
            <Suspense fallback={<Loader />}>
                <Routes>
                    {/* user */}
                    <Route
                        path="/profile"
                        element={
                            <LayoutMain>
                                <ProfilePage />
                            </LayoutMain>
                        }
                    />
                    <Route
                        path="/change-advert/:id"
                        element={
                            <LayoutMain>
                                <ChangeAdvert />
                            </LayoutMain>
                        }
                    />
                    <Route
                        path="/create-advert"
                        element={
                            <LayoutMain>
                                <CreateAdvert />
                            </LayoutMain>
                        }
                    />
                    {/* public */}
                    <Route
                        path="/search/:search"
                        element={
                            <LayoutMain>
                                <SearchPage />
                            </LayoutMain>
                        }
                    />
                    <Route
                        path="/favorite"
                        element={
                            <LayoutMain>
                                <FavoritePage />
                            </LayoutMain>
                        }
                    />
                    <Route
                        path="/advert/:id"
                        element={
                            <LayoutMain>
                                <SingleAdvert />
                            </LayoutMain>
                        }
                    />
                    <Route
                        path="/profile/:id"
                        element={
                            <LayoutMain>
                                <PublicProfile />
                            </LayoutMain>
                        }
                    />
                    <Route
                        path="/login"
                        element={
                            <LayoutFullPage>
                                <LoginPage />
                            </LayoutFullPage>
                        }
                    />
                    <Route
                        path="/registration"
                        element={
                            <LayoutFullPage>
                                <RegisterPage />
                            </LayoutFullPage>
                        }
                    />
                    <Route
                        path="/"
                        element={
                            <LayoutMain>
                                <HomePage />
                            </LayoutMain>
                        }
                    />
                </Routes>
            </Suspense>
        </Router>
    );
}

export default App;
