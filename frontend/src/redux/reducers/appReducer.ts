import { LOADING_OFF, LOADING_ON } from '../types';

const initialState = {
    isLoading: false
}

export const appReducer = (state = initialState, action: { type: string }) => {
    const { type } = action;
    switch (type) {
        case LOADING_ON:
            return {
                isLoading: true
            }
        case LOADING_OFF:
            return {
                isLoading: false
            }
        default:
            return state;
    }
}