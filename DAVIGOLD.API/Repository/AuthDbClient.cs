using DAVIGOLD.API.Model;
using DAVIGOLD.API.Utility;
using DAVIGOLD.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DAVIGOLD.API.Repository
{
    public class AuthDbClient
    {
        

        /// <summary>
        /// Authenticate User name and password in Database
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>

        public Response<TokenModel> AuthenticateUser(IOptions<AppSettingModel> appSettings, AuthModel auth)
        {
            var response = new Response<TokenModel>();

            SqlParameter[] param = {
                new SqlParameter("@Email",auth.Email),
                new SqlParameter("@Password",auth.Password)
            };

            string query = "Declare @userID INT;   SET @userID=(SELECT UserID FROM [dbo].[User] WHERE Email=@Email AND PasswordHash=HASHBYTES('SHA2_512', @Password)) SELECT @userID as UserID;";


           var result = SqlHelper.ExecuteCommandReturnBool(appSettings.Value.ConnectionString, query, param);
            if (result)
            {
                response.ReturnMessage = "User logged In Successfull";
                response.Data = GenerateToken(auth, appSettings);
                return response;
            }
            response.ReturnMessage = "Email or password is incorrect";
            return response;
        }



        #region Helper Methods
        private TokenModel  GenerateToken(AuthModel Auth, IOptions<AppSettingModel> appSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(appSettings.Value.JWT.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.Email, Auth?.Email)
              }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenModel { Token = tokenHandler.WriteToken(token) };
        }
        #endregion
    }
}
