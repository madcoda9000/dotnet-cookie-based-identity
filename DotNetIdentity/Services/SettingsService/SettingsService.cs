using DotNetIdentity.Data;
using DotNetIdentity.Models.BusinessModels;

namespace DotNetIdentity.Services.SettingsService {
    public class SettingsService : ISettingsService {
        // 1
    private readonly Lazy<GlobalSettings> _globalSettings;
    // 2
    public GlobalSettings Global { get { return _globalSettings.Value; } }

    private readonly Lazy<MailSettings> _mailSettings;
    public MailSettings Mail { get { return _mailSettings.Value; } }

    private readonly AppDbContext _unitOfWork;
    public SettingsService(AppDbContext unitOfWork)
    {
        // ARGUMENT CHECKING SKIPPED FOR BREVITY
        _unitOfWork = unitOfWork;
        // 3
        _globalSettings = new Lazy<GlobalSettings>(CreateSettings<GlobalSettings>);
        _mailSettings = new Lazy<MailSettings>(CreateSettings<MailSettings>);
    }

    public void Save()
    {
        // only save changes to settings that have been loaded
        if (_globalSettings.IsValueCreated)
            _globalSettings.Value.Save(_unitOfWork);

        if (_mailSettings.IsValueCreated)
            _mailSettings.Value.Save(_unitOfWork);

        _unitOfWork.SaveChanges();
    }
    // 4
    private T CreateSettings<T>() where T : AppSettingsBase, new()
    {
        var settings = new T();
        settings.Load(_unitOfWork);
        return settings;
    }
    }
}