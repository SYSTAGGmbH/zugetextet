import { useState } from "react";

export default function Token() {
  const getToken = () => {
    const tokenString = localStorage.getItem("token");
    return tokenString;
  };

  const [token, setToken] = useState(getToken());

  // if token is defined, create new Token
  const saveToken = (tokenString) => {
    if (tokenString !== undefined) {
      localStorage.setItem("token", tokenString);
      setToken(tokenString);
    }
  };

  //return Json with (function) values for token and setToken
  return {
    setToken: saveToken,
    token: token,
  };
}

//Send GET to validate token. Return true if token is still valid.
export async function sendTokenForValidation({
  setIsLoggedIn,
  setIsValidating,
}) {
  const url = "/api/token";
  const response = await fetch(url, {
    method: "GET",
    headers: {
      Authorization: localStorage.getItem("token"),
    },
  });
  if (response.status === 200) {
    const value = await response.json();
    setIsLoggedIn(value);
    setIsValidating(false);
    return value;
  }
  setIsValidating(false);
}

export { Token };
