using System.ComponentModel.DataAnnotations;

namespace DotNetIdentity.Models.DataModels {
    /// <summary>
    /// class define an Settings object
    /// </summary>
    public class ApplicationSettings {
        /// <summary>
        /// property name
        /// </summary>
        /// <value>string</value>
        public string? Name {get;set;}
        /// <summary>
        /// property Type
        /// </summary>
        /// <value>string</value>
        public string? Type {get;set;}
        /// <summary>
        /// property value
        /// </summary>
        /// <value>string</value>
        public string? Value {get;set;}
    }
}