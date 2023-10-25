using System.ComponentModel.DataAnnotations;

namespace ContactUs.Models
{
    public class ContactData
    {
        public ContactData(string firstName, string lastName, string email, string message)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Message = message;
        }

        public ContactData()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Message = string.Empty;
        }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        /* 
         Bit of detail on the email validation choice here:

         I chose to use the default MS email validation attribute to validate the email address due to this being a sample project.
         This attribute just checks if the email address is in the form of *@* (where * is a string) and there is a single @ character.
         In a production environment a better way to "validate" an email address would be send a validation email to the address.
         If that email bounces back or is not validated in a certain time, then the address is assumed to be invalid.
         Whatever processing needs to happen with that email address (in this case allowing contact) can only happen after the address is validated this way.

         If user side validation is required in production then a regular expression would probably suffice but there are considerations
         to be taken into account with regard to international email address support, different host names, and whether or not
         the email server supports some of the more esoteric rules for email addresses (such as quotes, special characters, etc.).

         I think that most of those considerations are beyond the scope of a couple hour sample project but I wanted to include this
         note so you didn't think I was just slacking on the validation requirement!
         */
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }
    }
}