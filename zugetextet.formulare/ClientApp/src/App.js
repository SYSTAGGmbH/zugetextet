import "devextreme/dist/css/dx.common.css";
import "devextreme/dist/css/dx.light.css";
import React, { useEffect } from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import Layout from "./components/Layout";
import SubmitForm from "./components/SubmitForm/SubmitForm";
import "./css/dx.generic.zugetextet-scheme.css";
import "./custom.css";
import { Token } from "./components/Token";
import Login from "./components/Login/Login";
import Forms from "./components/Forms/Forms";
import FetchData from "./components/FetchData";
import ThankYou from "./components/SubmitForm/ThankYou";
import { useState } from "react";
import { sendTokenForValidation } from "./components/Token";
import deMessages from "devextreme/localization/messages/de.json";
import { locale, loadMessages } from "devextreme/localization";
import useAppMetaData from "./hooks/useAppMetaData";
import LoadingSpinner from "./components/LoadingSpinner";

export default function App() {
  const { setToken } = Token();
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [isValidating, setIsValidating] = useState(true);
  const { isMetaDataLoading } = useAppMetaData()

  useEffect(() => {
    sendTokenForValidation({ setIsLoggedIn, setIsValidating });
    loadMessages(deMessages);
    locale("de-DE");
  }, []);

  if(isValidating || isMetaDataLoading)
      return <LoadingSpinner/>

  return (
      <Routes>
        <Route path="/" element={<Layout isLoggedIn={isLoggedIn}></Layout>}>
          {isLoggedIn ? (
            <>
              <Route path="/" element={<Navigate to={"/fetch-data"} />} />
              <Route path="/fetch-data" element={<FetchData />} />
              <Route path="/forms" element={<Forms setIsLoggedIn={setIsLoggedIn} setIsValidating={setIsValidating}/>} />
            </>
          ) : (
            <>
              <Route path="/" element={<Navigate to={"/logindata"} />} />
              <Route path="/fetch-data" element={<Navigate to={"/logindata"} />} />
              <Route path="/forms" element={<Navigate to={"/logindata"} />} />
            </>
          )}
          <Route path="/logindata" element={<Login setToken={setToken} isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} />} />
          <Route path='/submit-form/:formId' element={<SubmitForm/>} />
          <Route path="/thank-you" element={<ThankYou />} />
        </Route>
      </Routes>
  );
}
