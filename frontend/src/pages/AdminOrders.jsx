import React, { useState, useEffect } from "react";
import axios from "../utils/axios";

const AdminOrders = () => {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchOrders = async () => {
      try {
        const response = await axios.get("/api/order/all"); // Fetch all orders
        console.log(response.data, "responseOrders");
        setOrders(response.data.$values || []);
        setLoading(false);
      } catch (err) {
        console.error("Error fetching orders:", err);
        setError("Failed to load orders.");
        setLoading(false);
      }
    };

    fetchOrders();
  }, []);

  const updateOrderStatus = async (orderId, newStatus) => {
    try {
      await axios.put(`/api/order/${orderId}`, { status: newStatus });
      setOrders(
        orders.map((order) =>
          order.id === orderId ? { ...order, status: newStatus } : order
        )
      );
    } catch (err) {
      console.error("Error updating order status:", err);
      setError("Failed to update order status.");
    }
  };

  if (loading) return <div className="container mt-4">Loading orders...</div>;
  if (error) return <div className="container mt-4 text-danger">{error}</div>;

  if (orders.length === 0)
    return <div className="container mt-4">No orders available.</div>;

  return (
    <div className="container mt-4">
      <h3 className="mb-4">Admin - Manage Orders</h3>
      {orders.map((order) => (
        <div key={order.id} className="card mb-4 shadow-sm">
          <div className="card-body">
            <h5>Order #{order.id}</h5>
            <p>
              <strong>Status:</strong> {order.status}
            </p>
            <p>
              <strong>Total Amount:</strong> ${order.totalAmount.toFixed(2)}
            </p>
            <p>
              <strong>Shipping Address:</strong> {order.shippingAddress},{" "}
              {order.shippingCity}, {order.shippingPostalCode}
            </p>

            

            <div className="mt-3">
              <button
                className="btn btn-sm btn-success me-2"
                onClick={() => updateOrderStatus(order.id, "Shipped")}
              >
                Mark as Shipped
              </button>
              <button
                className="btn btn-sm btn-danger"
                onClick={() => updateOrderStatus(order.id, "Cancelled")}
              >
                Cancel Order
              </button>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
};

export default AdminOrders;