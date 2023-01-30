using zugetextet.formulare.DTOs;

namespace zugetextet.formulare.Services
{
    public interface IFormDataService
    {
        Task<FormDataDto> CreateFormData(FormDataDto formDto);
        Task<List<FormDataDto>> GetAllFormData();
        bool ValidateAttachments(FormDataDto formDataDto);
    }
}
