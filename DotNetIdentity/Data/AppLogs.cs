namespace DotNetIdentity.Data {
    public class AppLogs {
        public int id {get;set;}
        public string? Exception {get;set;}
        public string Level {get;set;} = string.Empty;
        public string Message {get;set;} = string.Empty;
        public string MessageTemplate {get;set;} = string.Empty;
        public string Properties {get;set;} = string.Empty;
        public string Timestamp {get;set;} = string.Empty;

    }
}