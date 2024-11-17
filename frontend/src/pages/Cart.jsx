import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import CartItem from "../components/CartItem";
import axios from "../utils/axios";

const Cart = () => {
  const [cartData, setCartData] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchCart();
  }, []);

  const fetchCart = async () => {
    try {
      const response = await axios.get("/api/cart/");
      setCartData(response.data);
      setLoading(false);
    } catch (error) {
      console.error("Error fetching cart:", error);
      setLoading(false);
    }
  };

  const calculateSubTotal = () => {
    if (!cartData?.cartItems?.$values) return 0;
    return cartData.cartItems.$values.reduce(
      (total, item) => total + item.price * item.quantity,
      0
    );
  };

  if (loading) {
    return <div className="container mt-4">Loading...</div>;
  }

  return (
    <div className="container mt-4">
      <h5 className="mb-0">Cart</h5>
      {!cartData?.cartItems?.$values ||
      cartData.cartItems.$values.length === 0 ? (
        <p>Cart is empty. Add some items to your cart to view your cart.</p>
      ) : (
        <>
          {cartData.cartItems.$values.map((item) => (
            <CartItem key={item.id} item={item} />
          ))}

          <div className="d-flex justify-content-between">
            <p className="text-muted">Sub Total Price</p>
            <p className="text-muted">${calculateSubTotal().toFixed(2)}</p>
          </div>

          <div className="d-flex justify-content-end">
            <Link to="/checkout" className="btn btn-dark">
              Checkout
            </Link>
          </div>
        </>
      )}
    </div>
  );
};

export default Cart;
