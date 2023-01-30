using zugetextet.formulare.Data.Models;
using zugetextet.formulare.DTOs;

namespace zugetextet.formulare.Services
{
    public interface IAttachmentService
    {
        Task<AttachmentDto> CreateAttachment(AttachmentDto attachmentDto);
    }
}
