import React, { useRef, useEffect } from "react";
import {
  loadCaptchaEnginge,
  LoadCanvasTemplate,
  validateCaptcha,
} from "react-simple-captcha";
import "./CaptchaCode.css";
import ReactDOM from "react-dom";
import Button from "devextreme-react/button";
import { notifyFailure } from "../Notify";

export default function CaptchaCode({ open, handleSubmit, closePopup }) {
  let user_captcha_input = useRef(null);

  useEffect(() => {
    if (open) {
      user_captcha_input.current.focus();
      loadCaptchaEnginge(6);
    }
  }, [open]);

  if (open === false) return null; //dont show popup if open === false

  return ReactDOM.createPortal(
    <div className="popup-container">
      <div className="popup-body">
        <LoadCanvasTemplate />
        <label>
          <input id="user_captcha_input" ref={user_captcha_input} type="text" />
        </label>
        <>
          <Button
            className="user_captcha_btn"
            text="Absenden"
            onClick={validateUserCaptchaValue}
          />
          <Button
            className="user_captcha_btn"
            text="Abbrechen"
            onClick={closePopup}
          />
        </>
      </div>
    </div>,
    document.getElementById("portal")
  );

  function validateUserCaptchaValue() {
    if (validateCaptcha(user_captcha_input.current.value) === true) {
      loadCaptchaEnginge(6);
      handleSubmit();
    } else {
      notifyFailure("Der Text wurde inkorrekt ausgefüllt ");
    }
  }
}
