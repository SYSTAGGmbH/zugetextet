import React from "react";
import { GroupItem } from "devextreme-react/form";
import { FileUploader } from "devextreme-react";
import "./SubmitForm.css";
import { validateFileUplopad, validateNumberOfFiles } from "./SubmitForm";

export default function files(
  colSpan,
  form,
  allowedFileExtensionsString,
  allowedFileExtensions,
  allowedImageFileExtensionsString,
  allowedImageFileExtensions,
  maxFileSize,
  attachmentsRef
) {
  return (
    <GroupItem caption={"Dateien"} colCount={4}>
      <GroupItem
        caption={"Prosatext"}
        colSpan={colSpan.prosa}
        visible={form.prosaIsVisible}
      >
        <FileUploader
          className="fileuploader-container"
          multiple={false}
          accept={allowedFileExtensionsString}
          allowedFileExtensions={allowedFileExtensions}
          maxFileSize={maxFileSize}
          onValueChanged={(e) => {
            if (!validateFileUplopad(e, e.value[0], allowedFileExtensions)) {
              return;
            }
            attachmentsRef.current.prosatext = e.value[0];
          }}
          uploadMode={"useForm"}
          selectButtonText="Datei auswählen"
          labelText="oder in das Feld ziehen"
        />
      </GroupItem>
      <GroupItem
        caption={"Lyriktext (Max. " + form.amountLyrik + ")"}
        colSpan={colSpan.lyrik}
        visible={form.amountLyrik === 0 ? false : true}
      >
        <FileUploader
          className="fileuploader-container"
          multiple={form.amountLyrik === 1 ? false : true}
          accept={allowedFileExtensionsString}
          allowedFileExtensions={allowedFileExtensions}
          maxFileSize={maxFileSize}
          onValueChanged={(e) => {
            if (!validateFileUplopad(e, e.value[0], allowedFileExtensions)) {
              return;
            }

            validateNumberOfFiles(
              e,
              form.amountLyrik,
              "Lyriktexte ausgewählt werden."
            );

            attachmentsRef.current.lyrik = e.value;
          }}
          uploadMode={"useForm"}
          selectButtonText="Datei auswählen"
          labelText="oder in das Feld ziehen"
        />
      </GroupItem>
      <GroupItem caption={"Kurzbiographie"} colSpan={2}>
        <FileUploader
          className="fileuploader-container"
          multiple={false}
          accept={allowedFileExtensionsString}
          allowedFileExtensions={allowedFileExtensions}
          maxFileSize={maxFileSize}
          onValueChanged={(e) => {
            if (!validateFileUplopad(e, e.value[0], allowedFileExtensions)) {
              return;
            }
            attachmentsRef.current.kurzbiographie = e.value[0];
          }}
          uploadMode={"useForm"}
          selectButtonText="Datei auswählen"
          labelText="oder in das Feld ziehen"
        />
      </GroupItem>
      <GroupItem caption={"Kurzbibliographie"} colSpan={2}>
        <FileUploader
          className="fileuploader-container"
          multiple={false}
          accept={allowedFileExtensionsString}
          allowedFileExtensions={allowedFileExtensions}
          maxFileSize={maxFileSize}
          onValueChanged={(e) => {
            if (!validateFileUplopad(e, e.value[0], allowedFileExtensions)) {
              return;
            }
            attachmentsRef.current.kurzbibliographie = e.value[0];
          }}
          uploadMode={"useForm"}
          selectButtonText="Datei auswählen"
          labelText="oder in das Feld ziehen"
        />
      </GroupItem>
      <GroupItem
        caption={"Grafiken"}
        colSpan={4}
        visible={form.imagesIsVisible ? true : false}
      >
        <FileUploader
          className="fileuploader-container"
          multiple={true}
          accept={allowedImageFileExtensionsString}
          allowedFileExtensions={allowedImageFileExtensions}
          maxFileSize={maxFileSize}
          onValueChanged={(e) => {
            for (let i = 0; i < e.value.length; i++) {
              if (
                !validateFileUplopad(e, e.value[i], allowedImageFileExtensions)
              ) {
                return;
              }
            }

            validateNumberOfFiles(e, 5, "Grafiken ausgewählt werden.");

            attachmentsRef.current.images = e.value;
          }}
          uploadMode={"useForm"}
          selectButtonText="Datei auswählen"
          labelText="oder in das Feld ziehen"
        />
      </GroupItem>
    </GroupItem>
  );
}
