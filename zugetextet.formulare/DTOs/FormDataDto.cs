namespace zugetextet.formulare.DTOs
{
    public class FormDataDto
    {
        public Guid Id { get; set; }
        public Guid FormId { get; set; }
        public string FormName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public bool IsUnderage { get; set; } = false;
        public bool ConditionsOfParticipationConsent { get; set; } = false;
        public bool OriginatorAndPublicationConsent { get; set; } = false;
        public DateTime CreationDate { get; set; }
        public List<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>();
        public string LyrikDownloadUrl { get; set; } = string.Empty;
        public string ProsaDownloadUrl { get; set; } = string.Empty;
        public string KurzbiographieDownloadUrl { get; set; } = string.Empty;
        public string KurzbibliographieDownloadUrl { get; set; } = string.Empty;
        public string ZipImagesDownloadUrl { get; set; } = string.Empty;
        public string ParentalConsentDownloadUrl { get; set; } = string.Empty;
    }
}
