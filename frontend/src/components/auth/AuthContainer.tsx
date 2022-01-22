import Auth from './Auth';
import { Navigate, useNavigate } from 'react-router';
import { useDispatch } from 'react-redux';
import { useState } from 'react';
import { authService } from '../../services/authService';
import { userLogin } from '../../redux/actions/authActions';
import { useForm } from 'react-hook-form';

const AuthContainer = () => {
    
    const dispatch = useDispatch();
    const { register, handleSubmit } = useForm();

    // const [email, setEmail] = useState('');
    // const [login, setLogin] = useState('');
    const [toggle, setToggle] = useState(true);
    // const [password, setPassword] = useState('');

    const navigate = useNavigate();

    const submitLogin = (data: { email: string, password: string}) => {
        dispatch(userLogin(data.email, data.password, navigate));
    }

    const submitRegister = async (data: { email: string, login: string, password: string}) => {

        await authService.register(data.email, data.login, data.password);
        
        setToggle(false);
    }

    if (localStorage.getItem('token')) {
        return <Navigate to="/home" />
    }

    return <Auth
        toggle={toggle}
        register={register}
        handleSubmit={handleSubmit}
        setToggle={setToggle}
        submitLogin={submitLogin}
        submitRegister={submitRegister}
    />;

}

export default AuthContainer;