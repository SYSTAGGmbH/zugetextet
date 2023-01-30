using Microsoft.EntityFrameworkCore;
using zugetextet.formulare.Data;
using zugetextet.formulare.Data.Models;
using zugetextet.formulare.DTOs;

namespace zugetextet.formulare.Services.Implementation
{
    public class AttachmentVersionService : IAttachmentVersionService
    {
        private readonly ApplicationDbContext _context;

        public AttachmentVersionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAttachmentVersion(AttachmentDto attachmentDto, FormDataDto formDataDto)
        {
            var attachmentVersion = new AttachmentVersion()
            {
                Id = new Guid(),
                FormDataId = formDataDto.Id,
                CreationDate = DateTime.Today,
                Version = 1,
                OriginalAttachmentId = attachmentDto.Id,
                FileName = attachmentDto.FileName,
                FileBytes = attachmentDto.FileBytes,
                MimeType = attachmentDto.MimeType,
                Type = attachmentDto.Type,
            };
            _context.Add(attachmentVersion);

            await _context.SaveChangesAsync();
        }

        public AttachmentVersion GetAttachmentVersion(Guid attachmentVersionGuid)
        {
            AttachmentVersion attachmentVersions = _context.AttachmentVersion
                .Include(x => x.FormData)
                .First((attachmentVersion) =>  attachmentVersion.Id == attachmentVersionGuid );

            return attachmentVersions;
        }

        public AttachmentVersion? GetHighestAttachmentVersion(Guid attachmentGuid)
        {
            AttachmentVersion? attachmentVersion = _context.AttachmentVersion
                .Include(x => x.FormData)
                .Where(attachmentVersion => attachmentVersion.OriginalAttachmentId == attachmentGuid)
                .ToList()
                .MaxBy(attachmentVersion => attachmentVersion.Version);
            
            return attachmentVersion;
        }

        public byte[] GetFileBytes(AttachmentVersion _attachmentVersion)
        {
            AttachmentVersion attachmentVersion = _context.AttachmentVersion.Single(attachmentVersion => attachmentVersion.Id == _attachmentVersion.Id);

            return attachmentVersion.FileBytes;
        }
    }
}
