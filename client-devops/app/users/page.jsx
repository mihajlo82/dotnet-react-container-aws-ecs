"use client";

import { useEffect, useState } from "react";
import { getUsers, createUser, deleteUser } from "../src/lib/usersApi";

export default function UsersPage() {
  const [users, setUsers] = useState([]);
  const [name, setName] = useState("");
  const [email, setEmail] = useState(""); 

  useEffect(() => {
    loadUsers();
  }, []);

  async function loadUsers() {
    const data = await getUsers();
    setUsers(data);
  }

  async function handleSubmit(e) {
    e.preventDefault();

    await createUser({
      name,
      email, 
    });

    setName("");
    setEmail(""); 
    loadUsers();
  }

  async function handleDelete(id) {
    await deleteUser(id);
    loadUsers();
  }

  return (
    <div className="min-h-screen bg-gray-100 flex justify-center p-6">
      <div className="w-full max-w-2xl bg-white rounded-xl shadow-md p-6">
        <h1 className="text-2xl font-bold mb-6 text-gray-800">
          Users Management
        </h1>

        {/* Form */}
        <form
          onSubmit={handleSubmit}
          className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-6"
        >
          <input
            className="border rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
          <input
            className="border rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          /> 

          <button
            type="submit"
            className="md:col-span-3 bg-blue-600 text-white rounded-lg py-2 font-semibold hover:bg-blue-700 transition"
          >
            Add User
          </button>
        </form>

        {/* Users List */}
        <ul className="divide-y">
          {users.length > 0 && users.map((u) => (
            <li
              key={u.id}
              className="flex justify-between items-center py-3"
            >
              <div>
                <p className="font-medium text-gray-800">{u.name}</p>
                <p className="text-sm text-gray-500">
                  {u.email} · {u.age} years
                </p>
              </div>

              <button
                onClick={() => handleDelete(u.id)}
                className="text-red-500 hover:text-red-700 text-sm font-semibold"
              >
                Delete
              </button>
            </li>
          ))}
        </ul>

        {users.length === 0 && (
          <p className="text-center text-gray-500 mt-6">
            No users found
          </p>
        )}
      </div>
    </div>
  );
}
