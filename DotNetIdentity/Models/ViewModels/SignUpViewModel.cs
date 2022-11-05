namespace DotNetIdentity.Models.ViewModels
{
    /// <summary>
    /// model for Register view
    /// </summary>
    public class SignUpViewModel
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
        /// the gender
        /// </summary>
        /// <value>Gender</value>
        public Gender Gender { get; set; } = Gender.Unknown;
        /// <summary>
        /// the birthday
        /// </summary>
        /// <value>DateTime</value>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// the department
        /// </summary>
        /// <value>Department</value>
        public Department Department { get; set; } = Department.Software;
        /// <summary>
        /// the password
        /// </summary>
        /// <value>string</value>
        public string Password { get; set; } = default!;
        /// <summary>
        /// the firstname
        /// </summary>
        /// <value>string</value>
        public string FirstName {get; set;} = string.Empty;
        /// <summary>
        /// the lastname
        /// </summary>
        /// <value>string</value>
        public string LastName {get; set;} = string.Empty;
    }
}