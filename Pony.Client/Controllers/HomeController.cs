namespace Pony.Client.Controllers
{
    using System.Web.Mvc;
    using Pony.Client.Models;
    using Pony.Client.Service;

    public class HomeController : Controller
    {
        public HomeController(IGameService gameService)
        {
            this.GameService = gameService;
        }

        public IGameService GameService { get; }

        /// <summary>
        /// Default page
        /// </summary>
        /// <returns>Return Index view</returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Init game
        /// </summary>
        /// <param name="model">set pony name , width and height</param>
        /// <returns>Return Index view</returns>
        [HttpPost]
        public ActionResult Index(NewGameModel model)
        {
            if (this.ModelState.IsValid)
            {
                var response = this.GameService.NewGame(((PonyName)model.Name).GetDisplayName(), model.Width, model.Height, model.SelectedDifficulty);
                if (response.MazeId != null)
                {
                    return this.RedirectToAction("Index", "Game", new { id = response.MazeId });
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, response.State);
                }
            }

            return this.View(model);
        }

        /// <summary>
        /// About programmer contact
        /// </summary>
        /// <returns>Return AboutMe view</returns>
        public ActionResult AboutMe()
        {
            return this.View();
        }
    }
}