using zugetextet.formulare.Data.Models;
using zugetextet.formulare.DTOs;

namespace zugetextet.formulare.Services
{
    public interface IAttachmentVersionService
    {
        Task CreateAttachmentVersion(AttachmentDto attachmentDto, FormDataDto formDataDto);
        AttachmentVersion GetAttachmentVersion(Guid formDataId);
        AttachmentVersion? GetHighestAttachmentVersion(Guid attachmentGuid);
        byte[] GetFileBytes(AttachmentVersion _attachmentVersion);
    }
}
