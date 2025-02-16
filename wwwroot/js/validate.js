function validateForm() {
  let username = document.getElementById("username").value;
  let email = document.getElementById("email").value;

  // Simple sanitization
  let usernameRegex = /^[a-zA-Z0-9_]+$/;
  let emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

  if (!usernameRegex.test(username)) {
    alert("Invalid username");
    return false;
  }

  if (!emailRegex.test(email)) {
    alert("Invalid email");
    return false;
  }

  return true;
}
