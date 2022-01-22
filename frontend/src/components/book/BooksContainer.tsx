import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { booksLoad } from "../../redux/actions/bookActions";
import Books from "./Books";

const BooksContainer = () => {

    const dispatch = useDispatch();
    const books = useSelector((state: any) => state.appReducer.books);

    useEffect(() => {
        dispatch(booksLoad());
    }, []);

    return (
        <Books
            books={books}
        />);
}

export default BooksContainer;