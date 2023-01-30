import React from "react";
import {
  SimpleItem,
  GroupItem,
  Label,
  RequiredRule,
} from "devextreme-react/form";
import { FileUploader } from "devextreme-react";
import "./SubmitForm.css";
import { validateFileUplopad } from "./SubmitForm";

export default function consents(
  isUnderage,
  setIsUnderage,
  allowedParentalConsentFileExtensionsString,
  allowedParentalConsentFileExtensions,
  maxFileSize,
  attachmentsRef
) {
  return (
    <GroupItem caption={"Zustimmungen"}>
      <SimpleItem
        colSpan={1}
        dataField={"isUnderage"}
        editorType={"dxCheckBox"}
        editorOptions={{
          text: "Ich bin unter 18 Jahre alt.",
          value: isUnderage,
          onValueChanged: () => setIsUnderage(!isUnderage),
        }}
        cssClass={"checkbox"}
      >
        <Label
          text={"Ich bin unter 18 Jahre alt."}
          location={"right"}
          showColon={false}
          visible={false}
        />
      </SimpleItem>

      <GroupItem colSpan={3} cssClass="underage-file-upload">
        <FileUploader
          className="fileuploader-container"
          name="Bestätigung des Erziehungsberechtigten"
          disabled={!isUnderage}
          multiple={false}
          accept={allowedParentalConsentFileExtensionsString}
          allowedFileExtensions={allowedParentalConsentFileExtensions}
          maxFileSize={maxFileSize}
          onValueChanged={(e) => {
            if (
              !validateFileUplopad(
                e,
                e.value[0],
                allowedParentalConsentFileExtensions
              )
            ) {
              return;
            }
            attachmentsRef.current.parentalConsent = e.value[0];
          }}
          uploadMode={"useForm"}
          labelText="Schriftliche Bestätigung meines/r Erziehungsberechtigten"
          selectButtonText="Datei auswählen"
        />
      </GroupItem>

      <SimpleItem
        colSpan={1}
        dataField={"conditionsOfParticipationConsent"}
        editorType={"dxCheckBox"}
        editorOptions={{
          text: "Hiermit bestätige ich, dass ich die Teilnahmebedingungen gelesen habe und ihnen zustimme.",
        }}
        cssClass={"checkbox"}
      >
        <Label
          text={" "}
          location={"right"}
          alignment={"left"}
          showColon={false}
          visible={true}
        />
        <RequiredRule message={"Zustimmung erforderlich"} />
      </SimpleItem>

      <SimpleItem
        colSpan={1}
        dataField={"originatorAndPublicationConsent"}
        editorType={"dxCheckBox"}
        editorOptions={{
          text: "Hiermit bestätige ich, dass ich der Urheber meiner Texte und Werke bin und ihrer Veröffentlichung in Magazin und Blog zustimme.",
        }}
        cssClass={"checkbox"}
      >
        <Label
          text={" "}
          location={"right"}
          alignment={"left"}
          showColon={false}
          visible={true}
        />
        <RequiredRule message={"Zustimmung erforderlich"} />
      </SimpleItem>
    </GroupItem>
  );
}
