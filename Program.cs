using Microsoft.EntityFrameworkCore;
using ProblemAssignmnet2_SruthiKamisetti.Entities;
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddControllersWithViews();

string connStr = builder.Configuration.GetConnectionString("MyDatabase");
builder.Services.AddDbContext<CourseDBContext>(options => options.UseSqlServer(connStr));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
