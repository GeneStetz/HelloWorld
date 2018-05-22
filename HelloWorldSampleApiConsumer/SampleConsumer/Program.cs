using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

// For the sake of sharing the model between the API and this sample consumer class, I've included the Models project from the HelloWorldApi as part of this solution.
using HelloWorldApi.Models;

namespace SampleConsumer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Demonstrate getting a "Hello World" to display to the user from a collection of messages
            // via an HttpGet call to an Api in the format of //<some base address>/api/StandardMessages.

            // We are starting with the assumption that there are a collection of "standard" messages,
            // but that the Api allows for the message interface to be extended to other messages possibly in their own classes.
            // It might be easier to maintain if there is only one message class with all possible properties contained in such class,
            // but it might be easier for each caller to request only per what the caller needs while redisregarding properties not needed by the particular caller.

            var messages = GetStandardMessages();
            string msgFormatted = CreateStandardFormattedMessage(messages);
            if (!string.IsNullOrWhiteSpace(msgFormatted))
            {
                // For now, we'll display the message in a message box from this console app,
                // but later we can expand to send the message to something else.
                if (string.Equals(ConfigurationManager.AppSettings["MesssageDestination"], "MessageBox", StringComparison.InvariantCultureIgnoreCase))
                {
                    MessageBox.Show(msgFormatted, "Demonstration of HttpGet of a collection", MessageBoxButtons.OK);
                }
            }
        }

        private static string CreateStandardFormattedMessage(List<StandardMessage> standardMessages)
        {
            var sbMsg = new StringBuilder();
            foreach (var standardMessage in standardMessages)
            {
                sbMsg.AppendLine(standardMessage.RawMessage);
            }
            var msgFormatted = sbMsg.ToString();

            return msgFormatted;
        }

        private static List<StandardMessage> GetStandardMessages()
        {
            var messages = new List<StandardMessage>();

            var messageTask = GetStandardMessagesAsync();
            // We could do other work while we wait, 
            // but for now we'll just wait for the response, and potentially change this later when a new requirement necessitates it.
            messageTask.Wait();

            // TODO: For now I'm assuming that a task completes in a timely fashion, but in the future
            // we'll want to determine how we should handle a task which requires too much time to execute,
            // but we'll need to first ask how the business would want to handle such a situation, such as a custom return message, etc.
            var taskResult = messageTask.Result;

            // TODO: We expect to receive a message(s), but in the case there is none, 
            // we also need to confer with the business how they want the use case of empty data handled.
            if (taskResult.Count > 0) messages.AddRange(taskResult);

            return messages;
        }

        private static async Task<List<StandardMessage>> GetStandardMessagesAsync()
        {
            var messages = new List<StandardMessage>();

            var responseTask = GetApiResponseAsync("api/StandardMessages");
            // We could do other work while we wait, 
            // but for now we'll just wait for the response, and potentially change this later when a new requirement necessitates it.
            responseTask.Wait();

            // TODO: For now I'm assuming that a task completes in a timely fashion, but in the future
            // we'll want to determine how we should handle a task which requires too much time to execute,
            // but we'll need to first ask how the business would want to handle such a situation, such as a custom return message, etc.
            var response = responseTask.Result;

            if (response.IsSuccessStatusCode)
            {
                var rtn = await response.Content.ReadAsAsync<List<StandardMessage>>();
                // TODO: We expect to receive a message(s), but in the case there is none, 
                // we also need to confer with the business how they want the use case of empty data handled.
                if (rtn.Count > 0) messages.AddRange(rtn);
            }
            else
            {
                // TODO: Decide how an error should be handled, 
                // but for now we'll assume an empty set means that something wrong happened
            }

            return messages;
        }

        private static async Task<HttpResponseMessage> GetApiResponseAsync(string relativeUri)
        {
            HttpResponseMessage responseMessage;

            using (var client = new HttpClient())
            {
                // TODO: Once we have established a DNS for the HelloWorldApi, we'll replace the below Uri with it
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiBaseUri"]);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //GET Method  
                responseMessage = await client.GetAsync(relativeUri);
            }

            return responseMessage;
        }
    }
}
