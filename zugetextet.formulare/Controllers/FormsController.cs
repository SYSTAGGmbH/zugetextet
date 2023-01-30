using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using zugetextet.formulare.DTOs;
using zugetextet.formulare.Services;

namespace zugetextet.formulare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {

        public readonly IFormService _formService;
        private readonly ITokenService _tokenService;

        public FormsController(IFormService formService, ITokenService tokenService)
        {
            _formService = formService;
            _tokenService = tokenService;
        }

        /// <summary>
        ///     Get all submission forms.
        /// </summary>
        /// <param name="loadOptions">The load options from the DevExtreme datagrid.</param>
        /// <returns>A list of submission forms that match the load options.</returns>
        [HttpGet]
        public async Task<ActionResult<LoadResult>> GetAllForms(DataSourceLoadOptions loadOptions)
        {
            //reads Token-Information from header
            string authHeader = HttpContext.Request.Headers["Authorization"];
            bool validToken = _tokenService.ValidateCurrentToken(authHeader);
            if (validToken == false)
                return Unauthorized("Invalider Token. Bitte melden Sie sich erneut an");

            var source = await _formService.GetAllForms();

            loadOptions.PrimaryKey = new[] { "Id" };
            loadOptions.PaginateViaPrimaryKey = true;
            var result = DataSourceLoader.Load(source, loadOptions);

            return Ok(result);
        }

        /// <summary>
        ///     Get a specific submission form.
        /// </summary>
        /// <param name="formId">The guid of the submission form.</param>
        /// <returns>The submission form.</returns>
        [HttpGet("{formId}")]
        public async Task<ActionResult<FormDto>> GetForm(Guid formId)
        {
            FormDto? form = await _formService.GetForm(formId);

            if (form == null)
            {
                return NotFound();
            }
            
            return Ok(form);
        }

        /// <summary>
        ///     Creates a new submission form.
        /// </summary>
        /// <param name="formDto">The data for creating a new submission form.</param>
        /// <returns>The created submission form.</returns>
        [HttpPost("Create")]
        public async Task<ActionResult<FormDto>> CreateForm(FormDto formDto)
        {
            //reads Token-Information from header
            string authHeader = HttpContext.Request.Headers["Authorization"];
            bool validToken = _tokenService.ValidateCurrentToken(authHeader);

            if(validToken == false)
                return Unauthorized("Kein gültiger Token. Bitte melden Sie sich erneut an");

            if (formDto.Name.Length <= 0)
            {
                return BadRequest("Bitte einen Namen für die Ausschreibung angeben");
            } else if (formDto.From >= formDto.Until)
            {
                return BadRequest("Bitte ein Startdatum das vor dem Enddatum liegt wählen");
            }

            return Ok(await _formService.CreateForm(formDto));
        }

        /// <summary>
        ///     Updates a submission form.
        /// </summary>
        /// <param name="formDto">The data for updating a submission form.</param>
        /// <returns>The updated submission form.</returns>
        [HttpPut("Update")]
        public async Task<ActionResult<FormDto>> UpdateForm(FormDto formDto)
        {
            //reads Token-Information from header
            string authHeader = HttpContext.Request.Headers["Authorization"];
            bool validToken = _tokenService.ValidateCurrentToken(authHeader);

            if (validToken == false)
                return Unauthorized("Kein gültiger Token. Bitte melden Sie sich erneut an");

            FormDto? form = await _formService.UpdateForm(formDto);

            if (form == null)
            {
                return NotFound();
            }

            return Ok(form);
        }
    }
}
