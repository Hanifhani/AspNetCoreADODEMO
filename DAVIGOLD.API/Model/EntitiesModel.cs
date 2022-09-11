using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DAVIGOLD.API.Model
{
    public class ContactsModel
    {  
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Title { get; set; }

        public string MiddleName { get; set; }

        public string Notes { get; set; }

        public int? CompanyId { get; set; }

        public string Position { get; set; }

        public string Department { get; set; }

        public string BusinessEmail { get; set; }

        public string DirectLine { get; set; }

        public string PrimaryMobile { get; set; }

        public string SecondaryMobile { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string ZIPCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PersonalMobilePrimary { get; set; }

        public string PersonalMobileSecondary { get; set; }

        public string HomePhone { get; set; }

        public string PersonalEmail { get; set; }

        public string Linkedin { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string CreatedDate { get; set; }

        public string ModifiedDate { get; set; }

        public string Type { get; set; }
        public string Tags { get; set; }
        public List<Guid> TeamKnowledge { get; set; }

        public string Company { get; set; }

    }

   

    public class CompanyModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Country { get; set; }

        public string Address { get; set; }
        public string ZipCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string CompanyMainLine { get; set; }
        public int Type { get; set; }

    }


}
