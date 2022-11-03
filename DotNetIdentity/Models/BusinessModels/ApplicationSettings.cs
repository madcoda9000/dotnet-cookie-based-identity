using System.ComponentModel.DataAnnotations;

namespace DotNetIdentity.Models.BusinessModels {
    public class ApplicationSettings {
        public string? Name {get;set;}
        public string? Type {get;set;}
        public string? Value {get;set;}
    }
}