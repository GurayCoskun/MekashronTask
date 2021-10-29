using Mekashron_Login.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Mekashron_Login.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string UserName, string Password)
        {
            ServiceReference1.ICUTechClient refs = new ServiceReference1.ICUTechClient();
            var response=refs.LoginAsync(UserName, Password, "IPs").Result.@return;
            var result= LoginResult(response);
            if (result.ResultCode==-1) Error(result.ResultMessage); 
            else Suscess(result.ResultMessage);
            return RedirectToAction("Index");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string email, string Password, string FirstName,string LastName,string Mobile,int CountryID, int aID, string SignupIP, string retry)
        {
            if (!Password.Equals(retry)) 
            {
                Error("Passwords not matched");
                return RedirectToAction("Register");
            }
            else
            {
                ServiceReference1.ICUTechClient refs = new ServiceReference1.ICUTechClient();
                var response = refs.RegisterNewCustomerAsync(email,Password,FirstName,LastName,Mobile,CountryID, aID, SignupIP).Result.@return;
                var result = RegisterResult(response);
                Error(result.ResultMessage.Split(':')[1]);
                return RedirectToAction("Register");
            }
        }
        //Functions
        public Result LoginResult(string x)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(x)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Result));
                Result res = (Result)deserializer.ReadObject(ms);
                return res;
            }
        }
        public RegisterResult RegisterResult(string x)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(x)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(RegisterResult));
                RegisterResult res = (RegisterResult)deserializer.ReadObject(ms);
                return res;
            }
        }
        public void Suscess(string message)
        {
            List<string> messages = TempData["Success"] as List<string> ?? new List<string>();
            messages.Add(message);
            TempData["Success"] = messages;
        }
        public void Error(string message)
        {
            List<string> messages = TempData["Error"] as List<string> ?? new List<string>();
            messages.Add(message);
            TempData["Error"] = messages;
        }
    }
}
