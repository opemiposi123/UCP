using Identity.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UCP.Application.Implementation.Service;
using UCP.Application.Interface.Repository;
using UCP.Application.Interface.Service;
using UCP.Domain.Entity;
using UCP.Persistence.Context;
using UCP.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UCPDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<Member, IdentityRole>(opt =>
{

}).AddEntityFrameworkStores<UCPDbContext>()
.AddDefaultTokenProviders();
// Add services to the container.
builder.Services.AddControllersWithViews();
// REPOSITORY
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IRepository, RepositoryAsync>();
builder.Services.AddScoped<IApplyForLoanRepository, ApplyForLoanRepository>();
//SERVICES
builder.Services.AddScoped<IApplyForLoanService, ApplyForLoanService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IDashboardCountService, DashboardCountService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//await app.UseItToSeedSqlServer();
await app.UseItToSeedSqlServer();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
