import './Auth.css';

const Auth = (props: any) => {
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
                            <input type='submit' value='Войти' />
                        </form>
                    ) : (
                        <form onSubmit={props.handleSubmit(props.submitRegister)}>
                            <div>
                                <input type='text' className={props.errors.email && 'validate-error'}
                                    {...props.register("email", {
                                        required: true,
                                        maxLength: 150,
                                        pattern: /^(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])+$/i
                                    })}
                                    placeholder='email'
                                />
                                {props.errors.email &&
                                    <span className='text-error'>
                                        некорректный email
                                    </span>}
                            </div>
                            <div>
                                <input type='text' className={props.errors.login && 'validate-error'}
                                    {...props.register("login", {
                                        required: true,
                                        minLength: 6
                                    })}
                                    placeholder='логин'
                                />
                                {props.errors.login &&
                                    <span className='text-error'>
                                        не менее 6-ти символов
                                    </span>}
                            </div>
                            <div>
                                <input type='text' className={props.errors.password && 'validate-error'}
                                    {...props.register("password", {
                                        required: true,
                                        minLength: 8,
                                        maxLength: 100,
                                        pattern: /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]+$/i
                                    })}
                                    placeholder='пароль'
                                />
                                {props.errors.password &&
                                    <span className='text-error'>
                                        буквы латинского алфавита,цифры
                                    </span>}
                            </div>
                            <input type='submit' value='Зарегистрироваться' />
                        </form>
                    )}
                </div>
            </div>
        </div>
    );

}

export default Auth;