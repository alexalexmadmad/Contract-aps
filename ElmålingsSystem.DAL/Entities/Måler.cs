using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElmålingsSystem.DAL.Entities
{
    public class Måler
    {
        [Key]
        public int Id { get; set; }
        public double MåleromregningsFaktor { get; set; }
        public string MålerCifre { get; set; }
        public string Målertype { get; set; }
        public string Målingsenhed { get; set; }
        public string MålerBeskrivelse { get; set; }

        //Foreign key 1 til mange 
        public ICollection<Måleværdier> Måleværdier { get; set; }

        //Foreign key
        [ForeignKey("Installation")]
        public int InstallationId { get; set; }
        public Installation Installation { get; set; }
    }
}
