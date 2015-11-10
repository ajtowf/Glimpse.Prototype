﻿using System;
using Glimpse.Agent;
using Glimpse.Server;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace Glimpse.AgentServer.AspNet.Mvc.Simple.Sample
{
    public class Startup
    {
        public Startup()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\nGLIMPSE AGENT+SERVER (SIMPLE) RUNNING ON PORT 5000");
            Console.WriteLine("==================================================\n");
            Console.ResetColor();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGlimpse();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpse();
            
            app.UseMvcWithDefaultRoute();
        }
    }
}
