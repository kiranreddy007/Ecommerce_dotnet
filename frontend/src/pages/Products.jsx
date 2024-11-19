import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "../utils/axios";

const Products = () => {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await axios.get("/api/products");
        setProducts(response.data.$values || []);
        setLoading(false);
      } catch (err) {
        console.error("Error fetching products:", err);
        setError("Failed to load products.");
        setLoading(false);
      }
    };

    fetchProducts();
  }, []);

  const deleteProduct = async (productId) => {
    try {
      await axios.delete(`/api/products/${productId}`);
      setProducts(products.filter((product) => product.id !== productId));
    } catch (err) {
      console.error("Error deleting product:", err);
      setError("Failed to delete product.");
    }
  };

  return (
    <div>
      <h4>Products</h4>
      {loading && <div>Loading...</div>}
      {error && <div className="text-danger">{error}</div>}
      <div className="list-group">
        {products.map((product) => (
          <div key={product.id} className="list-group-item d-flex justify-content-between">
            <div>
              <h5>{product.name}</h5>
              <p>${product.price.toFixed(2)}</p>
            </div>
            <div>
              <Link to={`/admin/edit-product/${product.id}`} className="btn btn-sm btn-warning me-2">
                Edit
              </Link>
              <button className="btn btn-sm btn-danger" onClick={() => deleteProduct(product.id)}>
                Delete
              </button>
            </div>
          </div>
        ))}
      </div>
      <Link to="/admin/add-product" className="btn btn-primary mt-3">
        Add Product
      </Link>
    </div>
  );
};

export default Products;