import { FC } from "react";
import { LoginFormProps } from "../AuthTypes";

const LoginForm: FC<LoginFormProps> = (props: LoginFormProps) => {
    return (
        <form onSubmit={props.handleSubmitLogin(props.submitLogin)}>
            <div>
                <input type='text' className={props.errorsLogin.email && 'validate-error'}
                    {...props.login("email", {
                        required: true,
                        maxLength: 150
                    })}
                    placeholder='email'
                />
                {props.errorsLogin.email &&
                    <span className='text-error'>
                        обязательное поле
                    </span>}
            </div>
            <div>
                <input type='text' className={props.errorsLogin.password && 'validate-error'}
                    {...props.login("password", {
                        required: true,
                        maxLength: 150,
                    })}
                    placeholder='пароль'
                />
                {props.errorsLogin.password &&
                    <span className='text-error'>
                        обязательное поле
                    </span>}
            </div>
            <button type='submit' disabled={props.load} >
                {props.load ? <span className='loading'></span> : <span>Войти</span>}
            </button>
        </form>
    );
}

export default LoginForm;