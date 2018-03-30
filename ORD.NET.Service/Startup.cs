using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ORD.NET.DAL;
using Swashbuckle.AspNetCore.Swagger;
using System;
using ORD.NET.Model.Business;
using ORD.NET.Model.DTO;

namespace ORD.NET.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer()
                    .AddDbContext<OrdContext>(options => options
                        .UseSqlServer(Configuration.GetConnectionString("OrdDb"))
                        .ConfigureWarnings(warnings => warnings
                            .Throw(RelationalEventId.QueryClientEvaluationWarning)
                        )
                    );

            services.AddMemoryCache();
            services.AddTransient<DbContext, OrdContext>();
            services.AddTransient<IDishRepository, DishRepository>();
            services.AddTransient<IDishTypeRepository, DishTypeRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IZeppelinRepository, ZeppelinRepository>();
            services.AddTransient<ILogMailRepository, LogMailRepository>();
            services.AddTransient<IGroupsRepository, GroupsRepository>();

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ORD.Net Api", Version = "v1" });
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Order, OrderDTO>()
                    .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data.Add(src.OraOrdinazione)));
            });

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ORD.Net API v1");
                c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Head);
            });

            app.UseMvc();
        }
    }
}
