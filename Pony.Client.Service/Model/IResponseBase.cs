namespace Pony.Client.Service.Model
{
    public interface IResponseBase
    {
        string State { get; set; }

        string Content { get; set; }
    }
}
