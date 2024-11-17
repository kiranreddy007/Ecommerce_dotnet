import React from "react";
import { useForm } from "react-hook-form";
import CheckoutCartItem from "../components/CheckoutCartItem";

const Checkout = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();
  const items = [
    { id: 1, name: "Item 1", price: 20, quantity: 2 },
    { id: 2, name: "Item 2", price: 15, quantity: 1 },
    { id: 3, name: "Item 3", price: 25, quantity: 1 },
    { id: 4, name: "Item 4", price: 30, quantity: 1 },
  ];

  const subTotal = items.reduce(
    (sum, item) => sum + item.price * item.quantity,
    0
  );
  const serviceTax = subTotal * 0.025;
  const processingFee = subTotal * 0.025;
  const total = subTotal + serviceTax + processingFee;

  const onSubmit = (data) => {
    console.log("Order Submitted:", data);
    alert("Order placed successfully!");
  };

  return (
    <div className="container mt-4">
      <h3 className="mb-4">Checkout</h3>

      {/* Cart Items Section */}
      <div className="mb-4">
        <h5>Items</h5>
        <div className="row mb-3">
          {items.map((item) => (
            <CheckoutCartItem key={item.id} item={item} />
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
        </form>
      </div>

      {/* Payment Section */}
      <div className="mb-4">
        <h5>Payment</h5>
        <div className="mb-3">
          <label className="form-label">Card Number</label>
          <input
            type="text"
            className={`form-control ${errors.cardNumber ? "is-invalid" : ""}`}
            {...register("cardNumber", { required: "Card Number is required" })}
          />
          {errors.cardNumber && (
            <div className="invalid-feedback">{errors.cardNumber.message}</div>
          )}
        </div>
        <div className="row">
          <div className="col">
            <label className="form-label">Expiry Date</label>
            <input
              type="text"
              placeholder="MM/YY"
              className={`form-control ${errors.expiry ? "is-invalid" : ""}`}
              {...register("expiry", { required: "Expiry Date is required" })}
            />
            {errors.expiry && (
              <div className="invalid-feedback">{errors.expiry.message}</div>
            )}
          </div>
          <div className="col">
            <label className="form-label">CVV</label>
            <input
              type="text"
              placeholder="123"
              className={`form-control ${errors.cvv ? "is-invalid" : ""}`}
              {...register("cvv", { required: "CVV is required" })}
            />
            {errors.cvv && (
              <div className="invalid-feedback">{errors.cvv.message}</div>
            )}
          </div>
        </div>
      </div>

      {/* Summary Section */}
      <div className="mb-4">
        <h5>Order Summary</h5>
        <div className="d-flex justify-content-between">
          <p>Subtotal:</p>
          <p>${subTotal.toFixed(2)}</p>
        </div>
        <div className="d-flex justify-content-between">
          <p>Service Tax (2.5%):</p>
          <p>${serviceTax.toFixed(2)}</p>
        </div>
        <div className="d-flex justify-content-between">
          <p>Processing Fee (2.5%):</p>
          <p>${processingFee.toFixed(2)}</p>
        </div>
        <div className="d-flex justify-content-between fw-bold">
          <p>Total:</p>
          <p>${total.toFixed(2)}</p>
        </div>
      </div>

      {/* Order Button */}
      <div className="d-flex justify-content-end">
        <button className="btn btn-dark" onClick={handleSubmit(onSubmit)}>
          Order Now
        </button>
      </div>
    </div>
  );
};

export default Checkout;
