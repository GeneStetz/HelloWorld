using System.Collections.Generic;
using System.Web.Http;
using HelloWorldApi.Models;

namespace HelloWorldApi.Controllers
{
    public class StandardMessagesController : ApiController, IMessagesController
    {
        /// <summary>
        /// At the moment we'll assume there is only a single message for a standard message.
        /// In a real world scenario we might be retrieving messages from a database or a file, or even via another web service
        /// and place the below field with code to a separate class and project, perhaps in a business logic layer or service layer, etc.
        /// </summary>
        private readonly List<StandardMessage> _standardMessages = new List<StandardMessage>
        {
            new StandardMessage{Id = 1, RawMessage = "Hello World"}
        };

        /// <summary>
        /// So far there is no requirement for authorization, but this is where we could attribute for it when the security rules are determined.
        /// We have the option of attributing for authorization at the class level (so that all methods are treated with the same security),
        /// or we can attribute for different authorization at each method such as this one.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<StandardMessage> GetAllMessages()
        {
            return _standardMessages;
        }

        /*  We currently don't have requirements for an individual Get (by id or something like that),
            nor for a Post, Put, or Patch,
            but this is where those Http protocol driven methods would be
        */
    }
}
