using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using zugetextet.formulare.Data.Models;
using zugetextet.formulare.DTOs;
using zugetextet.formulare.Services;

namespace zugetextet.formulare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginDataController : ControllerBase
    {
        private readonly ILoginDataService _loginDataService;
        private readonly ITokenService _tokenService;

        public LoginDataController(ILoginDataService loginDataService, ITokenService tokenService)
        {
            _loginDataService = loginDataService;
            _tokenService = tokenService;
        }

        /// <summary>
        ///     Logs in a user.
        /// </summary>
        /// <param name="loginData">Username and password of the user.</param>
        /// <returns>A JWT for future authentification.</returns>
        [HttpPost]
        public ActionResult<LoginData> Login(LoginData loginData)
        {
            //Search for loginData with that username
            LoginData db_loginData = _loginDataService.GetLoginData(loginData.Username);

            //no user found
            if (db_loginData == null)
            {
                return BadRequest("Inkorrekter Benutzername");
            }

            String password = db_loginData.Password;
            String sha512_hashed_pw = _loginDataService.CreateSHA512(loginData.Password);

            if (sha512_hashed_pw != password)
            {
                return BadRequest("Benutzername und Passwort stimmen nicht überein");
            }

            string token = _tokenService.GenerateToken();

            return Ok(token);
        }
    }
}
