using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElmålingsSystem.DAL.Entities
{
    public class Måleværdier
    {
        [Key]
        public int Id { get; set; }
        public DateTime AflæsningDato { get; set; }
        public double Tællerstand { get; set; }
        public int ForbrugKWH { get; set; }

        //Foreign Key
        [ForeignKey("Måler")]
        public int MålerId { get; set; }
        public Måler Måler { get; set; }
    }
}
