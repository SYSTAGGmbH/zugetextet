namespace zugetextet.formulare.DTOs
{
    public class AppMetaDataDto
    {
        public string Version { get; set; } = string.Empty;
        public string ConditionsOfParticipationUrl { get; set; } = string.Empty;
        public string ImprintUrl { get; set; } = string.Empty;
        public string PrivacyPolicyUrl { get; set; } = string.Empty;
        public string AllowedMimeTypes { get; set; } = string.Empty;
        public List<string> AllowedFileExtensions { get; set; } = new List<string>();
        public string AllowedFileExtensionsString { get; set; } = string.Empty;
        public string AllowedImageMimeTypes { get; set; } = string.Empty;
        public List<string> AllowedImageFileExtensions { get; set; } = new List<string>();
        public string AllowedImageFileExtensionsString { get; set; } = string.Empty;
        public string AllowedParentalConsentMimeTypes { get; set; } = string.Empty;
        public List<string> AllowedParentalConsentFileExtensions { get; set; } = new List<string>();
        public string AllowedParentalConsentFileExtensionsString { get; set; } = string.Empty;
        public int MaxFileSize { get; set; }
        public bool SubmitFormHeaderVisible { get; set; }
        public bool SubmitFormFooterVisible { get; set; }
    }
}
