using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Dapper;

namespace Pineapple.Data
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
                ICP         TEXT
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
                SubDisplayName  TEXT,
                Status          TEXT
            )";

        private string CategoryItem =
            @"Create Table CategoryItem
            (
                CategoryItemId  INTEGER primary key AUTOINCREMENT,
                CategoryId      INTEGER,
                DisplayOrder    INTEGER,
                Title           TEXT,
                SubTitle        TEXT,
                Time            TEXT,
                DisplayText     TEXT,
                Image           TEXT,
                Status          TEXT
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