namespace DotNetIdentity.Models.ViewModels
{
    /// <summary>
    /// model for EditUser view
    /// </summary>
    public class EditUserViewModel
    {
        /// <summary>
        /// the user id
        /// </summary>
        /// <value>string</value>
        public string Id { get; set; } = default!;
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
        /// the gener
        /// </summary>
        /// <value>Gender</value>
        public Gender Gender { get; set; } = Gender.Unknown;
        /// <summary>
        /// the department
        /// </summary>
        /// <value>Department</value>
        public Department Department { get; set; } = Department.Software;
        /// <summary>
        /// the roles assigned
        /// </summary>
        /// <returns>List of type AppRole</returns>
        public List<AssignRoleViewModel> Roles { get; set; } = new();
        /// <summary>
        /// the firstname
        /// </summary>
        /// <value>string</value>
        public string FirstName {get;set;} = string.Empty;
        /// <summary>
        /// the lastname
        /// </summary>
        /// <value>string</value>
        public string LastName {get; set;} = string.Empty;
    }
}