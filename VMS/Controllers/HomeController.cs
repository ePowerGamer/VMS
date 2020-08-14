using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VMS.Models;

namespace VMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //GET
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountModel accountModel)
        {
            using (AccountContext db = new AccountContext())
            {
                AccountModel account = db.Accounts.Find(accountModel.Username);
                if (account == null)
                {
                    ViewData["Error"] = "Account not found.";
                    return View();
                }
                else
                {
                    HashAlgorithm sha = SHA256.Create();
                    byte[] hashed = sha.ComputeHash(Encoding.ASCII.GetBytes(accountModel.Password + account.Salt));
                    string pw = Convert.ToBase64String(hashed);

                    if (pw == account.Password)
                    {
                        Session["Account"] = account;
                        return View("Index");
                    }
                    else
                    {
                        ViewData["Error"] = "Login failed. Check your username and password.";
                        return View();
                    }
                }
            }
        }
    }
}