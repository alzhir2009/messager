using Messager.Models;

namespace Messager.Interfaces.Services
{
    public interface IDataService
    {
        string SaveMessage(MessageApiModel message);
        void UpdateSentInfo(string messageId, bool isSent);
    }
}
