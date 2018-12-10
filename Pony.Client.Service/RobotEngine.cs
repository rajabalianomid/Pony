namespace Pony.Client.Service
{
    using Pony.Client.Service.Model;

    public class RobotEngine : IRobotEngine
    {
        private readonly IGameService gameService;
        private readonly IPathService pathService;

        public RobotEngine(IGameService gameService, IPathService pathService)
        {
            this.gameService = gameService;
            this.pathService = pathService;
        }

        /// <summary>
        /// Go to next place
        /// </summary>
        /// <param name="mazeId">Maze Id</param>
        /// <returns>Return ResponseMove object</returns>
        public ResponseMove GotoNext(string mazeId)
        {
            var pathWay = this.pathService.FindMoves(mazeId);
            var result = this.gameService.Move(mazeId, pathWay.NextMove);
            return result;
        }
    }
}
