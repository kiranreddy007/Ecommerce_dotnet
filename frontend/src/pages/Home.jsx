import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import Slide from "../components/Slide";
import Product from "../components/Product";
import axios from "../utils/axios";

const Home = () => {
//fetch products from backend
const [products, setProducts] = useState([]);
const [loading, setLoading] = useState(true);
const [error, setError] = useState(null);

useEffect(() => {
  const fetchProducts = async () => {
    try {
      const response = await axios.get("/api/products");

      
      setProducts(response.data.$values || []);
      setLoading(false);
    } catch (error) {
      console.error("Error fetching products:", error);
      setError("Failed to load products");
      setLoading(false);
    }
  };

  fetchProducts();
  
  }, []);
  
  return (
    
    <div className="home">
      
      <Slide />

      <h2 className="mt-4 ps-2"> Products</h2>
      <div className="row m-0">
        {products.map((product) => (
          
            <Product product={product} />
          
        ))}

        {loading && <div className="text-center">Loading...</div>}
        {error && <div className="text-center">{error}</div>}
      </div>

      


    </div>
  );
};

export default Home;
