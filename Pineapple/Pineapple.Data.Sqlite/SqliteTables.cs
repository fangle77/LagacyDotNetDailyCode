using Dapper;

namespace Pineapple.Data.Sqlite
{
    internal class SqliteTables
    {
        #region Tables
        private string Catalog =
            @"Create table if not exists Catalog
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
            @"Create table if not exists Category
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
            @"Create table if not exists CategoryItem
            (
                CategoryItemId  INTEGER primary key AUTOINCREMENT,
                Title           TEXT,
                SubTitle        TEXT,
                Time            TEXT,
                Content         TEXT,
                DisplayOrder    INTEGER
            )";

        private string Template =
            @" Create table if not exists Template
            (
                TemplateId      INTEGER primary key AUTOINCREMENT,
                TemplateName    TEXT,
                ViewFile        TEXT
            )";

        private string Navigation =
            @"Create table if not exists Navigation
            (
                NavigationId    INTEGER primary key AUTOINCREMENT,
                NavigationName  TEXT,
                DisplayText     TEXT,
                SubText         TEXT,
                ParentId        INTEGER,
                DisplayOrder    INTEGER
            )";

        private string Attachment =
            @"Create table if not exists Attachment
            (
                AttachmentId    INTEGER primary key AUTOINCREMENT,
                OriginName      TEXT,
                FileName        TEXT,
                Path            TEXT,
                ContentType     TEXT,
                Type            TEXT,
                Size            INT,
                Alt             TEXT,
                Version         INT
            )";

        #endregion

        #region Mappings

        private string CatalogNavigationMapping =
            @" Create table if not exists CatalogNavigationMapping
            (
                MappingId       INTEGER primary key AUTOINCREMENT,
                CatalogId       INTEGER,
                NavigationId    INTEGER
            )";

        private string CatalogCategoryMapping =
           @" Create table if not exists CatalogCategoryMapping
            (
                MappingId       INTEGER primary key AUTOINCREMENT,
                CatalogId       INTEGER,
                CategoryId      INTEGER
            )";

        private string CategoryNavigationMapping =
            @" Create table if not exists CategoryNavigationMapping
            (
                MappingId       INTEGER primary key AUTOINCREMENT,
                CategoryId      INTEGER,
                NavigationId    INTEGER
            )";

        private string CategoryItemMapping =
           @" Create table if not exists CategoryItemMapping
            (
                MappingId       INTEGER primary key AUTOINCREMENT,
                CategoryId      INTEGER,
                CategoryItemId  INTEGER
            )";

        private string CategoryTemplateMapping =
          @" Create table if not exists CategoryTemplateMapping
            (
                MappingId       INTEGER primary key AUTOINCREMENT,
                CategoryId      INTEGER,
                TemplateId      INTEGER
            )";

        #endregion

        #region NoSql

        private string CompanyInfo =
            @" Create table if not exists CompanyInfo
            (
                Id       INTEGER primary key AUTOINCREMENT,
                Name     TEXT,
                Content    TEXT
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
                cnn.Execute(Attachment);
                cnn.Execute(CompanyInfo);
            }
        }
    }
}