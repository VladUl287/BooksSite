import { authService } from '../../services/authService';
import { LOGIN, LOGOUT } from '../types';
import { loadingOff, loadingOn } from './appActions';

export const userLogin = (email: string, password: string, navigate: Function) => async (dispatch: Function) => {
    let result = await authService.login(email, password);
    if (result.status === 200) {
        localStorage.setItem('token', result.data.token);

        dispatch({
            type: LOGIN,
            payload: {
                token: result.data.token,
            }
        });

        navigate('/');
    }
}

export const userLogout = () => async (dispatch: Function) => {
    try {
        await authService.logout();
    } finally {
        dispatch({
            type: LOGOUT
        });
    }
}

export const checkAuth = () => async (dispatch: Function) => {
    dispatch(loadingOn());
    try {
        const result = await authService.refresh();
        localStorage.setItem('token', result.data.token);

        dispatch({
            type: LOGIN,
            payload: {
                token: result.data.token,
            }
        });
    } catch {
        localStorage.removeItem('token');
    } finally {
        dispatch(loadingOff());
    }
}