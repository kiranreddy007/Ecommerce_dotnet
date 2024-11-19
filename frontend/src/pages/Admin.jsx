import { useState } from "react";
import { Link } from "react-router-dom";
import Products from "./Products";
import AdminOrders from "./AdminOrders"; // Import the new AdminOrders component

import Users from "./Users";

const Admin = () => {
  const [activeTab, setActiveTab] = useState("products");

  return (
    <div className="container mt-4">
      <h3 className="mb-4">Admin Panel</h3>
      <div className="nav nav-tabs">
        <button
          className={`nav-link ${activeTab === "products" ? "active" : ""}`}
          onClick={() => setActiveTab("products")}
        >
          Products
        </button>
        <button
          className={`nav-link ${activeTab === "orders" ? "active" : ""}`}
          onClick={() => setActiveTab("orders")}
        >
          Orders
        </button>
        <button
          className={`nav-link ${activeTab === "users" ? "active" : ""}`}
          onClick={() => setActiveTab("users")}
        >
          Users
        </button>
      </div>

      <div className="tab-content mt-4">
        {activeTab === "products" && <Products />}
        {activeTab === "orders" && <AdminOrders />}
        {activeTab === "users" && <Users />}
      </div>
    </div>
  );
};

export default Admin;