import { SET_BOOKS } from './../types';
import { bookService } from './../../http/services/bookService';

export const getBooks = () => async (dispatch: Function) => {
    let result = await bookService.getBooks();
    dispatch({
        type: SET_BOOKS,
        payload: {
            books: result.data,
        }
    });
}