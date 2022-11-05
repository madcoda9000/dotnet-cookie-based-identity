using DotNetIdentity.Data;
using DotNetIdentity.Models.BusinessModels;

namespace DotNetIdentity.Services.SettingsService {
    /// \todo add ldap settings class
    /// <summary>
    /// class for settings service
    /// </summary>
    public class SettingsService : ISettingsService {
    /// <summary>
    /// global settings property (lazy)
    /// </summary>
    private readonly Lazy<GlobalSettings> _globalSettings;
    /// <summary>
    /// global settings property
    /// </summary>
    /// <value></value>
    public GlobalSettings Global { get { return _globalSettings.Value; } }
    /// <summary>
    /// mail settings property (lazy)
    /// </summary>
    private readonly Lazy<MailSettings> _mailSettings;
    /// <summary>
    /// mail settzings property
    /// </summary>
    /// <value></value>
    public MailSettings Mail { get { return _mailSettings.Value; } }
    /// <summary>
    /// dbcontext property
    /// </summary>
    private readonly AppDbContext _unitOfWork;

    /// <summary>
    /// class constructor
    /// </summary>
    /// <param name="unitOfWork"></param>
    public SettingsService(AppDbContext unitOfWork)
    {
        // ARGUMENT CHECKING SKIPPED FOR BREVITY
        _unitOfWork = unitOfWork;
        // 3
        _globalSettings = new Lazy<GlobalSettings>(CreateSettings<GlobalSettings>);
        _mailSettings = new Lazy<MailSettings>(CreateSettings<MailSettings>);
    }

    /// <summary>
    /// sve method
    /// </summary>
    public void Save()
    {
        // only save changes to settings that have been loaded
        if (_globalSettings.IsValueCreated)
            _globalSettings.Value.Save(_unitOfWork);

        if (_mailSettings.IsValueCreated)
            _mailSettings.Value.Save(_unitOfWork);

        _unitOfWork.SaveChanges();
    }
    /// <summary>
    /// method to create instance of the settings classes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private T CreateSettings<T>() where T : AppSettingsBase, new()
    {
        var settings = new T();
        settings.Load(_unitOfWork);
        return settings;
    }
    }
}