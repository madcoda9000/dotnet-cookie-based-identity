using DotNetIdentity.Models.BusinessModels;

namespace DotNetIdentity.Services.SettingsService {
    public interface ISettingsService {
        GlobalSettings Global { get; }
        MailSettings Mail { get; }
        void Save();
    }
}