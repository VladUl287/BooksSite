import { IBook } from './../../models/IBook';
import { SET_BOOKS } from '../types';

const initialState = {
    books: new Array<IBook>()
}

export const bookReducer = (state = initialState, action: { type: string, payload: { books: IBook[] } }) => {
    const { type, payload } = action;
    switch (type) {
        case SET_BOOKS:
            return {
                ...state,
                books: payload.books
            }
        default:
            return state;
    }
}