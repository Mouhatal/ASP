using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfGL.Models;

namespace WcfGL
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {
        private BdWebGlContext db = new BdWebGlContext();

        public bool AddPersonne(Personne personne)
        {
            bool rep = false;
            try
            {
                db.personnes.Add(personne);
                db.SaveChanges();
                rep = true;
            }
            catch (Exception ex)
            {

            }
            return rep;
        }

        public bool DeletePersonne(int id)
        {
            bool rep = false;
            try
            {
                Personne personne = db.personnes.Find(id);
                db.personnes.Remove(personne);
                db.SaveChanges();
                rep = true;
            }
            catch (Exception ex)
            {

            }
            return rep;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public Personne GetPersonneByID(int id)
        {
            return db.personnes.Find(id);
        }

        //public List<Personne> GetPersonnes(
        //{
        //    return db.personnes.ToList();
        //}

        public List<Personne> GetPersonnes(string TelPersonne, string DateNaissancePersonne)
        {
            var personne = db.personnes.ToList();
            if (!string.IsNullOrEmpty(TelPersonne))
            {
                personne=personne.Where(a => a.TelPersonne.ToUpper().Contains(TelPersonne.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(DateNaissancePersonne))
            {
                DateTime laDate = DateTime.Parse(DateNaissancePersonne);
                personne = personne.Where(a => a.DateNaissancePersonne == laDate).ToList();
            }
            return personne;
        }

        public bool UpdatePersonne(Personne personne)
        {
            bool rep = false;
            try
            {
                db.Entry(personne).State = EntityState.Modified;
                db.SaveChanges();
                rep = true;
            }
            catch (Exception ex)
            {

            }
            return rep;
        }

        //public Personne GetPersonneByID(int id)
        //{
        //    return db.personnes.Find(IdPer);
        //}
    }


}
