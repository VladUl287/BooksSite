import { HIDE_ALERT, SHOW_ALERT } from '../types';

const initialState = {
    alert: null
}

export const appReducer = (state = initialState, action: { type: string, payload: { message: string } }) => {
    const { type, payload } = action;
    switch (type) {
        case SHOW_ALERT:
            return {
                ...state,
                alert: payload.message
            }
        case HIDE_ALERT:
            return {
                ...state,
                alert: null
            }
        default:
            return state;
    }
}