using LiteDB;
using Messager.Interfaces.Services;
using Messager.Models;
using System.Linq;

namespace Messager.Services
{
    public class DataService : IDataService
    {
        private const string Name = "messages";
        private string _connectionString;

        public DataService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string SaveMessage(MessageApiModel message)
        {
            string result;

            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = db.GetCollection<MessageModel>(Name);
                collection.EnsureIndex(x => x.Id);

                var id = collection.Insert(new MessageModel
                {
                    Recipients = message.Recipients,
                    Subject = message.Subject,
                    Body = message.Body,
                    IsSent = true
                });

                

                result = id.AsString;
            }

            return result;
        }

        public void UpdateSentInfo(string messageId, bool isSent)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = db.GetCollection<MessageModel>(Name);

                var message = collection.Find(x => x.Id == int.Parse(messageId)).FirstOrDefault();

                if (message == null)
                    return;

                message.IsSent = isSent;
                collection.Update(message);
            }
        }
    }
}
