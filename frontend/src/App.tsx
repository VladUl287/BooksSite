import './App.css';
import { useEffect } from 'react';
import Home from './components/home/Home';
import Navbar from './components/navbar/Navbar';
import { useDispatch, useSelector } from 'react-redux';
import { checkAuth } from './redux/actions/authActions';
import AuthContainer from './components/auth/AuthContainer';
import { Navigate, Route, Routes, useNavigate } from 'react-router-dom';
import { loadingOff } from './redux/actions/appActions';
import BooksContainer from './components/book/BooksContainer';

const App = () => {

  const dispatch = useDispatch();
  const navigate = useNavigate();
  let { isAuth, isLoading } = useSelector((state: any) => state.appReducer);

  useEffect(() => {
    let token = localStorage.getItem('token');
    if (token) {
      dispatch(checkAuth());
    } else {
      dispatch(loadingOff());
      navigate('/auth');
    }
  }, []);

  if (isLoading) {
    return <div>Loading app...</div>
  }

  return (
    <div className="App">
      {isAuth && <Navbar /> }
      {console.log(isAuth)}
      <Routes>
        <Route path="/home" element={
          <AuthGuard>
            <Home />
          </AuthGuard>
        } />
        <Route path="/books" element={
          <AuthGuard>
            <BooksContainer />
          </AuthGuard>
        } />
        <Route path="/auth" element={<AuthContainer />}></Route>
      </Routes>
    </div>
  );
}

const AuthGuard = ({ children }: { children: JSX.Element }) => {
  const token = localStorage.getItem('token');

  if (!token) {
    return <Navigate to="/auth" />;
  }

  return children;
}

export default App;