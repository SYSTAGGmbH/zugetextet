using Microsoft.AspNetCore.Mvc;
using zugetextet.formulare.Data.Models;
using zugetextet.formulare.Services;
using zugetextet.formulare.Services.Implementation;

namespace zugetextet.formulare.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {

        private readonly IAttachmentVersionService _attachmentVersionService;
        
        public DownloadController(IAttachmentVersionService attachmentVersionService)
        {
            _attachmentVersionService = attachmentVersionService;
        }

        /// <summary>
        ///     Download the file with the specified Guid.
        /// </summary>
        /// <param name="guid">The guid of the file to be downloaded.</param>
        /// <returns>The file as file stream.</returns>
        [HttpGet("{guid}")]
        public IActionResult ReturnFileWith(Guid guid)
        {
            // Search for FileBytes in AttachmentVersion with same guid
            AttachmentVersion attachmentVersion = _attachmentVersionService.GetAttachmentVersion(guid);
            byte[] byteArr = _attachmentVersionService.GetFileBytes(attachmentVersion);

            MemoryStream stream = new MemoryStream(byteArr);

            return new FileStreamResult(stream, attachmentVersion.MimeType)
            {
                FileDownloadName = FormDataService.RemoveInvalidUnderscores(attachmentVersion.FileName),
            };

        }
    }
}
