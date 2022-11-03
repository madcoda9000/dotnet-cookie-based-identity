using System.Reflection;
using DotNetIdentity.Data;
using DotNetIdentity.Models.DataModels;

namespace DotNetIdentity.Services.SettingsService {
    public abstract class AppSettingsBase {

        // 1 name and properties cached in readonly fields
        private readonly string _name;
        private readonly PropertyInfo[] _properties;

        public AppSettingsBase()
        {
            var type = this.GetType();
            _name = type.Name;
            // 2
            _properties = type.GetProperties();
        }

        public virtual void Load(AppDbContext unitOfWork)
        {
            // ARGUMENT CHECKING SKIPPED FOR BREVITY
            // 3 get settings for this type name
            var settings = unitOfWork.AppSettings!.Where(w => w.Type == _name).ToList();

            foreach (var propertyInfo in _properties)
            {
                // get the setting from the settings list
                var setting = settings.SingleOrDefault(s => s.Name == propertyInfo.Name);
                if (setting != null)
                {
                    // 4 assign the setting values to the properties in the type inheriting this class
                    propertyInfo.SetValue(this, Convert.ChangeType(setting.Value, propertyInfo.PropertyType));
                }
            }
        }

        public virtual void Save(AppDbContext unitOfWork)
        {
            // 5 load existing settings for this type
            var settings = unitOfWork.AppSettings!.Where(w => w.Type == _name).ToList();

            foreach (var propertyInfo in _properties)
            {
                object propertyValue = propertyInfo.GetValue(this, null)!;
                string value = (propertyValue == null) ? null! : propertyValue.ToString()!;

                var setting = settings.SingleOrDefault(s => s.Name == propertyInfo.Name);
                if (setting != null)
                {
                    // 6 update existing value
                    setting.Value = value;
                }
                else
                {
                    // 7 create new setting
                    var newSetting = new ApplicationSettings()
                    {
                        Name = propertyInfo.Name,
                        Type = _name,
                        Value = value,
                    };
                    unitOfWork.AppSettings!.Add(newSetting);
                }
            }
        }
    }
}