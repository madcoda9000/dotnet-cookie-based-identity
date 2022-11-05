namespace DotNetIdentity.Models.ViewModels
{
    /// <summary>
    /// model for UserEditView
    /// </summary>
    public class AssignRoleViewModel
    {
        /// <summary>
        /// the role id
        /// </summary>
        /// <value>string</value>
        public string RoleId { get; set; } = default!;
        /// <summary>
        /// the role neame
        /// </summary>
        /// <value>string</value>
        public string RoleName { get; set; } = default!;
        /// <summary>
        /// if role is assigned
        /// </summary>
        /// <value>bool</value>
        public bool IsAssigned { get; set; }
    }
}