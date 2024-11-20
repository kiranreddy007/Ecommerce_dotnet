//Add product page
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import axios from "../utils/axios";

const AddProduct = () => {
    const [name, setName] = useState("");
    const [price, setPrice] = useState("");
    const [description, setDescription] = useState("");
    const [imageFile, setImageFile] = useState(null);
    const [stock, setStock] = useState("");
    const [discount, setDiscount] = useState("");
    const [category, setCategory] = useState("");

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    
    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
    
        const formData = new FormData();
        formData.append("name", name);
        formData.append("price", price);
        formData.append("description", description);
        formData.append("imageFile", imageFile);
        formData.append("stock", stock);
        formData.append("discount", discount);
        formData.append("category", category);
    
        try {
        await axios.post("/api/products", formData, {
            headers: {
            "Content-Type": "multipart/form-data",
            },
        });
        setLoading(false);
        setError(null);
        setName("");
        setPrice("");
        setDescription("");
        setImageFile(null);

        // Redirect to admin page after adding product
        navigate("/admin");


    
        } catch (error) {
        console.error("Error adding product:", error);
        setLoading(false);
        setError("Failed to add product");
        }
    };
    
    return (
        <div className="container mt-4">
        <h4 className="mt-4 ps-2">Add Product</h4>
        <form onSubmit={handleSubmit}>
            <div className="mb-3">
            <label htmlFor="name" className="form-label">
                Name
            </label>
            <input
                type="text"
                className="form-control"
                id="name"
                value={name}
                onChange={(e) => setName(e.target.value)}
            />
            </div>
            <div className="mb-3">
            <label htmlFor="price" className="form-label">
                Price
            </label>
            <input
                type="number"
                className="form-control"
                id="price"
                value={price}
                onChange={(e) => setPrice(e.target.value)}
            />
            </div>
            <div className="mb-3">
            <label htmlFor="description" className="form-label">
                Description
            </label>
            <textarea
                className="form-control"
                id="description"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
            />
            </div>
            <div className="mb-3">
            <label htmlFor="imageFile" className="form-label">
                Image
            </label>
            <input
                type="file"
                className="form-control"
                id="imageFile"
                onChange={(e) => setImageFile(e.target.files[0])}
            />
            </div>
            <div className="mb-3">
            <label htmlFor="stock" className="form-label">
                Stock
            </label>
            <input
                type="number"
                className="form-control"
                id="stock"
                value={stock}
                onChange={(e) => setStock(e.target.value)}
            />
            </div>
            <div className="mb-3">
            <label htmlFor="discount" className="form-label">
                Discount
            </label>
            <input
                type="number"
                className="form-control"
                id="discount"
                value={discount}
                onChange={(e) => setDiscount(e.target.value)}
            />
            </div>


            
            <div className="mb-3">
            <label htmlFor="category" className="form-label">
                Category
            </label>
            <select
                className="form-select"
                id="category"
                value={category}
                onChange={(e) => setCategory(e.target.value)}
            >
                <option value="">Select category</option>
                <option value="Laptops">Laptops</option>
                <option value="SmartPhones">Headphones</option>
                <option value="Headphones">Smartphones</option>
            </select>

           

            </div>

            <button type="submit" className="btn btn-primary">
            {loading ? "Adding..." : "Add Product"}
            </button>
            {error && <div className="text-danger mt-2">{error}</div>}
        </form>
        <Link to="/admin" className="btn btn-link mt-3">
            Go back
        </Link>
        </div>
    );
}

export default AddProduct;