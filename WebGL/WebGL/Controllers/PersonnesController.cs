using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WcfGL.Models;
using PagedList.Mvc;
using PagedList;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WebGL.Controllers
{
    public class PersonnesController : Controller
    {
        //private BdWebGlContext db = new BdWebGlContext();
        private WcfGLs.Service1Client service = new WcfGLs.Service1Client();
        int pageSize = 3;

        // GET: Personnes
        public ActionResult Index(string TelPersonne, string DateNaissancePersonne, int? page)
        {
            page = page.HasValue ? page : 1;
            ViewBag.TelPersonne = !string.IsNullOrEmpty(TelPersonne) ? TelPersonne : string.Empty;
            ViewBag.DateNaissancePersonne = !string.IsNullOrEmpty(DateNaissancePersonne) ? DateNaissancePersonne : string.Empty;
            return View(service.GetPersonnes(TelPersonne, DateNaissancePersonne).ToPagedList((int)page, pageSize));
            //return View(GetAllPersonneAPI().ToPagedList((int)page, pageSize));
            //return View(GetAllPersonneApiPHP().ToPagedList((int)page, pageSize));
        }

        // GET: Personnes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = service.GetPersonneByID((int)id);
            //Personne personne = GetPersonneByIdAPI((int)id);
            //Personne personne = GetPersonneByIdAPIPHP((int)id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // GET: Personnes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personnes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPersonne,NomPrenomPersonne,AdressePersonne,TelPersonne,EmailPersonne,DateNaissancePersonne")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                service.AddPersonne(personne);
                //AddPersonneAPI(personne);
                //AddPersonneAPIPHP(personne);
                return RedirectToAction("Index");
            }

            return View(personne);
        }

        // GET: Personnes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = service.GetPersonneByID((int)id);
            //Personne personne = GetPersonneByIdAPI((int)id);
            //Personne personne = GetPersonneByIdAPIPHP((int)id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // POST: Personnes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPersonne,NomPrenomPersonne,AdressePersonne,TelPersonne,EmailPersonne,DateNaissancePersonne")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                service.UpdatePersonne(personne);
                //UpdatePersonneAPI(personne);
                //UpdatePersonneAPIPHP(personne);
                return RedirectToAction("Index");
            }
            return View(personne);
        }

        // GET: Personnes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = service.GetPersonneByID((int)id);
            //Personne personne = GetPersonneByIdAPI((int)id);
            //Personne personne = GetPersonneByIdAPIPHP((int)id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // POST: Personnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.DeletePersonne((int)id);
            //DeletePersonneAPI((int)id);
            //DeletePersonneAPIPHP();
            return RedirectToAction("Index");
        }


        /*
         *Ecriture des fonctioncs 
         * Permettant la 
         * consomma de l'API  WEBAPIGL 
         */


        public List<Personne> GetAllPersonneAPI()
        {
            HttpClient client;
            client = new HttpClient();
            var personnes = new List<Personne>();
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ServeurApiURL"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync("api/personnes/GetAllPersonnes").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                personnes = JsonConvert.DeserializeObject<List<Personne>>(responseData);
            }
            return personnes;
        }

        public Personne GetPersonneByIdAPI(int id)
        {
            HttpClient client;
            client = new HttpClient();
            var personne = new Personne();
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ServeurApiURL"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync("api/personnes/GetPersonneById/"+id).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                personne = JsonConvert.DeserializeObject<Personne>(responseData);
            }
            return personne;
        }

        public bool AddPersonneAPI(Personne personne)
        {
            bool rep = false;
            string idpersonne = personne.IdPersonne > 0 ? personne.IdPersonne.ToString() : string.Empty;
            var values = new Dictionary<string, string>
                    {
                        { "IdPersonne", idpersonne },
                        { "NomPrenomPersonne", personne.NomPrenomPersonne },
                        { "AdressePersonne", personne.AdressePersonne },
                        { "TelPersonne", personne.TelPersonne },
                        { "EmailPersonne", personne.EmailPersonne },
                        { "DateNaissancePersonne", personne.DateNaissancePersonne.ToString() }
                    };
            var content = new FormUrlEncodedContent(values);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ServeurApiURL"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PostAsync("api/personnes/AddPersonne", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        rep = true;
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {

            }
            return rep;
        }
        public bool UpdatePersonneAPI(Personne personne)
        {
            bool rep = false;
            var values = new Dictionary<string, string>
                    {
                        { "IdPersonne", personne.IdPersonne.ToString() },
                        { "NomPrenomPersonne", personne.NomPrenomPersonne },
                        { "AdressePersonne", personne.AdressePersonne },
                        { "TelPersonne", personne.TelPersonne },
                        { "EmailPersonne", personne.EmailPersonne },
                        { "DateNaissancePersonne", personne.DateNaissancePersonne.ToString() }
                    };
            var content = new FormUrlEncodedContent(values);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ServeurApiURL"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PutAsync("api/personnes/UpdatePersonne/" + personne.IdPersonne, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        rep = true;
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {

            }
            return rep;
        }
        public bool DeletePersonneAPI(int id)
        {
            bool rep = false;
           
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ServeurApiURL"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.DeleteAsync("api/personnes/DeletePersonne/" + id).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        rep = true;
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {

            }
            return rep;
        }


        /*
        *Ecriture des fonctioncs 
        * Permettant la 
        * consomma de l'API  PHP
        */

        public List<Personne> GetAllPersonneApiPHP()
        {
            HttpClient client;
            client = new HttpClient();
            var personnes = new List<Personne>();
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ServeurApiURL1"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync("api/read.php").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                personnes = JsonConvert.DeserializeObject<List<Personne>>(responseData);
            }
            return personnes;
        }

        public Personne GetPersonneByIdAPIPHP(int id)
        {
            HttpClient client;
            client = new HttpClient();
            var personne = new Personne();
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ServeurApiURL1"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync("api/single_read.php?IdPersonne=" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                personne = JsonConvert.DeserializeObject<Personne>(responseData);
            }
            return personne;
        }

        public bool AddPersonneAPIPHP(Personne personne)
        {
            bool rep = false;
            string idpersonne = personne.IdPersonne > 0 ? personne.IdPersonne.ToString() : string.Empty;
            var values = new Dictionary<string, string>
                    {
                        { "IdPersonne", idpersonne },
                        { "NomPrenomPersonne", personne.NomPrenomPersonne },
                        { "AdressePersonne", personne.AdressePersonne },
                        { "TelPersonne", personne.TelPersonne },
                        { "EmailPersonne", personne.EmailPersonne },
                        { "DateNaissancePersonne", personne.DateNaissancePersonne.ToString() }
                    };
            var content = new FormUrlEncodedContent(values);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ServeurApiURL1"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PostAsync("api/create.php", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        rep = true;
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {

            }
            return rep;
        }
        public bool UpdatePersonneAPIPHP(Personne personne)
        {
            bool rep = false;
            var values = new Dictionary<string, string>
                    {
                        { "IdPersonne", personne.IdPersonne.ToString() },
                        { "NomPrenomPersonne", personne.NomPrenomPersonne },
                        { "AdressePersonne", personne.AdressePersonne },
                        { "TelPersonne", personne.TelPersonne },
                        { "EmailPersonne", personne.EmailPersonne },
                        { "DateNaissancePersonne", personne.DateNaissancePersonne.ToString() }
                    };
            var content = new FormUrlEncodedContent(values);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ServeurApiURL1"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PostAsync("api/update.php", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        rep = true;
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {

            }
            return rep;
        }
        public bool DeletePersonneAPIPHP()
        {
            bool rep = false;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ServeurApiURL1"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.DeleteAsync("api/delete.php").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        rep = true;
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {

            }
            return rep;
        }


        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
