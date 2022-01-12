using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElmålingsSystem.DAL.Entities
{
    public class Installation
    {
        [Key]
        public int Id { get; set; }
        public double ForventetÅrsforbrug { get; set; }
        public int AflæsningsFrekvens { get; set; }
        public string Aflæsningsform { get; set; }
        public string Afbrydelsesart { get; set; }
        public string Tilslutningstype { get; set; }
        public string EffektgrænseAmpere { get; set; }
        public string EffektgrænseKW { get; set; }
        public string KommuneNavn { get; set; }
        public string VejNavn { get; set; }
        public string HusNummer { get; set; }
        public string Etage { get; set; }
        public string Dør { get; set; }
        public int PostNummer { get; set; }
        public string ByNavn { get; set; }
        public string LandeKode { get; set; }

        // outgoing relations
        public Måler Måler { get; set; }
        public LejerKunde LejerKunde { get; set; }

        // foreign keys
        [ForeignKey("EjerKunde")]
        public int EjerKundeId { get; set; }
        public EjerKunde EjerKunde { get; set; }
    }
}
