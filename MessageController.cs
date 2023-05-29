using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;
using WebApplication8.Services;
using WebApplication8.ViewModel;

namespace WebApplication8.Controllers
{
    public class MessageController : Controller
    {
        private readonly MessageDBService messageService = new MessageDBService();
        public ActionResult Index(int P_Id = 1)
        {
            ViewData["P_Id"] = P_Id;
            return PartialView();
        }
        public ActionResult MessageList(int P_Id, int Page = 1)
        {
            MessageViewModel Data = new MessageViewModel();
            Data.Paging = new ForPaging(Page);
            Data.P_Id = P_Id;
            Data.DataList = messageService.GetDataList(Data.Paging, Data.P_Id);
            return PartialView(Data);
        }

        public ActionResult Create(int P_Id)
        {
            ViewData["P_Id"] = P_Id;
            return PartialView();
        }

        [HttpPost]
        public ActionResult Add(int P_Id, [Bind(Include = "Content")] Message Data)
        {
            Data.P_Id = P_Id;
            Data.Account = User.Identity.Name;
            messageService.InsertMessage(Data);
            return RedirectToAction("Index", new { P_Id = P_Id });
        }

        public ActionResult UpdateMessage(int P_Id, int M_Id)
        {
            Message Data = new Message();
            Data.P_Id = P_Id;
            Data.M_Id = M_Id;
            Data = messageService.ContentMessage(P_Id, M_Id);
            return PartialView(Data);
        }
        [HttpPost]
        public ActionResult UpdateMessage(int P_Id, int M_Id, string Content)
        {
            Message message = new Message();
            message.P_Id = P_Id;
            message.M_Id = M_Id;
            message.Content = Content;
            messageService.UpdateMessage(message);
            return RedirectToAction("Item", "Item", new { Id = P_Id });
        }

        public ActionResult DeleteMessage(int P_Id, int M_Id)
        {
            messageService.DeleteMessage(P_Id, M_Id);
            return RedirectToAction("Item", "Item", new { Id = P_Id });
        }
    }
}