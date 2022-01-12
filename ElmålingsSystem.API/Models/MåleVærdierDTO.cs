using ElmålingsSystem.API.Infrastructure;
using System;

namespace ElmålingsSystem.API.Models
{
    public class MåleVærdierDTO
    {
        public int Id { get; set; }
        public DateTime AflæsningDato { get; set; }
        public double Tællerstand { get; set; }
        public int ForbrugKWH { get; set; }
    }
}
