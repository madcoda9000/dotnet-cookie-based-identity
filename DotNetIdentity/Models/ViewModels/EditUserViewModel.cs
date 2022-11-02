namespace DotNetIdentity.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public Gender Gender { get; set; } = Gender.Unknown;
        public Department Department { get; set; } = Department.Software;
        public List<AssignRoleViewModel> Roles { get; set; } = new();
        public string FirstName {get;set;} = string.Empty;
        public string LastName {get; set;} = string.Empty;
    }
}