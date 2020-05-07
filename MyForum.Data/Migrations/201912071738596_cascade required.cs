namespace MyForum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascaderequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Forum_Id", "dbo.Fora");
            DropForeignKey("dbo.PostReplies", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Posts", new[] { "Forum_Id" });
            DropIndex("dbo.PostReplies", new[] { "Post_Id" });
            AlterColumn("dbo.Posts", "Forum_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.PostReplies", "Post_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "Forum_Id");
            CreateIndex("dbo.PostReplies", "Post_Id");
            AddForeignKey("dbo.Posts", "Forum_Id", "dbo.Fora", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostReplies", "Post_Id", "dbo.Posts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostReplies", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "Forum_Id", "dbo.Fora");
            DropIndex("dbo.PostReplies", new[] { "Post_Id" });
            DropIndex("dbo.Posts", new[] { "Forum_Id" });
            AlterColumn("dbo.PostReplies", "Post_Id", c => c.Int());
            AlterColumn("dbo.Posts", "Forum_Id", c => c.Int());
            CreateIndex("dbo.PostReplies", "Post_Id");
            CreateIndex("dbo.Posts", "Forum_Id");
            AddForeignKey("dbo.PostReplies", "Post_Id", "dbo.Posts", "Id");
            AddForeignKey("dbo.Posts", "Forum_Id", "dbo.Fora", "Id");
        }
    }
}
