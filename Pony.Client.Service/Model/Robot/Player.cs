namespace Pony.Client.Service.Model.Robot
{
    using System.Collections.Generic;

    public class Player
    {
        public Player()
        {
            this.PCurrentState = 0;
            this.DCurrentState = 0;
            this.EndState = 0;
            this.Walk = new List<Way>();
        }

        public int PCurrentState { get; set; }

        public int DCurrentState { get; set; }

        public int EndState { get; set; }

        public PonyPosition PonyPlace { get; set; }

        public List<Way> Walk { get; set; }

        public class PonyPosition
        {
            public int WPosition { get; set; }

            public int EPosition { get; set; }

            public int NPosition { get; set; }

            public int SPosition { get; set; }
        }
    }
}
