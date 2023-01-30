import { useContext } from "react";
import AppMetaDataContext from "../context/AppMetaDataContext";

// custom hook for using AppMetaDataContext
export default function useAppMetaData() {
  const appMetaData = useContext(AppMetaDataContext);

  return appMetaData;
}
