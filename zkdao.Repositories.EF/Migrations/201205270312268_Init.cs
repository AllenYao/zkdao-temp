namespace zkdao.Repositories.EF.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "User",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ActEnum = c.Int(nullable: false),
                        RoleEnum = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 50),
                        Link = c.String(maxLength: 100),
                        QQ = c.String(maxLength: 50),
                        Weibo = c.String(maxLength: 50),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastLogin = c.DateTime(),
                        DateLastPasswordChange = c.DateTime(),
                        ApprovedID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "Info",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ActEnum = c.Int(nullable: false),
                        PosterID = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        ContentRich = c.String(),
                        LinkUrl = c.String(maxLength: 200),
                        LinkImage = c.String(maxLength: 200),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        GradeAverage = c.Int(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        User_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("User", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "InfoReply",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ActEnum = c.Int(nullable: false),
                        PosterID = c.Guid(nullable: false),
                        InfoID = c.Guid(nullable: false),
                        ContentRich = c.String(),
                        PlusAmount = c.Int(nullable: false),
                        MinusAmount = c.Int(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        User_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Info", t => t.InfoID, cascadeDelete: true)
                .ForeignKey("User", t => t.User_ID)
                .Index(t => t.InfoID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "InfoRelaTag",
                c => new
                    {
                        InfoID = c.Guid(nullable: false),
                        TagID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.InfoID, t.TagID })
                .ForeignKey("Info", t => t.InfoID, cascadeDelete: true)
                .ForeignKey("Tag", t => t.TagID, cascadeDelete: true)
                .Index(t => t.InfoID)
                .Index(t => t.TagID);
            
            CreateTable(
                "Tag",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ActEnum = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        GroupEnum = c.Int(nullable: false),
                        IsNecessary = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "ProductReply",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ActEnum = c.Int(nullable: false),
                        PosterID = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        AdvContentRich = c.String(),
                        ShortContentRich = c.String(),
                        PlusAmount = c.Int(nullable: false),
                        MinusAmount = c.Int(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        User_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("User", t => t.User_ID)
                .ForeignKey("Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.User_ID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "ReplyChild",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ActEnum = c.Int(nullable: false),
                        PosterID = c.Guid(nullable: false),
                        TargetID = c.Guid(nullable: false),
                        Content = c.String(),
                        CreatTime = c.DateTime(nullable: false),
                        User_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("User", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "UserRelaInfo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        InfoID = c.Guid(nullable: false),
                        GiveGrade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("User", t => t.UserID, cascadeDelete: true)
                .ForeignKey("Info", t => t.InfoID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.InfoID);
            
            CreateTable(
                "UserRelaProduct",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        UserToGoodsEnum = c.Int(nullable: false),
                        IsEager = c.Boolean(nullable: false),
                        GiveGrade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("User", t => t.UserID, cascadeDelete: true)
                .ForeignKey("Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "Product",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ActEnum = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Name = c.String(nullable: false, maxLength: 200),
                        Link = c.String(nullable: false, maxLength: 200),
                        PriceHistoryLink = c.String(maxLength: 200),
                        PriceHistory = c.String(),
                        Image = c.String(),
                        EagerAmount = c.Int(nullable: false),
                        EagerHistory = c.String(),
                        GradeAverage = c.Int(nullable: false),
                        GradeHistory = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "UserRelaReply",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        TargetID = c.Guid(nullable: false),
                        PlusOrMinus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropIndex("UserRelaReply", new[] { "UserID" });
            DropIndex("UserRelaProduct", new[] { "ProductID" });
            DropIndex("UserRelaProduct", new[] { "UserID" });
            DropIndex("UserRelaInfo", new[] { "InfoID" });
            DropIndex("UserRelaInfo", new[] { "UserID" });
            DropIndex("ReplyChild", new[] { "User_ID" });
            DropIndex("ProductReply", new[] { "ProductID" });
            DropIndex("ProductReply", new[] { "User_ID" });
            DropIndex("InfoRelaTag", new[] { "TagID" });
            DropIndex("InfoRelaTag", new[] { "InfoID" });
            DropIndex("InfoReply", new[] { "User_ID" });
            DropIndex("InfoReply", new[] { "InfoID" });
            DropIndex("Info", new[] { "User_ID" });
            DropForeignKey("UserRelaReply", "UserID", "User");
            DropForeignKey("UserRelaProduct", "ProductID", "Product");
            DropForeignKey("UserRelaProduct", "UserID", "User");
            DropForeignKey("UserRelaInfo", "InfoID", "Info");
            DropForeignKey("UserRelaInfo", "UserID", "User");
            DropForeignKey("ReplyChild", "User_ID", "User");
            DropForeignKey("ProductReply", "ProductID", "Product");
            DropForeignKey("ProductReply", "User_ID", "User");
            DropForeignKey("InfoRelaTag", "TagID", "Tag");
            DropForeignKey("InfoRelaTag", "InfoID", "Info");
            DropForeignKey("InfoReply", "User_ID", "User");
            DropForeignKey("InfoReply", "InfoID", "Info");
            DropForeignKey("Info", "User_ID", "User");
            DropTable("UserRelaReply");
            DropTable("Product");
            DropTable("UserRelaProduct");
            DropTable("UserRelaInfo");
            DropTable("ReplyChild");
            DropTable("ProductReply");
            DropTable("Tag");
            DropTable("InfoRelaTag");
            DropTable("InfoReply");
            DropTable("Info");
            DropTable("User");
        }
    }
}
