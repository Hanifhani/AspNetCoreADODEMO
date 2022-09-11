using DAVIGOLD.API.Model;
using DAVIGOLD.API.Utility;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace DAVIGOLD.API.Repository
{
    public class ContactDbClient
    {
        public Response<int> AddContact(ContactsModel contact, IOptions<AppSettingModel> appSettings)
        {
            var response = new Response<int>();


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
     new SqlParameter("@PersonalMobile1",contact.PersonalMobilePrimary),
     new SqlParameter("@PersonalMobile2",contact.PersonalMobileSecondary),
     new SqlParameter("@HomePhone",contact.HomePhone),
     new SqlParameter("@PersonalEmail",contact.PersonalEmail),
     new SqlParameter("@Linkedin",contact.Linkedin),
     new SqlParameter("@DateOfBirth", contact.DateOfBirth),
          new SqlParameter("@Linkedin",contact.Linkedin),
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
            return response;
        }

        
    }
}
