namespace Pony.Client.Service.Model
{
    using RestSharp.Serializers;

    public class ResponseBase : IResponseBase
    {
        [SerializeAs(Name = "state")]
        public string State { get; set; }

        public string Content { get; set; }
    }
}
