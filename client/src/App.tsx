import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { Suspense, lazy, useEffect } from "react";
import { useAppDispatch } from "./shared/hooks/storeHooks";
import { checkAuth } from "./store/slices/AuthSlice";
import LayoutMain from "./component/LayoutMain/LayoutMain";
import LayoutFullPage from "./component/LayoutFullPage/LayoutFullPage";
import Loader from "./component/Loader/Loader";
import axiosApi from "./shared/axios";
import ProtectedRoute from "./component/ProtectedRoute/ProtectedRoute";

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
const ChatsPage = lazy(() => import("./pages/ChatsPage/ChatsPage"));
const MessagePage = lazy(() => import("./pages/MessagePage/MessagePage"));

const visitor = async () => {
    await axiosApi.post("/auth/visit");
};

function App() {
    const dispatch = useAppDispatch();

    useEffect(() => {
        visitor();
        if (localStorage.getItem("token")) {
            dispatch(checkAuth());
        }
    }, []);

    return (
        <Router>
            <Suspense fallback={<Loader />}>
                <Routes>
                    {/* user */}
                    <Route element={<ProtectedRoute role={"User"} />}>
                        <Route
                            path="/message/:id"
                            element={
                                <LayoutMain>
                                    <MessagePage />
                                </LayoutMain>
                            }
                        />
                        <Route
                            path="/chats"
                            element={
                                <LayoutMain>
                                    <ChatsPage />
                                </LayoutMain>
                            }
                        />
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
                    </Route>
                    {/* public */}
                    <Route
                        path="/search/query/:search/location/:location"
                        element={
                            <LayoutMain>
                                <SearchPage />
                            </LayoutMain>
                        }
                    />
                    <Route
                        path="/search/query/:search"
                        element={
                            <LayoutMain>
                                <SearchPage />
                            </LayoutMain>
                        }
                    />
                    <Route
                        path="/search/location/:location"
                        element={
                            <LayoutMain>
                                <SearchPage />
                            </LayoutMain>
                        }
                    />
                    <Route
                        path="/search/category/:categoryId"
                        element={
                            <LayoutMain>
                                <SearchPage />
                            </LayoutMain>
                        }
                    />
                    <Route
                        path="/search/"
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
