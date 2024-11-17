import React, { useState, useEffect } from "react";
import axios from "../utils/axios";
import Product from "../components/Product";

const SearchPage = () => {
  const [searchQuery, setSearchQuery] = useState("");
  const [products, setProducts] = useState([]);

  // Function to fetch products based on search query
  const fetchProducts = async (query) => {
    try {
      const response = await axios.get(`/api/products?search=${query}`);
      setProducts(response.data.$values || []);
    } catch (error) {
      console.error("Error fetching products:", error);
      setProducts([]);
    }
  };

  // Debounce search to avoid too many API calls
  useEffect(() => {
    // also update current search query in browser URL
    const searchParams = new URLSearchParams(window.location.search);
    searchParams.set("q", searchQuery);
    window.history.pushState(null, "", "?" + searchParams.toString());
    const timeoutId = setTimeout(() => {
      if (searchQuery) {
        fetchProducts(searchQuery);
      } else {
        setProducts([]);
      }
    }, 300); // Wait 300ms after user stops typing

    return () => clearTimeout(timeoutId);
  }, [searchQuery]);

  useEffect(() => {
    // get search query from URL
    const searchParams = new URLSearchParams(window.location.search);
    const searchQuery = searchParams.get("q");

    // if search query is not empty, fetch products
    if (searchQuery) {
      fetchProducts(searchQuery);
    }
  }, []);

  const handleSearch = (e) => setSearchQuery(e.target.value);

  return (
    <div className="container mt-5">
      <h1 className="mb-4">Search Products</h1>

      {/* Search Input */}
      <div className="row mb-4">
        <div className="col-md-12">
          <div className="input-group">
            <input
              type="text"
              className="form-control"
              placeholder="Search by name..."
              value={searchQuery}
              onChange={handleSearch}
            />
          </div>
        </div>
      </div>

      {/* Product List */}
      <div className="row">
        {products.length > 0 ? (
          products.map((product) => (
            <Product key={product.id} product={product} />
          ))
        ) : (
          <div className="col-12">
            <p className="text-muted text-center">No products found.</p>
          </div>
        )}
      </div>
    </div>
  );
};

export default SearchPage;
