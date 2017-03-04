namespace Hospital.Models.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Hospital.Models.HospitalDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Hospital.Models.HospitalDbContext";
        }

        protected override void Seed(HospitalDbContext context)
        {
            var visitations = new List<Visit>()
            {
                new Visit()
                {
                    Comment = "Test Comment",
                    Date = DateTime.Now,
                    Doctor = new Doctor()
                    {
                        Name = "Gosho",
                        Specialty = "Nothing"
                    },
                    Patient = new Patient()
                    {
                        FirstName = "Gosho",
                        LastName = "Goshov",
                        Address = "ul. Gosho",
                        DateOfBirth = new DateTime(2010, 12, 10),
                        Email = "gosho.goshov@gmail.com",
                        IsMedicalInsured = true,
                        Picture = null,
                        Diagnoses = new List<Diagnosis>()
                        {
                            new Diagnosis()
                            {
                                Name = "Остра вирусна инфекция",
                                Comment = "Ще се мре"
                            }
                        },
                        PerscribedMedicaments = new List<Medicament>()
                        {
                            new Medicament()
                            {
                                Name = "Варикобустер"
                            }
                        }
                    }
                },
                new Visit()
                {
                    Comment = "Test Comment 2 2",
                    Date = DateTime.Now,
                    Doctor = new Doctor()
                    {
                        Name = "Gosho 2",
                        Specialty = "Nothing 2"
                    },
                    Patient = new Patient()
                    {
                        FirstName = "Stamat 2",
                        LastName = "Stamat 2",
                        Address = "ul. Stamat 2",
                        DateOfBirth = new DateTime(2010, 12, 10),
                        Email = "Stamat2.Stamat2@gmail.com",
                        IsMedicalInsured = true,
                        Picture = null,
                        Diagnoses = new List<Diagnosis>()
                        {
                            new Diagnosis()
                            {
                                Name = "ОВИ",
                                Comment = "Няма да се мре"
                            }
                        },
                        PerscribedMedicaments = new List<Medicament>()
                        {
                            new Medicament()
                            {
                                Name = "Test Name"
                            }
                        }
                    }
                }
            };

            context.Visitations.AddRange(visitations);
            context.SaveChanges();
        }
    }
}
