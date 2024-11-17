import react from "react";
import Product from "../components/Product";

const Tech = () => {
  return (
    <div className="container mt-4">
      <h4 className="mt-4 ps-2">Laptops</h4>
      <div className="row m-0">
        {[1, 2, 3, 4, 5, 6].map((item, index) => (
          <Product key={index} />
        ))}
      </div>

      <h4 className="mt-4 ps-2">Desktops</h4>
      <div className="row m-0">
        {[1, 2, 3, 4, 5, 6].map((item, index) => (
          <Product key={index} />
        ))}
      </div>

      <h4 className="mt-4 ps-2">Tablets</h4>
      <div className="row m-0">
        {[1, 2, 3, 4, 5, 6].map((item, index) => (
          <Product key={index} />
        ))}
      </div>

      <h4 className="mt-4 ps-2">Smartphones</h4>
      <div className="row m-0">
        {[1, 2, 3, 4, 5, 6].map((item, index) => (
          <Product key={index} />
        ))}
      </div>
    </div>
  );
};

export default Tech;
