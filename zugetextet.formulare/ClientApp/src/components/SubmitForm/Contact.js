import Form, {
  SimpleItem,
  GroupItem,
  Label,
  ButtonItem,
  RequiredRule,
  StringLengthRule,
  Item,
  EmailRule,
} from "devextreme-react/form";

export default function contact() {
  return (
    <GroupItem caption={"Kontakt und Anschrift"} colCount={3} colSpan={3}>
      <GroupItem colCount={1}>
        <SimpleItem dataField={"firstName"} colSpan={1}>
          <Label text={"Vorname"} />

          <RequiredRule message={"Bitte Vorname angeben"} />
          <StringLengthRule min={1} max={200} />
        </SimpleItem>
        <SimpleItem dataField={"lastName"} colSpan={1}>
          <Label text={"Nachname"} />

          <RequiredRule message={"Bitte Nachname angeben"} />
          <StringLengthRule min={1} max={200} />
        </SimpleItem>
        <SimpleItem dataField={"authorname"} colSpan={1}>
          <Label text={"Autorenname"} />

          <StringLengthRule min={0} max={200} />
        </SimpleItem>
        <Item
          editorType={"dxSelectBox"}
          editorOptions={{
            dataSource: ["Männlich", "Weiblich", "Divers"],
          }}
          dataField={"gender"}
        >
          <Label text={"Geschlecht"} />
          <RequiredRule message={"Bitte Geschlecht angeben"} />
        </Item>
      </GroupItem>

      <GroupItem colCount={1}>
        <SimpleItem dataField={"email"} colSpan={1}>
          <Label text={"Email"} />

          <RequiredRule message={"Bitte E-Mail Adresse angeben"} />
          <EmailRule message={"Bitte eine gültige E-Mail Adresse angeben"} />
          <StringLengthRule min={1} max={200} />
        </SimpleItem>

        <SimpleItem dataField={"street"} colSpan={1}>
          <Label text={"Straße"} />

          <RequiredRule message={"Bitte Straße angeben"} />
          <StringLengthRule min={1} max={200} />
        </SimpleItem>
        <SimpleItem dataField={"zipcode"} colSpan={1}>
          <Label text={"Postleitzahl"} />

          <RequiredRule message={"Bitte Postleizahl angeben"} />
          <StringLengthRule min={1} max={200} />
        </SimpleItem>
        <SimpleItem dataField={"city"} colSpan={1}>
          <Label text={"Ort"} />

          <RequiredRule message={"Bitte Ort angeben"} />
          <StringLengthRule min={1} max={200} />
        </SimpleItem>
      </GroupItem>

      <GroupItem colCount={1}></GroupItem>
    </GroupItem>
  );
}
