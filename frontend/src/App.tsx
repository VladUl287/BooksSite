import './App.css';
import { useEffect } from 'react';
import Home from './components/home/Home';
import Navbar from './components/navbar/Navbar';
import { useDispatch, useSelector } from 'react-redux';
import { checkAuth } from './redux/actions/authActions';
import AuthContainer from './components/auth/AuthContainer';
import { Navigate, Route, Routes, useNavigate } from 'react-router-dom';
import { loadingOff } from './redux/actions/appActions';
import Books from './components/book/Books';

const App = () => {

  const dispatch = useDispatch();
  const navigate = useNavigate();
  let { authReducer, appReducer } = useSelector((state: any) => state);

  useEffect(() => {
    if (localStorage.getItem('token')) {
      dispatch(checkAuth());
    } else {
      dispatch(loadingOff());
      navigate('/auth');
    }
  }, []);

  if (appReducer.isLoading) {
    return <div>Loading app...</div>
  }

  return (
    <div className="App">
      {authReducer.isAuth && <Navbar /> }
      {console.log(authReducer.isAuth)}
      <Routes>
        <Route path="/home" element={
          <AuthGuard>
            <Home />
          </AuthGuard>
        } />
        <Route path="/books" element={
          <AuthGuard>
            <Books />
          </AuthGuard>
        } />
        <Route path="/auth" element={<AuthContainer />}></Route>
      </Routes>
    </div>
  );
}

const AuthGuard = ({ children }: { children: JSX.Element }) => {
  if (!localStorage.getItem('token')) {
    return <Navigate to="/auth" />;
  }

  return children;
}

export default App;