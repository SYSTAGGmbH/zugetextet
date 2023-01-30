using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using zugetextet.formulare.DTOs;
using zugetextet.formulare.Services;

namespace zugetextet.formulare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormDataController : ControllerBase
    {
        private readonly IFormService _formService;
        private readonly IFormDataService _formDataService;
        private readonly ITokenService _tokenService;

        public FormDataController(IFormService formService, IFormDataService formDataService, ITokenService tokenService, IAttachmentVersionService attachmentVersionService)
        {
            _formService = formService;
            _formDataService = formDataService;
            _tokenService = tokenService;
        }

        /// <summary>
        ///     Get all submissions/FormDatas.
        /// </summary>
        /// <param name="loadOptions">The load options from the DevExtreme datagrid.</param>
        /// <returns>A list of submissions/FormDatas that match the load options.</returns>
        [HttpGet]
        public async Task<ActionResult<LoadResult>> GetAll(DataSourceLoadOptions loadOptions)
        {
            //reads Token-Information from header        
            string authHeader = HttpContext.Request.Headers["Authorization"];
            bool validToken = _tokenService.ValidateCurrentToken(authHeader);

            if (validToken == false)
                return Unauthorized("Invalider Token. Bitte melden Sie sich erneut an");

            var source = await _formDataService.GetAllFormData();

            loadOptions.PrimaryKey = new[] { "Id" };
            loadOptions.PaginateViaPrimaryKey = true;
            var result = DataSourceLoader.Load(source, loadOptions);

            return Ok(result);
        }

        /// <summary>
        ///     Creates a new submission/FormData.
        /// </summary>
        /// <param name="formDataDto">The submitted data.</param>
        /// <returns>The created submission/FormData.</returns>
        [RequestSizeLimit(800000000)]
        [HttpPost("Create")]
        public async Task<ActionResult<FormDataDto>> Create(FormDataDto formDataDto)
        {
            var form = await _formService.GetForm(formDataDto.FormId);

            if (form == null)
            {
                return BadRequest("Diese Ausschreibung existiert nicht.");
            }

            if (DateTime.Now < form.From || DateTime.Now > form.Until)
            {
                return BadRequest($"Einsedungen für diese Ausschreibung sind nur vom {form.From} bis zum {form.Until} möglich.");
            }

            if (!_formDataService.ValidateAttachments(formDataDto))
            {
                return BadRequest("Eine oder mehrere Dateien haben einen unerlaubten Dateityp.");
            }

            return Ok(await _formDataService.CreateFormData(formDataDto));
        }
    }
}
