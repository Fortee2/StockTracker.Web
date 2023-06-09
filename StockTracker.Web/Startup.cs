﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockTracker.Infrastructure.Investing;
using Microsoft.EntityFrameworkCore;
using System;
using StockTracker.Infrastructure.Repository.Interfaces;
using StockTracker.Infrastructure.Repository;
using StockTracker.Business.Services.Interfaces;
using StockTracker.Business.Services;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

namespace StockTracker.Web
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

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            
            services.AddDbContext<InvestingContext>(options => options.UseMySQL(Configuration.GetConnectionString("InvestingDatabase")));

            services.AddScoped<ISecuritiesRepo, SecuritiesRepository>();
            services.AddTransient<IActivitiesRepo, ActivitiesRepository>();
            services.AddScoped<IMovingAverageRepo, MovingAveragesRepo>();
            services.AddScoped<ISecuritiesService, SecuritiesService>();
            services.AddScoped<ISlopeService, SlopeService>();
            services.AddScoped<IChartService, ChartService>();
            services.AddScoped<IJobStatusRepo, JobStatusRepository>();
            services.AddScoped<IAveragesRepo, AveragesRepository>();
            services.AddScoped<IPortfolioRepo, PortfolioRepository>();
            services.AddScoped<IPriceDirectionRepo, PriceDirectionRepository>();
            services.AddScoped<IIndustrySectorRepo, IndustrySectorRepository>();
            services.AddScoped<IRsiRepo, RelativeStrengthRepository>();
            services.AddScoped<IAverageService, AveragesService>();
            services.AddScoped<IMACDService, MACDService>();
            services.AddScoped<IRelativeStrengthService, RelativeStrengthService>();
            services.AddScoped<IHighLowService, HighLowService>();
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddHostedService<MovingAveragesBackgroundService>();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                }
            });
        }
    }
}
