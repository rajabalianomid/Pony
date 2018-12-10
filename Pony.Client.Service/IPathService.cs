namespace Pony.Client.Service
{
    using Pony.Client.Service.Model.Robot;

    public interface IPathService
    {
        /// <summary>
        /// Find Pony path
        /// </summary>
        /// <param name="mazeId">Maze Id</param>
        /// <returns>Return PathWay</returns>
        PathWay FindMoves(string mazeId);
    }
}
