using DotNetIdentity.Models.BusinessModels;

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
        /// save method
        /// </summary>
        void Save();
    }
}