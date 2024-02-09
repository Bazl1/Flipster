import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { useEffect } from "react";
import { useAppDispatch } from "./shared/hooks/storeHooks";
import { checkAuth } from "./store/slices/AuthSlice";
import LayoutMain from "./component/LayoutMain/LayoutMain";
import LoginPage from "./pages/LoginPage/LoginPage";
import LayoutFullPage from "./component/LayoutFullPage/LayoutFullPage";
import RegisterPage from "./pages/RegisterPage/RegisterPage";

function App() {
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (localStorage.getItem("token")) {
            dispatch(checkAuth);
        }
    }, []);

    return (
        <Router>
            <Routes>
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
            </Routes>
        </Router>
    );
}

export default App;
