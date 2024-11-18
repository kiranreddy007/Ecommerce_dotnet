import React from "react";
import axios from "../utils/axios";

const Orders = () => {
  const [orders, setOrders] = React.useState([]);
  const [loading, setLoading] = React.useState(true);
  const [error, setError] = React.useState(null);

  React.useEffect(() => {
    const fetchOrders = async () => {
      try {
        const response = await axios.get("/api/order/user");
        setOrders(response.data.$values || []); // Assuming `$values` is present in the response
        setLoading(false);
      } catch (err) {
        console.error("Error fetching orders:", err);
        setError("Failed to fetch orders. Please try again later.");
        setLoading(false);
      }
    };

    fetchOrders();
  }, []);

  if (loading) {
    return <div className="container mt-4">Loading orders...</div>;
  }

  if (error) {
    return (
      <div className="container mt-4 text-danger">
        <p>{error}</p>
      </div>
    );
  }

  if (orders.length === 0) {
    return (
      <div className="container mt-4">
        <p>No orders found.</p>
      </div>
    );
  }

  return (
    <div className="container mt-4">
      <h3 className="mb-4">Your Orders</h3>
      {orders.map((order) => (
        <div key={order.id} className="card mb-4 shadow-sm">
          <div className="card-header bg-primary text-white">
            <h5 className="mb-0">Order #{order.id}</h5>
            <small>Order Status: {order.status || "Pending"}</small>
          </div>
          <div className="card-body">
            <p className="mb-3">
              <strong>Shipping Address:</strong>
              <br />
              {order.shippingFirstName || "N/A"} {order.shippingLastName || "N/A"}
              <br />
              {order.shippingAddress || "N/A"}, {order.shippingCity || "N/A"},{" "}
              {order.shippingPostalCode || "N/A"}
            </p>
            <div className="order-items">
              {order.orderItems?.$values?.map((item) => (
                <div
                  key={item.id}
                  className="d-flex align-items-center mb-3 border-bottom pb-3"
                >
                  <img
                    src={`/${item.product.imagePath}`}
                    alt={item.product.name}
                    style={{ width: "80px", height: "80px", objectFit: "cover" }}
                    className="me-3"
                  />
                  <div>
                    <h6 className="mb-1">{item.product.name}</h6>
                    <p className="mb-1 text-muted">{item.product.description}</p>
                    <p className="mb-0">
                      {item.quantity} x ${item.product.price.toFixed(2)} ={" "}
                      <strong>${(item.quantity * item.product.price).toFixed(2)}</strong>
                    </p>
                  </div>
                </div>
              ))}
            </div>
            <div className="mt-3 text-end">
              <h5 className="text-primary">
                Total Amount: ${order.totalAmount.toFixed(2)}
              </h5>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
};

export default Orders;