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
        // Only display products that are not soft-deleted
        const activeProducts = response.data.$values.filter((product) => !product.isDeleted);
        setProducts(activeProducts || []);
        setLoading(false);
      } catch (err) {
        console.error("Error fetching products:", err);
        setError("Failed to load products.");
        setLoading(false);
      }
    };

    fetchProducts();
  }, []);

  const handleDelete = async (productId) => {
    if (window.confirm("Are you sure you want to delete this product?")) {
      try {
        await axios.delete(`/api/products/${productId}`);
        setProducts(products.filter((product) => product.id !== productId));
      } catch (err) {
        console.error("Error deleting product:", err);
        setError("Failed to delete product.");
      }
    }
  };

  return (
    <div>
      <h4>Products</h4>
      {loading && <div>Loading...</div>}
      {error && <div className="text-danger">{error}</div>}
      <div className="list-group">
        {products.map((product) => (
          <div key={product.id} className="list-group-item d-flex justify-content-between align-items-center">
            <div className="d-flex">
              {/* Product Image */}
              {product.imagePath && (
                <img
                  src={`/${product.imagePath}`}
                  alt={product.name}
                  style={{ width: "50px", height: "50px", objectFit: "cover", marginRight: "10px" }}
                />
              )}
              <div>
                <h5>{product.name}</h5>
                <p>${product.price.toFixed(2)}</p>
              </div>
            </div>
            <div>
              {/* Edit Product Button */}
              <Link to={`/admin/edit-product/${product.id}`} className="btn btn-sm btn-warning me-2">
                Edit
              </Link>
              {/* Delete Product Button */}
              <button
                onClick={() => handleDelete(product.id)}
                className="btn btn-sm btn-danger"
              >
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