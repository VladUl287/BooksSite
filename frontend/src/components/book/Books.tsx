import "./Books.css";
import { useEffect } from "react";
import { AppDispatch } from "../../App";
import { IState } from "../../models/IState";
import { useDispatch, useSelector } from "react-redux";
import { getBooks } from "../../redux/actions/bookActions";

const Books = () => {
    const dispatch: AppDispatch = useDispatch();
    const books = useSelector((state: IState) => state.book.books);

    useEffect(() => {
        dispatch(getBooks());
    }, []);

    let image = "https://s-media-cache-ak0.pinimg.com/564x/f9/8e/2d/f98e2d661445620266c0855d418aab71.jpg";
    return (
        <div className="books-wrap">
            {books.map((book, i) => (
                <div key={i} className="book">
                    <div className="cover">
                        <img src={image} />
                    </div>
                    <div className="description">
                        <p className="title">{book.name}<br />
                            <span className="author">{book.author}</span></p>
                    </div>
                </div>
            ))}
        </div>
    );
}

export default Books;