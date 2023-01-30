using System.ComponentModel.DataAnnotations.Schema;

namespace zugetextet.formulare.Data.Models
{
    public class AttachmentVersion
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public int Version { get; set; }

        public Guid FormDataId { get; set; }

        [ForeignKey("FormDataId")]
        public FormData FormData { get; set; }

        public Guid OriginalAttachmentId { get; set; }

        [ForeignKey("OriginalAttachmentId")]
        public Attachment OriginalAttachment { get; set; }
        public string FileName { get; set; }

        public byte[] FileBytes { get; set; }

        public string MimeType { get; set; }

        public string Type { get; set; }

    }
}
