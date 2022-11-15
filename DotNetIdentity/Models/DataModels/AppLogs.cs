namespace DotNetIdentity.Models.DataModels {
    /// <summary>
    /// class to define ann Log object
    /// </summary>
    public class AppLogs {
        /// <summary>
        /// property id
        /// </summary>
        /// <value>int</value>
        public int id {get;set;}
        /// <summary>
        /// property Exception
        /// </summary>
        /// <value>string</value>
        public string? Exception {get;set;}
        /// <summary>
        /// property Level
        /// </summary>
        /// <value>string</value>
        public string Level {get;set;} = string.Empty;
        /// <summary>
        /// property Message
        /// </summary>
        /// <value>string</value>
        public string Message {get;set;} = string.Empty;
        /// <summary>
        /// property MessageTemplate
        /// </summary>
        /// <value>string</value>
        public string MessageTemplate {get;set;} = string.Empty;
        /// <summary>
        /// property Properties
        /// </summary>
        /// <value>string</value>
        public string Properties {get;set;} = string.Empty;
        /// <summary>
        /// property Timestamp
        /// </summary>
        /// <value>string</value>
        public DateTime Timestamp {get;set;}

    }
}