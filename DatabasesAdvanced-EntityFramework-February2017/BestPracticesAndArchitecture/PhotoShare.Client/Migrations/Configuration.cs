namespace PhotoShare.Client.Migrations
{
    using System.Data.Entity.Migrations;

    using Client;
    using PhotoShare.Models;
    using System;
    using System.Collections.Generic;

    internal class Configuration : DbMigrationsConfiguration<PhotoShareContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PhotoShareContext context)
        {
            var usersToAdd = new User()
            {
                Username = "PeshoGoshov",
                Password = "Pesho_5",
                Email = "pesho.peshov@gmail.com",
                ProfilePicture = null,
                FirstName = "Pesho",
                LastName = "Peshov",
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now,
                Age = 99,
                IsDeleted = false,
                BornTown = new Town()
                {
                    Name = "Sofia",
                    Country = "Bulgaria"
                },
                CurrentTown = new Town()
                {
                    Name = "Sofia",
                    Country = "Bulgaria"
                },
                Friends = new List<User>()
                {
                    new User()
                    {
                        Username = "StamatStamatov",
                        Password = "Stamat_6",
                        Email = "stamat.stamatov@gmail.com",
                        ProfilePicture = null,
                        FirstName = "Stamat",
                        LastName = "Stamatov",
                        RegisteredOn = DateTime.Now,
                        LastTimeLoggedIn = DateTime.Now,
                        Age = 44,
                        IsDeleted = false,
                        BornTown = new Town()
                        {
                            Name = "Sofia",
                            Country = "Bulgaria"
                        },
                        CurrentTown = new Town()
                        {
                            Name = "Sofia",
                            Country = "Bulgaria"
                        }
                    },
                    new User()
                    {
                        Username = "GoshoGoshov",
                        Password = "Gosho_7",
                        Email = "gosho.goshov@gmail.com",
                        ProfilePicture = null,
                        FirstName = "Gosho",
                        LastName = "Goshov",
                        RegisteredOn = DateTime.Now,
                        LastTimeLoggedIn = DateTime.Now,
                        Age = 33,
                        IsDeleted = false,
                        BornTown = new Town()
                        {
                            Name = "Sofia",
                            Country = "Bulgaria"
                        },
                        CurrentTown = new Town()
                        {
                            Name = "Sofia",
                            Country = "Bulgaria"
                        }
                    }
                },
                AlbumRoles = new List<AlbumRole>()
                {
                    new AlbumRole()
                    {
                        Album = new Album()
                        {
                            IsPublic = true,
                            Name = "2017",
                            Tags = new List<Tag>()
                            {
                                new Tag()
                                {
                                    Name = "#newTag"
                                },
                                new Tag()
                                {
                                    Name = "#2017"
                                },
                                new Tag()
                                {
                                    Name = "#hashtag"
                                }
                            },
                            Pictures = new List<Picture>()
                            {
                                new Picture()
                                {
                                    Caption = "2017",
                                    Path = "www.google.com",
                                    Title = "2017"
                                },
                                new Picture()
                                {
                                    Caption = "2017 - 2",
                                    Path = "www.google.com",
                                    Title = "2017 - 2"
                                },
                                new Picture()
                                {
                                    Caption = "2017 - 3",
                                    Path = "www.google.com",
                                    Title = "2017 - 3"
                                }
                            },
                            BackgroundColor = Color.Blue,
                            AlbumRoles = new List<AlbumRole>()
                            {
                                new AlbumRole()
                                {
                                    Role = Role.Viewer
                                },
                                new AlbumRole()
                                {
                                    Role = Role.Owner
                                }
                            }
                        }
                    }
                }
            };

            context.Users.Add(usersToAdd);
            context.SaveChanges();
        }
    }
}
