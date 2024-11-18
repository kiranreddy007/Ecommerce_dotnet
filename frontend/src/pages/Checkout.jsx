import React from "react";
import { useForm } from "react-hook-form";
import axios from "../utils/axios";

const Checkout = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();

  const [items, setItems] = React.useState([]);
  const [loading, setLoading] = React.useState(true);
  const [selectedItems, setSelectedItems] = React.useState([]);

  React.useEffect(() => {
    const fetchCart = async () => {
      try {
        const response = await axios.get("/api/cart/");
        setItems(response.data.cartItems.$values || []);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching cart:", error);
        setLoading(false);
      }
    };

    fetchCart();
  }, []);

  const toggleItemSelection = (itemId) => {
    setSelectedItems((prevSelected) =>
      prevSelected.includes(itemId)
        ? prevSelected.filter((id) => id !== itemId)
        : [...prevSelected, itemId]
    );
  };

  if (loading) {
    return <div className="container mt-4">Loading...</div>;
  }

  const subTotal = selectedItems.reduce((sum, itemId) => {
    const item = items.find((item) => item.id === itemId);
    return item ? sum + item.product.price * item.quantity : sum;
  }, 0);

  const onSubmit = async (data) => {
    if (selectedItems.length === 0) {
      alert("Please select at least one item to place an order.");
      return;
    }

    try {
      const requestPayload = {
        cartItemIds: selectedItems,
        shippingFirstName: data.firstName,
        shippingLastName: data.lastName,
        shippingAddress: data.address,
        shippingCity: data.city,
        shippingPostalCode: data.postalCode,
      };

      const response = await axios.post("/api/order/", requestPayload);
      console.log("Order Submitted:", response.data);
      alert("Order placed successfully!");
    } catch (error) {
      console.error("Error placing order:", error);
      alert("Error placing order. Please try again.");
    }
  };

  return (
    <div className="container mt-4">
      <h3 className="mb-4">Checkout</h3>

      {/* Cart Items Section */}
      <div className="mb-4">
        <h5>Items</h5>
        <div className="row mb-3">
          {items.map((item) => (
            <div key={item.id} className="col-12">
              <div className="d-flex align-items-center justify-content-between">
                <div>
                  <h6>{item.product.name}</h6>
                  <p className="mb-1">Quantity: {item.quantity}</p>
                  <p className="mb-1">Price: ${item.product.price.toFixed(2)}</p>
                </div>
                <div>
                  <input
                    type="checkbox"
                    checked={selectedItems.includes(item.id)}
                    onChange={() => toggleItemSelection(item.id)}
                  />
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* Address Form */}
      <div className="mb-4">
        <h5>Shipping Address</h5>
        <form onSubmit={handleSubmit(onSubmit)}>
          <div className="row mb-3">
            <div className="col">
              <label className="form-label">First Name</label>
              <input
                type="text"
                className={`form-control ${
                  errors.firstName ? "is-invalid" : ""
                }`}
                {...register("firstName", {
                  required: "First Name is required",
                })}
              />
              {errors.firstName && (
                <div className="invalid-feedback">
                  {errors.firstName.message}
                </div>
              )}
            </div>
            <div className="col">
              <label className="form-label">Last Name</label>
              <input
                type="text"
                className={`form-control ${
                  errors.lastName ? "is-invalid" : ""
                }`}
                {...register("lastName", { required: "Last Name is required" })}
              />
              {errors.lastName && (
                <div className="invalid-feedback">
                  {errors.lastName.message}
                </div>
              )}
            </div>
          </div>
          <div className="mb-3">
            <label className="form-label">Address</label>
            <input
              type="text"
              className={`form-control ${errors.address ? "is-invalid" : ""}`}
              {...register("address", { required: "Address is required" })}
            />
            {errors.address && (
              <div className="invalid-feedback">{errors.address.message}</div>
            )}
          </div>
          <div className="row mb-3">
            <div className="col">
              <label className="form-label">City</label>
              <input
                type="text"
                className={`form-control ${errors.city ? "is-invalid" : ""}`}
                {...register("city", { required: "City is required" })}
              />
              {errors.city && (
                <div className="invalid-feedback">{errors.city.message}</div>
              )}
            </div>
            <div className="col">
              <label className="form-label">Postal Code</label>
              <input
                type="text"
                className={`form-control ${
                  errors.postalCode ? "is-invalid" : ""
                }`}
                {...register("postalCode", {
                  required: "Postal Code is required",
                })}
              />
              {errors.postalCode && (
                <div className="invalid-feedback">
                  {errors.postalCode.message}
                </div>
              )}
            </div>
          </div>
          <button type="submit" className="btn btn-dark">
            Place Order
          </button>
        </form>
      </div>

      {/* Summary Section */}
      <div className="mb-4">
        <h5>Order Summary</h5>
        <div className="d-flex justify-content-between">
          <p>Subtotal:</p>
          <p>${subTotal.toFixed(2)}</p>
        </div>
      </div>
    </div>
  );
};

export default Checkout;