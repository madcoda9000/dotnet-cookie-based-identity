namespace DotNetIdentity.Models.BusinessModels
{
    /// <summary>
    /// class to define a email
    /// </summary>
    public class EmailModel
    {
        /// <summary>
        /// property subject
        /// </summary>
        /// <value>string</value>
        public string Subject { get; set; } = default!;
        /// <summary>
        /// property body
        /// </summary>
        /// <value>string</value>
        public string Body { get; set; } = default!;
        /// <summary>
        /// property to
        /// </summary>
        /// <value>string</value>
        public string To { get; set; } = default!;
    }
}