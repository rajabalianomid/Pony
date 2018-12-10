namespace Pony.Client.Service.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Pony.Client.Service.Model;

    [TestClass]
    public class RestfulClientTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_Pass_RequestBody_Null()
        {
            var mockPathService = new Mock<IRestfulClient>();
            var result = new ResponseNewGame();
            mockPathService.Setup(x => x.Execute<ResponseNewGame>(It.IsAny<RestClientModel>())).Throws(new ArgumentNullException("Name"));
            mockPathService.Object.Execute<ResponseNewGame>(new RestClientModel { RequestBody = null });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void When_Pass_Bad_RouteValues()
        {
            var mockPathService = new Mock<IRestfulClient>();
            var result = new ResponseNewGame();
            mockPathService.Setup(x => x.Execute<ResponseNewGame>(It.IsAny<RestClientModel>())).Throws(new Exception());
            mockPathService.Object.Execute<ResponseNewGame>(new RestClientModel { RouteValues = new { UserName = "1" }, HttpMethod = RestSharp.Method.GET });
        }
    }
}
