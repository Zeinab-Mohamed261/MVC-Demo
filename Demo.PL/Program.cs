using Demo.BLL.Profiles;
using Demo.BLL.Services.AttachmentService;
using Demo.BLL.Services.Classes;
using Demo.BLL.Services.Intrfaces;
using Demo.DAL.Data;
using Demo.DAL.Data.Repositries.Classes;
using Demo.DAL.Data.Repositries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Servive [DI]
            // Add services to the container.
            builder.Services.AddControllersWithViews(); 
            //builder.Services.AddSingleton<AppDbContext>(); //life time per application  [exception,caching]
            //builder.Services.AddScoped<AppDbContext>();    //life time per request   ??????**  //Allow DI for AppDbContext
            //builder.Services.AddTransient<AppDbContext>(); //life time per Operation in request
            builder.Services.AddDbContext<AppDbContext>(options=>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepositpry>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            #region Employee
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            
            //builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<IEmployeeService , EmployeeService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            #endregion
            #endregion

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            #region Configuren [Middlewares]
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication(); //owner
            //app.UseAuthorization();  //Admin

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion
            app.Run();
        }
    }
}
