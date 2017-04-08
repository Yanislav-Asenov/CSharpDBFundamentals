namespace TeamBuilder.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 25),
                        Description = c.String(maxLength: 250),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "IX_Teams_Name",
                                    new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { IsUnique: True }")
                                },
                            }),
                        Description = c.String(maxLength: 32),
                        Acronym = c.String(nullable: false, maxLength: 3, fixedLength: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Invitations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvitedUserId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.InvitedUserId)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .Index(t => t.InvitedUserId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 25,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "IX_Users_Username",
                                    new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { IsUnique: True }")
                                },
                            }),
                        FirstName = c.String(maxLength: 25),
                        LastName = c.String(maxLength: 25),
                        Password = c.String(maxLength: 30),
                        Gender = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamEvents",
                c => new
                    {
                        TeamId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamId, t.EventId })
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.UserTeams",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.TeamId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invitations", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Invitations", "InvitedUserId", "dbo.Users");
            DropForeignKey("dbo.UserTeams", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.UserTeams", "UserId", "dbo.Users");
            DropForeignKey("dbo.TeamEvents", "EventId", "dbo.Events");
            DropForeignKey("dbo.TeamEvents", "TeamId", "dbo.Teams");
            DropIndex("dbo.UserTeams", new[] { "TeamId" });
            DropIndex("dbo.UserTeams", new[] { "UserId" });
            DropIndex("dbo.TeamEvents", new[] { "EventId" });
            DropIndex("dbo.TeamEvents", new[] { "TeamId" });
            DropIndex("dbo.Invitations", new[] { "TeamId" });
            DropIndex("dbo.Invitations", new[] { "InvitedUserId" });
            DropTable("dbo.UserTeams");
            DropTable("dbo.TeamEvents");
            DropTable("dbo.Users",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "Username",
                        new Dictionary<string, object>
                        {
                            { "IX_Users_Username", "IndexAnnotation: { IsUnique: True }" },
                        }
                    },
                });
            DropTable("dbo.Invitations");
            DropTable("dbo.Teams",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "Name",
                        new Dictionary<string, object>
                        {
                            { "IX_Teams_Name", "IndexAnnotation: { IsUnique: True }" },
                        }
                    },
                });
            DropTable("dbo.Events");
        }
    }
}
