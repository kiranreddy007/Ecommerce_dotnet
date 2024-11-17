import React from "react";

const CheckoutCartItem = ({ item }) => {
  return (
    <div className="col-md-3 col-sm-6 col-12 mb-4">
      <div className="d-flex align-items-center">
        <img
          src="https://picsum.photos/seed/picsum/50/50"
          alt={item.name}
          className="rounded"
          style={{
            height: "5rem",
            width: "5rem",
            objectFit: "cover",
          }}
        />

        <div className="ms-2">
          <h6 className="mb-0" style={{ fontSize: "0.85rem" }}>
            {item.name}
          </h6>
          <div className="d-flex align-items-center">
            <div>
              <p className="mb-0 me-2" style={{ fontSize: "0.75rem" }}>
                ${item.price}
              </p>
              <p
                style={{
                  fontSize: "0.75rem",
                }}
                className=" me-2"
              >
                Quantity: {item.quantity}
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CheckoutCartItem;
