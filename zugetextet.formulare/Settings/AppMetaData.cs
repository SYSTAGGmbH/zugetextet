namespace zugetextet.formulare.Settings
{
    public class AppMetaData
    {
        public string Version { get; set; } = string.Empty;
        public string ConditionsOfParticipationUrl { get; set; } = string.Empty;
        public string ImprintUrl { get; set; } = string.Empty;
        public string PrivacyPolicyUrl { get; set; } = string.Empty;
        public List<string> AllowedMimeTypes { get; set; } = new List<string>();
        public List<string> AllowedFileExtensions { get; set; } = new List<string>();
        public List<string> AllowedImageMimeTypes { get; set; } = new List<string>();
        public List<string> AllowedImageFileExtensions { get; set; } = new List<string>();
        public List<string> AllowedParentalConsentMimeTypes { get; set; } = new List<string>();
        public List<string> AllowedParentalConsentFileExtensions { get; set; } = new List<string>();
        public int MaxFileSize { get; set; }
        public string InitialUsername { get; set; } = string.Empty;
        public string InitialPasswordSHA512Hash { get; set; } = string.Empty;
        public string TokenSecret { get; set; } = string.Empty;
        public string FrontendDomain { get; set; } = string.Empty;
        public bool SubmitFormHeaderVisible { get; set; }
        public bool SubmitFormFooterVisible { get; set; }
    }
}
