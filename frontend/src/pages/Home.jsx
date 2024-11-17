import { useState } from "react";
import { Link } from "react-router-dom";
import Slide from "../components/Slide";
import Product from "../components/Product";

const Home = () => {
  return (
    <div className="home">
      <Slide />

      <h4 className="mt-4 ps-2">Products</h4>
      <div className="row m-0">
        {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map(
          (item, index) => (
            <Product key={index} />
          )
        )}
      </div>
    </div>
  );
};

export default Home;
