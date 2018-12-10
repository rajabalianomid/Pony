namespace Pony.Client.Service
{
    using Pony.Client.Service.Model;
    using static Pony.Client.Service.Model.Robot.PathWay;

    public interface IGameService
    {
        /// <summary>
        /// Create new game
        /// </summary>
        /// <param name="name">Pony name</param>
        /// <param name="width">Cell count</param>
        /// <param name="height">Row count</param>
        /// <param name="difficulty">Game difficulty</param>
        /// <returns>Return ResponseNewGame</returns>
        ResponseNewGame NewGame(string name, int width = 15, int height = 15, int difficulty = 0);

        /// <summary>
        /// Update game state
        /// </summary>
        /// <param name="mazeId">Maze id</param>
        /// <returns>Return ResponseCurrentState</returns>
        ResponseCurrentState ChangeCurrentState(string mazeId);

        /// <summary>
        /// Move Pony
        /// </summary>
        /// <param name="mazeId">Maze Id</param>
        /// <param name="move">Move Type</param>
        /// <returns>Return ResponseMove</returns>
        ResponseMove Move(string mazeId, MoveType move = MoveType.Stay);

        /// <summary>
        /// Print
        /// </summary>
        /// <param name="mazeId">Maze Id</param>
        /// <returns>Return Game Background String</returns>
        string Print(string mazeId);
    }
}
