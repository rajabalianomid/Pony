namespace Pony.Client.Service.Model
{
    using System.Collections.Generic;
    using RestSharp.Serializers;

    public class ResponseCurrentState
    {
        [SerializeAs(Name = "pony")]
        public List<int> Pony { get; set; }

        [SerializeAs(Name = "domokun")]
        public List<int> Domokun { get; set; }

        [SerializeAs(Name = "end-point")]
        public List<int> EndPoint { get; set; }

        [SerializeAs(Name = "size")]
        public List<int> Size { get; set; }

        [SerializeAs(Name = "difficulty")]
        public int Difficulty { get; set; }

        [SerializeAs(Name = "data")]
        public List<List<object>> Data { get; set; }

        [SerializeAs(Name = "maze_id")]
        public string MazeId { get; set; }

        [SerializeAs(Name = "game-state")]
        public GameState GameState { get; set; }
    }
}