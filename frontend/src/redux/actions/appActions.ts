import { LOADING_OFF, LOADING_ON } from '../types';

export const loadingOn = () => { 
    return { type: LOADING_ON } 
}
export const loadingOff = () => { 
    return { type: LOADING_OFF } 
}