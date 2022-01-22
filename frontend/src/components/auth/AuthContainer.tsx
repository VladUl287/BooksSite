import Auth from './Auth';
import { Navigate, useNavigate } from 'react-router';
import { useDispatch } from 'react-redux';
import { useState } from 'react';
import { authService } from '../../services/authService';
import { userLogin } from '../../redux/actions/authActions';
import { useForm } from 'react-hook-form';
import { IAuthModel } from '../../models/IAuthModel';

const AuthContainer = () => {

    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [toggle, setToggle] = useState(true);
    
    const { 
        register, 
        handleSubmit,
        reset,
        formState: { errors } } = useForm();
    const { 
        register: login, 
        handleSubmit: handleSubmitLogin, 
        formState: { errors: errorsLogin } } = useForm();

    const submitLogin = (data: IAuthModel) => {
        dispatch(userLogin(data.email, data.password, navigate));
    }

    const submitRegister = async (data: IAuthModel) => {
        await authService.register(data.email, data.login, data.password);
        setToggle(false);
        reset();
    }

    if (localStorage.getItem('token')) {
        return <Navigate to="/home" />
    }

    return <Auth
        toggle={toggle}
        setToggle={setToggle}
        errorsLogin={errorsLogin}
        login={login}
        handleSubmitLogin={handleSubmitLogin}
        errors={errors}
        register={register}
        handleSubmit={handleSubmit}
        submitLogin={submitLogin}
        submitRegister={submitRegister}
    />;

}

export default AuthContainer;