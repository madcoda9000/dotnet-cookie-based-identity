namespace DotNetIdentity.Models.ViewModels
{
    /// <summary>
    /// model for UpdateProfile view
    /// </summary>
    public class UpdateProfileViewModel
    {
        /// <summary>
        /// the username
        /// </summary>
        /// <value>string</value>
        public string UserName { get; set; } = default!;
        /// <summary>
        /// the email
        /// </summary>
        /// <value>string</value>
        public string Email { get; set; } = default!;
        /// <summary>
        /// the phone number
        /// </summary>
        /// <value>string</value>
        public string PhoneNumber { get; set; } = default!;
        /// <summary>
        /// the gender
        /// </summary>
        /// <value>Gender</value>
        public Gender Gender { get; set; }
        /// <summary>
        /// the birthday
        /// </summary>
        /// <value>DateTime</value>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// the profile picture
        /// </summary>
        /// <value>IFormFile</value>
        public IFormFile? ProfilePicture { get; set; }
    }
}