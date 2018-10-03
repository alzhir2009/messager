using LiteDB;

namespace Messager.Models
{
    public class MessageModel : MessageBase
    {
        [BsonId]
        public int Id { get; set; }

        public bool IsSent { get; set; }
    }
}
