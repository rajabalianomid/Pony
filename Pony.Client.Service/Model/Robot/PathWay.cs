namespace Pony.Client.Service.Model.Robot
{
    using System.Collections.Generic;
    using System.Linq;

    public class PathWay
    {
        public PathWay(int position, List<MoveType> path = null)
        {
            this.Position = position;
            if (this.Path == null)
            {
                this.Path = new List<MoveType>();
            }

            this.Path.AddRange(path);
        }

        public enum MoveType
        {
            Stay = 0,
            North = 1,
            South = 2,
            West = 3,
            East = 4,
            Auto = 5
        }

        internal int Position { get; set; }

        internal MoveType Move { get; set; }

        internal List<MoveType> Path { get; set; }

        internal MoveType NextMove => this.Path.First();

        internal int PathCount => this.Path.Count();
    }
}
