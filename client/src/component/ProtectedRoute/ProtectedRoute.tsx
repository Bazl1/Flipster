import { Navigate, Outlet } from "react-router-dom";
import { useAppSelector } from "../../shared/hooks/storeHooks";
import { selectUserInfo } from "../../store/selectors";

interface ProtectedRouteProps {
    role: string;
    redirectPath?: string;
}

const ProtectedRoute = ({ role, redirectPath = "/" }: ProtectedRouteProps) => {
    const user = useAppSelector(selectUserInfo);

    if (role !== user.role) {
        return <Navigate to={redirectPath} replace />;
    }
    return <Outlet />;
};

export default ProtectedRoute;
