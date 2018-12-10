namespace Pony.Client.Service.Model
{
    using System.Collections.Generic;
    using RestSharp.Serializers;

    public class GameState : ResponseBase
    {
        [SerializeAs(Name = "pony")]
        public string StateResult { get; set; }
    }
}