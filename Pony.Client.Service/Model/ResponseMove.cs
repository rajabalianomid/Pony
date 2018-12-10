namespace Pony.Client.Service.Model
{
    using RestSharp.Serializers;

    public class ResponseMove : ResponseBase
    {
        [SerializeAs(Name = "stateresult")]
        public string StateResult { get; set; }

        [SerializeAs(Name = "hiddenurl")]
        public string HiddenUrl { get; set; }
    }
}
