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
                        <form onSubmit={props.handleSubmit(props.submitLogin)}>
                            <div>
                                {/* <input
                                    type='text'
                                    placeholder='email'
                                    onChange={e => props.setEmail(e.target.value)}
                                /> */}
                                <input type='text'
                                    {...props.register("email", { required: true, maxLength: 150 })}
                                    placeholder='email'
                                />
                            </div>
                            <div>
                                <input type='text'
                                    {...props.register("password", { required: true, maxLength: 150 })}
                                    placeholder='пароль'
                                />
                            </div>
                            <input type='submit' value='Войти' />
                        </form>
                    ) : (
                        <form onSubmit={props.submitRegister}>
                            <div>
                                <input
                                    type='text'
                                    onChange={e => props.setEmail(e.target.value)}
                                    placeholder='email'
                                />
                            </div>
                            <div>
                                <input
                                    type='text'
                                    onChange={e => props.setLogin(e.target.value)}
                                    placeholder='логин'
                                />
                            </div>
                            <div>
                                <input
                                    type='text'
                                    onChange={e => props.setPassword(e.target.value)}
                                    placeholder='пароль'
                                />
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