using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Data;
using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibraryManagement.ViewModel;
using LibraryManagement.Data.Authentification;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyIdentityDbContext>(options => options.UseInMemoryDatabase("LibraryContext"));
            services.AddIdentity<MyIdentityUser, MyIdentityRole>()
            .AddEntityFrameworkStores<MyIdentityDbContext>()
            .AddDefaultTokenProviders();
            services.AddDbContext<LibraryDbContext>(options => options.UseInMemoryDatabase("LibraryContext"));
            services.AddTransient<UserManager<MyIdentityUser>>();
            services.AddTransient<SignInManager<MyIdentityUser>>();
            services.AddTransient<RoleManager<MyIdentityRole>>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); 
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Seed(app);
        }
    }
}
