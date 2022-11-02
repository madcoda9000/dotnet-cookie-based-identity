namespace DotNetIdentity.Models.BusinessModels
{
    public class EmailModel
    {
        public string Subject { get; set; } = default!;
        public string Body { get; set; } = default!;
        public string To { get; set; } = default!;
    }
}