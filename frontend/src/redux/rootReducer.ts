import { combineReducers } from 'redux';
import { appReducer } from './reducers/appReducer';
import { authReducer } from './reducers/authReducer';
import { bookReducer } from './reducers/bookReducer';

export const rootReducer = combineReducers({
    authReducer,
    bookReducer,
    appReducer
});