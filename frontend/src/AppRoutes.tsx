import { FC } from "react";
import { IState } from "./models/IState";
import { useSelector } from "react-redux";
import Home from "./components/home/Home";
import Books from "./components/book/Books";
import AuthContainer from "./components/auth/AuthContainer";
import { Navigate, Route, Routes } from "react-router-dom";

type RouteProps = { isAuth: boolean };

const AppRoutes: FC<RouteProps> = (props: RouteProps) => {
    return (
        <Routes>
            <Route path="/home" element={
                <AuthGuard>
                    <Home />
                </AuthGuard>
            } />
            <Route path="/books" element={
                <AuthGuard>
                    <Books />
                </AuthGuard>
            } />
            <Route path="/auth" element={<AuthContainer />} />
            <Route path="*" element={
                <Navigate to={"/" + props.isAuth ? "home" : "auth"} />
            } />
        </Routes>
    );
}

const AuthGuard = ({ children }: { children: JSX.Element }) => {
    const isAuth = useSelector((state: IState) => state.auth.isAuth);

    if (!localStorage.getItem('token') || !isAuth) {
        return <Navigate to="/auth" />;
    }

    return children;
}

export default AppRoutes;