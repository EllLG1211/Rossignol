using API_REST.DTOs;
using AutoMapper;
using Model.Business.Entries;
using Model.Business.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAutoMapper(cfg => cfg.CreateMap<AccountDTO, ConnectedUser>())
    .AddAutoMapper(cfg => cfg.CreateMap<MailedUserDTO, MailedUser>())
    .AddAutoMapper(cfg => cfg.CreateMap<SharedEntryDTO, SharedEntry>())
    //.AddAutoMapper(cfg => cfg.CreateMap<EntryDTO, Entry>())
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
