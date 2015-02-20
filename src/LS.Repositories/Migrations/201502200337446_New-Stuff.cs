namespace RC.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewStuff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        ReleaseYear = c.DateTime(nullable: false),
                        PreferredFormat = c.Int(nullable: false),
                        Artist_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.Artist_Id, cascadeDelete: true)
                .Index(t => t.Artist_Id);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Song_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Songs", t => t.Song_Id)
                .Index(t => t.Song_Id);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Duration = c.Time(nullable: false, precision: 7),
                        Album_Id = c.String(maxLength: 128),
                        Artist_Id = c.String(maxLength: 128),
                        Artist_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.Album_Id)
                .ForeignKey("dbo.Artists", t => t.Artist_Id)
                .ForeignKey("dbo.Artists", t => t.Artist_Id1)
                .Index(t => t.Album_Id)
                .Index(t => t.Artist_Id)
                .Index(t => t.Artist_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "Artist_Id", "dbo.Artists");
            DropForeignKey("dbo.Songs", "Artist_Id1", "dbo.Artists");
            DropForeignKey("dbo.Artists", "Song_Id", "dbo.Songs");
            DropForeignKey("dbo.Songs", "Artist_Id", "dbo.Artists");
            DropForeignKey("dbo.Songs", "Album_Id", "dbo.Albums");
            DropIndex("dbo.Songs", new[] { "Artist_Id1" });
            DropIndex("dbo.Songs", new[] { "Artist_Id" });
            DropIndex("dbo.Songs", new[] { "Album_Id" });
            DropIndex("dbo.Artists", new[] { "Song_Id" });
            DropIndex("dbo.Albums", new[] { "Artist_Id" });
            DropTable("dbo.Songs");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
        }
    }
}
