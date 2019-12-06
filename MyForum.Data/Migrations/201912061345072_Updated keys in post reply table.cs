namespace MyForum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedkeysinpostreplytable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Forum_Id", "dbo.Fora");
            DropIndex("dbo.Posts", new[] { "Forum_Id" });
            AlterColumn("dbo.Posts", "Forum_Id", c => c.Int());
            CreateIndex("dbo.Posts", "Forum_Id");
            AddForeignKey("dbo.Posts", "Forum_Id", "dbo.Fora", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Forum_Id", "dbo.Fora");
            DropIndex("dbo.Posts", new[] { "Forum_Id" });
            AlterColumn("dbo.Posts", "Forum_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "Forum_Id");
            AddForeignKey("dbo.Posts", "Forum_Id", "dbo.Fora", "Id", cascadeDelete: true);
        }
    }
}
