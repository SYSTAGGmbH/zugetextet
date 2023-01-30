using Microsoft.AspNetCore.Mvc;
using zugetextet.formulare.DTOs;
using zugetextet.formulare.Services;

namespace zugetextet.formulare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppMetaDataController : ControllerBase
    {
        private readonly IAppMetaDataService _metaDataService;

        public AppMetaDataController(IAppMetaDataService metaDataService)
        {
            _metaDataService = metaDataService;
        }

        /// <summary>
        ///     Gets the meta data that is relevant for the frontend.
        /// </summary>
        /// <returns>The meta data.</returns>
        [HttpGet]
        public ActionResult<AppMetaDataDto> GetAppMetaData()
        {
            return _metaDataService.GetFrontendMetaData();
        }
    }
}
