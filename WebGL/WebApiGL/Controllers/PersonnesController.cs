using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiGL.Models;

namespace WebApiGL.Controllers
{
    public class PersonnesController : ApiController
    {
        bdWebGl0Entities db = new bdWebGl0Entities();

        // GET: api/Personnes
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        public List<Personnes> GetAllPersonnes()
        {
            return db.Personnes.ToList();
        }


        //// GET: api/Personnes/5
        [HttpGet]
        public Personnes GetPersonneById(int id)
        {
            return db.Personnes.Find(id);
        }

        // POST: api/Personnes
        [HttpPost]
        public HttpResponseMessage AddPersonne(Personnes personnes)
        {
      
            try
            {
                db.Personnes.Add(personnes);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        // PUT: api/Personnes/5
        [HttpPut]
        public HttpResponseMessage UpdatePersonne(int id, Personnes personnes)
        {
            try
            {
                if (id == personnes.IdPersonne)
                {
                    db.Entry(personnes).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return response;
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        // DELETE: api/Personnes/5
        [HttpDelete]
        public HttpResponseMessage DeletePersonne(int id)
        {
            try
            {
                Personnes personnes = db.Personnes.Find(id);
                if (personnes != null)
                {
                    db.Personnes.Remove(personnes);
                    db.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return response;
                }
            }
            catch
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }
    }
}
