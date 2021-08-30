using System;
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using INDG.GRIP.Trader.Application;

namespace INDG.GRIP.Trader.Bootstrapper.Extensions
{
    internal static class AutoMapperStartupExtensions
    {
        internal static void AutoMapperConfigure(this IWebHostEnvironment env)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddApplicationMaps();
            });

            if (env != null && env.IsDevelopment())
            {
                try
                {
                    config.AssertConfigurationIsValid();
                }
                catch (Exception e)
                {
                    Log.Fatal(e.ToString());
                    Debugger.Break();
                    throw;
                }
            }
        }
    }
}