import { IUser } from './IUser';

export interface IState {
    auth: {
        user: IUser,
        isAuth: boolean
    }
}