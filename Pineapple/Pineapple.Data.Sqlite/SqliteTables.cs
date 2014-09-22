using Dapper;

namespace Pineapple.Data.Sqlite
{
    public class SqliteTables
    {
        private string Catalog =
            @"Create Table Catalog
            (
                CatalogId   INTEGER primary key AUTOINCREMENT,
                CatalogCode TEXT,
                CatalogName TEXT,
                Title       TEXT,
                Description TEXT,
                Icon        TEXT,
                Logo        TEXT,
                CopyRight   TEXT,
                ICP         TEXT,
                Status      TEXT
            )";

        private string Category =
            @"Create Table Category
            (
                CategoryId      INTEGER primary key AUTOINCREMENT,
                CategoryName    TEXT,
                Description     TEXT,
                ParentId        INTEGER,
                DisplayOrder    INTEGER,
                DisplayName     TEXT,
                SubDisplayName  TEXT
            )";

        private string CategoryItem =
            @"Create Table CategoryItem
            (
                CategoryItemId  INTEGER primary key AUTOINCREMENT,
                DisplayOrder    INTEGER,
                Title           TEXT,
                SubTitle        TEXT,
                Time            TEXT,
                DisplayText     TEXT,
                Image           TEXT
            )";

        public void CreateTables()
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                cnn.Open();
                cnn.Execute(Catalog);
                cnn.Execute(Category);
                cnn.Execute(CategoryItem);
            }
        }
    }
}