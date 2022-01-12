using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElmålingsSystem.DAL.Entities
{
    public class LejerKunde
    {
        [Key]
        public int Id { get; set; }
        public int CprNr { get; set; }
        public string ForNavn { get; set; }
        public string EfterNavn { get; set; }

        // foreign key
        [ForeignKey("EjerKunde")]
        public int EjerkundeId { get; set; }
        public EjerKunde EjerKunde { get; set; }

        [ForeignKey("Installation")]
        public int InstallationId { get; set; }
        public Installation Installation { get; set; }
    }
}
