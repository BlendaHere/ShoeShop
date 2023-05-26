import React from 'react';
import './App.css';
import {Home} from './home';
import {Brand} from './cruds/Brand';
import {Category} from './cruds/Category';
import {ClientAccount} from './cruds/ClientAccount';
import {ClientOrder} from './cruds/ClientOrder';
import {Product} from './cruds/Product';
import {WishList} from './cruds/WishList';
import {BrowserRouter as Router, Routes, Route,NavLink} from 'react-router-dom';

function App() {
  return (
    <Router>
    <div className="App container">
      <h3 className="d-flex justify-content-center m-3">
        React JS Frontend
      </h3>
        
      <nav className="navbar navbar-expand-sm bg-light navbar-dark">
        <ul className="navbar-nav">
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/home">
              Home
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/brand">
            Brand
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/category">
            Category
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/clientAccount">
            ClientAccount
            </NavLink>
          </li> <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/clientOrder">
            ClientOrder
            </NavLink>
          </li> <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/product">
            Product
            </NavLink>
          </li> <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/wishList">
            WishList
            </NavLink>
          </li>
        </ul>
      </nav>

      <Routes>
      <Route path='/' element={<Home />}/>
          <Route path='/brand' element={<Brand />}/>
          <Route path='/category' element={<Category />}/>
          <Route path='/clientAccount' element={<ClientAccount />}/>
          <Route path='/clientOrder' element={<ClientOrder />}/>
          <Route path='/product' element={<Product />}/>
          <Route path='/wishList' element={<WishList />}/>
      </Routes>
    </div>
    </Router>
  );
}

export default App;