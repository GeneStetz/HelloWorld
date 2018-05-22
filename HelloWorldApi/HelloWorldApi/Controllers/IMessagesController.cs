using System.Collections.Generic;
using System.Web.Http;
using HelloWorldApi.Models;

namespace HelloWorldApi.Controllers
{
    public interface IMessagesController 
    {
        [HttpGet]
        List<StandardMessage> GetAllMessages();
    }
}