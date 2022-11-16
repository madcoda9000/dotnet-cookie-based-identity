using DotNetIdentity.Data;
using DotNetIdentity.Models.BusinessModels;

namespace DotNetIdentity.Services.SettingsService {
    /// <summary>
    /// class for settings service
    /// </summary>
    public class SettingsService : ISettingsService {
        /// <summary>
        /// global settings property (lazy)
        /// </summary>
        private readonly Lazy<LdapSettings> _ldapSettings;
        /// <summary>
        /// global settings property
        /// </summary>
        /// <value></value>
        public LdapSettings Ldap { get { return _ldapSettings.Value; } }
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
        /// Brand settings property (lazy)
        /// </summary>
        private readonly Lazy<BrandSettings> _brandSettings;
        /// <summary>
        /// brand settings property
        /// </summary>
        /// <value></value>
        public BrandSettings Brand { get { return _brandSettings.Value; } }
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
            _unitOfWork = unitOfWork;

            _globalSettings = new Lazy<GlobalSettings>(CreateSettings<GlobalSettings>);
            _mailSettings = new Lazy<MailSettings>(CreateSettings<MailSettings>);
            _ldapSettings = new Lazy<LdapSettings>(CreateSettings<LdapSettings>);
            _brandSettings = new Lazy<BrandSettings>(CreateSettings<BrandSettings>);
        }

        /// <summary>
        /// save method
        /// </summary>
        /// <returns></returns>
        public async Task Save()
        {
            // only save changes to settings that have been loaded
            if (_globalSettings.IsValueCreated)
                await _globalSettings.Value.Save(_unitOfWork);

            if (_mailSettings.IsValueCreated)
                await _mailSettings.Value.Save(_unitOfWork);

            if (_ldapSettings.IsValueCreated)
                await _ldapSettings.Value.Save(_unitOfWork);

            if (_brandSettings.IsValueCreated)
                await _brandSettings.Value.Save(_unitOfWork);

            await _unitOfWork.SaveChangesAsync();               
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