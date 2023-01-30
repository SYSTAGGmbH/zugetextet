using Microsoft.AspNetCore.Mvc;
using zugetextet.formulare.Services;

namespace zugetextet.formulare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        ///     Checks if the token sent for authentification is valid.
        /// </summary>
        /// <returns>true if the token is valid, false otherwise.</returns>
        [HttpGet]
        public bool ValidateToken()
        {
            //reads Token-Information from header
            string authHeader = HttpContext.Request.Headers["Authorization"];

            if (authHeader == null)
                return false;

            bool validToken = _tokenService.ValidateCurrentToken(authHeader);

            if (validToken == false)
                return false;

            return true;
        }
    }
}
