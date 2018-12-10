namespace Pony.Client.Service.Model
{
    using RestSharp.Serializers;

    public class ResponseNewGame : ResponseBase
    {
        [SerializeAs(Name = "maze_id")]
        public string MazeId { get; set; }
    }
}