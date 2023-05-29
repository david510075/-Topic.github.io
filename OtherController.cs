using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;
using WebApplication8.ViewModel;
using WebApplication8.Services;

namespace WebApplication8.Controllers
{
    public class OtherController : Controller
    {
        private readonly ItemService itemService = new ItemService();
        // GET: Other
        public ActionResult shelter()
        {
            return View();
        }

        public ActionResult Knowledge()
        {
            return View();
        }
        public ActionResult dog()
        {
            return View();
        }
        public ActionResult cat()
        {
            return View();
        }
        public ActionResult Backstage()
        {

            return View();
        }
        public ActionResult MBackstage()
        {
            List<User> DataList = new List<User>();
            DataList = itemService.GetUser();
            return PartialView(DataList);
        }
        public ActionResult PBackstage()
        {
            Item Data = new Item();
            Data = itemService.GetPet();
            return PartialView(Data);
        }
        public ActionResult DeleteM(int Account)
        {
            itemService.DeleteUser(Account);
            return RedirectToAction("MBackstage");
        }
        public ActionResult DeleteP(int Id)
        {
            itemService.DeletePet(Id);
            return RedirectToAction("PBackstage");
        }

    }
}