
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DutchTreat
{
    public class Startup
    {
        private IConfiguration _configs;
        public Startup(IConfiguration configs)
        {
            _configs = configs;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<StoreUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                //Passowrd
            }).AddEntityFrameworkStores<DutchContext>();

            services.AddAuthentication().
                AddCookie()
                .AddJwtBearer(cfg =>
                cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = _configs["Tokens:Issuer"],
                    ValidAudience = _configs["Tokens:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs["Tokens:Key"]))
                });//JWT Bearer Token

            services.AddDbContext<DutchContext>(cfg =>
            {
                cfg.UseSqlServer(_configs.GetConnectionString("DutchContextDb"));
            });
            services.AddTransient<DutchSeeder>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());//to use automapper
            services.AddScoped<IDutchRepository, DutchRepository>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation()
                .AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //added newtonsofjosn for handling the reference loops
            services.AddRazorPages(); //for using only razor pages in mvc 
            services.AddTransient<IMailService, NullMailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //} 
            // app.UseDefaultFiles();        //commented to enable  MVC6
            //#if DEBUG
            //            app.UseDeveloperExceptionPage(); // allow to view the exception and we have to restric tthis to developer only in debug mode
            //#endif

            //But the better way to handle the exception is environment wise not by running mode 
            //so instead of debug we can use env variable to tackle this

            if (env.IsDevelopment()) // it internally check the environment variable for the application // it internally calls env.IsEnvironment("Developmnet/testing etc");
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            app.UseStaticFiles();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("<html><body><H1>Hello World!</H1></body></html>");
            //});
            //normal run means -  it will always print the content of writeasync for any request or url.

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(cfg =>
            {
                cfg.MapRazorPages();
                cfg.MapControllerRoute("Default", "/{controller}/{action}/{id?}",
                new { controller = "App", action = "Index" });
            });
        }
    }
}
