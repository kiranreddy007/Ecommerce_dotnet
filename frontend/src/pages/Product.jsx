import React, { useState, useEffect } from "react";
import ReactMarkdown from "react-markdown";
import { useParams } from "react-router-dom";
import axios from "../utils/axios";

const ProductPage = () => {
  const [quantity, setQuantity] = useState(1);
  const [product, setProduct] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const { id } = useParams();

  useEffect(() => {
    const fetchProduct = async () => {
      try {
        const response = await axios.get(`/api/products/${id}`);
        setProduct(response.data);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching product:", error);
        setError("Failed to load product");
        setLoading(false);
      }
    };

    fetchProduct();
  }, [id]);

  if (loading) return <div className="container mt-5">Loading...</div>;
  if (error) return <div className="container mt-5">{error}</div>;
  if (!product) return <div className="container mt-5">Product not found</div>;

  const discountedPrice =
    product.price - (product.price * product.discount) / 100;

  /**
     * 
     * @returns curl --location --request POST 'http://localhost:5126/api/cart/1?quantity=3' \
--data ''
     */
  // 1 is product id
  // 3 is quantity
  const handleAddToCart = () => {
    axios
      .post(`/api/cart/${product.id}?quantity=${quantity}`)
      .then((response) => {
        console.log(response.data);
        // navigate to cart page
        window.location.href = "/cart";
      });
  };

  return (
    <div className="container mt-5">
      <div className="row">
        {/* Category pill */}
        <div className="col-12 mb-3">
          <span className="badge bg-primary text-uppercase">
            {product.category}
          </span>
        </div>

        {/* Product content */}
        <div className="col-md-6">
          <img
            src={"/" + product.imagePath}
            alt={product.name}
            style={{
              width: "100%",
              height: "auto",
            }}
            className="img-fluid rounded shadow-sm"
          />
        </div>
        <div className="col-md-6">
          <h1 className="display-6">{product.name}</h1>
          <p className="lead">
            ${discountedPrice.toFixed(2)}
            {product.discount > 0 && (
              <small className="text-muted text-decoration-line-through ms-2">
                ${product.price.toFixed(2)}
              </small>
            )}
          </p>
          <p className="text-muted">Stock: {product.stock} units</p>
          <hr />
          <div className="mt-3">
            <ReactMarkdown>{product.description}</ReactMarkdown>
          </div>
          {/* dropdown with 1 to 10 quantity options */}
          <div className="dropdown mt-3">
            <select
              className="form-select"
              aria-label="Quantity"
              value={quantity}
              onChange={(e) => setQuantity(e.target.value)}
            >
              <option value="1">1</option>
              <option value="2">2</option>
              <option value="3">3</option>
              <option value="4">4</option>
              <option value="5">5</option>
              <option value="6">6</option>
              <option value="7">7</option>
              <option value="8">8</option>
              <option value="9">9</option>
              <option value="10">10</option>
            </select>
          </div>
          <button className="btn btn-dark mt-3" onClick={handleAddToCart}>
            Add to Cart
          </button>
        </div>
      </div>
    </div>
  );
};

export default ProductPage;
