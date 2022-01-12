using ElmålingsSystem.API.Infrastructure;

namespace ElmålingsSystem.API.Models
{
    public class LejerKundeDTO
    {
        public int Id { get; set; }
        public int CprNr { get; set; }
        public string ForNavn { get; set; }
        public string EfterNavn { get; set; }
    }
}
