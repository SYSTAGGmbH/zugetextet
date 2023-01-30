import React from "react";
import {
  Column,
  DataGrid,
  FilterRow,
  Export,
  SearchPanel,
  GroupPanel,
  HeaderFilter,
  FilterPanel,
  Paging,
} from "devextreme-react/data-grid";
import { createStore } from "devextreme-aspnet-data-nojquery";
import { Button } from "devextreme-react/button";
import { Workbook } from "exceljs";
import saveAs from "file-saver";
import { exportDataGrid } from "devextreme/excel_exporter";

// stores data for dataGrid
// handles API GET Requests
const dataSource = createStore({
  key: "id",
  loadUrl: `/api/formdata`,
  onBeforeSend: (operation, ajaxOptions) => {
    if (operation === "load") {
      ajaxOptions.headers = {
        Authorization: localStorage.getItem("token"),
        "Content-type": "application/json",
        Accept: "application/json",
      };
    }
  },
});

export default function FetchData() {
  function renderFormDataTable() {
    return (
      <DataGrid
        dataSource={dataSource}
        remoteOperations={true}
        columnAutoWidth={true}
        rowAlternationEnabled={true}
        showBorders={true}
        onExporting={onExporting}
      >
        <Column
          caption={"Eingesendet am"}
          dataType={"date"}
          dataField={"creationDate"}
          format={"dd.MM.yyyy"}
          sortOrder={"desc"}
        />
        <Column caption={"Ausschreibung"} dataField={"formName"} />
        <Column caption={"Vorname"} dataField={"firstName"} />
        <Column caption={"Nachname"} dataField={"lastName"} />
        <Column caption={"Autorenname"} dataField={"authorName"} />
        <Column caption={"Geschlecht"} dataField={"gender"} />
        <Column caption={"E-Mail"} dataField={"email"} />
        <Column caption={"Straße"} dataField={"street"} />
        <Column caption={"PLZ"} dataField={"zipcode"} />
        <Column caption={"Stadt"} dataField={"city"} />
        <Column caption={"Minderjährig"} dataField={"isUnderage"} />
        <Column
          caption={"Teilnahmebedingungen akzeptiert"}
          dataField={"conditionsOfParticipationConsent"}
        />
        <Column
          caption={"Einwilligung zur Veröffentlichung"}
          dataField={"originatorAndPublicationConsent"}
        />
        <Column
          caption={"Download der Lyrik"}
          dataField={"lyrikDownloadUrl"}
          cellRender={cellRender}
          alignment={"center"}
        />
        <Column
          caption={"Download des Prosatexts"}
          dataField={"prosaDownloadUrl"}
          cellRender={cellRender}
          alignment={"center"}
        />
        <Column
          caption={"Download der Kurzbiographie"}
          dataField={"kurzbiographieDownloadUrl"}
          cellRender={cellRender}
          alignment={"center"}
        />
        <Column
          caption={"Download der Kurzbibliographie"}
          dataField={"kurzbibliographieDownloadUrl"}
          cellRender={cellRender}
          alignment={"center"}
        />
        <Column
          caption={"Download der Grafiken"}
          dataField={"zipImagesDownloadUrl"}
          cellRender={cellRender}
          alignment={"center"}
        />
        <Column
          caption={"Download der Bestätigung der Erziehungsberechtigten"}
          dataField={"parentalConsentDownloadUrl"}
          cellRender={cellRender}
          alignment={"center"}
        />

        <SearchPanel visible={true} />
        <GroupPanel visible={true} />
        <FilterRow visible={true} />
        <HeaderFilter visible={true} />
        <FilterPanel visible={false} />
        <Paging defaultPageSize={10} />

        <Export enabled={true} fileName={"Einsendungen"} />
      </DataGrid>
    );
  }

  // handles export to Excel
  function onExporting(e) {
    const columArray = [
      "isUnderage",
      "conditionsOfParticipationConsent",
      "originatorAndPublicationConsent",
    ];
    let urlColumnWidth = null;

    const workbook = new Workbook();
    const worksheet = workbook.addWorksheet("Einsendungen");
    exportDataGrid({
      component: e.component,
      worksheet: worksheet,
      customizeCell: function ({ gridCell, excelCell }) {
        if (gridCell.rowType === "data") {
          if (gridCell.column.dataField.includes("DownloadUrl")) {
            if (gridCell.value !== "") {
              if (urlColumnWidth === null) {
                urlColumnWidth = gridCell.value.length + 1;
              }
              excelCell.value = {
                text: gridCell.value,
                hyperlink: gridCell.value,
              };
              excelCell.alignment = { horizontal: "left" };
              excelCell.font = { color: { argb: "FF0000FF" }, underline: true };
            }
          } else if (columArray.includes(gridCell.column.dataField)) {
            if (gridCell.value !== "") {
              excelCell.value = gridCell.value === true ? "✓" : "";
              excelCell.alignment = { horizontal: "center" };
            }
          }
        }
      },
    }).then(function () {
      for (let i = 13; i < 19; i++) {
        worksheet.columns[i].width =
          urlColumnWidth === null ? 40 : urlColumnWidth;
      }
      workbook.xlsx.writeBuffer().then(function (buffer) {
        saveAs(
          new Blob([buffer], { type: "application/octet-stream" }),
          "Einsendungen.xlsx"
        );
      });
    });
    e.cancel = true;
  }

  // Render download-Button only if download-Url exists
  function cellRender(data) {
    if (data.displayValue.length === 0) {
      return "";
    }

    const url = data.displayValue;
    return <Button icon="download" onClick={() => saveAs(url)} />;
  }

  return (
    <div>
      <h1 id="tabelLabel">Einsendungen</h1>
      {renderFormDataTable()}
    </div>
  );
}
