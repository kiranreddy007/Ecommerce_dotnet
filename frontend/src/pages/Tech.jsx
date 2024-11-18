import React, { useEffect, useState } from "react";
import Product from "../components/Product";
import axios from "../utils/axios"; // Ensure axios is configured for your API base URL

const Tech = () => {
  const [products, setProducts] = useState({ laptops: [], smartphones: [], medicine: [] });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        // Fetch products for each category
        const laptopsResponse = await axios.get("api/products?categories=laptop");
        const smartphonesResponse = await axios.get("/api/products?categories=Smartphones");
     

        setProducts({
          laptops: laptopsResponse.data.$values,
          smartphones: smartphonesResponse.data.$values,
          
        });
        setLoading(false);
      } catch (err) {
        console.error("Error fetching products:", err);
        setError("Failed to fetch products. Please try again later.");
        setLoading(false);
      }
    };

    fetchProducts();
  }, []);

  if (loading) {
    return <div className="container mt-4">Loading products...</div>;
  }

  if (error) {
    return (
      <div className="container mt-4 text-danger">
        <p>{error}</p>
      </div>
    );
  }

  return (
    <div className="container mt-4">
      <h4 className="mt-4 ps-2">Laptops</h4>
      <div className="row m-0">
        {products.laptops.length > 0 ? (
          products.laptops.map((product) => <Product key={product.id} product={product} />)
        ) : (
          <p>No laptops available.</p>
        )}
      </div>

      <h4 className="mt-4 ps-2">Smartphones</h4>
      <div className="row m-0">
        {products.smartphones.length > 0 ? (
          products.smartphones.map((product) => <Product key={product.id} product={product} />)
        ) : (
          <p>No smartphones available.</p>
        )}
      </div>

      
    </div>
  );
};

export default Tech;