namespace TeamBuilder.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamEvents", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamEvents", "EventId", "dbo.Events");
            DropForeignKey("dbo.Invitations", "TeamId", "dbo.Teams");
            DropIndex("dbo.TeamEvents", new[] { "TeamId" });
            DropIndex("dbo.TeamEvents", new[] { "EventId" });
            CreateTable(
                "dbo.EventTeams",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EventId, t.TeamId })
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.EventId)
                .Index(t => t.TeamId);
            
            AddColumn("dbo.Teams", "CreatorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Teams", "Name", c => c.String(nullable: false, maxLength: 25,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_Teams_Name",
                        new AnnotationValues(oldValue: "IndexAnnotation: { IsUnique: True }", newValue: null)
                    },
                }));
            CreateIndex("dbo.Teams", "CreatorId");
            AddForeignKey("dbo.Teams", "CreatorId", "dbo.Users", "Id");
            AddForeignKey("dbo.Invitations", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            DropTable("dbo.TeamEvents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeamEvents",
                c => new
                    {
                        TeamId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamId, t.EventId });
            
            DropForeignKey("dbo.Invitations", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.EventTeams", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.EventTeams", "EventId", "dbo.Events");
            DropForeignKey("dbo.Teams", "CreatorId", "dbo.Users");
            DropIndex("dbo.EventTeams", new[] { "TeamId" });
            DropIndex("dbo.EventTeams", new[] { "EventId" });
            DropIndex("dbo.Teams", new[] { "CreatorId" });
            AlterColumn("dbo.Teams", "Name", c => c.String(nullable: false, maxLength: 25,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_Teams_Name",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { IsUnique: True }")
                    },
                }));
            DropColumn("dbo.Teams", "CreatorId");
            DropTable("dbo.EventTeams");
            CreateIndex("dbo.TeamEvents", "EventId");
            CreateIndex("dbo.TeamEvents", "TeamId");
            AddForeignKey("dbo.Invitations", "TeamId", "dbo.Teams", "Id");
            AddForeignKey("dbo.TeamEvents", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamEvents", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
        }
    }
}
