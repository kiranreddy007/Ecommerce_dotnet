import React from "react";
import { useState } from "react";
import axios from "../utils/axios";

const CartItem = ({ item, onCartUpdate }) => {
  const [quantity, setQuantity] = useState(item.quantity);

  const handleQuantityChange = async (newQuantity) => {
    if (newQuantity < 1) return;

    try {
      await axios.put(`/api/cart/${item.id}?quantity=${newQuantity}`);
      setQuantity(newQuantity);
      if (onCartUpdate) onCartUpdate();

      //refresh the page
      
    } catch (error) {
      console.error("Error updating quantity:", error);
    }
  };

  const handleRemove = async () => {
    try {
      await axios.delete(`/api/cart/${item.id}`);
      if (onCartUpdate) onCartUpdate();

      //refresh the page
      window.location.reload();
    } catch (error) {
      console.error("Error removing item:", error);
    }
  };

  return (
    <div className="d-flex align-items-center mb-3 border-bottom pb-3">
      <img
        src={`http://localhost:5126/${item.product.imagePath}`}
        alt={item.product.name}
        className="rounded"
        style={{
          height: "100px",
          width: "100px",
          objectFit: "cover",
        }}
        onError={(e) => {
          e.target.src = "https://picsum.photos/seed/picsum/100/100"; // fallback image
        }}
      />

      <div className="ms-3 flex-grow-1">
        <h6 className="mb-1">{item.product.name}</h6>
        <p className="mb-1 text-muted" style={{ fontSize: "0.85rem" }}>
          {item.product.description}
        </p>
        <p className="mb-1 fw-bold">${item.product.price}</p>

        <div className="d-flex align-items-center">
          <button
            className="btn btn-sm btn-outline-dark px-2"
            onClick={() => handleQuantityChange(quantity - 1)}
            disabled={quantity <= 1}
          >
            -
          </button>
          <span className="mx-2">{quantity}</span>
          <button
            className="btn btn-sm btn-outline-dark px-2"
            onClick={() => handleQuantityChange(quantity + 1)}
            disabled={quantity >= item.product.stock}
          >
            +
          </button>
        </div>
      </div>

      <div>
        <button className="btn btn-sm btn-danger" onClick={handleRemove}>
          Remove
        </button>
      </div>
    </div>
  );
};

export default CartItem;
