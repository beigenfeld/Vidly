namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieInStock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "InStock", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "InStock");
        }
    }
}
