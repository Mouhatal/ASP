using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WcfGL.Models
{
    public class Personne
    {
        [Key]
        public int IdPersonne { get; set; }

        [Display(Name = "Nom et prénom"), Required(ErrorMessage = "*"), MaxLength(160)]
        public string NomPrenomPersonne { get; set; }

        [Display(Name = "Adresse"), Required(ErrorMessage = "*"), MaxLength(160)]
        public string AdressePersonne { get; set; }

        [Display(Name = "Téléphone"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string TelPersonne { get; set; }

        [Display(Name = "Email"), Required(ErrorMessage = "*"), MaxLength(80), DataType(DataType.EmailAddress)]
        public string EmailPersonne { get; set; }

        [Display(Name = "Date de naissance"), Required(ErrorMessage = "*"), DisplayFormat(DataFormatString = "{0:dd-MM-yyy}", ApplyFormatInEditMode = true)]
        public DateTime DateNaissancePersonne { get; set; }
    }
}