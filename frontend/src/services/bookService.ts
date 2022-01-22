import { AxiosResponse } from "axios"
import instance from "../http"

const getBooks = (): Promise<AxiosResponse<any>> => {
    return instance.get('api/books');
}

export const bookService = {
    getBooks
}