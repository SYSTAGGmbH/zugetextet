namespace zugetextet.formulare.DTOs
{
    public class FormDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
        public DateTime CreationDate { get; set; }
        public string Url { get => $"{Program.AppMetaData.FrontendDomain}/submit-form/{Id}"; }
        public bool ProsaIsVisible { get; set; } = false;
        public int AmountLyrik { get; set; } = 0;
        public bool ImagesIsVisible { get; set; } = false;
    }
}
