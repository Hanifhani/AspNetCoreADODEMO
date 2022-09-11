using DAVIGOLD.API.Model;
using DAVIGOLD.API.Utility;
using System.Data.SqlClient;

namespace DAVIGOLD.API.Translator
{
  

    public static class ContactTranslator
    {
        public static ContactsModel TranslateAsContact(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new ContactsModel();
            if (reader.IsColumnExists("Id"))
                item.Id = SqlHelper.GetNullableInt32(reader, "Id");

            if (reader.IsColumnExists("FirstName"))
                item.FirstName = SqlHelper.GetNullableString(reader, "FirstName");

            if (reader.IsColumnExists("LastName"))
                item.LastName = SqlHelper.GetNullableString(reader, "LastName");

            if (reader.IsColumnExists("MiddleName"))
                item.MiddleName = SqlHelper.GetNullableString(reader, "MiddleName");

            if (reader.IsColumnExists("Title"))
                item.Title = SqlHelper.GetNullableString(reader, "Title");

            if (reader.IsColumnExists("Notes"))
                item.Notes = SqlHelper.GetNullableString(reader, "Notes");

            if (reader.IsColumnExists("CompanyId"))
                item.CompanyId = SqlHelper.GetNullableInt32(reader, "CompanyId");

            if (reader.IsColumnExists("Position"))
                item.Position = SqlHelper.GetNullableString(reader, "Position");

            if (reader.IsColumnExists("Department"))
                item.Department = SqlHelper.GetNullableString(reader, "Department");

            if (reader.IsColumnExists("BusinessEmail"))
                item.BusinessEmail = SqlHelper.GetNullableString(reader, "BusinessEmail");

            if (reader.IsColumnExists("DirectLine"))
                item.DirectLine = SqlHelper.GetNullableString(reader, "DirectLine");

            if (reader.IsColumnExists("PrimaryMobile"))
                item.PrimaryMobile = SqlHelper.GetNullableString(reader, "PrimaryMobile");

            if (reader.IsColumnExists("SecondaryMobile"))
                item.SecondaryMobile = SqlHelper.GetNullableString(reader, "SecondaryMobile");

            if (reader.IsColumnExists("Country"))
                item.Country = SqlHelper.GetNullableString(reader, "Country");

            if (reader.IsColumnExists("ZIPCode"))
                item.ZIPCode = SqlHelper.GetNullableString(reader, "ZIPCode");

            if (reader.IsColumnExists("Address"))
                item.Address = SqlHelper.GetNullableString(reader, "Address");

            if (reader.IsColumnExists("City"))
                item.City = SqlHelper.GetNullableString(reader, "City");

            if (reader.IsColumnExists("State"))
                item.State = SqlHelper.GetNullableString(reader, "State");

            if (reader.IsColumnExists("PersonalMobilePrimary"))
                item.PersonalMobilePrimary = SqlHelper.GetNullableString(reader, "PersonalMobilePrimary");

            if (reader.IsColumnExists("PersonalMobileSecondary"))
                item.PersonalMobileSecondary = SqlHelper.GetNullableString(reader, "PersonalMobileSecondary");
            if (reader.IsColumnExists("State"))
                item.State = SqlHelper.GetNullableString(reader, "State");

            if (reader.IsColumnExists("HomePhone"))
                item.HomePhone = SqlHelper.GetNullableString(reader, "HomePhone");

            if (reader.IsColumnExists("PersonalEmail"))
                item.PersonalEmail = SqlHelper.GetNullableString(reader, "PersonalEmail");

            if (reader.IsColumnExists("Linkedin"))
                item.Linkedin = SqlHelper.GetNullableString(reader, "Linkedin");

            if (reader.IsColumnExists("DateOfBirth"))
                item.DateOfBirth = SqlHelper.GetNullableDateTime(reader, "DateOfBirth");

            if (reader.IsColumnExists("CreatedDate"))
                item.CreatedDate = SqlHelper.GetNullableString(reader, "CreatedDate");

            if (reader.IsColumnExists("ModifiedDate"))
                item.ModifiedDate = SqlHelper.GetNullableString(reader, "ModifiedDate");

            if (reader.IsColumnExists("Type"))
                item.Type = SqlHelper.GetNullableString(reader, "Type");

            if (reader.IsColumnExists("TeamKnowledge"))
            {
                var teamKnowledge = SqlHelper.GetNullableString(reader, "TeamKnowledge");
                //convert to List of Guid
                item.TeamKnowledge = teamKnowledge?.Split(",").Select(x =>  Guid.Parse(x)).ToList();
            }
                

            if (reader.IsColumnExists("Tags"))
                item.Tags = SqlHelper.GetNullableString(reader, "Tags");

            if (reader.IsColumnExists("Company"))
                item.Company = SqlHelper.GetNullableString(reader, "Company");


            return item;
        }

        public static List<ContactsModel> TranslateAsContactList(this SqlDataReader reader)
        {
            var list = new List<ContactsModel>();
            while (reader.Read())
            {
                list.Add(TranslateAsContact(reader, true));
            }
            return list;
        }
    }
}
