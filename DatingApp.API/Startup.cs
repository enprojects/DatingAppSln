using System;

using System.Text;
using System.Threading.Tasks;
using DatingApp.Common.Interfaces;
using DatingApp.Common.Interfaces.IRepos;
using DatingApp.Common.Interfaces.IServices;
using DatingApp.Common.Repositories;
using DatingApp.Common.Services;
using DatingApp.Data;
using DatingApp.Data.Helpers;
using DatingApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;



namespace DatingApp.API
{

    public class Startup
    {


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionManager.configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();

            services.AddDbContext<DatingContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped<IValuesRepos, ValuesRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IService, ValueServices>();

            services.AddScoped<IAuthService, AuthService>();

            #region  Ex1 get specific implemintation for IServMultipaleConcreate
            // in case we have multi services for the same interfae and we want 
            // register for specific serv 
            // in here we have 2 examples, the first one is by creating provider 
            // the second by generic 

            // https://www.codeproject.com/Tips/870246/How-to-register-and-use-Multiple-Classes-Implement
            // https://stackoverflow.com/questions/39174989/how-to-register-multiple-implementations-of-the-same-interface-in-asp-net-core

            services.AddScoped<ConcreateA>();
            services.AddScoped<ConcreateB>();
            services.AddTransient<Func<string, IServMultipaleConcreate>>(provider =>
            {
                return key =>
                {
                    if (key == "A")
                    {
                        return provider.GetService<ConcreateA>();
                    }
                    return provider.GetService<ConcreateB>();
                };
            });
            #endregion

            #region Ex2 get specific imp IServMultipaleConcreateGeneric 

            services.AddScoped<IServMultipaleConcreateGeneric<ConcreateC>, ConcreateC>();
            services.AddScoped<IServMultipaleConcreateGeneric<ConcreateD>, ConcreateD>();
            #endregion







            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Key").Value)),
                         ValidateIssuer=false,
                          ValidateAudience=false
                    };
                });




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
           // app.UseCors(x => x.AllowAnyOrigin());
            app.UseAuthentication();
            app.UseMvc();
            //    app.Use(async (context, next) =>
            //    {

            //        await context.Response.WriteAsync("test");

            //        await next.Invoke();
            //    });


            //    app.Use(async (context, next) =>
            //    {

            //        await context.Response.WriteAsync("test 2");

            //        return;
            //    });
            //}
        }
    }

}