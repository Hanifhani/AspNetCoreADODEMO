using DAVIGOLD.API.Model;
using DAVIGOLD.API.Utility;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace DAVIGOLD.API.Repository
{
    public class CompanyDbClient
    {

        public Response<int> AddCompany(CompanyModel company, IOptions<AppSettingModel> appSetting)
        {
            var response = new Response<int>();
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
           var result = SqlHelper.ExecuteCommandReturnInt(appSetting.Value.ConnectionString, query, param);
            response.Data = result;
            response.IsSuccess = result > 0;
            response.ReturnMessage = result > 0 ? "Company Created Successfully" : "Company creation failed";
            return response;


        }
    }
}
