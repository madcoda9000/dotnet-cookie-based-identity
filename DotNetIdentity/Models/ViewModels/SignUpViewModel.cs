namespace DotNetIdentity.Models.ViewModels
{
    public class SignUpViewModel
    {
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public Gender Gender { get; set; } = Gender.Unknown;
        public DateTime BirthDay { get; set; }
        public Department Department { get; set; } = Department.Software;
        public string Password { get; set; } = default!;
        public string FirstName {get; set;} = string.Empty;
        public string LastName {get; set;} = string.Empty;
    }
}