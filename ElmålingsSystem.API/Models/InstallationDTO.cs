using ElmålingsSystem.API.Infrastructure;

namespace ElmålingsSystem.API.Models
{
    public class InstallationDTO
    {

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
    }
}
