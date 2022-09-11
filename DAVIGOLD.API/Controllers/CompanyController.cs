using DAVIGOLD.API.Model;
using DAVIGOLD.API.Repository;
using DAVIGOLD.API.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace DAVIGOLD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CompanyController : ControllerBase
    {

        [HttpPost("Add")]
        public IActionResult AddCompany(CompanyModel company, [FromServices] IOptions<AppSettingModel> appSetting)
        {
            var response = new Response<int>();
            try
            {
                #region Sql Params
                SqlParameter[] param =
                {

                 new SqlParameter("@Name",company.Name),
                 new SqlParameter("@Type",company.Type),
                 new SqlParameter("@Country",company.Country),
                 new SqlParameter("@Address",company.Address),
                 new SqlParameter("@City",company.City),
                 new SqlParameter("@State",company.State),
                 new SqlParameter("@ZipCode",company.ZipCode),
                 new SqlParameter("@CompanyMainLine",company.ZipCode),
                 };
                #endregion
                #region Query
                string query = @"INSERT INTO dbo.Company ( Country, ZipCode, City, State, CompanyMainLine, Name, Type ) VALUES ( @Country, @ZipCode, @City, @State, @CompanyMainLine, @Name, @Type ) SELECT Scope_Identity();";

                #endregion

                #region Execute Query
                var result = SqlHelper.ExecuteCommandReturnInt(appSetting.Value.ConnectionString, query, param);
                response.Data = result;
                response.IsSuccess = result > 0;
                response.ReturnMessage = result > 0 ? "Company Created Successfully" : "Company creation failed";

                #endregion

            }
            catch (Exception ex)
            {

                response.ReturnMessage = ex.ToString();
            }
            return Ok(response);
        }
    }
}
