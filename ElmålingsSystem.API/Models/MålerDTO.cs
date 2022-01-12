using ElmålingsSystem.API.Infrastructure;

namespace ElmålingsSystem.API.Models
{
    public class MålerDTO
    {
        public int Id { get; set; }
        public double MåleromregningsFaktor { get; set; }
        public string MålerCifre { get; set; }
        public string Målertype { get; set; }
        public string Målingsenhed { get; set; }
        public string MålerBeskrivelse { get; set; }
    }
}
