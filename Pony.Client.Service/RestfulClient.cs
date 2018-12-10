namespace Pony.Client.Service
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using Pony.Client.Service.Model;
    using RestSharp;
    using RestSharp.Deserializers;

    public class RestfulClient : IRestfulClient, IDisposable
    {
        private readonly string endpoint = null;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestfulClient"/> class.
        /// Instance of RestfulClient
        /// </summary>
        /// <param name="endpoint">set endpointName in webconfig for example : https://ponychallenge.trustpilot.com/pony-challenge/maze by default endpointName = "PonyEndPoint" </param>
        public RestfulClient()
            : this("PonyEndPoint")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestfulClient"/> class.
        /// Instance of RestfulClient
        /// </summary>
        /// <param name="endpoint">change endpointName for example : https://ponychallenge.trustpilot.com/pony-challenge/maze by default endpointName = "PonyEndPoint" </param>
        public RestfulClient(string endpoint, bool isAddress = false)
        {
            this.endpoint = isAddress ? endpoint : ConfigurationManager.AppSettings[endpoint];
        }

        /// <summary>
        /// Execute Api
        /// </summary>
        /// <typeparam name="T">Return object type</typeparam>
        /// <param name="model">Included body request,Http Method and RouteValues</param>
        /// <returns>Return T</returns>
        public T Execute<T>(RestClientModel model)
            where T : class, new()
        {
            var result = new T();
            if (model.HttpMethod == Method.POST && model.RequestBody == null)
            {
                throw new ArgumentNullException("RequestBody");
            }

            string queryString = model.RouteValues != null ? this.ConvertRouteValuesToQueryString(model.RouteValues) : null;
            IRestClient client = new RestClient(this.endpoint + queryString);
            var request = new RestRequest(model.HttpMethod);
            request.AddHeader("pony-token", Guid.NewGuid().ToString());
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            if (model.RequestBody != null)
            {
                request.AddParameter("application/json", model.RequestBody, ParameterType.RequestBody);
            }

            var response = client.Execute<T>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.Content);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
            {
                result = new JsonDeserializer().Deserialize<T>(response);
            }

            if (result is IResponseBase)
            {
                (result as IResponseBase).Content = response.Content;
            }

            return result;
        }

        private string ConvertRouteValuesToQueryString(object routeValues)
        {
            var properties = routeValues.GetType().GetProperties()
                .Where(w => w.GetValue(routeValues, null) != null)
                .Select(s => s.Name + "=" + HttpUtility.UrlEncode(s.GetValue(routeValues, null).ToString())).ToList();
            var foundId = properties.FirstOrDefault(f => f.Split('=')[0].ToLower().StartsWith("id"));
            string url = null;
            if (foundId != null)
            {
                properties.Remove(foundId);
                url = foundId.ToLower().Replace("id=", "/");
            }
            properties.Where(f => string.IsNullOrEmpty(f.Split('=')[1])).ToList().ForEach(f => url += ("/" + f.Replace("=", null)));
            return url + string.Join("&", properties.Where(f => !string.IsNullOrEmpty(f.Split('=')[1])));
        }

        public void Dispose() => GC.SuppressFinalize(this);

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }
            }

            this.disposed = true;
        }
    }
}
