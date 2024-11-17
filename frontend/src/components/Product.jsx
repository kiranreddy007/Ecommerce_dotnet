import React from "react";

const Product = ({ product }) => {
  const discountedPrice =
    product.price - (product.price * product.discount) / 100;

  const imagePath = "/" + product.imagePath;
  return (
    <div className="col-md-2 col-sm-6 col-12 mb-4">
      <a href={`/products/${product.id}`} className="text-decoration-none">
        <div className="card border-0">
          <img
            src={imagePath}
            className="card-img"
            alt={product.name}
            style={{ height: "10rem", width: "100%", objectFit: "cover" }}
          />
          <div className="card-">
            <p className="card-title mt-2 mb-0 pb-0">{product.name}</p>
            <p className="card-text m-0 pt-0">
              ${discountedPrice.toFixed(2)}
              {product.discount > 0 && (
                <small className="text-muted text-decoration-line-through ms-2">
                  ${product.price.toFixed(2)}
                </small>
              )}
            </p>
            <a
              href={`/products/${product.id}`}
              className="btn btn-dark btn-sm px-1 py-0"
            >
              <span style={{ fontSize: "0.7rem" }}>Add to Cart</span>
            </a>
          </div>
        </div>
      </a>
    </div>
  );
};

export default Product;
