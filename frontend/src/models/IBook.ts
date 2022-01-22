import { IAuthor } from './IAuthor';
export interface IBook {
    name: string,
    image: string,
    facialImage: string,
    description: string,
    authors: IAuthor[]
}