import './Navbar.css';
import { useDispatch } from "react-redux";
import { Link } from "react-router-dom";
import { userLogout } from "../../redux/actions/authActions";

const Navbar = () => {
    const dispatch = useDispatch();

    const submitLogout = () => {
        dispatch(userLogout());
    }

    return (
        <div>
            <nav>
                <ul>
                    <li>
                        <Link to="/home">Главная</Link>
                    </li>
                    <li>
                        <Link to="/books">Книги</Link>
                    </li>
                    <li className="right">
                        <button onClick={submitLogout}>Выход</button>
                    </li>
                </ul>
            </nav>

        </div>
    );
}

export default Navbar;