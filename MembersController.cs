using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Security;
using WebApplication8.Services;
using WebApplication8.ViewModel;
using WebApplication8.Models;
using System.Web.Configuration;

namespace WebApplication8.Controllers
{
    public class MembersController : Controller
    {
        private readonly MembersDBService membersService = new MembersDBService();
        private readonly MailService mailService = new MailService();

        public string RoleData()
        {
            string RoleData = membersService.GetRole(User.Identity.Name);
            return RoleData;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Item");
            return View();
        }
        [HttpPost]
        public ActionResult Register(User newMember)
        {
            if (membersService.AccountCheck(newMember.Account))
            {
                if (ModelState.IsValid)
                {
                    membersService.Register(newMember);
                    string RoleData = membersService.GetRole(newMember.Account);
                    JwtService jwtService = new JwtService();
                    string cookieName = WebConfigurationManager.AppSettings["CookieName"].ToString();
                    string Token = jwtService.GenerateToken(newMember.Account, RoleData);
                    HttpCookie cookie = new HttpCookie(cookieName);
                    cookie.Value = Server.UrlEncode(Token);
                    Response.Cookies.Add(cookie);
                    Response.Cookies[cookieName].Expires = DateTime.Now.AddMinutes(Convert.ToInt32(WebConfigurationManager.AppSettings["ExpireMinutes"]));
                    return RedirectToAction("Index", "Item");
                }
                else
                {
                    newMember.Account = null;
                    newMember.Password = null;
                    return View(newMember);
                }
            }
            else
            {
                ModelState.AddModelError("Account", "此帳號已註冊過");
                return View();
            }

        }


        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Item");
            return View();

        }
        [HttpPost]
        public ActionResult Login(MembersLoginViewModel LoginMember)
        {

            string ValidateStr = membersService.LoginCheck(LoginMember.Account, LoginMember.Password);
            if (string.IsNullOrEmpty(ValidateStr))
            {
                string RoleData = membersService.GetRole(LoginMember.Account);
                JwtService jwtService = new JwtService();
                string cookieName = WebConfigurationManager.AppSettings["CookieName"].ToString();
                string Token = jwtService.GenerateToken(LoginMember.Account, RoleData);
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Value = Server.UrlEncode(Token);
                Response.Cookies.Add(cookie);
                Response.Cookies[cookieName].Expires = DateTime.Now.AddMinutes(Convert.ToInt32(WebConfigurationManager.AppSettings["ExpireMinutes"]));
                return RedirectToAction("Index", "Item");
            }
            else
            {
                ModelState.AddModelError("", ValidateStr);
                return View(LoginMember);
            }

        }
        public ActionResult Logout()
        {
            string cookieName = WebConfigurationManager.AppSettings["CookieName"].ToString();
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Expires = DateTime.Now.AddDays(-1);
            cookie.Values.Clear();
            Response.Cookies.Set(cookie);
            return RedirectToAction("Login");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel ChangeData)
        {
            if (ModelState.IsValid)
            {
                ViewData["ChangeState"] = membersService.ChangePassword(User.Identity.Name, ChangeData.Password, ChangeData.NewPassword);
            }
            return View();
        }
    }
}