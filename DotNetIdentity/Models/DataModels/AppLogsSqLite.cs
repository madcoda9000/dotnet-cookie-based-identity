namespace DotNetIdentity.Models.DataModels
{
    /// <summary>
    /// class to define ann Log object for the sqlite serilog sink as this sink has a different table structure
    /// </summary>
    public class AppLogsSqLite
    {
        /// <summary>
        /// property id
        /// </summary>
        /// <value>int</value>
        public int id { get; set; }
        /// <summary>
        /// property Timestamp
        /// </summary>
        /// <value>string</value>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// property Level
        /// </summary>
        /// <value>string</value>
        public string Level { get; set; } = string.Empty;
        /// <summary>
        /// property Exception
        /// </summary>
        /// <value>string</value>        
        public string? Exception { get; set; }        
        /// <summary>
        /// property Message
        /// </summary>
        /// <value>string</value>
        public string RenderedMessage { get; set; } = string.Empty;
        /// <summary>
        /// property Properties
        /// </summary>
        /// <value>string</value>
        public string Properties { get; set; } = string.Empty;        

    }
}