using Dapper;

namespace Pineapple.Data.Sqlite
{
    internal class SqliteTables
    {
        #region Tables
        private string Catalog =
            @"Create Table Catalog
            (
                CatalogId   INTEGER primary key AUTOINCREMENT,
                CatalogCode TEXT,
                CatalogName TEXT,
                Title       TEXT,
                ContentType TEXT,
                Keywords    TEXT,
                Description TEXT,
                Icon        TEXT,
                Logo        TEXT,
                CopyrightTime TEXT,
                Copyright   TEXT,
                ICP         TEXT,
                Status      TEXT
            )";

        private string Category =
            @"Create Table Category
            (
                CategoryId      INTEGER primary key AUTOINCREMENT,
                CategoryName    TEXT,
                DisplayName     TEXT,
                SubDisplayName  TEXT
                Description     TEXT,
                ParentId        INTEGER,
                DisplayOrder    INTEGER
            )";

        private string CategoryItem =
            @"Create Table CategoryItem
            (
                CategoryItemId  INTEGER primary key AUTOINCREMENT,
                Title           TEXT,
                SubTitle        TEXT,
                Time            TEXT,
                Content         TEXT,
                DisplayOrder    INTEGER
            )";

        private string Template =
            @" Create Table Template
            (
                TemplateId      INTEGER primary key AUTOINCREMENT,
                TemplateName    TEXT,
                ViewFile        TEXT
            )";

        private string Navigation =
            @"Create Table Navigation
            (
                NavigationId    INTEGER primary key AUTOINCREMENT,
                NavigationName  TEXT,
                DisplayText     TEXT,
                SubText         TEXT,
                ParentId        INTEGER,
                DisplayOrder    INTEGER
            )";

        #endregion

        #region Mappings

        private string CatalogNavigationMapping =
            @" Create Table CatalogNavigationMapping
            (
                MappingId       INTEGER primary key AUTOINCREMENT,
                CatalogId       INTEGER,
                NavigationId    INTEGER
            )";

        private string CatalogCategoryMapping =
           @" Create Table CatalogCategoryMapping
            (
                MappingId       INTEGER primary key AUTOINCREMENT,
                CatalogId       INTEGER,
                CategoryId      INTEGER
            )";

        private string CategoryNavigationMapping =
            @" Create Table CategoryNavigationMapping
            (
                MappingId       INTEGER primary key AUTOINCREMENT,
                CategoryId      INTEGER,
                NavigationId    INTEGER
            )";

        private string CategoryItemMapping =
           @" Create Table CategoryItemMapping
            (
                MappingId       INTEGER primary key AUTOINCREMENT,
                CategoryId      INTEGER,
                CategoryItemId  INTEGER
            )";

        private string CategoryTemplateMapping =
          @" Create Table CategoryTemplateMapping
            (
                MappingId       INTEGER primary key AUTOINCREMENT,
                CategoryId      INTEGER,
                TemplateId      INTEGER
            )";

        #endregion

        public void CreateTables()
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                cnn.Execute(Catalog);
                cnn.Execute(Category);
                cnn.Execute(CategoryItem);
                cnn.Execute(Template);
                cnn.Execute(Navigation);
                cnn.Execute(CatalogNavigationMapping);
                cnn.Execute(CatalogCategoryMapping);
                cnn.Execute(CategoryItemMapping);
                cnn.Execute(CategoryNavigationMapping);
                cnn.Execute(CategoryTemplateMapping);
            }
        }
    }
}