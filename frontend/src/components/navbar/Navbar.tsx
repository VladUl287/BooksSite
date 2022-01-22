import './Navbar.css';
import { useDispatch } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
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
                        <Link to="/home">Home</Link>
                    </li>
                    <li>
                        <Link to="/users">Users</Link>
                    </li>
                    <li className="right">
                        <button onClick={submitLogout}>Logout</button>
                    </li>
                </ul>
            </nav>

        </div>
    );
}

export default Navbar;