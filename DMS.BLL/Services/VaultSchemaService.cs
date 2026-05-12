using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Services
{

    public class VaultSchemaService
    {
        private readonly IConfiguration
            _configuration;

        public VaultSchemaService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task RunSchemaAsync(
            string databaseName)
        {
            var connectionString =
                _configuration
                    .GetConnectionString(
                        "MasterConnection");

            var builder =
                new SqlConnectionStringBuilder(
                    connectionString);

            builder.InitialCatalog =
                databaseName;

            using var connection =
                new SqlConnection(
                    builder.ConnectionString);

            await connection.OpenAsync();

            var sql = @"

-- =========================================
-- OBJECT TYPES
-- =========================================

IF NOT EXISTS
(
    SELECT *
    FROM sys.tables
    WHERE name = 'OBJECT_TYPES'
)
BEGIN

CREATE TABLE OBJECT_TYPES
(
    ObjectTypeID INT
        PRIMARY KEY IDENTITY,

    ObjectTypeName NVARCHAR(200),

    TableName NVARCHAR(200),

    HasFiles BIT DEFAULT 1,

    IsSystem BIT DEFAULT 0,

    IsActive BIT DEFAULT 1,

    CreatedOn DATETIME
        DEFAULT GETDATE(),

    CreatedBy INT
)

END

-- =========================================
-- OBJECT CLASSES
-- =========================================

IF NOT EXISTS
(
    SELECT *
    FROM sys.tables
    WHERE name = 'OBJECT_CLASSES'
)
BEGIN

CREATE TABLE OBJECT_CLASSES
(
    ClassID INT
        PRIMARY KEY IDENTITY,

    ObjectTypeID INT,

    ClassName NVARCHAR(200),

    Description NVARCHAR(MAX),

    IconName NVARCHAR(100),

    ColorCode NVARCHAR(50),

    IsActive BIT DEFAULT 1,

    CreatedOn DATETIME
        DEFAULT GETDATE(),

    CreatedBy INT
)

END

-- =========================================
-- OBJECT PROPERTIES
-- =========================================

IF NOT EXISTS
(
    SELECT *
    FROM sys.tables
    WHERE name = 'OBJECT_PROPERTIES'
)
BEGIN

CREATE TABLE OBJECT_PROPERTIES
(
    PropertyID INT
        PRIMARY KEY IDENTITY,

    ObjectTypeID INT,

    PropertyName NVARCHAR(200),

    DisplayName NVARCHAR(200),

    DataType NVARCHAR(50),

    ControlType NVARCHAR(50),

    IsRequired BIT DEFAULT 0,

    IsUnique BIT DEFAULT 0,

    IsSearchable BIT DEFAULT 1,

    IsSystem BIT DEFAULT 0,

    DefaultValue NVARCHAR(MAX),

    LookupObjectTypeID INT NULL,

    SortOrder INT,

    IsActive BIT DEFAULT 1
)

END

-- =========================================
-- OBJECT RECORDS
-- =========================================

IF NOT EXISTS
(
    SELECT *
    FROM sys.tables
    WHERE name = 'OBJECT_RECORDS'
)
BEGIN

CREATE TABLE OBJECT_RECORDS
(
    RecordID BIGINT
        PRIMARY KEY IDENTITY,

    ObjectTypeID INT,

    ClassID INT,

    Title NVARCHAR(500),

    CurrentVersion INT DEFAULT 1,

    IsDeleted BIT DEFAULT 0,

    CreatedOn DATETIME
        DEFAULT GETDATE(),

    CreatedBy INT,

    UpdatedOn DATETIME NULL,

    UpdatedBy INT NULL
)

END

-- =========================================
-- PROPERTY VALUES
-- =========================================

IF NOT EXISTS
(
    SELECT *
    FROM sys.tables
    WHERE name = 'OBJECT_PROPERTY_VALUES'
)
BEGIN

CREATE TABLE OBJECT_PROPERTY_VALUES
(
    ValueID BIGINT
        PRIMARY KEY IDENTITY,

    RecordID BIGINT,

    PropertyID INT,

    PropertyValue NVARCHAR(MAX)
)

END

-- =========================================
-- FILES
-- =========================================

IF NOT EXISTS
(
    SELECT *
    FROM sys.tables
    WHERE name = 'OBJECT_FILES'
)
BEGIN

CREATE TABLE OBJECT_FILES
(
    FileID BIGINT
        PRIMARY KEY IDENTITY,

    RecordID BIGINT,

    OriginalFileName NVARCHAR(500),

    StoredFileName NVARCHAR(500),

    FileExtension NVARCHAR(50),

    FilePath NVARCHAR(MAX),

    FileSize BIGINT,

    MimeType NVARCHAR(200),

    VersionNo INT,

    UploadedOn DATETIME
        DEFAULT GETDATE(),

    UploadedBy INT
)

END

-- =========================================
-- RELATIONS
-- =========================================

IF NOT EXISTS
(
    SELECT *
    FROM sys.tables
    WHERE name = 'OBJECT_RELATIONS'
)
BEGIN

CREATE TABLE OBJECT_RELATIONS
(
    RelationID BIGINT
        PRIMARY KEY IDENTITY,

    ParentRecordID BIGINT,

    ChildRecordID BIGINT,

    RelationType NVARCHAR(100)
)

END

";

            await connection.ExecuteAsync(sql);
        }
    }
}
