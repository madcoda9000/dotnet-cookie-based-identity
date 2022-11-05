namespace DotNetIdentity.Models.ViewModels
{
    /// <summary>
    /// model for the UpsertRole view
    /// </summary>
    public class UpsertRoleViewModel
    {
        /// <summary>
        /// the role id
        /// </summary>
        /// <value>string</value>
        public string? Id { get; set; }
        /// <summary>
        /// the role name
        /// </summary>
        /// <value>string</value>
        public string Name { get; set; } = default!;
    }
}