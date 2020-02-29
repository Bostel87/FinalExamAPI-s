using FinalExam.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalExam.Controllers
{
    public class FinalExamController : ApiController
    {

        //Instaciamos el EF para poder hacer uso del Modelo de la db
        private readonly TestEntities db = new TestEntities();
        public IHttpActionResult Get()
        {
            var Notas = db.Notas.ToList();

            return Json(Notas);
        }

        public IHttpActionResult Get(int id)
        {
            var cita = db.Notas.Where(w => w.CODIGO == id).FirstOrDefault();

            return Json(cita);
        }
        [Route("api/FinalExam/busquedatitulo/{titulo}")]
        [HttpGet]
        public IHttpActionResult Get(string titulo)
        {
            var Notas = db.Notas.Where(c => c.TITULO == titulo).Select(x => new { x.TITULO, x.TEXTO}).ToList();

            return Json(Notas);
        }
        public IHttpActionResult Get(DateTime fecha)
        {
            var Notas = db.Notas.Where(c => c.FECHA == fecha).FirstOrDefault();

            return Json(Notas);
        }

        public IHttpActionResult Post([FromUri]Notas notas)
        {
            if (ModelState.IsValid)
            {
                db.Notas.Add(notas);
                db.SaveChanges();
                return Json(new { Resultado = "Registro Guardado" });
            }
            else
            {
                return Json(new { Resultado = "Fallo Al Guardar" });
            }
        }

        public IHttpActionResult Put([FromUri]Notas notas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notas).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Resultado = "Registro Actualizado" });
            }
            else
            {
                return Json(new { Resultado = "Fallo Al Modificar" });
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var CitaBusqueda = db.Notas.Where(w => w.CODIGO == id).FirstOrDefault();
                db.Notas.Remove(CitaBusqueda);
                db.SaveChanges();
                return Json(new { Resultado = "Registro Eliminado" });
            }
            catch (Exception e)
            {
                return Json(new { Resultado = e.ToString() });


            }
        }



    }


}
