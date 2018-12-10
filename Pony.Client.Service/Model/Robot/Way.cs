namespace Pony.Client.Service.Model.Robot
{
    public class Way
    {
        public Way()
        {
        }

        public int Position { get; set; }

        public bool North { get; set; }

        public bool South { get; set; }

        public bool West { get; set; }

        public bool East { get; set; }
    }
}
