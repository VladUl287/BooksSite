import { HIDE_ALERT, SHOW_ALERT } from './../types';

export const showAlert = (message: string) => {
    return {
        type: SHOW_ALERT,
        payload: {
            message
        }
    } 
}

export const hideAlert = () => { 
    return { 
        type: HIDE_ALERT 
    }
}