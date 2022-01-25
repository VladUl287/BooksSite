import { IBook } from './IBook';
import { IUser } from './IUser';

export interface IState {
    auth: {
        user: IUser,
        isAuth: boolean
    }
    book: {
        books: IBook[]
    }
}