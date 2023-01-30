using zugetextet.formulare.Data;
using zugetextet.formulare.Data.Models;
using zugetextet.formulare.DTOs;

namespace zugetextet.formulare.Services.Implementation
{
    public class AttachmentService : IAttachmentService
    {
        private readonly ApplicationDbContext _context;

        public AttachmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AttachmentDto> CreateAttachment(AttachmentDto attachmentDto)
        {
            var attachment = new Attachment()
            {
                Id = new Guid(),
                CreationDate = DateTime.Today,
            };

            _context.Add(attachment);
            await _context.SaveChangesAsync();

            attachmentDto.Id = attachment.Id;
            attachmentDto.CreationDate = attachment.CreationDate;

            return attachmentDto;
        }
    }
}
