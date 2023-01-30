import React, { useRef, useEffect, useState } from "react";
import Form, { ButtonItem } from "devextreme-react/form";
import { notifyFailure, notifySuccess } from "../Notify";
import { useNavigate, useParams } from "react-router-dom";
import "./SubmitForm.css";
import CaptchaCode from "../CaptchaCode/CaptchaCode";
import useAppMetaData from "../../hooks/useAppMetaData";
import { Buffer } from "buffer";
import Popup from "../Popup";
import LoadingSpinner from "../LoadingSpinner";
import contact from "./Contact";
import files from "./Files";
import consents from "./Consents";

export default function SubmitForm() {
  const navigate = useNavigate();
  const params = useParams();
  const formDataRef = useRef({
    gender: "",
    firstName: "",
    lastName: "",
    street: "",
    zipcode: "",
    city: "",
    attachments: [],
    isUnderage: false,
    conditionsOfParticipationConsent: false,
    originatorAndPublicationConsent: false,
  });
  const attachmentsRef = useRef({
    prosatext: null,
    lyrik: null,
    kurzbiographie: null,
    kurzbibliographie: null,
    parentalConsent: null,
    images: null,
  });

  const colSpan = useRef({
    prosa: null,
    lyrik: null,
  });

  const [isLoading, setIsLoading] = useState(true);
  const [formNotFound, setFormNotFound] = useState(false);
  const [form, setForm] = useState({});
  const [isUnderage, setIsUnderage] = useState(false);
  const [open, setOpen] = useState(false); //CaptchaCode
  const [loadingSpinngerPopup, setLoadingSpinngerPopup] = useState(false);

  const {
    allowedFileExtensions,
    allowedFileExtensionsString,
    allowedImageFileExtensions,
    allowedImageFileExtensionsString,
    allowedParentalConsentFileExtensions,
    allowedParentalConsentFileExtensionsString,
    maxFileSize,
  } = useAppMetaData();

  useEffect(() => {
    getForm();
  }, []);

  return isLoading ? <div></div> : renderPage();

  function renderPage() {
    if (formNotFound) {
      return (
        <div className="fs-1 d-flex aligns-items-center justify-content-center">
          Formular nicht gefunden.
        </div>
      );
    }

    return Date.now() > Date.parse(form.until) ||
      Date.now() < Date.parse(form.from)
      ? renderErrorSite()
      : renderSubmitForm();
  }

  function renderErrorSite() {
    return (
      <div>
        <h1>
          Einsendungen für "{form.name}" sind nur im Zeitraum von{" "}
          {new Date(Date.parse(form.from)).toLocaleDateString()} bis{" "}
          {new Date(Date.parse(form.until)).toLocaleDateString()} möglich.
        </h1>
      </div>
    );
  }

  function renderSubmitForm() {
    return (
      <form action="submitForm" onSubmit={openCaptchaCode}>
        <CaptchaCode
          open={open}
          handleSubmit={handleSubmit}
          closePopup={() => setOpen(false)}
        />
        <Popup open={loadingSpinngerPopup}>
          <LoadingSpinner />
        </Popup>
        <h1>
          {form.name} - Einsendeschluss am{" "}
          {new Date(Date.parse(form.until)).toLocaleDateString()}
        </h1>
        <div className="required-disclaimer">
          Mit <span className="required-star">*</span> gekennzeichnete Felder
          sind Pflichtfelder.
        </div>
        <Form formData={formDataRef.current} style={{ paddingBottom: "10rem" }}>
          {contact()}
          {files(
            colSpan.current,
            form,
            allowedFileExtensionsString,
            allowedFileExtensions,
            allowedImageFileExtensionsString,
            allowedImageFileExtensions,
            maxFileSize,
            attachmentsRef
          )}
          {consents(
            isUnderage,
            setIsUnderage,
            allowedParentalConsentFileExtensionsString,
            allowedParentalConsentFileExtensions,
            maxFileSize,
            attachmentsRef
          )}

          <ButtonItem
            colSpan={3}
            buttonOptions={{
              text: "Absenden",
              type: "default",
              useSubmitBehavior: true,
            }}
          />
        </Form>
      </form>
    );
  }

  function getForm() {
    const url = "/api/forms/" + params.formId;
    fetch(url, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
    })
      .then((response) => {
        setIsLoading(false);

        if (response.status === 200) {
          return response.json();
        } else {
          setFormNotFound(true);
        }
      })
      .then((form) => {
        setForm(form);
        // determine width of prosa-FileUpload-Field based on the visibility of lyrik-FileUpload-Field
        // and vice-versa
        colSpan.current.prosa = form.amountLyrik === 0 ? 4 : 2;
        colSpan.current.lyrik = form.prosaIsVisible ? 2 : 4;
      })
      .catch(() => {});
  }

  function openCaptchaCode(e) {
    e.preventDefault();

    if (
      formDataRef.current.isUnderage &&
      (attachmentsRef.current.parentalConsent === null ||
        attachmentsRef.current.parentalConsent === undefined)
    ) {
      notifyFailure(
        "Unter 18 jährige müssen eine unterschriebene schriftliche Bestätigung" +
          " ihres/r Erziehungsberechtigten hochladen."
      );
      return;
    }

    setOpen(true);
  }

  async function handleSubmit() {
    setLoadingSpinngerPopup(true);
    setOpen(false);

    const attachments = [];

    if (attachmentsRef.current.prosatext !== null) {
      attachments.push(
        await generateAttachmentDto(attachmentsRef.current.prosatext, "Prosa")
      );
    }

    const lyrikAttachments = attachmentsRef.current.lyrik;
    if (lyrikAttachments !== null && lyrikAttachments.length > 0) {
      // loop over lyrikAttachment and add them to attachments-Array
      for (
        let i = 0, j = attachments.length;
        i < lyrikAttachments.length;
        i++, j++
      ) {
        attachments[j] = await generateAttachmentDto(
          lyrikAttachments[i],
          "Lyrik"
        );
      }
    }

    if (attachmentsRef.current.kurzbiographie !== null) {
      attachments.push(
        await generateAttachmentDto(
          attachmentsRef.current.kurzbiographie,
          "Kurzbiographie"
        )
      );
    }

    if (attachmentsRef.current.kurzbibliographie !== null) {
      attachments.push(
        await generateAttachmentDto(
          attachmentsRef.current.kurzbibliographie,
          "Kurzbibliographie"
        )
      );
    }

    const imageAttachments = attachmentsRef.current.images;
    if (imageAttachments !== null && imageAttachments.length > 0) {
      // loop over imageAttachments and add them to attachments-Array
      for (
        let i = 0, j = attachments.length;
        i < imageAttachments.length;
        i++, j++
      ) {
        attachments[j] = await generateAttachmentDto(
          imageAttachments[i],
          "Grafiken"
        );
      }
    }

    if (attachmentsRef.current.parentalConsent !== null) {
      attachments.push(
        await generateAttachmentDto(
          attachmentsRef.current.parentalConsent,
          "ErlaubnisErziehungsberechtigter"
        )
      );
    }

    const requestData = {
      formId: params.formId,
      ...formDataRef.current,
      attachments: attachments,
    };

    fetch("/api/formdata/create", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
      body: JSON.stringify(requestData),
    })
      .then((response) => {
        setLoadingSpinngerPopup(false);
        if (response.status === 200) {
          notifySuccess("Das Formular wurde erfolgreich übermittelt");
          navigate("/thank-you");
          return response.json();
        } else {
          notifyFailure(
            "Es ist ein Fehler beim Übermitteln des Formulars eingetreten "
          );
        }
      })
      .catch(() => {});
  }
}

// create JSON out of the file. Convert file into Base64-String
async function generateAttachmentDto(file, type) {
  if (file !== null) {
    return {
      type: type,
      mimeType: file.type,
      fileBytes: await convertFileToBase64(file),
      fileName: file.name,
    };
  }

  return null;
}

async function convertFileToBase64(file) {
  return Buffer.from(await file.arrayBuffer()).toString("base64");
}

// event is the devextreme file uploader "value changed" event
export function validateFileUplopad(event, file, allowedUploadFileExtensions) {
  if (file === undefined || file === null) {
    return false;
  }

  if (
    !allowedUploadFileExtensions.some((fileExtension, index) => {
      return file.name.endsWith(fileExtension);
    })
  ) {
    event.component.reset();
    notifyFailure(
      `Bitte eine Datei mit einer erlaubten Dateiendung auswählen. Gültig Dateiendungen: ${allowedUploadFileExtensions.join(
        ", "
      )}`,
      7000
    );
    return false;
  }

  return true;
}

export function validateNumberOfFiles(e, maxUploadSize, rightSideText) {
  if (e.value.length > maxUploadSize) {
    e.component.reset();
    notifyFailure(
      "Es dürfen nicht mehr wie " + maxUploadSize + " " + rightSideText
    );
    return;
  }
}
