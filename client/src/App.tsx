import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { useEffect } from "react";
import { useAppDispatch } from "./shared/hooks/storeHooks";
import { checkAuth } from "./store/slices/AuthSlice";
import LayoutMain from "./component/LayoutMain/LayoutMain";
import LoginPage from "./pages/LoginPage/LoginPage";
import LayoutFullPage from "./component/LayoutFullPage/LayoutFullPage";
import RegisterPage from "./pages/RegisterPage/RegisterPage";
import HomePage from "./pages/HomePage/HomePage";
import ProfilePage from "./pages/ProfilePage/ProfilePage";
import PublicProfile from "./pages/PublicProfile/PublicProfile";
import CreateAdvert from "./pages/CreateAdvert/CreateAdvert";

function App() {
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (localStorage.getItem("token")) {
            dispatch(checkAuth());
        }
    }, []);

    return (
        <Router>
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
                    path="/create-advert"
                    element={
                        <LayoutMain>
                            <CreateAdvert />
                        </LayoutMain>
                    }
                />
                {/* public */}
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
        </Router>
    );
}

export default App;
