using ElmålingsSystem.API.Infrastructure;

namespace ElmålingsSystem.API.Models
{
    public class EjerKundeDTO
    {
        public int Id { get; set; }
        public int CprNr { get; set; }
        public string ForNavn { get; set; }
        public string EfterNavn { get; set; }
        public string VejNavn { get; set; }
        public string HusNummer { get; set; }
        public string Etage { get; set; }
        public string Dør { get; set; }
        public int PostNummer { get; set; }
        public string ByNavn { get; set; }
        public string KommuneNavn { get; set; }
    }
}
