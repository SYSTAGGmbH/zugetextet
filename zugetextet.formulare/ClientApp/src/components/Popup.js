import React from "react";
import "./CaptchaCode/CaptchaCode.css";
import ReactDOM from "react-dom";

export default function Popup({ children, open }) {
  //dont show popup if open === false
  if (open === false) return null;

  return ReactDOM.createPortal(
    <div className="popup-container">
      <div className="popup-body">{children}</div>
    </div>,
    // get the element defined in index.html
    document.getElementById("portal")
  );
}
