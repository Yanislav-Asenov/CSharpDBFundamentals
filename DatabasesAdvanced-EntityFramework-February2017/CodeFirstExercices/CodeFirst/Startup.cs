namespace CodeFirst
{
    using Gringotts.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;

    class Startup
    {
        private static readonly GringottsDbContext _dbContext = new GringottsDbContext();

        static void Main()
        {
            //CreateDbAndInsertData(); 07
            //CreateUsersTableAndInsertData(); 08
        }

        static void CreateUsersTableAndInsertData()
        {
            var usersToAdd = new List<User>()
            {
                new User
                {
                    Username = "Pesho",
                    Age = 50,
                    Email = "pesho@gmail.com",
                    IsDeleted = false,
                    LastTimeLoggedIn = DateTime.Now,
                    Password = "1Af_10123",
                    ProfilePicture = null,
                    RegisteredOn = new DateTime(2013, 12, 12)
                },
                new User
                {
                    Username = "Gosho",
                    Age = 33,
                    Email = "gosho@gmail.com",
                    IsDeleted = false,
                    LastTimeLoggedIn = DateTime.Now,
                    Password = "1Af_30123",
                    ProfilePicture = null,
                    RegisteredOn = new DateTime(2014, 5, 5)
                },
                new User
                {
                    Username = "Stamat",
                    Age = 44,
                    Email = "stamat@gmail.com",
                    IsDeleted = false,
                    LastTimeLoggedIn = DateTime.Now,
                    Password = "1Af_34123",
                    ProfilePicture = null,
                    RegisteredOn = new DateTime(2015, 2, 2)
                }
            };

            _dbContext.Users.AddRange(usersToAdd);


            try
            {
                _dbContext.SaveChanges();
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

            _dbContext.WizzardDeposits.AddRange(recordsToAdd);

            try
            {
                _dbContext.SaveChanges();
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
