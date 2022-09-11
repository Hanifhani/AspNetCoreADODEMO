using DAVIGOLD.API.Model;
using DAVIGOLD.API.Repository;
using DAVIGOLD.API.Utility;
using DAVIGOLD.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DAVIGOLD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  IActionResult Index([FromBody] AuthModel auth, [FromServices] IOptions<AppSettingModel> appSettings)
        {
            #region Sql Params & Query
            var response = new Response<TokenModel>();

            SqlParameter[] param = {
                new SqlParameter("@Login",auth.Login),
                new SqlParameter("@Password",auth.Password)
            };

            string query = "Declare @userID INT;   SET @userID=(SELECT UserID FROM [dbo].[User] WHERE Login=@Login AND PasswordHash=HASHBYTES('SHA2_512', @Password+CAST(Salt AS NVARCHAR(36)))) SELECT @userID as UserID;";
            #endregion

            #region Execute Query
            var result = SqlHelper.ExecuteCommandReturnBool(appSettings.Value.ConnectionString, query, param);
            if (result)
            {
                response.ReturnMessage = "User logged In Successfull";
                response.IsSuccess = result;
                response.Data = GenerateToken(auth, appSettings);
                return Ok(response);
            }
            response.ReturnMessage = "Login or password is incorrect"; 
            #endregion
            //var response =  DbClientFactory<AuthDbClient>.Instance.AuthenticateUser(appSettings, auth);
            return Ok(response);
        }

        #region Helper Methods
        private TokenModel GenerateToken(AuthModel Auth, IOptions<AppSettingModel> appSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(appSettings.Value.JWT.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.UserData, Auth?.Login)
              }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenModel { Token = tokenHandler.WriteToken(token), Expires = token.ValidTo };
        }
        #endregion
    }
}
