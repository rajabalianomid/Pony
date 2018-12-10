namespace Pony.Client.Controllers
{
    using System.Web.Mvc;
    using Pony.Client.Mapper;
    using Pony.Client.Models;
    using Pony.Client.Service;
    using static Pony.Client.Service.Model.Robot.PathWay;

    public class GameController : Controller
    {
        public GameController(IPathService pathService, IRobotEngine robotEngine, IGameService gameService)
        {
            this.PathService = pathService;
            this.RobotEngine = robotEngine;
            this.GameService = gameService;
        }

        public IPathService PathService { get; }

        public IRobotEngine RobotEngine { get; }

        public IGameService GameService { get; }

        /// <summary>
        /// Show game page
        /// </summary>
        /// <param name="id">maze id</param>
        /// <returns>Return View Index</returns>
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return this.RedirectToAction("index", "home");
            }

            return this.View(nameof(this.Index), new DisplayModel { Id = id });
        }

        /// <summary>
        /// Manual movment
        /// </summary>
        /// <param name="model">Movment instruction</param>
        /// <returns>Return Json</returns>
        [ValidateAntiForgeryToken]
        public JsonResult Move(MoveModel model)
        {
            var response = this.GameService.Move(model.MazeId, (MoveType)model.Direction).ToMap();
            response.Data = this.GameService.Print(model.MazeId);
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Print Game
        /// </summary>
        /// <param name="id">Maze Id</param>
        /// <returns>Return View Print</returns>
        public ActionResult Print(string id)
        {
            var response = new ResponseMoveModel
            {
                Data = this.GameService.Print(id)
            };
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Find direction automatic
        /// </summary>
        /// <param name="model">Movment instruction</param>
        /// <returns>Return View Auto Move</returns>
        [ValidateAntiForgeryToken]
        public JsonResult AutoMove(MoveModel model)
        {
            var response = this.RobotEngine.GotoNext(model.MazeId).ToMap();
            response.MazeId = model.MazeId;
            response.IsAuto = true;
            response.Data = this.GameService.Print(model.MazeId);
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}