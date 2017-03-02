namespace CodeFirst
{
    using Gringotts.Models;
    using Hospital.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;
    using Users.Models;

    class Startup
    {
        private static readonly GringottsDbContext _gringottsDbContext = new GringottsDbContext();
        private static readonly HospitalDbContext _hospitalDbContext = new HospitalDbContext();
        private static readonly UsersDbContext _usersDbContext = new UsersDbContext();

        static void Main()
        {
            // CreateDbAndInsertData(); 07
            // CreateUsersTableAndInsertData(); 08 Method is commented out
            // CreateHospitalDbAndInsertData(); 09 - 10
            // CreateUsersDb(); 11 Data inserted through SQL Management Studio
            // PrintUsersbyEmailProvider 11
            // PrintUsersByEmailProvider("d.edu"); 11
            // PrintUsersByEmailProvider("t.org"); 11
            // RemoveInactiveUsers(new DateTime(2013, 5, 1)); 12
            // RemoveInactiveUsers(new DateTime(2014, 4, 10)); 12
            // RemoveInactiveUsers(new DateTime(2013, 12, 12)); 12
        }

        static void RemoveInactiveUsers(DateTime targetDate)
        {
            var usersToDelete = _usersDbContext.Users
                .Where(u => u.LastTimeLoggedIn < targetDate)
                .ToList();

            int numberOfUsersDeleted = 0;
            usersToDelete.ForEach(u =>
            {
                u.IsDeleted = true;
                numberOfUsersDeleted++;
            });

            _usersDbContext.SaveChanges();
            Console.WriteLine($"{numberOfUsersDeleted} users have been deleted");
        }

        static void PrintUsersByEmailProvider(string emailProvider)
        {
            var usersToPrint = _usersDbContext.Users
                .Where(u => u.Email.EndsWith(emailProvider))
                .Select(u => new
                {
                    Username = u.Username,
                    Email = u.Email
                })
                .ToList();

            StringBuilder content = new StringBuilder();

            usersToPrint.ForEach(u =>
            {
                if (u.Email.EndsWith(emailProvider))
                {
                    content.AppendLine($"{u.Username} {u.Email}");
                }
            });

            Console.Write(content);
        }

        static void CreateUsersDb()
        {
            var users = _usersDbContext.Users.ToList();
        }

        static void CreateHospitalDbAndInsertData()
        {
            var gosho = new Patient()
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
                Visitations = new List<Visit>()
                {
                    new Visit()
                    {
                        Date = new DateTime(2015, 5, 5),
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
            };

            _hospitalDbContext.Patients.Add(gosho);
            _hospitalDbContext.SaveChanges();
        }

        //static void CreateUsersTableAndInsertData()
        //{
        //    var usersToAdd = new List<User>()
        //    {
        //        new User
        //        {
        //            Username = "Pesho",
        //            Age = 50,
        //            Email = "pesho@gmail.com",
        //            IsDeleted = false,
        //            LastTimeLoggedIn = DateTime.Now,
        //            Password = "1Af_10123",
        //            ProfilePicture = null,
        //            RegisteredOn = new DateTime(2013, 12, 12)
        //        },
        //        new User
        //        {
        //            Username = "Gosho",
        //            Age = 33,
        //            Email = "gosho@gmail.com",
        //            IsDeleted = false,
        //            LastTimeLoggedIn = DateTime.Now,
        //            Password = "1Af_30123",
        //            ProfilePicture = null,
        //            RegisteredOn = new DateTime(2014, 5, 5)
        //        },
        //        new User
        //        {
        //            Username = "Stamat",
        //            Age = 44,
        //            Email = "stamat@gmail.com",
        //            IsDeleted = false,
        //            LastTimeLoggedIn = DateTime.Now,
        //            Password = "1Af_34123",
        //            ProfilePicture = null,
        //            RegisteredOn = new DateTime(2015, 2, 2)
        //        }
        //    };

        //    _gringottsDbContext.Users.AddRange(usersToAdd);

        //    try
        //    {
        //        _gringottsDbContext.SaveChanges();
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        foreach (var dbEntityValidationResult in ex.EntityValidationErrors)
        //        {
        //            foreach (var dbValidationError in dbEntityValidationResult.ValidationErrors)
        //            {
        //                Console.WriteLine(dbValidationError.ErrorMessage);
        //            }

        //        }
        //    }
        //}

        static void CreateDbAndInsertData()
        {
            var recordsToAdd = new List<WizardDeposit>()
                {
                    new WizardDeposit
                    {
                        FirstName = "Albus",
                        LastName = "Dumbledore",
                        Age = 150,
                        MagicWandCreator = "Antioch Peverell",
                        MagicWandSize = 15,
                        DepositStartDate = new DateTime(2016, 10, 20),
                        DepositExpirationDate = new DateTime(2020, 10, 20),
                        DepositAmount = 20000.24m,
                        DepositCharge = 0.2m,
                        IsDepositExpired = false
                    },
                    new WizardDeposit
                    {
                        FirstName = "Pesho",
                        LastName = "Peshov",
                        Age = 99,
                        MagicWandCreator = "Stamat Jivotnoto",
                        MagicWandSize = 19,
                        DepositStartDate = new DateTime(2017, 2, 20),
                        DepositExpirationDate = new DateTime(2021, 2, 20),
                        DepositAmount = 9999900.24m,
                        DepositCharge = 0.1m,
                        IsDepositExpired = false
                    },
                    new WizardDeposit
                    {
                        FirstName = "Gosho",
                        LastName = "Goshov",
                        Age = 33,
                        MagicWandCreator = "Maria",
                        MagicWandSize = 55,
                        DepositStartDate = new DateTime(2017, 3, 15),
                        DepositExpirationDate = new DateTime(2021, 3, 15),
                        DepositAmount = 55555.55m,
                        DepositCharge = 0.5m,
                        IsDepositExpired = false
                    }
                };

            _gringottsDbContext.WizzardDeposits.AddRange(recordsToAdd);

            try
            {
                _gringottsDbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var dbEntityValidationResult in ex.EntityValidationErrors)
                {
                    foreach (var dbValidationError in dbEntityValidationResult.ValidationErrors)
                    {
                        Console.WriteLine(dbValidationError.ErrorMessage);
                    }

                }
            }
        }
    }
}
