import { bookService } from './../../services/bookService';
import { SET_BOOKS } from '../types';

export const booksLoad = () => async (dispatch: Function) => {
    const result = await bookService.getBooks();

    if (result.status == 200) {
        dispatch({
            type: SET_BOOKS,
            payload: {
                books: result.data
            }
        });
    }
}