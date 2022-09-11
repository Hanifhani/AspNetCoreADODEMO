using DAVIGOLD.API.Model;
using DAVIGOLD.API.Repository;
using DAVIGOLD.API.Translator;
using DAVIGOLD.API.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Text.Json;

namespace DAVIGOLD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
       

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddContact(ContactsModel contact, [FromServices] IOptions<AppSettingModel> appSettings)
        {
            var response = new Response<int>();

            try
            {
                #region  Sql Params
                SqlParameter[] param =
                {
                new SqlParameter("@FirstName",contact.FirstName),
                new SqlParameter("@LastName",contact.LastName),
                new SqlParameter("@Title",contact.Title),
                new SqlParameter("@MiddleName",contact.MiddleName),
                new SqlParameter("@Notes",contact.Notes),
                new SqlParameter("@CompanyId",contact.CompanyId),
                new SqlParameter("@Position",contact.Position),
                new SqlParameter("@Department",contact.Department),
                new SqlParameter("@BusinessEmail",contact.BusinessEmail),
                new SqlParameter("@DirectLine",contact.DirectLine),
                new SqlParameter("@PrimaryMobile",contact.PrimaryMobile),
                new SqlParameter("@SecondaryMobile",contact.SecondaryMobile),
                new SqlParameter("@Country",contact.Country),
                new SqlParameter("@Address",contact.Address),
                new SqlParameter("@ZIPCode",contact.ZIPCode),
                new SqlParameter("@City",contact.City),
                new SqlParameter("@State",contact.State),
                new SqlParameter("@PersonalMobilePrimary",contact.PersonalMobilePrimary),
                new SqlParameter("@PersonalMobileSecondary",contact.PersonalMobileSecondary),
                new SqlParameter("@HomePhone",contact.HomePhone),
                new SqlParameter("@PersonalEmail",contact.PersonalEmail),
                new SqlParameter("@Linkedin",contact.Linkedin),
                new SqlParameter("@DateOfBirth", contact.DateOfBirth),
                new SqlParameter("@Types", contact.Type),
                new SqlParameter("@Tags",contact.Tags),
                new SqlParameter("@TeamKnowledge", string.Join(",", contact.TeamKnowledge))
            };
                #endregion

                #region Query

                string query = @"   DECLARE @ContactId INT = 0;
   INSERT INTO dbo.Contacts ( FirstName, LastName, Title, MiddleName, Notes, CompanyId, Position, Department, BusinessEmail, DirectLine, PrimaryMobile, SecondaryMobile, Country, Address, ZIPCode, City, State, PersonalMobile1, PersonalMobile2, HomePhone, PersonalEmail, Linkedin, DateOfBirth )
   VALUES
   (@FirstName, @LastName, @Title, @MiddleName, @Notes, @CompanyId, @Position, @Department, @BusinessEmail, @DirectLine,
 @PrimaryMobile, @SecondaryMobile, @Country, @Address, @ZIPCode, @City, @State, @PersonalMobilePrimary,
 @PersonalMobileSecondary, @HomePhone, @PersonalEmail, @Linkedin, @DateOfBirth);

	   SELECT @ContactId = SCOPE_IDENTITY();
	   --Types
	   INSERT INTO dbo.ContactTypes(ContactId,Type)
	   SELECT @ContactId, tuple  FROM dbo.CSVToTable(@Types, ',')

	   ---Tags
	   INSERT INTO dbo.ContactTags(ContactId,Tag)
	   SELECT @ContactId, tuple  FROM dbo.CSVToTable(@Tags, ',')
	   --Team Knowledge
	   INSERT INTO dbo.TeamKnowledge(ContactId, UserId)
	   SELECT @ContactId,CAST(tuple AS UNIQUEIDENTIFIER) FROM dbo.CSVToTable(@TeamKnowledge, ','); SELECT @ContactId;";
                #endregion

                #region Execute Query
                var result = SqlHelper.ExecuteCommandReturnInt(appSettings.Value.ConnectionString, query, param);
                response.Data = result;
                response.IsSuccess = result > 0;
                response.ReturnMessage = result > 0 ? "Contact Created Successfully" : "Contact creaton failed";
                #endregion

                
            }
            catch (Exception ex )
            {

                response.ReturnMessage = ex.ToString();
            }
            return Ok(response);
        }


        [HttpGet("List")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddContact([FromQuery] ContactQueryStringParameter paramters, [FromServices] IOptions<AppSettingModel> appSettings)
        {
          

            #region  Sql Params
            SqlParameter[] param =
            {

     new SqlParameter("@PageNo",paramters.PageNumber),
     new SqlParameter("@PageSize",paramters.PageSize),
     new SqlParameter("@Country",string.IsNullOrEmpty(paramters.Country) ? DBNull.Value : paramters.Country ),
     new SqlParameter("@Type",string.IsNullOrEmpty(paramters.Type) ? DBNull.Value : paramters.Type)
            };
            #endregion


           

            #region Query
            string query = @"   DROP TABLE IF EXISTS #tmpTypes;
            SELECT ct.ContactId,
                   STRING_AGG(ct.Type, ',') Type
            INTO #tmpTypes
            FROM dbo.ContactTypes ct
            WHERE (
                      ISNULL(@Type, '') = ''
                      OR ct.Type = @Type
                  )
            GROUP BY ct.ContactId;



            SELECT ct.Id,
                   ct.FirstName,
                   ct.LastName,
                   ct.Title,
                   ct.MiddleName,
                   ct.Notes,
                   ct.CompanyId,
                   ct.Position,
                   ct.Department,
                   ct.BusinessEmail,
                   ct.DirectLine,
                   ct.PrimaryMobile,
                   ct.SecondaryMobile,
                   ct.Country,
                   ct.Address,
                   ct.ZIPCode,
                   ct.City,
                   ct.State,
                   ct.PersonalMobile1 PersonalMobilePrimary,
                   ct.PersonalMobile2 PersonalMobileSecondary,
                   ct.HomePhone,
                   ct.PersonalEmail,
                   ct.Linkedin,
                   ct.DateOfBirth,
                   ct.CreatedDate,
                   ct.ModifiedDate,
                   c.Name AS Company,
                   tt.Type,
                   (
                       SELECT STRING_AGG(tg.Tag, ',')
                       FROM dbo.ContactTags tg
                       WHERE tg.ContactId = ct.Id
                   ) Tags,
                   (
                       SELECT STRING_AGG(CAST(tm.UserId AS NVARCHAR(100)), ',')
                       FROM dbo.TeamKnowledge tm
                       WHERE tm.ContactId = ct.Id
                   ) TeamKnowledge
            FROM dbo.Contacts ct
                LEFT JOIN #tmpTypes tt
                    ON tt.ContactId = ct.Id
                LEFT JOIN dbo.Company c
                    ON c.Id = ct.CompanyId
            WHERE (
                      ISNULL(@Country, '') = ''
                      OR ct.Country = @Country
                  )
         AND (@Type = ''  OR @Type = tt.Type)
            ORDER BY c.CreatedDate desc
     OFFSET ((@PageNo - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY;";
            #endregion



           

          

            #region Execute Query & Pagination 
            var result = SqlHelper.ExtecuteCommandReturnData<List<ContactsModel>>(appSettings.Value.ConnectionString, query, x=> x.TranslateAsContactList(), param);



            var metaData = new
            {
                TotalCount = result.Count(),
                PageSize = paramters.PageSize,
                CurrentPage = paramters.PageNumber,
                HasPrevipus = paramters.PageNumber > 1,
                TotalPages = (int)Math.Ceiling(result.Count() / (double)paramters.PageSize),
                HasNext = paramters.PageNumber < ((int)Math.Ceiling(result.Count() / (double)paramters.PageSize))

            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));
            #endregion

            return Ok(result);
        }
    }
}
