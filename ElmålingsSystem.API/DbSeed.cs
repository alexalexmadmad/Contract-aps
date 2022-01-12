using ElmålingsSystem.DAL;
using ElmålingsSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElmålingsSystem.API
{
    public class DbSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MålingContext(
                serviceProvider.GetRequiredService<DbContextOptions<MålingContext>>()))
            {
                if (context.EjerKunder.Any())
                    return;

                context.EjerKunder.AddRange(
                    new EjerKunde
                    {
                        Id = 1,
                        ForNavn = "Ejer 1",
                    },
                    new EjerKunde
                    {
                        Id = 2,
                        ForNavn = "Ejer 2"
                    });
                context.SaveChanges();

                context.Installationer.AddRange(
                    new Installation
                    {
                        Id = 1,
                        AflæsningsFrekvens = 10,
                        VejNavn = "Vej",
                        HusNummer = "1",
                        EjerKundeId = 1
                    },
                    new Installation
                    {
                        Id = 2,
                        AflæsningsFrekvens = 10,
                        VejNavn = "Vej",
                        HusNummer = "2",
                        EjerKundeId = 1
                    },
                    new Installation
                    {
                        Id = 3,
                        AflæsningsFrekvens = 11,
                        VejNavn = "Vej",
                        HusNummer = "2a",
                        EjerKundeId = 1
                    },
                    new Installation
                    {
                        Id = 4,
                        AflæsningsFrekvens = 10,
                        VejNavn = "Vej",
                        HusNummer = "3",
                        EjerKundeId = 2
                    });
                context.SaveChanges();

                context.LejerKunder.AddRange(
                    new LejerKunde
                    {
                        Id = 1,
                        ForNavn = "Lejer 1",
                        EjerkundeId = 1,
                        InstallationId = 1
                    },
                    new LejerKunde
                    {
                        Id = 2,
                        ForNavn = "Lejer 2",
                        EjerkundeId = 1,
                        InstallationId = 2
                    },
                    new LejerKunde
                    {
                        Id = 3,
                        ForNavn = "Lejer 3",
                        EjerkundeId = 1,
                        InstallationId = 3
                    },
                    new LejerKunde
                    {
                        Id = 4,
                        ForNavn = "Lejer 4",
                        EjerkundeId = 2,
                        InstallationId = 4
                    });
                context.SaveChanges();

                context.Måler.AddRange(
                    new Måler
                    {
                        Id = 1,
                        Målertype = "spc",
                        InstallationId = 1,
                    },
                    new Måler
                    {
                        Id = 2,
                        Målertype = "apc",
                        InstallationId = 2,
                    },
                    new Måler
                    {
                        Id = 3,
                        Målertype = "apc",
                        InstallationId = 3,
                    },
                    new Måler
                    {
                        Id = 4,
                        Målertype = "spc",
                        InstallationId = 4,
                    });
                context.SaveChanges();

                int tempId = 1;
                for (int i = 1; i <= context.Måler.Count(); i++)
                {
                    for (int j = 10; j <= 35; j++)
                    {
                        context.Måleværdier.Add(
                            new Måleværdier
                            {
                                Id = tempId,
                                AflæsningDato = DateTime.Now.AddHours(j),
                                MålerId = i
                            });
                        context.SaveChanges();
                        tempId++;
                    }
                }
            }
        }
    }
}
