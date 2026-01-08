const API_URL = `${process.env.NEXT_PUBLIC_API_URL}/users`;
// export async function getUsers() {
//   const res = await fetch(API_URL, { cache: "no-store" });
//   return res.json();
// }

// export async function createUser(user) {
//   const res = await fetch(API_URL, {
//     method: "POST",
//     headers: { "Content-Type": "application/json" },
//     body: JSON.stringify(user),
//   });
//   return res.json();
// }

// export async function deleteUser(id) {
//   await fetch(`${API_URL}/${id}`, {
//     method: "DELETE",
//   });
// }


export async function getUsers() {
  const res = await fetch('/api/users', { cache: "no-store" });
  return res.json();
}

export async function createUser(user) {
  const res = await fetch('/api/users', {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(user),
  });
  return res.json();
}

export async function deleteUser(id) {
  await fetch(`/api/users/${id}`, {
    method: "DELETE",
  });
}
