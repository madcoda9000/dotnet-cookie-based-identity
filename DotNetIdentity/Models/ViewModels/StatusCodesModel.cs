namespace DotNetIdentity.Models.ViewModels {
    /// <summary>
    /// model for Error Statuscodes
    /// </summary>
    public class StatusCodesModel {
        /// <summary>
        /// the status code
        /// </summary>
        /// <value>int</value>
        public int StatusCode {get; set;}
        /// <summary>
        /// the error message
        /// </summary>
        /// <value>string</value>
        public string? ErrorMessage {get; set;}
        /// <summary>
        /// the route that thowed the exception
        /// </summary>
        /// <value>string</value>
        public string? RouteOfException {get; set;}
    }
}