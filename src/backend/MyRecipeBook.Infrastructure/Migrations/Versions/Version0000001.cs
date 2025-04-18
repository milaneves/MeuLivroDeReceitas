﻿using FluentMigrator;

namespace MyRecipeBook.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_USER,"Create table to save the user's information")]
    public class Version0000001 : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Users")
                 .WithColumn("Name").AsString(255).NotNullable()
                 .WithColumn("Email").AsString(255).NotNullable()
                 .WithColumn("Password").AsString(2000).NotNullable()
                 .WithColumn("UserIdentifier").AsGuid().NotNullable();
        }
    }
}
