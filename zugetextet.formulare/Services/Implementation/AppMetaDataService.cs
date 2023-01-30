using zugetextet.formulare.DTOs;

namespace zugetextet.formulare.Services.Implementation
{
    public class AppMetaDataService : IAppMetaDataService
    { 
        // Returns the app meta data relevant for the frontend
        public AppMetaDataDto GetFrontendMetaData()
        {
            var AppMetaData = Program.AppMetaData;

            return new AppMetaDataDto()
            {
                Version = AppMetaData.Version,
                ConditionsOfParticipationUrl = AppMetaData.ConditionsOfParticipationUrl,
                ImprintUrl = AppMetaData.ImprintUrl,
                PrivacyPolicyUrl = AppMetaData.PrivacyPolicyUrl,
                AllowedMimeTypes = string.Join(",", AppMetaData.AllowedMimeTypes),
                AllowedFileExtensions = AppMetaData.AllowedFileExtensions,
                AllowedFileExtensionsString = string.Join(",", AppMetaData.AllowedFileExtensions),
                AllowedImageMimeTypes = string.Join(",", AppMetaData.AllowedImageMimeTypes),
                AllowedImageFileExtensions = AppMetaData.AllowedImageFileExtensions,
                AllowedImageFileExtensionsString = string.Join(",", AppMetaData.AllowedImageFileExtensions),
                AllowedParentalConsentMimeTypes = string.Join(",", AppMetaData.AllowedParentalConsentMimeTypes),
                AllowedParentalConsentFileExtensions = AppMetaData.AllowedParentalConsentFileExtensions,
                AllowedParentalConsentFileExtensionsString = string.Join(",", AppMetaData.AllowedParentalConsentFileExtensions),
                MaxFileSize = AppMetaData.MaxFileSize,
                SubmitFormHeaderVisible = AppMetaData.SubmitFormHeaderVisible,
                SubmitFormFooterVisible = AppMetaData.SubmitFormFooterVisible,
            };
        }
    }
}
