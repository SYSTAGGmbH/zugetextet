import React, { useState, useEffect } from "react";

const AppMetaDataContext = React.createContext();

export function AppMetaDataProvider({ children }) {
  const [isLoading, setIsLoading] = useState(true);
  const [appMetaData, setAppMetaData] = useState({
    version: "",
    conditionsOfParticipationUrl: "",
    imprintUrl: "",
    privacyPolicyUrl: "",
    allowedMimeTypes: "",
    allowedFileExtensions: [],
    allowedFileExtensionsString: "",
    allowedImageMimeTypes: "",
    allowedImageFileExtensions: [],
    allowedImageFileExtensionsString: "",
    maxFileSize: 0,
    submitFormHeaderVisible: false,
    submitFormFooterVisible: false,
  });

  useEffect(() => {
    const abortController = new AbortController();

    async function getAppMetaData() {
      const response = await fetch("/api/appmetadata", {
        signal: abortController.signal,
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        },
      });

      const appMetaDataDto = await response.json();

      setAppMetaData({
        version: appMetaDataDto.version,
        conditionsOfParticipationUrl:
          appMetaDataDto.conditionsOfParticipationUrl,
        imprintUrl: appMetaDataDto.imprintUrl,
        privacyPolicyUrl: appMetaDataDto.privacyPolicyUrl,
        allowedMimeTypes: appMetaDataDto.allowedMimeTypes,
        allowedFileExtensions: appMetaDataDto.allowedFileExtensions,
        allowedFileExtensionsString: appMetaDataDto.allowedFileExtensionsString,
        allowedImageMimeTypes: appMetaDataDto.allowedImageMimeTypes,
        allowedImageFileExtensions: appMetaDataDto.allowedImageFileExtensions,
        allowedImageFileExtensionsString:
          appMetaDataDto.allowedImageFileExtensionsString,
        allowedParentalConsentMimeTypes:
          appMetaDataDto.allowedParentalConsentMimeTypes,
        allowedParentalConsentFileExtensions:
          appMetaDataDto.allowedParentalConsentFileExtensions,
        allowedParentalConsentFileExtensionsString:
          appMetaDataDto.allowedParentalConsentFileExtensionsString,
        maxFileSize: appMetaDataDto.maxFileSize,
        submitFormHeaderVisible: appMetaDataDto.submitFormHeaderVisible,
        submitFormFooterVisible: appMetaDataDto.submitFormFooterVisible,
      });
      setIsLoading(false);
    }

    getAppMetaData();

    return () => {
      abortController.abort();
    };
  }, []);

  return (
    <AppMetaDataContext.Provider
      value={{ isMetaDataLoading: isLoading, ...appMetaData }}
    >
      {children}
    </AppMetaDataContext.Provider>
  );
}

export default AppMetaDataContext;
