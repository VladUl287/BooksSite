import { bookReducer } from './reducers/bookReducer';
import { combineReducers } from 'redux';
import { authReducer } from './reducers/authReducer';

export const rootReducer = combineReducers({
    auth: authReducer,
    book: bookReducer
});