namespace Messager.Models
{
    public class MessageBase
    {
        public string[] Recipients { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
