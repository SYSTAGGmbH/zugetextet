import React from "react";
import DataGrid, {
  Column,
  Editing,
  Popup,
  Paging,
  Form,
} from "devextreme-react/data-grid";
import "devextreme-react/text-area";
import { Item, RequiredRule, PatternRule } from "devextreme-react/form";
import "./Forms.css";
import { useRef } from "react";
import { createStore } from "devextreme-aspnet-data-nojquery";
import { sendTokenForValidation } from "../Token";
import { Button } from "devextreme-react/button";

// stores data for dataGrid
// handles API POST-GET-UPDATE Requests
let formsSource = createStore({
  key: "id",
  loadUrl: `/api/forms`,
  insertUrl: "/api/forms/create",
  updateUrl: "/api/forms/update",
  onBeforeSend: (operation, ajaxOptions) => {
    if (operation === "load") {
      ajaxOptions.headers = {
        Authorization: localStorage.getItem("token"),
      };
    } else if (operation === "insert") {
      ajaxOptions.headers = {
        Authorization: localStorage.getItem("token"),
        "Content-type": "application/json",
        Accept: "application/json",
      };
      ajaxOptions.data = ajaxOptions.data.values;
    } else if (operation === "update") {
      ajaxOptions.headers = {
        Authorization: localStorage.getItem("token"),
        "Content-type": "application/json",
        Accept: "application/json",
      };
      ajaxOptions.data = JSON.stringify({
        id: ajaxOptions.data.key,
        ...JSON.parse(ajaxOptions.data.values),
      });
    }
  },
});

export default function Forms({ setIsLoggedIn, setIsValidating }) {
  let dataGridRef = useRef(null);

  return (
    <div id="data-grid-demo">
      <h1 id="tabelLabel">Ausschreibungen</h1>

      <DataGrid
        ref={dataGridRef}
        dataSource={formsSource}
        showBorders={true}
        remoteOperations={true}
        columnAutoWidth={true}
        rowAlternationEnabled={true}
        onRowUpdating={onRowUpdating}
      >
        <Editing
          mode="popup"
          allowUpdating={true}
          allowAdding={true}
          allowDeleting={false}
          useIcons={true}
        >
          <Popup
            title="Ausschreibungen"
            showTitle={true}
            width={700}
            height={525}
            onHidden={validateToken}
          ></Popup>
          <Form>
            <Item itemType="group" colCount={1} colSpan={2}>
              <Item dataField="name">
                <RequiredRule message="Name muss angegeben werden" />
                <PatternRule
                  message="Name darf keine Zeichen und Sonderzeichen enthalten"
                  pattern={/^[^0-9]+$/}
                />
              </Item>
              <Item dataField="from" format={"dd.MM.yyyy"}>
                <RequiredRule message="Startdatum muss angegeben werden" />
              </Item>
              <Item dataField="until" format={"dd.MM.yyyy"}>
                <RequiredRule message="Enddatum muss angegeben werden" />
              </Item>
              <Item dataField="prosaIsVisible"></Item>
              <Item dataField="imagesIsVisible"></Item>
              <Item
                dataField="amountLyrik"
                editorType="dxSlider"
                editorOptions={{
                  min: 0,
                  max: 5,
                  label: { visible: true },
                  tooltip: {
                    enabled: true,
                    showMode: "always",
                    position: "bottom",
                  },
                }}
              >
                <RequiredRule message="Maximale Anzahl an Lyriktexten muss festgelegt werden" />
              </Item>
            </Item>
          </Form>
        </Editing>
        <Column
          caption={"Name"}
          dataField={"name"}
          cssClass={"align-vertical"}
        />
        <Column
          caption={"Von"}
          dataField={"from"}
          dataType={"date"}
          format={"dd.MM.yyyy"}
          cssClass={"align-vertical"}
        />
        <Column
          caption={"Bis"}
          dataField={"until"}
          dataType={"date"}
          format={"dd.MM.yyyy"}
          sortOrder={"desc"}
          cssClass={"align-vertical"}
        />
        <Column
          caption={"Erstellt am"}
          dataField={"creationDate"}
          dataType={"date"}
          format={"dd.MM.yyyy"}
          cssClass={"align-vertical"}
        />
        <Column
          dataField={"url"}
          cellRender={cellRender}
          alignment={"center"}
          cssClass={"align-vertical"}
        />

        <Column
          caption={"Prosatext sichtbar"}
          dataField={"prosaIsVisible"}
          dataType={"boolean"}
          cssClass={"align-vertical"}
        />
        <Column
          caption={"Grafiken sichtbar"}
          dataField={"imagesIsVisible"}
          dataType={"boolean"}
          cssClass={"align-vertical"}
        />
        <Column
          caption={"Max. Anzahl Lyriktexte"}
          dataField={"amountLyrik"}
          dataType={"number"}
          alignment={"center"}
          cssClass={"align-vertical"}
        />
        <Paging enabled={true} defaultPageSize={10} />
      </DataGrid>
    </div>
  );

  // convert Url into Buttons
  function cellRender(data) {
    return (
      <>
        <Button
          icon="link"
          elementAttr={{ class: "button-margin" }}
          onClick={() => window.open(data.value)}
        />
        <Button
          icon="copy"
          elementAttr={{ class: "button-margin" }}
          onClick={() => navigator.clipboard.writeText(data.value)}
        />
      </>
    );
  }

  function validateToken() {
    sendTokenForValidation({ setIsLoggedIn, setIsValidating });
  }
}

// Send all data to backend, not just the changed values
function onRowUpdating(e) {
  for (var property in e.oldData) {
    if (!e.newData.hasOwnProperty(property)) {
      e.newData[property] = e.oldData[property];
    }
  }
}
