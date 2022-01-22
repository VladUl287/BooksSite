import { combineReducers } from 'redux';
import { appReducer } from './reducers/appReducer';
import { authReducer } from './reducers/authReducer';

export const rootReducer = combineReducers({
    authReducer,
    appReducer
});