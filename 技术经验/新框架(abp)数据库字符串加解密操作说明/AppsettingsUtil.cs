using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyProject.Core.Utililty
{
    public class AppsettingsUtil
    {
        public static readonly Microsoft.Extensions.Configuration.IConfiguration Configuration;

        static AppsettingsUtil()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .Build();
        }

        public static T GetObjectSection<T>(string key) where T : class, new()
        {
            var obj = new ServiceCollection()
                .AddOptions()
                .Configure<T>(Configuration.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return obj;
        }

        public static string GetStringSection(string key)
        {
            return Configuration.GetValue<string>(key);
        }

        public static T GetSection<T>(string key)
        {
            return Configuration.GetValue<T>(key);
        }
    }
}
