using DMS.BLL.Interfaces;
using DMS.BLL.Security;
using DMS.BLL.Services;
using DMS.DAL.Dapper;
using DMS.DAL.Interfaces;
using DMS.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>(); 
builder.Services.AddScoped<IServerRepository, ServerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserSessionRepository, UserSessionRepository>();
builder.Services.AddScoped<IServerService, ServerService>();
builder.Services.AddScoped<IVaultRepository, VaultRepository>();
builder.Services.AddScoped<IVaultService, VaultService>();
builder.Services.AddScoped<IObjectTypeRepository,ObjectTypeRepository>();
builder.Services.AddScoped<IObjectClassRepository, ObjectClassRepository>();
builder.Services.AddScoped<IObjectTypeService,ObjectTypeService>();
builder.Services.AddScoped<IObjectPropertyRepository,ObjectPropertyRepository>();
builder.Services.AddScoped<IObjectPropertyService,ObjectPropertyService>();
builder.Services
    .AddScoped<
        IDynamicRecordRepository,
        DynamicRecordRepository>();

builder.Services
    .AddScoped<
        IDynamicRecordService,
        DynamicRecordService>();
builder.Services
    .AddScoped<
        ISearchRepository,
        SearchRepository>();

builder.Services
    .AddScoped<
        ISearchService,
        SearchService>();
builder.Services
    .AddScoped<
        IFileRepository,
        FileRepository>();

builder.Services
    .AddScoped<
        IFileService,
        FileService>();
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IObjectClassService,ObjectClassService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<SqlConnectionFactory>();
builder.Services.AddScoped<DatabaseCreatorService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<ServerService>();
builder.Services.AddScoped<UserRoleService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<VaultFolderService>();
builder.Services.AddScoped<VaultSchemaService>();
builder.Services.AddScoped<VaultPermissionService>();
builder.Services.AddScoped<PermissionService>();
builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var jwtKey =
    builder.Configuration["Jwt:Secret"]
    ?? throw new Exception("JWT Key Missing");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,

            ValidateAudience = true,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidAudience =  builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
});
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
