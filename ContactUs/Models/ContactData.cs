using System.ComponentModel.DataAnnotations;

namespace ContactUs.Models
{
    public class ContactData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // TODO: Validate email address, custom attribute? MS default?
        public string Email { get; set; }

        public string Message { get; set; }
    }
}