using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelloWorldApi.Controllers;
using HelloWorldApi.Models;

namespace HelloWorldApi.Tests
{
    [TestClass]
    public class UnitTestGetStdMsg
    {
        [TestMethod]
        public void TestGetFromCollection_OneResult()
        {
            var result = GetMessagesFromCollection();

            Assert.IsTrue(result.Count == 1);
        }

        [TestMethod]
        public void TestGetFromCollection_CorrectMessage()
        {
            var result = GetMessagesFromCollection();

            StringAssert.Contains(result[0].RawMessage, "Hello World");
        }

        private List<StandardMessage> GetMessagesFromCollection()
        {
            var controller = new StandardMessagesController();
            var result = controller.GetAllMessages();

            return result;
        }
    }
}
