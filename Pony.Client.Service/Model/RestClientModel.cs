namespace Pony.Client.Service.Model
{
    using RestSharp;

    public class RestClientModel
    {
        public RestClientModel()
        {
            this.HttpMethod = Method.POST;
        }

        public string RequestBody { get; set; }

        public object RouteValues { get; set; }

        public Method HttpMethod { get; set; }
    }
}
