using System.ComponentModel.DataAnnotations.Schema;

namespace zugetextet.formulare.Data.Models
{
    public class FormData
    {
        public Guid Id { get; set; }
        public Guid FormId { get; set; }
        public Forms Form { get; set; }
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

        public Guid? Lyrik { get; set; }      
        public Guid? Prosa { get; set; }
        public Guid? Kurzbiographie { get; set; }
        public Guid? Kurzbibliographie { get; set; }
        public Guid? Images { get; set; }
        public Guid? ParentalConsent { get; set; }

        [ForeignKey("Lyrik")]
        public Attachment? AttachmentLyrik { get; set; }

        [ForeignKey("Prosa")]
        public Attachment? AttachmentProsa { get; set; }

        [ForeignKey("Kurzbiographie")]
        public Attachment? AttachmentKurzbiographie { get; set; }

        [ForeignKey("Kurzbibliographie")]
        public Attachment? AttachmentKurzbibliographie { get; set; }

        [ForeignKey("Images")]
        public Attachment? AttachmentImages { get; set; }

        [ForeignKey("ParentalConsent")]
        public Attachment? AttachmentConsent { get; set; }


    }
}
