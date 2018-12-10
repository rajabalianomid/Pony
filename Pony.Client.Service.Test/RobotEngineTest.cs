namespace Pony.Client.Service.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Pony.Client.Service.Model;
    using WireMock.RequestBuilders;
    using WireMock.ResponseBuilders;
    using WireMock.Server;
    using static Pony.Client.Service.Model.Robot.PathWay;

    [TestClass]
    public class RobotEngineTest
    {
        [TestMethod]
        [DataRow("123", "active")]
        public void Go_To_Next_Until_Get_To_End_Test(string mazeId, string state)
        {
            var stubPathService = new Mock<IPathService>();
            var server = FluentMockServer.Start(null, false);
            var client = new RestfulClient(server.Urls.Select(u => string.Concat(new Uri(u))).First() + "pony-challenge/maze", true);
            IGameService gameService = new GameService(client);

            var resultResponseMove = new ResponseMove();
            var pathWay = new Model.Robot.PathWay(0, new List<MoveType> { MoveType.West });
            stubPathService.Setup(fm => fm.FindMoves(mazeId)).Returns(pathWay);

            var responseBody = new Dictionary<string, object>
            {
                ["state"] = "active",
                ["state-result"] = "Move accepted"
            };
            server.Given(Request.Create().UsingPost())
                        .RespondWith(Response.Create()
                            .WithStatusCode(200)
                            .WithHeader("content-type", "application/json; charset=utf-8")
                            .WithBodyAsJson(responseBody, Encoding.UTF8));

            var roboteEngine = new RobotEngine(gameService, stubPathService.Object);
            var result = roboteEngine.GotoNext(mazeId);
            Assert.AreEqual(result.State, "active");
        }
    }
}
