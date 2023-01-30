namespace zugetextet.formulare.DTOs
{
    public class AttachmentDto
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }
        public string MimeType { get; set; }
        public string Type { get; set; }
    }
}
