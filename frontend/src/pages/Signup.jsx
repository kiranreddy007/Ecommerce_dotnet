import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";

const SignupPage = () => {
  return (
    <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
      <div
        className="card shadow-lg p-4"
        style={{ width: "25rem", marginBottom: "10rem" }}
      >
        <div className="card-body">
          <h6 className="card-title text-center mb-4">Create an Account</h6>
          <form>
            <div className="mb-3">
              <label htmlFor="username" className="form-label mb-0">
                Username
              </label>
              <input
                type="text"
                className="form-control"
                id="username"
                placeholder="Enter your username"
              />
            </div>
            <div className="mb-3">
              <label htmlFor="email" className="form-label mb-0">
                Email Address
              </label>
              <input
                type="email"
                className="form-control"
                id="email"
                placeholder="Enter your email"
              />
            </div>
            <div className="mb-3">
              <label htmlFor="password" className="form-label mb-0">
                Password
              </label>
              <input
                type="password"
                className="form-control"
                id="password"
                placeholder="Enter your password"
              />
            </div>
            <div className="mb-3">
              <label htmlFor="confirmPassword" className="form-label mb-0">
                Confirm Password
              </label>
              <input
                type="password"
                className="form-control"
                id="confirmPassword"
                placeholder="Confirm your password"
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
