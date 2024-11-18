//admin cms page
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";

import axios from "../utils/axios";
import Product from "../components/Product";

const Admin = () => {
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
        <div className="container mt-4">
        <h4 className="mt-4 ps-2">Products</h4>
        <div className="row m-0">
            {products.map((product) => (
            <Product key={product.id} product={product} />
            ))}
    
            {loading && <div className="text-center">Loading...</div>}
            {error && <div className="text-center">{error}</div>}
        </div>
    
        <Link to="/admin/add-product" className="btn btn-primary mt-4">
            Add Product
        </Link>
        </div>
    );
    };

export default Admin;