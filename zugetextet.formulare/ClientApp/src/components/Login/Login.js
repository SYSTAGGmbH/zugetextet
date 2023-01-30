import React from "react";
import "devextreme-react/text-area";
import { RequiredRule, PatternRule } from "devextreme-react/form";
import { useState, useCallback } from "react";
import TextBox from "devextreme-react/text-box";
import ValidationGroup from "devextreme-react/validation-group";
import Button from "devextreme-react/button";
import { useNavigate } from "react-router-dom";
import "./Login.css";
import PropTypes from "prop-types";
import { notifyFailure } from "../Notify";


export default function Login({ setToken, isLoggedIn, setIsLoggedIn }) {
  Login.prototype = {
    setToken: PropTypes.func.isRequired,
  };
  let [loginData, setLoginData] = useState("");
  let [pwData, setPwData] = useState("");
  const navigate = useNavigate();
  const onLoginDataValueChange = useCallback((v) => {
    setLoginData(v.value);
  }, []);
  const onPwDataValueChange = useCallback((v) => {
    setPwData(v.value);
  }, []);
  const handleSubmit = async () => {
    await loginUser();
  };

  async function loginUser() {
    const jsonData = {
      Username: loginData,
      Password: pwData,
    };
    return fetch("/api/logindata", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
      body: JSON.stringify(jsonData),
    })
      .then(async (response) => {
        if (response.status === 200) {
          setIsLoggedIn(true);
          setToken(await response.json());
          navigate("/fetch-data");
        } else {
          notifyFailure("Ungültige Logindaten");
        }
      })
      .then((value) => {
        return value;
      })
      .catch(() => {});
  }

  function renderLogout() {
    return (
      <div id="center">
        <h1> Abmelden </h1>
        <ValidationGroup name="loginGroup">
          <Button id="logoutBtn" text="Abmelden" onClick={logoutUser} />
        </ValidationGroup>
      </div>
    );
  }

  function renderLogin() {
    return (
      <div id="center">
        <h1> Anmelden </h1>
        <ValidationGroup name="loginGroup">
          <TextBox
            id="loginField"
            value={loginData}
            placeholder="Benutzername"
            valueChangeEvent="change"
            onValueChanged={onLoginDataValueChange}
          >
            <RequiredRule message="Benutzername muss angegeben werden" />
            <PatternRule
              message="Name darf keine Zeichen und Sonderzeichen enthalten"
              pattern={/^[^0-9]+$/}
            />
          </TextBox>
          <TextBox
            id="loginField"
            value={pwData}
            mode="password"
            placeholder="Passwort"
            valueChangeEvent="change"
            onValueChanged={onPwDataValueChange}
          >
            <RequiredRule message="Password muss angegeben werden" />
          </TextBox>
          <Button id="loginBtn" text="Anmelden" onClick={handleSubmit} />
        </ValidationGroup>
      </div>
    );
  }

  return isLoggedIn ? renderLogout() : renderLogin();

  function logoutUser() {
    setIsLoggedIn(false);
    localStorage.removeItem("token");
    navigate("/logindata");
  }
}
