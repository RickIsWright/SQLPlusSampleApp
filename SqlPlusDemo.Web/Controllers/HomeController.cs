using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlPlus.Data.Default;
using SqlPlus.Data.Default.Models;
using SqlPlusDemo.Settings;
using SqlPlusDemo.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SqlPlusDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        [HttpGet]
        public IActionResult Feedback()
        {
            //Instantiate the derived view model and pass to view
            return View(new FeedbackInputViewModel());
        }

        [HttpPost]
        public IActionResult Feedback(FeedbackInputViewModel input)
        {
            if (!input.IsValid())
            {
                //This is only needed for cases where the IsValid method is overriden
                AddModelErrors(input);

                //Return the input object
                return View(input);
            }

            Service service = new Service(ConnectionStrings.Default);

            //Since the FeedbackInputViewModel derives from the FeedbackUpsertInput object,
            //which implements the IFeedbackUpsertInput we can pass the derived object to the service.
            //No silly mapping between objects
            var result = service.FeedbackUpsert(input);

            return new ContentResult { Content = $"Action: {result.ReturnValue} FeedbackId: {result.FeedbackId}" };
        }

        //Helper to add model state errors
        private void AddModelErrors(IValidInput input)
        {
            foreach (ValidationResult vr in input.ValidationResults)
            {
                if (vr.MemberNames == null)
                {
                    ModelState.AddModelError("", vr.ErrorMessage);
                }
                else
                {
                    foreach (string key in vr.MemberNames)
                    {
                        ModelState.AddModelError(key, vr.ErrorMessage);
                    }
                }
            }
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
