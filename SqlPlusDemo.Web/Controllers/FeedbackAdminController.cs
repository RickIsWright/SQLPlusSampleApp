using Microsoft.AspNetCore.Mvc;
using SqlPlus.Data;
using SqlPlus.Data.Models;
using SqlPlusDemo.Settings;

namespace SqlPlusDemo.Web.Controllers
{
    public class FeedbackAdminController : Controller
    {
        public IActionResult Index(int page = 1)
        {
            int pageSize = 20;
            
            Service service = new Service(ConnectionStrings.Default);

            //call the service passing in the page number and page size
            var result = service.FeedbackPaged(
                new FeedbackPagedInput 
                { 
                    PageNumber = page, PageSize = pageSize 
                });

            //We need this in the view
            ViewBag.Page = page;

            return View(result);
        }

        public IActionResult Delete(int id, int page)
        {
            Service service = new Service(ConnectionStrings.Default);
            
            //call the service passing in the id
            service.FeedbackDelete(new FeedbackDeleteInput { FeedbackId = id });

            return RedirectToAction(nameof(Index), new { page = page });
        }
    }
}