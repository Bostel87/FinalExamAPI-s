using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FinalExam.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.Codigo = CreateApiKey();
            return View();
        }

        public static string CreateApiKey()
        {
            var bytes = new byte[256 / 8];
            using (var aleatorio = RandomNumberGenerator.Create())
                aleatorio.GetBytes(bytes);
            return ToBase62String(bytes);
        }

        private static string ToBase62String(byte[] bytes)
        {
            const string alfabeto = "0123456789abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            BigInteger div = new BigInteger(bytes);
            var builder = new StringBuilder();
            while (div != 0)
            {
                div = BigInteger.DivRem(div, alfabeto.Length, out BigInteger remainder);
                builder.Insert(0,alfabeto[Math.Abs(((int)remainder))]);

            }
            return builder.ToString();
        }
    }
}
