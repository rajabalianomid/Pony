namespace Pony.Client.Service
{
    using Pony.Client.Service.Model;

    public interface IRobotEngine
    {
        /// <summary>
        /// Go to next place
        /// </summary>
        /// <param name="mazeId">Maze Id</param>
        /// <returns>Return ResponseMove</returns>
        ResponseMove GotoNext(string mazeId);
    }
}
