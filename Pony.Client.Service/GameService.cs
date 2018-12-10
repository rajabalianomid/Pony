namespace Pony.Client.Service
{
    using Pony.Client.Service.Model;
    using RestSharp;
    using static Pony.Client.Service.Model.Robot.PathWay;

    public class GameService : IGameService
    {
        private IRestfulClient restfulClient;

        public GameService(IRestfulClient restfulClient)
        {
            this.restfulClient = restfulClient;
        }

        /// <summary>
        /// Create new game
        /// </summary>
        /// <param name="name">Pony name</param>
        /// <param name="width">Cell count</param>
        /// <param name="height">Row count</param>
        /// <param name="difficulty">Game difficulty</param>
        /// <returns>Return ResponseNewGame</returns>
        public ResponseNewGame NewGame(string name, int width = 15, int height = 15, int difficulty = 0) =>
            this.restfulClient.Execute<ResponseNewGame>(new RestClientModel
            {
                RequestBody = "{\r\n  \"maze-width\": " + width + ",\r\n  \"maze-height\": " + height + ",\r\n  \"maze-player-name\": \"" + name + "\",\r\n  \"difficulty\": " + difficulty + "\r\n}"
            });

        /// <summary>
        /// Update game state
        /// </summary>
        /// <param name="mazeId">Maze id</param>
        /// <returns>Return ResponseCurrentState</returns>
        public ResponseCurrentState ChangeCurrentState(string mazeId) =>
            this.restfulClient.Execute<ResponseCurrentState>(new RestClientModel
            {
                HttpMethod = Method.GET,
                RouteValues = new { id = mazeId }
            });

        /// <summary>
        /// Move Pony
        /// </summary>
        /// <param name="mazeId">Maze Id</param>
        /// <param name="move">Move Type</param>
        /// <returns>Return ResponseMove</returns>
        public ResponseMove Move(string mazeId, MoveType move = MoveType.Stay) =>
            this.restfulClient.Execute<ResponseMove>(new RestClientModel
            {
                RequestBody = "{\r\n  \"direction\": \"" + move.ToString().ToLower() + "\"\r\n}",
                RouteValues = new { id = mazeId }
            });

        /// <summary>
        /// Print Pony Game
        /// </summary>
        /// <param name="mazeId">Maze Id</param>
        /// <returns>Return game shape</returns>
        public string Print(string mazeId) =>
            this.restfulClient.Execute<ResponseBase>(new RestClientModel
            {
                HttpMethod = Method.GET,
                RouteValues = new { id = mazeId, print = string.Empty }
            }).Content;
    }
}
