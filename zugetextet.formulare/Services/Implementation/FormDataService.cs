using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IO.Compression;
using System.Text;
using zugetextet.formulare.Data;
using zugetextet.formulare.Data.Models;
using zugetextet.formulare.DTOs;
using zugetextet.formulare.Settings;

namespace zugetextet.formulare.Services.Implementation
{
    public class FormDataService : IFormDataService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAttachmentService _attachmentService;

        private readonly IAttachmentVersionService _attachmentVersionService;

        public FormDataService(ApplicationDbContext context, IAttachmentService attachmentService, IAttachmentVersionService attachmentVersionService)
        {

            _attachmentService = attachmentService;
            _context = context;
            _attachmentVersionService = attachmentVersionService;
        }

        public async Task<FormDataDto> CreateFormData(FormDataDto formDataDto)
        {
            var formData = new FormData()
            {
                FormId = formDataDto.FormId,
                FirstName = formDataDto.FirstName,
                LastName = formDataDto.LastName,
                AuthorName = formDataDto.AuthorName,
                Gender = formDataDto.Gender,
                Email = formDataDto.Email,
                Street = formDataDto.Street,
                Zipcode = formDataDto.Zipcode,
                City = formDataDto.City,
                IsUnderage = formDataDto.IsUnderage,
                ConditionsOfParticipationConsent = formDataDto.ConditionsOfParticipationConsent,
                OriginatorAndPublicationConsent = formDataDto.OriginatorAndPublicationConsent,
                CreationDate = DateTime.Today,
                
            };

            _context.Add(formData);
            await _context.SaveChangesAsync();
            formDataDto.Id = formData.Id;

            Guid? lyrikId = null;
            Guid? prosaTextId = null;
            Guid? kurzbiographieId = null;
            Guid? kurzbibliographie = null;
            Guid? images = null;
            Guid? parentalConsent = null;

            foreach (var attachmentDto in formDataDto.Attachments)
            {
                // Ignore attachments of type "Grafiken" or "Lyrik"
                // Attachments of tyype "Grafiken" or "Lyrik" are handled later,
                // because they might need to be zipped
                if (attachmentDto.Type == "Grafiken" || attachmentDto.Type == "Lyrik") 
                {
                    continue;
                }

                var attachment = await _attachmentService.CreateAttachment(attachmentDto);

                if (attachment.Type == "Prosa")
                {
                    prosaTextId = attachment.Id;
                }
                if (attachment.Type == "Kurzbiographie")
                {
                    kurzbiographieId = attachment.Id;
                }
                if (attachment.Type == "Kurzbibliographie")
                {
                    kurzbibliographie = attachment.Id;
                }
                if (attachment.Type == "ErlaubnisErziehungsberechtigter")
                {
                    parentalConsent = attachment.Id;
                }

                attachment.FileName = GenerateFileName(attachment, formDataDto);
                await _attachmentVersionService.CreateAttachmentVersion(attachment, formDataDto);
            }

            // Get all attachments of type "Lyrik"
            var lyrikAttachmentDtos = formDataDto.Attachments
                .Where((attachmentDto) => attachmentDto.Type == "Lyrik")
                .ToList();

            // Just save the "Lyrik" attachment if only one was submitted
            if (lyrikAttachmentDtos.Count == 1) {
                var attachmentDto = await _attachmentService.CreateAttachment(lyrikAttachmentDtos.First());
                lyrikId = attachmentDto.Id;
                attachmentDto.FileName = GenerateFileName(attachmentDto, formDataDto);
                await _attachmentVersionService.CreateAttachmentVersion(attachmentDto, formDataDto);
            } 
            // Zip the "Lyrik" attachments if there is more than one
            else if (lyrikAttachmentDtos.Count > 1)
            {
                var lyrikZipAttachmentDto = new AttachmentDto()
                {
                    FileName = "InitialFileName.zip",
                    FileBytes = ZipAttachments(lyrikAttachmentDtos),
                    MimeType = "application/zip",
                    Type = "Lyrik",
                };
                lyrikZipAttachmentDto.FileName = GenerateFileName(lyrikZipAttachmentDto, formDataDto);

                lyrikZipAttachmentDto = await _attachmentService.CreateAttachment(lyrikZipAttachmentDto);
                lyrikId = lyrikZipAttachmentDto.Id;
                await _attachmentVersionService.CreateAttachmentVersion(lyrikZipAttachmentDto, formDataDto);
            }

            // Get all attachments of type "Grafiken"
            var imagesAttachmentDtos = formDataDto.Attachments
                .Where((attachmentDto) => attachmentDto.Type == "Grafiken")
                .ToList();

            // Zip images if there are any
            if (!imagesAttachmentDtos.IsNullOrEmpty())
            {
                var imagesZipAttachmentDto = new AttachmentDto()
                {
                    FileName = "InitialFileName.zip",
                    FileBytes = ZipAttachments(imagesAttachmentDtos),
                    MimeType = "application/zip",
                    Type = "Grafiken",
                };
                imagesZipAttachmentDto.FileName = GenerateFileName(imagesZipAttachmentDto, formDataDto);

                imagesZipAttachmentDto = await _attachmentService.CreateAttachment(imagesZipAttachmentDto);
                images = imagesZipAttachmentDto.Id;
                await _attachmentVersionService.CreateAttachmentVersion(imagesZipAttachmentDto, formDataDto);
            }

            formData.Lyrik = lyrikId;
            formData.Prosa = prosaTextId;
            formData.Kurzbiographie = kurzbiographieId;
            formData.Kurzbibliographie = kurzbibliographie;
            formData.Images = images;
            formData.ParentalConsent = parentalConsent;

            _context.FormData.Update(formData);
            await _context.SaveChangesAsync();

            return formDataDto;
        }



        public async Task<List<FormDataDto>> GetAllFormData()
        {
            var formDataList = await _context.FormData
                .Include(formData => formData.Form)
                .ToListAsync();
            List<FormDataDto> formDataDtoList = new();

            foreach (FormData formData in formDataList)
            {
                formDataDtoList.Add(new FormDataDto()
                {
                    Id = formData.Id,
                    FormId = formData.FormId,
                    FormName = formData.Form.Name,
                    FirstName = formData.FirstName,
                    LastName = formData.LastName,
                    AuthorName = formData.AuthorName,
                    Gender = formData.Gender,
                    Email = formData.Email,
                    Street = formData.Street,
                    Zipcode = formData.Zipcode,
                    City = formData.City,
                    IsUnderage = formData.IsUnderage,
                    ConditionsOfParticipationConsent = formData.ConditionsOfParticipationConsent,
                    OriginatorAndPublicationConsent = formData.OriginatorAndPublicationConsent,
                    CreationDate = formData.CreationDate,
                    LyrikDownloadUrl = GenerateDownloadUrl(formData.Lyrik),
                    ProsaDownloadUrl = GenerateDownloadUrl(formData.Prosa),
                    KurzbiographieDownloadUrl = GenerateDownloadUrl(formData.Kurzbiographie),
                    KurzbibliographieDownloadUrl = GenerateDownloadUrl(formData.Kurzbibliographie),
                    ZipImagesDownloadUrl = GenerateDownloadUrl(formData.Images),
                    ParentalConsentDownloadUrl = GenerateDownloadUrl(formData.ParentalConsent),
                });
                
            }

            return formDataDtoList;
        }

        public bool ValidateAttachments(FormDataDto formDataDto)
        {
            var AppMetaData = Program.AppMetaData;

            foreach (var attachmentDto in formDataDto.Attachments)
            {
                IEnumerable<string> allowedFileExtensions = new List<string>();
                IEnumerable<string> allowedMimeTypes = new List<string>();

                switch(attachmentDto.Type)
                {
                    case "Lyrik":
                    case "Prosa":
                    case "Kurzbiographie":
                    case "Kurzbibliographie":
                        allowedFileExtensions = AppMetaData.AllowedFileExtensions;
                        allowedMimeTypes = AppMetaData.AllowedMimeTypes;
                        break;
                    case "Grafiken":
                        allowedFileExtensions = AppMetaData.AllowedImageFileExtensions;
                        allowedMimeTypes = AppMetaData.AllowedImageMimeTypes;
                        break;
                    case "ErlaubnisErziehungsberechtigter":
                        allowedFileExtensions = AppMetaData.AllowedParentalConsentFileExtensions;
                        allowedMimeTypes = AppMetaData.AllowedParentalConsentMimeTypes;
                        break;
                }
                
                // Return false if file extension isn't allowed
                if (!allowedFileExtensions.Any((fileExt) => attachmentDto.FileName.EndsWith(fileExt)))
                {
                    return false;
                }

                // Return false if mime type isn't allowed
                if (!allowedMimeTypes.Any((mimeType) => attachmentDto.MimeType == mimeType))
                {
                    return false;
                }
            }

            return true;
        }
        private string GenerateDownloadUrl(Guid? guid)
        {
            if(guid == null)
            {
                return string.Empty;
            }

            // Look for AttachmentVersion with highest Version
            AttachmentVersion? attachmentVersion = _attachmentVersionService.GetHighestAttachmentVersion((Guid)guid);

            if (attachmentVersion == null)
            {
                return string.Empty;
            }

            string domain = Program.AppMetaData.FrontendDomain;

            return $"{domain}/download/{attachmentVersion.Id}";
        }

        private static string GenerateFileName(AttachmentDto attachmentDto, FormDataDto formDataDto)
        {

            string fileExtension = Path.GetExtension(attachmentDto.FileName);
            string sanitizedFilename = SanitizeFileName($"{formDataDto.FirstName}_{formDataDto.LastName}_{attachmentDto.Type}{fileExtension}");

            return RemoveInvalidUnderscores(sanitizedFilename);
        }

        private static string SanitizeFileName(string fileName)
        {
            StringBuilder sanitizedFileName = new(fileName);
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                sanitizedFileName = sanitizedFileName.Replace(invalidChar.ToString(), "");
            }

            return sanitizedFileName.ToString();
        }

        // Remove duplicate underscores
        public static string RemoveInvalidUnderscores(string fileName)
        {
            // normilaze the strings because they seem not to match
            string normalizedFileName = fileName.Normalize(NormalizationForm.FormD);
            char myChar = '_';
            string normalizedChar = myChar.ToString().Normalize(NormalizationForm.FormD);

            List<int> indexes = new();

            // Delete "_"-char if its generated more than once
            for (int i = 0; i < normalizedFileName.Length; i++)
            {
                if (normalizedFileName[i].Equals(normalizedChar) && normalizedFileName[i + 1].Equals(normalizedChar))
                {
                    indexes.Add(i);
                }
            }

            // Start with highest index
            indexes.Sort();
            indexes.Reverse();

            foreach (int index in indexes)
            {
                normalizedFileName = normalizedFileName.Remove(index, 1);
            }

            return normalizedFileName;
        }

        private static byte[] ZipAttachments(List<AttachmentDto> attachmentDtos)
        {
            using var compressedFileStream = new MemoryStream();
            // Create an archive and store the stream in memory.
            using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
            {
                foreach (var attachmentDto in attachmentDtos)
                {
                    // Create a zip entry for each attachment
                    var zipEntry = zipArchive.CreateEntry(attachmentDto.FileName);

                    // Get the stream of the attachment
                    using var originalFileStream = new MemoryStream(attachmentDto.FileBytes);
                    using var zipEntryStream = zipEntry.Open();
                    //Copy the attachment stream to the zip entry stream
                    originalFileStream.CopyTo(zipEntryStream);
                }
            }

            return compressedFileStream.ToArray();
        }

    }
}
