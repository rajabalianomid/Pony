using Pony.Client.Models;
using Pony.Client.Service.Model;

namespace Pony.Client.Mapper
{
    public static class Mapping
    {
        public static ResponseMoveModel ToMap(this ResponseMove model)
        {
            return new ResponseMoveModel
            {
                MazeId = model.StateResult,
                State = model.State
            };
        }
    }
}