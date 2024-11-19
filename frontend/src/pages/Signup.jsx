import React, { useState } from "react";
import axios from "../utils/axios"; // Ensure axios is correctly set up in your utils
import { useNavigate } from "react-router-dom";

const SignupPage = () => {
  const navigate = useNavigate();

  // Form data state
  const [formData, setFormData] = useState({
    Username: "", // Matches backend field name
    Email: "",
    Password: "",
    ConfirmPassword: "", // This is for frontend validation only
    PhoneNumber: "", // Matches backend field name
  });

  // Error and success messages
  const [errorMessage, setErrorMessage] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  // Handle input changes
  const handleChange = (e) => {
    const { id, value } = e.target;
    setFormData({ ...formData, [id]: value });
  };

  // Handle form submission
  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrorMessage("");
    setSuccessMessage("");

    // Validation: Check if passwords match
    if (formData.Password !== formData.ConfirmPassword) {
      setErrorMessage("Passwords do not match.");
      return;
    }

    try {
      // Make API call to register the user
      const response = await axios.post("/api/users/register", {
        Username: formData.Username,
        Email: formData.Email,
        Password: formData.Password,
        PhoneNumber: formData.PhoneNumber,
        Role: "User", // Ensure role is correctly handled in the backend
      });

      // Success response handling
      if (response.status === 200 || response.status === 201) {
        setSuccessMessage("Registration successful. Redirecting to login...");
        setTimeout(() => {
          navigate("/login"); // Redirect to the login page
        }, 2000);
      }
    } catch (error) {
      // Error response handling
      if (error.response && error.response.data) {
        setErrorMessage(error.response.data.message || "Registration failed.");
      } else {
        setErrorMessage("An error occurred. Please try again.");
      }
    }
  };

  return (
    <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
      <div
        className="card shadow-lg p-4"
        style={{ width: "25rem", marginBottom: "10rem" }}
      >
        <div className="card-body">
          <h6 className="card-title text-center mb-4">Create an Account</h6>
          {errorMessage && (
            <div className="alert alert-danger" role="alert">
              {errorMessage}
            </div>
          )}
          {successMessage && (
            <div className="alert alert-success" role="alert">
              {successMessage}
            </div>
          )}
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label htmlFor="Username" className="form-label mb-0">
                Username
              </label>
              <input
                type="text"
                className="form-control"
                id="Username"
                placeholder="Enter your username"
                value={formData.Username}
                onChange={handleChange}
                required
              />
            </div>
            <div className="mb-3">
              <label htmlFor="Email" className="form-label mb-0">
                Email Address
              </label>
              <input
                type="email"
                className="form-control"
                id="Email"
                placeholder="Enter your email"
                value={formData.Email}
                onChange={handleChange}
                required
              />
            </div>
            <div className="mb-3">
              <label htmlFor="PhoneNumber" className="form-label mb-0">
                Phone Number
              </label>
              <input
                type="text"
                className="form-control"
                id="PhoneNumber"
                placeholder="Enter your phone number"
                value={formData.PhoneNumber}
                onChange={handleChange}
                required
              />
            </div>
            <div className="mb-3">
              <label htmlFor="Password" className="form-label mb-0">
                Password
              </label>
              <input
                type="password"
                className="form-control"
                id="Password"
                placeholder="Enter your password"
                value={formData.Password}
                onChange={handleChange}
                required
              />
            </div>
            <div className="mb-3">
              <label htmlFor="ConfirmPassword" className="form-label mb-0">
                Confirm Password
              </label>
              <input
                type="password"
                className="form-control"
                id="ConfirmPassword"
                placeholder="Confirm your password"
                value={formData.ConfirmPassword}
                onChange={handleChange}
                required
              />
            </div>
            <button type="submit" className="btn btn-dark btn-sm w-100">
              Sign Up
            </button>
          </form>
          <p className="text-center mt-3">
            Already have an account?{" "}
            <a href="/login" className="text-primary text-decoration-none">
              Login
            </a>
          </p>
        </div>
      </div>
    </div>
  );
};

export default SignupPage;