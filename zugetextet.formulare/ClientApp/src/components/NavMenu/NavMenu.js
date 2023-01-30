import React, { useState } from "react";
import { Collapse, Navbar, NavbarToggler, NavItem, NavLink } from "reactstrap";
import { Link } from "react-router-dom";
import "./NavMenu.css";
import zugetextet_logo from "../../assets/img/zugetextet_logo.png";

export function NavMenu({ isLoggedIn }) {
  let loginText;
  let [collapsed, setCollapsed] = useState(true);

  function toggleNavbar() {
    setCollapsed(!collapsed);
  }

  if (!isLoggedIn) {
    loginText = "Anmelden";
  } else {
    loginText = "Abmelden";
  }

  return (
    <header>
      <div className="center-logo">
        <a href="https://zugetextet.com">
          <img
            src={zugetextet_logo}
            alt={"Logo von zugetextet.com"}
            className={"logo"}
          />
        </a>
      </div>
      <Navbar
        className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3 bg-black"
        container
        light
      >
        <NavbarToggler onClick={toggleNavbar} className="mr-2" />
        <Collapse
          className="d-sm-inline-flex flex-sm-row-reverse"
          isOpen={!collapsed}
          navbar
        >
          <ul className="navbar-nav flex-grow mx-auto">
            {!isLoggedIn ? (
              <></>
            ) : (
              <>
                <NavItem>
                  <NavLink
                    tag={Link}
                    className="navhover text-light"
                    to="/fetch-data"
                  >
                    Einsendungen
                  </NavLink>
                </NavItem>

                <NavItem>
                  <NavLink
                    tag={Link}
                    className="navhover text-light"
                    to="/forms"
                  >
                    Ausschreibungen
                  </NavLink>
                </NavItem>
              </>
            )}

            <NavItem>
              <NavLink
                tag={Link}
                className="navhover text-light"
                to="/logindata"
              >
                {loginText}
              </NavLink>
            </NavItem>

            <NavItem></NavItem>
          </ul>
        </Collapse>
      </Navbar>
    </header>
  );
}
