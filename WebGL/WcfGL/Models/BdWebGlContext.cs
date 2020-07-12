using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WcfGL.Models
{
    public class BdWebGlContext : DbContext
    {
        public BdWebGlContext() : base("ConneWebGL")
        {

        }
        public DbSet<Personne> personnes { get; set; }
    }
}