using zugetextet.formulare.DTOs;

namespace zugetextet.formulare.Services
{
    public interface IFormService
    {
        Task<FormDto> CreateForm(FormDto formDto);
        Task<List<FormDto>> GetAllForms();
        Task<FormDto?> GetForm(Guid formId);
        Task<FormDto?> UpdateForm(FormDto formDto);
    }
}
