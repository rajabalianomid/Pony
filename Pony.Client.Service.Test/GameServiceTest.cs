namespace Pony.Client.Service.Test
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Pony.Client.Service.Model;
    using WireMock.RequestBuilders;
    using WireMock.ResponseBuilders;
    using WireMock.Server;

    [TestClass]
    public class GameServiceTest
    {
        [TestMethod]
        [DataRow("123")]
        public void Print(string mazeId)
        {
            var server = FluentMockServer.Start(null, false);
            var client = new RestfulClient(server.Urls.Select(u => string.Concat(new Uri(u))).First() + $"pony-challenge/maze", true);
            IGameService gameService = new GameService(client);
            server.Given(Request.Create().UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("content-type", "application/json; charset=utf-8")
                .WithBody("+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+\n|   |                           |               |           |\n+   +---+   +   +---+---+---+   +   +---+---+   +---+---+   +\n|       |   |   |           |       |                       |\n+---+   +---+   +---+---+   +---+---+   +---+---+---+---+---+\n|   |       |           |               |       |           |\n+   +---+   +   +---+   +   +---+---+---+   +   +   +---+   +\n|       |   |       |   |   |               |       |       |\n+   +---+   +---+   +   +---+   +---+---+---+---+---+   +   +\n|           |   |   | P     |   |       | D         |   |   |\n+   +---+---+   +   +---+   +   +   +---+   +---+   +   +   +\n|   |                   |       |   |       |   |   |   |   |\n+   +---+   +---+---+   +---+---+   +   +---+   +   +   +---+\n|       |   |       |       |       |   |           |       |\n+---+   +---+   +   +---+   +   +   +   +   +---+---+   +   +\n|   |   |       |       |       |   |   |   |       |   |   |\n+   +   +   +---+---+   +---+---+   +   +   +   +   +---+   +\n|   |   |           |       |       |   |       |           |\n+   +   +---+---+   +---+   +---+   +   +---+---+---+---+   +\n|       |           |   |       |   |   |               |   |\n+   +---+   +---+---+   +---+   +   +   +---+---+   +   +   +\n|           |                   |   |           |   |   |   |\n+---+---+---+   +---+---+---+---+   +---+---+   +   +   +   +\n|   |       |           |                   |   |   |       |\n+   +   +   +---+---+   +---+---+---+---+---+   +---+---+---+\n|       |               |                   |               |\n+   +---+---+---+---+---+   +---+---+---+   +   +---+---+   +\n|   |       |       |           |       |   |   |       |   |\n+   +   +   +   +   +---+---+   +   +---+   +---+   +   +   +\n|       |       |               | E                 |       |\n+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+"));

            var gameServiceobj = gameService.Print(mazeId);
            Assert.IsTrue(gameServiceobj.Contains("+"));
        }

        [TestMethod]
        [DataRow(null)]
        public void When_Pass_Name_Invalid_Name_To_NewGame(string name)
        {
            var mockRestFulClient = new Mock<IRestfulClient>();
            var result = new ResponseNewGame();
            mockRestFulClient.Setup(x => x.Execute<ResponseNewGame>(It.IsAny<RestClientModel>())).Callback<RestClientModel>(c =>
            {
                result = new ResponseNewGame
                {
                    State = "Only ponies can play"
                };
            }).Returns(() => result);
            IGameService gameService = new GameService(mockRestFulClient.Object);
            var gameServiceobj = gameService.NewGame(name);
            Assert.AreEqual(gameServiceobj.State, "Only ponies can play");
        }

        [TestMethod]
        [DataRow("Spike", 1, 100)]
        public void When_Pass_Name_InValid_Height_And_Width_To_NewGame(string name, int width, int heghit)
        {
            var mockPathService = new Mock<IRestfulClient>();
            var result = new ResponseNewGame();
            mockPathService.Setup(x => x.Execute<ResponseNewGame>(It.IsAny<RestClientModel>())).Callback<RestClientModel>(c =>
            {
                result = new ResponseNewGame
                {
                    State = "Maze dimensions should be between 15 and 25"
                };
            }).Returns(() => result);
            IGameService gameService = new GameService(mockPathService.Object);
            var gameServiceobj = gameService.NewGame(name, width, heghit);
            Assert.AreEqual(gameServiceobj.State, "Maze dimensions should be between 15 and 25");
        }

        [TestMethod]
        [DataRow("123")]
        public void When_Pass_Name_Invalid_MazeId_To_ChangeCurrentState(string mazeId)
        {
            var mockPathService = new Mock<IRestfulClient>();
            var result = new ResponseCurrentState();
            mockPathService.Setup(x => x.Execute<ResponseCurrentState>(It.IsAny<RestClientModel>())).Callback<RestClientModel>(c =>
            {
                result = new ResponseCurrentState
                {
                    GameState = new GameState { State = "maze with that id is not found" }
                };
            }).Returns(() => result);
            IGameService gameService = new GameService(mockPathService.Object);
            var gameServiceobj = gameService.ChangeCurrentState(mazeId);
            Assert.AreEqual(gameServiceobj.GameState.State, "maze with that id is not found");
        }

        [TestMethod]
        [DataRow("123")]
        public void When_Pass_Name_Invalid_MazeId_To_Move(string mazeId)
        {
            var mockPathService = new Mock<IRestfulClient>();
            var result = new ResponseMove();
            mockPathService.Setup(x => x.Execute<ResponseMove>(It.IsAny<RestClientModel>())).Callback<RestClientModel>(c =>
            {
                result = new ResponseMove
                {
                    State = "maze with that id is not found"
                };
            }).Returns(() => result);
            IGameService gameService = new GameService(mockPathService.Object);
            var gameServiceobj = gameService.Move(mazeId);
            Assert.AreEqual(gameServiceobj.State, "maze with that id is not found");
        }
    }
}
