namespace MyForum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedcascadefunction : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Posts", new[] { "User_Id" });
            DropIndex("dbo.PostReplies", new[] { "User_Id" });
            AlterColumn("dbo.Posts", "User_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.PostReplies", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Posts", "User_Id");
            CreateIndex("dbo.PostReplies", "User_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PostReplies", new[] { "User_Id" });
            DropIndex("dbo.Posts", new[] { "User_Id" });
            AlterColumn("dbo.PostReplies", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Posts", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.PostReplies", "User_Id");
            CreateIndex("dbo.Posts", "User_Id");
        }
    }
}
