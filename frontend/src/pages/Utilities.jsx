import react from "react";
import Product from "../components/Product";

const Utilities = () => {
  return (
    <div className="container mt-4">
      <h4 className="mt-4 ps-2">Home Appliances</h4>
      <div className="row m-0">
        {[1, 2, 3, 4, 5, 6].map((item, index) => (
          <Product key={index} />
        ))}
      </div>

      <h4 className="mt-4 ps-2">Kitchen Appliances</h4>
      <div className="row m-0">
        {[1, 2, 3, 4, 5, 6].map((item, index) => (
          <Product key={index} />
        ))}
      </div>

      <h4 className="mt-4 ps-2"> Electronics</h4>
      <div className="row m-0">
        {[1, 2, 3, 4, 5, 6].map((item, index) => (
          <Product key={index} />
        ))}
      </div>

      <h4 className="mt-4 ps-2"> Tools</h4>
      <div className="row m-0">
        {[1, 2, 3, 4, 5, 6].map((item, index) => (
          <Product key={index} />
        ))}
      </div>
    </div>
  );
};

export default Utilities;
