using FinalExam.Handlers;
using FinalExam.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FinalExam.Controllers
{
    public class UsuariosController : Controller
    {
        
        // GET: Usuarios
        public ActionResult Index()
        {
            using (TestEntities db = new TestEntities()) {
                var usuarios = db.USUARIOS.ToList();
                return View(usuarios);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(USUARIOS usu)
        {
            using (TestEntities db = new TestEntities())
            {
                if (ModelState.IsValid)
                {
                    usu.APIKEY = CreateApiKey();
                    db.USUARIOS.Add(usu);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
        }

        public ActionResult Details(int id)
        {
            using (TestEntities db = new TestEntities())
            {
                return View(db.USUARIOS.Where(s => s.CODIGO == id).FirstOrDefault());
            }
            
        }

        public ActionResult Edit(int id)
        {
            using (TestEntities db = new TestEntities())
            {
                return View(db.USUARIOS.Where(s => s.CODIGO == id).FirstOrDefault());
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, USUARIOS usu)
        {
            try
            {
                using (TestEntities db = new TestEntities()) 
                {
                    db.Entry(usu).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            using (TestEntities db = new TestEntities())
            {
                return View(db.USUARIOS.Where(s => s.CODIGO == id).FirstOrDefault());
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, USUARIOS usu)
        {
            try
            {
                using (TestEntities db = new TestEntities())
                {
                    USUARIOS us = db.USUARIOS.Where(x => x.CODIGO == id).FirstOrDefault();
                    db.USUARIOS.Remove(us);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }
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
                builder.Insert(0, alfabeto[Math.Abs(((int)remainder))]);

            }
            return builder.ToString();
        }
    }
}