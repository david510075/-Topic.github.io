using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;
using WebApplication8.Services;
using WebApplication8.ViewModel;

namespace WebApplication8.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemService itemService = new ItemService();
        private readonly MessageDBService messageService = new MessageDBService();


        public ActionResult Index(Search Search, int Page )
        {
            ItemViewModel Data = new ItemViewModel();
            Data.Account = User.Identity.Name;
            if (string.IsNullOrEmpty(Search.Sanimal))
            {
                if (string.IsNullOrEmpty(Search.Ssex))
                {
                    if (string.IsNullOrEmpty(Search.Splace))
                    {
                        Data.Paging = new ForPaging(Page);
                        Data.IdList = itemService.GetIdList(Data.Paging);
                        Data.ItemBlock = new List<ItemDetailViewModel>();
                        foreach (var Id in Data.IdList)
                        {
                            ItemDetailViewModel newBlock = new ItemDetailViewModel();
                            newBlock.Data = itemService.GetDataById(Id);
                            Data.ItemBlock.Add(newBlock);
                        }
                        return View(Data);
                    }
                    else
                    {
                        Data.ItemBlock = new List<ItemDetailViewModel>();
                        Data.Search = Search;
                        Data.Paging = new ForPaging(Page);
                        Data.IdList = itemService.GetIdLists(Search);
                        foreach (var Id in Data.IdList)
                        {
                            ItemDetailViewModel newBlock = new ItemDetailViewModel();
                            newBlock.Data = itemService.GetAllDataList(Id);
                            Data.ItemBlock.Add(newBlock);
                        }
                        return View(Data);
                    }
                }
                else
                {
                    Data.ItemBlock = new List<ItemDetailViewModel>();
                    Data.Search = Search;
                    Data.Paging = new ForPaging(Page);
                    Data.IdList = itemService.GetIdLists(Search);
                    foreach (var Id in Data.IdList)
                    {
                        ItemDetailViewModel newBlock = new ItemDetailViewModel();
                        newBlock.Data = itemService.GetAllDataList(Id);
                        Data.ItemBlock.Add(newBlock);
                    }
                    return View(Data);
                }
            }
            else
            {
                Data.ItemBlock = new List<ItemDetailViewModel>();
                Data.Search = Search;
                Data.Paging = new ForPaging(Page);
                Data.IdList = itemService.GetIdLists(Search);
                foreach(var Id in Data.IdList)
                {
                    ItemDetailViewModel newBlock = new ItemDetailViewModel();
                    newBlock.Data = itemService.GetAllDataList(Id);
                    Data.ItemBlock.Add(newBlock);
                }
                return View(Data);
            }

        }
        
        public ActionResult Item(int Id)
        {
            ItemDetailViewModel ViewData = new ItemDetailViewModel();
            ForPaging paging = new ForPaging(0);
            ViewData.DataList = messageService.GetDataList(paging, Id);
            ViewData.Data = itemService.GetDataById(Id);
            
            return View(ViewData);
        }

        public ActionResult ItemBlock(int Id)
        {
            ItemDetailViewModel ViewData = new ItemDetailViewModel();
            ViewData.Data = itemService.GetDataById(Id);
            return PartialView(ViewData);
        }
        [Authorize]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [Authorize]

        public ActionResult Create(ItemCreateViewModel Data)
        {
            if(Data.ItemImage != null)
            {
                string filename = Path.GetFileName(Data.ItemImage.FileName);
                string Url = Path.Combine(Server.MapPath("~/Upload/"), filename);
                Data.ItemImage.SaveAs(Url);
                Data.NewData.Image = filename;
                Data.NewData.Account = User.Identity.Name;
                itemService.Insert(Data.NewData);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("ItemImage", "請選擇上傳檔案");
                return RedirectToAction("Index");
            }
        }
        [Authorize]
        public ActionResult EditPage(int A_Id)
        {
            Item Data = new Item();
            Data = itemService.GetDataById(A_Id);
            return PartialView(Data);
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditPage(int Id, Item Data)
        {
            if (itemService.CheckUpdate(Id))
            {
                itemService.UpdateArticle(Data);
            }
            return RedirectToAction("Item", new { Id = Id });
        }
        [Authorize]
        public ActionResult Delete(int Id)
        {
            itemService.Delete(Id);
            return RedirectToAction("Index");
        }
        

    }
}