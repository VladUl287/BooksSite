import { AxiosResponse } from "axios"
import instance from "..";
import { IBook } from "../../models/IBook";

const getBooks = (): Promise<AxiosResponse<IBook[]>> => {
    return instance.get<IBook[]>('api/book');
}

export const bookService = {
    getBooks
}