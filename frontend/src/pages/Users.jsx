import React, { useState, useEffect } from "react";
import axios from "../utils/axios";

const Users = () => {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const response = await axios.get("/api/users/all");
        setUsers(response.data.$values || []);
        setLoading(false);
      } catch (err) {
        console.error("Error fetching users:", err);
        setError("Failed to load users.");
        setLoading(false);
      }
    };

    fetchUsers();
  }, []);

  const updateUserRole = async (userId, newRole) => {
    try {
      await axios.put(`/api/users/${userId}`, { role: newRole });
      setUsers(
        users.map((user) =>
          user.id === userId ? { ...user, role: newRole } : user
        )
      );
    } catch (err) {
      console.error("Error updating user role:", err);
    }
  };

  return (
    <div>
      <h4>Users</h4>
      {loading && <div>Loading...</div>}
      {error && <div className="text-danger">{error}</div>}
      {users.map((user) => (
        <div key={user.id} className="list-group-item d-flex justify-content-between">
          <div>
            <h5>{user.username}</h5>
            <p>Role: {user.role}</p>
          </div>
          <div>
            <button
              className="btn btn-sm btn-success me-2"
              onClick={() => updateUserRole(user.id, "Admin")}
            >
              Make Admin
            </button>
            <button
              className="btn btn-sm btn-secondary"
              onClick={() => updateUserRole(user.id, "User")}
            >
              Revoke Admin
            </button>
          </div>
        </div>
      ))}
    </div>
  );
};

export default Users;