namespace Pony.Client.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using Pony.Client.Service.Model.Robot;
    using static Pony.Client.Service.Model.Robot.PathWay;

    public class PathService : IPathService
    {
        public PathService(IGameService gameService)
        {
            this.GameService = gameService;
            this.Player = new Player();
            this.FinalWay = new List<PathWay>();
        }

        private List<PathWay> FinalWay { get; set; }

        private Player Player { get; set; }

        private int Height { get; set; }

        private int Width { get; set; }

        private IGameService GameService { get; }

        /// <summary>
        /// Find all open path from pony
        /// </summary>
        /// <param name="mazeId">Maze Id</param>
        /// <returns>Return Player object</returns>
        private Player FindAllowWalks(string mazeId)
        {
            var foundCurrentState = this.GameService.ChangeCurrentState(mazeId);
            this.Width = foundCurrentState.Size[0];
            this.Height = foundCurrentState.Size[1];
            var ponyPosition = foundCurrentState.Pony[0];
            var domokunPosition = foundCurrentState.Domokun[0];

            this.Player = new Player
            {
                PCurrentState = ponyPosition,
                DCurrentState = foundCurrentState.Domokun[0],
                EndState = foundCurrentState.EndPoint[0],
                PonyPlace = new Player.PonyPosition
                {
                    EPosition = (ponyPosition + 1) % this.Width != 0 && !foundCurrentState.Data.ToList()[ponyPosition + 1].Contains(this.ConvertToString(MoveType.West)) ? ponyPosition + 1 : ponyPosition,
                    NPosition = !foundCurrentState.Data.ToList()[ponyPosition].Contains(this.ConvertToString(MoveType.North)) ? ponyPosition - this.Width : ponyPosition,
                    SPosition = ponyPosition + this.Width < (this.Width * this.Height) && !foundCurrentState.Data.ToList()[ponyPosition + this.Width].Contains(this.ConvertToString(MoveType.North)) ? ponyPosition + this.Width : ponyPosition,
                    WPosition = !foundCurrentState.Data.ToList()[ponyPosition].Contains(this.ConvertToString(MoveType.West)) ? ponyPosition - 1 : ponyPosition,
                },
                Walk = foundCurrentState.Data.ToList().Select((item, index) =>
                {
                    return new Way
                    {
                        Position = index,
                        West = !item.Contains(this.ConvertToString(MoveType.West)),
                        North = !item.Contains(this.ConvertToString(MoveType.North)),
                        East = (index + 1) % this.Width != 0 ? !foundCurrentState.Data.ToList()[index + 1].Contains(this.ConvertToString(MoveType.West)) : false,
                        South = index + this.Width < (this.Width * this.Height) ? !foundCurrentState.Data.ToList()[index + this.Width].Contains(this.ConvertToString(MoveType.North)) : false
                    };
                }).ToList()
            };
            return this.Player;
        }

        /// <summary>
        /// Find finall move
        /// </summary>
        /// <param name="mazeId">Maze Id</param>
        /// <returns>Return PathWay object</returns>
        public PathWay FindMoves(string mazeId)
        {
            this.FindAllowWalks(mazeId);
            int fromPosition = this.Player.PCurrentState;
            int gotoPosition = this.Player.EndState;
            Way current = this.Player.Walk[fromPosition];
            var rightWay = new List<PathWay>();
            if (current.East)
            {
                rightWay.Add(new PathWay(fromPosition + 1, new List<MoveType> { MoveType.East }));
            }

            if (current.West)
            {
                rightWay.Add(new PathWay(fromPosition - 1, new List<MoveType> { MoveType.West }));
            }

            if (current.North)
            {
                rightWay.Add(new PathWay(fromPosition - this.Width, new List<MoveType> { MoveType.North }));
            }

            if (current.South)
            {
                rightWay.Add(new PathWay(fromPosition + this.Width, new List<MoveType> { MoveType.South }));
            }

            rightWay.ForEach(f => this.FindNextMove(f, f.Path.Last(), gotoPosition));
            rightWay.ForEach(f =>
            {
                if (f.Position == gotoPosition)
                {
                    this.FinalWay.Add(f);
                }
            });
            return this.FinalWay.OrderByDescending(o => o.PathCount).FirstOrDefault();
        }

        private void FindNextMove(PathWay rightWay, MoveType lastMove, int gotoPosition)
        {
            var current = this.Player.Walk[rightWay.Position];
            var nextRightWay = new List<PathWay>();
            while (true)
            {
                if (current.East && lastMove != MoveType.West)
                {
                    rightWay.Path.Add(MoveType.East);
                    nextRightWay.Add(new PathWay(rightWay.Position + 1, rightWay.Path));
                }

                if (current.West && lastMove != MoveType.East)
                {
                    rightWay.Path.Add(MoveType.West);
                    nextRightWay.Add(new PathWay(rightWay.Position - 1, rightWay.Path));
                }

                if (current.North && lastMove != MoveType.South)
                {
                    rightWay.Path.Add(MoveType.North);
                    nextRightWay.Add(new PathWay(rightWay.Position - this.Width, rightWay.Path));
                }

                if (current.South && lastMove != MoveType.North)
                {
                    rightWay.Path.Add(MoveType.South);
                    nextRightWay.Add(new PathWay(rightWay.Position + this.Width, rightWay.Path));
                }

                nextRightWay.ForEach(f =>
                {
                    if (f.Position == gotoPosition)
                    {
                        this.FinalWay.Add(f);
                    }

                    this.FindNextMove(f, f.Path.Last(), gotoPosition);
                });
                break;
            }
        }

        /// <summary>
        /// Conver MoveType to string lower
        /// </summary>
        /// <param name="moveType">Move Type</param>
        /// <returns>Return string</returns>
        private string ConvertToString(MoveType moveType)
        {
            return moveType.ToString().ToLower();
        }
    }
}
