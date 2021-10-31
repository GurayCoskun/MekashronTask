using Mekashron_Login.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            LoginProcess(response);
            return RedirectToAction("Index");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string email, string Password, string FirstName,string LastName,string Mobile,int CountryID, int aID, string SignupIP, string retry)
        {
            
            ServiceReference1.ICUTechClient refs = new ServiceReference1.ICUTechClient();
            var response = refs.RegisterNewCustomerAsync(email,Password,FirstName,LastName,Mobile,CountryID, aID, SignupIP).Result.@return;
            var result = RegisterResult(response);
            if (result.EntityId != -1)
            {
                Success("Registration Successful.");
                return RedirectToAction("Index");
            }
            Error(result.ResultMessage.Split(':')[1]);
            return RedirectToAction("Register");
              
        }
        //Functions
        public void LoginProcess(string x)
        {
            LoginSucces res = new LoginSucces();
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(x)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(LoginSucces));
                res = (LoginSucces)deserializer.ReadObject(ms);
            }
            if (res.FirstName==null){
                Result res1 = new Result();
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(x)))
                {
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Result));
                    res1 = (Result)deserializer.ReadObject(ms);
                }
                Error(res1.ResultMessage);
            }
            else Success("Welcome " + res.FirstName + " " + res.LastName);          
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
        public void Success(string message)
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
