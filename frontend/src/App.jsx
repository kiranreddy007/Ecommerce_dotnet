import { useState } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";

import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min.js";

import Navbar from "./components/Navbar.jsx";
import Home from "./pages/Home.jsx";
import Tech from "./pages/Tech.jsx";
import Utilities from "./pages/Utilities.jsx";
import Product from "./pages/Product.jsx";
import Cart from "./pages/Cart.jsx";
import Checkout from "./pages/Checkout.jsx";
import Login from "./pages/Login.jsx";
import Logout from "./pages/Logout.jsx";
import Signup from "./pages/Signup.jsx";
import Search from "./pages/Search.jsx";
import Orders from "./pages/Orders.jsx";
import "./App.css";
import Admin from "./pages/Admin.jsx";
import AddProduct from "./pages/AddProduct.jsx";
import AdminOrders from "./pages/AdminOrders.jsx";
import Users from "./pages/Users.jsx";
import Products from "./pages/Products.jsx";
import EditProduct from "./pages/EditProduct.jsx";

function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/search" element={<Search />} />
        <Route path="/tech" element={<Tech />} />
        <Route path="/utilities" element={<Utilities />} />
        <Route path="/products/:id" element={<Product />} />
        <Route path="/cart" element={<Cart />} />
        <Route path="/checkout" element={<Checkout />} />
        <Route path="/login" element={<Login />} />
        <Route path="/logout" element={<Logout />} />
        <Route path="/signup" element={<Signup />} />
        <Route path="/orders" element={<Orders />} />
        <Route path="/admin" element={<Admin />} />
        <Route path="/admin/add-product" element={<AddProduct />} />
        <Route path="/admin/orders" element={<AdminOrders />} />
        <Route path="/admin/users" element={<Users />} />
        <Route path="/admin/products" element={<Products />} />
        <Route path="/admin/edit-product/:id" element={<EditProduct />} />


      </Routes>
    </BrowserRouter>
  );
}

export default App;
