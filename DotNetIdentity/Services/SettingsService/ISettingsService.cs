using DotNetIdentity.Models.BusinessModels;
using System.Threading.Tasks;

namespace DotNetIdentity.Services.SettingsService {
    /// <summary>
    /// Interface class for SettingsService
    /// </summary>
    public interface ISettingsService {
        /// <summary>
        /// property global settings
        /// </summary>
        /// <value>GlobalSettings</value>
        GlobalSettings Global { get; }
        /// <summary>
        /// property mail settings
        /// </summary>
        /// <value>MailSettings</value>
        MailSettings Mail { get; }
        /// <summary>
        /// property mail settings
        /// </summary>
        /// <value>MailSettings</value>
        LdapSettings Ldap { get; }
        /// <summary>
        /// property brand settings
        /// </summary>
        /// <value>MailSettings</value>
        BrandSettings Brand { get; }
        /// <summary>
        /// save method
        /// </summary>
        Task Save();
    }
}