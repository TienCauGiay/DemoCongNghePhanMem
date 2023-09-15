﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Demo.CongNghePhanMem.Core.Exceptions;
using Demo.CongNghePhanMem.Core.Interfaces.Infrastructures;
using Demo.CongNghePhanMem.Core.Interfaces.Services;
using Demo.CongNghePhanMem.Core.Middlewares;
using Demo.CongNghePhanMem.Core.Services;
using Demo.CongNghePhanMem.Infrastructure.Repository;
using System.Net;
using System.Runtime.Versioning;
using Demo.CongNghePhanMem.Core.Resources;
using Demo.CongNghePhanMem.Core.Interfaces.UnitOfWork;
using Demo.CongNghePhanMem.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// validate dữ liệu 
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = actioncontext =>
    {
        var modelstate = actioncontext.ModelState;
        var errors = new Dictionary<string, string>();

        foreach (var entry in modelstate)
        {
            var key = entry.Key;
            var valueerrors = entry.Value.Errors.Select(error => error.ErrorMessage);
            var errormessage = string.Join(", ", valueerrors);

            errors[key] = errormessage;
        }

        return new BadRequestObjectResult(new BaseException
        {
            ErrorCode = (int)HttpStatusCode.BadRequest,
            DevMessage = ResourceVN.Validate_User_Input_Error,
            UserMessage = ResourceVN.Validate_User_Input_Error,
            TraceId = "",
            MoreInfo = "",
            Data = errors
        });
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Sử dụng thư viện AutoMapter để chuyển đổi giữa các đối tượng dữ liệu
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Policy
builder.Services.AddCors();

var connectionString = builder.Configuration["ConnectionString"];

// Tiêm DI
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));

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

// Middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.Run();
