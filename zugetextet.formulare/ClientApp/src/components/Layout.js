import React from "react";
import { Container } from "reactstrap";
import { NavMenu } from "./NavMenu/NavMenu";
import Footer from "./Footer";
import useAppMetaData from "../hooks/useAppMetaData";
import { Outlet } from "react-router-dom";

export default function Layout({ isLoggedIn }) {
  const { submitFormHeaderVisible, submitFormFooterVisible } = useAppMetaData();

  // hide NavBar for the Submit-Form and Thank-You-Screen if submitFormHeaderVisible == false 
  // hide Footer for the Submit-Form and Thank-You-Screen if submitFormFooterVisible == false
  return (
    <div className="d-flex flex-column min-vh-100">
      {(window.location.pathname.includes("submit-form") ||
        window.location.pathname.includes("thank-you")) &&
      !submitFormHeaderVisible ? (
        <></>
      ) : (
        <NavMenu isLoggedIn={isLoggedIn} />
      )}
      <Container>
        {/* Renders the Routes contained in App.js */}
        <Outlet />
      </Container>
      {(window.location.pathname.includes("submit-form") ||
        window.location.pathname.includes("thank-you")) &&
      !submitFormFooterVisible ? (
        <></>
      ) : (
        <Footer />
      )}
    </div>
  );
}
