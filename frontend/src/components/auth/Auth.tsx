import './Auth.css';
import { FC } from 'react';
import { AuthProps } from './AuthTypes';
import LoginForm from './forms/LoginForm';
import RegisterForm from './forms/RegisterForm';

const Auth: FC<AuthProps> = (props: AuthProps) => {
    return (
        <div className="forms-wrapper">
            <div className='forms'>
                <div className='toggle-type'>
                    <button
                        onClick={() => { props.setToggle(true) }}
                        className={props.toggle ? 'active' : ''}
                    >
                        Логин
                    </button>
                    <button
                        onClick={() => { props.setToggle(false) }}
                        className={!props.toggle ? 'active' : ''}
                    >
                        Регистрация
                    </button>
                </div>
                <div className='forms-zone'>
                    {props.toggle ? (
                        <LoginForm
                            load={props.load}
                            login={props.login}
                            errorsLogin={props.errorsLogin}
                            submitLogin={props.submitLogin}
                            handleSubmitLogin={props.handleSubmitLogin}
                        />
                    ) : (
                        <RegisterForm
                            load={props.load}
                            register={props.register}
                            errors={props.errors}
                            handleSubmit={props.handleSubmit}
                            submitRegister={props.submitRegister}
                        />
                    )}
                </div>
            </div>
        </div>
    );

}

export default Auth;