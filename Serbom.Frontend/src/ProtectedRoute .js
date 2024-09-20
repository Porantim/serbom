import { Navigate } from "react-router-dom";

export const ProtectedRoute = ({ children }) => {
    const { jwtkn } = sessionStorage.getItem("jwtkn");
    if (!jwtkn) {
        return <Navigate to="/login" />;
    }
    return children;
};