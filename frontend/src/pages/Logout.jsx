import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

const Logout = () => {
  const navigate = useNavigate();

  useEffect(() => {
    window.localStorage.clear();
    navigate("/login");
  }, []);

  return <div>Logging out...</div>;
};

export default Logout;
