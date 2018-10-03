using Messager.Interfaces.Services;
using Messager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Messager.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class MessageApiController : ControllerBase
    {
        private IDataService _dataService;

        public MessageApiController(IDataService dataService)
        {
            _dataService = dataService;
        }
        
        [HttpPost]
        public ActionResult Post([FromBody] MessageApiModel message)
        {
            if (message == null)
                return new BadRequestResult();
                       
            var id = _dataService.SaveMessage(message);

            Task.Run(() => SimulateSendingToNotificationService(id));

            return new JsonResult(id);

        }     
        
        private async Task SimulateSendingToNotificationService(string id)
        {
            await Task.Delay(3000);

            var isSuccess = new Random().Next(0, 100) < 30 ? false : true;

            if (!isSuccess)
            {
                _dataService.UpdateSentInfo(id, isSuccess);
            }
        }
    }
}
